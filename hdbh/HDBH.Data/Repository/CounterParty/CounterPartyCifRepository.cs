using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class CounterPartyCifRepository : BaseRepository<CounterPartyCifModel>, ICounterPartyCifRepository
    {
        private BaseContext _dbContext;
        public CounterPartyCifRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
