#region References

using System;
using UnitsNet;

#endregion



namespace Raspberry.IO.Components.Sensors.Temperature.Mcp9701a
{
	/// <summary>
	/// Represents a connection to a MCP9701A temperature sensor.
	/// </summary>
	/// <remarks>See <see cref="http://www.microchip.com/wwwproducts/Devices.aspx?dDocName=en022289"/>,
	/// <see cref="http://ww1.microchip.com/downloads/en/DeviceDoc/20001942F.pdf"/>,
	/// <see cref="http://au.rs-online.com/web/p/temperature-humidity-sensors/7387051/"/>.
	/// </remarks>
	public class Mcp9701aConnection : IDisposable
	{
		#region Constants

		const double TemperatureCoefficientMillivoltsPerDegreeC = 19.53;
		const double ZeroDegreesMillivoltOffset = 400 / TemperatureCoefficientMillivoltsPerDegreeC;
		const double CalibrationOffset = -3;

		#endregion

		#region Fields

		private readonly IInputAnalogPin inputPin;
		private readonly ElectricPotential referenceVoltage;

		#endregion

		#region Instance Management

		/// <summary>
		/// Initializes a new instance of the <see cref="Mcp9701aConnection"/> class.
		/// </summary>
		/// <param name="inputPin">The input pin.</param>
		/// <param name="referenceVoltage">The reference voltage.</param>
		public Mcp9701aConnection (IInputAnalogPin inputPin, ElectricPotential referenceVoltage)
		{
			this.inputPin = inputPin;
			this.referenceVoltage = referenceVoltage;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		void IDisposable.Dispose ()
		{
			Close ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the temperature.
		/// </summary>
		/// <returns>The temperature.</returns>
		public UnitsNet.Temperature GetTemperature ()
		{
			var milliVolts = (double)inputPin.Read ().Relative * (uint)referenceVoltage.Millivolts;
			var centigrade = (double)(milliVolts / TemperatureCoefficientMillivoltsPerDegreeC - ZeroDegreesMillivoltOffset) + CalibrationOffset;

			return UnitsNet.Temperature.FromDegreesCelsius (centigrade);
		}

		/// <summary>
		/// Closes this instance.
		/// </summary>
		public void Close ()
		{
			inputPin.Dispose ();
		}

		#endregion
	}
}
