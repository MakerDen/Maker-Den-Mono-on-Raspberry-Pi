using Glovebox.IoT.Base;
using System;
using System.Diagnostics;

namespace Glovebox.RaspberryPi.IO.Sensors {
    public class SensorMemory : SensorBase {

		Random rnd = new Random();
		Process p = Process.GetCurrentProcess();


        public SensorMemory(int SampleRateMilliseconds, string name)
            : base("mempi", "b", ValuesPerSample.One, SampleRateMilliseconds, name) {

                StartMeasuring();
        }

        protected override void Measure(double[] value) {


		//	Console.WriteLine(p.WorkingSet64.ToString ());

			value[0] = p.WorkingSet64 / 100000;
			

        }

        protected override string GeoLocation() {
            return string.Empty;        }

        public override double Current {
            get { return  rnd.Next(50000, 60000); }
        }

        protected override void SensorCleanup() {
			p.Dispose();
        }
    }
}
