using Raspberry.IO.InterIntegratedCircuit;

namespace Glovebox.ExplorerHat {
    public class CAP1208 {
        protected I2cDeviceConnection connection;

        enum RegisterMap {
            R_MAIN_CONTROL = 0x00,
            R_GENERAL_STATUS = 0x02,
            R_INPUT_STATUS = 0x03,
            R_LED_STATUS = 0x04,
            R_NOISE_FLAG_STATUS = 0x0A,
        }

        enum InputDelta {
            R_INPUT_1_DELTA = 0x10,
            R_INPUT_2_DELTA = 0x11,
            R_INPUT_3_DELTA = 0x12,
            R_INPUT_4_DELTA = 0x13,
            R_INPUT_5_DELTA = 0x14,
            R_INPUT_6_DELTA = 0x15,
            R_INPUT_7_DELTA = 0x16,
            R_INPUT_8_DELTA = 0x17,
        }

        enum General {
            R_SENSITIVITY = 0x1F,
            R_GENERAL_CONFIG = 0x20,
            R_INPUT_ENABLE = 0x21,
            R_INPUT_CONFIG = 0x22,
            R_INPUT_CONFIG2 = 0x23, //Default 0x0000011
            PID_CAP1208 = 0x6B,
            PID_CAP1188 = 0x50,
        }

        enum InteruptRepeatRate {
            R_SAMPLING_CONFIG = 0x24, // # Default 0x00111001
            R_CALIBRATION = 0x26, //# Default 0b00000000
            R_INTERRUPT_EN = 0x27, //# Default 0b11111111
            R_REPEAT_EN = 0x28, //# Default 0b11111111
            R_MTOUCH_CONFIG = 0x2A, //# Default 0b11111111
            R_MTOUCH_PAT_CONF = 0x2B,
            R_MTOUCH_PATTERN = 0x2D,
            R_COUNT_O_LIMIT = 0x2E,
            R_RECALIBRATION = 0x2F,
        }

        enum TouchThreshold {
            R_INPUT_1_THRESH = 0x30,
            R_INPUT_2_THRESH = 0x31,
            R_INPUT_3_THRESH = 0x32,
            R_INPUT_4_THRESH = 0x33,
            R_INPUT_5_THRESH = 0x34,
            R_INPUT_6_THRESH = 0x35,
            R_INPUT_7_THRESH = 0x36,
            R_INPUT_8_THRESH = 0x37,
            R_NOISE_THRESH = 0x38,
        }

        enum StandbyRegisters {
            R_STANDBY_CHANNEL = 0x40,
            R_STANDBY_CONFIG = 0x41,
            R_STANDBY_SENS = 0x42,
            R_STANDBY_THRESH = 0x43,
            R_CONFIGURATION2 = 0x44,
        }

        enum InputReferenceCount {
            R_INPUT_1_BCOUNT = 0x50,
            R_INPUT_2_BCOUNT = 0x51,
            R_INPUT_3_BCOUNT = 0x52,
            R_INPUT_4_BCOUNT = 0x53,
            R_INPUT_5_BCOUNT = 0x54,
            R_INPUT_6_BCOUNT = 0x55,
            R_INPUT_7_BCOUNT = 0x56,
            R_INPUT_8_BCOUNT = 0x57,
        }


        public CAP1208(I2cDriver i2cDriver) {
            connection = i2cDriver.Connect(0x28);
        }
    }
}
