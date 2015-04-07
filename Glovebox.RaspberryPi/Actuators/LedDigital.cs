using Glovebox.IoT.Base;
using Glovebox.IoT.Command;
using Raspberry.IO.GeneralPurpose;
using System;
using System.Threading;


namespace Glovebox.RaspberryPi.IO.Actuators {
    public class LedDigital : ActuatorBase {

        ProcessorPin procPin;
        ledState ts = new ledState();

        class ledState {
            public uint blinkMilliseconds = 0;
            public int BlinkMillisecondsToDate;
            public bool ledOn = false;
            public IGpioConnectionDriver led;
            public Timer MyTimer;
            public int blinkRateMilliseconds;
            public bool running = false;
        }

     

        public enum BlinkRate {
            Slow,
            Medium,
            Fast,
            VeryFast
        }

        /// <summary>
        /// Simnple Led control
        /// </summary>
        /// <param name="pin">From the SecretLabs.NETMF.Hardware.NetduinoPlus.Pins namespace</param>
        /// <param name="name">Unique identifying name for command and control</param>
        public LedDigital(IGpioConnectionDriver driver, ProcessorPin procPin, string name)
            : base(name, "led") {

            this.procPin = procPin;            

            ts.MyTimer = new Timer(new TimerCallback(BlinkTime_Tick), ts, Timeout.Infinite, Timeout.Infinite);
            
            ts.led = driver;
            ts.led.Allocate(procPin, PinDirection.Output);
            ts.led.SetPinResistor(procPin, PinResistor.PullUp);
        }

        public void On() {
            if (ts.running) { return; }
            ts.led.Write(procPin, true);
        }

        public void Off() {
            if (ts.running) { return; }
            ts.led.Write(procPin, false);
        }

        public void BlinkOn(uint Milliseconds, BlinkRate blinkRate) {

            if (ts.running) { return; }
            ts.running = true;

            ts.blinkMilliseconds = Milliseconds;
            ts.BlinkMillisecondsToDate = 0;
            ts.blinkRateMilliseconds = CalculateBlinkRate(blinkRate);
            ts.MyTimer.Change(0, ts.blinkRateMilliseconds);
        }

        void BlinkTime_Tick(object state) {
            var ts = (ledState)state;

            ts.led.Write(procPin, !ts.ledOn);
            ts.ledOn = !ts.ledOn;

            ts.BlinkMillisecondsToDate += ts.blinkRateMilliseconds;
            if (ts.BlinkMillisecondsToDate >= ts.blinkMilliseconds) {
                // turn off blink
                ts.MyTimer.Change(Timeout.Infinite, Timeout.Infinite);
                ts.led.Write(procPin, false);
                ts.running = false;
            }
        }

        int CalculateBlinkRate(BlinkRate rate) {
            int br = 500;
            switch (rate) {
                case BlinkRate.Slow:
                    br = 1000;
                    break;
                case BlinkRate.Medium:
                    br = 500;
                    break;
                case BlinkRate.Fast:
                    br = 75;
                    break;
                case BlinkRate.VeryFast:
                    br = 25;
                    break;
            }
            return br;
        }

        protected override void ActuatorCleanup() {
            
        }

        public override void Action(IotAction action) {
            // no actions implemented
        }
    }
}
