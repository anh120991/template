using HDBH.Data.Infrastructure;
using System.Threading.Tasks;

namespace HDBH.Data
{
    public class BaseUnitOfWork : IUnitOfWork
    {
        private readonly BaseContext _dbContext;
        #region Repositories
        //public IRepository<User> UserRepository = new GenericRepository<User>(_dbContext);
        #endregion
        public BaseUnitOfWork(BaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
