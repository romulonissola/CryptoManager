using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoManager.Domain.Contracts.Integration;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CryptoManager.WebApi.HealthCheck
{
    public class ExchangeIntegrationHealthCheck : IHealthCheck
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IExchangeIntegrationStrategyContext _exchangeIntegrationStrategyContext;

        public ExchangeIntegrationHealthCheck(
            IExchangeRepository exchangeRepository,
            IExchangeIntegrationStrategyContext exchangeIntegrationStrategyContext)
        {
            _exchangeRepository = exchangeRepository;
            _exchangeIntegrationStrategyContext = exchangeIntegrationStrategyContext;
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

                    var result = await _exchangeIntegrationStrategyContext.TestIntegrationUpAsync(exchangeType);
                    if (!result.HasSucceded)
                    {
                        errorList.Add(exchangeType.ToString(), $"Test not succeded, errorMessage {result.ErrorMessage} ");
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
