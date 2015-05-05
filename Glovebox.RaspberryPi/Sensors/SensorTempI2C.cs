using Glovebox.ExplorerHat;
using Glovebox.IoT.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glovebox.RaspberryPi.Sensors {
    public class SensorTempI2C : SensorBase {

        ADS1015 adc;
        //const double TemperatureCoefficientMillivoltsPerDegreeC = 19.53;
        //const double ZeroDegreesMillivoltOffset = 460 / TemperatureCoefficientMillivoltsPerDegreeC;

        const double TemperatureCoefficientMillivoltsPerDegreeC = 10.0;
        const double ZeroDegreesMillivoltOffset = 500 / TemperatureCoefficientMillivoltsPerDegreeC;

        const double CalibrationOffset = 0;

        public SensorTempI2C(ADS1015 adc, int SampleRateMilliseconds, string name)
            : base("temp", "c", ValuesPerSample.One, SampleRateMilliseconds, name) {
                this.adc = adc;
                StartMeasuring();
        }

        protected override void Measure(double[] value) {
            var temp = GetTemperature(adc.GetMillivolts(ADS1015.ProgrammableGain.Volt33));
            value[0] = temp.DegreesCelsius;
            Console.WriteLine(value[0].ToString());
        }

        public UnitsNet.Temperature GetTemperature(double voltage) {
            var milliVolts = voltage;
          //  Console.WriteLine("millivolts: " + milliVolts.ToString());
        //    var centigrade = (double)(milliVolts / TemperatureCoefficientMillivoltsPerDegreeC - ZeroDegreesMillivoltOffset) + CalibrationOffset;

            var centigrade = (double)((milliVolts - 545) / 10) + CalibrationOffset; // / TemperatureCoefficientMillivoltsPerDegreeC - ZeroDegreesMillivoltOffset) + CalibrationOffset;

            return UnitsNet.Temperature.FromDegreesCelsius(centigrade);
        }

        protected override string GeoLocation() {
            return string.Empty;
        }

        public override double Current {
            get { throw new NotImplementedException(); }
        }

        protected override void SensorCleanup() {
        }
    }
}
