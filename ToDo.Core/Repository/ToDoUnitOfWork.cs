using System;
using ToDo.Core.Data;

namespace ToDo.Core.Repository
{
    internal interface IUnitOfWork
         : IDisposable
    {
        void Save();
    }

    internal class ToDoUnitOfWork
        : IUnitOfWork
    {
        internal readonly IGenericRepository<ToDo_List> ToDoListRepository;
        internal readonly IGenericRepository<User> UserRepository;
        internal readonly IGenericRepository<UserToken> UserTokenRepository;

        public readonly ToDoListEntities DbContext;

        private bool _disposed = false;
        public ToDoUnitOfWork(ToDoListEntities context, IGenericRepository<ToDo_List> toDoListRepository, IGenericRepository<User> userRepository, IGenericRepository<UserToken> userTokenRepository)

        {
            ToDoListRepository = toDoListRepository;
            UserRepository = userRepository;
            UserTokenRepository = userTokenRepository;
            DbContext = context;
        }

        public void Save()
        {

            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                try
                {
                    var validationErrors = ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors;

                }
                catch
                {
                    //ignored
                }
                throw ex;
            }

        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}