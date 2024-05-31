using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace PhotoboothBranchService.Application.Services.FinalPictureServices
{
    public class FinalPictureService : IFinalPictureService
    {
        private readonly IFinalPictureRepository _finalPictureRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ISessionRepository _sessionRepository;

        public FinalPictureService(IFinalPictureRepository finalPictureRepository, IMapper mapper, 
            ICloudinaryService cloudinaryService, ISessionRepository sessionRepository)
        {
            _finalPictureRepository = finalPictureRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _sessionRepository = sessionRepository;
        }

        public async Task<Guid> CreateAsync(CreateFinalPictureRequest createModel)
        {
            FinalPicture finalPicture = _mapper.Map<FinalPicture>(createModel);
            return await _finalPictureRepository.AddAsync(finalPicture);
        }

        public async Task<FinalPictureResponse> CreateFinalPictureAsync(IFormFile file, Guid SessionID)
        {

            var uploadResult = await _cloudinaryService.AddPhotoAsync(file);
            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            var finalPicture = new FinalPicture
            {
                PictureURl = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                CreateDate = DateTime.UtcNow,
                PicturePrivacy = PhotoPrivacy.Public,
                SessionID = SessionID
            };

            await _finalPictureRepository.AddAsync(finalPicture);
            return new FinalPictureResponse
            {
                PictureID = finalPicture.PictureID,
                PictureURl = finalPicture.PictureURl,
                CreateDate = finalPicture.CreateDate,
                PicturePrivacy = finalPicture.PicturePrivacy
            };
        }

        //public async Task<FinalPictureResponse> CreateFinalPictureAsync(IFormFile file)
        //{
        //    // Upload the photo to Cloudinary
        //    var uploadResult = await _cloudinaryService.AddPhotoAsync(file);
        //    if (uploadResult.Error != null)
        //    {
        //        throw new Exception(uploadResult.Error.Message);
        //    }
        //    var us = _photoBoothBranchRepository.GetAsync(p => p.AccountID == );

        //    // Create a new session
        //    var session = new Session
        //    {
        //        CreateDate = DateTime.UtcNow,
        //        PhotosTaken = request.PrintQuantity,
        //        TotalPrice = 0.0, // Set your logic for the total price
        //        BranchesID = ,
        //        DiscountID = request.DiscountID,
        //        PrintPricingID = request.PrintPricingID,
        //        AccountID = request.AccountID
        //    };

        //    // Create a new final picture
        //    var finalPicture = new FinalPicture
        //    {
        //        PictureURl = uploadResult.SecureUrl.AbsoluteUri,
        //        PublicId = uploadResult.PublicId,
        //        CreateDate = DateTime.UtcNow,
        //        PicturePrivacy = PhotoPrivacy.Public,
        //        SessionID = session.SessionID,
        //        Session = session
        //    };

        //    _sessionRepository.AddAsync(session);
        //    _finalPictureRepository.AddAsync(finalPicture);

        //    return new FinalPictureResponse
        //    {
        //        PictureID = finalPicture.PictureID,
        //        PictureURl = finalPicture.PictureURl,
        //        CreateDate = finalPicture.CreateDate,
        //        PicturePrivacy = finalPicture.PicturePrivacy
        //    };
        //}

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
