using Raspberry.IO.Components.Converters.Mcp3002;
using Glovebox.IoT.Base;
using Raspberry.IO.Components.Sensors.Temperature.Mcp9701a;

namespace Glovebox.RaspberryPi.IO.Sensors
{
	public class SensorMcp9701a : SensorBase
	{

		//		const ConnectorPin adcMosi = ConnectorPin.P1Pin19;
		//		const ConnectorPin adcMiso = ConnectorPin.P1Pin21;
		//		const ConnectorPin adcClock = ConnectorPin.P1Pin23;
		//		const ConnectorPin adcCs = ConnectorPin.P1Pin24;
		//		MemoryGpioConnectionDriver driver = new MemoryGpioConnectionDriver ();
		//GpioConnectionSettings.DefaultDriver;

		Mcp3002SpiConnection adcConnection;
		Mcp9701aConnection tmpConnection;

		/// <summary>
		/// Initializes a new instance of the <see cref="Glovebox.Raspberry.IoT.SensorLight"/> class.
		/// </summary>
		/// <param name="SampleRateMilliseconds">Sample rate milliseconds.</param>
		/// <param name="name">Name.</param>
		public SensorMcp9701a (Mcp3002SpiConnection adcConnection, int SampleRateMilliseconds, string name)
			: base ("temp", "c", ValuesPerSample.One, SampleRateMilliseconds, name)
		{

			this.adcConnection = adcConnection;

//			adcConnection = new Mcp3002SpiConnection (
//				driver.Out (adcClock), driver.Out (adcCs), driver.In (adcMiso), driver.Out (adcMosi));
//
			tmpConnection = new Mcp9701aConnection (
				adcConnection.In (Mcp3002Channel.Channel0), 
				UnitsNet.ElectricPotential.FromVolts (3.3));


			StartMeasuring ();
		}

		protected override void Measure (double[] value)
		{
			lock (adcConnection.spiLock) {
			//	value [0] = tmpConnection.GetTemperature ();
				value [0] = tmpConnection.GetTemperature ().DegreesCelsius;

			}
		}

		protected override string GeoLocation ()
		{
			return string.Empty;
		}

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>The current.</value>
		public override double Current {
			get { 				 
				lock (adcConnection.spiLock) {
				//	return tmpConnection.GetTemperature ();
					return tmpConnection.GetTemperature ().DegreesCelsius;
				}
			}
		}

		protected override void SensorCleanup ()
		{
			tmpConnection.Close ();
//			adcConnection.Close ();
		}
	}
}


