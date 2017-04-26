using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        internal IDbSet<T> DbSet;
        internal DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public void Insert(T entity)
        {
            if (entity != null)
            {
                DbSet.Add(entity);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id);
            if (entity != null)
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                DbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public T Get(int id)
        {
            var entity = DbSet.Find(id);
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            var entities = DbSet;
            return entities;
        }
    }
}
