using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Staff.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=staffdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }, ServiceLifetime.Transient);

        return services;
    }
}