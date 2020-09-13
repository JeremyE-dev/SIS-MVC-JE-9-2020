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

            if(studentVM.Student.Major!=null)
            {
                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
            }
            
            
            if (ModelState.IsValid)
            {
                StudentRepository.Add(studentVM.Student);
                return RedirectToAction("List");
            }

            else
            {
              

                studentVM.SetCourseItems(CourseRepository.GetAll());
                foreach (var id in studentVM.SelectedCourseIds)
                    studentVM.Student.Courses.Add(CourseRepository.Get(id));

                studentVM.SetMajorItems(MajorRepository.GetAll());

                return View(studentVM);
            }

        }

    


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

      
        public ActionResult EditStudent(StudentVM studentVM)
        {
            
            studentVM.Student.Courses = new List<Course>();

        
            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

         
            var student = StudentRepository.Get(studentVM.Student.StudentId);
        

            if (studentVM.Student.Address.Street1 != null)
            {
              
                student.Address = new Address();
                student.Address.Street1 = studentVM.Student.Address.Street1;
            }

            
            if (studentVM.Student.Address.Street2 != null)
            {
              
                student.Address.Street2 = studentVM.Student.Address.Street2;

                studentVM.Student.Address.Street2 = "";
            }

            else
            {

                studentVM.Student.Address.Street2 = "";
            }


           
            if (studentVM.Student.Address.City != null)
            {
                student.Address.City = studentVM.Student.Address.City;
            }

           
            if (studentVM.Student.Address.State.StateAbbreviation != null)
            {
                student.Address.State = new State();
                student.Address.State.StateAbbreviation = studentVM.Student.Address.State.StateAbbreviation;
            }

        
            if (studentVM.Student.Address.PostalCode != null)
            {
                student.Address.PostalCode = studentVM.Student.Address.PostalCode;
            }



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