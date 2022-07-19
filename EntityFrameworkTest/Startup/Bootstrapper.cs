using Autofac;
using EntityFrameworkTest.View_Models;

namespace EntityFrameworkTest.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainView>().AsSelf();

            return builder.Build();
        }
    }
}
