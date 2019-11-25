using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.PersonalDetailsVM.SetCourseItems(CourseRepository.GetAll());
            viewModel.PersonalDetailsVM.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            studentVM.PersonalDetailsVM.Courses = new List<Course>();
            foreach (var id in studentVM.PersonalDetailsVM.SelectedCourseIds)
                studentVM.PersonalDetailsVM.Courses.Add(CourseRepository.Get(id));
            if (ModelState.IsValid)
            {
                var student = new Student();
                student.FirstName = studentVM.PersonalDetailsVM.FirstName;
                student.LastName = studentVM.PersonalDetailsVM.LastName;
                student.GPA = studentVM.PersonalDetailsVM.GPA;
                student.Major  = MajorRepository.Get(studentVM.PersonalDetailsVM.MajorId);
                student.Courses = studentVM.PersonalDetailsVM.Courses;
                student.Address = new Address();
                StudentRepository.Add(student);
                return RedirectToAction("List");
            }
            else
            {
                studentVM.PersonalDetailsVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.PersonalDetailsVM.SetMajorItems(MajorRepository.GetAll());
                return View(studentVM);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = StudentRepository.Get(id);
            var viewModel = new StudentVM();
            viewModel.AddressDetailsVM.StudentId  = student.StudentId;
            viewModel.PersonalDetailsVM.StudentId = student.StudentId;
            viewModel.PersonalDetailsVM.FirstName = student.FirstName;
            viewModel.PersonalDetailsVM.LastName = student.LastName;
            viewModel.PersonalDetailsVM.GPA = student.GPA;
            viewModel.AddressDetailsVM.Address = student.Address;
            viewModel.PersonalDetailsVM.MajorId = student.Major.MajorId;
            viewModel.PersonalDetailsVM.Courses = student.Courses;
            foreach (var course in viewModel.PersonalDetailsVM.Courses)
                viewModel.PersonalDetailsVM.SelectedCourseIds.Add(course.CourseId);
            viewModel.PersonalDetailsVM.SetCourseItems(CourseRepository.GetAll());
            viewModel.PersonalDetailsVM.SetMajorItems(MajorRepository.GetAll());
            viewModel.AddressDetailsVM.SetStateItems(StateRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditPersonal(PersonalDetailsVM PersonalDetailsVM)
        {            
            var studentVM = new StudentVM();
            studentVM.PersonalDetailsVM.SelectedCourseIds = PersonalDetailsVM.SelectedCourseIds;
            studentVM.PersonalDetailsVM.Courses = new List<Course>();
            foreach (var id in studentVM.PersonalDetailsVM.SelectedCourseIds)
                studentVM.PersonalDetailsVM.Courses.Add(CourseRepository.Get(id));

            if (ModelState.IsValid)
            {
                var student = new Student();
                student.StudentId = PersonalDetailsVM.StudentId;
                student.FirstName = PersonalDetailsVM.FirstName;
                student.LastName = PersonalDetailsVM.LastName;
                student.GPA = PersonalDetailsVM.GPA;
                student.Major = MajorRepository.Get(PersonalDetailsVM.MajorId);
                student.Courses = studentVM.PersonalDetailsVM.Courses;
                StudentRepository.Edit(student);
                return RedirectToAction("List");
            }
            else
            {
                studentVM.AddressDetailsVM.StudentId = PersonalDetailsVM.StudentId;
                studentVM.PersonalDetailsVM.StudentId = PersonalDetailsVM.StudentId;
                studentVM.PersonalDetailsVM.FirstName = PersonalDetailsVM.FirstName;
                studentVM.PersonalDetailsVM.LastName = PersonalDetailsVM.LastName;
                studentVM.PersonalDetailsVM.GPA = PersonalDetailsVM.GPA;
                studentVM.PersonalDetailsVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.PersonalDetailsVM.SetMajorItems(MajorRepository.GetAll());
                studentVM.AddressDetailsVM.SetStateItems(StateRepository.GetAll());
                var getstudent = StudentRepository.Get(PersonalDetailsVM.StudentId);
                studentVM.AddressDetailsVM.Address = getstudent.Address;
                return View("Edit", studentVM);
            }
        }

        [HttpPost]
        public ActionResult EditAddress(AddressDetailsVM AddressDetailsVM)
        {
            if (ModelState.IsValid)
            {
                StudentRepository.SaveAddress(AddressDetailsVM.StudentId, AddressDetailsVM.Address);
                return RedirectToAction("List");
            }
            else
            {
                var studentVM = new StudentVM();
                studentVM.PersonalDetailsVM.StudentId = AddressDetailsVM.StudentId;
                studentVM.AddressDetailsVM.StudentId = AddressDetailsVM.StudentId;
                var getstudent = StudentRepository.Get(AddressDetailsVM.StudentId);
                studentVM.PersonalDetailsVM.FirstName = getstudent.FirstName;
                studentVM.PersonalDetailsVM.LastName = getstudent.LastName;
                studentVM.PersonalDetailsVM.GPA = getstudent.GPA;
                studentVM.PersonalDetailsVM.MajorId = getstudent.Major.MajorId;
                studentVM.PersonalDetailsVM.Courses = new List<Course>();
                studentVM.PersonalDetailsVM.Courses = getstudent.Courses;
                foreach (var course in studentVM.PersonalDetailsVM.Courses)
                    studentVM.PersonalDetailsVM.SelectedCourseIds.Add(course.CourseId);
                studentVM.PersonalDetailsVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.PersonalDetailsVM.SetMajorItems(MajorRepository.GetAll());
                studentVM.AddressDetailsVM.SetStateItems(StateRepository.GetAll());                
                studentVM.AddressDetailsVM.Address = AddressDetailsVM.Address;
                return View("Edit", studentVM);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var student = StudentRepository.Get(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }
    }
}