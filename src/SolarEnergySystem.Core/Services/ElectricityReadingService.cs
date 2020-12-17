using SolarEnergySystem.Core.DTOs;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarEnergySystem.Core.Services
{
    public class ElectricityReadingService : IElectricityReadingService
    {
        private readonly IRepository<ElectricityReading, int> _readingRepository;

        public ElectricityReadingService(IRepository<ElectricityReading, int> readingRepository)
        {
            _readingRepository = readingRepository;
        }
        public ServiceResult<ElectricityReading> GetLastReading()
        {
            return ServiceResult<ElectricityReading>.SuccessResult(_readingRepository.GetAll().OrderBy(r => r.ReadingDateTime).First());
        }

        public ServiceResult<IEnumerable<Report>> GetReport(string panelId)
        {
            IEnumerable<Report> retorno = Enumerable.Empty<Report>();
            var readings = _readingRepository.GetAll().OrderBy(r => r.ReadingDateTime).Where(r => r.PanelId == panelId);
            for (int i = 0; i < 24; i++)
            {
                var hourReadings = readings.Where(r => r.ReadingDateTime.Hour == i);
                var newReport = new Report
                {
                    Hour = i,
                    KilowattAverage = hourReadings.Average(r => r.KiloWatt),
                    KilowattSum = hourReadings.Sum(r => r.KiloWatt),
                    MaxReading = hourReadings.Max(r => r.KiloWatt),
                    MinReading = hourReadings.Min(r => r.KiloWatt)
                };
                retorno.Append(newReport);
            }
            return ServiceResult<IEnumerable<Report>>.SuccessResult(retorno);   
        }

        public ServiceResult<ElectricityReading> RegisterReading(string panelId, DateTime time, double wattage)
        {
            var newReading = new ElectricityReading
            {
                KiloWatt = wattage,
                ReadingDateTime = time,
                PanelId = panelId
            };
            _readingRepository.Add(newReading);
            return ServiceResult<ElectricityReading>.SuccessResult(newReading);
        }
    }
}
