
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class RecipientInauRepository : BaseRepository<RecipientInauListModel>, IRecipientInauRepository
    {
        private BaseContext _dbContext;
        public RecipientInauRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
