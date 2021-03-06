
namespace Glovebox.IoT {
    public static class ConfigurationManager {
        // Best efforts to run the MQTT Broker at gloveboxAE.cloudapp.net 
        // You can install your own instance of Mosquitto MQTT Server from http://mosquitto.org 
        public static string Broker = "gloveboxAE.cloudapp.net";

        public static string NetworkId { get; set; }

        private static string _devId = "emul";
        public static string DeviceId {
            get { return _devId; }
            set {
                _devId = value == null || value.Length == 0 ? "emul" : value;
                _devId = _devId.Length > 5 ? _devId.Substring(0, 5) : _devId;
            }
        }

        public static string MqttNameSpace = "gb/";
        public static string[] MqqtSubscribe = new string[] { "gbcmd/#" };
        public static string MqttDeviceAnnounce = "gbdevice/";

        public static uint mqttPrePublishDelay = 0;  // milliseconds delay before mqtt publish
        public static uint mqttPostPublishDelay = 0; // milliseconds delay after mqtt publish
    }
}
