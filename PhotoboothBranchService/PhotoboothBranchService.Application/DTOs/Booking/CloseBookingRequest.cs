﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class CloseBookingRequest
    {
        public Guid BoothID { get; set; }
        public Guid BookingID { get; set; }
    }
}
