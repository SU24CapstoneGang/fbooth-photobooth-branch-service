using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.DTOs.Slot;
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

namespace PhotoboothBranchService.Application.Services.SlotServices
{
    public class SlotService : ISlotService
    {
        private readonly ISlotRepository _slotRepository;
        private readonly IMapper _mapper;
        private readonly IBookingSlotRepository _bookingSlotRepository;
        private readonly IBoothRepository _boothRepository;
        private readonly IBookingRepository _bookingRepository;

        public SlotService(ISlotRepository slotRepository, IMapper mapper, IBookingSlotRepository bookingSlotRepository, IBoothRepository boothRepository, IBookingRepository bookingRepository)
        {
            _slotRepository = slotRepository;
            _mapper = mapper;
            _bookingSlotRepository = bookingSlotRepository;
            _boothRepository = boothRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<SlotResponse>> GetAllAsync()
        {
            var slots = await _slotRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SlotResponse>>(slots.ToList().OrderBy(i => i.SlotEndTime));
        }

        public async Task<IEnumerable<SlotResponse>> GetAllPagingAsync(SlotFilter filter, PagingModel paging)
        {
            var slots = (await _slotRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listSlotResponse = _mapper.Map<IEnumerable<SlotResponse>>(slots);
            return listSlotResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).OrderBy(i => i.SlotEndTime);
        }

        public async Task<SlotResponse> GetByIdAsync(Guid id)
        {
            var slots = await _slotRepository.GetAsync(s => s.SlotID == id);
            var slot = slots.FirstOrDefault();
            if (slot == null)
            {
                throw new NotFoundException("Slot not found");
            }
            return _mapper.Map<SlotResponse>(slot);
        }

        public async Task<IEnumerable<SlotResponse>> GetBoothFreeSlot(Guid boothID, DateOnly date)
        {
            //get booth's slot for a date
            var slots = (await _slotRepository.GetAsync(i => i.BoothID == boothID)).ToList();
            var slotIdList = slots.Select(i => i.SlotID);


            //get booking of date of the booth
            var bookingIds = (await _bookingRepository.GetAsync(i => i.BoothID == boothID
                                 && i.StartTime.Date == new DateTime(date.Year,date.Month,date.Day).Date
                                 && i.BookingStatus != BookingStatus.Canceled && i.BookingStatus != BookingStatus.CancelledBySystem)).Select(i => i.BookingID).ToList();
            
            //get slot from booking id
            var usedSlots = await _bookingSlotRepository.GetAsync(i => bookingIds.Contains(i.BookingID));

            // Create a hash set for quick lookup
            var usedSlotIds = new HashSet<Guid>(usedSlots.Select(i => i.SlotID));
            foreach (var slot in slots)
            {
                if (usedSlotIds.Contains(slot.SlotID))
                {
                    slot.Status = StatusUse.Unusable; 
                }
            }
            return _mapper.Map<IEnumerable<SlotResponse>>(slots);
        }
        public async Task<IEnumerable<GetBranchFreeSlotResponse>> GetBranchFreeSlot(Guid BranchID, DateOnly date)
        {
            var booths = (await _boothRepository.GetAsync(i => i.BranchID == BranchID && i.Status == BoothStatus.Active)).ToList();
            List<GetBranchFreeSlotResponse> results = new List<GetBranchFreeSlotResponse>();
            if (!booths.Any())
            {
                return results;
            }
            foreach (var booth in booths)
            {
                results.Add(new GetBranchFreeSlotResponse
                {
                    BoothID = booth.BoothID,
                    Slots = await this.GetBoothFreeSlot(booth.BoothID, date)
                });
            }
            return results;
        }
        public async Task<IEnumerable<SlotResponse>> AutoCreateSlotByBooth(AutoCreateSlotRequest request)
        {
            var booth = (await _boothRepository.GetAsync(i => i.BoothID == request.BoothID, i => i.Branch)).SingleOrDefault();
            if (booth == null)
            {
                throw new NotFoundException("Booth not found");
            }
            var exitedslots = (await _slotRepository.GetAsync(i => i.BoothID == request.BoothID)).ToList();
            var slots = new List<Slot>();
            var currentTime = booth.Branch.OpeningTime;

            while (currentTime < booth.Branch.ClosingTime)
            {
                var slotEndTime = currentTime.Add(TimeSpan.FromMinutes(15));

                // Ensure the slot end time does not exceed the branch closing time
                if (slotEndTime > booth.Branch.ClosingTime)
                {
                    break;
                }
                if (!exitedslots.Any(s => s.SlotStartTime == currentTime && s.SlotEndTime == slotEndTime))
                {
                    var slot = new Slot
                    {
                        SlotStartTime = currentTime,
                        SlotEndTime = slotEndTime,
                        Price = request.Price,
                        Status = StatusUse.Available,
                        BoothID = request.BoothID
                    };
                    await _slotRepository.AddAsync(slot);
                    slots.Add(slot);
                }
                currentTime = slotEndTime; // Move to the next time slot
            }

            return _mapper.Map<IEnumerable<SlotResponse>>(slots);
        }
    }
}
