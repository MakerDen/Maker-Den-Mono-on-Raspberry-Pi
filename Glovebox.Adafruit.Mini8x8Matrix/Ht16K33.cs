#region References

using Raspberry.IO.InterIntegratedCircuit;

#endregion

namespace Glovebox.Adafruit.Mini8x8Matrix
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

		/// <summary>
		/// Initializes a new instance of the <see cref="Pcf8574I2cConnection"/> class.
		/// </summary>
		/// <param name="connection">The connection.</param>
		public Ht16K33I2cConnection(I2cDeviceConnection connection)
		{
			this.connection = connection;
            FrameInit();
		}

        #region Ht16K33 I2C Control Methods


        public void Write(byte[] frame) {
			connection.Write(frame);
		}

        public void FrameSetBlinkRate(byte br) {
            Write(new byte[] { (byte)(0x80 | 0x01 | (byte)br), 0x00 });
		}

        public void FrameSetBrightness(byte level) {
			if (level > 15) { level = 15; }
            Write(new byte[] { (byte)(0xE0 | level), 0x00 });
		}

        private void FrameInit() {
            Write(new byte[] { 0x21, 0x00 });
            Write(new byte[] { 0xA0, 0x00 });
		}

		#endregion

    }
}