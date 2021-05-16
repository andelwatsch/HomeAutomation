using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ioBroker.net;
using SmartHome.Model.Buttons;

namespace SmartHome.Entities.Buttons
{
    public class AllButtons
    {
        public AllButtons(IIoBrokerDotNet ioBrokerConnection)
        {
            DimAllLightsButton = new DimmableButton("IoBroker.Id", ioBrokerConnection);
        }

        public DimmableButton DimAllLightsButton { get; private set; }
    }
}
