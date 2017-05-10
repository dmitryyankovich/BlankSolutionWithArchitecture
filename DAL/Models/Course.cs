using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL.Interfaces;

namespace DAL.Models
{
    public class Course : IIdModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string Advantages { get; set; }
        public string SalaryLevel { get; set; }
        public string MinimalExpirience { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<CourseResponse> CourseResponses { get; set; }
    }

    public class CourseResponse : IIdModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public CourseResponseStatus Status { get; set; }
        public string RefinementText { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
    }
}
