using Application.Features.SimulatePerformance;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WebApi.Controllers;

namespace CdbCalculation.Controllers
{
    [ExcludeFromCodeCoverage]
    public class CdbController : ApiControllerBase
    {
        
        [HttpPost]
        public async Task<PerformanceResults> GetAsync([FromBody] SimulatePerformanceCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}