using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
    public class AddNotesRequestModel
    {
       
        [StringLength(50)]
        public string Title { get; set; }
        
        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

    }
}
