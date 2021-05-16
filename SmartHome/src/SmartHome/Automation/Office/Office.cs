using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SmartHome.Automation.Modules;
using SmartHome.Entities.Buttons;
using SmartHome.Entities.Lights;
using SmartHome.Model.Buttons;
using SmartHome.Model.Lights;

namespace SmartHome.Automation.Office
{
    public class Office : IHostedService
    {
        private readonly AllLights _allLights;
        private readonly AllButtons _allButtons;
        private readonly ButtonLightModule _buttonLightModule;

        public Office(AllLights allLights, AllButtons allButtons)
        {
            _allLights = allLights;
            _allButtons = allButtons;
            _buttonLightModule = new ButtonLightModule(allButtons.DimAllLightsButton, new List<SpotLight>() {allLights.BueroSpot});
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _buttonLightModule.StartAutomation();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
