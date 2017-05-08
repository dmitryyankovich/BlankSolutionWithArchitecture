using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models.Courses
{
    public class CoursesIndexVM
    {
        public List<CourseListVM> Courses { get; set; }
    }

    public class CourseListVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Description { get; set; }
        public string SalaryLevel { get; set; }
    }
}