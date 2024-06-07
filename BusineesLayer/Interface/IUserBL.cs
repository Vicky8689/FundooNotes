using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusineesLayer.Interface
{
    public interface IUserBL
    {
        public Task<bool> UserRegistration(RegistrationRequestModel userModel);
        public Task<string> Login(LoginRequestModel loginModel);

        public Task<string> ForgotPassword(ForgotPasswordRequestModel requestModel);

        public Task<UserEntity> ResetPass(int userId , ResetPassRequestModel passModel);
    }
}
