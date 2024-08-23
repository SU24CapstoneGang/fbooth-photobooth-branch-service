using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoSessionMapper : Profile
    {
        public PhotoSessionMapper()
        {
            CreateMap<CreatePhotoSessionRequest, PhotoSession>().HandleNullProperty();
            CreateMap<UpdatePhotoSessionRequest, PhotoSession>()
    .ForMember(dest => dest.SessionName, opt => opt.MapFrom((src, dest) => src.SessionName ?? dest.SessionName))
    .ForMember(dest => dest.TotalPhotoTaken, opt => opt.MapFrom((src, dest) => src.TotalPhotoTaken.HasValue ? src.TotalPhotoTaken.Value : dest.TotalPhotoTaken))
    .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Status.HasValue ? src.Status.Value : dest.Status))
    .ForMember(dest => dest.SessionIndex, opt => opt.Ignore()) // Assuming SessionIndex needs to be set separately or is not updated
    .ForMember(dest => dest.StartTime, opt => opt.Ignore()) // Assuming StartTime is not updated
    .ForMember(dest => dest.EndTime, opt => opt.Ignore()) // Assuming EndTime is not updated
    .ForMember(dest => dest.LayoutID, opt => opt.Ignore()) // Assuming LayoutID is not updated
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Typically set by creation logic
    .ForMember(dest => dest.LastModified, opt => opt.Ignore()) // Typically set by update logic
    .ForMember(dest => dest.BookingID, opt => opt.Ignore()) // Assuming BookingID is not updated
    .ForMember(dest => dest.Layout, opt => opt.Ignore()) // Typically managed separately
    .ForMember(dest => dest.Booking, opt => opt.Ignore()) // Typically managed separately
    .ForMember(dest => dest.Photos, opt => opt.Ignore()); // Typically managed separately

            CreateMap<PhotoSession, PhotoSessionResponse>().HandleNullProperty();
            CreateMap<PhotoSession, CreatePhotoSessionResponse>().HandleNullProperty();
        }
    }
}
