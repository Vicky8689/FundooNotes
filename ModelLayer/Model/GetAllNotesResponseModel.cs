using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
    public class GetAllNotesResponseModel
    {
        public int noteId { get; set; }       
        public string title { get; set; }       
        public string description { get; set; } 
        public string color { get; set; }

        public bool isArchive { get; set; } 

        public bool isTrash { get; set; } 
    }
}
