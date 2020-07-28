using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.SQLData
{
    public class RepositoryDbContext : BaseDbContext
    {
        private readonly string _connString;
        public RepositoryDbContext(string connstring) : base(connstring)
        {
            _connString = connstring;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Assembly modelInAssembly = Assembly.Load(new AssemblyName("TDG.DatabaseModel"));
            //var exportedTypes = modelInAssembly.ExportedTypes;

            //foreach (Type type in exportedTypes)
            //{
            //    if (type.IsClass)
            //    {

            //        var method = builder.GetType().GetMethod("Entity", new Type[] { });
            //        method = method.MakeGenericMethod(new Type[] { type });
            //        method.Invoke(builder, null);
            //    }
            //}
            base.OnModelCreating(builder);
        }
    }
}