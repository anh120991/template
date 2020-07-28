
using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{


    public class ProductRepository : BaseRepository<ProductModel>, IProductRepository
    {
        private BaseContext _dbContext;
        public ProductRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
