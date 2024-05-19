using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.FirebaseServices
{
    public class FirebaseService : IFirebaseService
    {
        public FirebaseService()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            string credential_path = path + "firebase.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
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
            var link = await FirebaseAuth.DefaultInstance.GeneratePasswordResetLinkAsync(email);
            return link;
        }
    }
}
