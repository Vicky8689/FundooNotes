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
    public class NoteLabelRL:INoteLabelRL
    {
        private readonly FundooNotesContext _context;
        public NoteLabelRL(FundooNotesContext context)
        {
            _context = context;
        }
        public bool AddNoteLabel( AddNoteLabelRequestModel notelabelModel)
        {
            NoteLableEntity noteLabledata = new NoteLableEntity();
            noteLabledata.LabelID=notelabelModel.LabelId;
            noteLabledata.NoteId = notelabelModel.NoteID;
            
            _context.NoteLable.Add(noteLabledata);
            var status = _context.SaveChanges();
            if (status > 0)
            {
                return true;
            }
            return false;
           
        }

        public bool DeleteNoteLabel( int noteId)
        {
            var getNoteLabel = _context.NoteLable.FirstOrDefault(x=>x.NoteId == noteId);
          
            _context.NoteLable.Remove(getNoteLabel);
            var status = _context.SaveChanges(); 
            if(status>0)
            {
                return true;

            }
            return false;
        }

    }
}
