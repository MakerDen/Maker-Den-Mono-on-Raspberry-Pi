using System;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Text;



namespace Glovebox.IoT {
    public static class Utilities {

        const string ntpServer = "au.pool.ntp.org";
        private readonly static string[] postcodes = new string[] { "3000", "6000", "2011" };
        private static Random rnd = new Random(Environment.TickCount);
        const int networkSettleTime = 1000;

        /// <summary>
        /// Randoms the number.
        /// </summary>
        /// <returns>The number.</returns>
        /// <param name="Range">Range.</param>
        public static int RandomNumber(int Range) {
            return rnd.Next(Range);
        }			

        /// <summary>
        /// Randoms the postcode.
        /// </summary>
        /// <returns>The postcode.</returns>
        public static string RandomPostcode() {
            return postcodes[rnd.Next(postcodes.Length)];
        }

        /// <summary>
        /// Byteses to string.
        /// </summary>
        /// <returns>The to string.</returns>
        /// <param name="Input">Input.</param>
        public static string BytesToString(byte[] Input) {
            char[] Output = new char[Input.Length];
            for (int Counter = 0; Counter < Input.Length; ++Counter) {
                Output[Counter] = (char)Input[Counter];
            }
            return new string(Output);
        }

        /// <summary>
        /// convert string to byte array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string str) {
            return new UTF8Encoding().GetBytes(str);
        }

        /// <summary>
        /// Starts the network services.
        /// </summary>
        /// <returns>The network services.</returns>
        /// <param name="deviceName">Device name.</param>
        /// <param name="connected">If set to <c>true</c> connected.</param>
        /// <param name="deviceGuid">Device GUID.</param>
        static public ServiceManager StartNetworkServices(string deviceName, bool connected, string deviceGuid) {
            ConfigurationManager.DeviceName = deviceName;
            ConfigurationManager.UniqueDeviceIdentifier = deviceGuid;
            if (!connected) { return null; }
            return new ServiceManager(ConfigurationManager.Broker, connected);
        }

        /// <summary>
        /// Gets the mac address.
        /// </summary>
        /// <returns>The mac address.</returns>
        public static string GetMacAddress() {
		

            return string.Empty;
        }

        private static string MacToString(byte[] macAddress) {
            string result = string.Empty;
            foreach (var part in macAddress) {
                result += part.ToString("X") + "-";
            }
            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// Absolute the specified value.
        /// </summary>
        /// <param name="value">Value.</param>
        public static int Absolute(int value) {
            // get absolute value bit shifting a 32 bit int
            int mask = value >> 31;
            return (mask + value) ^ mask;
        }
    }
}
