using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteLabelRL
    {
        public bool AddNoteLabel(AddNoteLabelRequestModel notelabelModel);
        public bool DeleteNoteLabel( int noteId);

    }
}
