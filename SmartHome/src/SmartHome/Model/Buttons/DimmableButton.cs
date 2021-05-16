using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ioBroker.net;
using Newtonsoft.Json.Linq;

namespace SmartHome.Model.Buttons
{
    public class DimmableButton
    {
        private readonly IIoBrokerDotNet _ioBrokerConnection;

        public DimmableButton(string stateId, IIoBrokerDotNet ioBrokerConnection)
        {
            _ioBrokerConnection = ioBrokerConnection;
            _ioBrokerConnection.SubscribeStateAsync<JArray>(stateId, ButtonStateChangedHandler);

        }

        public event Action<ButtonState> ButtonStateChanged;

        private void ButtonStateChangedHandler(JArray buttonState)
        {
            var firstNumber = buttonState[0][0].Value<int>();
            var secondNumber = buttonState[1].Value<int>();

            var currentButtonState = (firstNumber, secondNumber) switch
            {
                (0, 0) => ButtonState.Off,
                (0, 1) => ButtonState.Decrease,
                (1, 0) => ButtonState.Increase,
                (1, 1) => ButtonState.On,
                _ => throw new NotImplementedException(),
            };

            ButtonStateChanged?.Invoke(currentButtonState);
        }
    }
}
