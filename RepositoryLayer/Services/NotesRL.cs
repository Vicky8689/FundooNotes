using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.Model;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Helper;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL: INotesRL
    {
        private readonly FundooNotesContext _context;
        private IDistributedCache _cache;
        RedisMethods cacheMethod;
        Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        public NotesRL(FundooNotesContext context , IDistributedCache cache)
        {
            _context = context;
            cacheMethod = new RedisMethods(cache);
            _cache = cache;
           
        }

        //GetAllNotes
        public List<GetAllNotesResponseModel> GetAllNotes(int userId)
        {
            try { 
            var data = _context.Notes.Where(x => x.UserId == userId).ToList();
            List<GetAllNotesResponseModel> allNotes = new List<GetAllNotesResponseModel>();
            

            foreach(var item in data)
            {
                GetAllNotesResponseModel note = new GetAllNotesResponseModel();
                note.noteId = item.NoteId;
                note.title = item.Title;
                note.description = item.Description;
                note.color = item.Color;
                allNotes.Add(note);
            }

            return allNotes;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return null;
            }
        }

        //AddNote
        public bool AddNote(AddNotesRequestModel addNotesRequestModel, int userId)
        {
            try
            {
            
                NotesEntity notes = new NotesEntity();
                notes.Title= addNotesRequestModel.Title;
                notes.Description= addNotesRequestModel.Description;
                notes.Color= addNotesRequestModel.Color;
                notes.UserId= userId;
                _context.Add(notes);
                var result =  _context.SaveChanges();

                if (result > 0)
                {
                    //remove redis data
                    var cacheData = _cache.GetString(Convert.ToString(userId));
                    if (cacheData != null)
                    {
                        cacheMethod.RemoveCache(Convert.ToString(userId));
                    }

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return false;
                
            }
        }



        //DeleteNoteById
        public NotesEntity DeleteNoteById(int userId, int noteId)
        {
            try 
            { 

                var findEntitynote = _context.Notes.FirstOrDefault(x=>x.NoteId == noteId && x.UserId==userId);
                if(findEntitynote != null)
                {
                    var rm = _context.Notes.Remove(findEntitynote);
                    _context.SaveChanges();
                    //remove redis data
                    var cacheData = _cache.GetString(Convert.ToString(userId));
                    if (cacheData != null)
                    {
                        cacheMethod.RemoveCache(Convert.ToString(userId));
                    }

                    return findEntitynote;
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

        //UpdateNoteById
        public NotesEntity UpdateNoteById(int userId, int noteId, UpdateNoteRequestModel data)
        {
            try 
            { 
                var note = _context.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if(note != null)
                {
                    note.Title = data.title;
                    note.Description=data.description;
                    note.Color= data.color;
                    _context.Update(note);
                    _context.SaveChanges();
                    //remove redis data
                    var cacheData = _cache.GetString(Convert.ToString(userId));
                    if (cacheData != null)
                    {
                        cacheMethod.RemoveCache(Convert.ToString(userId));
                    }

                    return note;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return null;
            }
        }

        public NotesEntity NotesById(int userId, int noteId)
        {
            try
            {
                

                var getNote = _context.Notes.FirstOrDefault(x=>x.UserId==userId && x.NoteId==noteId);
                if(getNote != null)
                {
                    return getNote;
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


        public bool TrashNote(int userId, int noteId)
        {
            try
            { 
                var getNoterData=_context.Notes.FirstOrDefault(x=>x.UserId== userId && x.NoteId==noteId);
                if (getNoterData != null)
                {
                    getNoterData.IsTrash = true;
                    _context.Update(getNoterData);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return false;
            }
        }


        public bool UnTrashNote(int userId, int noteId)
        {
            try
            { 
                var getNoterData = _context.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if (getNoterData != null)
                {
                    getNoterData.IsTrash = false;
                    _context.Update(getNoterData);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return false;
            }
        }

        public bool ArchiveNote(int userId, int noteId)
        {
            try
            { 
                var getNoterData = _context.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if (getNoterData != null)
                {
                    getNoterData.IsArchive = true;
                    _context.Update(getNoterData);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return false;
            }
        }

        public bool UnArchiveNote(int userId, int noteId)
        {
            try
            { 
                var getNoterData = _context.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if (getNoterData != null)
                {
                    getNoterData.IsArchive = false;
                    _context.Update(getNoterData);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception Caught");
                return false;
            }
        }


    }
}
