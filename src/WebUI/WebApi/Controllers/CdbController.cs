using Application.Features.SimulatePerformance;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CdbCalculation.Controllers
{
    public class CdbController : ApiControllerBase
    {
        
        [HttpPost]
        public async Task<PerformanceResults> GetAsync([FromBody] SimulatePerformanceCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}