using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class ContractFormRepository : BaseRepository<ContractFormModel>, IContractFormRepository
    {
        private BaseContext _dbContext;
        public ContractFormRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
