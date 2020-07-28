using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;


namespace HDBH.Data.Repository
{


    public class PaymentScheduleRepository : BaseRepository<PaymentScheduleModel>, IPaymentScheduleRepository
    {
        private BaseContext _dbContext;
        public PaymentScheduleRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
