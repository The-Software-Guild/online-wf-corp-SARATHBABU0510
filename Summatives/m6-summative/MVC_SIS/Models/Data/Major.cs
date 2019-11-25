using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Exercises.Models.Data
{
    public class Major
    {
        public int MajorId { get; set; }
        [Required(ErrorMessage = "You must enter Major Name")]
        public string MajorName { get; set; }
    }
}