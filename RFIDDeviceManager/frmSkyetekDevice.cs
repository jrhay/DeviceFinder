using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFIDDeviceManager
{
    public partial class frmSkyetekDevice : Form
    {
        SkyetekRFID _Device = null;

        public frmSkyetekDevice()
        {
            InitializeComponent();
        }

        public void Show(SkyetekRFID Device)
        {
            _Device = Device;
            this.Show();
        }
    }
}
