using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Device;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class DeviceMapper : Profile
    {
        public DeviceMapper()
        {
            CreateMap<CreateDeviceRequest, Device>().HandleNullProperty();
            CreateMap<UpdateDeviceRequest, Device>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Device, DeviceResponse>().HandleNullProperty();
            CreateMap<Device, CreateDeviceResponse>().HandleNullProperty();
        }
    }
}
