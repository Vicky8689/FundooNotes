using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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
    }
}
