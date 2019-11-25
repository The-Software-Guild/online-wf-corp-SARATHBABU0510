using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Exercises.Models.Data
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Please enter Street1")]
        public string Street1 { get; set; }
        public string Street2 { get; set; }

        [Required(ErrorMessage = "Please enter City")]
        public string City { get; set; }

        public State State { get; set; }

        [Required(ErrorMessage = "Please enter PostalCode")]
        public string PostalCode { get; set; }
    }
}