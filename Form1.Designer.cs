namespace trail01
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelDeviceCount = new System.Windows.Forms.Label();
            this.buttonDevices = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.labelFtdiStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.Location = new System.Drawing.Point(414, 21);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(136, 31);
            this.labelVersion.TabIndex = 38;
            this.labelVersion.Text = "version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDeviceCount
            // 
            this.labelDeviceCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDeviceCount.Location = new System.Drawing.Point(125, 68);
            this.labelDeviceCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDeviceCount.Name = "labelDeviceCount";
            this.labelDeviceCount.Size = new System.Drawing.Size(48, 35);
            this.labelDeviceCount.TabIndex = 40;
            this.labelDeviceCount.Text = "...";
            this.labelDeviceCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonDevices
            // 
            this.buttonDevices.Location = new System.Drawing.Point(34, 68);
            this.buttonDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDevices.Name = "buttonDevices";
            this.buttonDevices.Size = new System.Drawing.Size(83, 35);
            this.buttonDevices.TabIndex = 39;
            this.buttonDevices.Text = "Devices";
            this.buttonDevices.UseVisualStyleBackColor = true;
            this.buttonDevices.Click += new System.EventHandler(this.buttonDevices_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(34, 127);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(516, 160);
            this.listView1.TabIndex = 41;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // labelFtdiStatus
            // 
            this.labelFtdiStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelFtdiStatus.Location = new System.Drawing.Point(465, 68);
            this.labelFtdiStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFtdiStatus.Name = "labelFtdiStatus";
            this.labelFtdiStatus.Size = new System.Drawing.Size(246, 35);
            this.labelFtdiStatus.TabIndex = 42;
            this.labelFtdiStatus.Text = "...";
            this.labelFtdiStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 834);
            this.Controls.Add(this.labelFtdiStatus);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.labelDeviceCount);
            this.Controls.Add(this.buttonDevices);
            this.Controls.Add(this.labelVersion);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelDeviceCount;
        private System.Windows.Forms.Button buttonDevices;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label labelFtdiStatus;
    }
}

