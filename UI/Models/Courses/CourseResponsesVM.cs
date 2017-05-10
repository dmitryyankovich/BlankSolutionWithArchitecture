using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace UI.Models.Courses
{
    public class CourseResponsesVM
    {
        public List<CourseResponseVM> CourseResponses { get; set; }
    }

    public class CourseResponseVM
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public CourseResponseStatus Status { get; set; }
    }
}