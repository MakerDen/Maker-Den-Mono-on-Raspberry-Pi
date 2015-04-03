# IoT-Maker-Den-Mono-.NET-on-Raspberry-Pi2
IoT Maker Den migrated and implemented on Mono .NET on Raspberry Pi 2

Documentation in progress

Please refer to the [IoT Maker Den on .NET Micro Framework on Netduino documentaion](https://github.com/MakerDen/IoT-Maker-Den-NETMF/blob/master/README.md)

#Setting up Mono on Raspberry Pi

More to come, but for now refer to this excellent article of setting up Mono on your Raspberry Pi.[Getting started with the Raspberry Pi 2, for .NET developers](http://j.tlns.be/2015/02/getting-started-with-the-raspberry-pi-2-for-net-developers/)


# What is the Internet of Things Solution Accelerator?

The Internet of Things Solution Accelerator for the .NET Micro Framework provides a pluggable foundation to support sensors, actuators, data serialisation, communications, and command and control. 


![Alt text](https://github.com/MakerDen/IoT-Maker-Den-NETMF/blob/master/MakerDen/Lab%20Code/Maker%20Den%20IoT%20Framework.jpg)



## Extensible/pluggable framework supporting

1. Sensors
 * Physical: Light, Sound, Temperature, CPU Temperature
 * Virtual: Memory Usage, Diagnostics
 * Sensor data serialised to a JSON schema

2. Actuators
 * LED, 
 * Adafruit Mini 0.8" 8x8 LED Matrix
     - Low and high level pixel frame transformation primitives 
     - alphanumeric character and symbol drawing and scrolling capability 
3. Command and Control
 * Control relays, start NeoPixels etc via the communications layer

4. Communications
 * Pluggable – currently implemented on MQTT ([Mosquitto](http://mosquitto.org) MQTT Server running on Azure)

5. Supported and Tested
 * Raspberry Pi 2 and B+
 * [Visual Studio 2013 Community Edition, Express Desktop, Std, Pro and above](https://www.visualstudio.com/en-us/visual-studio-homepage-vs.aspx)
 * [Targets .NET 4.5 on Mono .NET on Raspberry Pi](http://www.mono-project.com/)


## IoT Dashboard
The IoT Dashboard allows you to visualise the data streamed to Azure. 

![IoT Dashboard](https://github.com/MakerDen/IoT-Maker-Den-NETMF/blob/master/MakerDen/Lab%20Code/IoTDashboard.JPG)

You can install the IoT Dashboard from [here](http://iotmakerdendashboard.azurewebsites.net/install/publish.htm).  Note, you will need to allow to run from unknown publisher.

#Prototype Board Layout



# Parts List
All parts are generally available worldwide.  This is a peliminary parts list with links to sites where I purchased the parts for the Maker Den on Mono (.NET) on Raspberry Pi 2 (also supports Raspberry Pi B+).  The links are for your reference only and in no way are these sites affiliated with Microsoft.


1. [Raspberry Pi 2](http://www.raspberrypi.org/)
2. [MCP3002-I/P, 10 bit ADC Differential - 2 x ADC Channels](http://au.rs-online.com/web/p/general-purpose-adcs/6696054/)
2. [MCP9701A-E/TO, 8 bit Analogue Temperature Sensor](http://au.rs-online.com/web/p/temperature-humidity-sensors/7387051)
3. [Light Dependent Resistor](http://au.rs-online.com/web/p/ldr-light-dependent-resistors/4558036)
4. [Prototyping Shield for Raspberry Pi Model B+](http://raspberry.piaustralia.com.au/collections/crusts-add-ons/products/prototyping-shield-for-raspberry-pi-model-b)
5. [Breadboard Mini Self-Adhesive](http://littlebirdelectronics.com.au/products/breadboard-mini-selfadhesive)
6. [Rubber Feet - Small Stick On - Pk.4 (or a case)](http://www.jaycar.com.au/PRODUCTS/Enclosures-%26-Panel-Hardware/Panel-Hardware/Equipment-Feet/Rubber-Feet---Small-Stick-On---Pk-4/p/HP0815)
7. [Adafruit Mini 0.8" 8x8 LED Matrix](http://littlebirdelectronics.com.au/products/adafruit-mini-0-8-8x8-led-matrix-w-i2c-backpack-yellow-green)
7. 1 x 22k ohm resistor for Light Dependent Resistor

Optional
1. [Electret Microphone Breakout Board](http://littlebirdelectronics.com.au/products/electret-microphone-breakout-board)



## Programming Models

### Declarative Event Driven Model

    using Glovebox.MicroFramework.Sensors;
    using Glovebox.Netduino.Actuators;
    using Glovebox.Raspberry.IoT;
    using Glovebox.Raspberry.IoT.Sensors;
    using Glovebox.RaspberryPi;
    using Glovebox.RaspberryPi.Actuators;
    using Glovebox.RaspberryPi.Actuators.AdaFruit8x8Matrix;
    using Glovebox.RaspberryPi.Actuators.PushButton;
    using Raspberry.IO.Components.Converters.Mcp3002;
    using Raspberry.IO.GeneralPurpose;
    using Raspberry.IO.InterIntegratedCircuit;
    using System.Threading;

    namespace MakerDenMono {
        class MainClass : MakerBaseIoT {
            // SPI Pins
            const ConnectorPin adcMosi = ConnectorPin.P1Pin19;
            const ConnectorPin adcMiso = ConnectorPin.P1Pin21;
            const ConnectorPin adcClock = ConnectorPin.P1Pin23;
            const ConnectorPin adcCs = ConnectorPin.P1Pin24;

            // I2C Pins
            const ConnectorPin sdaPin = ConnectorPin.P1Pin03;
            const ConnectorPin sclPin = ConnectorPin.P1Pin05;

            static MemoryGpioConnectionDriver driver = new MemoryGpioConnectionDriver();

            public static void Main(string[] args) {

                StartNetworkServices("Mono", true);

                var adcConnection = new Mcp3002SpiConnection(
                    driver.Out(adcClock), driver.Out(adcCs), driver.In(adcMiso), driver.Out(adcMosi));
                I2cDriver i2cDriver = new I2cDriver(sdaPin.ToProcessor(), sclPin.ToProcessor());
                IGpioConnectionDriver gpioDriver = GpioConnectionSettings.DefaultDriver;

                using (Sys sys = new Sys("dgrpi2"))
                using (led = new LedDigital(gpioDriver, "led01"))
                using (AdaFruit8x8Matrix matrix = new AdaFruit8x8Matrix(i2cDriver))
                using (SensorCPUTemp cpuTemp = new SensorCPUTemp(10000, "cpu01"))
                using (SensorMemory mem = new SensorMemory(2000, "mem01"))
                using (SensorLight light = new SensorLight(adcConnection, 1000, "light01"))
                using (SensorSound sound = new SensorSound (adcConnection, 1000, "sound01")) 
                using (SensorMcp9701a tempMcp9701a = new SensorMcp9701a(adcConnection, 15000, "temp02")) {

                    mem.OnBeforeMeasurement += OnBeforeMeasure;
                    mem.OnAfterMeasurement += OnMeasureCompleted;

                    cpuTemp.OnBeforeMeasurement += OnBeforeMeasure;
                    cpuTemp.OnAfterMeasurement += OnMeasureCompleted;

                    light.OnBeforeMeasurement += OnBeforeMeasure;
                    light.OnAfterMeasurement += OnMeasureCompleted;

                    tempMcp9701a.OnBeforeMeasurement += OnBeforeMeasure;
                    tempMcp9701a.OnAfterMeasurement += OnMeasureCompleted;

                    Thread.Sleep(Timeout.Infinite);
                }
            }
        }
    }



