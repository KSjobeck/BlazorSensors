using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorSensors
{
    /// <summary>
    /// The LinearAccelerationSensor interface of the Sensor APIs provides on each reading<br />the acceleration applied to the device along all three axes, but without the contribution of gravity.
    /// </summary>
    public class LinearAccelerationSensor : ISensor
    {
        /// <summary>
        /// Returns a double containing the linear acceleration of the device along the device's x axis.
        /// </summary>
        public double X { get; private set; }
        /// <summary>
        /// Returns a double containing the linear acceleration of the device along the device's y axis.
        /// </summary>
        public double Y { get; private set; }
        /// <summary>
        /// Returns a double containing the linear acceleration of the device along the device's z axis.
        /// </summary>
        public double Z { get; private set; }

        public bool Activated { get; private set; }

        public bool HasReading { get; private set; }

        public double TimeStamp { get; private set; }

        public double Frequency { get; set; }

        public event EventHandler<ErrorArgs> OnError;
        public event EventHandler<EventArgs> OnReading;
        public event EventHandler<EventArgs> OnActivate;


        private readonly IJSRuntime _jsRuntime;

        public LinearAccelerationSensor(IJSRuntime jsRuntime) =>
            _jsRuntime = jsRuntime;


        public async Task Start()
        {
            await _jsRuntime.InvokeAsync<object>(
                "sensors.StartLinearAccelerationSensor",
                Frequency,DotNetObjectReference.Create(this));
            Activated = true;
            OnActivate?.Invoke(this, EventArgs.Empty);
        }

        public async Task Stop()
        {
            await _jsRuntime.InvokeAsync<object>(
                "sensors.StopLinearAccelerationSensor");
            Activated = false;
        }

        //CallBacks
        [JSInvokable]
        public void SensorData(double x, double y, double z, double timestamp)
        {
            (X, Y, Z, TimeStamp) = (x, y, z, timestamp);
            OnReading?.Invoke(this, EventArgs.Empty);
        }

        [JSInvokable]
        public void SensorError(string name, string message)=>
            OnError?.Invoke(this, new ErrorArgs(name,message));
    }
}