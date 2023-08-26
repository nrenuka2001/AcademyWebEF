﻿using AcademyWebEF.BusinessEntities;
using AcademyWebEF.Models;
using AcademyWebEF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcademyWebEF
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
    public class StudentController : Controller
    {
        private readonly StudentService studentService;
        private readonly UserService userService;

        public StudentController() //Constructor
        {
            studentService = new StudentService();
            userService = new UserService();
        }

        public IActionResult StudentsList()
        {
            var students = studentService.FetchStudents();

            return View(students);
        }

        [HttpGet]
        public IActionResult StudentEditor()
        {
            StudentEditorModel model = studentService.PrepareStudentCreateModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(StudentEditorModel editorModel)
        {
            ModelState.Remove("Courses");

            if (ModelState.IsValid)
            {
                var userObj = userService.CreateUser(editorModel.RollNo, "123456", editorModel.Email, Roles.Student);

                studentService.CreateStudent(editorModel, userObj.UserId);

                return RedirectToAction("StudentsList");
            }
            else
            {
                ModelState.AddModelError("", "Student record not created, please fix errors and save again!");

                return View("StudentEditor", editorModel);
            }

        }

        [HttpGet]
        public IActionResult EditStudent(int studentId) // binding primitive type
        {
            var editorModel = studentService.PrepareStudentUpdateModel(studentId);

            return View(editorModel);
        }

        [HttpPost]
        public IActionResult Update(StudentEditorModel editorModel) // binding complex type
        {
            if (ModelState.IsValid)
            {
                studentService.UpdateStudent(editorModel);

                return RedirectToAction("StudentsList");
            }
            else
            {
                ModelState.AddModelError("", "Student record not updated, please fix errors and save again!");

                return View("EditStudent", editorModel);
            }
        }

        [HttpGet]
        public ViewResult StudentRO(int studentId)
        {
            var dbContext = new AcademyDbContext();

            // get student obj
            var studentObj = dbContext.Students
                                      .Include(p => p.Course)
                                      .Include(p => p.User)
                                      .FirstOrDefault(p => p.StudentId == studentId);

            return View(studentObj);
        }

        [HttpPost]
        public JsonResult DeleteStudent(int studId)
        {
            try
            {
                studentService.DeleteOperation(studId);


                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

    }
}