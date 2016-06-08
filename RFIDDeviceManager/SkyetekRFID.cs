using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace RFIDDeviceManager
{
    /// <summary>
    /// Class to communicate with a SkyeTek RFID device via USB/HID and the Skytek Protocol V3 
    /// (stpv3) as described in http://www.skyetek.com/docs/commonblade/stpv3guide.pdf
    /// 
    /// This class entirely replaces the default Skyetek interface code for .NET
    /// (stapiclr.dll) for USB-attached Skyetek modules all version of Windows running .NET 4.5
    /// 
    /// Originally written by Jeff Hay of Portable Knowledge, LLC, http://www.portablek.com
    /// </summary>
    public class SkyetekRFID : IDisposable
    {
        public static String SkyetekVID = "FEF";
        public static String SkyeModulePID = "F01";

        private SafeHandle _DeviceHandle = null;

        #region p/Invoke Declarations

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern SafeFileHandle CreateFile(String lpFileName, UInt32 dwDesiredAccess, Int32 dwShareMode, IntPtr lpSecurityAttributes, Int32 dwCreationDisposition, Int32 dwFlagsAndAttributes, IntPtr hTemplateFile);

        #endregion

        /// <summary>
        /// Is this instance valid and ready to be used?
        /// </summary>
        public Boolean isValid { get { return ((_DeviceHandle != null) && (!_DeviceHandle.IsInvalid)); } }

        /// <summary>
        /// Create a new instance by attempting to open the given USB path.  If unable to be opened,
        /// .isValid will return false.
        /// </summary>
        /// <param name="DevicePath">USB device path to attempt to open</param>
        public SkyetekRFID(String DevicePath)
        {
            // Attempt to open an existing device for shared read/write access
            _DeviceHandle = CreateFile(DevicePath, 0, 3, IntPtr.Zero, 3, 0, IntPtr.Zero);
        }

        public void Dispose()
        {
            if (_DeviceHandle != null)
                _DeviceHandle.Dispose();
        }

        /// <summary>
        /// Create a new SkyetekRFID instance with the device at the given path.
        /// </summary>
        /// <param name="DevicePath">Full USB path to the Skyetek RFID reader hardware device</param>
        /// <returns>Initialized instance if device found and opened successfully, null otherwise</returns>
        public static SkyetekRFID GetDeviceWithPath(String DevicePath)
        {
            SkyetekRFID Device = new SkyetekRFID(DevicePath);
            if (!Device.isValid)
                return null;

            return Device;
        }

    }
}
