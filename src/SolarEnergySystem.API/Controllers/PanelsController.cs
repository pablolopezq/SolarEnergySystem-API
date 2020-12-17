using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolarEnergySystem.Infrastructure;
using SolarEnergySystem.Core.Interfaces;
using SolarEnergySystem.API.Models;
using SolarEnergySystem.Core.Enums;

namespace SolarEnergySystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanelsController : ControllerBase
    {
        private readonly IPanelService _panelService;

        public PanelsController(IPanelService panelService)
        {
            _panelService = panelService;
        }

        [HttpGet]
        public ActionResult<PanelDto> Get()
        {
            var serviceResult = _panelService.GetAll();
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            return Ok(serviceResult.Result.Select(p => new PanelDto {
                MeasuringUnit = p.MeasuringUnit,
                PanelType = p.PanelType,
                SerialNumber = p.Id
            }));
        }

        [HttpPatch]
        [Route("{panelId}/register")]
        public ActionResult<string> RegisterReading(string panelId, [FromBody] double wattage)
        {
            var time = DateTime.UtcNow;

            var serviceResult = _panelService.RegisterReading(panelId, time, wattage);
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            return Ok(serviceResult.Result.Id);
        }
    }
}
