using BusineesLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using RepositoryLayer.Entity;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("api/lables")]
    public class LableController : Controller
    {
        private readonly ILableBL _lableBL;
        public LableController(ILableBL lableBL)
        {
            _lableBL = lableBL;
        }

        [Authorize]
        [HttpPost]
        [Route("createlable")]
        public IActionResult CreateLableController(CreateLabelRequestModel labelModel)
        {
            var id = User.FindFirstValue("UserID");
            int userId = Convert.ToInt32(id);
            var result =  _lableBL.CreateLable(userId , labelModel);
            ResponseModel<LabelResponseModel> response = new ResponseModel<LabelResponseModel>();
            if (result != null)
            {
                LabelResponseModel model = new LabelResponseModel();
                model.lName = result.LName;
                response.Data = model;
                response.Message = "Successful";
            }
            else
            {
                response.Success = false;
                response.Message = "Unsuccessful";
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("getLabelByID")]
        public IActionResult getLabelByIDController([FromQuery] GetLabelByIdRequestModel lableModel)
        {
            var id = User.FindFirstValue("UserID");
            int userId = Convert.ToInt32(id);

            var result =  _lableBL.GetLablById(userId, lableModel.labelId);
            ResponseModel<LabelResponseModel> response = new ResponseModel<LabelResponseModel>();
            if (result!= null)
            {
                LabelResponseModel model = new LabelResponseModel() { lName=result.LName };
                response.Message = "Successful";
                response.Data = model;
            }
            else
            {
                response.Success = false;
                response.Message = "UnSuccessful";
            }
            return Ok(response);

        }

        [Authorize]
        [HttpGet]
        [Route("getAllLabel")]
        public IActionResult getAllLabelController()
        {
            var id = User.FindFirstValue("UserID");
            int userId = Convert.ToInt32(id);

            var result  = _lableBL.getAllLabelID(userId);
            ResponseModel<List<GetAllLabelResponseModel>> response = new ResponseModel<List<GetAllLabelResponseModel>>();

            if (result!=null)
            {
                response.Message = "Successful";
                response.Data= result;
            }
            else
            {
                response.Success = false;
                response.Message = "UnSuccessful";
            }
            return Ok(response);


        }

        [Authorize]
        [HttpPut]
        [Route("updateLabel")]
        public IActionResult UpdateLabelController(UpdateLabelRequestModel labelModel)
        {
            var id = User.FindFirstValue("UserID");
            int userId = Convert.ToInt32(id);

            var result = _lableBL.UpdateLabelById(userId, labelModel);
            ResponseModel<LabelResponseModel> response = new ResponseModel<LabelResponseModel>();
            if (result != null)
            {
                LabelResponseModel model = new LabelResponseModel() { lName = result.LName };
                response.Message = "Successful";
            }
            else
            {
                response.Success = false;
                response.Message = "UnSuccessful";
            }
            return Ok(response);


        }

        [Authorize]
        [HttpPut]
        [Route("deleteLabel")]
        public IActionResult DeleteLabelController([FromQuery] int labelId)
        {
            var id = User.FindFirstValue("UserID");
            int userId = Convert.ToInt32(id);
            var result =  _lableBL.DeleteLabelById(userId,labelId);
            ResponseModel<bool> response = new ResponseModel<bool>();
            if (result)
            {
                response.Message = "Successful";
                
            }
            else
            {
                response.Success = false;
                response.Message = "UnSuccessful";
            }
            return Ok(response);

        }

    }

}
