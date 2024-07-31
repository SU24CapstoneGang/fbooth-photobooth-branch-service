using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Application.DTOs.Refund;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Application.DTOs.VNPayPayment;
using PhotoboothBranchService.Application.Services.MoMoServices;
using PhotoboothBranchService.Application.Services.VNPayServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.RefundServices
{
    public class RefundService : IRefundService
    {
        private readonly IMapper _mapper;
        private readonly IRefundRepository _refundRepository;
        private readonly IMoMoService _moMoService;
        private readonly IVNPayService _vNPayService;
        private readonly IBookingRepository _sessionOrderRepository;
        private readonly ITransactionRepository _paymentRepository;

        public RefundService(IMapper mapper,IRefundRepository refundRepository, IMoMoService moMoService, IVNPayService vNPayService, IBookingRepository sessionOrderRepository, ITransactionRepository paymentRepository)
        {
            _mapper = mapper;
            _refundRepository = refundRepository;
            _moMoService = moMoService;
            _vNPayService = vNPayService;
            _sessionOrderRepository = sessionOrderRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<RefundResponse>> GetAllAsync()
        {
            var refunds = await _refundRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RefundResponse>>(refunds.ToList());
        }

        public async Task<IEnumerable<RefundResponse>> GetAllPagingAsync(RefundFilter filter, PagingModel paging)
        {
            var refunds = (await _refundRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listRefundResponse = _mapper.Map<IEnumerable<RefundResponse>>(refunds);
            return listRefundResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        public async Task<RefundResponse> GetByIdAsync(Guid id)
        {
            var refunds = await _refundRepository.GetAsync(m => m.RefundID == id);
            return _mapper.Map<RefundResponse>(refunds.FirstOrDefault());
        }

        //refund 
        public async Task<(IEnumerable<RefundResponse> refundResponses, IEnumerable<TransactionResponse> failPayment)> RefundByOrderId(Guid orderId, bool isFullRefund, string? ipAddress)
        {
            var payments = (await _paymentRepository.GetAsync(i => i.BookingID == orderId && i.TransactionStatus == TransactionStatus.Success)).ToList();
            var responseList = new List<RefundResponse>();
            var failList = new List<TransactionResponse>();
            foreach (var payment in payments)
            {
                try
                {
                    responseList.Add(await this.RefundByPaymentID(payment.TransactionID, isFullRefund, ipAddress));
                }
                catch (Exception)
                {
                    failList.Add(_mapper.Map<TransactionResponse>(payment));
                }
            }
            return (responseList,failList);
        }
        public async Task<RefundResponse> RefundByPaymentID(Guid paymentId, bool isFullRefund, string? ipAddress)
        {
            //var payment = (await _paymentRepository.GetAsync(i => i.TransactionID == paymentId, i => i.PaymentMethod)).FirstOrDefault();
            //if (payment == null)
            //{
            //    throw new NotFoundException("Not found Payment ID to refund");
            //}
            //if (payment.PaymentStatus == PaymentStatus.RefundedFull)
            //{
            //    throw new BadRequestException("Already refund this payment, can not refund anymore");
            //}
            //if (payment.PaymentMethod.Status != PaymentMethodStatus.Active)
            //{
            //    throw new BadRequestException("This method not availble to refund anymore");
            //}
            //if (ipAddress.IsNullOrEmpty())
            //{
            //    IPAddress localIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            //    ipAddress = localIp.ToString();
            //}
            //Refund? refund;
            //switch (payment.PaymentMethod.PaymentMethodName)
            //{
            //    case "VNPay":
            //        var order = (await _sessionOrderRepository.GetAsync(i => i.BookingID == payment.SessionOrderID)).FirstOrDefault();
            //        VnpayRefundRequest refundRequest = new VnpayRefundRequest
            //        {
            //            Amount = isFullRefund ? payment.Amount : (payment.Amount / 10 * 5),
            //            PayDate = payment.TransactionDateTime,
            //            RefundCategory = isFullRefund ? "02" : "03",
            //            PaymentID = GuidAlphanumericConverter.GuidToAlphanumeric(payment.TransactionID),
            //            TransId = payment.GatewayTransactionID,
            //            User = GuidAlphanumericConverter.GuidToAlphanumeric(order.CustomerID.Value),
            //        };
            //        var responseVNPay = await _vNPayService.RefundTransaction(refundRequest, ipAddress);

            //        if (responseVNPay.Vnp_ResponseCode == "00")
            //        {
            //            refund = new Refund
            //            {
            //                RefundID = Guid.NewGuid(),
            //                PaymentID = payment.TransactionID,
            //                Amount = long.Parse(responseVNPay.Vnp_Amount.Substring(0, responseVNPay.Vnp_Amount.Length - 2)),
            //                RefundDateTime = DateTime.ParseExact(responseVNPay.Vnp_PayDate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture),
            //                TransactionID = responseVNPay.Vnp_TransactionNo,
            //                Description = responseVNPay.Vnp_OrderInfo
            //            };
            //            switch (responseVNPay.Vnp_TransactionStatus)
            //            {
            //                case "00":
            //                    refund.Status = RefundStatus.Success;
            //                    break;
            //                case "05":
            //                case "06":
            //                    refund.Status = RefundStatus.PartnerProcessing;
            //                    break;
            //                default:
            //                    refund.Status = RefundStatus.Fail;
            //                    break;
            //            }

            //            await _refundRepository.AddAsync(refund);
            //            payment.PaymentStatus = responseVNPay.Vnp_TransactionType.Equals("02") || payment.Amount == await this.TotalRefund(payment.TransactionID) ? PaymentStatus.RefundedFull : PaymentStatus.RefundedPartial;
            //        }
            //        else
            //        {
            //            throw new BadRequestException("An error in refund process");
            //        }
            //        break;
            //    case "MoMo":
            //        MoMoRefundResponse responseMoMo = await _moMoService.RefundById(payment.TransactionID, isFullRefund);
            //        if (responseMoMo.Status == 0)
            //        {
            //            refund = new Refund
            //            {
            //                RefundID = responseMoMo.RequestId,
            //                PaymentID = payment.TransactionID,
            //                Amount = responseMoMo.Amount,
            //                RefundDateTime = DateTime.Now,
            //                TransactionID = responseMoMo.Transid,
            //                Description = responseMoMo.Message
            //            };
            //            await _refundRepository.AddAsync(refund);
            //            payment.PaymentStatus = responseMoMo.Amount == payment.Amount || payment.Amount == await this.TotalRefund(payment.TransactionID) ? PaymentStatus.RefundedFull : PaymentStatus.RefundedPartial;
            //        }
            //        else
            //        {
            //            throw new BadRequestException("An error in refund process");
            //        }
            //        break;
            //    default:
            //        throw new BadRequestException("Payment method not availbe to use, please try later");
            //}
            //await _paymentRepository.UpdateAsync(payment);
            //return _mapper.Map<RefundResponse>(refund);
            return null;
        }

        private async Task<long> TotalRefund(Guid paymentID) {
            var refunds = await _refundRepository.GetAsync(i => i.PaymentID == paymentID && i.Status != RefundStatus.Fail);
            return refunds.Sum(i => i.Amount);
        }
    }
}
