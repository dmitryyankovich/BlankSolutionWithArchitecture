using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        private DbContext Context { get; set; }

        public void Commit()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        private Repository<User> _userRepository;
        public IRepository<User> UserRepository
        {
            get { return _userRepository ?? (_userRepository = new Repository<User>(Context)); }
        }

        private Repository<CustomRole> _roleRepository;
        public IRepository<CustomRole> RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new Repository<CustomRole>(Context)); }
        }
        private Repository<Company> _companyRepository;
        public IRepository<Company> CompanyRepository
        {
            get { return _companyRepository ?? (_companyRepository = new Repository<Company>(Context)); }
        }
        private Repository<Course> _courseRepository;
        public IRepository<Course> CourseRepository
        {
            get { return _courseRepository ?? (_courseRepository = new Repository<Course>(Context)); }
        }
        private Repository<Resume> _resumeRepository;
        public IRepository<Resume> ResumeRepository
        {
            get { return _resumeRepository ?? (_resumeRepository = new Repository<Resume>(Context)); }
        }
        private Repository<Tag> _tagRepository;
        public IRepository<Tag> TagRepository
        {
            get { return _tagRepository ?? (_tagRepository = new Repository<Tag>(Context)); }
        }
        private Repository<CourseResponse> _courseResponseRepository;
        public IRepository<CourseResponse> CourseResponseRepository
        {
            get { return _courseResponseRepository ?? (_courseResponseRepository = new Repository<CourseResponse>(Context)); }
        }
    }
}
