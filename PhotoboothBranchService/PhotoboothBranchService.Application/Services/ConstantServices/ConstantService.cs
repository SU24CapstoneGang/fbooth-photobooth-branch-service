using AutoMapper;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs.Constant;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ConstantServices
{
    public class ConstantService : IConstantService
    {
        private readonly IConstantRepository _constantRepository;
        private readonly Dictionary<string, string> _constants;
        private readonly IMapper _mapper;

        public ConstantService(IConstantRepository constantRepository, IMapper mapper)
        {
            _constantRepository = constantRepository;
            _constants = new Dictionary<string, string>();
            _mapper = mapper;
        }

        public async Task LoadConstantsAsync()
        {
            var constants = (await _constantRepository.GetAllAsync()).ToList();
            _constants.Clear();
            foreach (var constant in constants)
            {
                _constants[constant.ConstantKey] = constant.ConstantValue;
            }
        }
        public string GetConstantValue(string key)
        {
            return _constants.TryGetValue(key, out var value) ? value : null;
        }

        public async Task UpdateConstantAsync(string key, UpdateConstantRequest request)
        {
            var constant = (await _constantRepository.GetAsync(i => i.ConstantKey == key)).FirstOrDefault();
            if (constant == null)
            {
                throw new NotFoundException("Not found Constant");
            }
            if (request.ConstantValue  != null)
            {
                if (!ValidateConstantValue(request.ConstantValue, constant.ConstantType))
                {
                    throw new BadRequestException("Invalid value for the specified constant type.");
                }
            }
            
            var updateConstant = _mapper.Map(request, constant);
            await _constantRepository.UpdateAsync(updateConstant);
            await LoadConstantsAsync();
        }
        private bool ValidateConstantValue(string value, ConstantType constantType)
        {
            return constantType switch
            {
                ConstantType.Int => int.TryParse(value, out _),
                ConstantType.Decimal => decimal.TryParse(value, out _),
                ConstantType.Bool => bool.TryParse(value, out _),
                ConstantType.DateTime => DateTime.TryParse(value, out _),
                ConstantType.Guid => Guid.TryParse(value, out _),
                ConstantType.Time => TimeSpan.TryParseExact(value, "hh\\:mm", null, out _),
                ConstantType.String => true, // No validation needed for strings
                _ => false, // Default case for any unknown ConstantType
            };
        }
    }
}
