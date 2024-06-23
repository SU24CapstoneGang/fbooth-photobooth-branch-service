﻿namespace PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment
{
    public class VnpayRequest
    {
        public Guid PaymentID { get; set; }
        public long Amount { get; set; }
        public string? BankCode { get; set; }
        public Guid SessionOrderID { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public string? OrderInformation { get; set; }
        public string ClientIpAddress { get; set; }
    }
}
