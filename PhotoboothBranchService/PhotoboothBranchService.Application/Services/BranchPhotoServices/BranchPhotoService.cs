using AutoMapper;
using PhotoboothBranchService.Application.DTOs.BoothPhoto;
using PhotoboothBranchService.Application.DTOs.BranchPhoto;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.BranchPhotoServices
{
    public class BranchPhotoService : IBranchPhotoService
    {
        private readonly IBranchPhotoRepository _branchPhotoRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public BranchPhotoService(IBranchPhotoRepository branchPhotoRepository, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            _branchPhotoRepository = branchPhotoRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }
        public async Task DeleteAsync(Guid id)
        {
            var picture = (await _branchPhotoRepository.GetAsync(p => p.BranchPhotoId == id)).FirstOrDefault();
            if (picture == null)
            {
                throw new KeyNotFoundException("picture not found.");
            }
            await _branchPhotoRepository.RemoveAsync(picture);
            await _cloudinaryService.DeletePhotoAsync(picture.CouldID);
        }

        public async Task<IEnumerable<BranchPhotoResponse>> GetAllAsync()
        {
            var pictures = await _branchPhotoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BranchPhotoResponse>>(pictures);
        }

        public async Task<BranchPhotoResponse> GetByIdAsync(Guid id)
        {
            var picture = (await _branchPhotoRepository.GetAsync(p => p.BranchPhotoId == id)).FirstOrDefault();
            if (picture == null)
            {
                throw new KeyNotFoundException("picture not found.");
            }
            return _mapper.Map<BranchPhotoResponse>(picture);
        }
    }
}
