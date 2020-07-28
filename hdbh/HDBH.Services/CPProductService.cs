using HDBG.Log;
using HDBH.BusinessRepository.Interface;
using HDBH.Log.Enums;
using HDBH.Models;
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
    public class CPProductService : BaseService, ICPProductService
    {

        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        private ICached _cached;

        #region STORE
        private string Store_insertCpProdCommis = "dbo.insertCpProdCommis";
        private string Store_insertCpUncontractProd = "dbo.insertCpUncontractProd ";
        private string Store_searchCpProdCommis = "dbo.searchCpProdCommis";
        private string Store_getDetailCpProdCommis = "dbo.getDetailCpProdCommis";

        private string Store_updateCpProdCommis = "dbo.updateCpProdCommis";
        private string Store_updateCpUncontractProd = "dbo.updateCpUncontractProd";
        private string Store_searchCpProdCommisInau = "dbo.searchCpProdCommisInau";

        private string Store_approveCpProdCommisu = "dbo.approveCpProdCommis";
        private string Store_getListCpProdForContract = "dbo.getListCpProdForContract";
        private string Store_deleteCpProdCommis = "dbo.deleteCpProdCommis";

        #endregion

        public CPProductService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository, ICached cached) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
            _cached = cached;
        }

        public ResultModel approveCpProdCommis(CPProductApproveModel model)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ ParameterName = "counterPartyId ", DbType = DbType.Int64, Value =model.counterPartyId  , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = model.counterPartyGroup == null ? (object)DBNull.Value : model.counterPartyGroup, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "approveStatus", DbType = DbType.String,Value = model.approveStatus == null ? (object)DBNull.Value : model.approveStatus , Size = Int32.MaxValue  },
                        new SqlParameter{ ParameterName = "approveContent", DbType = DbType.String,Value = model.approveContent == null ? (object)DBNull.Value : model.approveContent , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                    };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_approveCpProdCommisu, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CPProductService => approveCpProdCommis", ex);
                return null;
            }
        }

        public ResultModel deleteCpProdCommis(long counterPartyId, string counterPartyGroup, string status, string userId, string branchCode)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ ParameterName = "counterPartyId ", DbType = DbType.Int64, Value =counterPartyId  , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = counterPartyGroup == null ? (object)DBNull.Value : counterPartyGroup, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "status", DbType = DbType.String,Value = status == null ? (object)DBNull.Value : status , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                    };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_deleteCpProdCommis, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CPProductService => deleteCpProdCommis", ex);
                return null;
            }
        }

        public CPProductDetailModel getDetailCpProdCommis(CPProductGetDetailViewModel input)
        {
            try
            {
                CPProductDetailModel resultObject = new CPProductDetailModel();
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
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getDetailCpProdCommis, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<CPProductDetailModel>(result.errorMessage);
                }
                else
                {
                    HDBH.Log.WriteLog.Error("CPProductService => getDetailCpProdCommis => Detail" + result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CPProductService => getDetailCpProdCommis", ex);
                return null;
            }
        }

        public CPProductDetailModel getListCpProdForContract(long counterPartyId, string cifCounterParty, string counterPartyGroup, DateTime? dataDate, string userId)
        {
            try
            {
                CPProductDetailModel resultObject = new CPProductDetailModel();
                SqlParameter[] prms = new SqlParameter[]
               {
                     new SqlParameter{ ParameterName = "counterPartyId", DbType = DbType.Int64, Value = counterPartyId, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "cifCounterParty", DbType = DbType.String, Value = string.IsNullOrEmpty(cifCounterParty) ? (object)DBNull.Value : cifCounterParty, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = string.IsNullOrEmpty(counterPartyGroup) ? (object)DBNull.Value : counterPartyGroup, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "dataDate",  DbType = DbType.Date, Value = dataDate != null ? (object)DBNull.Value : dataDate, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = userId == null ? (object)DBNull.Value : userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
               };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListCpProdForContract, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<CPProductDetailModel>(result.errorMessage);
                }
                else
                {
                    HDBH.Log.WriteLog.Error("CPProductService => Store_getListCpProdForContract => Detail" + result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CPProductService => Store_getListCpProdForContract", ex);
                return null;
            }
        }

        public ResultModel insertCpProdCommis(CPProductInsertModel model)
        {
            try
            {
                ResultModel result = new ResultModel();
                if (model.counterPartyGroup.ToUpper() == "UNCONTRACT")
                {
                    SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "counterPartyId ", DbType = DbType.Int64, Value =model.counterPartyId  , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = model.counterPartyGroup == null ? (object)DBNull.Value : model.counterPartyGroup, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "status", DbType = DbType.String,Value = model.counterPartyStatus == null ? (object)DBNull.Value : model.counterPartyStatus , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "productList", DbType = DbType.String, Value = model.jsonProductList == null ? (object)DBNull.Value : model.jsonProductList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fileList", DbType = DbType.String, Value = model.jsonFileList == null ? (object)DBNull.Value : model.jsonFileList, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                     };
                    result = _Repository.ExecWithStoreProcedureCommand(Store_insertCpUncontractProd, prms);
                }
                else
                {
                    SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "counterPartyId ", DbType = DbType.Int64, Value =model.counterPartyId  , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = model.counterPartyGroup == null ? (object)DBNull.Value : model.counterPartyGroup, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "status", DbType = DbType.String,Value = model.counterPartyStatus == null ? (object)DBNull.Value : model.counterPartyStatus , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "productList", DbType = DbType.String, Value = model.jsonProductList == null ? (object)DBNull.Value : model.jsonProductList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "commisionList", DbType = DbType.String, Value = model.jsonCommisionList == null ? (object)DBNull.Value : model.jsonCommisionList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fileList", DbType = DbType.String, Value = model.jsonFileList == null ? (object)DBNull.Value : model.jsonFileList, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                    result = _Repository.ExecWithStoreProcedureCommand(Store_insertCpProdCommis, prms);
                }
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CPProductService => insertCpProdCommis", ex);
                return null;
            }
        }



        public CPProductSearchModel searchCpProdCommis(CPProductSearchViewModel model)
        {
            try
            {
                CPProductSearchModel resultObject = new CPProductSearchModel();
                SqlParameter[] prms = new SqlParameter[]
               {
                    new SqlParameter{ ParameterName = "fromDate", DbType = DbType.Date, Value =  model.fromDate == null ? (object)DBNull.Value : model.fromDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "toDate", DbType = DbType.Date, Value =  model.toDate == null ? (object)DBNull.Value : model.toDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "cif", DbType = DbType.String, Value =  model.cif == null ? (object)DBNull.Value : model.cif , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "shortName", DbType = DbType.String, Value =  model.shortName == null ? (object)DBNull.Value : model.shortName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "cpGroupCode", DbType = DbType.String, Value =  model.cpGroupCode == null ? (object)DBNull.Value : model.cpGroupCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userUpdated", DbType = DbType.String, Value =  model.userUpdated == null ? (object)DBNull.Value : model.userUpdated, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "statusList", DbType = DbType.String, Value =  model.statusList == null ? (object)DBNull.Value : model.statusList, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value =  model.branchCode == null ? (object)DBNull.Value : model.branchCode, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "isInau", DbType = DbType.String, Value =  model.isInau, Size = Int32.MaxValue  },
                        new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value =  model.userId == null ? (object)DBNull.Value : model.userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = model.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = model.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
               };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchCpProdCommis, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<CPProductSearchModel>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CPProductService => searchCpProdCommis", ex);
                return null;
            }
        }

        public CPProductSearchModel searchCpProdCommisInau(CPProductSearchViewModel model)
        {
            try
            {
                CPProductSearchModel resultObject = new CPProductSearchModel();
                SqlParameter[] prms = new SqlParameter[]
               {
                    new SqlParameter{ ParameterName = "fromDate", DbType = DbType.Date, Value =  model.fromDate == null ? (object)DBNull.Value : model.fromDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "toDate", DbType = DbType.Date, Value =  model.toDate == null ? (object)DBNull.Value : model.toDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "cif", DbType = DbType.String, Value =  model.cif == null ? (object)DBNull.Value : model.cif , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "shortName", DbType = DbType.String, Value =  model.shortName == null ? (object)DBNull.Value : model.shortName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "cpGroupCode", DbType = DbType.String, Value =  model.cpGroupCode == null ? (object)DBNull.Value : model.cpGroupCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userUpdated", DbType = DbType.String, Value =  model.userUpdated == null ? (object)DBNull.Value : model.userUpdated, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value =  model.branchCode == null ? (object)DBNull.Value : model.branchCode, Size = Int32.MaxValue  },
                        new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value =  model.userId == null ? (object)DBNull.Value : model.userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = model.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = model.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
               };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchCpProdCommisInau, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<CPProductSearchModel>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CPProductService => searchCpProdCommisInau", ex);
                return null;
            }
        }

        public ResultModel updateCpProdCommis(CPProductInsertModel model)
        {
            try
            {
                ResultModel result = new ResultModel();
                if (model.counterPartyGroup.ToUpper() == "UNCONTRACT")
                {
                    SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "counterPartyId ", DbType = DbType.Int64, Value =model.counterPartyId  , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = model.counterPartyGroup == null ? (object)DBNull.Value : model.counterPartyGroup, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "status", DbType = DbType.String,Value = model.counterPartyStatus == null ? (object)DBNull.Value : model.counterPartyStatus , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "productList", DbType = DbType.String, Value = model.jsonProductList == null ? (object)DBNull.Value : model.jsonProductList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fileList", DbType = DbType.String, Value = model.jsonFileList == null ? (object)DBNull.Value : model.jsonFileList, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                     };
                    result = _Repository.ExecWithStoreProcedureCommand(Store_updateCpUncontractProd, prms);
                }
                else
                {
                    SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "counterPartyId ", DbType = DbType.Int64, Value =model.counterPartyId  , Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = model.counterPartyGroup == null ? (object)DBNull.Value : model.counterPartyGroup, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "status", DbType = DbType.String,Value = model.counterPartyStatus == null ? (object)DBNull.Value : model.counterPartyStatus , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "isInau ", DbType = DbType.Int64, Value =model.isInau  , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "productList", DbType = DbType.String, Value = model.jsonProductList == null ? (object)DBNull.Value : model.jsonProductList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "commisionList", DbType = DbType.String, Value = model.jsonCommisionList == null ? (object)DBNull.Value : model.jsonCommisionList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fileList", DbType = DbType.String, Value = model.jsonFileList == null ? (object)DBNull.Value : model.jsonFileList, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                    result = _Repository.ExecWithStoreProcedureCommand(Store_updateCpProdCommis, prms);
                }
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("CPProductService => updateCpProdCommis", ex);
                return null;
            }
        }
    }
}
