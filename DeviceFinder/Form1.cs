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
            List<DeviceInfo> devices = null;
            if (chkUSBDevices.Checked)
                devices = DeviceManager.DeviceManager.GetConnectedUSBDevices();
            else if (chkHIDDevices.Checked)
                devices = DeviceManager.DeviceManager.GetConnectedHIDDevices();

            dataGridView1.DataSource = devices;
        }

        private void chkUSBDevices_CheckedChanged(object sender, EventArgs e)
        {
            PopulateDeviceList();
        }

    }
}
