
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class InsuranceInauRepository : BaseRepository<InsuranceInauListModel>, IInsuranceInauRepository
    {
        private BaseContext _dbContext;
        public InsuranceInauRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
