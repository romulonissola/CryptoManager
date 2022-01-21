using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CryptoManager.WebApi.HealthCheck
{
    public class ExchangeIntegrationPingHealthCheck : IHealthCheck
    {
        private readonly IExchangeRepository _exchangeRepository;

        public ExchangeIntegrationPingHealthCheck(
            IExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var errorList = new Dictionary<string, object>();
                var exchangeIntegratedList = Enum.GetValues(typeof(ExchangesIntegratedType)).Cast<ExchangesIntegratedType>();
                foreach (var exchangeType in exchangeIntegratedList)
                {
                    var exchange = await _exchangeRepository.GetByExchangeTypeAsync(exchangeType);
                    if (exchange == null)
                    {
                        errorList.Add(exchangeType.ToString() ,"Not configured");
                        continue;
                    }
                    var host = new Uri(exchange.APIUrl).Host;
                    using var ping = new Ping();
                    var reply = await ping.SendPingAsync(host);
                    if (reply.Status != IPStatus.Success)
                    {
                        errorList.Add(exchangeType.ToString(), "Ping failed");
                        continue;
                    }

                    if (reply.RoundtripTime > 100)
                    {
                        errorList.Add(exchangeType.ToString(), "Slow response time");
                        continue;
                    }

                    
                }

                if(errorList.Count() == exchangeIntegratedList.Count())
                {
                    return HealthCheckResult.Unhealthy("Exchange Integration Status", data: errorList);
                }
                else if (errorList.Any())
                {
                    return HealthCheckResult.Degraded("Exchange Integration Status", data: errorList);
                }

                return HealthCheckResult.Healthy("Exchange Integration Status");
            }
            catch(Exception ex)
            {
                return HealthCheckResult.Unhealthy("Exchange Integration Status", exception: ex);
            }
        }
    }
}
