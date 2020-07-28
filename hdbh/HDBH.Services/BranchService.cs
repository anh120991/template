using HDBG.Log;
using HDBH.BusinessRepository.Interface;
using HDBH.Services.Interface;
using HDBH.SQLData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using HDBH.Models.DatabaseModel;

namespace HDBH.Services
{
    public class BranchService : BaseService, IBranchService
    {
        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        private ICached _cached;

        #region STORE
        private string Store_getListBranch = "dbo.getListBranch";
        #endregion
        public BranchService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository, ICached cached) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
            _cached = cached;
        }

        public List<BranchDetailModel> getListBranch(string branchCode)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = string.IsNullOrEmpty(branchCode) ? (object)DBNull.Value : branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListBranch, prms);
                List<BranchDetailModel> resultObject = new List<BranchDetailModel>();
                resultObject = JsonConvert.DeserializeObject<List<BranchDetailModel>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("BranchService => getListBranch", ex);
                return null;
            }
        }



    }
}
