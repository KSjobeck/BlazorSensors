using System;

namespace BlazorSensors
{
    public class ErrorArgs: EventArgs
    {
        public string Name { get; }
        public string Message { get; }

        public ErrorArgs(string name, string message) =>
            (Name, Message) = (name, message);
    }
}
