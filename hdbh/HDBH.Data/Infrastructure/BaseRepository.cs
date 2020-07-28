using HDBH.Models;
using HDBH.Models.HelperModel;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HDBH.Data.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly BaseContext _dbContext;
        private DbSet<T> _dbSet => _dbContext.Set<T>();


        //private DbSet<User> Users { get; set; }




        public BaseRepository(BaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {

            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {

            _dbContext.Set<T>().Add(t);
            return t;
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _dbContext.Set<T>().Find(key);
            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(t);
            }
            return exist;
        }

        public int Count()
        {
            return _dbContext.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public ICollection<T> ExecWithStoreProcedure(string query, OracleParameter[] param)
        {
            string pamameterString = string.Empty;
            if (!query.Contains("@p"))
            {
                if (param != null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        pamameterString += " :" + param[i].ParameterName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            string fullquery = "BEGIN " + query + "(" + pamameterString + "); End;";
            return _dbContext.Set<T>().FromSql(fullquery, param).ToList();  //.FromSql(fullquery, param).ToList();
        }

        public ICollection<K> ExecWithStoreProcedure<K>(string query, OracleParameter[] param) where K : class
        {
            string pamameterString = string.Empty;
            if (!query.Contains("@p"))
            {
                if (param != null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        pamameterString += " :" + param[i].ParameterName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            string fullquery = "BEGIN " + query + "(" + pamameterString + "); End;";
            return _dbContext.Set<K>().FromSql<K>(fullquery, param).ToList();  //.FromSql(fullquery, param).ToList();
        }

        public ResultModel ExcuteNonQuery(string query, OracleParameter[] param)
        {
            string pamameterString = string.Empty;
            if (!query.Contains("@p"))
            {
                if (param != null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        pamameterString += " :" + param[i].ParameterName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            string fullquery = "BEGIN " + query + "(" + pamameterString + "); End;";
            return _dbContext.Set<ResultModel>().FromSql(fullquery, param).FirstOrDefault();
        }

        public ActionAttributeReturn ExecuteReturnAction(string query, OracleParameter[] param)
        {
            string pamameterString = string.Empty;
            List<OracleParameter> prmsLst = new List<OracleParameter>();
            if (!query.Contains("@p"))
            {
                if (param != null)
                {
                    foreach (var item in param)
                    {
                        prmsLst.Add(item);
                    }

                }
                prmsLst.Add(new OracleParameter { ParameterName = "ERRCODE", OracleDbType = OracleDbType.Int32, Direction = System.Data.ParameterDirection.Output });
                prmsLst.Add(new OracleParameter { ParameterName = "ERRMSG", OracleDbType = OracleDbType.NVarchar2, Direction = System.Data.ParameterDirection.Output, Size = 4000 });
                foreach (var Oraparam in prmsLst)
                {
                    pamameterString += " :" + Oraparam.ParameterName + ",";
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            var prms = prmsLst.ToArray();
            string fullquery = "BEGIN " + query + "(" + pamameterString + "); END;";
            var cmd = _dbContext.Database.ExecuteSqlCommand(fullquery, prms);


            return new ActionAttributeReturn
            {
                errMsg = Convert.ToString(prms.FirstOrDefault(x => x.ParameterName == "ERRMSG").Value),
                errCode = Convert.ToInt32(prms.FirstOrDefault(x => x.ParameterName == "ERRCODE").Value.ToString())
            };
        }


        public int ExecuteSqlCommand(string query, OracleParameter[] param)
        {
            string pamameterString = string.Empty;
            if (!query.Contains("@p"))
            {
                if (param != null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        pamameterString += " :" + param[i].ParameterName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            string fullquery = "BEGIN " + query + "(" + pamameterString + "); End;";
            var cmd = _dbContext.Database.ExecuteSqlCommand(fullquery, param);
            return cmd;
        }

        public Dictionary<string, object> ExecuteMultipleList(string query, List<Type> ObjectParse, params object[] parameters)
        {
            string pamameterString = string.Empty;
            if (!query.Contains("@p"))
            {
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        pamameterString += " @p" + i.ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            string fullquery = query + pamameterString;

            DataSet ds = new DataSet("ds");
            //WebConfigurationManager.ConnectionStrings[LogConnection].ConnectionString;
            using (OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.96.62.10)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = testdwdb)));User Id=CONTRACTMAN; Password=contractman;"))
            {
                OracleCommand oraComm = new OracleCommand(fullquery, conn);
                for (int i = 0; i < parameters.Length; i++)
                {
                    //oraComm.Parameters.Add("@p" + i.ToString(), parameters[i]);
                    //oraComm.Parameters.Add(new OracleParameter(parameters[i].ToString(), parameters);
                }
                oraComm.CommandType = CommandType.StoredProcedure;
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = oraComm;
                da.Fill(ds);
            }
            if (ds.Tables.Count > 0 && ds.Tables.Count == ObjectParse.Count)
            {
                Dictionary<string, object> resultQuery = new Dictionary<string, object>();
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable dt = ds.Tables[i];
                    Type crrType = ObjectParse[i];
                    var result = ConvertToListObject(dt, crrType);
                    resultQuery.Add(i.ToString(), result);
                }
                return resultQuery;
            }
            return null;
        }

        public Dictionary<string, object> ExecuteMultipleList(string query, List<Type> ObjectParse, ref OracleParameter[] parameters)
        {
            string pamameterString = string.Empty;
            if (!query.Contains("@p"))
            {
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        pamameterString += " @p" + i.ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(pamameterString))
                {
                    pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
                }
            }
            string fullquery = query + pamameterString;

            DataSet ds = new DataSet("ds");
            //WebConfigurationManager.ConnectionStrings[LogConnection].ConnectionString;
            using (SqlConnection conn = new SqlConnection("ConnectionString"))
            {
                SqlCommand sqlComm = new SqlCommand(fullquery, conn);
                for (int i = 0; i < parameters.Length; i++)
                {
                    sqlComm.Parameters.AddWithValue("@p" + i.ToString(), parameters[i]);
                }
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
            }
            Dictionary<string, object> resultQuery = new Dictionary<string, object>();
            if (ds.Tables.Count > 0 && ds.Tables.Count == ObjectParse.Count)
            {
                
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable dt = ds.Tables[i];
                    Type crrType = ObjectParse[i];
                    var result = ConvertToListObject(dt, crrType);
                    resultQuery.Add(i.ToString(), result);
                }
                
            }
            _dbContext.Database.ExecuteSqlCommand(fullquery, parameters);
            return resultQuery;
            
        }
        private List<object> ConvertToListObject(DataTable dt, Type t)
        {
            List<object> resultList = new List<object>();
            //Type t = typeof(K);
            PropertyDescriptorCollection curProp = TypeDescriptor.GetProperties(t);
            foreach (DataRow row in dt.Rows)
            {
                var objConvert = Activator.CreateInstance(t);
                foreach (PropertyDescriptor prop in curProp)
                {
                    prop.SetValue(objConvert, row[prop.Name.ToUpper()]);
                }
                resultList.Add(objConvert);
            }
            return resultList;
        }

        //public ICollection<T> ExecWithStoreProcedureToJson(string query, OracleParameter[] param)
        //{
        //    string pamameterString = string.Empty;
        //    if (!query.Contains("@p"))
        //    {
        //        if (param != null)
        //        {
        //            for (int i = 0; i < param.Length; i++)
        //            {
        //                pamameterString += " :" + param[i].ParameterName + ",";
        //            }
        //        }
        //        if (!string.IsNullOrEmpty(pamameterString))
        //        {
        //            pamameterString = pamameterString.Substring(0, pamameterString.Length - 1);
        //        }
        //    }
        //    string fullquery = "BEGIN " + query + "(" + pamameterString + "); End;";
        //    var result = _dbContext.Set<ReponseModel>().FromSql(fullquery, param).ToList();
        //    if(result != null && result.Any())
        //    {
        //        var _item = result.FirstOrDefault();
        //        string strTemp = JObject.Parse(_item.RESPONSEDATA).SelectToken("ROWSET").ToString();
        //        return  JsonConvert.DeserializeObject<List<T>>(strTemp);
        //    }
        //    return new List<T>();
        //}


        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
