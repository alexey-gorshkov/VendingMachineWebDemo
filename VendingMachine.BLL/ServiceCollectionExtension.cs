using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using VendingMachine.BLL.Infrastructure;
using VendingMachine.BLL.Interfaces;
using VendingMachine.BLL.Services;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;

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

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddDefaultIdentity<User>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.Tokens.EmailConfirmationTokenProvider = "emailconf";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconf");

            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromHours(5));
            // custom lifespan for email confirmation token:
            services.Configure<EmailConfirmationTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromDays(1));

            var jwtAppSettingOptions = configuration.GetSection("JwtIssuerOptions");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtAppSettingOptions["JwtIssuer"],
                        ValidAudience = jwtAppSettingOptions["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions["JwtKey"]))
                    };
                });

            services.AddAutoMapper();

            return services;
        }
    }
}
