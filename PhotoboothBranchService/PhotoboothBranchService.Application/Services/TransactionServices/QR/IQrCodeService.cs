namespace PhotoboothBranchService.Application.Services.TransactionServices.QR
{
    public interface IQrCodeService
    {
        Task<string> GetQrCodeDataAsync(string qrCodePageUrl);
    }
}
