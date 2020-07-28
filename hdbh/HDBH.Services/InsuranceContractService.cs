using HDBG.Log;
using HDBH.BusinessRepository.Interface;
using HDBH.Log.Enums;
using HDBH.Models;
using HDBH.Models.Constant;
using HDBH.Models.DatabaseModel;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel;
using HDBH.Models.ViewModel.InsuranceContractModel;
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
    public class InsuranceContractService : BaseService, IInsuranceContractService
    {

        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        private ICached _cached;

        #region STORE
        private string Store_getListContractType = "dbo.getListContractType";
        private string Store_getListExceptType = "dbo.getListExceptType";
        private string Store_getListContractForm = "dbo.getListContractForm";
        private string Store_searchInsuranceContract = "dbo.searchInsuranceContract";
        private string Store_getListCpProdForContract = "dbo.getListCpProdForContract";
        private string Store_getDetailCollateral = "dbo.getDetailCollateral";
        private string Store_searchInsuranceContractInau = "dbo.searchInsuranceContractInau";
        private string Store_getDetailInsurContract = "dbo.getDetailInsurContract";
        private string Store_insertInsurContractCollateral = "dbo.insertInsurContractCollateral";
        private string Store_updateInsurContractCollateral = "dbo.updateInsurContractCollateral";
        private string Store_approveInsurContract = "dbo.approveInsurContract";
        private string Store_approveInsurContractLv2 = "dbo.approveInsurContractLv2";
        private string Store_insertInsurContract = "dbo.insertInsurContract";
        private string Store_updateInsurContract = "dbo.updateInsurContract";
        private string Store_updateCloseInsurContract = "dbo.updateCloseInsurContract";
        #endregion

        public InsuranceContractService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository, ICached cached) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
            _cached = cached;
        }

        public CollateralItemModel getDetailCollateral(string collateralCode, string userId)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                      new SqlParameter{ ParameterName = "collateralCode", DbType = DbType.String, Value = string.IsNullOrEmpty(collateralCode) ? (object)DBNull.Value : collateralCode, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(userId) ? (object)DBNull.Value : userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getDetailCollateral, prms);
                var resultObject = JsonConvert.DeserializeObject<CollateralItemModel>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => getDetailCollateral", ex);
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
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListContractForm, prms);
                var resultObject = JsonConvert.DeserializeObject<List<InsuranceContractFormModel>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => getListContractType", ex);
                return null;
            }
        }

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
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListContractType, prms);
                var resultObject = JsonConvert.DeserializeObject<List<InsuranceContractTypeModel>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => getListContractType", ex);
                return null;
            }
        }

        public CPProductDetailModel getListCpProdForContract(long counterPartyId, string cifCounterParty, string counterPartyGroup, string dataDate, string userId)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                        new SqlParameter{ ParameterName = "counterPartyId", DbType = DbType.Int64, Value = counterPartyId, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "cifCounterParty", DbType = DbType.String, Value = string.IsNullOrEmpty(cifCounterParty) ? (object)DBNull.Value : cifCounterParty, Size = Int32.MaxValue  },
                       new SqlParameter{ ParameterName = "counterPartyGroup", DbType = DbType.String, Value = string.IsNullOrEmpty(counterPartyGroup) ? (object)DBNull.Value : counterPartyGroup, Size = Int32.MaxValue  },
                        new SqlParameter{ ParameterName = "dataDate", DbType = DbType.String, Value = string.IsNullOrEmpty(dataDate) ? (object)DBNull.Value : dataDate, Size = Int32.MaxValue  },
                         new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(userId) ? (object)DBNull.Value : userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListCpProdForContract, prms);
                var resultObject = JsonConvert.DeserializeObject<CPProductDetailModel>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => getListContractType", ex);
                return null;
            }
        }

        public List<InsuranceExceptTypeModel> getListExceptType(string insuranceContractType)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "insuranceContractType", DbType = DbType.String, Value = string.IsNullOrEmpty(insuranceContractType) ? (object)DBNull.Value : insuranceContractType, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListExceptType, prms);
                var resultObject = JsonConvert.DeserializeObject<List<InsuranceExceptTypeModel>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => getListExceptType", ex);
                return null;
            }
        }

        public ResultModel insertInsurContractCollateral(InsuranceContractCollateralInsertModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value =input.insuranceContractNo == null ? (object)DBNull.Value : input.insuranceContractNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractTypeCode", DbType = DbType.String, Value = input.contractTypeCode == null ? (object)DBNull.Value : input.contractTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractFormCode", DbType = DbType.String, Value = input.contractFormCode == null ? (object)DBNull.Value : input.contractFormCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceCertification", DbType = DbType.String,Value = input.insuranceCertification == null ? (object)DBNull.Value : input.insuranceCertification, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "refInsuranceNo", DbType = DbType.DateTime,Value = input.refInsuranceNo == null ? (object)DBNull.Value : input.refInsuranceNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "exceptionTypeCode", DbType = DbType.String, Value = input.exceptionTypeCode == null ? (object)DBNull.Value : input.exceptionTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerCif", DbType = DbType.String, Value = input.customerCif == null ? (object)DBNull.Value : input.customerCif, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerName", DbType = DbType.String, Value = input.customerName == null ? (object)DBNull.Value : input.customerName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "noncifCustomerName", DbType = DbType.String, Value = input.noncifCustomerName == null ? (object)DBNull.Value : input.noncifCustomerName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contactNumber", DbType = DbType.String, Value = input.contactNumber == null ? (object)DBNull.Value : input.contactNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "consultantOfficerCode", DbType = DbType.String, Value =input.consultantOfficerCode == null ? (object)DBNull.Value : input.consultantOfficerCode , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "consultantOfficerName", DbType = DbType.String, Value = input.consultantOfficerName == null ? (object)DBNull.Value : input.consultantOfficerName, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "consultantBranchCode", DbType = DbType.String, Value = input.consultantBranchCode == null ? (object)DBNull.Value : input.consultantBranchCode, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "isContractOcb", DbType = DbType.Int32, Value = input.isContractOcb, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceValue", DbType = DbType.Decimal, Value = input.insuranceValue , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "totalFeeAmount", DbType = DbType.Decimal, Value = input.totalFeeAmount, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "signedDate", DbType = DbType.Date, Value = input.signedDate == null ? (object)DBNull.Value : input.signedDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "effectiveDate", DbType = DbType.Date, Value = input.effectiveDate == null ? (object)DBNull.Value : input.effectiveDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "dueDate", DbType = DbType.Date, Value = input.dueDate == null ? (object)DBNull.Value : input.dueDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractDescription", DbType = DbType.String, Value = input.contractDescription == null ? (object)DBNull.Value : input.contractDescription, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = input.branchCode == null ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "collateralList", DbType = DbType.String, Value = input.jsonCollateralList == null ? (object)DBNull.Value : input.jsonCollateralList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "productList",  DbType = DbType.String, Value = input.jsonProductList == null ? (object)DBNull.Value : input.jsonProductList, Size = Int32.MaxValue   },
                     new SqlParameter{ ParameterName = "scheduleList",  DbType = DbType.String, Value = input.jsonScheduleList == null ? (object)DBNull.Value : input.jsonScheduleList, Size = Int32.MaxValue   },
                    new SqlParameter{ ParameterName = "paymentDetailList",  DbType = DbType.String, Value = input.jsonPaymentDetailList == null ? (object)DBNull.Value : input.jsonPaymentDetailList, Size = Int32.MaxValue   },
                    new SqlParameter{ ParameterName = "fileList",  DbType = DbType.String, Value = input.jsonFileList == null ? (object)DBNull.Value : input.jsonFileList, Size = Int32.MaxValue   },



                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_insertInsurContractCollateral, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => insertInsurContractCollateral", ex);
                return null;
            }
        }

        public ListInsuranceContractDetailViewModel searchInsuranceContract(SearchInsuranceContract input)
        {

            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "fromDate", DbType = DbType.Date, Value = input.fromDate == null ? (object)DBNull.Value: input.fromDate, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "toDate", DbType = DbType.Date, Value =  input.toDate == null ? (object)DBNull.Value: input.toDate , Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "isInau", DbType = DbType.Int32, Value = input.isInau, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.insuranceContractNo) ? (object)DBNull.Value : input.insuranceContractNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "refInsuranceNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.refInsuranceNo) ? (object)DBNull.Value : input.refInsuranceNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractTypeCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.contractTypeCode) ? (object)DBNull.Value : input.contractTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractFormCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.contractFormCode) ? (object)DBNull.Value : input.contractFormCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userUpdated", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userUpdated) ? (object)DBNull.Value : input.userUpdated, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "processStatusList", DbType = DbType.String, Value = string.IsNullOrEmpty(input.processStatusList) ? (object)DBNull.Value : input.processStatusList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerCif", DbType = DbType.String, Value = string.IsNullOrEmpty(input.customerCif) ? (object)DBNull.Value : input.customerCif, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "renewStatus", DbType = DbType.Int32, Value = input.renewStatus, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.branchCode) ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userId) ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = input.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = input.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var resultObject = new ListInsuranceContractDetailViewModel();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchInsuranceContract, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<ListInsuranceContractDetailViewModel>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => searchInsuranceContract", ex);
                return new ListInsuranceContractDetailViewModel();
            }
        }

        public ListInsuranceContractDetailViewModel searchInsuranceContractInau(SearchInsuranceContract input)
        {

            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "fromDate", DbType = DbType.Date, Value = input.fromDate == null ? (object)DBNull.Value: input.fromDate, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "toDate", DbType = DbType.Date, Value =  input.toDate == null ? (object)DBNull.Value: input.toDate , Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.insuranceContractNo) ? (object)DBNull.Value : input.insuranceContractNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "refInsuranceNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.refInsuranceNo) ? (object)DBNull.Value : input.refInsuranceNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractTypeCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.contractTypeCode) ? (object)DBNull.Value : input.contractTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractFormCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.contractFormCode) ? (object)DBNull.Value : input.contractFormCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userUpdated", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userUpdated) ? (object)DBNull.Value : input.userUpdated, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "processStatusList", DbType = DbType.String, Value = string.IsNullOrEmpty(input.processStatusList) ? (object)DBNull.Value : input.processStatusList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerCif", DbType = DbType.String, Value = string.IsNullOrEmpty(input.customerCif) ? (object)DBNull.Value : input.customerCif, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.branchCode) ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userId) ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = input.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = input.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var resultObject = new ListInsuranceContractDetailViewModel();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchInsuranceContractInau, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<ListInsuranceContractDetailViewModel>(result.errorMessage);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => searchInsuranceContractInauL1", ex);
                return new ListInsuranceContractDetailViewModel();
            }
        }


        public GetDetailInsuranceContractModel getDetailInsurContract(InputApproveInsuranceContractModel input)
        {

            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "insuranceId", DbType = DbType.Int64, Value = input.insuranceId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.insuranceContractNo) ? (object)DBNull.Value : input.insuranceContractNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userId) ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var resultObject = new GetDetailInsuranceContractModel();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getDetailInsurContract, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<GetDetailInsuranceContractModel>(result.errorMessage);
                }

                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => getDetailInsurContract", ex);
                return new GetDetailInsuranceContractModel();
            }
        }

        public ResultModel updateInsurContractCollateral(InsuranceContractCollateralInsertModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                     new SqlParameter{ ParameterName = "insuranceId", DbType = DbType.Int64, Value = input.insuranceId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value =input.insuranceContractNo == null ? (object)DBNull.Value : input.insuranceContractNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractTypeCode", DbType = DbType.String, Value = input.contractTypeCode == null ? (object)DBNull.Value : input.contractTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractFormCode", DbType = DbType.String, Value = input.contractFormCode == null ? (object)DBNull.Value : input.contractFormCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceCertification", DbType = DbType.String,Value = input.insuranceCertification == null ? (object)DBNull.Value : input.insuranceCertification, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "refInsuranceNo", DbType = DbType.DateTime,Value = input.refInsuranceNo == null ? (object)DBNull.Value : input.refInsuranceNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "exceptionTypeCode", DbType = DbType.String, Value = input.exceptionTypeCode == null ? (object)DBNull.Value : input.exceptionTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerCif", DbType = DbType.String, Value = input.customerCif == null ? (object)DBNull.Value : input.customerCif, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerName", DbType = DbType.String, Value = input.customerName == null ? (object)DBNull.Value : input.customerName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "noncifCustomerName", DbType = DbType.String, Value = input.noncifCustomerName == null ? (object)DBNull.Value : input.noncifCustomerName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contactNumber", DbType = DbType.String, Value = input.contactNumber == null ? (object)DBNull.Value : input.contactNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "consultantOfficerCode", DbType = DbType.String, Value =input.consultantOfficerCode == null ? (object)DBNull.Value : input.consultantOfficerCode , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "consultantOfficerName", DbType = DbType.String, Value = input.consultantOfficerName == null ? (object)DBNull.Value : input.consultantOfficerName, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "consultantBranchCode", DbType = DbType.String, Value = input.consultantBranchCode == null ? (object)DBNull.Value : input.consultantBranchCode, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "isContractOcb", DbType = DbType.Int32, Value = input.isContractOcb, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceValue", DbType = DbType.Decimal, Value = input.insuranceValue , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "totalFeeAmount", DbType = DbType.Decimal, Value = input.totalFeeAmount, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "signedDate", DbType = DbType.Date, Value = input.signedDate == null ? (object)DBNull.Value : input.signedDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "effectiveDate", DbType = DbType.Date, Value = input.effectiveDate == null ? (object)DBNull.Value : input.effectiveDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "dueDate", DbType = DbType.Date, Value = input.dueDate == null ? (object)DBNull.Value : input.dueDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractDescription", DbType = DbType.String, Value = input.contractDescription == null ? (object)DBNull.Value : input.contractDescription, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "processStatus", DbType = DbType.String, Value = input.processStatus == null ? (object)DBNull.Value : input.processStatus, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = input.branchCode == null ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "collateralList", DbType = DbType.String, Value = input.jsonCollateralList == null ? (object)DBNull.Value : input.jsonCollateralList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "productList",  DbType = DbType.String, Value = input.jsonProductList == null ? (object)DBNull.Value : input.jsonProductList, Size = Int32.MaxValue   },
                     new SqlParameter{ ParameterName = "scheduleList",  DbType = DbType.String, Value = input.jsonScheduleList == null ? (object)DBNull.Value : input.jsonScheduleList, Size = Int32.MaxValue   },
                    new SqlParameter{ ParameterName = "paymentDetailList",  DbType = DbType.String, Value = input.jsonPaymentDetailList == null ? (object)DBNull.Value : input.jsonPaymentDetailList, Size = Int32.MaxValue   },
                    new SqlParameter{ ParameterName = "fileList",  DbType = DbType.String, Value = input.jsonFileList == null ? (object)DBNull.Value : input.jsonFileList, Size = Int32.MaxValue   },



                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_updateInsurContractCollateral, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => updateInsurContractCollateral", ex);
                return null;
            }
        }

        public ResultModel approveInsurContract(long insuranceId, string approveStatus, string approveContent, string userId, string branchCode)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                     new SqlParameter{ ParameterName = "insuranceId", DbType = DbType.Int64, Value = insuranceId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "approveStatus", DbType = DbType.String, Value = approveStatus == null ? (object)DBNull.Value : approveStatus , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "approveContent", DbType = DbType.String, Value = approveContent == null ? (object)DBNull.Value : approveContent, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = userId == null ? (object)DBNull.Value : userId, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = branchCode == null ? (object)DBNull.Value : branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = new ResultModel();
                result = _Repository.ExecWithStoreProcedureCommand(Store_approveInsurContract, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => approveInsurContract", ex);
                return new ResultModel();
            }
        }

        public ResultModel approveInsurContractLv2(long insuranceId, string approveStatus, string approveContent, string userId, string branchCode)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                     new SqlParameter{ ParameterName = "insuranceId", DbType = DbType.Int64, Value = insuranceId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "approveStatus", DbType = DbType.String, Value = approveStatus == null ? (object)DBNull.Value : approveStatus , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "approveContent", DbType = DbType.String, Value = approveContent == null ? (object)DBNull.Value : approveContent, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = userId == null ? (object)DBNull.Value : userId, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = branchCode == null ? (object)DBNull.Value : branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_approveInsurContractLv2, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => approveInsurContractLv2", ex);
                return null;
            }
        }

        public ResultModel insertInsurContract(InsuranceContractCollateralInsertModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value =input.insuranceContractNo == null ? (object)DBNull.Value : input.insuranceContractNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractTypeCode", DbType = DbType.String, Value = input.contractTypeCode == null ? (object)DBNull.Value : input.contractTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractFormCode", DbType = DbType.String, Value = input.contractFormCode == null ? (object)DBNull.Value : input.contractFormCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceCertification", DbType = DbType.String,Value = input.insuranceCertification == null ? (object)DBNull.Value : input.insuranceCertification, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "refInsuranceNo", DbType = DbType.DateTime,Value = input.refInsuranceNo == null ? (object)DBNull.Value : input.refInsuranceNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "exceptionTypeCode", DbType = DbType.String, Value = input.exceptionTypeCode == null ? (object)DBNull.Value : input.exceptionTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerCif", DbType = DbType.String, Value = input.customerCif == null ? (object)DBNull.Value : input.customerCif, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerName", DbType = DbType.String, Value = input.customerName == null ? (object)DBNull.Value : input.customerName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "noncifCustomerName", DbType = DbType.String, Value = input.noncifCustomerName == null ? (object)DBNull.Value : input.noncifCustomerName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contactNumber", DbType = DbType.String, Value = input.contactNumber == null ? (object)DBNull.Value : input.contactNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "consultantOfficerCode", DbType = DbType.String, Value =input.consultantOfficerCode == null ? (object)DBNull.Value : input.consultantOfficerCode , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "consultantOfficerName", DbType = DbType.String, Value = input.consultantOfficerName == null ? (object)DBNull.Value : input.consultantOfficerName, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "consultantBranchCode", DbType = DbType.String, Value = input.consultantBranchCode == null ? (object)DBNull.Value : input.consultantBranchCode, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "isContractOcb", DbType = DbType.Int32, Value = input.isContractOcb, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceValue", DbType = DbType.Decimal, Value = input.insuranceValue , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "totalFeeAmount", DbType = DbType.Decimal, Value = input.totalFeeAmount, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "signedDate", DbType = DbType.Date, Value = input.signedDate == null ? (object)DBNull.Value : input.signedDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "effectiveDate", DbType = DbType.Date, Value = input.effectiveDate == null ? (object)DBNull.Value : input.effectiveDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "dueDate", DbType = DbType.Date, Value = input.dueDate == null ? (object)DBNull.Value : input.dueDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractDescription", DbType = DbType.String, Value = input.contractDescription == null ? (object)DBNull.Value : input.contractDescription, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = input.branchCode == null ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "productList",  DbType = DbType.String, Value = input.jsonProductList == null ? (object)DBNull.Value : input.jsonProductList, Size = Int32.MaxValue   },
                     new SqlParameter{ ParameterName = "scheduleList",  DbType = DbType.String, Value = input.jsonScheduleList == null ? (object)DBNull.Value : input.jsonScheduleList, Size = Int32.MaxValue   },
                    new SqlParameter{ ParameterName = "paymentDetailList",  DbType = DbType.String, Value = input.jsonPaymentDetailList == null ? (object)DBNull.Value : input.jsonPaymentDetailList, Size = Int32.MaxValue   },
                    new SqlParameter{ ParameterName = "fileList",  DbType = DbType.String, Value = input.jsonFileList == null ? (object)DBNull.Value : input.jsonFileList, Size = Int32.MaxValue   },



                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_insertInsurContract, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => insertInsurContract", ex);
                return null;
            }
        }

        public ResultModel updateInsurContract(InsuranceContractCollateralInsertModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "insuranceId", DbType = DbType.Int64, Value = input.insuranceId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value =input.insuranceContractNo == null ? (object)DBNull.Value : input.insuranceContractNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractTypeCode", DbType = DbType.String, Value = input.contractTypeCode == null ? (object)DBNull.Value : input.contractTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractFormCode", DbType = DbType.String, Value = input.contractFormCode == null ? (object)DBNull.Value : input.contractFormCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceCertification", DbType = DbType.String,Value = input.insuranceCertification == null ? (object)DBNull.Value : input.insuranceCertification, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "refInsuranceNo", DbType = DbType.DateTime,Value = input.refInsuranceNo == null ? (object)DBNull.Value : input.refInsuranceNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "exceptionTypeCode", DbType = DbType.String, Value = input.exceptionTypeCode == null ? (object)DBNull.Value : input.exceptionTypeCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerCif", DbType = DbType.String, Value = input.customerCif == null ? (object)DBNull.Value : input.customerCif, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerName", DbType = DbType.String, Value = input.customerName == null ? (object)DBNull.Value : input.customerName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "noncifCustomerName", DbType = DbType.String, Value = input.noncifCustomerName == null ? (object)DBNull.Value : input.noncifCustomerName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contactNumber", DbType = DbType.String, Value = input.contactNumber == null ? (object)DBNull.Value : input.contactNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "consultantOfficerCode", DbType = DbType.String, Value =input.consultantOfficerCode == null ? (object)DBNull.Value : input.consultantOfficerCode , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "consultantOfficerName", DbType = DbType.String, Value = input.consultantOfficerName == null ? (object)DBNull.Value : input.consultantOfficerName, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "consultantBranchCode", DbType = DbType.String, Value = input.consultantBranchCode == null ? (object)DBNull.Value : input.consultantBranchCode, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "isContractOcb", DbType = DbType.Int32, Value = input.isContractOcb, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceValue", DbType = DbType.Decimal, Value = input.insuranceValue , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "totalFeeAmount", DbType = DbType.Decimal, Value = input.totalFeeAmount, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "signedDate", DbType = DbType.Date, Value = input.signedDate == null ? (object)DBNull.Value : input.signedDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "effectiveDate", DbType = DbType.Date, Value = input.effectiveDate == null ? (object)DBNull.Value : input.effectiveDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "dueDate", DbType = DbType.Date, Value = input.dueDate == null ? (object)DBNull.Value : input.dueDate, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "contractDescription", DbType = DbType.String, Value = input.contractDescription == null ? (object)DBNull.Value : input.contractDescription, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = input.branchCode == null ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "productList",  DbType = DbType.String, Value = input.jsonProductList == null ? (object)DBNull.Value : input.jsonProductList, Size = Int32.MaxValue   },
                     new SqlParameter{ ParameterName = "scheduleList",  DbType = DbType.String, Value = input.jsonScheduleList == null ? (object)DBNull.Value : input.jsonScheduleList, Size = Int32.MaxValue   },
                    new SqlParameter{ ParameterName = "paymentDetailList",  DbType = DbType.String, Value = input.jsonPaymentDetailList == null ? (object)DBNull.Value : input.jsonPaymentDetailList, Size = Int32.MaxValue   },
                    new SqlParameter{ ParameterName = "fileList",  DbType = DbType.String, Value = input.jsonFileList == null ? (object)DBNull.Value : input.jsonFileList, Size = Int32.MaxValue   },



                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_updateInsurContract, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => updateInsurContract", ex);
                return null;
            }
        }

        public ResultModel updateCloseInsurContract(string insuranceId, string insuranceContractNo, string userId, string branchCode)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "insuranceId", DbType = DbType.Int64, Value = insuranceId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value =insuranceContractNo == null ? (object)DBNull.Value : insuranceContractNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = userId == null ? (object)DBNull.Value : userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = branchCode == null ? (object)DBNull.Value : branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_updateCloseInsurContract, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("InsuranceContractService => updateCloseInsurContract", ex);
                return null;
            }
        }
    }
}
