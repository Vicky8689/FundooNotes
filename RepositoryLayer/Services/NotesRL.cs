using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
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
        public NotesRL(FundooNotesContext context)
        {
            _context = context;
        }

        //GetAllNotes
        public List<GetAllNotesResponseModel> GetAllNotes(int userId)
        {
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

        //AddNote
        public bool AddNote(AddNotesRequestModel addNotesRequestModel, int userId)
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
                return true;
            }

            return false;
        }



        //DeleteNoteById
        public NotesEntity DeleteNoteById(int userId, int noteId)
        {
            var findEntitynote = _context.Notes.FirstOrDefault(x=>x.NoteId == noteId && x.UserId==userId);
            if(findEntitynote != null)
            {
                var rm = _context.Notes.Remove(findEntitynote);
                _context.SaveChanges();
                return findEntitynote;
            }
            else
            {
                return null;
            }
        }

        //UpdateNoteById
        public NotesEntity UpdateNoteById(int userId, int noteId, UpdateNoteRequestModel data)
        {
            var note = _context.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
            if(note != null)
            {
                note.Title = data.title;
                note.Description=data.description;
                note.Color= data.color;
                _context.Update(note);
                _context.SaveChanges();
            return note;
            }
            return null;

        }

        public NotesEntity NotesById(int userId, int noteId)
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
    }
}
