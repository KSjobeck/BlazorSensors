using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorSensors
{
    public class AmbientLightSensor : ISensor
    {
        /// <summary>
        /// Returns the current light level in lux of the ambient light level around the hosting device.
        /// </summary>
        public double Illuminance { get; set; }
        public bool Activated { get; private set; }

        public bool HasReading { get; private set; }

        public double TimeStamp => throw new NotImplementedException();

        public event EventHandler<ErrorArgs> OnError;
        public event EventHandler<EventArgs> OnReading;
        public event EventHandler<EventArgs> OnActivate;

        private readonly IJSRuntime _jsRuntime;

        public AmbientLightSensor(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;


        public async Task Start()
        {
            await _jsRuntime.InvokeAsync<object>(
                "sensors.StartAmbientLightSensor",
                DotNetObjectReference.Create(this));
            Activated = true;
            OnActivate?.Invoke(this, EventArgs.Empty);
        }

        public async Task Stop()
        {
            await _jsRuntime.InvokeAsync<object>(
                "sensors.StopAmbientLightSensor");
            Activated = false;
        }

        //CallBacks
        [JSInvokable]
        public void SensorData(double lux)
        {
            Illuminance = lux;
            OnReading?.Invoke(this, EventArgs.Empty);
        }

        [JSInvokable]
        public void SensorError(string name, string message) =>
            OnError?.Invoke(this, new ErrorArgs(name, message));
    }
}
