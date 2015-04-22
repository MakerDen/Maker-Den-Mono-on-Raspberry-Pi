using Glovebox.Adafruit.Mini8x8Matrix;
using Glovebox.ExplorerHat;
using Glovebox.RaspberryPi.IO.Actuators;
using Glovebox.RaspberryPi.IO.Sensors;
using Raspberry.IO.GeneralPurpose;
using System.Threading;

namespace MakerDenMono {
    class MainClass : MakerBaseIoT {
        public static void Main(string[] args) {

            InitializeDrivers();

            LedDigital[] leds = new LedDigital[4];

            using (ADS1015 adc = new ADS1015(i2cDriver))
            using (Adafruit8x8Matrix miniMatrix = new Adafruit8x8Matrix(new Ht16K33I2cConnection(i2cDriver.Connect(0x70)), "matrix"))
            using (AdaFruitMatrixRunV2 matrix = new AdaFruitMatrixRunV2(miniMatrix))        
            
            using (leds[0] = new LedDigital(gpioDriver, ProcessorPin.Pin4, "led00"))
            using (leds[1] = new LedDigital(gpioDriver, ProcessorPin.Pin17, "led01"))
            using (leds[2] = new LedDigital(gpioDriver, ProcessorPin.Pin27, "led02"))
            using (leds[3] = new LedDigital(gpioDriver, ProcessorPin.Pin5, "led03")) {
                while (true) {
                    System.Console.WriteLine(adc.Sample(ADS1015.Channel.A3));
                    for (int i = 0; i < leds.Length; i++) {
                        leds[i].On();
                        Thread.Sleep(5);
                    }
                    for (int i = 0; i < leds.Length; i++) {
                        leds[i].Off();
                        Thread.Sleep(5);
                    }
                }
            }

            //         StartNetworkServices("RPiB", true, "dgrpib");

            using (Sys sys = new Sys("system"))
            using (SensorCPUTemp cpuTemp = new SensorCPUTemp(10000, "cpu01"))
            using (SensorMemory mem = new SensorMemory(2000, "mem01"))
            using (led = new LedDigital(gpioDriver, ProcessorPin.Pin17, "led01"))
            using (Relay relay = new Relay(gpioDriver, ProcessorPin.Pin06, "relay01"))
            using (Adafruit8x8Matrix miniMatrix = new Adafruit8x8Matrix(new Ht16K33I2cConnection(i2cDriver.Connect(0x70)), "matrix"))
            using (AdaFruitMatrixRunV2 matrix = new AdaFruitMatrixRunV2(miniMatrix))
            //          using (SensorLight light = new SensorLight(spiConnection, 1000, "light01"))
            //       using (SensorMcp9701a tempMcp9701a = new SensorMcp9701a(spiConnection, 15000, "temp02")) 
     
     {

                cpuTemp.OnBeforeMeasurement += OnBeforeMeasure;
                cpuTemp.OnAfterMeasurement += OnMeasureCompleted;

                mem.OnBeforeMeasurement += OnBeforeMeasure;
                mem.OnAfterMeasurement += OnMeasureCompleted;

                //light.OnBeforeMeasurement += OnBeforeMeasure;
                //light.OnAfterMeasurement += OnMeasureCompleted;

                //tempMcp9701a.OnBeforeMeasurement += OnBeforeMeasure;
                //tempMcp9701a.OnAfterMeasurement += OnMeasureCompleted;

                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}
