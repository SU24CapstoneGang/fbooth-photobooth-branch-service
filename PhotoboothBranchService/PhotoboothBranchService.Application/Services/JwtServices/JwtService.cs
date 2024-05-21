using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Authentication;
using System.Net.Http.Json;

namespace PhotoboothBranchService.Application.Services.JwtServices
{
    public class JwtService : IJwtService
    {
        private readonly HttpClient _httpClient;
        private readonly string API_KEY = JsonHelper.GetFromAppSettings("FirebaseJwt:ApiKey");


        public JwtService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponeModel> GetForCredentialsAsync(string email, string password)
        {
            var request = new
            {
                email,
                password,
                returnSecureToken = true
            };
            var response = await _httpClient.PostAsJsonAsync("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + API_KEY, request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var authToken = await response.Content.ReadFromJsonAsync<AuthenTokenRespone>();
                if (authToken != null)
                {
                    return new LoginResponeModel
                    {
                        TokenId = authToken.IdToken,
                        RefreshToken = authToken.RefreshToken,
                    };
                }
            }
            throw new BadRequestException("Fail for getting credential!!!");
        }

        public async Task<LoginResponeModel> RefreshToken(string refreshToken)
        {
            var request = new
            {
                grant_type = "refresh_token",
                refresh_token = refreshToken
            };
            var response = await _httpClient.PostAsJsonAsync("https://securetoken.googleapis.com/v1/token?key=" + API_KEY, request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var authToken = await response.Content.ReadFromJsonAsync<RefreshTokenResponseModel>();
                if (authToken != null)
                {
                    return new LoginResponeModel
                    {
                        TokenId = authToken.IdToken,
                        RefreshToken = authToken.RefreshToken,
                    };
                }
            }
            throw new BadRequestException("Fail for refresh token!!!");
        }
    }
}

