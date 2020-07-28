using HDBH.Models;
using HDBH.Models.HelperModel;
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
    public interface IRepository
    {
        ResultModel ExecWithStoreProcedureCommand(string query, SqlParameter[] parameters);
    }
}