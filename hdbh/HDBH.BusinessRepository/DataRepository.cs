using HDBH.BusinessRepository.Interface;
using HDBH.Models.DatabaseModel;
using HDBH.SQLData;
using HDBH.SQLData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.BusinessRepository
{
    public class DataRepository : GenericRepository, IDataRepository
    {
        private RepositoryDbContext _dbContext;

        public DataRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }

        #region Override GenericRepository



        #endregion

        #region Overload GenericRepository



        #endregion


    }
}
