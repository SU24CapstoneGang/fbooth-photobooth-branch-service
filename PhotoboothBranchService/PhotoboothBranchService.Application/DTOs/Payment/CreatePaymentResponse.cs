﻿namespace PhotoboothBranchService.Application.DTOs.Transaction
{
    public class CreatePaymentResponse
    {
        public string TransactionUlr { get; set; }
        public Guid PaymentID { get; set; }

    }
}
