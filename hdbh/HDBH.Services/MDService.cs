using HDBG.Log;
using HDBH.BusinessRepository.Interface;
using HDBH.Models.DatabaseModel;
using HDBH.Services.Interface;
using HDBH.SQLData.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Services
{
    public class MDService : BaseService, IMDService
    {
        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        private ICached _cached;


        public MDService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository, ICached cached) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
            _cached = cached;
        }

        #region STORE
        private string Store_getListContractType = "dbo.getListContractType";
        private string Store_getListContractForm = "dbo.getListContractForm";
        private string Store_getListStatus = "dbo.getListStatus";
        #endregion

        public List<InsuranceContractTypeModel> getListContractType(string contractTypeCode, int isNonlifeInsurance)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ ParameterName = "contractTypeCode", DbType = DbType.String, Value = string.IsNullOrEmpty(contractTypeCode) ? (object)DBNull.Value : contractTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "isNonlifeInsurance", DbType = DbType.Int64, Value = isNonlifeInsurance, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                List<InsuranceContractTypeModel> resultObject = new List<InsuranceContractTypeModel>();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListContractType, prms);
                resultObject = JsonConvert.DeserializeObject<List<InsuranceContractTypeModel>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("MDService => getListContractType", ex);
                return null;
            }
        }

        public List<InsuranceContractFormModel> getListContractForm(string contractTypeCode, int isNonlifeInsurance)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ ParameterName = "contractTypeCode", DbType = DbType.String, Value = string.IsNullOrEmpty(contractTypeCode) ? (object)DBNull.Value : contractTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "isNonlifeInsurance", DbType = DbType.Int64, Value = isNonlifeInsurance, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                List<InsuranceContractFormModel> resultObject = new List<InsuranceContractFormModel>();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListContractForm, prms);
                resultObject = JsonConvert.DeserializeObject<List<InsuranceContractFormModel>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("MDService => getListContractForm", ex);
                return null;
            }
        }

        public List<MDProcessStatus> getListStatus(int isStatusInau)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "isStatusInau", DbType = DbType.Int64, Value = isStatusInau, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListStatus, prms);
                var resultObject = JsonConvert.DeserializeObject<List<MDProcessStatus>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("MDService => getListStatus", ex);
                return null;
            }
        }
    }


}
