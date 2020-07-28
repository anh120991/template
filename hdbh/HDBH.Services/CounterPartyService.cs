using HDBG.Log;
using HDBH.BusinessRepository.Interface;
using HDBH.Log.Enums;
using HDBH.Models.Constant;
using HDBH.Models.DatabaseModel;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel;
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
    public class CounterPartyService : BaseService, ICounterPartyService
    {

        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        private ICached _cached;

        #region STORE
        private string Store_GetList = "dbo.getListCounterPartyGroup";
        private string Store_insertCounterParty = "dbo.insertCounterParty";
        private string Store_searchCounterParty = "dbo.searchCounterParty";
        private string Store_getDetailCounterParty = "dbo.getDetailCounterParty";
        private string Store_updateCounterParty = "dbo.updateCounterParty";
        private string Store_getListCounterParty = "dbo.getListCounterParty";

        #endregion

        public CounterPartyService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository, ICached cached) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
            _cached = cached;
        }

        public CounterPartyDetailModel getDetailCounterParty(CounterPartyGetDetailModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
               {
                     new SqlParameter{ ParameterName = "counterPartyId", DbType = DbType.Int64, Value = input.counterPartyId, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "cifCounterParty", DbType = DbType.String, Value = string.IsNullOrEmpty(input.cifCounterParty) ? (object)DBNull.Value : input.cifCounterParty, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = string.IsNullOrEmpty(input.counterPartyGroup) ? (object)DBNull.Value : input.counterPartyGroup, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "isInau", DbType = DbType.Int32, Value = input.isInau, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
               };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getDetailCounterParty, prms);
                var resultObject = JsonConvert.DeserializeObject<CounterPartyDetailModel>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CounterPartyService => getDetailCounterParty", ex);
                return null;
            }
        }

        public List<CounterPartySelectItemModel> getListCounterParty(string cpGroupCode, string userId)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
               {
                     new SqlParameter{ ParameterName = "cpGroupCode", DbType = DbType.String, Value = string.IsNullOrEmpty(cpGroupCode) ? (object)DBNull.Value : cpGroupCode, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = userId == null ? (object)DBNull.Value : userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
               };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListCounterParty, prms);
                var resultObject = JsonConvert.DeserializeObject<List<CounterPartySelectItemModel>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CounterPartyService => getListCounterParty", ex);
                return null;
            }
        }

        public List<CounterPartyGroupModel> getListCounterPartyGroup()
        {
            try
            {
                var rsCache = _cached.Get<List<CounterPartyGroupModel>>(CachedKey.counterPartyGroupList);
                if (rsCache != null)
                {
                    return (List<CounterPartyGroupModel>)rsCache;
                }
                else
                {
                    SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                    var result = _Repository.ExecWithStoreProcedureCommand(Store_GetList, prms);
                    var resultObject = JsonConvert.DeserializeObject<List<CounterPartyGroupModel>>(result.errorMessage);
                    _cached.Set(CacheModeEnum.Add, CachedKey.counterPartyGroupList, resultObject, new TimeSpan(1, 0, 0, 0));
                    return resultObject;
                }
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CounterPartyService => getListCounterPartyGroup", ex);
                return null;
            }
        }

        public ResultModel insertCounterParty(CounterPartyDetailInsertModel model)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "counterPartyName", DbType = DbType.String, Value =model.counterPartyName == null ? (object)DBNull.Value : model.counterPartyName , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "cifCounterParty", DbType = DbType.String, Value = model.cifCounterParty == null ? (object)DBNull.Value : model.cifCounterParty, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = model.counterPartyGroup, Size = Int32.MaxValue  },
                       new SqlParameter{ ParameterName = "shortName", DbType = DbType.String, Value = model.shortName, Size = Int32.MaxValue  },
                        new SqlParameter{ ParameterName = "signedContractDate", DbType = DbType.DateTime, Value = model.signedContractDate , Size = Int32.MaxValue  },
                         new SqlParameter{ ParameterName = "paymentAccount", DbType = DbType.String, Value = model.paymentAccount, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "counterPartyDesc", DbType = DbType.String, Value = model.counterPartyDesc == null ? (object)DBNull.Value : model.counterPartyDesc, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "counterPartyStatus", DbType = DbType.String, Value = model.counterPartyStatus, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_insertCounterParty, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CounterPartyService => insertCounterParty", ex);
                return null;
            }
        }

        public CounterPartySearchModel searchCounterParty(CounterPartyListSeachModel model)
        {
            try
            {
                CounterPartySearchModel resultObject = new CounterPartySearchModel();
                SqlParameter[] prms = new SqlParameter[]
               {
                    new SqlParameter{ ParameterName = "fromDate", DbType = DbType.Date, Value =  model.fromDate == null ? (object)DBNull.Value : model.fromDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "toDate", DbType = DbType.Date, Value =  model.toDate == null ? (object)DBNull.Value : model.toDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "cif", DbType = DbType.String, Value =  model.cif == null ? (object)DBNull.Value : model.cif , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "shortName", DbType = DbType.String, Value =  model.shortName == null ? (object)DBNull.Value : model.shortName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "cpGroupCode", DbType = DbType.String, Value =  model.cpGroupCode == null ? (object)DBNull.Value : model.cpGroupCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userUpdated", DbType = DbType.String, Value =  model.userUpdated == null ? (object)DBNull.Value : model.userUpdated, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "status", DbType = DbType.String, Value =  model.status == null ? (object)DBNull.Value : model.status, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = model.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = model.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
               };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchCounterParty, prms);
                if(result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<CounterPartySearchModel>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CounterPartyService => searchCounterParty", ex);
                return null;
            }
        }

        public ResultModel updateCounterParty(CounterPartyDetailInsertModel model)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "counterPartyId", DbType = DbType.Int64, Value =model.counterPartyId , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "counterPartyName", DbType = DbType.String, Value =model.counterPartyName == null ? (object)DBNull.Value : model.counterPartyName , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "cifCounterParty", DbType = DbType.String, Value = model.cifCounterParty == null ? (object)DBNull.Value : model.cifCounterParty, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = model.counterPartyGroup, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "shortName", DbType = DbType.String, Value = model.shortName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "signedContractDate", DbType = DbType.DateTime, Value = model.signedContractDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "paymentAccount", DbType = DbType.String, Value = model.paymentAccount, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "counterPartyDesc", DbType = DbType.String, Value = model.counterPartyDesc, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "counterPartyStatus", DbType = DbType.String, Value = model.counterPartyStatus, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_updateCounterParty, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CounterPartyService => updateCounterParty", ex);
                return null;
            }
        }
    }
}
