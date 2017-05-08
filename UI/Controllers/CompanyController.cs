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
    public class CompanyController : BaseController
    {
        public CompanyController(IUnitOfWork uow, ApplicationUserManager userManager)
            : base(uow, userManager)
        {
        }

        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                FillFromViewModel(company,model);
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

        private void FillFromViewModel(Company company,CompanyVM model)
        {
            company.Address = model.Address;
            company.Benefits = model.Benefits;
            company.City = model.City;
            company.ContactEmail = model.ContactEmail;
            company.ContactFirstName = model.ContactFirstName;
            company.ContactLastName = model.ContactLastName;
            company.ContactPhone = model.ContactPhone;
            company.Name = model.Name;
            company.Users.Add(CurrentUser);
        }
        #endregion
    }
}
