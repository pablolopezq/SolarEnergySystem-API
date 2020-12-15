using System;
using System.Collections.Generic;
using System.Text;
using SolarEnergySystem.Core.Enums;

namespace SolarEnergySystem.Core.Entities
{
    public class Panel
    {
        public Panel()
        {
            ElectricityReadings = new HashSet<ElectricityReading>();
        }

        public PanelType PanelType { get; set; }

        public string SerialNumber { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Brand { get; set; }

        public MeasuringUnit MeasuringUnit { get; set; }

        public ICollection<ElectricityReading> ElectricityReadings { get; set; }
    }
}
