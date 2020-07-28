using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class ContractTypeRepository : BaseRepository<ContractTypeModel>, IContractTypeRepository
    {
        private BaseContext _dbContext;
        public ContractTypeRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
