using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class ExpectTypeRepository : BaseRepository<ExpectTypeModel>, IExpectTypeRepository
    {
        private BaseContext _dbContext;
        public ExpectTypeRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
