using PhotoboothBranchService.Application.Common.Exceptions;
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
        private ITransactionRepository _paymentRepository;
        private IBookingRepository _sessionOrderRepository;
        private IBranchRepository _boothBranchRepository;
        private IServiceSessionRepository _serviceItemRepository;

        public EmailService(IAccountRepository accountRepository, 
            ITransactionRepository paymentRepository, 
            IBookingRepository sessionOrderRepository, 
            IBranchRepository boothBranchRepository, 
            IServiceSessionRepository serviceItemRepository)
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
            var payment = (await _paymentRepository.GetAsync(i => i.TransactionID == paymentId, i=>i.PaymentMethod)).FirstOrDefault();
            if (payment == null)
            {
                throw new NotFoundException("Not found payment");
            }
            var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.BookingID == payment.SessionOrderID)).FirstOrDefault();
            if (sessionOrder == null)
            {
                throw new NotFoundException("Not found Order of payment");
            }
            var user = (await _accountRepository.GetAsync(i => i.AccountID == sessionOrder.CustomerID)).FirstOrDefault();
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
            sbBody.AppendLine($"<p>Payment time: {payment.TransactionDateTime.ToString("dddd, MMMM dd, yyyy h:mm tt")}</p>");

            await this.SendEmail(user.Email, subject, sbBody.ToString());
        }

        public async Task SendBookingInformation(Guid sessionOrderId)
        {
            var sessionOrder = (await _sessionOrderRepository
                .GetAsync(i => i.BookingID == sessionOrderId,
                    includeProperties: new Expression<Func<Booking, object>>[]
                        {
                            i => i.BookingServices,
                            i => i.Booth,
                        }
                ))
                .FirstOrDefault();
            if (sessionOrder == null)
            {
                throw new NotFoundException("Not found Order of payment");
            }
            var user = (await _accountRepository.GetAsync(i => i.AccountID == sessionOrder.CustomerID)).FirstOrDefault();
            if (user == null)
            {
                throw new NotFoundException("Not found user");
            }
            var branch = (await _boothBranchRepository.GetAsync(i => i.BranchID == sessionOrder.Booth.BranchID)).FirstOrDefault();
            if (branch == null)
            {
                throw new NotFoundException("Not found branch");
            }
            string subject = "Booking information";
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine("<p>Here is your booking's information</p>");
            sbBody.AppendLine("<br>");
            sbBody.AppendLine($"<p><strong>Session Order ID:</strong> {sessionOrder.BookingID}</p>");

            sbBody.AppendLine("<h3>Branch Details</h3>");
            sbBody.AppendLine($"<p><strong>Branch Name:</strong> {branch.BranchName}</p>");
            sbBody.AppendLine($"<p><strong>Branch Address:</strong> {branch.Address}</p>");
            sbBody.AppendLine($"<p><strong>Booth Name:</strong> {sessionOrder.Booth.BoothName}</p>");

            sbBody.AppendLine("<h3>Package Information</h3>");

            if (sessionOrder.BookingServices.Count > 0)
            {
                var serviceItemList = (await _serviceItemRepository
                    .GetAsync(i => sessionOrder.BookingServices.Select(i => i.BookingServiceID).ToList().Contains(i.BookingServiceID), i => i.Service)
                    ).ToList();
                if (serviceItemList != null && serviceItemList.Count == sessionOrder.BookingServices.Count)
                {
                    sbBody.AppendLine("<p>Service(s) in Order:</p>");
                    sbBody.AppendLine("<br>");
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
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{serviceItem.Service.PackageName}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{serviceItem.Quantity}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{serviceItem.Price:N0}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{serviceItem.SubTotal:N0}</td>");
                        sbBody.AppendLine("</tr>");
                    }

                    sbBody.AppendLine("<tr>");
                    sbBody.AppendLine("<td colspan='3' style='border: 1px solid black; padding: 8px; text-align: right;'><strong>Total Price (With Package's price):</strong></td>");
                    sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'><strong>{sessionOrder.PaymentAmount:N0}</strong> VND</td>");
                    sbBody.AppendLine("</tr>");

                    sbBody.AppendLine("</table>");
                } else
                {
                    throw new Exception("Service not found in Item list");
                }
            }
            else{
                sbBody.AppendLine($"<p>Total price: {sessionOrder.PaymentAmount}</p>");
            }

            sbBody.AppendLine($"<p>Start Time: {sessionOrder.StartTime.ToString("dddd, MMMM dd, yyyy h:mm tt")}</p>");
            //sbBody.AppendLine($"<p>End Time: {sessionOrder.EndTime.Value.ToString("dddd, MMMM dd, yyyy h:mm tt")}</p>");
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
                sbBody.AppendLine("<head>");
                sbBody.AppendLine("<style>");
                sbBody.AppendLine("p { margin: 5px 0; }"); 
                sbBody.AppendLine("</style>");
                sbBody.AppendLine("</head>");
                sbBody.AppendLine("<body style='font-family: Arial, sans-serif;'>");
                sbBody.AppendLine("<p>Thank you for using our photo booth! </p>");
                sbBody.AppendLine("<br>");

                //insert body need to send
                sbBody.AppendLine(body);

                sbBody.AppendLine("<br>");
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
