using System;
using System.Collections.Generic;
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class UserLoginRepository : BaseRepository<UserLoginModel>, IUserLoginRepository
    {
        private BaseContext _dbContext;
        public UserLoginRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
