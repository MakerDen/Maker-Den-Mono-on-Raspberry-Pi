﻿using Glovebox.IoT.Base;
using Glovebox.IoT.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glovebox.RaspberryPi.IO.Actuators {
    public class Sys : ActuatorBase {

        ProcessStartInfo info = new ProcessStartInfo();

        public enum Actions {
            Halt,
            Reboot
        }


        /// <summary>
        /// Create a System control
        /// </summary>
        /// <param name="name">Unique identifying name for command and control</param>
        public Sys(string name)
            : base(name, "system") {

            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
        }

        protected override void ActuatorCleanup() {
        }

        public void Action(Actions action) {
            switch (action) {
                case Actions.Halt:
                    Halt();
                    break;
                case Actions.Reboot:
                    Reboot();
                    break;
                default:
                    break;
            }
        }

        public override void Action(IotAction action) {
            if (action.identified) { return; }
            switch (action.cmd) {
                case "halt":
                    Halt();
                    break;
                case "reboot":
                    Reboot();
                    break;
            }
        }

        public void Halt() {
            info.FileName = "sudo";
            info.Arguments = "halt";
            Process.Start(info);
        }

        public void Reboot() {
            info.FileName = "sudo";
            info.Arguments = "reboot";
            Process.Start(info);
        }
    }
}
