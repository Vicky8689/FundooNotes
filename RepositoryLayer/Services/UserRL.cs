using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Model;
using NLog;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Helper;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly FundooNotesContext _Context;
       private readonly ILogger<UserRL> _logger;
       Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        public UserRL(FundooNotesContext Context)
        {
            _Context = Context;
        }

        public async Task<UserEntity> UserRegistration(RegistrationRequestModel userModel)
        {
            try
            {
                
                var result = new UserEntity();
                var IsPresent = await _Context.Users.FirstOrDefaultAsync(x => x.Email == userModel.Email);

                if (IsPresent == null)
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity.Email = userModel.Email;
                    userEntity.FirstName = userModel.FirstName;
                    userEntity.LastName = userModel.LastName;
                    userEntity.Password = userModel.Password;

                    var dbresult = await _Context.AddAsync(userEntity); //maping userEntity to Context
                    result = dbresult.Entity;
                    await _Context.SaveChangesAsync();
                   

                    return result;
                }
                else
                {
                    return null;
                }
            }catch (Exception ex) { 
                logger.Error(ex,"Exception Caught");
                return null;
            }
            

        }



        public async Task<UserEntity> Login(LoginRequestModel loginModel)
        {
            try
            {

            return await _Context.Users.FirstOrDefaultAsync(x => x.Email == loginModel.Email );
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return null;
            }

        }

        //forgotpassword
        public async Task<UserEntity> ForgotPassword(ForgotPasswordRequestModel requestModel)
        {
            try
            {

            return await _Context.Users.FirstOrDefaultAsync(x => x.Email == requestModel.email);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return null;
            }
        }

        //resetpassword

        public async Task<UserEntity> ResetPass(int userId, string hashPass)
        {
            try 
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
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return null;
            }


        }
    }
}
