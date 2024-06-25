using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PhotoSessionServices
{
    public class PhotoSessionService : IPhotoSessionService
    {
        private readonly IPhotoSessionRepository _photoSessionRepository;
        private readonly IMapper _mapper;
        private readonly ISessionOrderRepository _sessionOrderRepository;
        public PhotoSessionService(IPhotoSessionRepository photoSessionRepository, IMapper mapper, ISessionOrderRepository sessionOrderRepository)
        {
            _photoSessionRepository = photoSessionRepository;
            _mapper = mapper;
            _sessionOrderRepository = sessionOrderRepository;
        }

        // Create
        public async Task<CreatePhotoSessionResponse> CreateAsync(CreatePhotoSessionRequest createModel)
        {
            var photoSession = _mapper.Map<PhotoSession>(createModel);
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
            listPhotoSessionResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listPhotoSessionResponse;
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

            var updatedPhotoSession = _mapper.Map(updateModel, photoSession);
            if (updatedPhotoSession.Status == Domain.Enum.PhotoSessionStatus.Canceled)
            {
                updatedPhotoSession.EndTime = DateTime.Now;
                await _photoSessionRepository.UpdateAsync(updatedPhotoSession);
                await _sessionOrderRepository.updateTotalPrice(updatedPhotoSession.SessionOrderID);
            } else
            {
                await _photoSessionRepository.UpdateAsync(updatedPhotoSession);
            }
        }

        public async Task<bool> ValidatePhotoSession(ValidateSessionPhotoRequest validateSessionPhotoRequest)
        {
            var photoSession = (await _photoSessionRepository
                .GetAsync(i => i.PhotoSessionID == validateSessionPhotoRequest.PhotoSessionID))
                .FirstOrDefault();
            if (photoSession != null)
            {
                if (photoSession.ValidateCode == validateSessionPhotoRequest.ValidateCode)
                {
                    TimeSpan difference = DateTime.Now - photoSession.StartTime;
                    photoSession.StartTime += difference;
                    photoSession.EndTime += difference;
                    photoSession.Status = Domain.Enum.PhotoSessionStatus.Ongoing;
                    await _photoSessionRepository.UpdateAsync(photoSession);
                    return true;
                } else
                {
                    throw new Exception("Wrong validate code, please try again");
                }
            } else
            {
                throw new NotFoundException("No photo session found, please resigter session with our staff");
            }
        }
    }
}
