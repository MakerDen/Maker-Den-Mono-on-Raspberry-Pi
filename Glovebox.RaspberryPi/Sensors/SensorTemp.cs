using Glovebox.IoT.Base;
using Raspberry.IO.InterIntegratedCircuit;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.Components.Converters.Pcf8591T;


namespace Glovebox.Raspberry.IoT
{
	public class SensorTemp : SensorBase
	{

		// I2C Pins
		const ConnectorPin sdaPin = ConnectorPin.P1Pin03;
		const ConnectorPin sclPin = ConnectorPin.P1Pin05;

		I2cDriver driver;
		Pcf8591TI2cConnection deviceConnection;

		public SensorTemp (int SampleRateMilliseconds, string name)
            : base("temp", "c", ValuesPerSample.One, SampleRateMilliseconds, name)
		{
			driver = new I2cDriver (sdaPin.ToProcessor (), sclPin.ToProcessor ());
			deviceConnection = new Pcf8591TI2cConnection (driver.Connect (0x49));

			StartMeasuring ();
		}

		protected override void Measure (double[] value)
		{
			value [0] = deviceConnection.GetReading (Pcf8591TI2cConnection.Register.A0);
		}

		protected override string GeoLocation ()
		{
			return string.Empty;
		}

		public override double Current {
			get { return deviceConnection.GetReading (Pcf8591TI2cConnection.Register.A0); }
		}

		protected override void SensorCleanup ()
		{
			driver.Dispose ();
		}
	}
}

