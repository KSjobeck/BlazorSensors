using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorSensors
{
    public class AbsoluteOrientationSensor : ISensor
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double W { get; private set; }
        public bool Activated { get; private set; }

        public bool HasReading { get; private set; }

        public double TimeStamp { get; private set; }

        public double Frequency { get; set; }
        public string ReferenceFrame = "device";

        public event EventHandler<ErrorArgs> OnError;
        public event EventHandler<EventArgs> OnReading;
        public event EventHandler<EventArgs> OnActivate;

        private readonly IJSRuntime _jsRuntime;

        public AbsoluteOrientationSensor(IJSRuntime jSRuntime) => _jsRuntime = jSRuntime;

        public async Task Start()
        {
            await _jsRuntime.InvokeAsync<object>(
                "sensors.StartAbsoluteOrientationSensor",
                Frequency,ReferenceFrame, DotNetObjectReference.Create(this));
            Activated = true;
            OnActivate?.Invoke(this, EventArgs.Empty);
        }

        public async Task Stop()
        {
            await _jsRuntime.InvokeAsync<object>(
                "sensors.StopAbsoluteOrientationSensor");
            Activated = false;
        }

        //CallBacks
        [JSInvokable]
        public void SensorData(double x, double y, double z, double w, double timestamp)
        {
            (X, Y, Z, W, TimeStamp) = (x, y, z, w, timestamp);
            OnReading?.Invoke(this, EventArgs.Empty);
        }

        [JSInvokable]
        public void SensorError(string name, string message) =>
            OnError?.Invoke(this, new ErrorArgs(name, message));
    }
}