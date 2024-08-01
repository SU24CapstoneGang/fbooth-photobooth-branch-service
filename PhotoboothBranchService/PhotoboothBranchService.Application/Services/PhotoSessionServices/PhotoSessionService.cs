﻿using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PhotoSessionServices
{
    public class PhotoSessionService : IPhotoSessionService
    {
        private readonly IPhotoSessionRepository _photoSessionRepository;
        private readonly IMapper _mapper;
        private readonly ILayoutRepository _layoutRepository;
        private readonly IBookingRepository _bookingRepository;
        public PhotoSessionService(IPhotoSessionRepository photoSessionRepository, IMapper mapper, ILayoutRepository layoutRepository, IBookingRepository bookingRepository)
        {
            _photoSessionRepository = photoSessionRepository;
            _mapper = mapper;
            _layoutRepository = layoutRepository;
            _bookingRepository = bookingRepository;
        }

        // Create
        public async Task<CreatePhotoSessionResponse> CreateAsync(CreatePhotoSessionRequest createModel)
        {
            var timeNow = DateTimeHelper.GetVietnamTimeNow();
            var validateBooking = (await _bookingRepository
                .GetAsync(i => i.BookingID == createModel.BookingID
                && (i.EndTime > timeNow && i.StartTime < timeNow)
                && i.Status == BookingStatus.Completed && !i.IsCancelled)) == null;
            if (validateBooking)
            {
                throw new Exception("Session Order are not going, it expired or not coming");
            }
            var layout = (await _layoutRepository.GetAsync(i => i.LayoutID == createModel.LayoutID)).FirstOrDefault();
            if (layout == null)
            {
                throw new NotFoundException("Not found Layout");
            }
            if (layout.Status == StatusUse.Unusable)
            {
                throw new BadRequestException("Layout from request is unable to use now");
            }
            var photoSession = _mapper.Map<PhotoSession>(createModel);
            photoSession.TotalPhotoTaken = layout.PhotoSlot;
            photoSession.SessionIndex = (await _photoSessionRepository.GetAsync(i => i.BookingID == createModel.BookingID)).Count() + 1;
            await _photoSessionRepository.AddAsync(photoSession);
            return _mapper.Map<CreatePhotoSessionResponse>(photoSession);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var photoSessions = await _photoSessionRepository.GetAsync(p => p.PhotoSessionID == id);
                var photoSession = photoSessions.FirstOrDefault();
                if (photoSession != null)
                {
                    await _photoSessionRepository.RemoveAsync(photoSession);
                }
            }
            catch
            {
                throw;
            }
        }

        // Read all
        public async Task<IEnumerable<PhotoSessionResponse>> GetAllAsync()
        {
            var photoSessions = await _photoSessionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoSessionResponse>>(photoSessions.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<PhotoSessionResponse>> GetAllPagingAsync(PhotoSessionFilter filter, PagingModel paging)
        {
            var photoSessions = (await _photoSessionRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listPhotoSessionResponse = _mapper.Map<IEnumerable<PhotoSessionResponse>>(photoSessions);
            return listPhotoSessionResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        // Read by ID
        public async Task<PhotoSessionResponse> GetByIdAsync(Guid id)
        {
            var photoSessions = await _photoSessionRepository.GetAsync(p => p.PhotoSessionID == id);
            var photoSession = photoSessions.FirstOrDefault();
            return _mapper.Map<PhotoSessionResponse>(photoSession);
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdatePhotoSessionRequest updateModel)
        {
            var photoSession = (await _photoSessionRepository.GetAsync(p => p.PhotoSessionID == id)).FirstOrDefault();
            if (photoSession == null)
            {
                throw new KeyNotFoundException("Photo session not found.");
            }

            //case update layout
            var updatedPhotoSession = _mapper.Map(updateModel, photoSession);
            if (updateModel.LayoutID.HasValue)
            {
                var layout = (await _layoutRepository.GetAsync(i => i.LayoutID == updateModel.LayoutID)).FirstOrDefault();
                if (layout != null)
                {
                    updatedPhotoSession.TotalPhotoTaken = layout.PhotoSlot;
                }
                else
                {
                    throw new NotFoundException("Not found Layout");
                }
            }
            //case end session
            if (updatedPhotoSession.Status == PhotoSessionStatus.Ended)
            {
                updatedPhotoSession.EndTime = DateTimeHelper.GetVietnamTimeNow();
            }

            await _photoSessionRepository.UpdateAsync(updatedPhotoSession);
        }
    }
}
