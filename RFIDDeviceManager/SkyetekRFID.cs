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

        #region STPv3 Flags

        [Flags]
        public enum Flag : short 
        {
            Loop        = 1 << 0,  // Enable Loop Mode
            Inventory   = 1 << 1,  // Inventory tags in the RF field
            Lock        = 1 << 2,  // Lock tags or blocks of tag memory
            RF          = 1 << 3,  // Keep RF on after command execution
            AFI         = 1 << 4,  // Use Application Field Identifier (AFI) field to select tags
            CRC         = 1 << 5,  // CRC Required for Request/Response
            TID         = 1 << 6,  // Tag ID (TID) present in the Request
            RID         = 1 << 7,  // Reader ID (RID) present in the Request/Response
            Encryption  = 1 << 8,  // Encrypt/Decrypt data for Writes/Reads
            HMAC        = 1 << 9, // Write/Verify HMAC for Reads/Writes
            Session     = 1 << 10, // Use the Session Field to select tags
            Data        = 1 << 11, // Data field is present in the Request
        }

        #endregion

        #region STPv3 System Parameters

        /// <summary>
        /// Possible system parameter values.  Not all devices support all parameters; check with the particular
        /// modudle datasheet to determine appropriate parameters for your device.
        /// </summary>
        public enum Parameter : ushort
        {
            SERIAL_NUMBER = 0x0000,
            FIRMWARE_VER = 0x0001,
            HARDWARE_VER = 0x0002,
            PRODUCT_CODE = 0x0003,
            RID = 0x0004,
            READER_NAME = 0x0005,
            HOST_INTERFACE = 0x0006,
            BAUD_RATE = 0x0007,
            USER_PORT_DIR = 0x0008,
            USER_PORT_VAL = 0x0009,
            MUX_CONTROL = 0x000A,
            OPERATING_MODE = 0x000C,
            ENCRYPTION_SCHEME = 0x000D,
            HMAC_SCHEME = 0x000E,
            TAG_POPULATION = 0x0010,
            RETRY_COUNT = 0x0011,
            TX_POWER = 0x0012,

            CURRENT_FREQUENCY = 0x0030,
            START_FREQUENCY = 0x0031,
            STOP_FREQUENCY = 0x0032,
            FEATURE_LOCK = 0x0033,
            HOP_CHANNEL_SPACING = 0x0034,
            FREQEUNCY_HOP_SEQUENCE = 0x0035,
            MODULATION_DEPTH = 0x0036,
            REGULATORY_MODE = 0x0037,
            LBT_ADJUST = 0x0038,
            BOARD_TEMPERATURE = 0x0039,
            ETSI_SIGNAL_STRENGTH = 0x003A,
            SYNTHESIZER_POWER_LEVEL = 0x003C,
            CURRENT_DAC_VALUE = 0x003F,
            POWER_DETECTOR_VALUE = 0x0040,
            PULSE_SHAPING_MODE = 0x0041,
            PA_TABLE = 0x0042,
            REGULATOR_SWITCH = 0x0043,

            SITE_SURVEY = 0x0044,
            OPTIMAL_POWER_GEN1 = 0x0045,
            OPTIMAL_POWER_GEN2 = 0x0046,
            OPTIMAL_POWER_6B = 0x0047,
            TEST_MODE = 0x0048,
            OPTIMAL_POWER_EM = 0x0049
        }


        #endregion

        #region STPv3 Requests (Commands)

        /*
         *             public static STPv3Command READ_SYSTEM_PARAMETER = new STPv3Command(0x1201, "Read System Parameter", 300, false, true, false);
            public static STPv3Command WRITE_SYSTEM_PARAMETER = new STPv3Command(0x1202, "Write System Parameter", 300, false, true, true);

            public static STPv3Command STORE_DEFAULT_SYSTEM_PARAMETER = new STPv3Command(0x1301, "Store Default System Parameter", 300, false, true, true);
            public static STPv3Command RETRIEVE_DEFAULT_SYSTEM_PARAMETER = new STPv3Command(0x1302, "Retrieve Default System Parameter", 300, false, true, false);
*/

        enum Command : ushort
        {
            ReadParameter  = 0x1201,
            WriteParameter = 0x1202,
            StoreDefaultParameter = 0x1301,
            RetrieveDefaultParameter = 0x1302
        }

        /// <summary>
        /// Class used to build up and manipulate a STPv3 Request to the RFID device
        /// </summary>
        class Request
        {
            private Command Code = 0x0;   // Command to device
            private Flag Flags = 0x0;   // Flags for command

            private UInt32 RID = 0x0;   // Reader ID
            private UInt16 TagType = 0x0;   // Tag Type (0x0 = omit from request)
            private String TID = null;  // Tag ID (max of 16 bytes)
            private Byte AFI = 0x0;   // Application Field Identifier, if used 
            private Byte Session = 0x0;   // Session Number if this command is part of a session
            private UInt16 Address = 0x0;   // Address to be used (0x0 = omit from request)
            private UInt16 NumBlocks = 0x0;   // Number of blocks written to or read from address (0x0 = omit from request)
            private Byte[] Data = null;  // Data (max of 1K bytes)

            public Request(Command cmd)
            {
                this.Code = cmd;
            }

            #region Flag Handling

            public bool FlagIsSet(Flag flag)
            {
                return (this.Flags & flag) > 0;
            }

            public void SetFlag(Flag flag)
            {
                this.Flags |= flag;
            }

            public void ClearFlag(Flag flag)
            {
                this.Flags &= ~flag;
            }

            public Flag GetFlags()
            {
                return this.Flags;
            }

            #endregion

            #region Request Formatting

            private Byte[] GetMSBFirst(UInt16 value)
            {
                Byte[] bytes = new Byte[2];

                bytes[0] = (Byte)((value >> 8) & 0x0FF);
                bytes[1] = (Byte)(value & 0x0FF);

                return bytes;
            }

            private Byte[] GetMSBFirst(UInt32 value)
            {
                Byte[] bytes = new Byte[4];

                bytes[0] = (Byte)((value >> 24) & 0x0FF);
                bytes[1] = (Byte)((value >> 16) & 0x0FF);
                bytes[2] = (Byte)((value >> 8) & 0x0FF);
                bytes[3] = (Byte)(value & 0x0FF);

                return bytes;
            }

            // Return the ASCII-mode request string 
            public override string ToString()
            {
                return ToString(false);
            }

            public string ToString(Boolean FormatRequest)
            {
                StringBuilder Str = new StringBuilder(10);

                foreach (Byte b in this.ToBytes())
                {
                    Byte first = (Byte)((b & 0x0F0) >> 4);
                    Str.Append((first < 10) ? (Char)(first + '0') : (Char)((first - 10) + 'A'));

                    Byte second = (Byte)(b & 0x0F);
                    Str.Append((second < 10) ? (Char)(second + '0') : (Char)((second - 10) + 'A'));
                }

                // Remove the STX and message length (not used in ASCII mode)
                if (Str.Length > 6)
                    Str.Remove(0, 6);

                // Add a initial and final <CR> to frame the request
                if (FormatRequest)
                {
                    Str.Insert(0, (Char)0x0D);
                    Str.Append((Char)0x0D);
                }

                return Str.ToString();
            }

            // Return the binary-mode request byte stream
            public Byte[] ToBytes()
            {
                List<Byte> bytes = new List<byte>();

                SetFlag(Flag.CRC);
                bytes.AddRange(GetMSBFirst((UInt16)this.Flags));
                bytes.AddRange(GetMSBFirst((UInt16)this.Code));

                if (FlagIsSet(Flag.RID))
                    bytes.AddRange(GetMSBFirst(RID));

                if (TagType > 0x0)
                    bytes.AddRange(GetMSBFirst(TagType));

                if (FlagIsSet(Flag.TID))
                {
                    if ((String.IsNullOrEmpty(TID)) || (TID.Length > 16))
                    {
                        bytes.Add(0x0);
                        bytes.Add(0x0);
                    }
                    else
                    {
                        bytes.Add((Byte)(TID.Length));
                        bytes.AddRange(Encoding.ASCII.GetBytes(TID));
                    }
                }

                if (FlagIsSet(Flag.AFI))
                    bytes.Add(AFI);

                if (FlagIsSet(Flag.Session))
                    bytes.Add(Session);

                if (Address != 0x0)
                    bytes.AddRange(GetMSBFirst(Address));

                if (NumBlocks != 0x0)
                    bytes.AddRange(GetMSBFirst(NumBlocks));

                if (FlagIsSet(Flag.Data))
                {
                    bytes.AddRange(GetMSBFirst((UInt16)this.Data.Length));
                    bytes.AddRange(this.Data);
                }

                // Insert Total lengh of data
                UInt16 Length = (UInt16)(bytes.Count() + 2); // Add the length bytes into total length
                bytes.InsertRange(0, GetMSBFirst(Length));

                // Calculate CRC
                UInt16 CRC = CRC16(0, bytes);
                bytes.AddRange(GetMSBFirst(CRC));

                // Insert STX
                bytes.Insert(0, 0x02);

                return bytes.ToArray();
            }

            private UInt16 CRC16(UInt16 Preset, List<Byte> bytes)
            {
                UInt16 CRC = Preset;
                foreach (Byte b in bytes)
                {
                    CRC ^= (UInt16)b;
                    for (int i = 0; i < 8; i++)
                    {
                        Boolean test = (CRC & 0x001) > 0;
                        CRC >>= 1;
                        if (test)
                            CRC ^= 0x8408;
                    }
                }

                return CRC;
            }

            #endregion

            #region Request Parameter Accessors

            #region RID

            public void ClearRID()
            {
                ClearFlag(Flag.RID);
                RID = 0x0;
            }

            public void SetRID(UInt32 ID)
            {
                RID = ID;
                if (RID > 0)
                    SetFlag(Flag.RID);
            }

            public UInt32 GetRID()
            {
                return this.RID;
            }

            #endregion

            #region TagType

            public void ClearTagType()
            {
                TagType = 0x0;
            }

            public void SetTagType(UInt16 Type)
            {
                TagType = Type;
            }

            public UInt16 GetTagType()
            {
                return this.TagType;
            }

            #endregion

            #region TID

            public void ClearTID()
            {
                ClearFlag(Flag.TID);
                TID = null;
            }

            public void SetTID(String ID)
            {
                if (String.IsNullOrEmpty(ID))
                    ClearTID();

                if (ID.Count() > 16)
                    throw new Exception("Skyetek STPv3 TID can not exceed 16 bytes");

                this.TID = ID;
                SetFlag(Flag.TID);
            }

            public String GetTID()
            {
                return this.TID;
            }

            #endregion

            #region AFI

            public void ClearAFI()
            {
                ClearFlag(Flag.AFI);
                AFI = 0x0;
            }

            public void SetAFI(Byte ApplicationFieldID)
            {
                AFI = ApplicationFieldID;
                if (AFI > 0)
                    SetFlag(Flag.AFI);
            }

            public Byte GetAFI()
            {
                return this.AFI;
            }

            #endregion

            #region Session

            public void ClearSession()
            {
                ClearFlag(Flag.Session);
                Session = 0x0;
            }

            public void SetSession(Byte SessionNumber)
            {
                Session = SessionNumber;
                if (Session > 0)
                    SetFlag(Flag.Session);
            }

            public Byte GetSession()
            {
                return this.Session;
            }

            #endregion

            #region Address

            public void ClearAddress()
            {
                Address = 0x0;
            }

            public void SetAddress(UInt16 Addr)
            {
                Address = Addr;
            }

            public UInt16 GetAddress()
            {
                return this.Address;
            }

            #endregion

            #region NumBlocks

            public void ClearNumBlocks()
            {
                NumBlocks = 0x0;
            }

            public void SetNumBlocks(UInt16 Num)
            {
                NumBlocks = Num;
            }

            public UInt16 GetNumBlocks()
            {
                return this.NumBlocks;
            }

            #endregion

            #region Data

            public void ClearData()
            {
                ClearFlag(Flag.Data);
                Data = null;
            }

            public void SetData(Byte[] Data)
            {
                if ((Data == null) || (Data.Length == 0))
                    ClearData();

                if (Data.Length > 1024)
                    throw new Exception("Skyetek STPv3 Request Data can not exceed 1024 bytes");

                this.Data = Data;
                SetFlag(Flag.Data);
            }

            public Byte[] GetData()
            {
                return this.Data;
            }

            #endregion

            #endregion

        }

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

        #region Basic Command Request/Response



        #endregion

        #region System Parameters

        /// <summary>
        /// Attempt to retreive the given parameter value as a byte stream.
        /// </summary>
        /// <param name="param">Parameter value to retreive</param>
        /// <returns>Current value of parameter on device, or NULL on any error</returns>
        public byte[] GetParameterValue(Parameter param)
        {
            Request cmd = new Request(Command.ReadParameter);
            cmd.SetAddress((UInt16)Parameter.USER_PORT_VAL);
            cmd.SetNumBlocks(1);

            String str = cmd.ToString();
            
            return null;
        }

        #endregion

    }
}
