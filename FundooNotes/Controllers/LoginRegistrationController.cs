using BusineesLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Helper;
using System;
using System.Security.Claims;
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
        //Registration
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

                var emailStatus = EmailSender.sendMail(userModel.Email, "You are Registered");
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


        //login
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginController(LoginRequestModel loginModel)
        {
            var result = await _userBL.Login(loginModel);
            ResponseModel<LoginResponseModel> response = new ResponseModel<LoginResponseModel>();
            LoginResponseModel loginResponseModel = new LoginResponseModel();
            if (result!=null)
            {
                
                loginResponseModel.Email = loginModel.Email;
                response.Success = true;
                response.Message = result;
                response.Data = loginResponseModel;
                return Ok(response);
            }
            else
            {
                loginResponseModel.Email = loginModel.Email;
                response.Success = false;
                response.Message = "Invalid Credentials";
                response.Data = loginResponseModel;
                return BadRequest(response);
            }

            

        }

        //forgotPassword
        [Route("forgotpass")]
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordController(ForgotPasswordRequestModel requestModel)
        {

            var result = await _userBL.ForgotPassword(requestModel);
            ResponseModel<string> response = new ResponseModel<string>();
            try
            {
                if (result != null)
                {

                    response.Message = result;
                    response.Data = requestModel.email;
                }
                else
                {
                    response.Success = false;
                    response.Message = "User Not found";
                }
            }catch(Exception ex) { }
            return Ok(response);
            
        }

        //resetPassword
        [Route("resetpass")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ResetPasswordController(ResetPassRequestModel passModel)
        {
            ResponseModel<ResetPassResponseModel> response =new ResponseModel<ResetPassResponseModel>();
            try
            {
            var id = User.FindFirstValue("UserID");
            int userId = Convert.ToInt32(id);
            var result = await _userBL.ResetPass(userId , passModel);
                if (result != null)
                {
                    ResetPassResponseModel responseData = new ResetPassResponseModel() { email = result.Email };
                    response.Message = "Passwors Reset Succesful..";
                    response.Data = responseData;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Unsuccesful";
                }
            }catch(Exception ex) { }

            return Ok(response);




        }
    
    }
}
