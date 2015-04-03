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
 * Physical: Light, Sound, Temperature
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




# Parts List
This is a peliminary parts list with links to sites where I purchased the parts for the Maker Den on Mono (.NET) on Raspberry Pi 2 (also supports Raspberry Pi B+).  The links are for your reference only and in no way are these sites affiliated with Microsoft.


1. [Raspberry Pi 2](http://www.raspberrypi.org/)
2. [MCP3002-I/P, 10 bit ADC Differential - 2 x ADC Channels](http://au.rs-online.com/web/p/general-purpose-adcs/6696054/)
2. [MCP9701A-E/TO, 8 bit Analogue Temperature Sensor](http://au.rs-online.com/web/p/temperature-humidity-sensors/7387051)
3. [Light Dependent Resistor](http://au.rs-online.com/web/p/ldr-light-dependent-resistors/4558036)
4. [Prototyping Shield for Raspberry Pi Model B+](http://raspberry.piaustralia.com.au/collections/crusts-add-ons/products/prototyping-shield-for-raspberry-pi-model-b)
5. [Breadboard Mini Self-Adhesive](http://littlebirdelectronics.com.au/products/breadboard-mini-selfadhesive)
6. [Rubber Feet - Small Stick On - Pk.4 (or a case)](http://www.jaycar.com.au/PRODUCTS/Enclosures-%26-Panel-Hardware/Panel-Hardware/Equipment-Feet/Rubber-Feet---Small-Stick-On---Pk-4/p/HP0815)
7. [Adafruit Mini 0.8" 8x8 LED Matrix](http://littlebirdelectronics.com.au/products/adafruit-mini-0-8-8x8-led-matrix-w-i2c-backpack-yellow-green)
7. 1 x 22k ohm resistor for Light Dependent Resistor



