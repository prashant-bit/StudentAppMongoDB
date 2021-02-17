using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using StudentApp.Models;
using StudentApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp.Controllers
{
    public class StudentsController : Controller
    {
        // GET: StudentsController
        private readonly StudentService _studentService;
        private object students;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
            
        }
        public ActionResult Index()
        {
            var stu = _studentService.Get();
            var dep =_studentService.GetDep();
           // var result2 = _studentService.Get2();
            var result = from o in stu.AsQueryable()
                         join i in dep.AsQueryable()
                         on o.Dep_Id equals i.Id
                         into Joined_Department
                         from b in Joined_Department
                         select new Student
                         {
                             ImageUrl = o.ImageUrl,
                             Id = o.Id,
                             Usn = o.Usn,
                             Email = o.Email,
                             Dep_Name = b.Name

                         };
            return View(result.ToList());
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentService.Get(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: StudentsController/Create
        public ActionResult Create()
        {
            LoadOptions();
            return View();
        }

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if(ModelState.IsValid)
            {
                _studentService.Create(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: StudentsController/Edit/5
        public ActionResult Edit(string id)
        {
            LoadOptions();
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentService.Get(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id,Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _studentService.Update(id, student);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(student);
            }
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentService.Get(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfiremd(string id)
        {
            try
            {
                var student = _studentService.Get(id);

                if (student == null)
                {
                    return NotFound();
                }

                _studentService.Remove(student.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void LoadOptions()
        {
            try {
                List<Department> depList = new List<Department>();
                depList = _studentService.GetDep();
                ViewBag.DepList = depList;
            }
            catch{ }
        }
    }
}
