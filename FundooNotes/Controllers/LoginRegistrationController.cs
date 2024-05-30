using BusineesLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("fundoonotes")]
    public class LoginRegistrationController : Controller
    {
        private readonly IUserBL _userBL;
        public LoginRegistrationController(IUserBL userBL)
        {
            _userBL = userBL;           
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> RegistrationController(RegistrationRequestModel userModel)
        {
            var result = await _userBL.UserRegistration(userModel);
            ResponseModel<RegistrationResponseModel> responseModel = new ResponseModel<RegistrationResponseModel>();
            if (result)
            {
                RegistrationResponseModel registrationResponseModel = new RegistrationResponseModel();
                registrationResponseModel.FirstName = userModel.FirstName;
                registrationResponseModel.LastName = userModel.LastName;
                registrationResponseModel.Email = userModel.Email;


                responseModel.Message = "You are Registerd";
                responseModel.Data = registrationResponseModel;
                
                return Ok(responseModel);
            }
            else
            {
                responseModel.Success = false;
                responseModel.Message = "User Already Existes";
                return Conflict(responseModel);
            }


          
           
        }
    }
}
