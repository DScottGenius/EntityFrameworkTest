using Autofac;
using EntityFrameworkTest.Startup;
using System.Windows;

namespace EntityFrameworkTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var boot = new Bootstrapper();
            IContainer container = boot.Bootstrap();
            var _mainWindow = container.Resolve<MainWindow>();
            _mainWindow.Show();
        }
    }
}
