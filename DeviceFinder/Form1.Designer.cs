namespace DeviceFinder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.grpDevices = new System.Windows.Forms.GroupBox();
            this.grpDeviceType = new System.Windows.Forms.GroupBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtPID = new System.Windows.Forms.TextBox();
            this.lblPID = new System.Windows.Forms.Label();
            this.txtVID = new System.Windows.Forms.TextBox();
            this.lblVID = new System.Windows.Forms.Label();
            this.chkHIDDevices = new System.Windows.Forms.RadioButton();
            this.chkUSBDevices = new System.Windows.Forms.RadioButton();
            this.Manufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FriendlyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Class = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interface = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Instance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Revision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevicePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Service = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Enumerator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InterfaceGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grpDevices.SuspendLayout();
            this.grpDeviceType.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Manufacturer,
            this.Description,
            this.FriendlyName,
            this.VID,
            this.PID,
            this.Class,
            this.Location,
            this.Interface,
            this.Instance,
            this.Revision,
            this.DeviceID,
            this.DevicePath,
            this.Service,
            this.Enumerator,
            this.ClassGUID,
            this.InterfaceGUID});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(892, 466);
            this.dataGridView1.TabIndex = 0;
            // 
            // grpDevices
            // 
            this.grpDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDevices.Controls.Add(this.dataGridView1);
            this.grpDevices.Location = new System.Drawing.Point(12, 87);
            this.grpDevices.Name = "grpDevices";
            this.grpDevices.Size = new System.Drawing.Size(898, 485);
            this.grpDevices.TabIndex = 1;
            this.grpDevices.TabStop = false;
            this.grpDevices.Text = "Found Devices";
            // 
            // grpDeviceType
            // 
            this.grpDeviceType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDeviceType.Controls.Add(this.btnScan);
            this.grpDeviceType.Controls.Add(this.txtPID);
            this.grpDeviceType.Controls.Add(this.lblPID);
            this.grpDeviceType.Controls.Add(this.txtVID);
            this.grpDeviceType.Controls.Add(this.lblVID);
            this.grpDeviceType.Controls.Add(this.chkHIDDevices);
            this.grpDeviceType.Controls.Add(this.chkUSBDevices);
            this.grpDeviceType.Location = new System.Drawing.Point(12, 13);
            this.grpDeviceType.Name = "grpDeviceType";
            this.grpDeviceType.Size = new System.Drawing.Size(898, 68);
            this.grpDeviceType.TabIndex = 2;
            this.grpDeviceType.TabStop = false;
            this.grpDeviceType.Text = "Device Type";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(375, 26);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(125, 23);
            this.btnScan.TabIndex = 6;
            this.btnScan.Text = "Scan for Devices";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtPID
            // 
            this.txtPID.Location = new System.Drawing.Point(298, 40);
            this.txtPID.Name = "txtPID";
            this.txtPID.Size = new System.Drawing.Size(47, 20);
            this.txtPID.TabIndex = 5;
            // 
            // lblPID
            // 
            this.lblPID.AutoSize = true;
            this.lblPID.Location = new System.Drawing.Point(267, 43);
            this.lblPID.Name = "lblPID";
            this.lblPID.Size = new System.Drawing.Size(25, 13);
            this.lblPID.TabIndex = 4;
            this.lblPID.Text = "PID";
            // 
            // txtVID
            // 
            this.txtVID.Location = new System.Drawing.Point(298, 15);
            this.txtVID.Name = "txtVID";
            this.txtVID.Size = new System.Drawing.Size(47, 20);
            this.txtVID.TabIndex = 3;
            // 
            // lblVID
            // 
            this.lblVID.AutoSize = true;
            this.lblVID.Location = new System.Drawing.Point(267, 18);
            this.lblVID.Name = "lblVID";
            this.lblVID.Size = new System.Drawing.Size(25, 13);
            this.lblVID.TabIndex = 2;
            this.lblVID.Text = "VID";
            // 
            // chkHIDDevices
            // 
            this.chkHIDDevices.AutoSize = true;
            this.chkHIDDevices.Location = new System.Drawing.Point(153, 32);
            this.chkHIDDevices.Name = "chkHIDDevices";
            this.chkHIDDevices.Size = new System.Drawing.Size(86, 17);
            this.chkHIDDevices.TabIndex = 1;
            this.chkHIDDevices.Text = "HID Devices";
            this.chkHIDDevices.UseVisualStyleBackColor = true;
            this.chkHIDDevices.CheckedChanged += new System.EventHandler(this.chkUSBDevices_CheckedChanged);
            // 
            // chkUSBDevices
            // 
            this.chkUSBDevices.AutoSize = true;
            this.chkUSBDevices.Checked = true;
            this.chkUSBDevices.Location = new System.Drawing.Point(27, 32);
            this.chkUSBDevices.Name = "chkUSBDevices";
            this.chkUSBDevices.Size = new System.Drawing.Size(103, 17);
            this.chkUSBDevices.TabIndex = 0;
            this.chkUSBDevices.TabStop = true;
            this.chkUSBDevices.Text = "All USB Devices";
            this.chkUSBDevices.UseVisualStyleBackColor = true;
            this.chkUSBDevices.CheckedChanged += new System.EventHandler(this.chkUSBDevices_CheckedChanged);
            // 
            // Manufacturer
            // 
            this.Manufacturer.DataPropertyName = "Manufacturer";
            this.Manufacturer.HeaderText = "Manufacturer";
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.ReadOnly = true;
            this.Manufacturer.Width = 200;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 200;
            // 
            // FriendlyName
            // 
            this.FriendlyName.DataPropertyName = "FriendlyName";
            this.FriendlyName.HeaderText = "Friendly Name";
            this.FriendlyName.Name = "FriendlyName";
            this.FriendlyName.ReadOnly = true;
            // 
            // VID
            // 
            this.VID.DataPropertyName = "VID";
            this.VID.HeaderText = "VID";
            this.VID.Name = "VID";
            this.VID.ReadOnly = true;
            this.VID.Width = 50;
            // 
            // PID
            // 
            this.PID.DataPropertyName = "PID";
            this.PID.HeaderText = "PID";
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Width = 50;
            // 
            // Class
            // 
            this.Class.DataPropertyName = "Class";
            this.Class.HeaderText = "Class";
            this.Class.Name = "Class";
            this.Class.ReadOnly = true;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "Location";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            // 
            // Interface
            // 
            this.Interface.DataPropertyName = "Interface";
            this.Interface.HeaderText = "Interface";
            this.Interface.Name = "Interface";
            this.Interface.ReadOnly = true;
            this.Interface.Width = 52;
            // 
            // Instance
            // 
            this.Instance.DataPropertyName = "Instance";
            this.Instance.HeaderText = "Instance";
            this.Instance.Name = "Instance";
            this.Instance.ReadOnly = true;
            this.Instance.Width = 50;
            // 
            // Revision
            // 
            this.Revision.DataPropertyName = "Revision";
            this.Revision.HeaderText = "Revision";
            this.Revision.Name = "Revision";
            this.Revision.ReadOnly = true;
            this.Revision.Width = 50;
            // 
            // DeviceID
            // 
            this.DeviceID.DataPropertyName = "DeviceID";
            this.DeviceID.HeaderText = "Device ID";
            this.DeviceID.Name = "DeviceID";
            this.DeviceID.ReadOnly = true;
            this.DeviceID.Width = 300;
            // 
            // DevicePath
            // 
            this.DevicePath.DataPropertyName = "DevicePath";
            this.DevicePath.HeaderText = "DevicePath";
            this.DevicePath.Name = "DevicePath";
            this.DevicePath.ReadOnly = true;
            this.DevicePath.Width = 250;
            // 
            // Service
            // 
            this.Service.DataPropertyName = "ServiceName";
            this.Service.HeaderText = "Service";
            this.Service.Name = "Service";
            this.Service.ReadOnly = true;
            // 
            // Enumerator
            // 
            this.Enumerator.DataPropertyName = "Enumerator";
            this.Enumerator.HeaderText = "Enumerator";
            this.Enumerator.Name = "Enumerator";
            this.Enumerator.ReadOnly = true;
            // 
            // ClassGUID
            // 
            this.ClassGUID.DataPropertyName = "ClassGUID";
            this.ClassGUID.HeaderText = "Device Class";
            this.ClassGUID.Name = "ClassGUID";
            this.ClassGUID.ReadOnly = true;
            this.ClassGUID.Width = 210;
            // 
            // InterfaceGUID
            // 
            this.InterfaceGUID.DataPropertyName = "InterfaceGUID";
            this.InterfaceGUID.HeaderText = "Interface Type (GUID)";
            this.InterfaceGUID.Name = "InterfaceGUID";
            this.InterfaceGUID.ReadOnly = true;
            this.InterfaceGUID.Width = 210;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 584);
            this.Controls.Add(this.grpDeviceType);
            this.Controls.Add(this.grpDevices);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grpDevices.ResumeLayout(false);
            this.grpDeviceType.ResumeLayout(false);
            this.grpDeviceType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox grpDevices;
        private System.Windows.Forms.GroupBox grpDeviceType;
        private System.Windows.Forms.RadioButton chkUSBDevices;
        private System.Windows.Forms.RadioButton chkHIDDevices;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox txtPID;
        private System.Windows.Forms.Label lblPID;
        private System.Windows.Forms.TextBox txtVID;
        private System.Windows.Forms.Label lblVID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Manufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn FriendlyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Class;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interface;
        private System.Windows.Forms.DataGridViewTextBoxColumn Instance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Revision;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevicePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Service;
        private System.Windows.Forms.DataGridViewTextBoxColumn Enumerator;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn InterfaceGUID;


    }
}

