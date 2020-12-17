using SolarEnergySystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarEnergySystem.API.Models
{
    public class PanelDto
    {
        public string SerialNumber { get; set; }

        public PanelType PanelType { get; set; }

        public MeasuringUnit MeasuringUnit { get; set; }
    }
}
