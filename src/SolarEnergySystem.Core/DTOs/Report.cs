using System;
using System.Collections.Generic;
using System.Text;

namespace SolarEnergySystem.Core.DTOs
{
    public class Report
    {
        public int Hour { get; set; }

        public int KilowattSum { get; set; }

        public int KilowattAverage { get; set; }

        public int MaxReading { get; set; }

        public int MinReading { get; set; }
    }
}
