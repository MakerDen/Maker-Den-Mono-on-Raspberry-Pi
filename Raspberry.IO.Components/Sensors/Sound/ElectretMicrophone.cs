using System;
using UnitsNet;

namespace Raspberry.IO.Components.Sensors.Sound.Electret
{
	/// <summary>
	/// Represents a connection to a TMP36 temperature sensor.
	/// </summary>
	/// <remarks>See <see cref="http://learn.adafruit.com/send-raspberry-pi-data-to-cosm"/>.</remarks>
	public class ElectretConnection : IDisposable
	{
		#region Fields

		private readonly IInputAnalogPin analogPin;

		const int numberOfSamples = 32;
		const int averagedOver = 8;
		const int midpoint = 512;
		int runningAverage = 0;
		int sample;

		#endregion

		#region Instance Management

		/// <summary>
		/// Initializes a new instance of the <see cref="Tmp36Connection"/> class.
		/// </summary>
		/// <param name="inputPin">The input pin.</param>
		/// <param name="referenceVoltage">The reference voltage.</param>
		public ElectretConnection(IInputAnalogPin analogPin)
		{
			this.analogPin = analogPin;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		void IDisposable.Dispose()
		{
			Close();
		}

		#endregion

		#region Methods

		public int SampleSound(){
			int sumOfSamples = 0;
			int averageReading = 0;

			for (int i = 0; i < numberOfSamples; i++) {
				var value = (int)(analogPin.Read().Value - midpoint);
				value = Math.Abs (value);
				sumOfSamples += value;

			}

			averageReading = sumOfSamples / numberOfSamples;
			runningAverage = (((averagedOver - 1) * runningAverage) + averageReading) / averagedOver;

			return runningAverage;
		}



		/// <summary>
		/// Closes this instance.
		/// </summary>
		public void Close()
		{
			analogPin.Dispose();
		}

		#endregion
	}
}
