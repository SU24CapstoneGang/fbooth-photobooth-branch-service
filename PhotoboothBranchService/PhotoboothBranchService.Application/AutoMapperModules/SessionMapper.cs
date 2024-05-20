using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.RequestModels.Session;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Session;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class SessionMapper : Profile
    {
        public SessionMapper() {
            CreateMap<CreateSessionRequest,Session>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateSessionRequest,Session>().ReverseMap().HandleNullProperty();
            CreateMap<SessionResponse,Session>().ReverseMap().HandleNullProperty();
        }
    }
}
