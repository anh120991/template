using HDBH.SQLData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.SQLData
{
    public class BaseUnitOfWork : IUnitOfWork
    {
        private readonly BaseDbContext _dbContext;

        #region Repositories



        #endregion

        public BaseUnitOfWork(BaseDbContext dbContext)
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