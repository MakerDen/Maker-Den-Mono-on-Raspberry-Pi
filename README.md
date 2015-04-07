# IoT-Maker-Den-Mono-.NET-on-Raspberry-Pi2
IoT Maker Den migrated and implemented on Mono .NET on Raspberry Pi 2

Documentation in progress

Please refer to the [IoT Maker Den on .NET Micro Framework on Netduino documentaion](https://github.com/MakerDen/IoT-Maker-Den-NETMF/blob/master/README.md)

This project can run with or without the configurted Prototype shield.  See the code examples below for running barebones or fully configured Maker Den Prototype shield.

#Setting up Mono on Raspberry Pi

More to come, but for now refer to this excellent article of setting up Mono on your Raspberry Pi.[Getting started with the Raspberry Pi 2, for .NET developers](http://j.tlns.be/2015/02/getting-started-with-the-raspberry-pi-2-for-net-developers/)


# What is the Internet of Things Solution Accelerator?

The Internet of Things Solution Accelerator for the .NET Micro Framework provides a pluggable foundation to support sensors, actuators, data serialisation, communications, and command and control. 


![Alt text](https://github.com/MakerDen/IoT-Maker-Den-Mono-.NET-on-Raspberry-Pi-2/blob/master/MakerDenMono/Lab%20Code/Assets/Internet%20of%20Things%20Maker%20Den.jpg)



## Extensible/pluggable framework supporting

1. Sensors
 * Physical: Light, Sound, Temperature, CPU Temperature
 * Virtual: Memory Usage, Diagnostics
 * Sensor data serialised to a JSON schema

2. Actuators
 * LED, Relay
 * Adafruit Mini 0.8" 8x8 LED Matrix
     - Low and high level pixel frame transformation primitives 
     - alphanumeric character and symbol drawing and scrolling capability 
3. Command and Control
 * Control relays, send messages to a Adafruit Mini 8x8 Matrix etc via the communications layer

4. Communications
 * Pluggable – currently implemented on MQTT ([Mosquitto](http://mosquitto.org) MQTT Server running on Azure)

5. Supported and Tested
 * Raspberry Pi 2 and B+
 * [Visual Studio 2013 Community Edition, Express Desktop, Std, Pro and above](https://www.visualstudio.com/en-us/visual-studio-homepage-vs.aspx)
 * [Targets .NET 4.5 on Mono .NET on Raspberry Pi](http://www.mono-project.com/)


## IoT Dashboard
The IoT Dashboard allows you to visualise the data streamed to Azure. 

![IoT Dashboard](https://github.com/MakerDen/IoT-Maker-Den-Mono-.NET-on-Raspberry-Pi-2/blob/master/MakerDenMono/Lab%20Code/Assets/IoTDashboard.JPG)

You can install the IoT Dashboard from [here](http://iotmakerdendashboard.azurewebsites.net/install/publish.htm).  Note, you will need to allow to run from unknown publisher.

#Prototype Board Layout


##Wiring and Component Layout

![Wiring and Components](https://github.com/MakerDen/IoT-Maker-Den-Mono-.NET-on-Raspberry-Pi-2/blob/master/MakerDenMono/Lab%20Code/Assets/Component%20Layout.JPG)

## Complete Board

Click the image to see Maker Den on Raspberry Pi in Action. The Raspberry Pi 2 on the right, Netduino 2 Plus on the left.

![Complete with components](https://github.com/MakerDen/IoT-Maker-Den-Mono-.NET-on-Raspberry-Pi-2/blob/master/MakerDenMono/Lab%20Code/Assets/Component%20Complete.JPG)

<a href="https://www.youtube.com/embed/krNViqufGW4" target="_blank">Watch Maker Den on Raspberry Pi in action on YouTube</a>

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

#### Raspbery Pi Barebones without a Prototype Board

No prototype board required.

Connect Ethernet port to router/internet.

This example does the following

1. Start network services and posts sensor data via MQTT to IoT Dashboard
1. Loads Sys for remote shutdown and reboot via MQTT, 
2. Reports CPU Temperature, 
3. Reports .NET Working set memory.  


		using Glovebox.Adafruit.Mini8x8Matrix;
		using Glovebox.RaspberryPi.IO.Actuators;
		using Glovebox.RaspberryPi.IO.Sensors;
		using Raspberry.IO.GeneralPurpose;
		using System.Threading;

		namespace MakerDenMono {
			class MainClass : MakerBaseIoT {
				public static void Main(string[] args) {

					InitializeDrivers();

					StartNetworkServices("mono", true);

					using (Sys sys = new Sys("rpi"))
					using (SensorCPUTemp cpuTemp = new SensorCPUTemp(10000, "cpu01"))
					using (SensorMemory mem = new SensorMemory(2000, "mem01")) {

						cpuTemp.OnBeforeMeasurement += OnBeforeMeasure;
						cpuTemp.OnAfterMeasurement += OnMeasureCompleted;

						mem.OnBeforeMeasurement += OnBeforeMeasure;
						mem.OnAfterMeasurement += OnMeasureCompleted;

						Thread.Sleep(Timeout.Infinite);
					}
				}
			}
		}


#### Raspberry Pi with Maker Den Configured Prototype Board

Connect Ethernet port to router/internet.

This example does the following

1. Start network services and posts sensor data via MQTT to IoT Dashboard
1. Loads Sys for remote shutdown and reboot via MQTT, 
2. Reports CPU Temperature, 
3. Reports .NET Working set memory.  
4. Blinks LED for every sensor measurement
5. Relay support with remote on/off via MQTT
6. Adafruit Mini LED 8x8 Matrix running test pattern
7. LDR based light level sensor
8. Temperature sensor 

        using Glovebox.Adafruit.Mini8x8Matrix;
        using Glovebox.RaspberryPi.IO.Actuators;
        using Glovebox.RaspberryPi.IO.Sensors;
        using Raspberry.IO.GeneralPurpose;
        using System.Threading;

        namespace MakerDenMono {
            class MainClass : MakerBaseIoT {
                public static void Main(string[] args) {

                    InitializeDrivers();

                    StartNetworkServices("mono", true);

                    using (Sys sys = new Sys("rpi"))
                    using (SensorCPUTemp cpuTemp = new SensorCPUTemp(10000, "cpu01"))
                    using (SensorMemory mem = new SensorMemory(2000, "mem01"))
                    using (led = new LedDigital(gpioDriver, ProcessorPin.Pin13, "led01"))
                    using (Relay relay = new Relay(gpioDriver, ProcessorPin.Pin06, "relay01"))
                    using (AdaFruitMatrixRun matrix = new AdaFruitMatrixRun(i2cDriver.Connect(0x70)))
                    using (SensorLight light = new SensorLight(spiConnection, 1000, "light01"))
                    using (SensorMcp9701a tempMcp9701a = new SensorMcp9701a(spiConnection, 15000, "temp02")) {

                        cpuTemp.OnBeforeMeasurement += OnBeforeMeasure;
                        cpuTemp.OnAfterMeasurement += OnMeasureCompleted;

                        mem.OnBeforeMeasurement += OnBeforeMeasure;
                        mem.OnAfterMeasurement += OnMeasureCompleted;

                        light.OnBeforeMeasurement += OnBeforeMeasure;
                        light.OnAfterMeasurement += OnMeasureCompleted;

                        tempMcp9701a.OnBeforeMeasurement += OnBeforeMeasure;
                        tempMcp9701a.OnAfterMeasurement += OnMeasureCompleted;

                        Thread.Sleep(Timeout.Infinite);
                    }
                }
            }
        }

