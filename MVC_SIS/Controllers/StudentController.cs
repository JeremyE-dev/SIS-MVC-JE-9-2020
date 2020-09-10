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
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            //trying to add use validation

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
            StudentRepository.Add(studentVM.Student);

            return RedirectToAction("List");

            /*

            if (string.IsNullOrEmpty(studentVM.Student.Major.MajorName)) 
            {

                ModelState.AddModelError("Student.Major.MajorName", "Please select a major");
                return RedirectToAction("Add");
            }

            else
            {
                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
                StudentRepository.Add(studentVM.Student);

                return RedirectToAction("List");
            }
            */


           
        }

        //add edit functioins here


        [HttpGet]

        //this will get the student record I want, then I can display that record in the view
        public ActionResult EditStudent(int id)
        {
            var student = StudentRepository.Get(id);
            var viewModel = new StudentVM();
            viewModel.Student = StudentRepository.Get(id);
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);     
        }

     

        [HttpPost]
        public ActionResult EditStudent(StudentVM studentVM)
        {
            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            var student = StudentRepository.Get(studentVM.Student.StudentId);
            //if studentVM.Student.Address.Street1 is null
            // required field - do not post , return to add screen add error message

            if (studentVM.Student.Address.Street1 == null)
            {
                ModelState.AddModelError("Street1", "Please Enter a Street Address");
            }

            if (studentVM.Student.Address.Street2 == null)
            {
                student.Address = new Address();

                student.Address.Street2 = "this field is optional";
            }

            if (studentVM.Student.Address.City == null)
            {
                ModelState.AddModelError("City", "Please Enter a City");
            }

            if (studentVM.Student.Address.State == null)
            {
                ModelState.AddModelError("State", "Please Enter a State");
            }

            if (studentVM.Student.Address.PostalCode == null)
            {
                ModelState.AddModelError("Postal Code", "Please Enter a Postal Code");
            }

            //student.Address.Street1 = studentVM.Student.Address.Street1;
            //student.Address.Street2 = studentVM.Student.Address.Street2;
            //student.Address.City = studentVM.Student.Address.City;
            //student.Address.State = studentVM.Student.Address.State;
            //student.Address.PostalCode = studentVM.Student.Address.PostalCode;

            if (ModelState.IsValid)
            {
                StudentRepository.Edit(studentVM.Student);

                return RedirectToAction("List");
            }
            else
            {
                return View("studentVM");
            }


        }

        [HttpGet]
        //going to try changing from "state abbrevfiation to id" as variable name
        public ActionResult DeleteStudent(int id)
        {
            var student = StudentRepository.Get(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult DeleteStudent(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }
    }
}