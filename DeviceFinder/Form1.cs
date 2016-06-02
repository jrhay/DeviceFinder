using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DeviceManager;

namespace DeviceFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            PopulateDeviceList();
        }

        void PopulateDeviceList()
        {
            List<DeviceInfo> devices = DeviceManager.DeviceManager.GetDevices(DeviceManager.GUID_DEVINTERFACE.GUID_DEVINTERFACE_USB_DEVICE, Guid.Empty, true);
            listDevices.Clear();
            foreach (DeviceInfo device in devices)
            {
                listDevices.Items.Add(device.Path);
            }
        }
    }
}
