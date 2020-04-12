# BlazorSensors
JSInterop for web api sensors

# Library Usage
- Add <script src="_content/BlazorSensors/sensors.js"></script> to wwwroot/index.html file of the file.
- Add @using BlazorSensors to _Imports.razor file

# Sensor Usage

## Step 1: Add SensorService
in program.cs add a singleton based on the sensors you need to use.
```csharp
builder.Services.AddSingleton<Accelerometer>();
```  
## Step 2: Inject SensorService
Inject the Sensor service in the pages it needs to be used in.

```html
@page "/counter"
@inject AbsoluteOrientationSensor sensor
<h1>Counter</h1>
```

## Step 3: Use the sensor
In This example the sensor is activated on the click of a button.
```html
<button class="btn btn-primary" @onclick="StartSensor">Start</button>
```

```csharp
    private async Task StartSensor()
    {
        if (!sensor.Activated)
        {
            (minX, minY, minZ) = (0, 0, 0);
            (maxX, maxY, maxZ) = (0, 0, 0);
            txt = "sensor started";
            sensor.Frequency = 60;
            sensor.OnReading += OnSensorReading;
            sensor.OnError += OnSensorError;
            await sensor.Start();
            StateHasChanged();
        }
    }

    private async Task StopSensor()
    {
        if (sensor.Activated)
            await sensor?.Stop();
    }

    private void OnSensorReading(object sender, EventArgs args)
    {
        txt = $"x:{sensor.X}; y:{sensor.Y}; z:{sensor.Z}, w:{sensor.W}";

        StateHasChanged();
    }


    private void OnSensorError(object sender, ErrorArgs e)
    {
        txt = $"Error: {e.Name}<br>{e.Message}";
        StateHasChanged();
    }
```
