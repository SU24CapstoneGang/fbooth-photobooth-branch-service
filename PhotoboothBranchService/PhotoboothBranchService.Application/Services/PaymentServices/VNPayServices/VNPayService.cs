using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using PhotoboothBranchService.Application.Common;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices
{
    public class VNPayService : IVNPayService
    {
        private readonly string vnp_Returnurl;
        private readonly string vnp_Url;
        private readonly string vnp_TmnCode; 
        private readonly string vnp_HashSecret;
        private readonly string vnp_Api;
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        public VNPayService(ITransactionHistoryRepository transactionHistoryRepository)
        {
            vnp_Returnurl = JsonHelper.GetFromAppSettings("VNPay:vnp_Returnurl");//URL nhan ket qua tra ve 
            vnp_Url = JsonHelper.GetFromAppSettings("VNPay:vnp_Url"); //URL thanh toan cua VNPAY 
            vnp_TmnCode = JsonHelper.GetFromAppSettings("VNPay:vnp_TmnCode"); //Ma định danh merchant kết nối (Terminal Id)
            vnp_HashSecret = JsonHelper.GetFromAppSettings("VNPay:vnp_HashSecret"); //Secret Key
            vnp_Api = JsonHelper.GetFromAppSettings("VNPay:vnp_Api");
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        public async Task<string> Pay(VnpayRequest paymentRequest)
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

        public async Task<VnpayQueryResponse> Query(string orderId, string payDate, string clientIp)
        {
            var vnp_RequestId = DateTime.Now.Ticks.ToString();
            var vnp_Version = VnPayLibrary.VERSION;
            var vnp_Command = "querydr";
            var vnp_TxnRef = orderId;
            var vnp_OrderInfo = "Truy van giao dich:" + orderId;
            var vnp_TransactionDate = payDate;
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_IpAddr = clientIp;

            var signData = $"{vnp_RequestId}|{vnp_Version}|{vnp_Command}|{vnp_TmnCode}|{vnp_TxnRef}|{vnp_TransactionDate}|{vnp_CreateDate}|{vnp_IpAddr}|{vnp_OrderInfo}";
            var vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);

            var qdrData = new
            {
                vnp_RequestId,
                vnp_Version,
                vnp_Command,
                vnp_TmnCode,
                vnp_TxnRef,
                vnp_OrderInfo,
                vnp_TransactionDate,
                vnp_CreateDate,
                vnp_IpAddr,
                vnp_SecureHash
            };
            var jsonData = JsonConvert.SerializeObject(qdrData);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(vnp_Api, content);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                var vnpayResponse = JsonConvert.DeserializeObject<VnpayQueryResponse>(responseData);
                return vnpayResponse;
            }
        }

        public async Task<VnpayRefundResponse> RefundTransaction(VnpayRefundRequest request, string clientIp)
        {
            var vnp_RequestId = DateTime.Now.Ticks.ToString();
            var vnp_Version = "2.1.0";
            var vnp_Command = "refund";
            var vnp_TransactionType = request.RefundCategory;
            var vnp_Amount = Convert.ToInt64(request.Amount) * 100;
            var vnp_TxnRef = request.SessionId;
            var vnp_OrderInfo = "Hoan tien giao dich:" + request.SessionId;
            var vnp_TransactionNo = request.TransId;
            var vnp_TransactionDate = request.PayDate;
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_CreateBy = request.User;
            var vnp_IpAddr = clientIp;

            var signData = $"{vnp_RequestId}|{vnp_Version}|{vnp_Command}|{vnp_TmnCode}|{vnp_TransactionType}|{vnp_TxnRef}|{vnp_Amount}|{vnp_TransactionNo}|{vnp_TransactionDate}|{vnp_CreateBy}|{vnp_CreateDate}|{vnp_IpAddr}|{vnp_OrderInfo}";
            var vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);

            var rfData = new
            {
                vnp_RequestId,
                vnp_Version,
                vnp_Command,
                vnp_TmnCode,
                vnp_TransactionType,
                vnp_TxnRef,
                vnp_Amount,
                vnp_OrderInfo,
                vnp_TransactionNo,
                vnp_TransactionDate,
                vnp_CreateBy,
                vnp_CreateDate,
                vnp_IpAddr,
                vnp_SecureHash
            };
            var jsonData = JsonConvert.SerializeObject(rfData);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(vnp_Api, content);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                var vnpayResponse = JsonConvert.DeserializeObject<VnpayRefundResponse>(responseData);
                return vnpayResponse;
            }
        }

        public VnpayResponse Return(IQueryCollection queryString)
        {
            VnPayLibrary vnpay = new VnPayLibrary();

            foreach (var key in queryString.Keys)
            {
                // get all querystring data
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, queryString[key]);
                }
            }

            string orderId = vnpay.GetResponseData("vnp_TxnRef");
            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = queryString["vnp_SecureHash"];
            string terminalID = queryString["vnp_TmnCode"];
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            string bankCode = queryString["vnp_BankCode"];

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if (checkSignature)
            {
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    _transactionHistoryRepository.AddAsync(new TransactionHistory()
                    {
                        ThirdpartyID = vnpayTranId.ToString(),
                        SessionID = new Guid(orderId),

                    });
                    return new VnpayResponse
                    {
                        Message = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ",
                        TerminalID = terminalID,
                        SessionId = new Guid(orderId),
                        VnpayTranId = vnpayTranId,
                        Amount = vnp_Amount,
                        BankCode = bankCode,
                        Success = true
                    };
                }
                else
                {
                    return new VnpayResponse
                    {
                        Message = "Có lỗi xảy ra trong quá trình xử lý.",
                        ErrorCode = vnp_ResponseCode,
                        TerminalID = terminalID,
                        SessionId = new Guid(orderId),
                        VnpayTranId = vnpayTranId,
                        Amount = vnp_Amount,
                        BankCode = bankCode,
                        Success = false
                    };
                }
            }
            else
            {
                return new VnpayResponse
                {
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Success = false
                };
            }
        }
    }
}
