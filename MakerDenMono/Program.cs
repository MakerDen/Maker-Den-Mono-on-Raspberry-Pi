using Glovebox.MicroFramework.Sensors;
using Glovebox.Netduino.Actuators;
using Glovebox.Raspberry.IoT;
using Glovebox.Raspberry.IoT.Sensors;
using Glovebox.RaspberryPi;
using Glovebox.RaspberryPi.Actuators;
using Glovebox.RaspberryPi.Actuators.AdaFruit8x8Matrix;
using System.Threading;

namespace MakerDenMono {
    class MainClass : MakerBaseIoT {      
        public static void Main(string[] args) {

            StartNetworkServices("Mono", true);

            using (Sys sys = new Sys("dgrpi2"))
            using (led = new LedDigital(gpioDriver, "led01"))
            using (AdaFruit8x8Matrix matrix = new AdaFruit8x8Matrix(i2cDriver))
            using (SensorCPUTemp cpuTemp = new SensorCPUTemp(10000, "cpu01"))
            using (SensorMemory mem = new SensorMemory(2000, "mem01"))
            using (SensorLight light = new SensorLight(adcConnection, 1000, "light01"))
            using (SensorSound sound = new SensorSound(adcConnection, 1000, "sound01"))
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
