using PhotoboothBranchService.Application.Common;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;

namespace PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices
{
    public class VNPayService : IVNPayService
    {
        string vnp_Returnurl = JsonHelper.GetFromAppSettings("VNPay:vnp_Returnurl");//URL nhan ket qua tra ve 
        string vnp_Url = JsonHelper.GetFromAppSettings("VNPay:vnp_Url"); //URL thanh toan cua VNPAY 
        string vnp_TmnCode = JsonHelper.GetFromAppSettings("VNPay:vnp_TmnCode"); //Ma định danh merchant kết nối (Terminal Id)
        string vnp_HashSecret = JsonHelper.GetFromAppSettings("VNPay:vnp_HashSecret"); //Secret Key
        public async Task<string> Pay(PaymentRequest paymentRequest)
        {
            //create library
            VnPayLibrary vnpay = new VnPayLibrary();

            //add data to request
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString());
            if (paymentRequest.BankCode == "VNPAYQR")
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (paymentRequest.BankCode == "VNBANK")
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (paymentRequest.BankCode == "INTCARD")
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }
            else
            {
                vnpay.AddRequestData("vnp_BankCode", "");
            };
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            if (paymentRequest.ClientIpAddress != null)
            {
                vnpay.AddRequestData("vnp_IpAddr", paymentRequest.ClientIpAddress);
            }
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:");
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", paymentRequest.SessionID.ToString());

            //build request url
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }

        public void Query()
        {
            throw new NotImplementedException();
        }

        public void Refund()
        {
            throw new NotImplementedException();
        }

        public void Return()
        {
            throw new NotImplementedException();
        }
    }
}
