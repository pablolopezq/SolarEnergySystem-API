using SolarEnergySystem.Core.DTOs;
using SolarEnergySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarEnergySystem.Core.Interfaces
{
    public interface IElectricityReadingService
    {
        ServiceResult<ElectricityReading> GetLastReading();

        ServiceResult<ElectricityReading> RegisterReading(string panelId, DateTime time, double wattage);

        ServiceResult<IEnumerable<Report>> GetReport(string panelId);
    }
}
