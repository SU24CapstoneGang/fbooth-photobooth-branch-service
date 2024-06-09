﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ServiceItem
    {
        public Guid ServiceItemID { get; set; }
        public short Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Guid PhotoSessionID { get; set; }
        public virtual PhotoSession PhotoSession { get; set; }
        public Guid ServiceID { get; set; }
        public virtual Service Service { get; set; }
        public Guid? SessionOrderID { get; set; }
        public virtual SessionOrder SessionOrder { get; set; }
    }
}
