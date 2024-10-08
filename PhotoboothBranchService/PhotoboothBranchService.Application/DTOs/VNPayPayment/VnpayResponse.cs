﻿namespace PhotoboothBranchService.Application.DTOs.VNPayPayment
{
    public class VnpayResponse
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string TerminalID { get; set; }
        public Guid PaymentID { get; set; }
        public long VnpayTranId { get; set; }
        public long Amount { get; set; }
        public string BankCode { get; set; }
        public bool Success { get; set; }
    }
}
