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

        private readonly IAccountRepository _accountRepository;
        private readonly IPaymentRepository _transactionRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBranchRepository _boothBranchRepository;
        private readonly IBookingServiceRepository _bookingServiceRepository;
        private readonly IRefundRepository _refundRepository;
        private readonly IBookingSlotRepository _bookingSlotRepository;

        public EmailService(IAccountRepository accountRepository,
            IPaymentRepository paymentRepository,
            IBookingRepository sessionOrderRepository,
            IBranchRepository boothBranchRepository,
            IBookingServiceRepository serviceItemRepository,
            IRefundRepository refundRepository, IBookingSlotRepository bookingSlotRepository)
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
            _bookingSlotRepository = bookingSlotRepository;
        }

        public async Task SendRefundBillInformation(Guid refundId)
        {
            var refund = (await _refundRepository.GetAsync(i => i.RefundID == refundId)).FirstOrDefault();
            if (refund == null)
            {
                throw new NotFoundException("Not found refund");
            }
            var transaction = (await _transactionRepository.GetAsync(i => i.PaymentID == refund.PaymentID, i => i.PaymentMethod)).FirstOrDefault();
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
            sbBody.AppendLine($"<li><strong>Transaction Date & Time:</strong> {transaction.PaymentDateTime:dddd, MMMM dd, yyyy h:mm tt}</li>");
            sbBody.AppendLine($"<li><strong>Transaction Code:</strong> {transaction.TransactionID}</li>");
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
                            i => i.Booth,
                        }
                ))
                .FirstOrDefault();
            var trans = (await _transactionRepository.GetAsync(i => i.PaymentID == transactionID, i => i.PaymentMethod)).SingleOrDefault();
            if (trans == null)
            {
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

            sbBody.AppendLine($"<p><strong>Booking Code:</strong> {booking.CustomerBusinessID}</p>");

            sbBody.AppendLine("<h3>Branch Details</h3>");
            sbBody.AppendLine($"<p><strong>Branch Name:</strong> {branch.BranchName}</p>");
            sbBody.AppendLine($"<p><strong>Branch Address:</strong> {branch.Address}</p>");
            sbBody.AppendLine($"<p><strong>Booth Name:</strong> {booking.Booth.BoothName}</p>");

            var slots = (await _bookingSlotRepository.GetAsync(i => i.BookingID == booking.BookingID, i => i.Slot)).OrderBy(i => i.Slot.SlotStartTime).ToList();
            sbBody.AppendLine("<p>Slot(s) in Booking:</p>");
            sbBody.AppendLine("<br>");
            sbBody.AppendLine("<table style='width:100%; border-collapse: collapse;'>");
            sbBody.AppendLine("<tr>");
            sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Time</th>");
            sbBody.AppendLine("<th style='border: 1px solid black; padding: 8px;'>Price</th>");
            sbBody.AppendLine("</tr>");
            // the booking fee
            foreach (var slot in slots)
            {
                sbBody.AppendLine("<tr>");
                sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>Slot {slot.Slot.SlotStartTime.ToString(@"hh\:mm")} - {slot.Slot.SlotEndTime.ToString(@"hh\:mm")} </td>");
                sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'>{slot.Price:N0}</td>");
                sbBody.AppendLine("</tr>");
            }
            sbBody.AppendLine("<tr>");
            sbBody.AppendLine("<td colspan='1' style='border: 1px solid black; padding: 8px; text-align: right;'><strong>Total:</strong></td>");
            sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'><strong>{booking.HireBoothFee:N0}</strong> VND</td>");
            sbBody.AppendLine("</tr>");
            sbBody.AppendLine("</table>");


            var bookingServices = (await _bookingServiceRepository
                .GetAsync(i => i.BookingID == booking.BookingID, i => i.Service)
                ).ToList();
            if (bookingServices.Any())
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
                sbBody.AppendLine("<td colspan='4' style='border: 1px solid black; padding: 8px; text-align: right;'><strong>Total:</strong></td>");
                sbBody.AppendLine($"<td style='border: 1px solid black; padding: 8px;'><strong>{(booking.TotalPrice - booking.HireBoothFee):N0}</strong> VND</td>");
                sbBody.AppendLine("</tr>");

                sbBody.AppendLine("</table>");
            }
            // Notify total price
            sbBody.AppendLine("<br>");
            sbBody.AppendLine($"<p><strong>Total Price:</strong> This booking's total cost is <strong>{booking.TotalPrice:N0} VND</strong>.</p>");

            sbBody.AppendLine($"<p>This booking was paid thourgh {trans.PaymentMethod.PaymentMethodName} in {trans.PaymentDateTime.ToString("dddd, MMMM dd, yyyy h:mm tt")}</p>");

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
            sbBody.AppendLine($"<p>We regret to inform you that your booking with the reference number <strong>{booking.CustomerBusinessID}</strong> has been successfully canceled.</p>");
            sbBody.AppendLine("<p>If you have any questions or need further assistance, please feel free to contact our customer support team.</p>");
            sbBody.AppendLine("<br>");
            sbBody.AppendLine("<p>We apologize for any inconvenience caused and thank you for your understanding.</p>");
            sbBody.AppendLine("<br>");
            await this.SendEmail(booking.Account.Email, "Booking Cancellation Confirmation", sbBody.ToString(), $"{booking.Account.FirstName} {booking.Account.LastName}");
        }
        public async Task SendAutoRegistEmailNoti(string email, string link, string customerName)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine($"<p>We are pleased to inform you that an account has been automatically created for you with the email address <strong>{email}</strong> while booking with our staff.</p>");
            sbBody.AppendLine("<p>This account has been set up to streamline your future bookings and enhance your experience with us. With this account, you can easily manage your bookings, view your history, and access exclusive features.</p>");
            sbBody.AppendLine($"<p>To complete the setup, we encourage you to <a href=\"{link}\">activate your account</a> by setting a password. This will ensure that your account is secure and accessible only to you.</p>");
            sbBody.AppendLine("<br>");
            sbBody.AppendLine("<p>If you have any questions or need assistance with your account, please do not hesitate to contact our customer support team.</p>");
            sbBody.AppendLine("<p>We look forward to serving you and hope you enjoy the convenience of your new account.</p>");
            sbBody.AppendLine("<br>");
            await this.SendEmail(email, "Account auto register notification", sbBody.ToString(), customerName);
        }
        public async Task SendResetPasswordEmail(string email, string resetLink, string customerName)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine("<p>We received a request to reset your password for your account associated with this email address.</p>");
            sbBody.AppendLine("<p>If you made this request, please click the link below to reset your password:</p>");
            sbBody.AppendLine($"<p><a href=\"{resetLink}\">Reset your password</a></p>");
            sbBody.AppendLine("<p>If you did not request a password reset, you can safely ignore this email. Your account will remain secure.</p>");
            sbBody.AppendLine("<br>");
            sbBody.AppendLine("<p>If you have any questions or need further assistance, please feel free to contact our support team.</p>");
            sbBody.AppendLine("<br>");
            await this.SendEmail(email, "Password Reset Request", sbBody.ToString(), customerName);
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
                sbBody.AppendLine($"<h1>Dear, {customerName}</h1>");
                sbBody.AppendLine("<p>Thank you for using our services.</p>");

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
