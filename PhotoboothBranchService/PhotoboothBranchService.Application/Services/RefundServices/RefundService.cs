using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using OpenCvSharp;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Application.DTOs.Refund;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Application.DTOs.VNPayPayment;
using PhotoboothBranchService.Application.Services.EmailServices;
using PhotoboothBranchService.Application.Services.MoMoServices;
using PhotoboothBranchService.Application.Services.VNPayServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static OpenCvSharp.Stitcher;

namespace PhotoboothBranchService.Application.Services.RefundServices
{
    public class RefundService : IRefundService
    {
        private readonly IMapper _mapper;
        private readonly IRefundRepository _refundRepository;
        private readonly IMoMoService _moMoService;
        private readonly IVNPayService _vNPayService;
        private readonly IBookingRepository _bookingRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEmailService _emailService;
        private readonly IAccountRepository _accountRepository;
        public RefundService(IMapper mapper,IRefundRepository refundRepository, IMoMoService moMoService, IVNPayService vNPayService, IBookingRepository sessionOrderRepository, ITransactionRepository paymentRepository, IEmailService emailService, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _refundRepository = refundRepository;
            _moMoService = moMoService;
            _vNPayService = vNPayService;
            _bookingRepository = sessionOrderRepository;
            _transactionRepository = paymentRepository;
            _emailService = emailService;
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<RefundResponse>> GetAllAsync()
        {
            var refunds = await _refundRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RefundResponse>>(refunds.ToList().OrderByDescending(i => i.RefundDateTime));
        }

        public async Task<IEnumerable<RefundResponse>> GetAllPagingAsync(RefundFilter filter, PagingModel paging)
        {
            var refunds = (await _refundRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listRefundResponse = _mapper.Map<IEnumerable<RefundResponse>>(refunds);
            return listRefundResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).OrderByDescending(i => i.RefundDateTime);
        }

        public async Task<RefundResponse> GetByIdAsync(Guid id)
        {
            var refunds = await _refundRepository.GetAsync(m => m.RefundID == id);
            return _mapper.Map<RefundResponse>(refunds.FirstOrDefault());
        }

        //refund 
        public async Task<IEnumerable<RefundResponse>> RefundByBookingID(Guid orderId, bool isFullRefund, string? ipAddress, string? email)
        {
            var payments = (await _transactionRepository.GetAsync(i => i.BookingID == orderId && i.Status == TransactionStatus.Success)).ToList();
            var responseList = new List<RefundResponse>();
            foreach (var trans in payments)
            {
                try
                {
                    responseList.Add(await this.RefundByTransID(trans.PaymentID, isFullRefund, ipAddress, email));
                }
                catch (Exception ex)
                {
                    responseList.Add(new RefundResponse
                    {
                        TransactionID = new Guid(),
                        Status = RefundStatus.Fail,
                        Description = "",
                        RefundDateTime = DateTimeHelper.GetVietnamTimeNow(),
                        GatewayTransactionID = trans.TransactionID,
                        ResponseMessage = ex.Message,
                    });
                }
            }
            return (responseList);
        }
        public async Task<RefundResponse> RefundByTransID(Guid paymentId, bool isFullRefund, string? ipAddress, string? email)
        {
            var transaction = (await _transactionRepository.GetAsync(i => i.PaymentID == paymentId, i => i.PaymentMethod)).FirstOrDefault();
            if (transaction == null)
            {
                throw new NotFoundException("Not found Payment ID to refund");
            }
            if (transaction.Status == TransactionStatus.RefundedFull)
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
            var booking = (await _bookingRepository.GetAsync(i => i.BookingID == transaction.BookingID, includeProperties: new Expression<Func<Booking, object>>[]
            {
                u => u.FullPaymentPolicy,
                u => u.Account
            })).FirstOrDefault();
            if (booking == null)
            {
                throw new BadRequestException("No booking found");
            }
            var refundAmount = isFullRefund ? transaction.Amount - await this.TotalRefund(transaction.PaymentID) : (transaction.Amount * booking.FullPaymentPolicy.RefundPercent / 100);
            switch (transaction.PaymentMethod.PaymentMethodName)
            {
                case "VNPay":
                    VnpayRefundRequest refundRequest = new VnpayRefundRequest
                    {
                        Amount = refundAmount,
                        PayDate = transaction.PaymentDateTime,
                        RefundCategory = isFullRefund ? "02" : "03",
                        PaymentID = GuidAlphanumericConverter.GuidToAlphanumeric(transaction.PaymentID),
                        TransId = transaction.TransactionID,
                        User = GuidAlphanumericConverter.GuidToAlphanumeric(booking.CustomerID),
                    };
                    var responseVNPay = await _vNPayService.RefundTransaction(refundRequest, ipAddress);

                    if (responseVNPay.Vnp_ResponseCode == "00")
                    {
                        refund = new Refund
                        {
                            RefundID = Guid.NewGuid(),
                            PaymentID = transaction.PaymentID,
                            Amount = long.Parse(responseVNPay.Vnp_Amount.Substring(0, responseVNPay.Vnp_Amount.Length - 2)),
                            RefundDateTime = DateTime.ParseExact(responseVNPay.Vnp_PayDate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture),
                            TransactionID = responseVNPay.Vnp_TransactionNo,
                            Description = responseVNPay.Vnp_OrderInfo,
                            ResponseMessage = responseVNPay.Vnp_Message,
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

                        transaction.Status = responseVNPay.Vnp_TransactionType.Equals("02") || transaction.Amount == await this.TotalRefund(transaction.PaymentID) ? TransactionStatus.RefundedFull : TransactionStatus.RefundedPartial;
                    }
                    else
                    {
                        throw new BadRequestException("An error in refund process");
                    }
                    break;
                case "MoMo":
                    string description = "Refund for transaction " + transaction.PaymentID.ToString();
                    MoMoRefundResponse responseMoMo = await _moMoService.RefundById(transaction.PaymentID, refundAmount, description);
                    refund = new Refund
                    {
                        RefundID = responseMoMo.RequestId,
                        PaymentID = transaction.PaymentID,
                        Amount = responseMoMo.Amount,
                        RefundDateTime = DateTimeHelper.GetVietnamTimeNow(),
                        TransactionID = responseMoMo.Transid,
                        ResponseMessage = responseMoMo.Message,
                        Description = description,
                    };
                    if (responseMoMo.Status == 0)
                    {
                        refund.Status = RefundStatus.Success;
                        transaction.Status = responseMoMo.Amount == transaction.Amount || transaction.Amount == await this.TotalRefund(transaction.PaymentID) ? TransactionStatus.RefundedFull : TransactionStatus.RefundedPartial;
                    }
                    else
                    {
                        refund.Status = RefundStatus.Fail;
                    }
                    break;
                case "Cash":
                    if (email.IsNullOrEmpty())
                    {
                        throw new ForbiddenAccessException();
                    }
                    var account = (await _accountRepository.GetAsync(i => i.Email.Equals(email))).FirstOrDefault();
                    if (account == null)
                    {
                        throw new ForbiddenAccessException();
                    }
                    if (account.Role == AccountRole.Customer)
                    {
                        throw new ForbiddenAccessException("This method can not be use by customer account");
                    }
                    if (account.Role != AccountRole.Admin)
                    {
                        if (account.BranchID.Value != booking.Booth.BranchID)
                        {
                            throw new ForbiddenAccessException("Booking and Staff are not from same branch to do this refund");
                        }
                    }
                    refund = new Refund
                    {
                        RefundID = Guid.NewGuid(),
                        PaymentID = transaction.PaymentID,
                        Amount = refundAmount,
                        RefundDateTime = DateTimeHelper.GetVietnamTimeNow(),
                        TransactionID = account.AccountID.ToString(),
                        ResponseMessage = "Success",
                        Description = $"Refund for transaction {transaction.PaymentID}",
                        Status = RefundStatus.Success,
                    };
                    break;
                default:
                    throw new BadRequestException("Payment method not availbe to use, please try later");
            }
            await _refundRepository.AddAsync(refund);
            if (refund.Status != RefundStatus.Fail)
            {
                await _emailService.SendRefundBillInformation(refund.RefundID);
            }
            await _transactionRepository.UpdateAsync(transaction);
            return _mapper.Map<RefundResponse>(refund);
        }

        private async Task<long> TotalRefund(Guid transID) {
            var refunds = await _refundRepository.GetAsync(i => i.PaymentID == transID && i.Status != RefundStatus.Fail);
            return refunds.Sum(i => i.Amount);
        }
    }
}
