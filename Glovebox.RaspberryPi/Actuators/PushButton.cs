using Raspberry.IO.GeneralPurpose;
using System;
using System.Diagnostics;
using System.Threading;

namespace Glovebox.RaspberryPi.IO.Actuators {
    public class PushButton : IDisposable {

        ProcessStartInfo info = new ProcessStartInfo();
        IGpioConnectionDriver driver;
        ProcessorPin procPin;
        DateTime lastPushTime = DateTime.MinValue;
        int inputCount = 0;

        Thread matrix;

        public PushButton(IGpioConnectionDriver driver, ProcessorPin pin) {
            this.driver = driver;

            procPin = pin;
 

            driver.Allocate(procPin, PinDirection.Input);
            driver.SetPinDetectedEdges(procPin, PinDetectedEdges.Rising);
            driver.SetPinResistor(procPin, PinResistor.PullDown);

            info.FileName = "sudo";
            info.Arguments = "shutdown now -h";

            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;


            matrix = new Thread(new ThreadStart(this.RunSequence));
            matrix.Start();
        }

        private void RunSequence() {

            try {

                var isHigh = driver.Read(procPin);

               while (true) {
                    //var now = DateTime.Now;

                    //Console.WriteLine(now + "." + now.Millisecond.ToString("000") + ": " + (isHigh ? "HIGH" : "LOW"));

                   driver.Wait(procPin, !isHigh); //TODO: infinite
                   isHigh = driver.Read(procPin);
                  //  isHigh = !isHigh;
                    inputCount++;
                    Console.WriteLine("interupt: " + inputCount.ToString());
                    if (inputCount > 10) {
                        Console.WriteLine("shutdown");
                  //      var p = Process.Start(info);
                    }
                  //  var p = Process.Start(info);
                }
            }
            catch { }
            finally {
                // Leaves the pin unreleased so that other processes can keep reading
                //driver.Release(procPin);
            }
        }



        void IDisposable.Dispose() {

        }
    }
}
