using ioBroker.net;
using SmartHome.Model.Lights;

namespace SmartHome.Entities.Lights
{
    public class AllLights
    {
        public AllLights(IIoBrokerDotNet ioBrokerConnection)
        {
            BueroSpot = new SpotLight("IoBroker.Id", ioBrokerConnection);
        }

        public SpotLight BueroSpot { get; private set; }
    }
}
