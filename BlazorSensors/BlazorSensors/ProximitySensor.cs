using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorSensors
{
    public class ProximitySensor : ISensor
    {

        /// <summary>
        /// Represents the proximity of the device to an object in centimeters
        /// </summary>
        public double Value { get; private set; }
        /// <summary>
        /// Describes the minimum distance the sensor can detect, in centimeters.
        /// </summary>
        public double Min { get; private set; }
        /// <summary>
        /// Describes the maximum distance the sensor can detect, in centimeters
        /// </summary>
        public double Max { get; private set; }

        public bool Activated { get; private set; }

        public bool HasReading => throw new NotImplementedException();

        public double TimeStamp => throw new NotImplementedException();

        public event EventHandler<ErrorArgs> OnError;
        public event EventHandler<EventArgs> OnReading;
        public event EventHandler<EventArgs> OnActivate;

        private readonly IJSRuntime _jsRuntime;

        public ProximitySensor(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;

        public async Task Start()
        {
            await _jsRuntime.InvokeAsync<object>(
                "sensors.StartProximitySensor",
                DotNetObjectReference.Create(this));
            Activated = true;
            OnActivate?.Invoke(this, EventArgs.Empty);
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }

        //CallBacks
        [JSInvokable]
        public void SensorData(double value, double min, double max)
        {
            (Value, Min, Max) = (value, min, max);
            OnReading?.Invoke(this, EventArgs.Empty);
        }

        [JSInvokable]
        public void SensorError(string name, string message) =>
            OnError?.Invoke(this, new ErrorArgs(name, message));
    }
}
