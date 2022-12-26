using Microsoft.Extensions.DependencyInjection;
using Staff.Application.Interfaces;
using Staff.Application.Services;

namespace Staff.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IEmployeeService, EmployeeService>();
        services.AddTransient<IDepartmentService, DepartmentService>();

        return services;
    }
}