using HDBH.Models;
using HDBH.Models.HelperModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.SQLData.Infrastructure
{
    public class GenericRepository : IRepository
    {
        private readonly BaseDbContext _dbContext;
        public GenericRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ResultModel ExecWithStoreProcedureCommand(string query, SqlParameter[] parameters)
        {
            ResultModel result = new ResultModel();
            var queryString = "{0} {1}";

            var paramsList = string.Empty;

            foreach (var param in parameters)
            {
                paramsList += "@" + param.ParameterName + (param.Direction != ParameterDirection.Output ? "," : " OUTPUT,");
            }
            string fullquery = String.Format(queryString, query, paramsList.Substring(0, paramsList.Length - 1));
            var _response = _dbContext.Database.ExecuteSqlCommand(fullquery, parameters);
            result.errorMessage = Convert.ToString(parameters.FirstOrDefault(x => x.ParameterName == "resultMessage").Value);
            result.errorCode = Convert.ToInt32(parameters.FirstOrDefault(x => x.ParameterName == "resultCode").Value);
            return result;
        }
    }
}
