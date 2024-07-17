namespace PhotoboothBranchService.Application.DTOs.VNPayPayment
{
    public class VnpayRefundResponse
    {
        public string Vnp_ResponseId { get; set; }
        public string Vnp_Command { get; set; }
        public string Vnp_ResponseCode { get; set; }
        public string Vnp_Message { get; set; }
        public string Vnp_TmnCode { get; set; }
        public string Vnp_TxnRef { get; set; }
        public string Vnp_Amount { get; set; }
        public string Vnp_OrderInfo { get; set; }
        public string Vnp_BankCode { get; set; }
        public string Vnp_PayDate { get; set; }
        public string Vnp_TransactionNo { get; set; }
        public string Vnp_TransactionType { get; set; }
        public string Vnp_TransactionStatus { get; set; }
        public string Vnp_SecureHash { get; set; }
    }
}
