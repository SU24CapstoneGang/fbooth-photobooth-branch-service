using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Refund;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class RefundMapper : Profile
    {
        public RefundMapper()
        {
            CreateMap<Refund, RefundResponse>().HandleNullProperty();
        }
    }
}
