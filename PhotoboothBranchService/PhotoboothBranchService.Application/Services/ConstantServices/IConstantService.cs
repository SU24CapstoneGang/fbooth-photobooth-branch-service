using PhotoboothBranchService.Application.DTOs.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ConstantServices
{
    public interface IConstantService
    {
        string GetConstantValue(string key);
        Task LoadConstantsAsync();
        Task UpdateConstantAsync(string key, UpdateConstantRequest request);
    }
}
