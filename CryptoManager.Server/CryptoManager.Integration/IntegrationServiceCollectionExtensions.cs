using System;
using System.Collections.Generic;
using System.Text;
using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using CryptoManager.Integration.Clients;
using CryptoManager.Integration.ExchangeIntegrationStrategies;
using CryptoManager.Integration.Utils;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CryptoManager.Integration
{
    public static class IntegrationServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrations(this IServiceCollection services)
        {
            services.AddScoped<IExchangeIntegrationCache, ExchangeIntegrationCache>();
            services.AddScoped<IExchangeIntegrationStrategyContext, ExchangeIntegrationStrategyContext>();
            services.AddScoped<IExchangeIntegrationStrategy, BinanceIntegrationStrategy>();
            services.AddScoped<IExchangeIntegrationStrategy, BitcoinTradeIntegrationStrategy>();
            services.AddScoped<IExchangeIntegrationStrategy, CoinbaseIntegrationStrategy>();
            services.AddScoped<IExchangeIntegrationStrategy, HitBTCIntegrationStrategy>();
            services.AddScoped<IExchangeIntegrationStrategy, KuCoinIntegrationStrategy>();

            //clients
            services.AddRefitClient<IBinanceIntegrationClient>()
                .ConfigureHttpClient(async (sp, c) =>
                {
                    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                    using var serviceScope = serviceScopeFactory.CreateScope();
                    var exchangeRepository = serviceScope.ServiceProvider.GetService<IExchangeRepository>();
                    var exchange = await exchangeRepository.GetByExchangeTypeAsync(ExchangesIntegratedType.Binance);
                    if (exchange != null)
                    {
                        c.BaseAddress = new Uri(exchange.APIUrl);
                    }
                });
            services.AddRefitClient<IBitcoinTradeIntegrationClient>()
                .ConfigureHttpClient(async (sp, c) =>
                {
                    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                    using var serviceScope = serviceScopeFactory.CreateScope();
                    var exchangeRepository = serviceScope.ServiceProvider.GetService<IExchangeRepository>();
                    var exchange = await exchangeRepository.GetByExchangeTypeAsync(ExchangesIntegratedType.BitcoinTrade);
                    if (exchange != null)
                    {
                        c.BaseAddress = new Uri(exchange.APIUrl);
                    }
                });
            services.AddRefitClient<ICoinbaseIntegrationClient>()
                .ConfigureHttpClient(async (sp, c) =>
                {
                    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                    using var serviceScope = serviceScopeFactory.CreateScope();
                    var exchangeRepository = serviceScope.ServiceProvider.GetService<IExchangeRepository>();
                    var exchange = await exchangeRepository.GetByExchangeTypeAsync(ExchangesIntegratedType.Coinbase);
                    if (exchange != null)
                    {
                        c.BaseAddress = new Uri(exchange.APIUrl);
                    }
                });
            services.AddRefitClient<IHitBTCIntegrationClient>()
                .ConfigureHttpClient(async (sp, c) =>
                {
                    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                    using var serviceScope = serviceScopeFactory.CreateScope();
                    var exchangeRepository = serviceScope.ServiceProvider.GetService<IExchangeRepository>();
                    var exchange = await exchangeRepository.GetByExchangeTypeAsync(ExchangesIntegratedType.HitBTC);
                    if (exchange != null)
                    {
                        c.BaseAddress = new Uri(exchange.APIUrl);
                    }
                });

            services.AddRefitClient<IKuCoinIntegrationClient>()
                .ConfigureHttpClient(async (sp, c) =>
                {
                    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                    using var serviceScope = serviceScopeFactory.CreateScope();
                    var exchangeRepository = serviceScope.ServiceProvider.GetService<IExchangeRepository>();
                    var exchange = await exchangeRepository.GetByExchangeTypeAsync(ExchangesIntegratedType.KuCoin);
                    if (exchange != null)
                    {
                        c.BaseAddress = new Uri(exchange.APIUrl);
                    }
                });

            return services;
        }
    }
}
