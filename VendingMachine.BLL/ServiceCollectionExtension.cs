using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.BLL.Interfaces;
using VendingMachine.BLL.Services;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IVendingMachineService, VendingMachineService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddSingleton(configuration.GetSection("EmailOptions").Get<EmailOptions>());                                

            return services;
        }
    }
}
