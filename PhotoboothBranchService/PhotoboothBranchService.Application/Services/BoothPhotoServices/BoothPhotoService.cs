using AutoMapper;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.DTOs.BoothPhoto;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.BoothPhotoServices
{
    public class BoothPhotoService : IBoothPhotoService
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IBoothPhotoRepository _boothPhotoRepository;
        private readonly IMapper _mapper;

        public BoothPhotoService(ICloudinaryService cloudinaryService, IBoothPhotoRepository boothPhotoRepository, IMapper mapper)
        {
            _cloudinaryService = cloudinaryService;
            _boothPhotoRepository = boothPhotoRepository;
            _mapper = mapper;
        }
        public async Task DeleteAsync(Guid id)
        {
            var picture = (await _boothPhotoRepository.GetAsync(p => p.BoothPhotoId == id)).FirstOrDefault();
            if (picture == null)
            {
                throw new KeyNotFoundException("picture not found.");
            }
            await _boothPhotoRepository.RemoveAsync(picture);
            await _cloudinaryService.DeletePhotoAsync(picture.CouldID);
        }

        public async Task<IEnumerable<BoothPhotoResponse>> GetAllAsync()
        {
            var pictures = await _boothPhotoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BoothPhotoResponse>>(pictures);
        }

        public async Task<BoothPhotoResponse> GetByIdAsync(Guid id)
        {
            var picture = (await _boothPhotoRepository.GetAsync(p => p.BoothPhotoId == id)).FirstOrDefault();
            if (picture == null)
            {
                throw new KeyNotFoundException("picture not found.");
            }
            return _mapper.Map<BoothPhotoResponse>(picture);
        }
    }
}
