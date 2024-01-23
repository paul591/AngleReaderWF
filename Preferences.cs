using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleReaderWF
{
    /// <summary>
    /// Preferences Class to hold the settings from the device.
    /// This is mainly used in the settings dialog
    /// </summary>
    public class Preferences
    {
        public Preferences() { }

        public int FilterDepth { get; set; }
        public int PulsePerRev { get; set; }
        public int LoopInterval { get; set; }

    }
}
