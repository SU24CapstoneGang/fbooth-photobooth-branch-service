﻿using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.IRepository
{
    public interface ISlotRepository : IRepositoryBase<Slot>
    {
        Task AddRangeAsync(IEnumerable<Slot> entities);
        Task UpdateRangeAsync(IEnumerable<Slot> entities);
    }
}
