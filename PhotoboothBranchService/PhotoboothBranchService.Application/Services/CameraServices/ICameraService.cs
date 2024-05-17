﻿using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.CameraServices;

public interface ICameraService : IService<CameraDTO>
{
    Task<IEnumerable<CameraDTO>> GetByName(string name);
}
