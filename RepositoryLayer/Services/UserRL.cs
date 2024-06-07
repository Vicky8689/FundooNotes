using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Helper;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly FundooNotesContext _Context;
        public UserRL(FundooNotesContext Context)
        {
            _Context = Context;
        }

        public async Task<UserEntity> UserRegistration(RegistrationRequestModel userModel)
        {
            
            var result = new UserEntity();
            var IsPresent = await _Context.Users.FirstOrDefaultAsync(x => x.Email == userModel.Email);
            if (IsPresent == null)
            {
                try
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity.Email = userModel.Email;
                    userEntity.FirstName = userModel.FirstName;
                    userEntity.LastName = userModel.LastName;
                    userEntity.Password = userModel.Password;

                    var dbresult = await _Context.AddAsync(userEntity); //mapin userEntity to Context
                    result = dbresult.Entity;
                    await _Context.SaveChangesAsync();

                }
                catch (Exception ex) { }
                return result;
            }
            else
            {
                return null;
            }

        }



        public async Task<UserEntity> Login(LoginRequestModel loginModel)
        {

            return await _Context.Users.FirstOrDefaultAsync(x => x.Email == loginModel.Email );

        }

        //forgotpassword
        public async Task<UserEntity> ForgotPassword(ForgotPasswordRequestModel requestModel)
        {
            return await _Context.Users.FirstOrDefaultAsync(x => x.Email == requestModel.email);
        }

        //resetpassword

        public async Task<UserEntity> ResetPass(int userId, string hashPass)
        {
            var getUserData = await _Context.Users.FirstOrDefaultAsync(x=>x.UserId == userId);
            if (getUserData != null)
            {
                getUserData.Password = hashPass;
                _Context.Update(getUserData);
                await _Context.SaveChangesAsync();
                return getUserData;
            }
            else
            {
                return null;

            }

            
        }
    }
}
