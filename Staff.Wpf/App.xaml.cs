using Microsoft.Extensions.DependencyInjection;
using Staff.Application;
using Staff.DataAccess;
using Staff.Domain;
using Staff.Wpf.Services;
using Staff.Wpf.ViewModels;
using Staff.Wpf.Views;
using System.Collections.Generic;
using System.Windows;

namespace Staff.Wpf;

public partial class App : System.Windows.Application
{
    public readonly ServiceProvider ServiceProvider;
    public App()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainView>();
        services.AddTransient<EmployeeView>();
        services.AddTransient<DepartmentView>();
        services.AddTransient<EmployeeDetailView>();
        services.AddTransient<DepartmentDetailView>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<EmployeeViewModel>();
        services.AddTransient<DepartmentViewModel>();
        services.AddSingleton<EmployeeDetailViewModel>();
        services.AddSingleton<DepartmentDetailViewModel>();

        services.AddTransient<IDialogService, DefaultDialogService>();
        services.AddTransient<IExporter<List<Employee>>, XmlEmployeeExporter>();
        services.AddTransient<IImporter<List<Employee>>, XmlEmployeeImporter>();

        services.AddApplication();
        services.AddDataAccess();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = ServiceProvider.GetService<MainView>();
        mainWindow?.Show();
    }
}
