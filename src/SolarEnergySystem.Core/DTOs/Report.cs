using System;
using System.Collections.Generic;
using System.Text;

namespace SolarEnergySystem.Core.DTOs
{
    public class Report
    {
        public int Hour { get; set; }

        public double KilowattSum { get; set; }

        public double KilowattAverage { get; set; }

        public double MaxReading { get; set; }

        public double MinReading { get; set; }
    }
}
