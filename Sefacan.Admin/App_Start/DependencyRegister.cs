using Autofac;
using Autofac.Integration.Mvc;
using Sefacan.Core.Infrastructure;
using Sefacan.Service;
using System.Web.Mvc;

namespace Sefacan.Admin
{
    public class DependencyRegister
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new AutofacWebTypesModule());

            //Service Library Register
            Service.DependencyRegister.Register(builder);
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterType<TagService>().As<ITagService>().InstancePerLifetimeScope();
            builder.RegisterType<UrlService>().As<IUrlService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            Engine.SetContainer(container);
        }
    }
}