using BusineesLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusineesLayer.Services
{
    public class NotesBL:INotesBL
    {
        private readonly INotesRL _notesRL;
        public NotesBL(INotesRL notesRL)
        {
            _notesRL = notesRL;
        }
        public List<GetAllNotesResponseModel> GetAllNotes(int userId)
        {
            var result = _notesRL.GetAllNotes(userId);
            return result;
        }
        public bool AddNote(AddNotesRequestModel addNotesRequestModel, int userId)
        {
            return _notesRL.AddNote(addNotesRequestModel,userId);

        }

        //deletNoteById
        public NotesEntity DeleteNoteById(int userId, int noteId)
        {
            return _notesRL.DeleteNoteById(userId, noteId);
        }

        //updateNoteByID
        public NotesEntity UpdateNoteById(int userId,int noteId, UpdateNoteRequestModel data)
        {
            return _notesRL.UpdateNoteById(userId, noteId,data );
        }

        //get note by id
        public NotesEntity NotesById(int userId, int noteId)
        {
            return _notesRL.NotesById(userId, noteId);
        }

    }
}
