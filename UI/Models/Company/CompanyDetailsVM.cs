using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models.Company
{
    public class CompanyDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Benefits { get; set; }
    }
}