using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.BookingSlot;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BookingSlotMapper : Profile
    {
        public BookingSlotMapper()
        {
            CreateMap<BookingSlot, BookingSlotResponse>()
                .ForMember(dest => dest.BookingSlotID, opt => opt.MapFrom(src => src.BookingSlotID))
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SlotStartTime, opt => opt.MapFrom(src => src.Slot.SlotStartTime))
                .ForMember(dest => dest.SlotEndTime, opt => opt.MapFrom(src => src.Slot.SlotEndTime))
                .HandleNullProperty();

        }
    }
}
