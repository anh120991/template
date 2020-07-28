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
    public class UserService : BaseService, IUserService
    {
        private string Store_getAllPermissionUser = "dbo.getAllPermissionUser";
        private readonly IRepositoryUnitOfWork _uow;
        private readonly IDataRepository _Repository;
        private ICached _cached;

        #region STORE
        private string Store_getListRole = "dbo.getListRole";
        private string Store_searchUser = "dbo.searchUser"; 
        private string Store_insertUser = "dbo.insertUser";
        private string Store_insertUserList = "dbo.insertUserList";
        #endregion
        public UserService(IRepositoryUnitOfWork unitOfWork, IDataRepository Repository, ICached cached) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _Repository = Repository;
            _cached = cached;
        }
        public UserLoginModel getAllPermissionUser(string userId)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {
                     new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_getAllPermissionUser, prms);
                UserLoginModel obj = new UserLoginModel();
                if (result.errorCode == 0)
                {
                    obj = JsonConvert.DeserializeObject<UserLoginModel>(result.errorMessage);
                }
                return obj;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("ModuleService => getListModule", ex);
                return null;
            }
        }

        public List<RoleModel> getListRole()
        {
            try
            {
                var rsCache = _cached.Get<List<RoleModel>>(CachedKey.RoleList);
                if (rsCache != null)
                {
                    return (List<RoleModel>)rsCache;
                }
                else
                {
                    SqlParameter[] prms = new SqlParameter[]
                {
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                };
                    var result = _Repository.ExecWithStoreProcedureCommand(Store_getListRole, prms);
                    var resultObject = JsonConvert.DeserializeObject<List<RoleModel>>(result.errorMessage);
                    _cached.Set(CacheModeEnum.Set, CachedKey.RoleList, resultObject, new TimeSpan(1, 0, 0, 0));
                    return resultObject;
                }
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("UserService => getListRole", ex);
                return null;
            }
        }

        public ResultModel insertUser(UserInsertViewModel model)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {

                    new SqlParameter{ ParameterName = "userName", DbType = DbType.String, Value = model.userName == null ? (object)DBNull.Value : model.userName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fullName", DbType = DbType.String,Value = model.fullName == null ? (object)DBNull.Value : model.fullName , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "officerCode", DbType = DbType.String,Value = model.officerCode == null ? (object)DBNull.Value : model.officerCode , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userBranchCode", DbType = DbType.String, Value = model.userBranchCode == null ? (object)DBNull.Value : model.userBranchCode, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "phoneNumber", DbType = DbType.String,Value = model.phoneNumber == null ? (object)DBNull.Value : model.phoneNumber , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "email", DbType = DbType.String,Value = model.email == null ? (object)DBNull.Value : model.email , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },
                       new SqlParameter{ ParameterName = "userRoleList", DbType = DbType.String,Value = model.strUserRoleList == null ? (object)DBNull.Value : model.strUserRoleList , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                    };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_insertUser, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("UserService => insertUser", ex);
                return null;
            }
        }

        public UserSearchModel searchUser(UserSearchViewModel input)
        {
            try
            {
                UserSearchModel resultObject = new UserSearchModel();
                SqlParameter[] prms = new SqlParameter[]
               {
                    new SqlParameter{ ParameterName = "userName", DbType = DbType.Date, Value =  input.userName == null ? (object)DBNull.Value : input.userName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "fullName", DbType = DbType.Date, Value =  input.fullName == null ? (object)DBNull.Value : input.fullName, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value =  input.branchCode == null ? (object)DBNull.Value : input.branchCode , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "isActive", DbType = DbType.Int32, Value = input.isActive, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "isDelete", DbType = DbType.Int32, Value = input.isDelete, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value =  input.userId == null ? (object)DBNull.Value : input.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageNumber", DbType = DbType.Int32, Value = input.pageNumber, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "pageSize", DbType = DbType.Int32, Value = input.pageSize, Size = Int32.MaxValue  },


                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
               };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_searchUser, prms);
                if (result.errorCode == 0)
                {
                    resultObject = JsonConvert.DeserializeObject<UserSearchModel>(result.errorMessage);
                }
                return resultObject;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("UserService => searchUser", ex);
                return new UserSearchModel();
            }
        }

        public ResultModel insertUserList(UserImportModel model)
        {
            try
            {
                SqlParameter[] prms = new SqlParameter[]
                {

                    new SqlParameter{ ParameterName = "userId", DbType = DbType.String, Value = model.userId, Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "branchCode", DbType = DbType.String, Value = model.branchCode, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "userRoleList", DbType = DbType.String,Value = model.strUserList == null ? (object)DBNull.Value : model.strUserList , Size = Int32.MaxValue  },
                    new SqlParameter{ ParameterName = "resultMessage", DbType = DbType.String, Direction= ParameterDirection.Output, Size = Int32.MaxValue  },
                     new SqlParameter{ ParameterName = "resultCode", DbType = DbType.Int32, Direction= ParameterDirection.Output, Size = Int32.MaxValue  }
                    };
                var result = _Repository.ExecWithStoreProcedureCommand(Store_insertUserList, prms);
                return result;
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("UserService => insertUserList", ex);
                return null;
            }
        }
    }
}
