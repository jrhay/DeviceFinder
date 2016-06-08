namespace RFIDDeviceManager
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
            this.btnScan = new System.Windows.Forms.Button();
            this.lstDevices = new System.Windows.Forms.ComboBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(13, 13);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(146, 23);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Scan for RFID Devices";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lstDevices
            // 
            this.lstDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDevices.FormattingEnabled = true;
            this.lstDevices.Location = new System.Drawing.Point(15, 74);
            this.lstDevices.Name = "lstDevices";
            this.lstDevices.Size = new System.Drawing.Size(512, 21);
            this.lstDevices.TabIndex = 1;
            this.lstDevices.SelectedIndexChanged += new System.EventHandler(this.lstDevices_SelectedIndexChanged);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Location = new System.Drawing.Point(12, 58);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(117, 13);
            this.lblSelect.TabIndex = 2;
            this.lblSelect.Text = "Select an RFID Device";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(415, 101);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(112, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open Device...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 137);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.lstDevices);
            this.Controls.Add(this.btnScan);
            this.Name = "Form1";
            this.Text = "RFID Device Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ComboBox lstDevices;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Button btnOpen;
    }
}

