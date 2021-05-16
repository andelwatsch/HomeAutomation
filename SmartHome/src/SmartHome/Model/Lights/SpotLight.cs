using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ioBroker.net;

namespace SmartHome.Model.Lights
{
    public class SpotLight
    {
        private readonly IIoBrokerDotNet _ioBrokerConnection;
        private readonly string _brightnessId;

        public SpotLight(string stateId, IIoBrokerDotNet ioBrokerConnection)
        {
            _ioBrokerConnection = ioBrokerConnection;
            _brightnessId = $"{stateId}.brightness";
        }

        public async Task SetBrightness(int brightness)
        {
            await _ioBrokerConnection.SetStateAsync(_brightnessId, brightness);
        }

    }
}
