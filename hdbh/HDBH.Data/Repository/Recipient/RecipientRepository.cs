
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class RecipientRepository : BaseRepository<RecipientListModel>, IRecipientRepository
    {
        private BaseContext _dbContext;
        public RecipientRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
