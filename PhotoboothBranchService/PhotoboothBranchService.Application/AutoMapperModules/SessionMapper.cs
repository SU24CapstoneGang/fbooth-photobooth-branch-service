using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Session;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class SessionMapper : Profile
    {
        public SessionMapper()
        {
            CreateMap<CreateSessionOrderRequest, SessionOrder>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateSessionOrderRequest, SessionOrder>().ReverseMap().HandleNullProperty();
            CreateMap<SessionOrderResponse, SessionOrder>().ReverseMap().HandleNullProperty();
        }
    }
}
