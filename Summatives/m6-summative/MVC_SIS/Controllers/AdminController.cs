using Exercises.Models.Data;
using Exercises.Models.ViewModels;
using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercises.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new Major());
        }

        [HttpPost]
        public ActionResult AddMajor(Major major)
        {
            if (ModelState.IsValid)
            {
                MajorRepository.Add(major.MajorName);
                return RedirectToAction("Majors");
            }
            else
            {
                return View("AddMajor", major);
            }            
        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult EditMajor(Major major)
        {
            if (ModelState.IsValid)
            {
                MajorRepository.Edit(major);
                return RedirectToAction("Majors");
            }
            else
            {
                return View("EditMajor", major);
            }                
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult DeleteMajor(Major major)
        {
            MajorRepository.Delete(major.MajorId);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult Courses()
        {
            var model = CourseRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View(new Course());
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepository.Add(course.CourseName);
                return RedirectToAction("Courses");
            }
            else
            {
                return View("AddCourse", course);
            }                
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult EditCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepository.Edit(course);
                return RedirectToAction("Courses");
            }
            else
            {
                return View("EditCourse", course);
            }                
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            var course = CourseRepository.Get(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult DeleteCourse(Course course)
        {
            CourseRepository.Delete(course.CourseId);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public ActionResult States()
        {
            var model = StateRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddState()
        {
            return View(new StateVM());
        }

        [HttpPost]
        public ActionResult AddState(StateVM stateVM)
        {
            if (ModelState.IsValid)
            {
                var state = new State();
                state.StateAbbreviation = stateVM.StateAbbreviation;
                state.StateName = stateVM.StateName;
                StateRepository.Add(state);
                return RedirectToAction("States");
            }
            else
            {
                return View("AddState", stateVM);
            }                
        }

        [HttpGet]
        public ActionResult EditState(string id)
        {
            var state = StateRepository.Get(id);
            var stateVM = new StateVM();
            stateVM.StateAbbreviation = state.StateAbbreviation;
            stateVM.StateName = state.StateName;
            return View(stateVM);
        }

        [HttpPost]
        public ActionResult EditState(StateVM stateVM)
        {
            if (ModelState.IsValid)
            {
                var state = new State();
                state.StateAbbreviation = stateVM.StateAbbreviation;
                state.StateName = stateVM.StateName;
                StateRepository.Edit(state);
                return RedirectToAction("States");
            }
            else
            {
                return View("EditState", stateVM);
            }                
        }

        [HttpGet]
        public ActionResult DeleteState(string id)
        {
            var state = StateRepository.Get(id);
            var stateVM = new StateVM();
            stateVM.StateAbbreviation = state.StateAbbreviation;
            stateVM.StateName = state.StateName;
            return View(stateVM);
        }

        [HttpPost]
        public ActionResult DeleteState(StateVM stateVM)
        {
            var state = new State();
            state.StateAbbreviation = stateVM.StateAbbreviation;
            state.StateName = stateVM.StateName;
            StateRepository.Delete(state.StateAbbreviation);
            return RedirectToAction("States");
        }
    }
}