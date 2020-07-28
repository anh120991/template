using HDBH.BusinessRepository.Interface;
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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace HDBH.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private string Store_searchCustomer = "dbo.searchCustomer";
        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        public CustomerService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
        }


        public CustomerModel searchCustomer(CustomerInputSearchModel input)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                     new SqlParameter{ ParameterName = "cif", DbType = DbType.String, Value =  input.cif == null ? (object)DBNull.Value : input.cif, Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "name", DbType = DbType.String, Value = input.name == null ? (object)DBNull.Value : input.name, Size = Int32.MaxValue  },
                       new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value =  input.pageNumber , Size = Int32.MaxValue  },
                      new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = input.pageSize, Size = Int32.MaxValue  },

                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchCustomer, prms);
                if(result.errorCode == 0)
                {
                    var resultObject = JsonConvert.DeserializeObject<CustomerModel>(result.errorMessage);
                    return resultObject;
                }
                return new CustomerModel();
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("ModuleService => searchCustomer", ex);
                return null;
            }
        }
    }
}
