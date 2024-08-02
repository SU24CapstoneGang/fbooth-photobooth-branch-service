using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp.ImgHash;
using PhotoboothBranchService.Application.Common;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.Services.EmailServices;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PhotoboothBranchService.Application.Services.MoMoServices
{
    public class MoMoService : IMoMoService
    {
        private readonly string momo_Api_Pay;
        private readonly string momo_Api_Refund;
        private readonly string accessKey;
        private readonly string secretKey;
        private readonly string partnerCode;
        //private readonly string redirectUrl;
        private readonly string ipnUrl;
        private readonly string public_key;
        private readonly ITransactionRepository _paymentRepository;

        public MoMoService(ITransactionRepository paymentRepository)
        {
            momo_Api_Pay = JsonHelper.GetFromAppSettings("MoMo:momo_Api");
            momo_Api_Refund = JsonHelper.GetFromAppSettings("MoMo:momo_refund_endpoint");
            accessKey = JsonHelper.GetFromAppSettings("MoMo:accessKey");
            secretKey = JsonHelper.GetFromAppSettings("MoMo:secretKey");
            partnerCode = JsonHelper.GetFromAppSettings("MoMo:partnerCode");
            //redirectUrl = JsonHelper.GetFromAppSettings("MoMo:redirectUrl");
            ipnUrl = JsonHelper.GetFromAppSettings("MoMo:ipnUrl");
            public_key = JsonHelper.GetFromAppSettings("MoMo:public_key");
            _paymentRepository = paymentRepository;
        }

        public string CreatePayment(MoMoRequest request)
        {
            string endpoint = momo_Api_Pay;

            string requestType = "captureWallet";

            //Before sign HMAC SHA256 signature
            string rawHash = "accessKey=" + accessKey +
                "&amount=" + request.amount +
                "&extraData=" + request.extraData +
                "&ipnUrl=" + ipnUrl +
                "&orderId=" + request.orderId +
                "&orderInfo=" + request.orderInfo +
                "&partnerCode=" + partnerCode +
                //"&redirectUrl=" + redirectUrl +
                "&redirectUrl=" + request.ReturnUrl +
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
                { "redirectUrl", request.ReturnUrl },
                //{ "redirectUrl", redirectUrl },
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
        public async Task<(Transaction transaction, MomoIPNResponse iPNResponse)> HandlePaymentResponeIPN(MoMoResponse momoResponse)
        {
            MoMoLibrary moMoLibrary = new MoMoLibrary();
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

            MomoIPNResponse iPNResponse = new MomoIPNResponse{
                partnerCode = momoResponse.partnerCode,
                requestId = momoResponse.requestId.ToString(),
                orderId = momoResponse.orderId.ToString(),
                resultCode = momoResponse.resultCode,
                message = momoResponse.message,
                responseTime = momoResponse.responseTime,
                extraData = momoResponse.extraData
            };

            var payment = (await _paymentRepository.GetAsync(i => i.TransactionID == momoResponse.orderId)).FirstOrDefault();
            if (payment == null)
            {
                throw new NotFoundException("No payment found");
            }
            else
            {
                string signature = moMoLibrary.signSHA256(rawHash, secretKey);
                bool checkSignature = moMoLibrary.ValidateSignature(rawHash, secretKey, momoResponse.signature);
                if (checkSignature && momoResponse.resultCode == 0)
                {
                    payment.TransactionStatus = TransactionStatus.Success;
                    payment.GatewayTransactionID = momoResponse.transId.ToString();
                }
                else
                {
                    payment.GatewayTransactionID = momoResponse.transId.ToString();
                    payment.TransactionStatus = TransactionStatus.Fail;
                }
            }
            await _paymentRepository.UpdateAsync(payment);

            rawHash = "accessKey=" + accessKey +
                   "&extraData=" + momoResponse.extraData +
                   "&message=" + momoResponse.message +
                   "&orderId=" + momoResponse.orderId +
                   "&partnerCode=" + momoResponse.partnerCode +
                   "&requestId=" + momoResponse.requestId +
                   "&responseTime=" + momoResponse.responseTime +
                   "&resultCode=" + momoResponse.resultCode;
            iPNResponse.signature = moMoLibrary.signSHA256(rawHash, secretKey);
            return (payment, iPNResponse);
        }
        public async Task<Transaction> Return(IQueryCollection queryString)
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
            return (await HandlePaymentResponeIPN(response)).transaction;

        }
        public async Task<MoMoRefundResponse> RefundById(Guid paymentID, long refundAmounf)
        {
            var payment = (await _paymentRepository.GetAsync(i => i.TransactionID == paymentID)).FirstOrDefault();
            if (payment != null)
            {
                string merchantRefId = payment.TransactionID.ToString();
                string momoTransId = payment.GatewayTransactionID;
                string version = "2.0";
                string requestId = Guid.NewGuid().ToString();
                string description = "Hoan tien giao dich" + payment.TransactionID.ToString();
                long amount = refundAmounf;
                MoMoLibrary moMoLibrary = new MoMoLibrary();
                string hash = moMoLibrary.buildRefundHash(partnerCode, merchantRefId, momoTransId, amount,
                description, public_key);

                string jsonRequest = "{\"partnerCode\":\"" +
                partnerCode + "\",\"requestId\":\"" +
                requestId + "\",\"version\":" +
                version + ",\"hash\":\"" +
                hash + "\"}";

                string responseMomo = MoMoLibrary.sendPaymentRequest(momo_Api_Refund, jsonRequest.ToString());
                if (responseMomo != null)
                {
                    MoMoRefundResponse response = JsonConvert.DeserializeObject<MoMoRefundResponse>(responseMomo);
                    response.RequestId = new Guid(requestId);
                    return response;
                }
                else
                {
                    throw new Exception("Error happend in refund progress");
                }
            }
            else
            {
                throw new NotFoundException("Payment not found");
            }
        }

    }
}
