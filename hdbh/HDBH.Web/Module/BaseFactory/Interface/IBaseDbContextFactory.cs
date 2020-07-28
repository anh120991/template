using HDBH.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HDBH.Web.Module.BaseFactory.Interface
{
    public interface IBaseDbContextFactory
    {
        RepositoryDbContext GetInstance();
    }
}