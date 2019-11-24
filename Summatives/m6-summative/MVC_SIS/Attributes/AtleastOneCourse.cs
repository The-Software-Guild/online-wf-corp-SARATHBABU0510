using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Exercises.Models.Data;

namespace Exercises.Attributes
{
    public class AtleastOneCourse : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is List<int>)
            {
                List<int> courseIds = (List<int>)value;
                if(courseIds.Any())
                {
                    return true;
                }                
            }
            return false;
        }
    }
}