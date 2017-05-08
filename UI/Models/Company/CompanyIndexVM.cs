using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models.Company
{
    public class CompanyIndexVM
    {
        public bool CanCreateCompany { get; set; }
        public List<CompanyListVM> Companies { get; set; }
    }

    public class CompanyListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string City { get; set; }
    }
}