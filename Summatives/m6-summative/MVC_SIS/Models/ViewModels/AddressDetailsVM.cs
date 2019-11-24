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
    public class AddressDetailsVM
    {
        public int StudentId { get; set; }
        public Address Address { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        
        public AddressDetailsVM()
        {
            StateItems = new List<SelectListItem>();
        }

        public void SetStateItems(IEnumerable<State> states)
        {
            foreach (var state in states)
            {
                StateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }
    }
}