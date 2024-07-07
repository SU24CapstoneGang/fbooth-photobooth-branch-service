using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PhotoboothBranchService.Application.Common;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Payment.MoMoPayment;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Reflection;

namespace PhotoboothBranchService.Application.Services.PaymentServices.MoMoServices
{
    public class MoMoService : IMoMoService
    {
        private readonly string momo_Api;
        private readonly string accessKey;
        private readonly string secretKey;
        private readonly string partnerCode;
        private readonly string redirectUrl;
        private readonly string ipnUrl;

        private readonly IPaymentRepository _paymentRepository;
        private readonly ISessionOrderRepository _sessionOrderRepository;
        public MoMoService(IPaymentRepository paymentRepository, ISessionOrderRepository sessionOrderRepository)
        {
            momo_Api = JsonHelper.GetFromAppSettings("MoMo:momo_Api");
            accessKey = JsonHelper.GetFromAppSettings("MoMo:accessKey");
            secretKey = JsonHelper.GetFromAppSettings("MoMo:secretKey");
            partnerCode = JsonHelper.GetFromAppSettings("MoMo:partnerCode");
            redirectUrl = JsonHelper.GetFromAppSettings("MoMo:redirectUrl");
            ipnUrl = JsonHelper.GetFromAppSettings("MoMo:ipnUrl");

            _paymentRepository = paymentRepository;
            _sessionOrderRepository = sessionOrderRepository;
        }

        public string CreatePayment(MoMoRequest request)
        {
            string endpoint = momo_Api;

            string requestType = "captureWallet";

            //Before sign HMAC SHA256 signature
            string rawHash = "accessKey=" + accessKey +
                "&amount=" + request.amount +
                "&extraData=" + request.extraData +
                "&ipnUrl=" + ipnUrl +
                "&orderId=" + request.orderId +
                "&orderInfo=" + request.orderInfo +
                "&partnerCode=" + partnerCode +
                "&redirectUrl=" + redirectUrl +
                "&requestId=" + request.requestId +
                "&requestType=" + requestType
                ;

            MoMoLibrary moMoLibrary = new MoMoLibrary();
            string signature = moMoLibrary.signSHA256(rawHash, secretKey);

            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", request.requestId },
                { "amount", request.amount },
                { "orderId", request.orderId },
                { "orderInfo", request.orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "en" },
                { "extraData", request.extraData },
                { "requestType", requestType },
                { "signature", signature }
            };

            string responseFromMomo = MoMoLibrary.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            if (jmessage.GetValue("payUrl").IsNullOrEmpty())
            {
                return jmessage.GetValue("payUrl").ToString();
            }
            else
            {
                return jmessage.GetValue("message").ToString();
            }
        }

        public async Task HandlePaymentResponeIPN(MoMoResponse momoResponse)
        {
            var rawHash = "accessKey=" + accessKey +
                   "&amount=" + momoResponse.amount +
                   "&extraData=" + momoResponse.extraData +
                   "&message=" + momoResponse.message +
                   "&orderId=" + momoResponse.orderId +
                   "&orderInfo=" + momoResponse.orderInfo +
                   "&orderType=" + momoResponse.orderType +
                   "&partnerCode=" + momoResponse.partnerCode +
                   "&payType=" + momoResponse.payType +
                   "&requestId=" + momoResponse.requestId +
                   "&responseTime=" + momoResponse.responseTime +
                   "&resultCode=" + momoResponse.resultCode +
                   "&transId=" + momoResponse.transId;

            var payment = (await _paymentRepository.GetAsync(i => i.PaymentID == momoResponse.orderId)).FirstOrDefault();
            if (payment == null)
            {
                throw new NotFoundException("No payment found");
            }
            else
            {
                MoMoLibrary moMoLibrary = new MoMoLibrary();
                bool checkSignature = moMoLibrary.ValidateSignature(rawHash, secretKey, momoResponse.signature);
                if (checkSignature && momoResponse.resultCode == 0)
                {
                    payment.PaymentStatus = PaymentStatus.Success;
                    payment.Signature = momoResponse.signature;
                    payment.TransactionID = momoResponse.transId.ToString();
                }
                else
                {
                    payment.TransactionID = momoResponse.transId.ToString();
                    payment.PaymentStatus = PaymentStatus.Fail;
                }
            }
            await _paymentRepository.UpdateAsync(payment);
            if (payment.PaymentStatus == PaymentStatus.Success)
            {
                var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.SessionOrderID == payment.SessionOrderID)).FirstOrDefault();
                if (sessionOrder != null && sessionOrder.Status == SessionOrderStatus.Created)
                {
                    if (payment.Amount < sessionOrder.TotalPrice)
                    {
                        sessionOrder.Status = SessionOrderStatus.Deposited;
                    }
                    else if (payment.Amount == sessionOrder.TotalPrice)
                    {
                        sessionOrder.Status = SessionOrderStatus.Waiting;
                    }
                    await _sessionOrderRepository.UpdateAsync(sessionOrder);
                }
                else if (sessionOrder != null && sessionOrder.Status == SessionOrderStatus.Deposited)
                {
                    var paymentCheck = (await _paymentRepository.GetAsync(i => i.SessionOrderID == sessionOrder.SessionOrderID && i.PaymentStatus == PaymentStatus.Success)).ToList();
                    if (paymentCheck.Sum(i=>i.Amount) == sessionOrder.TotalPrice)
                    {
                        sessionOrder.Status = SessionOrderStatus.Waiting;
                        await _sessionOrderRepository.UpdateAsync(sessionOrder);
                    }
                }
            }
        }
        public async Task Return(IQueryCollection queryString)
        {

            MoMoResponse response = new MoMoResponse();
            // Populate MoMoResponse object using reflection
            PropertyInfo[] properties = typeof(MoMoResponse).GetProperties();
            foreach (var property in properties)
            {
                if (queryString.TryGetValue(property.Name, out var value))
                {
                    try
                    {
                        if (property.PropertyType == typeof(Guid))
                        {
                            property.SetValue(response, Guid.Parse(value));
                        }
                        else if (property.PropertyType == typeof(long))
                        {
                            property.SetValue(response, long.Parse(value));
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            property.SetValue(response, int.Parse(value));
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(response, value.ToString());
                        }
                        else
                        {
                            // Handle other property types if needed
                            property.SetValue(response, Convert.ChangeType(value, property.PropertyType));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error converting value for property {property.Name}: {ex.Message}");
                    }
                }
            }
            await HandlePaymentResponeIPN(response);

        }
    }
}
