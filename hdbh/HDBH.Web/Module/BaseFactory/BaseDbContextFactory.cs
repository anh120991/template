using HDBH.SQLData;
using HDBH.Web.Module.BaseFactory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HDBH.Web.Module.BaseFactory
{
    public class BaseDbContextFactory : IBaseDbContextFactory
    {
        // Note : we can move this class to service layer.
        public RepositoryDbContext GetInstance()
        {
            String connectionString = this.GetConnectionString(HttpContext.Current);
            return new RepositoryDbContext(connectionString);
        }

        private String GetConnectionString(HttpContext context)
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings.Count > 0)
            {
                return WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
            else
            {
                return null;
            }

        }
    }
}