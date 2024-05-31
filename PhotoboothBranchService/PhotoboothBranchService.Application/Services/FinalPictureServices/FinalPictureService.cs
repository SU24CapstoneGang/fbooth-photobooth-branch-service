﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Application.Services.LayoutServices;
using PhotoboothBranchService.Application.Services.PrintPricingServices;
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
        private readonly IPrintPricingService _printPricingService;
        private readonly IPrintPricingRepository _printPricingRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ILayoutRepository _layoutRepository;

        public FinalPictureService(IFinalPictureRepository finalPictureRepository, IMapper mapper,
            ICloudinaryService cloudinaryService, ISessionRepository sessionRepository, IPrintPricingService printPricingService,
            IDiscountRepository discountRepository, ILayoutRepository layoutRepository)
        {
            _finalPictureRepository = finalPictureRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _sessionRepository = sessionRepository;
            _printPricingService = printPricingService;
            _discountRepository = discountRepository;
            _layoutRepository = layoutRepository;
        }

        public async Task<Guid> CreateAsync(CreateFinalPictureRequest createModel)
        {
            FinalPicture finalPicture = _mapper.Map<FinalPicture>(createModel);
            return await _finalPictureRepository.AddAsync(finalPicture);
        }

        public async Task<FinalPictureResponse> CreateFinalPictureAsync(IFormFile file, Guid branchID, int photoTaken, Guid layoutID, string? discountCode, Guid? accountID)
        {
            //var session = (await _sessionRepository.GetAsync(s => s.BranchesID.Equals(branchID) && s.FinalPicture == null)).FirstOrDefault();

            // Retrieve pricing information
            var layout = (await _layoutRepository.GetAsync(l => l.LayoutID.Equals(layoutID))).FirstOrDefault();
            if (layout == null)
            {
                throw new Exception("Layout not found.");
            }

            var printPricing = (await _printPricingRepository.GetAsync(p => p.MinQuantity < photoTaken || p.MinQuantity == photoTaken)).FirstOrDefault();
            if (printPricing == null)
            {
                throw new Exception("Print pricing not found.");
            }

            Discount discount = null;
            if (discountCode != null)
            {
                discount = (await _discountRepository.GetByCode(discountCode)).FirstOrDefault();
                if (discount == null)
                {
                    throw new Exception("Invalid discount code.");
                }
            }
            // Calculate total price per final picture
            decimal totalPrice = (decimal)layout.LayoutPrice  * photoTaken;

            // Apply discount if available
            if (discount != null)
            {
                totalPrice -= (totalPrice * (discount.DiscountRate / 100) * (printPricing.DiscountPerPrintNumber / 100));
            }

            // Calculate total price
            //decimal totalPrice = (decimal)layout.LayoutPrice + ((decimal)printPricing.UnitPrice * photoTaken);
            //if (discount != null)
            //{
            //    totalPrice -= (totalPrice * discount.DiscountRate / 100);
            //}

            // If no session exists for the branch, create a new session
            //if (session == null)
            //{
            var session = new Session
            {
                PhotosTaken = photoTaken,
                TotalPrice = (double)totalPrice,
                CreateDate = DateTime.UtcNow,
                BranchesID = branchID,
                PrintPricingID = printPricing.PrintPricingID,
                DiscountID = discount?.DiscountID
            };
            await _sessionRepository.AddAsync(session);
            //}

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
                SessionID = session.SessionID
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
