using Microsoft.Extensions.DependencyInjection;
using Staff.Application;
using Staff.DataAccess;
using Staff.Wpf.Services;
using Staff.Wpf.ViewModels;
using Staff.Wpf.Views;
using System.Windows;

namespace Staff.Wpf
{
    public partial class App : System.Windows.Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainView>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<EmployeeView>();
            services.AddSingleton<EmployeeViewModel>();
            services.AddTransient<DepartmentView>();
            services.AddSingleton<DepartmentViewModel>();
            services.AddTransient<IDialogService, DefaultDialogService>();
            services.AddApplication();
            services.AddDataAccess();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainView>();
            mainWindow?.Show();
        }
    }
}
