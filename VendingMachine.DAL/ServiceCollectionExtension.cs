using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Interfaces;
using VendingMachine.DAL.Repositories;

namespace VendingMachine.DAL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPurseRepository, PurseRepository>();
            services.AddScoped<IUserDepositRepository, UserDepositRepository>();

            return services;
        }
    }
}
