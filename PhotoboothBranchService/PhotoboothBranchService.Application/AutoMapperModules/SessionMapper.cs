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
            CreateMap<CreateSessionRequest, Session>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateSessionRequest, Session>().ReverseMap().HandleNullProperty();
            CreateMap<SessionResponse, Session>().ReverseMap().HandleNullProperty();
        }
    }
}
