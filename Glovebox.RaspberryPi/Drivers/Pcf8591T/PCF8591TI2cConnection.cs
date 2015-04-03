#region References

using Raspberry.IO.InterIntegratedCircuit;

#endregion

namespace Raspberry.IO.Components.Converters.Pcf8591T
{
    /// <summary>
	/// Represents a I2C connection to a Pcf8591T I/O Expander.
    /// </summary>
    /// <remarks>See <see cref="http://www.adafruit.com/datasheets/mcp23017.pdf"/> for more information.</remarks>
    public class Pcf8591TI2cConnection
    {
        #region Fields

        private readonly I2cDeviceConnection connection;

        #endregion

        #region Instance Management

        /// <summary>
        /// Initializes a new instance of the <see cref="Mcp23017I2cConnection"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public Pcf8591TI2cConnection(I2cDeviceConnection connection)
        {
            this.connection = connection;
        }

        #endregion

        #region Methods

   


        /// <summary>
        /// Gets the pin status.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <returns>The pin status.</returns>
        public byte GetReading(Register register)
        {
           // var register = ((int) pin & 0x0100) == 0x0000 ? Register.GPIOA : Register.GPIOB;
            register = Register.A0;
            connection.WriteByte((byte) register);
            var status = connection.ReadByte();

            return status;
        }

    

        #endregion

        #region Private Helpers

        public enum Register
        {
            A0 = 0x40,
            A1 = 0x41,
            A2 = 0x42,
            A3 = 0x43,
        }

        #endregion
    }
}