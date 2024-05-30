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
    }
}
