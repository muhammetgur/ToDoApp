using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ToDo.Core.Repository
{
    internal interface IGenericRepository<T>
        where T : class
    {
        T FindById(object EntityId);
        IEnumerable<T> Where(Expression<Func<T, bool>> Filter = null);
        void Insert(T Entity);
        void Update(T Entity);
        void Delete(object EntityId);
        void Delete(T Entity);
        IEnumerable<T> ToList();
        IQueryable<T> AsQueryable();
    }
}
