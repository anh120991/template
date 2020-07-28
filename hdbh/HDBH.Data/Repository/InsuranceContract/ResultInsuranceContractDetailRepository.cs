using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{
    public class ResultInsuranceContractDetailRepository : BaseRepository<ResultInsuranceContractDetailModel>, IResultInsuranceContractDetailRepository
    {
        private BaseContext _dbContext;
        public ResultInsuranceContractDetailRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
