using CryptoManager.Domain.Entities;
using CryptoManager.Repository.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Repository
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>
                    (options => {
                        options.Password.RequiredLength = 3;
                        options.Password.RequiredUniqueChars = 0;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                    .AddDefaultTokenProviders();
            return services;
        }
    }
}
