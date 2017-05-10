using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Identity;
using DAL.Interfaces;
using DAL.Models;
using UI.Core.Controllers;
using UI.Models.Company;

namespace UI.Controllers
{
    [Authorize]
    public class CompanyController : BaseController
    {
        public CompanyController(IUnitOfWork uow, ApplicationUserManager userManager)
            : base(uow, userManager)
        {
        }

        // GET: Company
        public ActionResult Index()
        {
            var model = new CompanyIndexVM
            {
                CanCreateCompany = User.IsInRole("CustomerAdministrator") && CurrentUser.Company == null,
                Companies = UnitOfWork.CompanyRepository.GetAll().ToList().Select(GetCourseListVM).ToList()
            };
            return View(model);
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            var company = UnitOfWork.CompanyRepository.Get(id);
            if (company == null)
            {
                //TODO: show error message
                throw new Exception("The customer doesn't exist.");
            }
            var model = GetViewModel(company);
            return View(model);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(CompanyVM model)
        {
            try
            {
                var company = new Company
                {
                    IsCreationRequest = true,
                };
                FillFromViewModel(company, model);
                UnitOfWork.CompanyRepository.Insert(company);
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
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

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
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

        private CompanyListVM GetCourseListVM(Company company)
        {
            return new CompanyListVM
            {
                Id = company.Id,
                City = company.City,
                ContactEmail = company.ContactEmail,
                ContactFirstName = company.ContactFirstName,
                ContactLastName = company.ContactLastName,
                ContactPhone = company.ContactPhone,
                Name = company.Name
            };
        }

        public CompanyDetailsVM GetViewModel(Company company)
        {
            return new CompanyDetailsVM
            {
                Id = company.Id,
                Address = company.Address,
                Benefits = company.Benefits,
                City = company.City,
                ContactEmail = company.ContactEmail,
                ContactFirstName = company.ContactFirstName,
                ContactLastName = company.ContactLastName,
                ContactPhone = company.ContactPhone,
                Name = company.Name
            };
        }

        private void FillFromViewModel(Company company, CompanyVM model)
        {
            company.Address = model.Address;
            company.Benefits = model.Benefits;
            company.City = model.City;
            company.ContactEmail = model.ContactEmail;
            company.ContactFirstName = model.ContactFirstName;
            company.ContactLastName = model.ContactLastName;
            company.ContactPhone = model.ContactPhone;
            company.Name = model.Name;
            company.Users = new List<User>();
            company.Users.Add(CurrentUser);
        }
        #endregion
    }
}
