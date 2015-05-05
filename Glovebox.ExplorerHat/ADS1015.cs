using Raspberry.IO.InterIntegratedCircuit;
using System;
using System.Threading;

//ADS1015 ADC on I2C Bus
// see data sheet at http:// ???

namespace Glovebox.ExplorerHat {
    public class ADS1015 : ADS1015Base, IDisposable {
        private static object deviceLock = new object();
        Channel channel;

        private const int REG_CONV = 0x00;
        private const int REG_CFG = 0x01;

        ushort config = 0;
        byte[] data = new byte[3];

        public enum SamplesPerSecond {
            SPS128,
            SPS250,
            SPS490,
            SPS920,
            SPS1600,
            SPS2400,
            SPS3300
        }

        private ushort[] SamplePerSecondMap = { 0x0000, 0x0020, 0x0040, 0x0060, 0x0080, 0x00A0, 0x00C0 };
        private ushort[] SamplesPerSecondRate = { 128, 250, 490, 920, 1600, 2400, 3300 };
        public enum Channel {
            A4 = 0x4000,
            A3 = 0x5000,
            A2 = 0x6000,
            A1 = 0x7000
        }

        //https://learn.adafruit.com/adafruit-4-channel-adc-breakouts/programming
        ushort[] programmableGainMap = { 0x0000, 0x0200, 0x0400, 0x0600, 0x0800, 0x0A00 };
        ushort[] programmableGain_Scaler = { 6144, 4096, 2048, 1024, 512, 256 };

        public enum ProgrammableGain {
            //   PGA_6_144V = 0, //6144,  //0
            Volt5 = 0,
            Volt33 = 1,
            //   PGA_4_096V = 1, //4096,  //1
            PGA_2_048V = 2, //2048,  //2
            PGA_1_024V = 3, //1024,  //3
            PGA_0_512V = 4, //512,   //4
            PGA_0_256V = 5, //256,   //5
        }

        public ADS1015(I2cDriver i2cDriver, Channel channel)
            : base(i2cDriver) {
            this.channel = channel;
            // Set disable comparator and set "single shot" mode	
            config = 0x0003 | 0x8000; // | 0x100;
        }

        public double GetMillivolts(ProgrammableGain gain = ProgrammableGain.Volt5, SamplesPerSecond sps = SamplesPerSecond.SPS1600) {
            byte[] result;

            config |= (ushort)SamplePerSecondMap[(int)sps];
            config |= (ushort)channel;
            config |= (ushort)programmableGainMap[(int)gain];

            data[0] = REG_CFG;
            data[1] = (byte)((config >> 8) & 0xFF);
            data[2] = (byte)(config & 0xFF);

            lock (deviceLock) {
                connection.Write(data);
                // delay in milliseconds
                //int delay = (1000.0 / SamplesPerSecondRate[(int)sps] + .1;
                int delay = 5;
                Thread.Sleep(delay);

                connection.WriteByte((byte)REG_CONV);
                result = connection.Read(2);
            }

            var mv = (ushort)(((result[0] << 8) | result[1]) >> 4) * programmableGain_Scaler[(int)gain] / 2048;

            System.Console.WriteLine("Millivolts: " + mv.ToString());

            return mv;

        }

        void IDisposable.Dispose() {

        }
    }
}
