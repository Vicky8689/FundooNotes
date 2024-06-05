using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class DeleteNoteByIdResponseModel
    {
        public int noteId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string color { get; set; }
    }
}
