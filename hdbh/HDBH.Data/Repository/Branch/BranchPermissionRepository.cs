using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class BranchPermissionRepository : BaseRepository<BranchPermissionModel>, IBranchPermissionRepository
    {
        private BaseContext _dbContext;
        public BranchPermissionRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
