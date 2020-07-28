using Autofac;
using Autofac.Integration.Mvc;
using HDBG.Log;
using HDBH.SQLData;
using HDBH.SQLData.Interface;
using HDBH.Web.Module.BaseFactory;
using HDBH.Web.Module.BaseFactory.Interface;
using System.Linq;
using System.Reflection;


namespace HDBH.Web.Module
{
    public class DataModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<User>().As<IUser>().InstancePerRequest();
            builder.RegisterType<RepositoryUnitOfWork>().As<IRepositoryUnitOfWork>().InstancePerRequest();
            builder.RegisterType<BaseDbContextFactory>().As<IBaseDbContextFactory>().InstancePerRequest();
            builder.Register(c => c.Resolve<IBaseDbContextFactory>().GetInstance())
                   .As<RepositoryDbContext>().InstancePerRequest();
            builder.RegisterType<CachedAccess>().As<ICached>().InstancePerRequest();
            builder.RegisterAssemblyTypes(Assembly.Load("HDBH.BusinessRepository"))
                      .Where(t => t.Name.EndsWith("Repository"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load("HDBH.CounterParty"));
            builder.RegisterAssemblyTypes(Assembly.Load("HDBH.CPProduct"));
            builder.RegisterAssemblyTypes(Assembly.Load("HDBH.InsuranceContract"));
            builder.RegisterAssemblyTypes(Assembly.Load("HDBH.Payment"));
            builder.RegisterAssemblyTypes(Assembly.Load("HDBH.Sys"));
        }
    }
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("HDBH.Services"))
                      .Where(t => t.Name.EndsWith("Service"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
        }
    }
    public class WebModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            // Register Common MVC Types
            builder.RegisterModule<AutofacWebTypesModule>();
            // Register MVC Controllers
            builder.RegisterControllers(assembly);
        }
    }
}