using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    /// <summary>
    /// Manage Windows HardwareID/DeviceID strings
    /// </summary>
    public class HardwareID
    {
        public enum HardwareIDFormat 
        {
            Unknown,
            DeviceIdentifier,
            GenericIdentifier
        }

        private String _ID = null;
        public String ID { get { return _ID; } }
        
        private HardwareIDFormat _Format = HardwareIDFormat.Unknown;
        public HardwareIDFormat Format { get { return _Format; } }

        private String _Enumerator = null;
        public String Enumerator { get { return _Enumerator; }}

        private String _VID = null;
        public String VID { get { return _VID; }}

        private String _PID = null;
        public String PID { get { return _PID; }}

        private String _Revision = null;
        public String Revision { get { return _Revision; } }

        private String _Interface = null;
        public String Interface { get { return _Interface; } }

        public HardwareID(String IDString)
        {
            this._ID = IDString;
            ParseIDString();
        }

        private void ResetValues() 
        {
            _Format = HardwareIDFormat.Unknown;
            _Enumerator = _VID = _PID = _Revision = _Interface = null;
        }

        private void ParseIDString()
        {
            ResetValues();
            if (!String.IsNullOrEmpty(_ID))
            {
                if (_ID[0] == '*')
                {
                    _Format = HardwareIDFormat.GenericIdentifier;
                    if (_ID.Length > 3)
                        _Enumerator = _ID.Substring(1, 3); // Should be "PNP"
                }
                else
                {
                    string[] Tokens = _ID.Split('\\');
                    if (Tokens.Count() > 1)
                    {
                        _Enumerator = Tokens[0];
                        ParseDescriptor(Tokens[1]);
                    }
                }
            }
        }

        private void ParseDescriptor(String Desc)
        {
            string[] Tokens = Desc.Split('&');
            foreach (String token in Tokens)
            {
                if (token.Length > 4)
                {
                    string KeyName = token.Substring(0, 4);
                    string Value = token.Substring(5);
                    if (KeyName.Equals("VID_", StringComparison.InvariantCulture))
                        _VID = Value;
                    else if (KeyName.Equals("PID_", StringComparison.InvariantCulture))
                        _PID = Value;
                    else if (KeyName.Equals("REV_", StringComparison.InvariantCulture))
                        _Revision = Value;
                    else if (KeyName.Substring(0, 3).Equals("MI_", StringComparison.InvariantCulture))
                        _Interface = KeyName.Substring(4) + Value;
                }
            }
        }

    }
}
