using Glovebox.IoT.Base;
using System.Diagnostics;

namespace Glovebox.RaspberryPi.IO.Sensors
{
	public class SensorCPUTemp : SensorBase
	{
		ProcessStartInfo info = new ProcessStartInfo ();

		public SensorCPUTemp (int SampleRateMilliseconds, string name)
            : base("cputmp", "c", ValuesPerSample.One, SampleRateMilliseconds, name)
		{

			info.FileName = "cat";
			info.Arguments = "/sys/class/thermal/thermal_zone0/temp";

			info.UseShellExecute = false;
			info.CreateNoWindow = true;

			info.RedirectStandardOutput = true;
			info.RedirectStandardError = true;

			StartMeasuring ();
		}

		protected override void Measure (double[] value)
		{
			value [0] = MeasureCPUTemp ();			

		}

		protected override string GeoLocation ()
		{
			return string.Empty;
		}

		public override double Current {
			get { return  MeasureCPUTemp (); }
		}

		protected override void SensorCleanup ()
		{

		}

		private double MeasureCPUTemp ()
		{
			double val;
			var p = Process.Start (info);

			var result = p.StandardOutput.ReadToEnd ();

			p.WaitForExit ();

			p.Dispose ();

		//	Console.WriteLine (result);

			if (double.TryParse (result, out val)) {
				return val / 1000;
			} else {
				return 0;
			}
		}
	}
}

