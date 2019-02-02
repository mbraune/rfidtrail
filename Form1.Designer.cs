using System.Drawing;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonDevices = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.backgroundWorker0 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker5 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker6 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker7 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker8 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker9 = new System.ComponentModel.BackgroundWorker();
            this.startAsyncButton = new System.Windows.Forms.Button();
            this.cancelAsyncButton = new System.Windows.Forms.Button();
            this.labelNum0 = new System.Windows.Forms.Label();
            this.labelNum1 = new System.Windows.Forms.Label();
            this.labelNum2 = new System.Windows.Forms.Label();
            this.labelNum3 = new System.Windows.Forms.Label();
            this.labelNum4 = new System.Windows.Forms.Label();
            this.labelNum5 = new System.Windows.Forms.Label();
            this.labelNum6 = new System.Windows.Forms.Label();
            this.labelNum7 = new System.Windows.Forms.Label();
            this.labelNum8 = new System.Windows.Forms.Label();
            this.labelNum9 = new System.Windows.Forms.Label();
            this.labelPort0 = new System.Windows.Forms.Label();
            this.labelPort1 = new System.Windows.Forms.Label();
            this.labelPort2 = new System.Windows.Forms.Label();
            this.labelPort3 = new System.Windows.Forms.Label();
            this.labelPort4 = new System.Windows.Forms.Label();
            this.labelPort5 = new System.Windows.Forms.Label();
            this.labelPort6 = new System.Windows.Forms.Label();
            this.labelPort7 = new System.Windows.Forms.Label();
            this.labelPort8 = new System.Windows.Forms.Label();
            this.labelPort9 = new System.Windows.Forms.Label();
            this.label0 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();

            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(360, 60);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(140, 26);
            this.labelVersion.TabIndex = 38;
            this.labelVersion.Text = "version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonDevices
            // 
            this.buttonDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDevices.Location = new System.Drawing.Point(16, 16);
            this.buttonDevices.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonDevices.Name = "buttonDevices";
            this.buttonDevices.Size = new System.Drawing.Size(90, 26);
            this.buttonDevices.TabIndex = 39;
            this.buttonDevices.Text = "List Devices";
            this.buttonDevices.UseVisualStyleBackColor = true;
            this.buttonDevices.Click += new System.EventHandler(this.buttonDevices_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Enabled = false;
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpen.Location = new System.Drawing.Point(220, 16);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(90, 26);
            this.buttonOpen.TabIndex = 43;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen1_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(360, 110);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(100, 52);
            this.resultLabel.TabIndex = 45;
            this.resultLabel.Text = "resultFile";
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backgroundWorker0
            // 
            this.backgroundWorker0.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker0_DoWork);
            this.backgroundWorker0.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker0_ProgressChanged);
            this.backgroundWorker0.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker0_RunWorkerCompleted);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker3_ProgressChanged);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker3_RunWorkerCompleted);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker4_DoWork);
            this.backgroundWorker4.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker4_ProgressChanged);
            this.backgroundWorker4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker4_RunWorkerCompleted);
            // 
            // backgroundWorker5
            // 
            this.backgroundWorker5.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker5_DoWork);
            this.backgroundWorker5.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker5_ProgressChanged);
            this.backgroundWorker5.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker5_RunWorkerCompleted);
            // 
            // backgroundWorker6
            // 
            this.backgroundWorker6.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker6_DoWork);
            this.backgroundWorker6.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker6_ProgressChanged);
            this.backgroundWorker6.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker6_RunWorkerCompleted);
            // 
            // backgroundWorker7
            // 
            this.backgroundWorker7.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker7_DoWork);
            this.backgroundWorker7.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker7_ProgressChanged);
            this.backgroundWorker7.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker7_RunWorkerCompleted);
            // 
            // backgroundWorker8
            // 
            this.backgroundWorker8.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker8_DoWork);
            this.backgroundWorker8.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker8_ProgressChanged);
            this.backgroundWorker8.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker8_RunWorkerCompleted);
            // 
            // backgroundWorker9
            // 
            this.backgroundWorker9.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker9_DoWork);
            this.backgroundWorker9.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker9_ProgressChanged);
            this.backgroundWorker9.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker9_RunWorkerCompleted);





            // 
            // startAsyncButton
            // 
            this.startAsyncButton.Enabled = false;
            this.startAsyncButton.Image = ((System.Drawing.Image)(resources.GetObject("startAsyncButton.Image")));
            this.startAsyncButton.Location = new System.Drawing.Point(360, 14);
            this.startAsyncButton.Name = "startAsyncButton";
            this.startAsyncButton.Size = new System.Drawing.Size(30, 30);
            this.startAsyncButton.TabIndex = 46;
            this.startAsyncButton.UseVisualStyleBackColor = true;
            this.startAsyncButton.Click += new System.EventHandler(this.startAsyncButton_Click);
            // 
            // cancelAsyncButton
            // 
            this.cancelAsyncButton.Enabled = false;
            this.cancelAsyncButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelAsyncButton.Image")));
            this.cancelAsyncButton.Location = new System.Drawing.Point(410, 14);
            this.cancelAsyncButton.Name = "cancelAsyncButton";
            this.cancelAsyncButton.Size = new System.Drawing.Size(30, 30);
            this.cancelAsyncButton.TabIndex = 47;
            this.cancelAsyncButton.UseVisualStyleBackColor = true;
            this.cancelAsyncButton.Click += new System.EventHandler(this.cancelAsyncButton_Click);
            // 
            // labelNum0
            // 
            this.labelNum0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum0.Location = new System.Drawing.Point(16, 60);
            this.labelNum0.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum0.Name = "labelNum0";
            this.labelNum0.Size = new System.Drawing.Size(90, 26);
            this.labelNum0.TabIndex = 48;
            this.labelNum0.Text = "...";
            this.labelNum0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum1
            // 
            this.labelNum1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum1.Location = new System.Drawing.Point(16, 86);
            this.labelNum1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum1.Name = "labelNum1";
            this.labelNum1.Size = new System.Drawing.Size(90, 26);
            this.labelNum1.TabIndex = 49;
            this.labelNum1.Text = "...";
            this.labelNum1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum2
            // 
            this.labelNum2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum2.Location = new System.Drawing.Point(16, 112);
            this.labelNum2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum2.Name = "labelNum2";
            this.labelNum2.Size = new System.Drawing.Size(90, 26);
            this.labelNum2.TabIndex = 50;
            this.labelNum2.Text = "...";
            this.labelNum2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum3
            // 
            this.labelNum3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum3.Location = new System.Drawing.Point(16, 138);
            this.labelNum3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum3.Name = "labelNum3";
            this.labelNum3.Size = new System.Drawing.Size(90, 26);
            this.labelNum3.TabIndex = 51;
            this.labelNum3.Text = "...";
            this.labelNum3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum4
            // 
            this.labelNum4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum4.Location = new System.Drawing.Point(16, 164);
            this.labelNum4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum4.Name = "labelNum4";
            this.labelNum4.Size = new System.Drawing.Size(90, 26);
            this.labelNum4.TabIndex = 52;
            this.labelNum4.Text = "...";
            this.labelNum4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum5
            // 
            this.labelNum5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum5.Location = new System.Drawing.Point(16, 190);
            this.labelNum5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum5.Name = "labelNum5";
            this.labelNum5.Size = new System.Drawing.Size(90, 26);
            this.labelNum5.TabIndex = 52;
            this.labelNum5.Text = "...";
            this.labelNum5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum6
            // 
            this.labelNum6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum6.Location = new System.Drawing.Point(16, 216);
            this.labelNum6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum6.Name = "labelNum6";
            this.labelNum6.Size = new System.Drawing.Size(90, 26);
            this.labelNum6.TabIndex = 52;
            this.labelNum6.Text = "...";
            this.labelNum6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum7
            // 
            this.labelNum7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum7.Location = new System.Drawing.Point(16, 242);
            this.labelNum7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum7.Name = "labelNum7";
            this.labelNum7.Size = new System.Drawing.Size(90, 26);
            this.labelNum7.TabIndex = 52;
            this.labelNum7.Text = "...";
            this.labelNum7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum8
            // 
            this.labelNum8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum8.Location = new System.Drawing.Point(16, 268);
            this.labelNum8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum8.Name = "labelNum8";
            this.labelNum8.Size = new System.Drawing.Size(90, 26);
            this.labelNum8.TabIndex = 52;
            this.labelNum8.Text = "...";
            this.labelNum8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNum9
            // 
            this.labelNum9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNum9.Location = new System.Drawing.Point(16, 294);
            this.labelNum9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelNum9.Name = "labelNum9";
            this.labelNum9.Size = new System.Drawing.Size(90, 26);
            this.labelNum9.TabIndex = 52;
            this.labelNum9.Text = "...";
            this.labelNum9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort0
            // 
            this.labelPort0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort0.Location = new System.Drawing.Point(110, 60);
            this.labelPort0.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort0.Name = "labelPort0";
            this.labelPort0.Size = new System.Drawing.Size(90, 26);
            this.labelPort0.TabIndex = 48;
            this.labelPort0.Text = "...";
            this.labelPort0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort1
            // 
            this.labelPort1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort1.Location = new System.Drawing.Point(110, 86);
            this.labelPort1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort1.Name = "labelPort1";
            this.labelPort1.Size = new System.Drawing.Size(90, 26);
            this.labelPort1.TabIndex = 49;
            this.labelPort1.Text = "...";
            this.labelPort1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort2
            // 
            this.labelPort2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort2.Location = new System.Drawing.Point(110, 112);
            this.labelPort2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort2.Name = "labelPort2";
            this.labelPort2.Size = new System.Drawing.Size(90, 26);
            this.labelPort2.TabIndex = 50;
            this.labelPort2.Text = "...";
            this.labelPort2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort3
            // 
            this.labelPort3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort3.Location = new System.Drawing.Point(110, 138);
            this.labelPort3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort3.Name = "labelPort3";
            this.labelPort3.Size = new System.Drawing.Size(90, 26);
            this.labelPort3.TabIndex = 51;
            this.labelPort3.Text = "...";
            this.labelPort3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort4
            // 
            this.labelPort4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort4.Location = new System.Drawing.Point(110, 164);
            this.labelPort4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort4.Name = "labelPort4";
            this.labelPort4.Size = new System.Drawing.Size(90, 26);
            this.labelPort4.TabIndex = 52;
            this.labelPort4.Text = "...";
            this.labelPort4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort5
            // 
            this.labelPort5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort5.Location = new System.Drawing.Point(110, 190);
            this.labelPort5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort5.Name = "labelPort5";
            this.labelPort5.Size = new System.Drawing.Size(90, 26);
            this.labelPort5.TabIndex = 52;
            this.labelPort5.Text = "...";
            this.labelPort5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort6
            // 
            this.labelPort6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort6.Location = new System.Drawing.Point(110, 216);
            this.labelPort6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort6.Name = "labelPort6";
            this.labelPort6.Size = new System.Drawing.Size(90, 26);
            this.labelPort6.TabIndex = 52;
            this.labelPort6.Text = "...";
            this.labelPort6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort7
            // 
            this.labelPort7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort7.Location = new System.Drawing.Point(110, 242);
            this.labelPort7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort7.Name = "labelPort7";
            this.labelPort7.Size = new System.Drawing.Size(90, 26);
            this.labelPort7.TabIndex = 52;
            this.labelPort7.Text = "...";
            this.labelPort7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort8
            // 
            this.labelPort8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort8.Location = new System.Drawing.Point(110, 268);
            this.labelPort8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort8.Name = "labelPort8";
            this.labelPort8.Size = new System.Drawing.Size(90, 26);
            this.labelPort8.TabIndex = 52;
            this.labelPort8.Text = "...";
            this.labelPort8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort9
            // 
            this.labelPort9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPort9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort9.Location = new System.Drawing.Point(110, 294);
            this.labelPort9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPort9.Name = "labelPort9";
            this.labelPort9.Size = new System.Drawing.Size(90, 26);
            this.labelPort9.TabIndex = 52;
            this.labelPort9.Text = "...";
            this.labelPort9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label0
            // 
            this.label0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label0.Location = new System.Drawing.Point(220, 60);
            this.label0.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(89, 26);
            this.label0.TabIndex = 48;
            this.label0.Text = "...";
            this.label0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 26);
            this.label1.TabIndex = 49;
            this.label1.Text = "...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(220, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 26);
            this.label2.TabIndex = 50;
            this.label2.Text = "...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(220, 138);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 26);
            this.label3.TabIndex = 51;
            this.label3.Text = "...";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(220, 164);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 26);
            this.label4.TabIndex = 52;
            this.label4.Text = "...";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(220, 190);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 26);
            this.label5.TabIndex = 52;
            this.label5.Text = "...";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(220, 216);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 26);
            this.label6.TabIndex = 52;
            this.label6.Text = "...";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(220, 242);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 26);
            this.label7.TabIndex = 52;
            this.label7.Text = "...";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(220, 268);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 26);
            this.label8.TabIndex = 52;
            this.label8.Text = "...";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(220, 294);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 26);
            this.label9.TabIndex = 52;
            this.label9.Text = "...";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 390);
            this.Controls.Add(this.labelNum0);
            this.Controls.Add(this.labelNum1);
            this.Controls.Add(this.labelNum2);
            this.Controls.Add(this.labelNum3);
            this.Controls.Add(this.labelNum4);
            this.Controls.Add(this.labelNum5);
            this.Controls.Add(this.labelNum6);
            this.Controls.Add(this.labelNum7);
            this.Controls.Add(this.labelNum8);
            this.Controls.Add(this.labelNum9);
            this.Controls.Add(this.labelPort0);
            this.Controls.Add(this.labelPort1);
            this.Controls.Add(this.labelPort2);
            this.Controls.Add(this.labelPort3);
            this.Controls.Add(this.labelPort4);
            this.Controls.Add(this.labelPort5);
            this.Controls.Add(this.labelPort6);
            this.Controls.Add(this.labelPort7);
            this.Controls.Add(this.labelPort8);
            this.Controls.Add(this.labelPort9);
            this.Controls.Add(this.label0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cancelAsyncButton);
            this.Controls.Add(this.startAsyncButton);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonDevices);
            this.Controls.Add(this.labelVersion);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Track RFID";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonDevices;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Button startAsyncButton;
        private System.Windows.Forms.Button cancelAsyncButton;
        private System.Windows.Forms.Label labelNum0;
        private System.Windows.Forms.Label labelNum1;
        private System.Windows.Forms.Label labelNum2;
        private System.Windows.Forms.Label labelNum3;
        private System.Windows.Forms.Label labelNum4;
        private System.Windows.Forms.Label labelNum5;
        private System.Windows.Forms.Label labelNum6;
        private System.Windows.Forms.Label labelNum7;
        private System.Windows.Forms.Label labelNum8;
        private System.Windows.Forms.Label labelNum9;
        private System.Windows.Forms.Label labelPort0;
        private System.Windows.Forms.Label labelPort1;
        private System.Windows.Forms.Label labelPort2;
        private System.Windows.Forms.Label labelPort3;
        private System.Windows.Forms.Label labelPort4;
        private System.Windows.Forms.Label labelPort5;
        private System.Windows.Forms.Label labelPort6;
        private System.Windows.Forms.Label labelPort7;
        private System.Windows.Forms.Label labelPort8;
        private System.Windows.Forms.Label labelPort9;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.ComponentModel.BackgroundWorker backgroundWorker0;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.ComponentModel.BackgroundWorker backgroundWorker5;
        private System.ComponentModel.BackgroundWorker backgroundWorker6;
        private System.ComponentModel.BackgroundWorker backgroundWorker7;
        private System.ComponentModel.BackgroundWorker backgroundWorker8;
        private System.ComponentModel.BackgroundWorker backgroundWorker9;
    }
}

