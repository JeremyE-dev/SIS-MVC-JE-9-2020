using Exercises.Models.Data;
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
        public static State oldState;

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
            if (string.IsNullOrEmpty(course.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please Enter a Course Name");
            }

            if (ModelState.IsValid)
            {
                CourseRepository.Add(course.CourseName);
                return RedirectToAction("Courses");
            }

            else
            {
                return View(new Course());
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

            if (string.IsNullOrEmpty(course.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please Enter a Course Name");
            }

            if (ModelState.IsValid)
            {
                CourseRepository.Edit(course);
                return RedirectToAction("Courses");
            }

            else
            {
                return View(course);
            }

        }


        [HttpGet]
        //going to try changing from "state abbrevfiation to id" as variable name
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
           
            return View(new State());
        }

        [HttpPost]
        public ActionResult AddState(State state)
        {
            if (string.IsNullOrEmpty(state.StateName))
            {
                ModelState.AddModelError("StateName", "Please Enter a State Name");
            }

            if (string.IsNullOrEmpty(state.StateAbbreviation))
            {
                ModelState.AddModelError("StateAbbreviation", "Please Enter a State Abbreviation");
            }

            if (ModelState.IsValid)
            {
                StateRepository.Add(state);

                return RedirectToAction("States");
            }

            else
            {
                return View(new State());
            }


           

        }

        [HttpGet]
        public ActionResult EditState(string id)
        {
            var state = StateRepository.Get(id);
            oldState = new State();
            oldState.StateAbbreviation= state.StateAbbreviation;
            oldState.StateName = state.StateName;
            return View(state);
        }

        [HttpPost]
        public ActionResult EditState(State state)
        {
            if (string.IsNullOrEmpty(state.StateName))
            {
                ModelState.AddModelError("StateName", "Please Enter a State Name");
            }

            if (string.IsNullOrEmpty(state.StateAbbreviation))
            {
                ModelState.AddModelError("StateAbbreviation", "Please Enter a State Abbreviation");
            }

            if (ModelState.IsValid)
            {
                //the state abbraviation was changed was changed
               if (StateRepository.Get(state.StateAbbreviation)==null)
                {
                    //edit the state that has the old state 
                    //abbreviation and change the state name to the name that was input
                    StateRepository.EditStateName(oldState, state);
                    return RedirectToAction("States");
                }

               else
                {
                    StateRepository.Edit(state);
                    return RedirectToAction("States");
                }

               
            }

            else
            {
                return View(state);
            }


        }


        [HttpGet]
        //going to try changing from "state abbrevfiation to id" as variable name
        public ActionResult DeleteState(string id)
        {
            var state = StateRepository.Get(id);
            return View(state);
        }

        [HttpPost]
        public ActionResult DeleteState(State state)
        {
            StateRepository.Delete(state.StateAbbreviation);
            return RedirectToAction("States");
        }

 
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
            if(string.IsNullOrEmpty(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please Enter a Major Name");
            }

            if(ModelState.IsValid)
            {
                MajorRepository.Add(major.MajorName);
                return RedirectToAction("Majors");
            }

            else
            {
                return View(new Major());
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
            if (string.IsNullOrEmpty(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "Please Enter a Major Name");
            }

            if (ModelState.IsValid)
            {
                MajorRepository.Edit(major);
                return RedirectToAction("Majors");
            }

            else
            {
                return View(major);
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

    }
}