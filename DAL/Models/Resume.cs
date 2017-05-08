using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DAL.Models
{
    public class Resume
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        public SkillsLevel SkillsLevel { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual User User { get; set; }
    }
}
