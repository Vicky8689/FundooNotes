using BusineesLayer.Interface;
using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Helper;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusineesLayer.Services
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL _userRL;
        private readonly IConfiguration _configuration;
        public UserBL(IUserRL userRL,IConfiguration configuration)
        {
            _userRL = userRL;
            _configuration = configuration;
        }
       
        public async Task<bool> UserRegistration(RegistrationRequestModel userModel)
        {
            //Hash the user password
            userModel.Password = HashPasswordBL.HashPsaaword(userModel.Password);

            var result = await _userRL.UserRegistration(userModel);
            if (result != null)
            {
                return true;
            }

            return false;

        }


        public async Task<string> Login(LoginRequestModel loginModel)
        {
           var result = await _userRL.Login(loginModel);
            if (result != null)
            {
                bool verifyUser = HashPasswordBL.VerifyHash(loginModel.Password, result.Password);               
                if (verifyUser)
                {

                return TokenGenerateRL.GenerateTokenRL(result);
                }
              //  return verifyUser;
            }
            return null;
        }


        public async Task<string> ForgotPassword(ForgotPasswordRequestModel requestModel)
        {
            var result =await _userRL.ForgotPassword(requestModel);
            if (result != null)
            {
                var genratedToken = TokenGenerateRL.GenerateTokenRL(result);
                //send email
               var emailStatus =  EmailSender.sendMail(requestModel.email, genratedToken);

                if (emailStatus)
                {
                    return $"Mail Send succesful on {requestModel.email} ";
                }
                else
                {
                    return "Email Not send";
                }
            }
            else
            {
                return null;
            }
        }



        public async Task<UserEntity> ResetPass(int userId ,ResetPassRequestModel passModel)
        {
            var hashPass = HashPasswordBL.HashPsaaword(passModel.pass);
            return await _userRL.ResetPass(userId, hashPass);
        }

    }
}
