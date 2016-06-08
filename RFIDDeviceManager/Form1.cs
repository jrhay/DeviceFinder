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

namespace RFIDDeviceManager
{
    public partial class Form1 : Form
    {
        static String SkyetekVID = "FEF";
        static String SkyetekPID = "F01";

        public Form1()
        {
            InitializeComponent();
            btnOpen.Enabled = false;
            btnScan_Click(null, null);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            btnScan.Enabled = false;

            lstDevices.Items.Clear();
            lstDevices.Text = null;

            lstDevices.Items.AddRange(Devices.GetConnectedHIDDevices(SkyetekVID, SkyetekPID).Select(x => x.DevicePath).ToArray());

            if (lstDevices.Items.Count > 0)
                lstDevices.SelectedIndex = 0;
            else
                lstDevices.SelectedIndex = -1;
            lstDevices_SelectedIndexChanged(null, null);

            btnScan.Enabled = true;
        }

        private void lstDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOpen.Enabled = lstDevices.SelectedIndex > -1;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                SkyetekRFID Device = new SkyetekRFID(lstDevices.Text);
                if (!Device.isValid)
                    throw new Win32Exception();

                frmSkyetekDevice SkyetekForm = new frmSkyetekDevice();
                SkyetekForm.Show(Device);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to open RFID Device: " + ex.Message, "RFID Device Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
