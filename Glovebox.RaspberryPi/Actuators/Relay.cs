using Glovebox.IoT.Base;
using Glovebox.IoT.Command;
using Raspberry.IO.GeneralPurpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glovebox.RaspberryPi.IO.Actuators {
    public class Relay : ActuatorBase {
        public enum Actions {
            On,
            Off
        }

        ConnectorPin userPin = ConnectorPin.P1Pin33;
        ProcessorPin procPin;
        public IGpioConnectionDriver driver;

        /// <summary>
        /// Create a relay control
        /// </summary>
        /// <param name="pin">From the SecretLabs.NETMF.Hardware.NetduinoPlus.Pins namespace</param>
        /// <param name="name">Unique identifying name for command and control</param>
        public Relay(IGpioConnectionDriver driver, ProcessorPin procPin, string name)
            : base(name, "relay") {

            this.procPin = procPin;
            this.driver = driver;

            driver.Allocate(procPin, PinDirection.Output);
            driver.SetPinResistor(procPin, PinResistor.PullUp);
        }

        protected override void ActuatorCleanup() {

        }

        public void Action(Actions action) {
            switch (action) {
                case Actions.On:
                    TurnOn();
                    break;
                case Actions.Off:
                    TurnOff();
                    break;
                default:
                    break;
            }
        }

        public override void Action(IotAction action) {
            switch (action.cmd) {
                case "on":
                    TurnOn();
                    break;
                case "off":
                    TurnOff();
                    break;
            }
        }

        public void TurnOn() {
            driver.Write(procPin, true);
        }

        public void TurnOff() {
            driver.Write(procPin, false);
        }
    }
}
