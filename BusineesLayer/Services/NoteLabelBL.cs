using BusineesLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusineesLayer.Services
{
    public class NoteLabelBL:INoteLabelBL
    {
        private readonly INoteLabelRL _noteLabelRL;
        public NoteLabelBL(INoteLabelRL noteLabelRL)
        {
            _noteLabelRL = noteLabelRL;

        }
        public bool AddNoteLabel( AddNoteLabelRequestModel notelabelModel)
        {
            return _noteLabelRL.AddNoteLabel( notelabelModel);
        }

        public bool DeleteNoteLabel( int noteId)
        {
            return _noteLabelRL.DeleteNoteLabel( noteId);
        }

    }
}
