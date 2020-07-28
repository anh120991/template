
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class RecipientDetailRepository : BaseRepository<RecipientDetailModel>, IRecipientDetailRepository
    {
        private BaseContext _dbContext;
        public RecipientDetailRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
