using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ToDo.Core.Data;
using System.Data.Entity;


namespace ToDo.Core.Repository
{
    internal class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly ToDoListEntities _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ToDoListEntities context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual T FindById(object entityId)
        {
            return _dbSet.Find(entityId);
        }

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _dbSet.Where(filter);
            }

            return _dbSet;
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object entityId)
        {
            T entityToDelete = _dbSet.Find(entityId);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached) //Concurrency için
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public IEnumerable<T> ToList()
        {
            return _dbSet.ToList();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
