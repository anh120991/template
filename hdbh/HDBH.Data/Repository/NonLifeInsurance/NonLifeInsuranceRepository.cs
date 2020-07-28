
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class NonLifeInsuranceRepository : BaseRepository<NonLifeContractListModel>, INonLifeInsuranceRepository
    {
        private BaseContext _dbContext;
        public NonLifeInsuranceRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
