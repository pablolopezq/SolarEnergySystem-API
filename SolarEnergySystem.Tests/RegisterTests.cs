using SolarEnergySystem.Core;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Tests.Builders;
using System;
using Xunit;

namespace SolarEnergySystem.Tests
{
    public class RegisterTests
    {
        [Fact]
        public void RegisterRegularPanelTestFails()
        {
            var builder = new PanelServiceTestBuilder();
            var service = builder.GetService();
            service.Setup(x => x.RegisterReading("abc", DateTime.UtcNow, 10))
                .Returns(ServiceResult<Panel>.ErrorResult("No ha pasado una hora desde el ultimo registro"));

            var controller = builder.Build(service.Object);

            var result = controller.RegisterReading("abc", 20);

            Assert.Equal("No ha pasado una hora desde el ultimo registro", result);
        }

        [Fact]
        public void RegisterLimitedPanelTestFails()
        {
            var builder = new PanelServiceTestBuilder();
            var service = builder.GetService();
            service.Setup(x => x.RegisterReading("abc", DateTime.UtcNow, 2))
                .Returns(ServiceResult<Panel>.ErrorResult("No ha pasado un dia desde el ultimo registro o potencia es menor a 5W"));

            var controller = builder.Build(service.Object);

            var result = controller.RegisterReading("abc", 2);

            Assert.Equal("No ha pasado un dia desde el ultimo registro o potencia es menor a 5W", result);
        }

        [Fact]
        public void RegisterUltimatePanelTestFails()
        {
            var builder = new PanelServiceTestBuilder();
            var service = builder.GetService();
            service.Setup(x => x.RegisterReading("abc", DateTime.UtcNow, 15))
                .Returns(ServiceResult<Panel>.ErrorResult("No ha pasado un minuto desde el ultimo registro o potencia es mayor a 5W"));

            var controller = builder.Build(service.Object);

            var result = controller.RegisterReading("abc", 15);

            Assert.Equal("No ha pasado un minuto desde el ultimo registro o potencia es mayor a 5W", result);
        }
    }
}
