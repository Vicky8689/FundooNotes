using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using System.Security.Claims;
using System;
using BusineesLayer.Interface;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("api/noteLabels")]
    public class NoteLabelController : Controller
    {
        private readonly INoteLabelBL _noteLabelBL;
        public NoteLabelController(INoteLabelBL noteLabelBL)
        {
            _noteLabelBL = noteLabelBL;
        }
        

        [Authorize]
        [HttpPost]
        [Route("addNoteLable")]
        public IActionResult AddNoteLabelController(AddNoteLabelRequestModel notelabelModel)
        {
            var id = User.FindFirstValue("UserID");
            int userId = Convert.ToInt32(id);

            var result =_noteLabelBL.AddNoteLabel( notelabelModel);
            ResponseModel<bool> response = new ResponseModel<bool>();
            if (result)
            {
                response.Message = "successful";
                response.Data = true;
            }
            else
            {
                response.Success =false;
                response.Message = "Unsuccessful";
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteNoteLable")]
        public IActionResult DeleteNoteLabelController(DeleteNoteLabelRequestModel notelabelModel)
        {
            var result = _noteLabelBL.DeleteNoteLabel(notelabelModel.NoteID);
            ResponseModel<bool> response = new ResponseModel<bool>();
            if (result)
            {
                response.Message = "successful";
                response.Data = true;

            }
            else
            {
                response.Success = false;
                response.Message = "Unsuccessful";
            }
            return Ok(response);
        }
    }
}
