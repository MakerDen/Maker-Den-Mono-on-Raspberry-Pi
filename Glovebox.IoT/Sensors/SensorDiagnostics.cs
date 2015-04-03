using Glovebox.IoT.Base;
using Glovebox.IoT.Command;

namespace Glovebox.IoT.Sensors {
    public class SensorDiagnostics : SensorBase {

        public SensorDiagnostics(int SampleRateMilliseconds, string name)
            : base("diag", "g", ValuesPerSample.Four, SampleRateMilliseconds, name) {

                StartMeasuring();
        }

        protected override void Measure(double[] value) {
            value[0] = TotalSensorMeasurements;
            value[1] = SensorErrorCount;
            value[2] = IotActionManager.TotalActions;
            value[3] = IotActionManager.ActionErrorCount;
        }

        protected override string GeoLocation() {
            return string.Empty;
        }

        public override double Current {
            get { return TotalSensorMeasurements; } 
        }

        protected override void SensorCleanup() {        
        }
    }
}
