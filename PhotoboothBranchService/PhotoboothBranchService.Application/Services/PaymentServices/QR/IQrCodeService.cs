namespace PhotoboothBranchService.Application.Services.PaymentServices.QR
{
    public interface IQrCodeService
    {
        Task<string> GetQrCodeDataAsync(string qrCodePageUrl);
    }
}
