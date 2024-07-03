using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.SessionPackage;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class SessionPackageMapper : Profile
    {
        public SessionPackageMapper()
        {
            CreateMap<CreateSessionPackageRequest, SessionPackage>().HandleNullProperty();
            CreateMap<UpdateSessionPackageRequest, SessionPackage>().HandleNullProperty();
            CreateMap<SessionPackage, SessionPackageResponse>().HandleNullProperty();
            CreateMap<SessionPackage, CreateSessionPackageResponse>().HandleNullProperty();
        }
    }
}
