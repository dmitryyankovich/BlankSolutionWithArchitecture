using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class ResumeVM
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        public SkillsLevel SkillsLevel { get; set; }
        public string Tags { get; set; }
    }
}