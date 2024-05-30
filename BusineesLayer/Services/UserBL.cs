using BusineesLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
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
        public UserBL(IUserRL userRL)
        {
            _userRL = userRL;
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
    }
}
