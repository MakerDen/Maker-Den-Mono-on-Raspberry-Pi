using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glovebox.RaspberryPi.Drivers {
    public interface IHt16K33 {

        void FrameUpdate(byte[] frame);

        void FrameSetBlinkRate(byte br);

        void FrameSetBrightness(byte level);

        void FrameInit();
    }
}
