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
        private readonly IBookingRepository _bookingRepository;
        private readonly ITransactionRepository _paymentRepository;

        public RefundService(IMapper mapper,IRefundRepository refundRepository, IMoMoService moMoService, IVNPayService vNPayService, IBookingRepository sessionOrderRepository, ITransactionRepository paymentRepository)
        {
            _mapper = mapper;
            _refundRepository = refundRepository;
            _moMoService = moMoService;
            _vNPayService = vNPayService;
            _bookingRepository = sessionOrderRepository;
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
        public async Task<IEnumerable<RefundResponse>> RefundByBookingID(Guid orderId, bool isFullRefund, string? ipAddress)
        {
            var payments = (await _paymentRepository.GetAsync(i => i.BookingID == orderId && i.TransactionStatus == TransactionStatus.Success)).ToList();
            var responseList = new List<RefundResponse>();
            var failList = new List<TransactionResponse>();
            foreach (var trans in payments)
            {
                    responseList.Add(await this.RefundByTransID(trans.TransactionID, isFullRefund, ipAddress));
            }
            return (responseList);
        }
        public async Task<RefundResponse> RefundByTransID(Guid paymentId, bool isFullRefund, string? ipAddress)
        {
            var transaction = (await _paymentRepository.GetAsync(i => i.TransactionID == paymentId, i => i.PaymentMethod)).FirstOrDefault();
            if (transaction == null)
            {
                throw new NotFoundException("Not found Payment ID to refund");
            }
            if (transaction.TransactionStatus == TransactionStatus.RefundedFull)
            {
                throw new BadRequestException("Already refund this payment, can not refund anymore");
            }
            if (transaction.PaymentMethod.Status != PaymentMethodStatus.Active)
            {
                throw new BadRequestException("This method not availble to refund anymore");
            }
            if (ipAddress.IsNullOrEmpty())
            {
                IPAddress localIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
                ipAddress = localIp.ToString();
            }
            Refund? refund;
            var booking = (await _bookingRepository.GetAsync(i => i.BookingID == transaction.BookingID, i => i.FullPaymentPolicy)).FirstOrDefault();
            if (booking == null)
            {
                throw new BadRequestException("No booking found");
            }
            switch (transaction.PaymentMethod.PaymentMethodName)
            {
                case "VNPay":
                    VnpayRefundRequest refundRequest = new VnpayRefundRequest
                    {
                        Amount = isFullRefund ? transaction.Amount : (transaction.Amount * booking.FullPaymentPolicy.RefundPercent / 100),
                        PayDate = transaction.TransactionDateTime,
                        RefundCategory = isFullRefund ? "02" : "03",
                        PaymentID = GuidAlphanumericConverter.GuidToAlphanumeric(transaction.TransactionID),
                        TransId = transaction.GatewayTransactionID,
                        User = GuidAlphanumericConverter.GuidToAlphanumeric(booking.CustomerID),
                    };
                    var responseVNPay = await _vNPayService.RefundTransaction(refundRequest, ipAddress);

                    if (responseVNPay.Vnp_ResponseCode == "00")
                    {
                        refund = new Refund
                        {
                            RefundID = Guid.NewGuid(),
                            TransactionID = transaction.TransactionID,
                            Amount = long.Parse(responseVNPay.Vnp_Amount.Substring(0, responseVNPay.Vnp_Amount.Length - 2)),
                            RefundDateTime = DateTime.ParseExact(responseVNPay.Vnp_PayDate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture),
                            GatewayTransactionID = responseVNPay.Vnp_TransactionNo,
                            Description = responseVNPay.Vnp_OrderInfo
                        };
                        switch (responseVNPay.Vnp_TransactionStatus)
                        {
                            case "00":
                                refund.Status = RefundStatus.Success;
                                break;
                            case "05":
                            case "06":
                                refund.Status = RefundStatus.PartnerProcessing;
                                break;
                            default:
                                refund.Status = RefundStatus.Fail;
                                break;
                        }

                        await _refundRepository.AddAsync(refund);
                        transaction.TransactionStatus = responseVNPay.Vnp_TransactionType.Equals("02") || transaction.Amount == await this.TotalRefund(transaction.TransactionID) ? TransactionStatus.RefundedFull : TransactionStatus.RefundedPartial;
                    }
                    else
                    {
                        throw new BadRequestException("An error in refund process");
                    }
                    break;
                case "MoMo":
                    var refundAmount = isFullRefund ? transaction.Amount : (transaction.Amount * booking.FullPaymentPolicy.RefundPercent / 100);
                    MoMoRefundResponse responseMoMo = await _moMoService.RefundById(transaction.TransactionID, refundAmount);
                    if (responseMoMo.Status == 0)
                    {
                        refund = new Refund
                        {
                            RefundID = responseMoMo.RequestId,
                            TransactionID = transaction.TransactionID,
                            Amount = responseMoMo.Amount,
                            RefundDateTime = DateTimeHelper.GetVietnamTimeNow(),
                            GatewayTransactionID = responseMoMo.Transid,
                            Description = responseMoMo.Message
                        };
                        await _refundRepository.AddAsync(refund);
                        transaction.TransactionStatus = responseMoMo.Amount == transaction.Amount || transaction.Amount == await this.TotalRefund(transaction.TransactionID) ? TransactionStatus.RefundedFull : TransactionStatus.RefundedPartial;
                    }
                    else
                    {
                        throw new Exception(responseMoMo.Message);
                    }
                    break;
                default:
                    throw new BadRequestException("Payment method not availbe to use, please try later");
            }
            await _paymentRepository.UpdateAsync(transaction);
            if (transaction.TransactionStatus == TransactionStatus.RefundedPartial)
            {
                booking.Status = BookingStatus.Refunded;
                booking.PaymentStatus = PaymentStatus.Refunded;
                await _bookingRepository.UpdateAsync(booking);
            }
            return _mapper.Map<RefundResponse>(refund);
        }

        private async Task<long> TotalRefund(Guid transID) {
            var refunds = await _refundRepository.GetAsync(i => i.TransactionID == transID && i.Status != RefundStatus.Fail);
            return refunds.Sum(i => i.Amount);
        }
    }
}
