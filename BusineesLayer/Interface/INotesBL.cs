using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusineesLayer.Interface
{
    public interface INotesBL
    {
        public List<GetAllNotesResponseModel> GetAllNotes(int userId);
        public bool AddNote(AddNotesRequestModel addNotesRequestModel,int userId);

        public NotesEntity DeleteNoteById(int userId,int noteId);

        public NotesEntity UpdateNoteById(int userId,int noteId, UpdateNoteRequestModel data);
    }
}
