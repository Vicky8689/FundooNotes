using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public Task<UserEntity> UserRegistration(RegistrationRequestModel userModel);
        public Task<UserEntity> Login(LoginRequestModel userModel);

        public Task<UserEntity> ForgotPassword(ForgotPasswordRequestModel requestModel);

        public Task<UserEntity> ResetPass(int userId ,string hashPass);
    }
}
