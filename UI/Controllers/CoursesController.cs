using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Identity;
using DAL.Interfaces;
using DAL.Models;
using UI.Core.Controllers;
using UI.Models.Courses;

namespace UI.Controllers
{
    [Authorize]
    public class CoursesController : BaseController
    {

        public CoursesController(IUnitOfWork uow, ApplicationUserManager userManager)
            : base(uow, userManager)
        {
        }

        // GET: Courses
        public ActionResult Index()
        {
            var model = new CoursesIndexVM
            {
                Courses = UnitOfWork.CourseRepository.GetAll().ToList().Select(GetCourseListVM).ToList()
            };
            return View(model);
        }

        // GET: Courses/Details/5
        public ActionResult Details(int id)
        {
            var course = UnitOfWork.CourseRepository.Get(id);
            if (course == null)
            {
                //TODO: show error message
                throw new Exception("The course doesn't exist.");
            }
            var model = GetViewModel(course);
            return View(model);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        public ActionResult Create(CourseVM model)
        {
            try
            {
                var course = new Course();
                FillFromViewModel(course, model);
                UnitOfWork.CourseRepository.Insert(course);
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Courses/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Courses/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Private methods

        private CourseListVM GetCourseListVM(Course course)
        {
            return new CourseListVM
            {
                Id = course.Id,
                CompanyName = course.Company.Name,
                Description = course.Description,
                LastUpdatedOn = course.UpdateDate,
                SalaryLevel = course.SalaryLevel,
                Title = course.Title
            };
        }

        private CourseDetailsVM GetViewModel(Course course)
        {
            return new CourseDetailsVM
            {
                Id = course.Id,
                CanBeEdited = User.IsInRole("CustomerAdministator") && course.Company == CurrentUser.Company,
                Title = course.Title,
                Advantages = course.Advantages,
                Description = course.Description,
                MinimalExpirience = course.MinimalExpirience,
                Requirements = course.Requirements,
                Responsibilities = course.Responsibilities,
                SalaryLevel = course.SalaryLevel,
                Tags = string.Join(",",course.Tags.Select(t => t.Name).ToList())      
            };
        }

        private void FillFromViewModel(Course course, CourseVM model)
        {
            course.Advantages = model.Advantages;
            course.Company = CurrentUser.Company;
            course.Description = model.Description;
            course.MinimalExpirience = model.MinimalExpirience;
            course.Requirements = model.Requirements;
            course.Responsibilities = model.Responsibilities;
            course.SalaryLevel = model.SalaryLevel;
            course.Title = model.Title;
            course.UpdateDate = DateTime.UtcNow;
            var tagsArray = model.Tags.Split(',');
            course.Tags = new List<Tag>();
            foreach (var tag in tagsArray)
            {
                var dbTag = new Tag
                {
                    Name = tag
                };
                UnitOfWork.TagRepository.Insert(dbTag);
                course.Tags.Add(dbTag);
            }
            UnitOfWork.Commit();
        }
        #endregion
    }
}
