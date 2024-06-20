using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusineesLayer.Interface
{
    public interface INoteLabelBL
    {
        public bool AddNoteLabel( AddNoteLabelRequestModel notelabelModel);
        public bool DeleteNoteLabel( int noteId);
    }
}
