using Glovebox.MicroFramework.Sensors;
using Glovebox.Netduino.Actuators;
using Glovebox.Raspberry.IoT;
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

            using (PushButton button = new PushButton(gpioDriver, ProcessorPin.Pin12))
            using (Sys sys = new Sys("dgrpi2"))
            using (led = new LedDigital(gpioDriver, "led01"))
            using (AdaFruit8x8Matrix matrix = new AdaFruit8x8Matrix(i2cDriver))
            using (SensorCPUTemp cpuTemp = new SensorCPUTemp(10000, "cpu01"))
            using (SensorMemory mem = new SensorMemory(2000, "mem01"))
            using (SensorLight light = new SensorLight(adcConnection, 1000, "light01"))
            //		using (SensorSound sound = new SensorSound (adcConnection, 1000, "sound01")) 
            //		using (SensorTmp36 temp = new SensorTmp36 (adcConnection, 15000, "temp01")) 
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
