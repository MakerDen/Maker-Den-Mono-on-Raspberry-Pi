#region References

using System;
using System.Globalization;
using Raspberry.IO.InterIntegratedCircuit;

#endregion

namespace Glovebox.RaspberryPi.Drivers
{
	/// <summary>
	/// Represents a I2C connection to a PCF8574 I/O Expander.
	/// </summary>
	/// <remarks>See <see cref="http://www.ti.com/lit/ds/symlink/pcf8574.pdf"/> for more information.</remarks>
	public class Ht16K33I2cConnection
	{
		#region Fields

		private readonly I2cDeviceConnection connection;

		#endregion

        public enum BlinkRate: byte {
			Off    = 0x00,  
			Fast    = 0x02, //2hz
			Medium  = 0x04, //1hz
			Slow    = 0x06, //0.5 hz
		}

		#region Instance Management

		/// <summary>
		/// Initializes a new instance of the <see cref="Pcf8574I2cConnection"/> class.
		/// </summary>
		/// <param name="connection">The connection.</param>
		public Ht16K33I2cConnection(I2cDeviceConnection connection)
		{
			this.connection = connection;
            FrameInit();
		}

		#endregion

		#region Methods


		protected void FrameUpdate(byte[] frame) {
			connection.Write(frame);
		}

        protected void FrameSetBlinkRate(BlinkRate br) {
            FrameUpdate(new byte[] { (byte)(0x80 | 0x01 | (byte)br), 0x00 });
		}

        protected void FrameSetBrightness(byte level) {
			if (level > 15) { level = 15; }
            FrameUpdate(new byte[] { (byte)(0xE0 | level), 0x00 });
		}

        private void FrameInit() {
            FrameUpdate(new byte[] { 0x21, 0x00 });
            FrameUpdate(new byte[] { 0xA0, 0x00 });
		}

		#endregion

	}
}