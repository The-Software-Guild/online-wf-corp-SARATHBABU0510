using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Exercises.Models.Data;
using System.ComponentModel.DataAnnotations;
using Exercises.Attributes;

namespace Exercises.Models.ViewModels
{
    public class StateVM
    {
        [Required(ErrorMessage = "You must select a state")]
        public string StateAbbreviation { get; set; }
        [Required(ErrorMessage = "You must enter State Name")]
        public string StateName { get; set; }
    }
}