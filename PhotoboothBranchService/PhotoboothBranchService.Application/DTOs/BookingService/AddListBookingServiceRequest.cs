﻿using PhotoboothBranchService.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.BookingService
{
    public class AddListBookingServiceRequest
    {
        public Guid BookingID { get; set; }
        [DictionaryValueGreaterThanZero(ErrorMessage = "Each service quantity must be greater than 0.")]
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
    }
}
