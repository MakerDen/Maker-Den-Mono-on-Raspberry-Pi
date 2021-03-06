using System;
using UnitsNet;

namespace Glovebox.RaspberryPi.IO.Sensors
{
	internal static class Convert
	{
        #region Methods

		public static decimal ToLux (this ElectricResistance variableResistor)
		{
			// See http://learn.adafruit.com/photocells/using-a-photocell
			// and http://www.emant.com/316002.page

			const decimal luxRatio = 500000;
			return luxRatio / (decimal)variableResistor.Ohms;
		}

        #endregion
	}
}

