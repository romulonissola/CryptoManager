using System;
using System.Collections.Generic;
using System.Text;
using CryptoManager.Integration.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoManager.Integration
{
    public static class IntegrationServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrations(this IServiceCollection services)
        {
            services.AddScoped<ExchangeIntegrationCache>();

            return services;
        }
    }
}
