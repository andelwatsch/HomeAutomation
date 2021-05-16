using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartHome.Model.Buttons;
using SmartHome.Model.Lights;

namespace SmartHome.Automation.Modules
{
    internal enum Dimm
    {
        Up,
        Down
    }

    public class ButtonLightModule
    {
        private readonly List<SpotLight> _lights;
        private readonly DimmableButton _button;
        private int _dimmValue;

        private bool _stopDimming;
        private bool _dimmingInProgress;

        public ButtonLightModule(DimmableButton button, List<SpotLight> lights)
        {
            _button = button;
            _lights = lights;
            _dimmValue = 0;
            _stopDimming = false;
        }

        public void StartAutomation()
        {
            _button.ButtonStateChanged += ButtonOnButtonStateChanged;
        }

        private void ButtonOnButtonStateChanged(ButtonState buttonState)
        {
            StopDimmingTask();

            switch (buttonState)
            {
                case ButtonState.Off:
                    _lights.ForEach(light => light.SetBrightness(0));
                    break;
                case ButtonState.On:
                    _lights.ForEach(light => light.SetBrightness(100));
                    break;
                case ButtonState.Decrease:
                    Task.Run(() => DimmLamp(Dimm.Down, TimeSpan.FromMilliseconds(100)));
                    break;
                case ButtonState.Increase:
                    Task.Run(() => DimmLamp(Dimm.Up, TimeSpan.FromMilliseconds(100)));
                    break;
            }
        }

        private void DimmLamp(Dimm dimm, TimeSpan waitTimeBetweenDimming)
        {
            _dimmingInProgress = true;
            while (!_stopDimming)
            {
                switch (dimm)
                {
                    case Dimm.Up:
                        if (_dimmValue < 100)
                        {
                            _dimmValue += 1;
                        }
                        else
                        {
                            _stopDimming = true;
                        }
                        break;
                    case Dimm.Down:
                        if (_dimmValue > 0)
                        {
                            _dimmValue -= 1;
                        }
                        else
                        {
                            _stopDimming = true;
                        }
                        break;
                }

                Thread.Sleep(waitTimeBetweenDimming);
            }

            _dimmingInProgress = false;
            _stopDimming = false;
        }

        private void StopDimmingTask()
        {
            if (_dimmingInProgress)
            {
                _stopDimming = true;

            }
        }
    }
}
