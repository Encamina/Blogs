using AspNetCoreODataWithModel.Data.Entities;

namespace AspNetCoreODataWithModel.Data.Repositories
{
    /// <summary>
    /// TaskRepository
    /// </summary>
    public class TaskRepository : RepositoryBase<Tarea>, ITaskRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataContext"></param>
        public TaskRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
