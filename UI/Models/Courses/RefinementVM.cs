using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models.Courses
{
    public class RefinementVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class RefinementAnswerVM
    {
        public string Message { get; set; }
        public string Answer { get; set; }
    }
}