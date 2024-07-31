using HtmlAgilityPack;

namespace PhotoboothBranchService.Application.Services.TransactionServices.QR
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public QrCodeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetQrCodeDataAsync(string qrCodePageUrl)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync(qrCodePageUrl);
                response.EnsureSuccessStatusCode();

                var htmlContent = await response.Content.ReadAsStringAsync();
                var qrCodeData = ExtractQrCodeDataFromHtml(htmlContent);

                return qrCodeData;
            }
        }

        private string ExtractQrCodeDataFromHtml(string htmlContent)
        {
            // Load the HTML content into an HtmlDocument
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            // Example: Assuming QR code is in an <img> tag with id="qrCodeImg"
            var imgNode = doc.DocumentNode.SelectSingleNode("//img[@class='qrcodeimg-modal']");
            if (imgNode != null)
            {
                var srcAttribute = imgNode.GetAttributeValue("src", null);
                if (srcAttribute != null)
                {
                    if (srcAttribute.StartsWith("data:image")) // Base64 encoded image
                    {
                        return srcAttribute;
                    }
                    else // URL to the image
                    {
                        // Handle relative URLs
                        return new Uri(new Uri("https://example.com"), srcAttribute).ToString();
                    }
                }
            }

            return null;
        }
    }
}

