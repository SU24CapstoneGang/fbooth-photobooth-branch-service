using OpenCvSharp;
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
        private ITransactionRepository _transactionRepository;
        private IBookingRepository _bookingRepository;
        private IBranchRepository _boothBranchRepository;
        private IBookingServiceRepository _bookingServiceRepository;
        private IRefundRepository _refundRepository;

        public EmailService(IAccountRepository accountRepository, 
            ITransactionRepository paymentRepository, 
            IBookingRepository sessionOrderRepository, 
            IBranchRepository boothBranchRepository, 
            IBookingServiceRepository serviceItemRepository,
            IRefundRepository refundRepository)
        {
            this.smtpServerName = JsonHelper.GetFromAppSettings("EmailConfig:SmtpServerName");
            this.smtpPortNumber = JsonHelper.GetFromAppSettings("EmailConfig:SmtpPortNumber");
            this.emailHostAddress = JsonHelper.GetFromAppSettings("EmailConfig:EmailHostAddress");
            this.emailHostPassword = JsonHelper.GetFromAppSettings("EmailConfig:EmailHostPassword");
            _bookingRepository = sessionOrderRepository;
            _accountRepository = accountRepository;
            _transactionRepository = paymentRepository;
            _boothBranchRepository = boothBranchRepository;
            _bookingServiceRepository = serviceItemRepository;
            _refundRepository = refundRepository;
        }

        public async Task SendRefundBillInformation(Guid refundId)
        {
            var refund = (await _refundRepository.GetAsync(i => i.RefundID == refundId)).FirstOrDefault();
            if (refund == null)
            {
                throw new NotFoundException("Not found refund");
            }
            var transaction = (await _transactionRepository.GetAsync(i => i.TransactionID == refund.TransactionID, i=>i.PaymentMethod)).FirstOrDefault();
            if (transaction == null)
            {
                throw new NotFoundException("Not found transaction");
            }
            var booking = (await _bookingRepository.GetAsync(i => i.BookingID == transaction.BookingID)).FirstOrDefault();
            if (booking == null)
            {
                throw new NotFoundException("Not found Order of transaction");
            }
            var user = (await _accountRepository.GetAsync(i => i.AccountID == booking.CustomerID)).FirstOrDefault();
            if (user == null)
            {
                throw new NotFoundException("Not found user");
            }
            string subject = "Refund Information";
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine("<p>Dear Customer,</p>");
            sbBody.AppendLine("<p>We are writing to inform you about the details of your recent refund.</p>");
            sbBody.AppendLine("<hr>");

            sbBody.AppendLine("<h3>Transaction Information</h3>");
            sbBody.AppendLine("<ul>");
            sbBody.AppendLine($"<li><strong>Payment Method:</strong> {transaction.PaymentMethod.PaymentMethodName}</li>");
            sbBody.AppendLine($"<li><strong>Original Transaction Amount:</strong> {transaction.Amount}</li>");
            sbBody.AppendLine($"<li><strong>Transaction Date & Time:</strong> {transaction.TransactionDateTime:dddd, MMMM dd, yyyy h:mm tt}</li>");
            sbBody.AppendLine($"<li><strong>Transaction Code:</strong> {transaction.GatewayTransactionID}</li>");
            sbBody.AppendLine("</ul>");
            sbBody.AppendLine("<hr>");

            sbBody.AppendLine("<h3>Refund Information</h3>");
            sbBody.AppendLine("<ul>");
            sbBody.AppendLine($"<li><strong>Refund Description:</strong> {refund.Description}</li>");
            sbBody.AppendLine($"<li><strong>Refund Amount:</strong> {refund.Amount}</li>");
            sbBody.AppendLine($"<li><strong>Refund Status:</strong> {refund.Status}</li>");
            sbBody.AppendLine($"<li><strong>Refund Message:</strong> {refund.ResponseMessage}</li>");
            sbBody.AppendLine($"<li><strong>Refund Date & Time:</strong> {refund.RefundDateTime:dddd, MMMM dd, yyyy h:mm tt}</li>");
            sbBody.AppendLine("</ul>");
            sbBody.AppendLine("<hr>");

            sbBody.AppendLine("<p>If you have any questions or need further assistance, please do not hesitate to contact our customer support team.</p>");
            sbBody.AppendLine("<p>Thank you for your understanding, and we apologize for any inconvenience this may have caused.</p>");
            await this.SendEmail(user.Email, subject, sbBody.ToString(), $"{user.FirstName}{user.LastName}");
        }

        public async Task SendBookingInformation(Guid bookingID, Guid transactionID)
        {
            var booking = (await _bookingRepository
                .GetAsync(i => i.BookingID == bookingID,
                    includeProperties: new Expression<Func<Booking, object>>[]
                        {
                            i => i.BookingServices,
                            i => i.Booth,
                        }
                ))
                .FirstOrDefault();
            var trans = (await _transactionRepository.GetAsync(i => i.TransactionID == transactionID, i => i.PaymentMethod)).SingleOrDefault();
            if ( trans == null ) {
                throw new NotFoundException("Not found transaction");
            }
            if (booking == null)
            {
                throw new NotFoundException("Not found booking of transaction");
            }
            var user = (await _accountRepository.GetAsync(i => i.AccountID == booking.CustomerID)).FirstOrDefault();
            if (user == null)
            {
                throw new NotFoundException("Not found user");
            }
            var branch = (await _boothBranchRepository.GetAsync(i => i.BranchID == booking.Booth.BranchID)).FirstOrDefault();
            if (branch == null)
            {
                throw new NotFoundException("Not found branch");
            }
            string subject = "Booking information";
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine("<p>Here is your booking's information</p>");
            sbBody.AppendLine("<br>");

            sbBody.AppendLine($"<p><strong>Booking Code:</strong> {booking.CustomerReferenceID}</p>");

            sbBody.AppendLine("<h3>Branch Details</h3>");
            sbBody.AppendLine($"<p><strong>Branch Name:</strong> {branch.BranchName}</p>");
            sbBody.AppendLine($"<p><strong>Branch Address:</strong> {branch.Address}</p>");
            sbBody.AppendLine($"<p><strong>Booth Name:</strong> {booking.Booth.BoothName}</p>");

            if (booking.BookingServices.Count > 0)
            {
                var bookingServices = (await _bookingServiceRepository
                    .GetAsync(i => booking.BookingServices.Select(i => i.BookingServiceID).ToList().Contains(i.BookingServiceID), i => i.Service)
                    ).ToList();
                if (bookingServices != null && bookingServices.Count == booking.BookingServices.Count)
                {
                    sbBody.AppendLine("<p>Service(s) in Booking:</p>");
                    sbBody.AppendLine("<br>");
                    sbBody.AppendLine("<table style='width:100%; border-collapse: collapse;'>");
                    sbBody.AppendLine("<tr>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Service Name</th>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Quantity</th>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Unit</th>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Price</th>");
                    sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Subtotal</th>");
                    sbBody.AppendLine("</tr>");

                    // the booking fee
                    var duration = (booking.EndTime - booking.StartTime).TotalHours;
                    duration = Math.Round(duration, 2);
                    sbBody.AppendLine("<tr>");
                    sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>Hire booth fee</td>");
                    sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{duration:N2}</td>");
                    sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>Hours</td>");
                    sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{booking.Booth.PricePerHour:N0}</td>");
                    sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{booking.HireBoothFee:N0}</td>");
                    sbBody.AppendLine("</tr>");

                    // the rest service
                    foreach (var bookingService in bookingServices)
                    {
                        sbBody.AppendLine("<tr>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{bookingService.Service.ServiceName}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{bookingService.Quantity}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{bookingService.Service.Unit}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{bookingService.Price:N0}</td>");
                        sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{bookingService.SubTotal:N0}</td>");
                        sbBody.AppendLine("</tr>");
                    }

                    sbBody.AppendLine("<tr>");
                    sbBody.AppendLine("<td colspan='3' style='border: 1px solid black; padding: 8px; text-align: right;'><strong>Total Price:</strong></td>");
                    sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'><strong>{booking.PaymentAmount:N0}</strong> VND</td>");
                    sbBody.AppendLine("</tr>");

                    sbBody.AppendLine("</table>");
                } else
                {
                    throw new Exception("Service not found in Item list");
                }
            }
            else{
                sbBody.AppendLine($"<p>Total price: {booking.PaymentAmount}</p>");
            }

            sbBody.AppendLine($"<p>This booking was paid thourgh {trans.PaymentMethod.PaymentMethodName} in {trans.TransactionDateTime.ToString("dddd, MMMM dd, yyyy h:mm tt")}</p>");


            sbBody.AppendLine($"<p><strong>Start Time</strong>: {booking.StartTime.ToString("dddd, MMMM dd, yyyy h:mm tt")} (UTC +7)</p>");
            sbBody.AppendLine($"<p><strong>End Time: </strong>{booking.EndTime.ToString("dddd, MMMM dd, yyyy h:mm tt")} (UTC +7)</p>");
            sbBody.AppendLine($"<p>Validate code (Enter this code to booth):<strong style='font-size: 1.2em; color: blue; font-weight: bold;'> {booking.ValidateCode}</strong> </p>");
            sbBody.AppendLine($"<p>We hope you arrive 5 minutes before the start time to receive instructions from the staff.</p>");
            await this.SendEmail(user.Email, subject, sbBody.ToString(), $"{user.FirstName}{user.LastName}");
        }

        public async Task SendCancelBookingInformation(Guid bookingID)
        {
            var booking = (await _bookingRepository
                .GetAsync(i => i.BookingID == bookingID,
                    includeProperties: new Expression<Func<Booking, object>>[]
                        {
                            i => i.Account,
                        }
                ))
                .FirstOrDefault();
            if (booking == null)
            {
                throw new NotFoundException("Not found booking of transaction");
            }

            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine($"<p>We regret to inform you that your booking with the reference number <strong>{booking.CustomerReferenceID}</strong> has been successfully canceled.</p>");
            sbBody.AppendLine("<p>If you have any questions or need further assistance, please feel free to contact our customer support team.</p>");
            sbBody.AppendLine("<br>");
            sbBody.AppendLine("<p>We apologize for any inconvenience caused and thank you for your understanding.</p>");
            sbBody.AppendLine("<br>");
            await this.SendEmail(booking.Account.Email, "Booking Cancellation Confirmation", sbBody.ToString(), $"{booking.Account.FirstName} {booking.Account.LastName}");
        }

        private async Task SendEmail(string emailClientAddress, string subject, string body, string customerName)
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
                //css
                sbBody.AppendLine("<style type=\"text/css\">");
                sbBody.AppendLine("body { font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f5f5f5; }");
                sbBody.AppendLine(".container { width: 100%; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; border: 1px solid #dddddd; border-radius: 10px; }");
                sbBody.AppendLine(".header { text-align: center; padding: 20px; background-color: #379eff; border-top-left-radius: 10px; border-top-right-radius: 10px; }");
                sbBody.AppendLine(".header img { width: 100px; height: auto; }");
                sbBody.AppendLine(".content { padding: 20px; text-align: center; }");
                sbBody.AppendLine(".content h1 { font-size: 24px; color: #333333; }");
                sbBody.AppendLine(".content p { font-size: 16px; color: #00366a; margin: 5px 0;}");
                sbBody.AppendLine(".footer { padding: 20px; text-align: center; font-size: 12px; color: #999999; background-color: #f5f5f5; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px; }");
                sbBody.AppendLine("</style>");
                sbBody.AppendLine("</head>");


                sbBody.AppendLine("<body>");
                sbBody.AppendLine("<div class=\"container\">");

                // Header with logo
                sbBody.AppendLine("<div class=\"header\">");
                sbBody.AppendLine("<img src=\"https://res.cloudinary.com/dfxvccyje/image/upload/v1721217361/Logo/FboothLogo.png\" alt=\"FBooth Logo\">");
                sbBody.AppendLine("</div>");
                // Greeting message
                sbBody.AppendLine("<div class=\"content\">");
                sbBody.AppendLine($"<h1>Hi, {customerName}</h1>");
                sbBody.AppendLine("<p>Thank you for using our photo booth.</p>");

                //insert body need to send
                sbBody.AppendLine(body);

                sbBody.AppendLine("<br>");
                sbBody.AppendLine("<p>Best regards,</p>");
                sbBody.AppendLine("<p>FBooth Team</p>");
                sbBody.AppendLine("</div>"); //close div content

                // Footer section
                sbBody.AppendLine("<div class=\"footer\">");
                sbBody.AppendLine("<p>&copy; 2023 FBooth. All rights reserved.</p>");
                sbBody.AppendLine("</div>");

                sbBody.AppendLine("</div>"); //close div container

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
