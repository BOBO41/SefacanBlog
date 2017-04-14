using Autofac;
using Sefacan.Core.Cache;
using Sefacan.Data;

namespace Sefacan.Service
{
    public class DependencyRegister
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>().As<IDataContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
        }
    }
}