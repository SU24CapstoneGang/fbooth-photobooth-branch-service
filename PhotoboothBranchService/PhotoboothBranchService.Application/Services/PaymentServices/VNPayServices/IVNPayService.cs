using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;

namespace PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices
{
    public interface IVNPayService
    {
        public Task<string> Pay(PaymentRequest paymentRequest);
        public void Query();
        public void Refund();
        public void Return();
    }
}
