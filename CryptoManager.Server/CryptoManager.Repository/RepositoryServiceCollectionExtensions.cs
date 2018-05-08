﻿using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.Entities;
using CryptoManager.Repository.DatabaseContext;
using CryptoManager.Repository.Infrastructure;
using CryptoManager.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoManager.Repository
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddSQLiteDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddDbContext<EntityContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddSQLServerDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddDbContext<EntityContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>
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

            services.AddScoped<IExchangeRepository, ExchangeRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            return services;
        }

        public static IServiceCollection AddORM(this IServiceCollection services)
        {
            services.AddScoped(typeof(IORM<>), typeof(EntityRepository<>));
            return services;
        }

        public static IApplicationBuilder EnsureCreateDatabase(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var applicationIdentityDbContext = serviceScope.ServiceProvider.GetService<ApplicationIdentityDbContext>();
                applicationIdentityDbContext.Database.EnsureCreated();

                var entityContext = serviceScope.ServiceProvider.GetService<EntityContext>();
                entityContext.Database.EnsureCreated();
            }

            return app;
        }
    }
}
