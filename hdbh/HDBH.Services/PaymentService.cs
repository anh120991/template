using HDBG.Log;
using HDBH.BusinessRepository.Interface;
using HDBH.Models.DatabaseModel.Payment;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel.Payment;
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
    public class PaymentService : BaseService, IPaymentService
    {
        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        private ICached _cached;

        #region STORE
        private string Store_searchRepayment = "dbo.searchRepayment";
        private string Store_searchRepaymentInau = "dbo.searchRepaymentInau";
        private string Store_searchRepaymentSchedule = "dbo.searchRepaymentSchedule";
        private string Store_getDetailRepayment = "dbo.getDetailRepayment";
        private string Store_approveRepayment = "dbo.approveRepayment";
        private string Store_insertRepayment = "dbo.insertRepayment";
        private string Store_updateRepayment = "dbo.updateRepayment";
        private string Store_getListScheduleUnpaid = "dbo.getListScheduleUnpaid";
        #endregion

        public PaymentService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository, ICached cached) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
            _cached = cached;
        }

        /// <summary>
        /// Màn hình Quản lý danh sách phiếu thu
        /// dutp
        /// 21/7
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ResultSearchListPayment searchRepayment(SearchPaymentModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "isInau", DbType = DbType.Int32, Value = input.isInau, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fromDate", DbType = DbType.Date, Value = input.fromDate == null ? (object)DBNull.Value: input.fromDate, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "toDate", DbType = DbType.Date, Value =  input.toDate == null ? (object)DBNull.Value: input.toDate , Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.insuranceContractNo) ? (object)DBNull.Value : input.insuranceContractNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "ftttNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.ftttNo) ? (object)DBNull.Value : input.ftttNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userUpdated", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userUpdated) ? (object)DBNull.Value : input.userUpdated, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "externalRepayment", DbType = DbType.Int32, Value = input.externalRepayment, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "processStatusList", DbType = DbType.String, Value = string.IsNullOrEmpty(input.processStatusList) ? (object)DBNull.Value : input.processStatusList, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.branchCode) ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userId) ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = input.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = input.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var resultObject = new ResultSearchListPayment();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchRepayment, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<ResultSearchListPayment>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("PaymentService => searchRepayment", ex);
                return new ResultSearchListPayment();
            }
        }

        /// <summary>
        /// Màn hình Quản lý danh sách phiếu thu chờ duyệt
        /// dutp
        /// 21/7/20
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ResultSearchListPayment searchRepaymentInau(SearchPaymentModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "fromDate", DbType = DbType.Date, Value = input.fromDate == null ? (object)DBNull.Value: input.fromDate, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "toDate", DbType = DbType.Date, Value =  input.toDate == null ? (object)DBNull.Value: input.toDate , Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.insuranceContractNo) ? (object)DBNull.Value : input.insuranceContractNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "ftttNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.ftttNo) ? (object)DBNull.Value : input.ftttNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userUpdated", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userUpdated) ? (object)DBNull.Value : input.userUpdated, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "externalRepayment", DbType = DbType.Int32, Value = input.externalRepayment, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.branchCode) ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userId) ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = input.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = input.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };

                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchRepaymentInau, prms);
                var resultObject = JsonConvert.DeserializeObject<ResultSearchListPayment>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("PaymentService => searchRepaymentInau", ex);
                return new ResultSearchListPayment();
            }
        }

        /// <summary>
        /// Truy vấn tình hình thu phí của hợp đồng
        /// dutp
        /// 22/7/20
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ResultSearchListRepaymentSchedule searchRepaymentSchedule(SearchRepaymentSchedule input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "fromDate", DbType = DbType.Date, Value = input.fromDate == null ? (object)DBNull.Value: input.fromDate, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "toDate", DbType = DbType.Date, Value =  input.toDate == null ? (object)DBNull.Value: input.toDate , Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.insuranceContractNo) ? (object)DBNull.Value : input.insuranceContractNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "customerCif", DbType = DbType.String, Value = string.IsNullOrEmpty(input.customerCif) ? (object)DBNull.Value : input.customerCif, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "paymentStatus", DbType = DbType.String, Value = string.IsNullOrEmpty(input.paymentStatus) ? (object)DBNull.Value : input.paymentStatus, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = string.IsNullOrEmpty(input.branchCode) ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userId) ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = input.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = input.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var resultObject = new ResultSearchListRepaymentSchedule();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchRepaymentSchedule, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<ResultSearchListRepaymentSchedule>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("PaymentService => searchRepaymentSchedule", ex);
                return new ResultSearchListRepaymentSchedule();
            }
        }

        /// <summary>
        /// Chi tiết thông tin thanh toán 
        /// dutp
        /// 22/7/20
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetPaymentDetailModel getDetailRepayment(InputGetDetailPaymentApprove input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "repaymentId", DbType = DbType.Int32, Value = input.repaymentId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userId) ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var resultObject = new GetPaymentDetailModel();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getDetailRepayment, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<GetPaymentDetailModel>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("PaymentService => getDetailRepayment", ex);
                return new GetPaymentDetailModel();
            }
        }


        ////// <summary>
        ///Duyệt thông tin thu phí BH
        /// dutp
        /// 22/7/20
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public ResultModel approveRepayment(ApprovePaymentModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "repaymentId", DbType = DbType.Int64, Value = input.repaymentId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "approveStatus", DbType = DbType.String, Value = input.approveStatus == null ? (object)DBNull.Value : input.approveStatus , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "approveContent", DbType = DbType.String, Value = input.approveContent == null ? (object)DBNull.Value : input.approveContent, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = input.branchCode == null ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_approveRepayment, prms);
                var resultObject = JsonConvert.DeserializeObject<ResultModel>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("PaymentService => approveRepayment", ex);
                return new ResultModel();
            }
        }

        ////// <summary>
        ///Insert Thêm mới phiếu thu 
        /// dutp
        /// 22/7/20
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public ResultModel insertRepayment(InputInsertUpdatePayment input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "scheduleId", DbType = DbType.Int64, Value = input.scheduleId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceId", DbType = DbType.Int64, Value = input.insuranceId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "ftttNo", DbType = DbType.String, Value = input.ftttNo == null ? (object)DBNull.Value : input.ftttNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "isExternalRepayment", DbType = DbType.Int16, Value = input.isExternalRepayment, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "paymentDate", DbType = DbType.Date, Value = input.paymentDate == null ? (object)DBNull.Value: input.paymentDate, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "totalAmount", DbType = DbType.Decimal, Value = input.totalAmount , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = input.branchCode == null ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fileList", DbType = DbType.String, Value = input.stringJsonfileList == null ? (object)DBNull.Value : input.stringJsonfileList, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                //resultObject = new ResultModel();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_insertRepayment, prms);
                var resultObject = JsonConvert.DeserializeObject<ResultModel>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("PaymentService => insertRepayment", ex);
                return new ResultModel();
            }
        }


        ////// <summary>
        ///Insert Thêm mới phiếu thu 
        /// dutp
        /// 22/7/20
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public ResultModel updateRepayment(InputInsertUpdatePayment input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "repaymentId", DbType = DbType.Int64, Value = input.repaymentId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "scheduleId", DbType = DbType.Int64, Value = input.scheduleId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "insuranceId", DbType = DbType.Int64, Value = input.insuranceId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "ftttNo", DbType = DbType.String, Value = input.ftttNo == null ? (object)DBNull.Value : input.ftttNo , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "isExternalRepayment", DbType = DbType.Int16, Value = input.isExternalRepayment, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "paymentDate", DbType = DbType.Date, Value = input.paymentDate == null ? (object)DBNull.Value: input.paymentDate, Size = Int32.MaxValue },
                    new SqlParameter{ ParameterName = "totalAmount", DbType = DbType.Decimal, Value = input.totalAmount , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = input.branchCode == null ? (object)DBNull.Value : input.branchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fileList", DbType = DbType.String, Value = input.stringJsonfileList == null ? (object)DBNull.Value : input.stringJsonfileList, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };

                var result = _Repository.ExecWithStoreProcedureCommand(Store_updateRepayment, prms);
                var resultObject = JsonConvert.DeserializeObject<ResultModel>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("PaymentService => updateRepayment", ex);
                return new ResultModel();
            }
        }

        /// <summary>
        /// Get chi tiết getListScheduleUnpaid 
        /// dutp
        /// 22/7/20
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetListScheduleUnpaid getListScheduleUnpaid(InputGetDetailPaymentInsert input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                 {
                    new SqlParameter{ ParameterName = "insuranceContractNo", DbType = DbType.String, Value = string.IsNullOrEmpty(input.insuranceContractNo) ? (object)DBNull.Value : input.insuranceContractNo, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = string.IsNullOrEmpty(input.userId) ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                 };
                var resultObject = new GetListScheduleUnpaid();
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getListScheduleUnpaid, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<GetListScheduleUnpaid>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("PaymentService => getListScheduleUnpaid", ex);
                return new GetListScheduleUnpaid();
            }
        }
    }
}
