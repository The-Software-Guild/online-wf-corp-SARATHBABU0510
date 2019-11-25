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
    public class PersonalDetailsVM
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please enter your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your GPA")]
        public decimal? GPA { get; set; }
        [Required(ErrorMessage = "You must select Major")]
        public int MajorId { get; set; }        
        public List<Course> Courses { get; set; }
        public List<SelectListItem> CourseItems { get; set; }
        public List<SelectListItem> MajorItems { get; set; }
        [AtleastOneCourse(ErrorMessage = "You must choose a Course")]
        public List<int> SelectedCourseIds { get; set; }

        public PersonalDetailsVM()
        {
            CourseItems = new List<SelectListItem>();
            MajorItems = new List<SelectListItem>();
            SelectedCourseIds = new List<int>();
        }

        public void SetCourseItems(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                CourseItems.Add(new SelectListItem()
                {
                    Value = course.CourseId.ToString(),
                    Text = course.CourseName
                });
            }
        }

        public void SetMajorItems(IEnumerable<Major> majors)
        {
            foreach (var major in majors)
            {
                MajorItems.Add(new SelectListItem()
                {
                    Value = major.MajorId.ToString(),
                    Text = major.MajorName
                });
            }
        }        
    }
}