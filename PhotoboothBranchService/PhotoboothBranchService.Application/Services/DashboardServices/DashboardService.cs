using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.DTOs.Dashboard;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.DTOs.Sticker;
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
        private readonly IBranchRepository _branchRepository;
        private readonly IBoothRepository _boothRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPhotoSessionRepository _photoSessionRepository;
        private readonly IPhotoStickerRepository _photoStickerRepository;
        private readonly IBookingServiceRepository _bookingServiceRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly ILayoutRepository _layoutRepository;
        private readonly IBackgroundRepository _backgroundRepository;
        private readonly IStickerRepository _stickerRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public DashboardService(IBranchRepository branchRepository,
            IBoothRepository boothRepository,
            IAccountRepository accountRepository,
            IBookingRepository sessionOrderRepository,
            IPhotoSessionRepository photoSessionRepository,
            IPhotoStickerRepository photoStickerRepository,
            IBookingServiceRepository serviceItemRepository,
            IPhotoRepository photoRepository,
            IMapper mapper,
            ILayoutRepository layoutRepository,
            IBackgroundRepository backgroundRepository,
            IStickerRepository stickerRepository, 
            IServiceRepository serviceRepository)
        {
            _branchRepository = branchRepository;
            _boothRepository = boothRepository;
            _accountRepository = accountRepository;
            _bookingRepository = sessionOrderRepository;
            _photoSessionRepository = photoSessionRepository;
            _photoStickerRepository = photoStickerRepository;
            _bookingServiceRepository = serviceItemRepository;
            _photoRepository = photoRepository;
            _mapper = mapper;
            _layoutRepository = layoutRepository;
            _backgroundRepository = backgroundRepository;
            _stickerRepository = stickerRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<BasicBranchDashboardResponse> BasicBranchDashboard(Guid branchID)
        {
            BasicBranchDashboardResponse response = new BasicBranchDashboardResponse();
            var staffs = await _accountRepository.GetAsync(i => i.BranchID == branchID && i.Role == AccountRole.Staff);
            response.StaffDashboard.TotalStaff = staffs.Count();
            if (response.StaffDashboard.TotalStaff != 0)
            {
                response.StaffDashboard.StaffActive = staffs.Select(i => i.Status == AccountStatus.Active).Count();
                response.StaffDashboard.StaffBlocked = staffs.Select(i => i.Status == AccountStatus.Blocked).Count();
            }
            var booths = await _boothRepository.GetAsync(i => i.BranchID == branchID);
            response.BoothDashboard.TotalBooth = booths.Count();

            if (response.BoothDashboard.TotalBooth == 0)
            {
                return response;
            }
            response.BoothDashboard.BoothActive = booths.Select(i => i.Status == BoothStatus.Active).Count();
            response.BoothDashboard.BoothInactive = booths.Select(i => i.Status == BoothStatus.Inactive).Count();
            var bookings = await _bookingRepository.GetAsync(i => booths.Select(b => b.BoothID).ToList().Contains(i.BoothID));
            response.TotalOrder = bookings.Count();
            if (response.TotalOrder == 0)
            {
                response.TotalRevenue = 0;
                response.CountCustomer = 0;
                return response;
            }
            response.CountCustomer = bookings.GroupBy(o => o.CustomerID).Count();
            response.TotalRevenue = bookings.Sum(o => o.TotalPrice) - bookings.Sum(i => i.RefundedAmount);
            return response;
        }
        public async Task<BasicDashboardResponse> BasicDashboard()
        {
            var response = new BasicDashboardResponse();
            //infor for branch
            var branchs = await _branchRepository.GetAllAsync();
            response.CountCustomer = (await _accountRepository.GetAsync(i => i.Role == AccountRole.Customer && i.Status == AccountStatus.Active)).Count();
            response.TotalBranch = branchs.Count();
            if (response.TotalBranch == 0)
            {
                response.TotalRevenue = 0;
                response.TotalBookings = 0;
                return response;
            }
            //infor for booth
            var booths = await _boothRepository.GetAllAsync();
            response.BoothDashboard.TotalBooth = booths.Count();
            if (response.BoothDashboard.TotalBooth == 0)
            {
                return response;
            }
            response.BoothDashboard.BoothActive = booths.Select(i => i.Status == BoothStatus.Active).Count();
            response.BoothDashboard.BoothInactive = booths.Select(i => i.Status == BoothStatus.Inactive).Count();
            response.BoothDashboard.BoothInUse = booths.Select(i => i.Status == BoothStatus.Booked).Count();
            var bookings = await _bookingRepository.GetAsync();
            response.TotalBookings = bookings.Count();
            response.TotalRevenue = response.TotalBookings == 0 ? 0 : bookings.Sum(i => i.TotalPrice) - bookings.Sum(i => i.RefundedAmount);
            return response;
        }
        public async Task<BookingDashboardResponse> BookingDashboard(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var result = new BookingDashboardResponse();
            var bookings = await this.GetBookings(branchID, startDate, endDate, true, i => i.FullPaymentPolicy);
            if (bookings.Count() == 0)
            {
                return result;
            }
            //classify the booking
            var inFuture = bookings.Where(i => i.BookingStatus == BookingStatus.PendingChecking && i.StartTime > DateTimeHelper.GetVietnamTimeNow()).ToList();
            var completed = bookings.Where(i => i.PaymentStatus == PaymentStatus.Paid && i.BookingStatus == BookingStatus.CompleteChecked).ToList();
            var canceleded = bookings.Where(i => i.BookingStatus == BookingStatus.Canceled || i.BookingStatus == BookingStatus.CancelledBySystem).ToList();
            var needPayExtra = bookings.Where(i => i.PaymentStatus == PaymentStatus.PendingPayExtra).ToList();
            var onGoing = bookings.Where(i => i.BookingStatus == BookingStatus.CompleteChecked).ToList();
            var needRefund = bookings.Where(i => i.PaymentStatus == PaymentStatus.PendingRefund).ToList();
            //caculate money
            result.TotalRevenue = bookings.Sum(i => i.TotalPrice); 
            result.TotalRefunded = canceleded.Where(i => i.BookingStatus == BookingStatus.Canceled && (i.PaymentStatus == PaymentStatus.Refunded || i.PaymentStatus == PaymentStatus.PendingRefund)).Sum(i => i.TotalPrice*i.FullPaymentPolicy.RefundPercent/100);
            result.TotalRefunded += canceleded.Where(i => i.BookingStatus == BookingStatus.CancelledBySystem).Sum(i => i.TotalPrice);
            result.TotalRevenue -= result.TotalRefunded;

            //mapping
            result.InFuture = _mapper.Map<List<BookingResponse>>(inFuture);
            result.Completed = _mapper.Map<List<BookingResponse>>(completed);
            result.Canceleded = _mapper.Map<List<BookingResponse>>(canceleded);
            result.NeedPayExtra = _mapper.Map<List<BookingResponse>>(needPayExtra);
            result.OnGoing = _mapper.Map<List<BookingResponse>>(onGoing);
            result.NeedRefund = _mapper.Map<List<BookingResponse>>(needRefund);
            return result;
        }
        public async Task<List<DashboardServiceResponse>> DashboradService(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var orders = await this.GetBookings(branchID, startDate, endDate, false);
            var serviceItem = await _bookingServiceRepository.GetAsync(i => orders.Select(o => o.BookingID).ToList().Contains(i.BookingID), i => i.Service);
            var ServiceCount = serviceItem
                .GroupBy(i => i.Service)
                .Select(g => new DashboardServiceResponse
                {
                    Quantity = g.Sum(o => o.Quantity),
                    Service = _mapper.Map<ServiceResponse>(g.Key)
                }).ToList();
            var services = await _serviceRepository.GetAsync();
            var existedId = ServiceCount.Select(i => i.Service.ServiceID);
            foreach (var item in services)
            {
                if (!existedId.Contains(item.ServiceID))
                {
                    ServiceCount.Add(new DashboardServiceResponse
                    {
                        Quantity = 0,
                        Service = _mapper.Map<ServiceResponse>(item)
                    });
                }
            }

            return ServiceCount.OrderByDescending(i => i.Quantity).ToList();
        }
        public async Task<List<DashboardLayoutResponse>> DashboardLayout(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var photoSessions = await this.GetPhotoSessions(branchID, startDate, endDate, i => i.Layout);
            var layoutCount = photoSessions
                .GroupBy(i => i.Layout)
                .Select(g => new DashboardLayoutResponse
                {
                    Count = g.Count(),
                    Layout = _mapper.Map<LayoutSummaryResponse>(g.Key)
                }).ToList();

            var layouts = await _layoutRepository.GetAsync();
            var existedId = layoutCount.Select(i => i.Layout.LayoutID);
            foreach (var item in layouts)
            {
                if (!existedId.Contains(item.LayoutID))
                {
                    layoutCount.Add(new DashboardLayoutResponse
                    {
                        Count = 0,
                        Layout = _mapper.Map<LayoutSummaryResponse>(item)
                    });
                }
            }
            return layoutCount.OrderByDescending(i => i.Count).ToList();
        }
        public async Task<List<DashboardBackgroundResponse>> DashboardBackground(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var photos = await this.GetPhotos(branchID, startDate, endDate, i => i.Background);
            var backgroundCount = photos
                .GroupBy(i => i.Background)
                .Select(g => new DashboardBackgroundResponse
                {
                    Count = g.Count(),
                    Background = _mapper.Map<BackgroundResponse>(g.Key)
                }).ToList();
            var backgrounds = await _backgroundRepository.GetAsync();
            var existedId = backgroundCount.Select(i => i.Background.BackgroundID);
            foreach (var item in backgrounds)
            {
                if (!existedId.Contains(item.BackgroundID))
                {
                    backgroundCount.Add(new DashboardBackgroundResponse
                    {
                        Count = 0,
                        Background = _mapper.Map<BackgroundResponse>(item)
                    });
                }
            }
            return backgroundCount.OrderByDescending(i => i.Count).ToList();
        }
        public async Task<List<DashboardStickerResponse>> DashboardSticker(Guid? branchID, DateOnly? startDate, DateOnly? endDate)
        {
            var photoStickers = await this.GetPhotoStickers(branchID, startDate, endDate, i => i.Sticker);
            var stickerCount = photoStickers
                .GroupBy(i => i.Sticker)
                .Select(g => new DashboardStickerResponse
                {
                    Count = g.Sum(i=>i.Quantity),
                    Sticker = _mapper.Map<StickerResponse>(g.Key)
                }).ToList();
            var stickers = await _stickerRepository.GetAsync();
            var existedId = stickerCount.Select(i => i.Sticker.StickerId);
            foreach (var sticker in stickers)
            {
                if (!existedId.Contains(sticker.StickerID))
                {
                    stickerCount.Add(new DashboardStickerResponse
                    {
                        Count = 0,
                        Sticker = _mapper.Map<StickerResponse>(sticker)
                    });
                }
            }
            return stickerCount.OrderByDescending(i => i.Count).ToList();
        }

        //private methods
        private async Task<List<PhotoSticker>> GetPhotoStickers(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<PhotoSticker, object>>[] includeProperties)
        {
            if (startDate == null && branchID == null && endDate == null)
            {
                return (await _photoStickerRepository.GetAsync(null, includeProperties)).ToList();
            }
            var photos = await this.GetPhotos(branchID, startDate, endDate);
            return photos.Count() == 0 ? new List<PhotoSticker>() : (await _photoStickerRepository.GetAsync(p => photos.Select(o => o.PhotoID).ToList().Contains(p.PhotoID), includeProperties)).ToList();
        }
        private async Task<List<Photo>> GetPhotos(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<Photo, object>>[] includeProperties)
        {
            if (startDate == null && branchID == null && endDate == null)
            {
                return (await _photoRepository.GetAsync(null, includeProperties)).ToList();
            }
            var photoSessions = await this.GetPhotoSessions(branchID, startDate, endDate);
            return photoSessions.Count() == 0 ? new List<Photo>() : (await _photoRepository.GetAsync(p => photoSessions.Select(o => o.PhotoSessionID).ToList().Contains(p.PhotoSessionID), includeProperties)).ToList();
        }
        private async Task<List<PhotoSession>> GetPhotoSessions(Guid? branchID, DateOnly? startDate, DateOnly? endDate, params Expression<Func<PhotoSession, object>>[] includeProperties)
        {
            if (startDate == null && branchID == null && endDate == null)
            {
                return (await _photoSessionRepository.GetAsync(null, includeProperties)).ToList();
            }
            var orders = await this.GetBookings(branchID, startDate, endDate, false);
            return orders.Count() == 0 ? new List<PhotoSession>() : (await _photoSessionRepository.GetAsync(i => orders.Select(o => o.BookingID).ToList().Contains(i.BookingID), includeProperties)).ToList();
        }
        private async Task<List<Booking>> GetBookings(Guid? branchID, DateOnly? startDate, DateOnly? endDate, bool getAllState ,params Expression<Func<Booking, object>>[] includeProperties)
        {
            IEnumerable<Booth> booths = new List<Booth>();

            if (branchID.HasValue)
            {
                booths = await _boothRepository.GetAsync(i => i.BranchID == branchID);
                if (!booths.Any()) // Use Any() to check if there are any booths
                {
                    return new List<Booking>(); // Return an empty list if no booths are found
                }
            }
            bool isEnd = false;
            bool isStart = false;
            Expression<Func<Booking, bool>> pre = branchID.HasValue ? i => booths.Select(b => b.BoothID).ToList().Contains(i.BoothID) : i => true;
            if (!getAllState)
            {
                pre = LinQHelper.AndAlso(pre, i => i.BookingStatus != BookingStatus.PendingPayment && i.BookingStatus != BookingStatus.Canceled && i.BookingStatus != BookingStatus.CancelledBySystem);
            }
            if (endDate != null && endDate != default(DateOnly))
            {
                isEnd = true;
                pre = LinQHelper.AndAlso(pre, so => so.EndTime.Date <= new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day));
            }
            if (startDate != null && startDate != default(DateOnly))
            {
                isStart = true;
                pre = LinQHelper.AndAlso(pre, so => so.EndTime.Date >= new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day));
            }

            //make sure endtime after start time
            if (endDate < startDate && isStart && isEnd)
            {
                throw new BadRequestException("The end date must be later than the start date.");
            }

            return (await _bookingRepository.GetAsync(pre, includeProperties)).ToList();
        }

    }
}
