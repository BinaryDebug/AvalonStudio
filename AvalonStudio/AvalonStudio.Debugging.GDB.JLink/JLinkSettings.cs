using System.ComponentModel;

namespace AvalonStudio.Debugging.GDB.JLink
{
    public enum JlinkInterfaceType
    {
        [Description("JTAG")] JTAG,
        [Description("SWD")] SWD,
        [Description("FINE")] FINE,
        [Description("2-wire-JTAG-PIC32")] JTAGPic32
    }

    public class JLinkSettings
    {
        public JLinkSettings()
        {
            Download = true;
            Reset = true;
        }

        public JlinkInterfaceType Interface { get; set; }
        public string TargetDevice { get; set; }
        public string DeviceKey { get; set; }
        public int SpeedkHz { get; set; }

        public bool Download { get; set; }

        public bool Reset { get; set; }
    }
}