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
            CreateMap<CreateSessionRequest, SessionOrder>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateSessionRequest, SessionOrder>().ReverseMap().HandleNullProperty();
            CreateMap<SessionResponse, SessionOrder>().ReverseMap().HandleNullProperty();
        }
    }
}
