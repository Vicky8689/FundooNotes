using BusineesLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    
    [Route("api/Notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL _notesBL;
        public NotesController(INotesBL notesBL)
        {
            _notesBL = notesBL;
        }
        //GetAllNotes
        [HttpGet]
        [Authorize]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotesController()
        {
            ResponseModel<List<GetAllNotesResponseModel>> response = new ResponseModel<List<GetAllNotesResponseModel>>();
            try
            {
                var id = User.FindFirstValue("UserID");
                int userId = Convert.ToInt32(id);
                var result = _notesBL.GetAllNotes(userId);

                if (result != null)
                {
                    response.Message = "Successfully";
                    response.Data = result;
                }
                else
                {
                    response.Message = "Unsuccessfully";
                }
            }catch(Exception ex) { }
            return Ok(response);
        }

        //AddNote
        [HttpPost]
        [Authorize]
        [Route("AddNotes")]
        public IActionResult AddNotesController(AddNotesRequestModel addNotesRequestModel)
        {
            ResponseModel<AddNotesResponseModel> response = new ResponseModel<AddNotesResponseModel>();
            try
            {
                var id = User.FindFirstValue("UserID");
                var userId = Convert.ToInt32(id);

                var result = _notesBL.AddNote(addNotesRequestModel, userId);
                AddNotesResponseModel responseModel = new AddNotesResponseModel();
                if (result)
                {
                    responseModel.Title = addNotesRequestModel.Title;
                    response.Message = "Note Added successfully";
                    response.Data = responseModel;                  
                }
                else
                {
                response.Success = false;
                response.Message = "Unsuccessfully";
                }              
            }catch(Exception ex) { }
            return Ok(response);
        }

        //DeleteNote
        [HttpDelete]
        [Authorize]
        [Route("DeleteNote")]
        public IActionResult DeleteNoteByIdController([FromQuery] int noteId)
        {
            var getUserId = User.FindFirstValue("UserID");
            int userId = Convert.ToInt32(getUserId);

            var result = _notesBL.DeleteNoteById(userId, noteId);

            ResponseModel<DeleteNoteByIdResponseModel> responseModel = new ResponseModel<DeleteNoteByIdResponseModel>();
            try
            {
                if (result != null)
                {
                    DeleteNoteByIdResponseModel deletedNote = new DeleteNoteByIdResponseModel() { noteId = result.NoteId, title = result.Title, description = result.Description, color = result.Color };
                    responseModel.Message = "Successful";
                    responseModel.Data = deletedNote;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "UnSuccessful";

                }
            }catch (Exception ex) { }
            return Ok(responseModel);
        }


        //updateNote
        [HttpPut]
        [Authorize]
        [Route("UpdateNote")]
        public IActionResult  UpdateNoteByIdController([FromQuery] int noteId, [FromQuery] string title, [FromQuery] string description, [FromQuery] string color)
        {
            ResponseModel<UpdateNoteResponseModel> response = new ResponseModel<UpdateNoteResponseModel>();
            try
            {

                //store Input Data
                UpdateNoteRequestModel data = new UpdateNoteRequestModel() { title = title, description = description, color = color };
                var getUserId = User.FindFirstValue("UserID");
                int userId = Convert.ToInt32(getUserId);
                var result = _notesBL.UpdateNoteById(userId, noteId, data);
                if (result != null)
                {
                    UpdateNoteResponseModel updatedNote = new UpdateNoteResponseModel() { noteId = result.NoteId, title = result.Title, description = result.Description, color = result.Color };
                    response.Message = "Successful";
                    response.Data = updatedNote;

                }
                else
                {
                    response.Success = false;
                    response.Message = "Unsuccessful";
                }
            }catch(Exception ex) { }
            return Ok(response);
        }

        //GetNoteById
        [HttpGet]
        [Authorize]
        [Route("NoteById")]
        public IActionResult GetNotesByIdController(NoteByIdrequestModel requestModel)
        {
            ResponseModel<NotesResponseModel> response = new ResponseModel<NotesResponseModel>();
            
            
            try
            {
                var id = User.FindFirstValue("UserID");
                int userId = Convert.ToInt32(id);
                var result = _notesBL.NotesById(userId,requestModel.noteId);

                if (result != null)
                {
                    NotesResponseModel note = new NotesResponseModel() {noteId =result.NoteId,title=result.Title,description =result.Description,color= result.Color };
                    response.Message = "Successfully";
                    response.Data = note;
                }
                else
                {
                    response.Message = "Unsuccessfully";
                }
            }
            catch (Exception ex) { }
            return Ok(response);
        }



    }
}
