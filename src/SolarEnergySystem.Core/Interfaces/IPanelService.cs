using SolarEnergySystem.Core.Entities;
using System;
using System.Collections.Generic;

namespace SolarEnergySystem.Core.Interfaces
{
    public interface IPanelService
    {
        ServiceResult<IEnumerable<Panel>> GetAll();

        ServiceResult<Panel> RegisterReading(string panelId, DateTime time, double wattage);
    }
}
