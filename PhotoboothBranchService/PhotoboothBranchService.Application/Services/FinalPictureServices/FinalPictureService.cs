using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.FinalPictureServices
{
    public class FinalPictureService : IFinalPictureService
    {
        private readonly IFinalPictureRepository _finalPictureRepository;
        private readonly IMapper _mapper;

        public FinalPictureService(IFinalPictureRepository finalPictureRepository, IMapper mapper)
        {
            _finalPictureRepository = finalPictureRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateFinalPictureRequest createModel)
        {
            FinalPicture finalPicture = _mapper.Map<FinalPicture>(createModel);
            return await _finalPictureRepository.AddAsync(finalPicture);
        }

        public async Task DeleteAsync(Guid id)
        {
            var finalPicture = await _finalPictureRepository.GetAsync(f => f.PictureID == id);
            var finalPictureToDelete = finalPicture.FirstOrDefault();
            if (finalPictureToDelete != null)
            {
                await _finalPictureRepository.RemoveAsync(finalPictureToDelete);
            }
        }

        public async Task<IEnumerable<FinalPictureResponse>> GetAllAsync()
        {
            var finalPictures = await _finalPictureRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FinalPictureResponse>>(finalPictures.ToList());
        }

        public async Task<IEnumerable<FinalPictureResponse>> GetAllPagingAsync(FinalPictureFilter filter, PagingModel paging)
        {
            var finalPictures = (await _finalPictureRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var finalPicturesResponse = _mapper.Map<IEnumerable<FinalPictureResponse>>(finalPictures);
            finalPicturesResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return finalPicturesResponse;
        }

        public async Task<FinalPictureResponse> GetByIdAsync(Guid id)
        {
            var finalPictures = await _finalPictureRepository.GetAsync(f => f.PictureID == id);
            return _mapper.Map<FinalPictureResponse>(finalPictures);
        }

        public async Task UpdateAsync(Guid id, UpdateFinalPictureRequest updateModel)
        {
            var finalPicture = (await _finalPictureRepository.GetAsync(f => f.PictureID == id)).FirstOrDefault();
            if (finalPicture == null)
            {
                throw new KeyNotFoundException("Final picture not found.");
            }

            var updatedFinalPicture = _mapper.Map(updateModel, finalPicture);
            await _finalPictureRepository.UpdateAsync(updatedFinalPicture);
        }
    }
}
