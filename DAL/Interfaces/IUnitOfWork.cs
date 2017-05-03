using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<CustomRole> RoleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Company> CompanyRepository { get; }
        IRepository<Course> CourseRepository { get; }
        IRepository<Resume> ResumeRepository { get; }
        IRepository<Tag> TagRepository { get; }
    }
}
