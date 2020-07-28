using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDBH.SQLData.Interface;

namespace HDBH.SQLData
{
    public class RepositoryUnitOfWork : BaseUnitOfWork, IRepositoryUnitOfWork
    {
        private readonly RepositoryDbContext _dbContext;

        #region Repositories

        //public IRepository<User> UserRepository = new GenericRepository<User>(_dbContext);

        #endregion

        public RepositoryUnitOfWork(RepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}