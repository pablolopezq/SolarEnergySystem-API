using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SolarEnergySystem.Core.DTOs;
using SolarEnergySystem.Core.Enums;
using SolarEnergySystem.Core.Interfaces;
using SolarEnergySystem.Infrastructure;

namespace SolarEnergySystem.API.Controllers
{
    [ApiController]
    [Route("panels")]
    public class AnalyticsController : ControllerBase
    {
        private readonly SolarEnergySystemDatabaseContext _context;
        private readonly IElectricityReadingService _electricityReadingService;

        public AnalyticsController(IElectricityReadingService electricityReadingService)
        {
            _electricityReadingService = electricityReadingService;
        }

        [Route("{panelId}/analytics")]
        public ActionResult<IEnumerable<Report>> Get(string panelId)
        {
            var serviceResult = _electricityReadingService.GetReport(panelId);
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            return Ok(serviceResult.Result);
        }
    }
}
