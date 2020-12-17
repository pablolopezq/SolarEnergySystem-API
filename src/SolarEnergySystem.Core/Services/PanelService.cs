using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Enums;
using SolarEnergySystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarEnergySystem.Core.Services
{
    public class PanelService : IPanelService
    {
        private readonly IRepository<Panel, string> _panelRepository;
        private readonly IElectricityReadingService _electricityReadingService;

        public PanelService(IRepository<Panel, string> panelRepository, IElectricityReadingService electricityReadingService)
        {
            _panelRepository = panelRepository;
            _electricityReadingService = electricityReadingService;
        }
        public ServiceResult<IEnumerable<Panel>> GetAll()
        {
            return ServiceResult<IEnumerable<Panel>>.SuccessResult(_panelRepository.GetAll().OrderBy(p => p.PanelType));
        }

        public ServiceResult<Panel> RegisterReading(string panelId, DateTime time, double wattage)
        {
            var panel = _panelRepository.GetById(panelId);
            var kiloWatts = wattage;
            if (wattage <= 0)
            {
                return ServiceResult<Panel>.ErrorResult("Valor incorrecto");
            }
            if (panel.MeasuringUnit == MeasuringUnit.Watt)
            {
                kiloWatts = wattage / 1000;
            }

            var lastReading = _electricityReadingService.GetLastReading().Result;
            var timeDelta = time - lastReading.ReadingDateTime;

            switch (panel.PanelType)
            {
                case PanelType.Regular:
                    if (timeDelta.TotalHours > 1)
                    {
                        _electricityReadingService.RegisterReading(panelId, time, kiloWatts);
                        return ServiceResult<Panel>.SuccessResult(panel);
                    }
                    return ServiceResult<Panel>.ErrorResult("No ha pasado una hora desde el ultimo registro");
                case PanelType.Limited:
                    if (timeDelta.TotalDays > 1 && kiloWatts > 5)
                    {
                        _electricityReadingService.RegisterReading(panelId, time, kiloWatts);
                        return ServiceResult<Panel>.SuccessResult(panel);
                    }
                    return ServiceResult<Panel>.ErrorResult("No ha pasado un dia desde el ultimo registro o potencia es menor a 5W");
                case PanelType.Ultimate:
                    if (timeDelta.TotalMinutes > 1 && kiloWatts < 5)
                    {
                        _electricityReadingService.RegisterReading(panelId, time, kiloWatts);
                        return ServiceResult<Panel>.SuccessResult(panel);
                    }
                    return ServiceResult<Panel>.ErrorResult("No ha pasado un minuto desde el ultimo registro o potencia es mayor a 5W");
                default:
                    return ServiceResult<Panel>.ErrorResult("Internal server error");
            }
        }
    }
}
