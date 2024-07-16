﻿using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhotoboothBranchService.Application.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private string smtpServerName;
        private string smtpPortNumber;
        private string emailHostAddress;
        private string emailHostPassword;

        private IAccountRepository _accountRepository;
        private IPaymentRepository _paymentRepository;
        private ISessionOrderRepository _sessionOrderRepository;
        private IBoothBranchRepository _boothBranchRepository;
        private IServiceItemRepository _serviceItemRepository;

        public EmailService(IAccountRepository accountRepository, 
            IPaymentRepository paymentRepository, 
            ISessionOrderRepository sessionOrderRepository, 
            IBoothBranchRepository boothBranchRepository, 
            IServiceItemRepository serviceItemRepository)
        {
            this.smtpServerName = JsonHelper.GetFromAppSettings("EmailConfig:SmtpServerName");
            this.smtpPortNumber = JsonHelper.GetFromAppSettings("EmailConfig:SmtpPortNumber");
            this.emailHostAddress = JsonHelper.GetFromAppSettings("EmailConfig:EmailHostAddress");
            this.emailHostPassword = JsonHelper.GetFromAppSettings("EmailConfig:EmailHostPassword");
            _sessionOrderRepository = sessionOrderRepository;
            _accountRepository = accountRepository;
            _paymentRepository = paymentRepository;
            _boothBranchRepository = boothBranchRepository;
            _serviceItemRepository = serviceItemRepository;
        }

        public async Task SendBillInformation(Guid paymentId)
        {
            var payment = (await _paymentRepository.GetAsync(i => i.PaymentID == paymentId, i=>i.PaymentMethod)).FirstOrDefault();
            if (payment == null)
            {
                throw new NotFoundException("Not found payment");
            }
            var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.SessionOrderID == payment.SessionOrderID)).FirstOrDefault();
            if (sessionOrder == null)
            {
                throw new NotFoundException("Not found Order of payment");
            }
            var user = (await _accountRepository.GetAsync(i => i.AccountID == sessionOrder.AccountID)).FirstOrDefault();
            if (user == null)
            {
                throw new NotFoundException("Not found user");
            }
            string subject = "Payment Information";
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine("<p>Here is your payment's information</p>");
            sbBody.AppendLine("<p></p>");
            sbBody.AppendLine($"<p>Payment method: {payment.PaymentMethod.PaymentMethodName}</p>");
            sbBody.AppendLine($"<p>Payment amount: {payment.Amount.ToString()}</p>");
            sbBody.AppendLine($"<p>Payment time: {payment.PaymentDateTime.ToString("dddd, MMMM dd, yyyy h:mm tt")}</p>");

            await this.SendEmail(user.Email, subject, sbBody.ToString());
        }

        public async Task SendBookingInformation(Guid sessionOrderId)
        {
            var sessionOrder = (await _sessionOrderRepository
                .GetAsync(i => i.SessionOrderID == sessionOrderId,
                    includeProperties: new Expression<Func<SessionOrder, object>>[]
                        {
                            i => i.ServiceItems,
                            i => i.SessionPackage,
                            i => i.Booth,
                        }
                ))
                .FirstOrDefault();
            if (sessionOrder == null)
            {
                throw new NotFoundException("Not found Order of payment");
            }
            var user = (await _accountRepository.GetAsync(i => i.AccountID == sessionOrder.AccountID)).FirstOrDefault();
            if (user == null)
            {
                throw new NotFoundException("Not found user");
            }
            var branch = (await _boothBranchRepository.GetAsync(i => i.BoothBranchID == sessionOrder.Booth.PhotoBoothBranchID)).FirstOrDefault();
            if (branch == null)
            {
                throw new NotFoundException("Not found branch");
            }
            string subject = "Booking information";
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine("<p>Here is your booking's information</p>");
            sbBody.AppendLine("<p></p>");
            sbBody.AppendLine($"<p>Session Order ID: {sessionOrder.SessionOrderID}</p>");

            sbBody.AppendLine($"<p>Branch name: {branch.BranchName}</p>");
            sbBody.AppendLine($"<p>Branch adress ID: {branch.Address}</p>");

            sbBody.AppendLine($"<p>Booth name: {sessionOrder.Booth.BoothName}</p>");

            sbBody.AppendLine($"<p>Package Information:</p>");
            sbBody.AppendLine($"<p style='text-indent: 30px;'>Package name: {sessionOrder.SessionPackage.SessionPackageName}</p>");
            sbBody.AppendLine($"<p style='text-indent: 30px;'>Time to send photos to mail: {sessionOrder.SessionPackage.EmailSendCount} time(s)</p>");
            sbBody.AppendLine($"<p style='text-indent: 30px;'>Print picture times: {sessionOrder.SessionPackage.PrintCount} time(s)</p>");
            sbBody.AppendLine($"<p style='text-indent: 30px;'>Duration: {sessionOrder.SessionPackage.Duration} minutes</p>");
            sbBody.AppendLine($"<p style='text-indent: 30px;'>Price: {sessionOrder.SessionPackage.Price:N0} VND</p>");

            if (sessionOrder.ServiceItems.Count > 0)
            {
                var serviceItemList = (await _serviceItemRepository
                    .GetAsync(i => sessionOrder.ServiceItems.Select(i => i.ServiceItemID).ToList().Contains(i.ServiceItemID), i => i.Service)
                    ).ToList();
                if (serviceItemList != null && serviceItemList.Count == sessionOrder.ServiceItems.Count)
                {
                    sbBody.AppendLine($"<p>Service(s) in Order:</p>");

                    sbBody.AppendLine("<table style='width:100%; border-collapse: collapse;'>");
                    sbBody.AppendLine("<tr>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Service Name</th>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Quantity</th>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Unit Price</th>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Subtotal</th>");
                    sbBody.AppendLine("</tr>");

                    foreach (var serviceItem in serviceItemList)
                    {
                        sbBody.AppendLine("<tr>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{serviceItem.Service.ServiceName}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{serviceItem.Quantity}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{serviceItem.UnitPrice:N0}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{serviceItem.SubTotal:N0}</td>");
                        sbBody.AppendLine("</tr>");
                    }

                    sbBody.AppendLine("<tr>");
                    sbBody.AppendLine("<td colspan='3' style='border: 1px solid black; padding: 8px; text-align: right;'><strong>Total Price (With Package's price):</strong></td>");
                    sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'><strong>{sessionOrder.TotalPrice:N0}</strong> VND</td>");
                    sbBody.AppendLine("</tr>");

                    sbBody.AppendLine("</table>");
                } else
                {
                    throw new Exception("Service not found in Item list");
                }
            }
            else{
                sbBody.AppendLine($"<p>Total price: {sessionOrder.TotalPrice}</p>");
            }

            sbBody.AppendLine($"<p>Start Time: {sessionOrder.StartTime.ToString("dddd, MMMM dd, yyyy h:mm tt")}</p>");
            sbBody.AppendLine($"<p>End Time: {sessionOrder.EndTime.Value.ToString("dddd, MMMM dd, yyyy h:mm tt")}</p>");
            sbBody.AppendLine($"<p>Validate code (Enter this code to booth):<strong> {sessionOrder.ValidateCode.ToString()}</strong> </p>");
            sbBody.AppendLine($"<p>We hope you arrive 5 minutes before the start time to receive instructions from the staff.</p>");
            await this.SendEmail(user.Email, subject, sbBody.ToString());
        }

        private async Task SendEmail(string emailClientAddress, string subject, string body)
        {
            try
            {
                if (!IsValidEmail(emailClientAddress))
                {
                    throw new BadRequestException("Wrong e-mail format \nPlease enter your e-mail correctly\nexample@mail.com");
                }

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtpServerName)
                {
                    Credentials = new NetworkCredential(emailHostAddress, emailHostPassword),
                    Port = int.Parse(smtpPortNumber),
                    EnableSsl = true
                };

                mail.From = new MailAddress(emailHostAddress);
                mail.To.Add(emailClientAddress);
                mail.Subject = subject;

                StringBuilder sbBody = new StringBuilder();
                sbBody.AppendLine("<html>");
                sbBody.AppendLine("<body>");
                sbBody.AppendLine("<h1>Hi,</h1>");
                sbBody.AppendLine("<p>Thank you for using our photo booth! </p>");
                sbBody.AppendLine("<p></p>");

                //insert body need to send
                sbBody.AppendLine(body);

                sbBody.AppendLine("<p></p>");
                sbBody.AppendLine("<p>Best regards,</p>");
                sbBody.AppendLine("<p>FBooth Team</p>");
                sbBody.AppendLine("</body>");
                sbBody.AppendLine("</html>"); 
                mail.Body = sbBody.ToString();
                mail.IsBodyHtml = true;

                await SmtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
