using AutoMapper;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Application.DTOs.Dashboard;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.DTOs.SessionPackage;
using PhotoboothBranchService.Application.Services.BoothBranchServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.DashboardServices
{
    public class DashboardService : IDashboardService
    {
        private readonly IBranchRepository _boothBranchRepository;
        private readonly IBoothRepository _boothRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISessionOrderRepository _sessionOrderRepository;
        private readonly IPhotoSessionRepository _photoSessionRepository;
        private readonly IPhotoStickerRepository _photoStickerRepository;
        private readonly IServiceItemRepository _serviceItemRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;

        public DashboardService(IBranchRepository boothBranchRepository,
            IBoothRepository boothRepository,
            IAccountRepository accountRepository,
            ISessionOrderRepository sessionOrderRepository,
            IPhotoSessionRepository photoSessionRepository,
            IPhotoStickerRepository photoStickerRepository,
            IServiceItemRepository serviceItemRepository,
            IPhotoRepository photoRepository,
            IMapper mapper)
        {
            _boothBranchRepository = boothBranchRepository;
            _boothRepository = boothRepository;
            _accountRepository = accountRepository;
            _sessionOrderRepository = sessionOrderRepository;
            _photoSessionRepository = photoSessionRepository;
            _photoStickerRepository = photoStickerRepository;
            _serviceItemRepository = serviceItemRepository;
            _photoRepository = photoRepository;
            _mapper = mapper;
        }

        public async Task<BasicBranchDashboardResponse> BasicBranchDashboard(Guid branchID)
        {
            BasicBranchDashboardResponse response = new BasicBranchDashboardResponse();
            response.TotalStaff = (await _accountRepository.GetAsync(i => i.BranchID == branchID &&i.Status == AccountStatus.Active && i.Role == Domain.Enum.AccountRole.Staff)).Count();
            var booths = await _boothRepository.GetAsync(i => i.BranchID == branchID);
            response.TotalBooth = booths.Count();
            if (response.TotalBooth == 0)
            {
                response.TotalRevenue = 0;
                response.CountCustomer = 0;
                response.TotalOrder = 0;
                return response;
            }
            var orders = await _sessionOrderRepository.GetAsync(i => booths.Select(b => b.BoothID).ToList().Contains(i.BoothID));
            response.TotalOrder = orders.Count();
            if (response.TotalOrder == 0)
            {
                response.TotalRevenue = 0;
                response.CountCustomer = 0;
                return response;
            }
            response.CountCustomer = orders.GroupBy(o => o.AccountID).Count();
            response.TotalRevenue = orders.Sum(o => o.TotalPrice);
            return response;
        }
        public async Task<BasicDashboardResponse> BasicDashboard()
        {
            var response = new BasicDashboardResponse();
            var branchs = await _boothBranchRepository.GetAllAsync();
            response.CountCustomer = (await _accountRepository.GetAsync(i => i.Role == AccountRole.Customer && i.Status == AccountStatus.Active)).Count();
            response.TotalBranch = branchs.Count();
            if (response.TotalBranch == 0)
            {
                response.TotalRevenue = 0;
                response.TotalBooth = 0;
                response.TotalStaff = 0;
                response.TotalOrder = 0;
                return response;
            }
            var booths = await _boothBranchRepository.GetAllAsync();
            response.TotalBooth = booths.Count();
            if (response.TotalBooth == 0)
            {
                response.TotalRevenue = 0;
                response.TotalOrder = 0;
                return response;
            }
            var orders = await _sessionOrderRepository.GetAsync(i=>i.Status==SessionOrderStatus.Done);
            response.TotalOrder = orders.Count();
            response.TotalRevenue = response.TotalOrder == 0 ? 0 : orders.Sum(i => i.TotalPrice);
            return response;
        }
        public async Task<List<DashboardServiceResponse>> DashboradService(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var orders = await this.GetSessionOrders(branchID, startDate, endDate);
            var serviceItem = await _serviceItemRepository.GetAsync(i => orders.Select(o => o.SessionOrderID).ToList().Contains(i.SessionOrderID), i => i.Service);
            if (serviceItem.Count() == 0)
            {
                return new List<DashboardServiceResponse>();
            }
            var ServiceCount = serviceItem
                .GroupBy(i => i.Service)
                .Select(g => new DashboardServiceResponse
                {
                    Quantity = g.Sum(o => o.Quantity),
                    Service = _mapper.Map<ServiceResponse>(g.Key)
                })
                .OrderByDescending(i => i.Quantity);
            return ServiceCount.ToList();
        }
        public async Task<List<DashboardSessionPackageResponse>> DashboradSessionPackage(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var orders = await this.GetSessionOrders(branchID, startDate, endDate, i => i.SessionPackage);
            if (orders.Count() == 0)
            {
                return new List<DashboardSessionPackageResponse>();
            }
            try
            {
                var sessionPackageCount = orders
                    .GroupBy(i => i.SessionPackage)
                    .Select(g => new DashboardSessionPackageResponse
                    {
                        Count = g.Count(),
                        SessionPackage = _mapper.Map<SessionPackageResponse>(g.Key)
                    })
                    .OrderByDescending(i => i.Count);
                return sessionPackageCount.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<DashboardLayoutResponse>> DashboardLayout(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var photoSessions = await this.GetPhotoSessions(branchID, startDate, endDate, i => i.Layout);
            if (photoSessions.Count() == 0)
            {
                return new List<DashboardLayoutResponse>();
            }
            var layoutCount = photoSessions
                .GroupBy(i => i.Layout)
                .Select(g => new DashboardLayoutResponse
                {
                    Count = g.Count(),
                    Layout = _mapper.Map<LayoutResponse>(g.Key)
                })
                .OrderByDescending(i => i.Count);
            return layoutCount.ToList();
        }
        public async Task<List<DashboardBackgroundResponse>> DashboardBackground(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var photos = await this.GetPhotos(branchID, startDate, endDate, i => i.Background);
            if (photos.Count() == 0)
            {
                return new List<DashboardBackgroundResponse>();
            }
            var backgroundCount = photos
                .GroupBy(i => i.Background)
                .Select(g => new DashboardBackgroundResponse
                {
                    Count = g.Count(),
                    Background = _mapper.Map<BackgroundResponse>(g.Key)
                })
                .OrderByDescending(i => i.Count);
            return backgroundCount.ToList();
        }
        private async Task<List<Photo>> GetPhotos(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<Photo, object>>[] includeProperties)
        {
            var photoSessions = await this.GetPhotoSessions(branchID, startDate, endDate);
            return photoSessions.Count() == 0 ? new List<Photo>() : (await _photoRepository.GetAsync(p => photoSessions.Select(o => o.PhotoSessionID).ToList().Contains(p.PhotoSessionID), includeProperties)).ToList();
        }
        private async Task<List<PhotoSession>> GetPhotoSessions(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<PhotoSession, object>>[] includeProperties)
        {
            var orders = await this.GetSessionOrders(branchID, startDate, endDate);
            return orders.Count() == 0 ? new List<PhotoSession>() : (await _photoSessionRepository.GetAsync(i => orders.Select(o => o.SessionOrderID).ToList().Contains(i.SessionOrderID), includeProperties)).ToList();
        }
        private async Task<List<SessionOrder>> GetSessionOrders(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<SessionOrder, object>>[] includeProperties)
        {
            var booths = branchID.HasValue ? await _boothRepository.GetAsync(i => i.BranchID == branchID) : null;
            if (branchID.HasValue && booths.Count() == 0)
            {
                return new List<SessionOrder>();
            }

            Expression<Func<SessionOrder, bool>> pre = branchID.HasValue ? i => booths.Select(b => b.BoothID).ToList().Contains(i.BoothID) : i => true;
            pre = LinQHelper.AndAlso(pre, i => i.Status == SessionOrderStatus.Done);
            if (endDate != null && endDate != default(DateOnly))
            {
                pre = LinQHelper.AndAlso(pre, so => DateOnly.FromDateTime(so.EndTime.Value) <= endDate);
            }
            if (startDate != null && startDate != default(DateOnly))
            {
                pre = LinQHelper.AndAlso(pre, so => DateOnly.FromDateTime(so.StartTime) >= startDate);
            }

            return (await _sessionOrderRepository.GetAsync(pre, includeProperties)).ToList();
        }

    }
}
