using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class ProductCounterPartyRepository : BaseRepository<ProductCounterPartyModel>, IProductCounterPartyRepository
    {
        private BaseContext _dbContext;
        public ProductCounterPartyRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
