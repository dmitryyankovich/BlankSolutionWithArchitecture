using BLL.Identity;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Core.Controllers;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(IUnitOfWork uow, ApplicationUserManager userManager)
            : base(uow, userManager)
        {
        }
        public ActionResult Index()
        {
            if (User.IsInRole("CustomerAdministrator"))
            {
                return RedirectToAction("Index", "Company");
            }
            return RedirectToAction("Index", "Courses");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreateResume()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ResumeVM model)
        {
            try
            {
                var resume = CurrentUser.Resume ?? new Resume();
                FillFromViewModel(resume, model);
                CurrentUser.Resume = resume;
                UnitOfWork.UserRepository.Update(CurrentUser);
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditResume()
        {
            var resume = CurrentUser.Resume;
            var model = new ResumeVM();
            if (resume != null)
            {
                FillViewModel(resume, model);
            }
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(ResumeVM model)
        {
            try
            {
                var resume = CurrentUser.Resume ?? new Resume();
                FillFromViewModel(resume, model);
                CurrentUser.Resume = resume;
                UnitOfWork.UserRepository.Update(CurrentUser);
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Private methods
        private void FillFromViewModel(Resume resume, ResumeVM model)
        {
            resume.Cellphone = model.Cellphone;
            resume.DateOfBirth = model.DateOfBirth;
            resume.EducationLevel = model.EducationLevel;
            resume.EnglishLevel = model.EnglishLevel;
            resume.User = CurrentUser;
            resume.UserId = CurrentUser.Id;
            resume.LastName = model.LastName;
            resume.Name = model.Name;
            resume.SkillsLevel = model.SkillsLevel;
            var tagsArray = model.Tags.Split(',');
            resume.Tags = new List<Tag>();
            foreach (var tag in tagsArray)
            {
                var dbTag = UnitOfWork.TagRepository.GetAll().FirstOrDefault(t => t.Name == tag);
                if (dbTag == null)
                {
                    dbTag = new Tag
                    {
                        Name = tag
                    };
                    UnitOfWork.TagRepository.Insert(dbTag);
                }
                resume.Tags.Add(dbTag);
            }
            UnitOfWork.Commit();
        }

        private void FillViewModel(Resume resume, ResumeVM model)
        {
            model.Cellphone = resume.Cellphone;
            model.DateOfBirth = resume.DateOfBirth;
            model.EducationLevel = resume.EducationLevel;
            model.EnglishLevel = resume.EnglishLevel;
            model.LastName = resume.LastName;
            model.Name = resume.Name;
            model.SkillsLevel = resume.SkillsLevel;
            model.Tags = string.Join(",", resume.Tags.Select(t => t.Name).ToList());
        }
        #endregion
    }
}