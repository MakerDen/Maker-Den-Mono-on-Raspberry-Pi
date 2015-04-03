using Glovebox.IoT.Base;
using Raspberry.IO.Components.Converters.Mcp3002;
using Raspberry.IO.Components.Sensors.Sound.Electret;


namespace Glovebox.Raspberry.IoT.Sensors
{
	public class SensorSound : SensorBase
	{
//		const ConnectorPin adcMosi = ConnectorPin.P1Pin19;
//		const ConnectorPin adcMiso = ConnectorPin.P1Pin21;
//		const ConnectorPin adcClock = ConnectorPin.P1Pin23;
//		const ConnectorPin adcCs = ConnectorPin.P1Pin24;
//		MemoryGpioConnectionDriver driver = new MemoryGpioConnectionDriver();
		//GpioConnectionSettings.DefaultDriver;

		Mcp3002SpiConnection adcConnection;
		ElectretConnection ElectretConnection;

		/// <summary>
		/// Initializes a new instance of the <see cref="Glovebox.Raspberry.IoT.SensorLight"/> class.
		/// </summary>
		/// <param name="SampleRateMilliseconds">Sample rate milliseconds.</param>
		/// <param name="name">Name.</param>
		public SensorSound(Mcp3002SpiConnection adcConnection, int SampleRateMilliseconds, string name)
			: base("sound", "d", ValuesPerSample.One, SampleRateMilliseconds, name)
		{
			this.adcConnection = adcConnection;
//			adcConnection = new Mcp3002SpiConnection(
//				driver.Out(adcClock), driver.Out(adcCs), driver.In(adcMiso), driver.Out(adcMosi));
//
			ElectretConnection = new ElectretConnection(
				adcConnection.In (Mcp3002Channel.Channel0));

			StartMeasuring();
		}

		protected override void Measure(double[] value)
		{
			lock (adcConnection.spiLock) {
				value [0] = (double)ElectretConnection.SampleSound ();
				//	value [0] = (double)lightConnection.GetResistance ().ToLux ();
				//			value[0] = (double)lightConnection.GetPercentage();
			}
		}

		protected override string GeoLocation()
		{
			return string.Empty;
		}

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>The current.</value>
		public override double Current
		{
			get
			{ 
				lock (adcConnection.spiLock) {
					return (double)ElectretConnection.SampleSound ();
				}
			}
		}

		protected override void SensorCleanup()
		{
			ElectretConnection.Close();
//			adcConnection.Close();
		}
	}
}

