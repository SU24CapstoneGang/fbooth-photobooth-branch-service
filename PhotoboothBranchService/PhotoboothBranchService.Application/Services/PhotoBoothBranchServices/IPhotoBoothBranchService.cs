using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PhotoBoothBranchServices;

public interface IPhotoBoothBranchService : IService<PhotoBoothBranchDTO>
{
    Task<IEnumerable<PhotoBoothBranchDTO>> GetAll(ManufactureStatus status);
    Task<IEnumerable<PhotoBoothBranchDTO>> GetByName(string name);
}
