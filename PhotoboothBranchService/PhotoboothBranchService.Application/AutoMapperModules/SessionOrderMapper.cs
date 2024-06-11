using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class SessionOrderMapper : Profile
    {
        public SessionOrderMapper()
        {
            CreateMap<CreateSessionOrderRequest, SessionOrder>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice ?? 0)) // Set TotalPrice to src.TotalPrice if it's not null, otherwise set it to 0
                .HandleNullProperty();
            CreateMap<UpdateSessionOrderRequest, SessionOrder>().ReverseMap().HandleNullProperty();
            CreateMap<SessionOrderResponse, SessionOrder>().ReverseMap().HandleNullProperty();
        }
    }
}
