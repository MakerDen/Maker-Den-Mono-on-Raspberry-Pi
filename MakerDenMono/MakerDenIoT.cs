using System;
using System.Runtime.CompilerServices;
using Glovebox.IoT;
using Glovebox.IoT.Base;
using Glovebox.Netduino.Actuators;
using System.Threading;



namespace MakerDenMono {
	public class MakerBaseIoT {
		static public ServiceManager sm;
        static public LedDigital led;


		protected static void StartNetworkServices(string deviceName, bool connected, string uniqueDeviceIdentifier = "") {
			sm = Utilities.StartNetworkServices(deviceName, connected, uniqueDeviceIdentifier);
		}

		/// <summary>
		/// Blink an LED before measuring sensor
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		static public uint OnBeforeMeasure(object sender, EventArgs e) {
            if (led == null) { return 0; }
            led.On();
            Thread.Sleep(100);
            led.Off();
			return 0;
		}

		static public uint OnMeasureCompleted(object sender, EventArgs e) {
			if (sm == null) {
				Console.WriteLine(((SensorBase.SensorItemEventArgs)e).ToString());
				return 0;
			}

			// sensor mesurement completed, now publish to MQTT Service running on Azure
			var json = ((SensorBase.SensorItemEventArgs)e).ToJson();
			var topic = ((SensorBase.SensorItemEventArgs)e).topic;

			return sm.Publish(topic, json);
		}
	}
}
