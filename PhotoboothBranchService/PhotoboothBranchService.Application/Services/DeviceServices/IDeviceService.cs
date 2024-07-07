﻿using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Device;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.DeviceServices
{
    public interface IDeviceService : IServiceBase<DeviceResponse, CreateDeviceRequest, CreateDeviceResponse, UpdateDeviceRequest, DeviceFilter, PagingModel>
    {
    }
}
