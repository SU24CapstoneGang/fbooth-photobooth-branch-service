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
            CreateMap<CreateSessionOrderRequest, SessionOrder>().HandleNullProperty();
            CreateMap<UpdateSessionOrderRequest, SessionOrder>().ReverseMap().HandleNullProperty();
            CreateMap<SessionOrder, SessionOrderResponse>().HandleNullProperty();
            CreateMap<SessionOrder, CreateSessionOrderResponse>().HandleNullProperty();
        }
    }
}
