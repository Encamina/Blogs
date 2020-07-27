using System;
using System.Linq;

namespace AspNetCoreODataWithModel.Data.Repositories
{
    /// <summary>
    /// IRepositoryBase
    /// </summary>
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        /// <summary>
        /// ListAllAsync
        /// </summary>
        /// <returns></returns>
        IQueryable<T> ListAll();

        /// <summary>
        /// ListAllAsync
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        IQueryable<T> ListAll(Func<IQueryable<T>, IQueryable<T>> func);
    }
}
