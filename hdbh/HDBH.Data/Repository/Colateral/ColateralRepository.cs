
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class ColateralRepository : BaseRepository<ColateralModel>, IColateralRepository
    {
        private BaseContext _dbContext;
        public ColateralRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
