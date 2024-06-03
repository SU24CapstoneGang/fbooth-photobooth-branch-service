using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PaymentServices.QR
{
    public interface IQrCodeService
    {
        Task<string> GetQrCodeDataAsync(string qrCodePageUrl);
    }
}
