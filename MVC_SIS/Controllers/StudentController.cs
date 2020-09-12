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




            if(MajorRepository.Get(studentVM.Student.Major.MajorId) != null)
            {
                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
                StudentRepository.Add(studentVM.Student);
            }

            //studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
            
            //StudentRepository.Add(studentVM.Student);

            //return RedirectToAction("List");

            

            if (studentVM.Student.Major.MajorName != null) 
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




            


           
        }

        //add edit functioins here


        [HttpGet]
        //when the "Edit" button in the student list is clicked - this method is called
        public ActionResult EditStudent(int id)
        {
            //set student to the student object associated with this id number
            // I don't think I need this, as it is not being used
            var student = StudentRepository.Get(id);
            //set viewModel to a new instance of StudentVM
            var viewModel = new StudentVM();
            //set the student (object) field in the viewModel instance to the
            // student associated with the student id
            viewModel.Student = StudentRepository.Get(id);
            //set the CouseItems field in the viewmodel instance to all of the 
            //courses in the couse repository
            viewModel.SetCourseItems(CourseRepository.GetAll());
            //set the CouseItems field in the viewmodel instance to all of the 
            //majors in  in the major repository
            viewModel.SetMajorItems(MajorRepository.GetAll());
            //send viewModel to the Edit Student View
            return View(viewModel);     
        }

     

        [HttpPost]

        //when save button is clicked this method is called
        public ActionResult EditStudent(StudentVM studentVM)
        {
            //set the couses field in the student field in the studentVM object to 
            //an empty list of courses
            studentVM.Student.Courses = new List<Course>();

            //for each course in the list of selected courses
            //add it to the courses list in the student field of the student view model - no courses in here
            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            //go to the major repository and get the major associated with this students major
            //-- This is here to replace the major in case it has changed after user input
            // set breakpoint to check if this value is null and is causeing an issue
            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

            //get the student assosiated with this student id, se "student" to that student object
            var student = StudentRepository.Get(studentVM.Student.StudentId);
            //if studentVM.Student.Address.Street1 is null
            // required field - do not post , return to add screen add error message

            //update street 1
            //student.Address.Street1 = studentVM.Student.Address.Street1;

            if (studentVM.Student.Address.Street1 != null)
            {
                //review this next 
                //ModelState.AddModelError("Street1", "Please Enter a Street Address");
                //update street 1
                student.Address.Street1 = studentVM.Student.Address.Street1;
            }

            
            if (studentVM.Student.Address.Street2 != null)
            {
                //this will cause everything to be null - comment out for next run
                //student.Address = new Address();
                student.Address.Street2 = studentVM.Student.Address.Street2;

                studentVM.Student.Address.Street2 = "";
            }

            else
            {

                studentVM.Student.Address.Street2 = "";
            }





            //student.Address.City = studentVM.Student.Address.City;
            if (studentVM.Student.Address.City != null)
            {
                student.Address.City = studentVM.Student.Address.City;
            }

            //student.Address.State = studentVM.Student.Address.State;
            if (studentVM.Student.Address.State.StateAbbreviation != null)
            {
                student.Address.State.StateAbbreviation = studentVM.Student.Address.State.StateAbbreviation;
            }

            //student.Address.PostalCode = studentVM.Student.Address.PostalCode;
            if (studentVM.Student.Address.PostalCode != null)
            {
                student.Address.PostalCode = studentVM.Student.Address.PostalCode;
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
                studentVM.SetMajorItems(MajorRepository.GetAll());
              
                return View(studentVM);//
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