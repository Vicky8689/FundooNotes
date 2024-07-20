using BusineesLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using ModelLayer.Model;

using Newtonsoft.Json.Linq;
using RepositoryLayer.Entity;
using RepositoryLayer.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    
    [Route("api/Notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL _notesBL;
        private IDistributedCache _cache;
        RedisMethods cacheMethod;
        public NotesController(INotesBL notesBL, IDistributedCache cache)
        {
            _notesBL = notesBL;
            cacheMethod = new RedisMethods(cache);
            _cache = cache;
        }

        

        //GetAllNotes
        [HttpGet]
        [Authorize]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotesController()
        {


            //if (HttpContext.Session.GetString("email") != null)
            //{
                ResponseModel<List<GetAllNotesResponseModel>> response = new ResponseModel<List<GetAllNotesResponseModel>>();
                              
                try
                {
                    var id = User.FindFirstValue("UserID");
                    int userId = Convert.ToInt32(id);
                    //get data from redis
                    var cacheData = _cache.GetString(id);
                    if (cacheData != null)
                    {
                        response.Message = "data from redis Successfully";
                        response.Data = JsonSerializer.Deserialize<List<GetAllNotesResponseModel>>(cacheData);
                    }
                    else
                    {
                        var result = _notesBL.GetAllNotes(userId);
                        if (result != null)
                        {
                            //set data in redis
                            cacheMethod.SetCache(id,result);
                            response.Message = "Successfully";
                            response.Data = result;
                        }
                        else
                        {
                            response.Message = "Unsuccessfully";
                        }
                    }
                }
                catch (Exception ex) { }
                return Ok(response);
            //}
            //else { return Ok("session time out123"); }
        }

        //AddNote
        [HttpPost]
        [Authorize]
        [Route("AddNotes")]
        public IActionResult AddNotesController(AddNotesRequestModel addNotesRequestModel)
        {
            //if (HttpContext.Session.GetString("email") != null)
            //{

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
            //}
            //else { return Ok("session time out134"); }
        }

        //DeleteNote
        [HttpDelete]
        [Authorize]
        [Route("DeleteNote")]
        public IActionResult DeleteNoteByIdController([FromQuery] int noteId)
        {
            //if (HttpContext.Session.GetString("email") != null)
            //{
            Console.WriteLine("this is id:"+noteId + "***************************************************************************************************************");
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
                }
                catch (Exception ex) { }
                return Ok(responseModel);
            //}
            //else { return Ok("Session timeout"); }
        }


        //updateNote
        [HttpPut]
        [Authorize]
        [Route("UpdateNote")]
        public IActionResult  UpdateNoteByIdController([FromQuery] int noteId, [FromQuery] string title, [FromQuery] string description, [FromQuery] string color)
        {
            //if (HttpContext.Session.GetString("email") != null)
            //{


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
                }
                catch (Exception ex) { }
                return Ok(response);
            //}
            //else { return Ok("session timeout"); }
        }

        //update note color
        [HttpPut]
        [Authorize]
        [Route("UpdateColorNote")]
        public IActionResult UpdateNoteColorByIdController(int noteId, string color)
        {
            
            ResponseModel<string> response = new ResponseModel<string>();
            try
            {

                //store Input Data
                //UpdateNoteRequestModel data = new UpdateNoteRequestModel() { title = title, description = description, color = color };
                var getUserId = User.FindFirstValue("UserID");
                int userId = Convert.ToInt32(getUserId);
                var result = _notesBL.UpdateNoteColorById(userId, noteId, color);
                if (result != null)
                {
                  //  UpdateNoteResponseModel updatedNote = new UpdateNoteResponseModel() { color = result.Color };
                    response.Message = "Successful";
                   

                }
                else
                {
                    response.Success = false;
                    response.Message = "Unsuccessful";
                }
            }
                catch (Exception ex) { }
            return Ok(response);
        }
        //else { return Ok("session timeout"); }

        //GetNoteById
        [HttpGet]
        [Authorize]
        [Route("NoteById")]
        public IActionResult GetNotesByIdController([FromQuery] NoteByIdrequestModel requestModel)
        {
            //if (HttpContext.Session.GetString("email") != null)
            //{
                ResponseModel<NotesResponseModel> response = new ResponseModel<NotesResponseModel>();


                try
                {
                    var id = User.FindFirstValue("UserID");
                    int userId = Convert.ToInt32(id);
                    //get cache data

                    var cacheData = _cache.GetString($"{id}:{requestModel.noteId}");
                    if (cacheData != null)
                    {
                        response.Message = " data from redis Successfully";
                        response.Data = JsonSerializer.Deserialize<NotesResponseModel>(cacheData);
                    }
                    else
                    {

                        var result = _notesBL.NotesById(userId, requestModel.noteId);

                        if (result != null)
                        {
                            NotesResponseModel note = new NotesResponseModel() { noteId = result.NoteId, title = result.Title, description = result.Description, color = result.Color };
                            _cache.SetString($"{id}:{requestModel.noteId}",JsonSerializer.Serialize(note));
                            response.Message = "Successfully";
                            response.Data = note;
                        }
                        else
                        {
                            response.Message = "Unsuccessfully";
                        }
                    }
                    
                }
                catch (Exception ex) { }
                return Ok(response);
            //}
            //else { return Ok("session timeout"); }
        }

        //trashNote
        [HttpPost]
        [Authorize]
        [Route("trashNote")]
        public IActionResult TrashNoteController([FromQuery] int noteId)
        {
            //if (HttpContext.Session.GetString("email") != null)
            //{

                var id = User.FindFirstValue("UserID");
                var userId = Convert.ToInt32(id);
                var result = _notesBL.TrashNote(userId, noteId);
                ResponseModel<string> response = new ResponseModel<string>();
                if (result)
                {
                    
                    response.Message = "Trash Successful";
                }
                else
                {
                    response.Success= false;
                    response.Message = "Unsuccesful";
                }
                return Ok(response);
            //}else { return Ok("session timeout"); }
        }

        //UntrashNote
        [HttpPost]
        [Authorize]
        [Route("unTrashNote")]
        public IActionResult UnTrashNoteController([FromQuery] int noteId)
        {
            //if (HttpContext.Session.GetString("email") != null)
            //{
                var id = User.FindFirstValue("UserID");
                var userId = Convert.ToInt32(id);

                var result = _notesBL.UnTrashNote(userId, noteId);
                ResponseModel<string> response = new ResponseModel<string>();
                if (result)
                {
                    response.Message = "UnTrash Successful";
                }
                else
                {
                    response.Message = "Unsuccesful";
                }
                return Ok(response);
            //}
            //else { return Ok("session timeout "); }
        }

        //Archive 
        [HttpPost]
        [Authorize]
        [Route("ArchiveNote")]
        public IActionResult ArchiveNoteController([FromQuery] int noteId)
        {
          
            //if (HttpContext.Session.GetString("email") != null)
            //{
            
                var id = User.FindFirstValue("UserID");
                var userId = Convert.ToInt32(id);
                var result = _notesBL.ArchiveNote(userId, noteId);
                ResponseModel<string> response = new ResponseModel<string>();
                if (result)
                {
                    response.Message = "Archive Successful";
                }
                else
                {
                    response.Message = "Unsuccesful";
                }
                return Ok(response);
            //}
            //else { return Ok("session time out"); }
        }

        //UnArchive 
        [HttpPost]
        [Authorize]
        [Route("UnArchiveNote")]
        public IActionResult UnArchiveNoteController([FromQuery] int noteId)
        {
            //if (HttpContext.Session.GetString("email") != null)
            //{
                var id = User.FindFirstValue("UserID");
                var userId = Convert.ToInt32(id);
                var result = _notesBL.UnArchiveNote(userId, noteId);
                ResponseModel<string> response = new ResponseModel<string>();
                if (result)
                {
                    response.Message = "UnArchive Successful";
                }
                else
                {
                    response.Message = "Unsuccesful";
                }
                return Ok(response);
            //}
            //else { return Ok("session timeout"); }
        }

     
    
    }
}
