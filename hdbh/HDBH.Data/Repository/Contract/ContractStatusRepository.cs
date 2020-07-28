using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{
    public class ContractStatusRepository : BaseRepository<ContractStatusModel>, IContractStatusRepository
    {
        private BaseContext _dbContext;
        public ContractStatusRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }



    }


}
