using System;
using System.Threading.Tasks;

namespace BlazorSensors
{
    public interface ISensor
    {
        /// <summary>
        /// Returns a Boolean indicating whether the sensor is active.
        /// </summary>
        bool Activated { get; }

        /// <summary>
        /// Returns a Boolean indicating whether the sensor has a reading.
        /// </summary>
        bool HasReading { get; }

        /// <summary>
        /// Returns the time stamp of the latest sensor reading in a DOMHighResTimeStamp<br />
        /// See https://developer.mozilla.org/en-US/docs/Web/API/DOMHighResTimeStamp for details.
        /// </summary>
        double TimeStamp { get; }

        /// <summary>
        /// Called when an error occurs on one of the child interfaces of the Sensor interface.
        /// </summary>
        event EventHandler<ErrorArgs> OnError;
        /// <summary>
        /// Called when a reading is taken on one of the child interfaces of the Sensor interface.
        /// </summary>
        event EventHandler<EventArgs> OnReading;
        /// <summary>
        /// Called when one of the Sensor interface's becomes active.
        /// </summary>
        event EventHandler<EventArgs> OnActivate;

        /// <summary>
        /// Activates one of the sensors based on Sensor.
        /// </summary>
        Task Start();
        /// <summary>
        /// Deactivates one of the sensors based on Sensor.
        /// </summary>
        Task Stop();
    }
}
