using AspNetCoreODataWithModel.Shared.Models.Database;
using System;
using System.Linq;

namespace AspNetCoreODataWithModel.Data.Repositories
{
    /// <summary>
    /// RepositoryBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly ApplicationContext dataContext; 
         
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataContext"></param>
        public RepositoryBase(ApplicationContext dataContext)
        { 
            this.dataContext = dataContext; 
        }

        /// <summary>
        /// ListAll
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> ListAll()
        {
            return dataContext.Set<T>().AsQueryable(); 
        }

        /// <summary>
        /// ListAll
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IQueryable<T> ListAll(Func<IQueryable<T>, IQueryable<T>> func)
        {
            return dataContext.Set<T>().AsQueryable();
        }

        #region IDisposable Support

        private bool disposedValue = false;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
