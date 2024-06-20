using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
    public class CreateLabelRequestModel
    {
        [StringLength(30)]
        public string LName { get; set; }

       
    }
}
