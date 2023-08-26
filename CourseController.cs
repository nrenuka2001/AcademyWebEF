using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AcademyWebEF
{
    [Authorize(Roles = Roles.Admin)]
    public class CourseController : Controller
    {
       
        public IActionResult CoursesList()
        {
            var DbContext = new AcademyDbContext();
            var course = DbContext.Courses.ToList();
            return View(course);
        }

        [HttpGet]
        public IActionResult CourseEditor()
        {
           var  model = new CourseEditorModel();
           return View(model);
        }

        [HttpPost]
        public IActionResult Create(CourseEditorModel coursemodel)
        {
          

            if (ModelState.IsValid)
            {
                Course coursedata = new Course();
                coursedata.CourseId = coursemodel.CourseId;
                coursedata.CourseTitle = coursemodel.CourseTitle;
                coursedata.DurationInDays = coursemodel.DurationInDays;
                coursedata.Price = coursemodel.Price;
                coursedata.IsActive = coursemodel.IsActive;

                var DbContext = new AcademyDbContext();
                DbContext.Courses.Add(coursedata);
                DbContext.SaveChanges();
                return RedirectToAction("CoursesList");


            }
            else
            {
                ModelState.AddModelError("", "Course record not created, please fix errors and save again!");

                return View("CourseEditor", coursemodel);
            }



        }

        [HttpGet]
        public IActionResult EditCourse(int courseId) // binding primitive type
        {
            var DbContext = new AcademyDbContext();

            // get student obj
            var courseObj = DbContext.Courses.Where(p => p.CourseId == courseId).FirstOrDefault();

            // create an object of model class
            // and bind the data from student obj
            var editorCourseModel = new CourseEditorModel();
            editorCourseModel.CourseTitle = courseObj.CourseTitle;
            editorCourseModel.DurationInDays = courseObj.DurationInDays;
            editorCourseModel.Price = courseObj.Price;
            bool IsActive = (bool)courseObj.IsActive;
            editorCourseModel.IsActive = courseObj.IsActive;


            return View(editorCourseModel);
        }

        [HttpPost]
        public IActionResult Update(CourseEditorModel editModel) // binding complex type
        {
            var DbContext = new AcademyDbContext();

            //fetching the student obj from database
            var CourseUpdate = DbContext.Courses.Where(p => p.CourseId == editModel.CourseId).FirstOrDefault();

            var edit = new CourseEditorModel();
            // updating the details of existing student
            CourseUpdate.CourseTitle = editModel.CourseTitle;
            CourseUpdate.DurationInDays = editModel.DurationInDays;
            CourseUpdate.Price = editModel.Price;
            CourseUpdate.IsActive = editModel.IsActive;

            DbContext.Courses.Update(CourseUpdate);
            DbContext.SaveChanges();
            return RedirectToAction("CoursesList");

        }



        [HttpGet]
        public ViewResult CourseRO(int courseId)
        {
            var DbContext = new AcademyDbContext();

            // get student obj
            var courseObj = DbContext.Courses
                                      
                                      .Where(p => p.CourseId == courseId).FirstOrDefault();

            return View(courseObj);
        }

        [HttpPost]
        public JsonResult DeleteCourse(int CourseId)
        {
           var DbContext =new AcademyDbContext();
            var delete = DbContext.Courses.Where(p => p.CourseId == CourseId).FirstOrDefault();
            DbContext.Courses.Remove(delete);
            DbContext.SaveChanges();
            return Json(true);
        }

    }
}