
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class ModuleListRepository : BaseRepository<ModuleListModel>, IModuleListRepository
    {
        private BaseContext _dbContext;
        public ModuleListRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
