using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class CounterPartyListRepository : BaseRepository<CounterPartyItemListModel>, ICounterPartyListRepository
    {
        private BaseContext _dbContext;
        public CounterPartyListRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
