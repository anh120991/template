using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class InsuranceContractListRepository : BaseRepository<InsuranceContractListModel>, IInsuranceContractListRepository
    {
        private BaseContext _dbContext;
        public InsuranceContractListRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
