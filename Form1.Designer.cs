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
            this.buttonOpen1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.startAsyncButton = new System.Windows.Forms.Button();
            this.cancelAsyncButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.Location = new System.Drawing.Point(461, 24);
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
            this.listView1.Size = new System.Drawing.Size(516, 213);
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
            // buttonOpen1
            // 
            this.buttonOpen1.Location = new System.Drawing.Point(34, 361);
            this.buttonOpen1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOpen1.Name = "buttonOpen1";
            this.buttonOpen1.Size = new System.Drawing.Size(129, 35);
            this.buttonOpen1.TabIndex = 43;
            this.buttonOpen1.Text = "OpenDevices";
            this.buttonOpen1.UseVisualStyleBackColor = true;
            this.buttonOpen1.Click += new System.EventHandler(this.buttonOpen1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 416);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(499, 133);
            this.textBox1.TabIndex = 44;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(30, 675);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(51, 20);
            this.resultLabel.TabIndex = 45;
            this.resultLabel.Text = "label1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // startAsyncButton
            // 
            this.startAsyncButton.Location = new System.Drawing.Point(34, 604);
            this.startAsyncButton.Name = "startAsyncButton";
            this.startAsyncButton.Size = new System.Drawing.Size(175, 46);
            this.startAsyncButton.TabIndex = 46;
            this.startAsyncButton.Text = "Start";
            this.startAsyncButton.UseVisualStyleBackColor = true;
            this.startAsyncButton.Click += new System.EventHandler(this.startAsyncButton_Click);
            // 
            // cancelAsyncButton
            // 
            this.cancelAsyncButton.Location = new System.Drawing.Point(358, 605);
            this.cancelAsyncButton.Name = "cancelAsyncButton";
            this.cancelAsyncButton.Size = new System.Drawing.Size(175, 45);
            this.cancelAsyncButton.TabIndex = 47;
            this.cancelAsyncButton.Text = "Cancel";
            this.cancelAsyncButton.UseVisualStyleBackColor = true;
            this.cancelAsyncButton.Click += new System.EventHandler(this.cancelAsyncButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 834);
            this.Controls.Add(this.cancelAsyncButton);
            this.Controls.Add(this.startAsyncButton);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonOpen1);
            this.Controls.Add(this.labelFtdiStatus);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.labelDeviceCount);
            this.Controls.Add(this.buttonDevices);
            this.Controls.Add(this.labelVersion);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelDeviceCount;
        private System.Windows.Forms.Button buttonDevices;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label labelFtdiStatus;
        private System.Windows.Forms.Button buttonOpen1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label resultLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button startAsyncButton;
        private System.Windows.Forms.Button cancelAsyncButton;
    }
}

