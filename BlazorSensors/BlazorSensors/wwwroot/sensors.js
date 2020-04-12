window.sensors = {
    LinearAccelerationSensor: null,
    ProximitySensor: null,
    AccelerometerSensor: null,
    AmbientLightSensor: null,
    GyroscopeSensor: null,
    MagnetometerSensor: null,
    AbsoluteOrientationSensor: null,
    RelativeOrientationSensor:null,
    StartLinearAccelerationSensor: function (frequency, instance) {

        if (window.sensors.LinearAccelerationSensor === null) {
            window.sensors.LinearAccelerationSensor = new LinearAccelerationSensor({ frequency: frequency });
            window.sensors.LinearAccelerationSensor.onreading = () => {
                let sensor = window.sensors.LinearAccelerationSensor;
                instance.invokeMethodAsync('SensorData', sensor.x, sensor.y, sensor.z, sensor.timestamp);
            }
            window.sensors.LinearAccelerationSensor.onerror = event => instance.invokeMethodAsync('SensorError', event.error.name, event.error.message);
            window.sensors.LinearAccelerationSensor.start();
        }
    },
    StopLinearAccelerationSensor: function(){
        if (window.sensors.LinearAccelerationSensor !== null) {
            window.sensors.LinearAccelerationSensor.stop();
            window.sensors.LinearAccelerationSensor = null;
        }
    },

    StartAccelerometer: function (frequency, instance) {

        if (window.sensors.AccelerometerSensor === null) {
            window.sensors.AccelerometerSensor = new Accelerometer({ frequency: frequency });
            window.sensors.AccelerometerSensor.onreading = () => {
                let sensor = window.sensors.AccelerometerSensor;
                instance.invokeMethodAsync('SensorData', sensor.x, sensor.y, sensor.z, sensor.timestamp);
            }
            window.sensors.AccelerometerSensor.onerror = event => instance.invokeMethodAsync('SensorError', event.error.name, event.error.message);
            window.sensors.AccelerometerSensor.start();
        }
    },
    StopAccelerometer: function () {
        if (window.sensors.AccelerometerSensor !== null) {
            window.sensors.AccelerometerSensor.stop();
            window.sensors.AccelerometerSensor = null;
        }
    },

    StartAbsoluteOrientationSensor: function (frequency,referenceFrame, instance) {
        if (window.sensors.AbsoluteOrientationSensor === null) {
            const options = { frequency: frequency, referenceFrame: referenceFrame };
            window.sensors.AbsoluteOrientationSensor = new AbsoluteOrientationSensor(options);
            window.sensors.AbsoluteOrientationSensor.onreading = () => {
                let sensor = window.sensors.AbsoluteOrientationSensor;
                instance.invokeMethodAsync('SensorData', sensor.quaternion[0], sensor.quaternion[1], sensor.quaternion[2], sensor.quaternion[3], sensor.timestamp);
            }
            window.sensors.AbsoluteOrientationSensor.onerror = event => instance.invokeMethodAsync('SensorError', event.error.name, event.error.message);
            window.sensors.AbsoluteOrientationSensor.start();
        }
    },
    StopAbsoluteOrientationSensor: function () {
        if (window.sensors.AbsoluteOrientationSensor !== null) {
            window.sensors.AbsoluteOrientationSensor.stop();
            window.sensors.AbsoluteOrientationSensor = null;
        }
    },

    StartRelativeOrientationSensor: function (frequency, referenceFrame, instance) {
        if (window.sensors.RelativeOrientationSensor === null) {
            const options = { frequency: frequency, referenceFrame: referenceFrame };
            window.sensors.RelativeOrientationSensor = new RelativeOrientationSensor(options);
            window.sensors.RelativeOrientationSensor.onreading = () => {
                let sensor = window.sensors.RelativeOrientationSensor;
                instance.invokeMethodAsync('SensorData', sensor.quaternion[0], sensor.quaternion[1], sensor.quaternion[2], sensor.quaternion[3], sensor.timestamp);
            }
            window.sensors.RelativeOrientationSensor.onerror = event => instance.invokeMethodAsync('SensorError', event.error.name, event.error.message);
            window.sensors.RelativeOrientationSensor.start();
        }
    },
    StopRelativeOrientationSensor: function () {
        if (window.sensors.RelativeOrientationSensor !== null) {
            window.sensors.RelativeOrientationSensor.stop();
            window.sensors.RelativeOrientationSensor = null;
        }
    },

    StartGyroscope: function (frequency, instance) {

        if (window.sensors.GyroscopeSensor === null) {
            window.sensors.GyroscopeSensor = new Gyroscope({ frequency: frequency });
            window.sensors.GyroscopeSensor.onreading = () => {
                let sensor = window.sensors.GyroscopeSensor;
                instance.invokeMethodAsync('SensorData', sensor.x, sensor.y, sensor.z, sensor.timestamp);
            }
            window.sensors.GyroscopeSensor.onerror = event => instance.invokeMethodAsync('SensorError', event.error.name, event.error.message);
            window.sensors.GyroscopeSensor.start();
        }
    },
    StopGyroscope: function () {
        if (window.sensors.GyroscopeSensor !== null) {
            window.sensors.GyroscopeSensor.stop();
            window.sensors.GyroscopeSensor = null;
        }
    },

    StartMagnetometer: function (frequency, instance) {

        if (window.sensors.MagnetometerSensor === null) {
            window.sensors.MagnetometerSensor = new Magnetometer({ frequency: frequency });
            window.sensors.MagnetometerSensor.onreading = () => {
                let sensor = window.sensors.MagnetometerSensor;
                instance.invokeMethodAsync('SensorData', sensor.x, sensor.y, sensor.z, sensor.timestamp);
            }
            window.sensors.MagnetometerSensor.onerror = event => instance.invokeMethodAsync('SensorError', event.error.name, event.error.message);
            window.sensors.MagnetometerSensor.start();
        }
    },
    StopMagnetometer: function () {
        if (window.sensors.MagnetometerSensor !== null) {
            window.sensors.MagnetometerSensor.stop();
            window.sensors.MagnetometerSensor = null;
        }
    },

    StartAmbientLightSensor: function (instance) {

        navigator.permissions.query({ name: 'ambient-light-sensor' }).then(function (result) {
            if (result.state === 'granted') {
                if ('AmbientLightSensor' in window) {
                    if (window.sensors.AmbientLightSensor === null) {
                        window.sensors.AmbientLightSensor = new AmbientLightSensor();
                        window.sensors.AmbientLightSensor.onreading = () => {
                            let sensor = window.sensors.AccelerometerSensor;
                            instance.invokeMethodAsync('SensorData', sensor.illuminance);
                        };
                        window.sensors.AmbientLightSensor.onerror = event => instance.invokeMethodAsync('SensorError', event.error.name, event.error.message);
                        window.sensors.AmbientLightSensor.start();
                    }
                }
            } else {
                console.error("Permision to use AmbientLightSensor denied");
            }
            // Don't do anything if the permission was denied.
        });


        
    },
    StopAmbientLightSensor: function () {
        if (window.sensors.AccelerometerSensor !== null) {
            window.sensors.AccelerometerSensor.stop();
            window.sensors.AccelerometerSensor = null;
        }
    },

    StartProximitySensor: function (instance) {

        if ('ondeviceproximity' in window) {
            window.addEventListener('deviceproximity', function (event) {
                instance.invokeMethodAsync('SensorData', event.value, event.min, event.max);
            });
        } else {
            // API not supported
            instance.invokeMethodAsync('SensorError', 'API not supported', 'API not supported');
        }

        
    }
}