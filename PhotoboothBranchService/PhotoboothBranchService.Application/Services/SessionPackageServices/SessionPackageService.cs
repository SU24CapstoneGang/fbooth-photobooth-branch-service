using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionPackage;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.SessionPackageServices
{
    public class SessionPackageService : ISessionPackageService
    {
        private readonly ISessionPackageRepository _sessionPackageRepository;
        private readonly IMapper _mapper;

        public SessionPackageService(ISessionPackageRepository sessionPackageRepository, IMapper mapper)
        {
            _sessionPackageRepository = sessionPackageRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<CreateSessionPackageResponse> CreateAsync(CreateSessionPackageRequest createModel)
        {
            var sessionPackage = _mapper.Map<SessionPackage>(createModel);
            await _sessionPackageRepository.AddAsync(sessionPackage);
            return _mapper.Map<CreateSessionPackageResponse>(sessionPackage);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var sessionPackages = await _sessionPackageRepository.GetAsync(s => s.SessionPackageID == id);
                var sessionPackage = sessionPackages.FirstOrDefault();
                if (sessionPackage != null)
                {
                    await _sessionPackageRepository.RemoveAsync(sessionPackage);
                }
            }
            catch
            {
                throw;
            }
        }

        // Read all
        public async Task<IEnumerable<SessionPackageResponse>> GetAllAsync()
        {
            var sessionPackages = await _sessionPackageRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SessionPackageResponse>>(sessionPackages.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<SessionPackageResponse>> GetAllPagingAsync(SessionPackageFilter filter, PagingModel paging)
        {
            var sessionPackages = (await _sessionPackageRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listSessionPackageResponse = _mapper.Map<IEnumerable<SessionPackageResponse>>(sessionPackages);
            return listSessionPackageResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        // Read by ID
        public async Task<SessionPackageResponse> GetByIdAsync(Guid id)
        {
            var sessionPackages = await _sessionPackageRepository.GetAsync(s => s.SessionPackageID == id);
            var sessionPackage = sessionPackages.FirstOrDefault();
            return _mapper.Map<SessionPackageResponse>(sessionPackage);
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateSessionPackageRequest updateModel)
        {
            var sessionPackage = (await _sessionPackageRepository.GetAsync(s => s.SessionPackageID == id)).FirstOrDefault();
            if (sessionPackage == null)
            {
                throw new KeyNotFoundException("Session package not found.");
            }

            var updatedSessionPackage = _mapper.Map(updateModel, sessionPackage);
            await _sessionPackageRepository.UpdateAsync(updatedSessionPackage);
        }
    }
}
