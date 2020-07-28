
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class RoleListRepository : BaseRepository<RoleListModel>, IRoleListRepository
    {
        private BaseContext _dbContext;
        public RoleListRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
