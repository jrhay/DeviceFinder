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
                devices = DeviceManager.Devices.GetConnectedUSBDevices(txtVID.Text, txtPID.Text);
            else if (chkHIDDevices.Checked)
                devices = DeviceManager.Devices.GetConnectedHIDDevices(txtVID.Text, txtPID.Text);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = devices;
        }

        private void chkUSBDevices_CheckedChanged(object sender, EventArgs e)
        {
            PopulateDeviceList();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            PopulateDeviceList();
        }

    }
}
