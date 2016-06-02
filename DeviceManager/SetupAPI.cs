using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace DeviceManager
{
    /// <summary>
    /// p/Invoke Declarations for access to Windows system API contained in setupapi.dll
    /// </summary>
    internal static class SetupAPI
    {
        #region Device Info Structures

        internal const Int32 INVALID_HANDLE_VALUE = -1;
        internal const Int32 BUFFER_SIZE = 260;

        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_DEVINFO_DATA
        {
            public uint cbSize;
            public Guid classGuid;
            public uint devInst;
            public IntPtr reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BUFFER_SIZE)]
            public string DevicePath;
        }

        #endregion

        #region GetDeviceInterfaceDetail

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(
           IntPtr hDevInfo,
           ref SP_DEVINFO_DATA deviceInterfaceData,
           ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
           UInt32 deviceInterfaceDetailDataSize,
           IntPtr requiredSize,
           ref SP_DEVINFO_DATA deviceInfoData
        );

        #endregion

        #region GetClassDevs

        [Flags]
        internal enum DiGetClassFlags : uint
        {
            DIGCF_DEFAULT = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE
            DIGCF_PRESENT = 0x00000002,
            DIGCF_ALLCLASSES = 0x00000004,
            DIGCF_PROFILE = 0x00000008,
            DIGCF_DEVICEINTERFACE = 0x00000010,
        }
        
        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, IntPtr Enumerator, IntPtr hwndParent, int Flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]     // 2nd form uses an Enumerator only, with null ClassGUID 
        internal static extern IntPtr SetupDiGetClassDevs(IntPtr ClassGuid, string Enumerator, IntPtr hwndParent, int Flags);

        #endregion

        #region GetDeviceID

        [DllImport("setupapi.dll", SetLastError = true)]
        internal static extern int CM_Get_Device_ID_Size(out int pulLen, UInt32 dnDevInst, int flags = 0);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        internal static extern int CM_Get_Device_ID(UInt32 dnDevInst, IntPtr buffer, int bufferLen, int flags = 0);

        /// <summary>
        /// Extract a Device ID string from a given device/interface info structure
        /// </summary>
        /// <param name="DevInfo">Device/Interface to obtain ID for</param>
        /// <returns>ID for the particular device/interface, or null on any error</returns>
        public static String GetDeviceID(SP_DEVINFO_DATA DevInfo)
        {
            int numChars;
            CM_Get_Device_ID_Size(out numChars, DevInfo.devInst);

            if (numChars < 1)
                return null;

            numChars += 1; // add room for null

            IntPtr idBuffer = Marshal.AllocHGlobal(numChars * Marshal.SystemDefaultCharSize);
            CM_Get_Device_ID(DevInfo.devInst, idBuffer, numChars);
            String DeviceID = Marshal.PtrToStringAuto(idBuffer);
            Marshal.FreeHGlobal(idBuffer);

            return DeviceID;
        }

        #endregion

        #region Device Registry Properties

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, uint Property, out UInt32 PropertyRegDataType,
            IntPtr PropertyBuffer, uint PropertyBufferSize, out uint RequiredSize);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, uint Property, out UInt32 PropertyRegDataType,
            IntPtr PropertyBuffer, uint PropertyBufferSize, IntPtr RequiredSize);

        /// <summary>
        /// Attempt to return a property value for a specific device
        /// </summary>
        /// <param name="deviceList">Device List containing the device</param>
        /// <param name="DevInfo">Device Info Structure</param>
        /// <param name="Property">Property to obtain, one of the SPDRP_ values</param>
        /// <returns>Property value as a string (if applicable), or null</returns>
        public static String GetDevicePropertyString(IntPtr deviceList, SP_DEVINFO_DATA DevInfo, uint Property)
        {
            UInt32 PropertyDataType;

            uint numBytes;
            SetupDiGetDeviceRegistryProperty(deviceList, ref DevInfo, Property, out PropertyDataType, IntPtr.Zero, 0, out numBytes);
            if (numBytes > 0)
            {
                IntPtr propertyBuffer = Marshal.AllocHGlobal((int)numBytes);
                SetupDiGetDeviceRegistryProperty(deviceList, ref DevInfo, Property, out PropertyDataType, propertyBuffer, numBytes, IntPtr.Zero);
                String PropertyValue = Marshal.PtrToStringAuto(propertyBuffer);
                Marshal.FreeHGlobal(propertyBuffer);
                return PropertyValue;
            }

            return null;
        }

        #endregion

        #region EnumDeviceInterfaces

        [DllImport("setupapi.dll", SetLastError = true)]
        internal static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern Boolean SetupDiEnumDeviceInterfaces(
           IntPtr hDevInfo,
           ref SP_DEVINFO_DATA devInfo,
           ref Guid interfaceClassGuid,
           UInt32 memberIndex,
           ref SP_DEVINFO_DATA deviceInterfaceData
        );

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern Boolean SetupDiEnumDeviceInterfaces(
           IntPtr hDevInfo,
           IntPtr devInfo,
           ref Guid interfaceClassGuid,
           UInt32 memberIndex,
           ref SP_DEVINFO_DATA deviceInterfaceData
        );

        #endregion

        #region Memory Cleanup
        
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        #endregion
    }
}
