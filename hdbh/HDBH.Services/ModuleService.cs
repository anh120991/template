using HDBH.BusinessRepository.Interface;
using HDBH.Models.DatabaseModel;
using HDBH.Models.HelperModel;
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
    public class ModuleService : BaseService, IModuleService
    {
        private string Store_GetList = "dbo.getListModule";
        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        public ModuleService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
        }
        public List<ModuleModel> getListModule()
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_GetList, prms);
                var resultObject = JsonConvert.DeserializeObject<List<ModuleModel>>(result.errorMessage);
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("ModuleService => getListModule", ex);
                return null;
            }
        }
    }
}
