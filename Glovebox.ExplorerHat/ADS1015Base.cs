using Raspberry.IO.InterIntegratedCircuit;

namespace Glovebox.ExplorerHat {
    public class ADS1015Base {

        protected static I2cDeviceConnection connection;

        public ADS1015Base(I2cDriver i2cDriver ) {
            if (connection != null) { return;}
            connection = i2cDriver.Connect(0x48);
        }
    }
}
