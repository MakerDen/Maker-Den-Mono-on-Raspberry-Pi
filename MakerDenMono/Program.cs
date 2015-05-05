using Glovebox.Adafruit.Mini8x8Matrix;
using Glovebox.ExplorerHat;
using Glovebox.RaspberryPi.IO.Actuators;
using Glovebox.RaspberryPi.IO.Sensors;
using Glovebox.RaspberryPi.Sensors;
using Raspberry.IO.GeneralPurpose;
using System.Threading;

namespace MakerDenMono {
    class MainClass : MakerBaseIoT {
        static Thread blink;
        static LedDigital green;

        private static void BlickThread() {
            const int delay = 50;

            while (true) {
                green.On();
                Thread.Sleep(delay);
                green.Off();
                Thread.Sleep(delay);
            }
        }

        public static void Main(string[] args) {

            blink = new Thread(new ThreadStart(BlickThread));
            blink.Priority = ThreadPriority.Normal;

            InitializeDrivers();

            StartNetworkServices("RPi2", true, "dgrpib");

            var connection = i2cDriver.Connect(0x48);

            green = new LedDigital(gpioDriver, ProcessorPin.Pin12, "green");

            blink.Start();

            LedDigital[] leds = new LedDigital[4];

            using (Sys sys = new Sys("system"))
            using (ADS1015 ldr = new ADS1015(i2cDriver, ADS1015.Channel.A3))
            using (ADS1015 temp = new ADS1015(i2cDriver, ADS1015.Channel.A4))
            using (Adafruit8x8Matrix miniMatrix = new Adafruit8x8Matrix(new Ht16K33I2cConnection(i2cDriver.Connect(0x70)), "matrix"))
            using (AdaFruitMatrixRunV2 matrix = new AdaFruitMatrixRunV2(miniMatrix))
            using (SensorTempI2C tempI2c = new SensorTempI2C(temp, 1000, "temp01"))

            using (leds[0] = new LedDigital(gpioDriver, ProcessorPin.Pin4, "led00"))
            using (leds[1] = new LedDigital(gpioDriver, ProcessorPin.Pin17, "led01"))
            using (leds[2] = new LedDigital(gpioDriver, ProcessorPin.Pin27, "led02"))
            using (leds[3] = new LedDigital(gpioDriver, ProcessorPin.Pin5, "led03")) 
            
            {

                tempI2c.OnBeforeMeasurement += OnBeforeMeasure;
                tempI2c.OnAfterMeasurement += OnMeasureCompleted;



                while (true) {
                    //           System.Console.WriteLine(ldr.GetMillivolts(ADS1015.ProgrammableGain.Volt33));
                    //            System.Console.WriteLine(temp.Sample(ADS1015.ProgrammableGain.Volt33));
                    for (int i = 0; i < leds.Length; i++) {
                        leds[i].On();
                    }
                    Thread.Sleep(4);
                    for (int i = 0; i < leds.Length; i++) {
                        leds[i].Off();
                    }
                    Thread.Sleep(1000);
                }

                Thread.Sleep(Timeout.Infinite);
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
