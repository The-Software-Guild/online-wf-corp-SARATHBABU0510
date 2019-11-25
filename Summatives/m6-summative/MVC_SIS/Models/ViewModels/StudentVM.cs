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
    public class StudentVM
    {
        public PersonalDetailsVM PersonalDetailsVM { get; set; }
        public AddressDetailsVM AddressDetailsVM { get; set; }
        
        public StudentVM()
        {
            PersonalDetailsVM = new PersonalDetailsVM();
            AddressDetailsVM = new AddressDetailsVM();
        }        
    }
}