using FirebaseAdmin.Auth;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.FirebaseServices
{
    public class FirebaseService : IFirebaseService
    {
        private readonly IAccountRepository _accountRepository;

        public FirebaseService(IAccountRepository accountRepository)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            string credential_path = path + "firebase.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);

            _accountRepository = accountRepository;
        }

        public async Task<string> RegisterAsync(string email, string password)
        {
            var userArgs = new UserRecordArgs
            {
                Email = email,
                Password = password
            };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

            return userRecord.Uid;
        }


        public async Task<string> GetResetPasswordLink(string email)
        {
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
            var account = (await _accountRepository.GetAsync(a => a.Email.Equals(email))).FirstOrDefault();
            if (userRecord == null || account == null)
            {
                throw new NotFoundException("Account", email, "Email not found");
            }

            var resetToken = Guid.NewGuid().ToString();
            account.ResetPasswordToken = resetToken;
            await _accountRepository.UpdateAsync(account);

            var link = await FirebaseAuth.DefaultInstance.GeneratePasswordResetLinkAsync(email);
            // Append the token to the reset link
            var resetLinkWithToken = $"{link}&resetToken={resetToken}";

            return resetLinkWithToken;
        }

        public async Task DeleteUserAsync(string email)
        {
            {
                var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);

                if (userRecord != null)
                {
                    await FirebaseAuth.DefaultInstance.DeleteUserAsync(userRecord.Uid);
                }
            }
        }

        public async Task UpdatePasswordOnFirebase(string email, string newPassword)
        {
            try
            {
                // Lấy thông tin người dùng từ Firebase bằng email
                var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);

                // Cập nhật mật khẩu mới cho người dùng trên Firebase
                await FirebaseAuth.DefaultInstance.UpdateUserAsync(new UserRecordArgs
                {
                    Uid = userRecord.Uid,
                    Password = newPassword
                });
            }
            catch (FirebaseAuthException ex)
            {
                // Xử lý lỗi từ Firebase
                throw new Exception($"Error updating password on Firebase: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                throw new Exception($"An error occurred while updating password on Firebase: {ex.Message}");
            }
        }
    }
}
