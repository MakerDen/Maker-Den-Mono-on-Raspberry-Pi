using System;
using System.Runtime.CompilerServices;
using Glovebox.IoT;
using Glovebox.IoT.Base;
using Glovebox.Netduino.Actuators;
using System.Threading;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.Components.Converters.Mcp3002;
using Raspberry.IO.InterIntegratedCircuit;



namespace MakerDenMono {
    public class MakerBaseIoT {

        // SPI Pins
        const ConnectorPin adcMosi = ConnectorPin.P1Pin19;
        const ConnectorPin adcMiso = ConnectorPin.P1Pin21;
        const ConnectorPin adcClock = ConnectorPin.P1Pin23;
        const ConnectorPin adcCs = ConnectorPin.P1Pin24;

        // I2C Pins
        const ConnectorPin sdaPin = ConnectorPin.P1Pin03;
        const ConnectorPin sclPin = ConnectorPin.P1Pin05;

        static MemoryGpioConnectionDriver driver;
        protected static Mcp3002SpiConnection adcConnection;
        protected static I2cDriver i2cDriver;
        protected static IGpioConnectionDriver gpioDriver;


        static public ServiceManager sm;
        static public LedDigital led;

        public MakerBaseIoT() {
        }

        static protected void InitDrivers() {
            driver = new MemoryGpioConnectionDriver();
            adcConnection = new Mcp3002SpiConnection(driver.Out(adcClock), driver.Out(adcCs), driver.In(adcMiso), driver.Out(adcMosi));
            i2cDriver = new I2cDriver(sdaPin.ToProcessor(), sclPin.ToProcessor());
            gpioDriver = GpioConnectionSettings.DefaultDriver;
        }

        protected static void StartNetworkServices(string deviceName, bool connected, string uniqueDeviceIdentifier = "") {
            sm = Utilities.StartNetworkServices(deviceName, connected, uniqueDeviceIdentifier);
        }

        /// <summary>
        /// Blink an LED before measuring sensor
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        static public uint OnBeforeMeasure(object sender, EventArgs e) {
            if (led == null) { return 0; }
            led.On();
            Thread.Sleep(100);
            led.Off();
            return 0;
        }

        static public uint OnMeasureCompleted(object sender, EventArgs e) {
            if (sm == null) {
                Console.WriteLine(((SensorBase.SensorItemEventArgs)e).ToString());
                return 0;
            }

            // sensor mesurement completed, now publish to MQTT Service running on Azure
            var json = ((SensorBase.SensorItemEventArgs)e).ToJson();
            var topic = ((SensorBase.SensorItemEventArgs)e).topic;

            return sm.Publish(topic, json);
        }
    }
}
