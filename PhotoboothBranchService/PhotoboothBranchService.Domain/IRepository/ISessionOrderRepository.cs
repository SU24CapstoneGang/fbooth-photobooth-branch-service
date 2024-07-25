﻿using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Domain.IRepository;

public interface ISessionOrderRepository : IRepositoryBase<Booking>
{
    Task updateTotalPrice(Guid SessionOrderID);
}
