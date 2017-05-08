using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
