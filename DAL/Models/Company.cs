using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Models
{
    public class Company : IIdModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PathToAvatar { get; set; }
        public string Benefits { get; set; }
        public bool IsCreationRequest { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
