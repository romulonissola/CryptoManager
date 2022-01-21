using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CryptoManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            this.healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            HealthReport report = await healthCheckService.CheckHealthAsync();
            var result = new
            {
                generalStatus = report.Status.ToString(),
                checks = report.Entries.Select(e =>
                    new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description.ToString(),
                        errors = e.Value.Data
                    })
            };
            return report.Status == HealthStatus.Healthy ? Ok(result) : StatusCode((int)HttpStatusCode.ServiceUnavailable, result);
        }
    }
}
