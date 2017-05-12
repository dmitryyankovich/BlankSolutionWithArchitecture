using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace UI.Models.Courses
{
    public class CourseDetailsVM
    {
        public bool CanBeEdited { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string Advantages { get; set; }
        public string SalaryLevel { get; set; }
        public string MinimalExpirience { get; set; }
        public string Tags { get; set; }
        public bool IsResponseSended { get; set; }
        public CourseResponseStatus? Status { get; set; }
        public string RefinementMessage { get; set; }
        public string RefinementAnswer { get; set; }
        public string Message { get; set; }
        public int? CourseResponseId { get; set; }
    }
}