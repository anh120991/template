using HDBH.Models;
using HDBH.Models.HelperModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HDBH.Data.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T t);
        int Count();
        Task<int> CountAsync();
        void Delete(T entity);
        void Dispose();
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsyn();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        ICollection<T> ExecWithStoreProcedure(string query, OracleParameter[] param);
        ICollection<K> ExecWithStoreProcedure<K>(string query, OracleParameter[] param) where K : class;
        ResultModel ExcuteNonQuery(string query, OracleParameter[] param);
        ActionAttributeReturn ExecuteReturnAction(string query, OracleParameter[] param);
        int ExecuteSqlCommand(string query, OracleParameter[] param);
        Task<T> GetAsync(int id);
        T Update(T t, object key);
        Dictionary<string, object> ExecuteMultipleList(string query, List<Type> ObjectParse, params object[] parameters);
        Dictionary<string, object> ExecuteMultipleList(string query, List<Type> ObjectParse, ref OracleParameter[] parameters);

    }
}
