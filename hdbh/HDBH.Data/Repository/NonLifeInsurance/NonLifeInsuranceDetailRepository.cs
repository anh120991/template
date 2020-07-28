
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class NonLifeInsuranceDetailRepository : BaseRepository<NonLifeInsuranceDetailModel>, INonLifeInsuranceDetailRepository
    {
        private BaseContext _dbContext;
        public NonLifeInsuranceDetailRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
