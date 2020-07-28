using HDBH.Data.Infrastructure;
using HDBH.Data.Interface;
using HDBH.Models;

namespace HDBH.Data.Repository
{
    public class UserRepository : BaseRepository<PhanQuyenUserModel>, IUserRepository
    {
        private BaseContext _dbContext;
        public UserRepository(BaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #region Override GenericRepository



        #endregion

        #region Overload GenericRepository

        public bool Update(PhanQuyenUserModel t)
        {
            //if (t == null)
            //    return false;
            //UserModel result = Update(t, t.Id);
            //if (result == null)
            //    return false;
            return true;
        }

        #endregion

    }


}
