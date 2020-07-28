using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class UserListRepository : BaseRepository<UserPermissionModel>, IUserListRepository
    {
        private BaseContext _dbContext;
        public UserListRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
