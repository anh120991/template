using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;
using HDBH.Models.HelperModel;

namespace HDBH.Data.Repository
{


    public class ActionReturnRepository : BaseRepository<ActionAttributeReturn>, IActionReturnRepository
    {
        private BaseContext _dbContext;
        public ActionReturnRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
