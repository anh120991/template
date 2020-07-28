using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class CounterPartyRepository : BaseRepository<CounterPartyModel>, ICounterPartyRepository
    {
        private BaseContext _dbContext;
        public CounterPartyRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
