//using AutoMapper;
//using PhotoboothBranchService.Application.Common.Exceptions;
//using PhotoboothBranchService.Application.DTOs.Background;
//using PhotoboothBranchService.Application.DTOs.Dashboard;
//using PhotoboothBranchService.Application.DTOs.Layout;
//using PhotoboothBranchService.Application.DTOs.Sticker;
//using PhotoboothBranchService.Domain.Common.Helper;
//using PhotoboothBranchService.Domain.Entities;
//using PhotoboothBranchService.Domain.Enum;
//using PhotoboothBranchService.Domain.IRepository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace PhotoboothBranchService.Application.Services.DashboardServices
//{
//    public class DashboardService : IDashboardService
//    {
//        private readonly IBranchRepository _branchRepository;
//        private readonly IBoothRepository _boothRepository;
//        private readonly IAccountRepository _accountRepository;
//        private readonly IBookingRepository _sessionOrderRepository;
//        private readonly IPhotoSessionRepository _photoSessionRepository;
//        private readonly IPhotoStickerRepository _photoStickerRepository;
//        private readonly IBookingServiceRepository _serviceItemRepository;
//        private readonly IPhotoRepository _photoRepository;
//        private readonly ILayoutRepository _layoutRepository;
//        private readonly IBackgroundRepository _backgroundRepository;
//        private readonly IStickerRepository _stickerRepository;
//        private readonly IMapper _mapper;

//        public DashboardService(IBranchRepository branchRepository,
//            IBoothRepository boothRepository,
//            IAccountRepository accountRepository,
//            IBookingRepository sessionOrderRepository,
//            IPhotoSessionRepository photoSessionRepository,
//            IPhotoStickerRepository photoStickerRepository,
//            IBookingServiceRepository serviceItemRepository,
//            IPhotoRepository photoRepository,
//            IMapper mapper,
//            ILayoutRepository layoutRepository,
//            IBackgroundRepository backgroundRepository,
//            IStickerRepository stickerRepository)
//        {
//            _branchRepository = branchRepository;
//            _boothRepository = boothRepository;
//            _accountRepository = accountRepository;
//            _sessionOrderRepository = sessionOrderRepository;
//            _photoSessionRepository = photoSessionRepository;
//            _photoStickerRepository = photoStickerRepository;
//            _serviceItemRepository = serviceItemRepository;
//            _photoRepository = photoRepository;
//            _mapper = mapper;
//            _layoutRepository = layoutRepository;
//            _backgroundRepository = backgroundRepository;
//            _stickerRepository = stickerRepository;
//        }

//        public async Task<BasicBranchDashboardResponse> BasicBranchDashboard(Guid branchID)
//        {
//            BasicBranchDashboardResponse response = new BasicBranchDashboardResponse();
//            var staffs = await _accountRepository.GetAsync(i => i.BranchID == branchID && i.Role == AccountRole.Staff);
//            response.StaffDashboard.TotalStaff = staffs.Count();
//            if (response.StaffDashboard.TotalStaff != 0)
//            {
//                response.StaffDashboard.StaffActive = staffs.Select(i => i.Status == AccountStatus.Active).Count();
//                response.StaffDashboard.StaffBlocked = staffs.Select(i => i.Status == AccountStatus.Blocked).Count();
//            }
//            var booths = await _boothRepository.GetAsync(i => i.BranchID == branchID);
//            response.BoothDashboard.TotalBooth = booths.Count();
            
//            if (response.BoothDashboard.TotalBooth == 0)
//            {
//                return response;
//            }
//            response.BoothDashboard.BoothMaintenance = booths.Select(i => i.Status == BoothStatus.Maintenance).Count();
//            response.BoothDashboard.BoothActive = booths.Select(i => i.Status == BoothStatus.Active).Count();
//            response.BoothDashboard.BoothInactive = booths.Select(i => i.Status == BoothStatus.Inactive).Count();
//            var orders = await _sessionOrderRepository.GetAsync(i => booths.Select(b => b.BoothID).ToList().Contains(i.BoothID));
//            response.TotalOrder = orders.Count();
//            if (response.TotalOrder == 0)
//            {
//                response.TotalRevenue = 0;
//                response.CountCustomer = 0;
//                return response;
//            }
//            response.CountCustomer = orders.GroupBy(o => o.CustomerID).Count();
//            response.TotalRevenue = orders.Sum(o => o.PaymentAmount);
//            return response;
//        }
//        public async Task<BasicDashboardResponse> BasicDashboard()
//        {
//            var response = new BasicDashboardResponse();
//            var branchs = await _branchRepository.GetAllAsync();
//            response.CountCustomer = (await _accountRepository.GetAsync(i => i.Role == AccountRole.Customer && i.Status == AccountStatus.Active)).Count();
//            response.TotalBranch = branchs.Count();
//            if (response.TotalBranch == 0)
//            {
//                response.TotalRevenue = 0;
//                response.TotalOrder = 0;
//                return response;
//            }
//            var booths = await _boothRepository.GetAllAsync();
//            if (response.BoothDashboard.TotalBooth == 0)
//            {
//                return response;
//            }
//            response.BoothDashboard.BoothMaintenance = booths.Select(i => i.Status == BoothStatus.Maintenance).Count();
//            response.BoothDashboard.BoothActive = booths.Select(i => i.Status == BoothStatus.Active).Count();
//            response.BoothDashboard.BoothInactive = booths.Select(i => i.Status == BoothStatus.Inactive).Count();
//            response.BoothDashboard.BoothInUse = booths.Select(i => i.Status == BoothStatus.InUse).Count();
//            var orders = await _sessionOrderRepository.GetAsync(i => i.Status == BookingStatus.Done);
//            response.TotalOrder = orders.Count();
//            response.TotalRevenue = response.TotalOrder == 0 ? 0 : orders.Sum(i => i.PaymentAmount);
//            return response;
//        }
//        public async Task<List<DashboardServiceResponse>> DashboradService(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
//        {
//            //var orders = await this.GetSessionOrders(branchID, startDate, endDate);
//            //var serviceItem = await _serviceItemRepository.GetAsync(i => orders.Select(o => o.BookingID).ToList().Contains(i.BookingID), i => i.Service);
//            //if (serviceItem.Count() == 0)
//            //{
//            //    return new List<DashboardServiceResponse>();
//            //}
//            //var ServiceCount = serviceItem
//            //    .GroupBy(i => i.Service)
//            //    .Select(g => new DashboardServiceResponse
//            //    {
//            //        Quantity = g.Sum(o => o.Quantity),
//            //        Service = _mapper.Map<ServicePackageResponse>(g.Key)
//            //    }).ToList();
//            //var services = await _serviceRepository.GetAsync();
//            //var existedId = ServiceCount.Select(i => i.Service.ServiceID);
//            //foreach (var item in services)
//            //{
//            //    if (!existedId.Contains(item.ServicePackageID))
//            //    {
//            //        ServiceCount.Add(new DashboardServiceResponse {
//            //            Quantity = 0, 
//            //            Service = _mapper.Map<ServicePackageResponse>(item)
//            //        });
//            //    }
//            //}

//            //return ServiceCount.OrderByDescending(i => i.Quantity).ToList();
//            return null;
//        }
//        public async Task<List<DashboardLayoutResponse>> DashboardLayout(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
//        {
//            var photoSessions = await this.GetPhotoSessions(branchID, startDate, endDate, i => i.Layout);
//            if (photoSessions.Count() == 0)
//            {
//                return new List<DashboardLayoutResponse>();
//            }
//            var layoutCount = photoSessions
//                .GroupBy(i => i.Layout)
//                .Select(g => new DashboardLayoutResponse
//                {
//                    Count = g.Count(),
//                    Layout = _mapper.Map<LayoutSummaryResponse>(g.Key)
//                }).ToList();

//            var layouts = await _layoutRepository.GetAsync();
//            var existedId = layoutCount.Select(i => i.Layout.LayoutID);
//            foreach (var item in layouts)
//            {
//                if (!existedId.Contains(item.LayoutID))
//                {
//                    layoutCount.Add(new DashboardLayoutResponse
//                    {
//                        Count = 0,
//                        Layout = _mapper.Map<LayoutSummaryResponse>(item)
//                    });
//                }
//            }
//            return layoutCount.OrderByDescending(i => i.Count).ToList();
//        }
//        public async Task<List<DashboardBackgroundResponse>> DashboardBackground(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
//        {
//            var photos = await this.GetPhotos(branchID, startDate, endDate, i => i.Background);
//            if (photos.Count() == 0)
//            {
//                return new List<DashboardBackgroundResponse>();
//            }
//            var backgroundCount = photos
//                .GroupBy(i => i.Background)
//                .Select(g => new DashboardBackgroundResponse
//                {
//                    Count = g.Count(),
//                    Background = _mapper.Map<BackgroundResponse>(g.Key)
//                }).ToList();
//            var backgrounds = await _backgroundRepository.GetAsync();
//            var existedId = backgroundCount.Select(i => i.Background.BackgroundID);
//            foreach (var item in backgrounds)
//            {
//                if (!existedId.Contains(item.BackgroundID))
//                {
//                    backgroundCount.Add(new DashboardBackgroundResponse
//                    {
//                        Count = 0,
//                        Background = _mapper.Map<BackgroundResponse>(item)
//                    });
//                }
//            }
//            return backgroundCount.OrderByDescending(i => i.Count).ToList();
//        }
//        public async Task<List<DashboardStickerResponse>> DashboardSticker(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
//        {
//            var photoStickers = await this.GetPhotoStickers(branchID, startDate, endDate, i => i.Sticker);
//            if (photoStickers.Count() == 0) 
//            {
//                return new List<DashboardStickerResponse>();
//            }
//            var stickerCount = photoStickers
//                .GroupBy(i => i.Sticker)
//                .Select(g => new DashboardStickerResponse
//                {
//                    Count = g.Count(),
//                    Sticker = _mapper.Map<StickerResponse>(g.Key)
//                }).ToList();
//            var stickers = await _stickerRepository.GetAsync();
//            var existedId = stickerCount.Select(i => i.Sticker.StickerId);
//            foreach (var sticker in stickers)
//            {
//                if (!existedId.Contains(sticker.StickerID))
//                {
//                    stickerCount.Add(new DashboardStickerResponse
//                    {
//                        Count = 0,
//                        Sticker = _mapper.Map<StickerResponse>(sticker)
//                    });
//                }
//            }
//            return stickerCount.OrderByDescending(i => i.Count).ToList();
//        }
//        private async Task<List<PhotoSticker>> GetPhotoStickers(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<PhotoSticker, object>>[] includeProperties)
//        {
//            if (startDate == null && branchID == null && endDate == null)
//            {
//                return (await _photoStickerRepository.GetAsync(null, includeProperties)).ToList();
//            }
//            var photos = await this.GetPhotos(branchID, startDate, endDate);
//            return photos.Count() == 0 ? new List<PhotoSticker>() : (await _photoStickerRepository.GetAsync(p => photos.Select(o => o.PhotoID).ToList().Contains(p.PhotoID), includeProperties)).ToList(); 
//        }
//        private async Task<List<Photo>> GetPhotos(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<Photo, object>>[] includeProperties)
//        {
//            if (startDate == null && branchID == null && endDate == null)
//            {
//                return (await _photoRepository.GetAsync(null, includeProperties)).ToList();
//            }
//            var photoSessions = await this.GetPhotoSessions(branchID, startDate, endDate);
//            return photoSessions.Count() == 0 ? new List<Photo>() : (await _photoRepository.GetAsync(p => photoSessions.Select(o => o.PhotoSessionID).ToList().Contains(p.PhotoSessionID), includeProperties)).ToList();
//        }
//        private async Task<List<PhotoSession>> GetPhotoSessions(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<PhotoSession, object>>[] includeProperties)
//        {
//            if (startDate == null && branchID == null && endDate == null)
//            {
//                return (await _photoSessionRepository.GetAsync(null, includeProperties)).ToList();
//            }
//            var orders = await this.GetSessionOrders(branchID, startDate, endDate);
//            return orders.Count() == 0 ? new List<PhotoSession>() : (await _photoSessionRepository.GetAsync(i => orders.Select(o => o.BookingID).ToList().Contains(i.BookingID), includeProperties)).ToList();
//        }
//        private async Task<List<Booking>> GetSessionOrders(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<Booking, object>>[] includeProperties)
//        {
//            //var booths = branchID.HasValue ? await _boothRepository.GetAsync(i => i.BranchID == branchID) : null;
//            //if (branchID.HasValue && booths.Count() == 0)
//            //{
//            //    return new List<Booking>();
//            //}
//            //bool isEnd = false; 
//            //bool isStart = false;
//            //Expression<Func<Booking, bool>> pre = branchID.HasValue ? i => booths.Select(b => b.BoothID).ToList().Contains(i.BoothID) : i => true;
//            //pre = LinQHelper.AndAlso(pre, i => i.Status == SessionOrderStatus.Done);
//            //if (endDate != null && endDate != default(DateOnly))
//            //{
//            //    isEnd = true;
//            //    pre = LinQHelper.AndAlso(pre, so => so.EndTime.Value.Date <= new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day));
//            //}
//            //if (startDate != null && startDate != default(DateOnly))
//            //{
//            //    isStart = true;
//            //    pre = LinQHelper.AndAlso(pre, so => so.EndTime.Value.Date >= new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day));
//            //}

//            ////make sure endtime after start time
//            //if (endDate <= startDate && isStart && isEnd)
//            //{
//            //    throw new BadRequestException("The end date must be later than the start date.");
//            //}

//            //return (await _sessionOrderRepository.GetAsync(pre, includeProperties)).ToList();
//            return null;
//        }

//    }
//}
