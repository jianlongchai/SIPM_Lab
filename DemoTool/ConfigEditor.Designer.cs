namespace DemoTool {
    partial class ConfigEditor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose ( );
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigEditor));
            this.grpReadOnlyParams = new System.Windows.Forms.GroupBox();
            this.pnMFGName = new System.Windows.Forms.Panel();
            this.txtMFGName = new System.Windows.Forms.TextBox();
            this.lblMFGName = new System.Windows.Forms.Label();
            this.grpWriteParams = new System.Windows.Forms.GroupBox();
            this.cbEnablePixelReverse = new System.Windows.Forms.ComboBox();
            this.lblEnablePixelReverse = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtQualifiedResolutionRangeFile = new System.Windows.Forms.TextBox();
            this.lblQualifiedResolutionRangeFile = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtQualifiedPeakRangeFile = new System.Windows.Forms.TextBox();
            this.lblQualifiedPeakRangeFile = new System.Windows.Forms.Label();
            this.cbDefaultQualifiedType = new System.Windows.Forms.ComboBox();
            this.lblDefaultQualifiedType = new System.Windows.Forms.Label();
            this.gpConfigVbias = new System.Windows.Forms.GroupBox();
            this.lblAutoAdjustVbias = new System.Windows.Forms.Label();
            this.rdEnableAutoVbiasAdjust = new System.Windows.Forms.PictureBox();
            this.grpReportCustomize = new System.Windows.Forms.GroupBox();
            this.lblIncludeCountGreyPic = new System.Windows.Forms.Label();
            this.lblIncludeEnergyGreyPic = new System.Windows.Forms.Label();
            this.rdIncludeCountGreyPic = new System.Windows.Forms.PictureBox();
            this.rdIncludeEnergyGreyPic = new System.Windows.Forms.PictureBox();
            this.lblIncludeResolutionGreyPic = new System.Windows.Forms.Label();
            this.rdIncludeResolutionGreyPic = new System.Windows.Forms.PictureBox();
            this.lblUseDifferentRanges = new System.Windows.Forms.Label();
            this.lblIncludeEnergyCount = new System.Windows.Forms.Label();
            this.rdUseDifferentRanges = new System.Windows.Forms.PictureBox();
            this.rdIncludeEnergyCount = new System.Windows.Forms.PictureBox();
            this.lblIncludeEnergySpectrum = new System.Windows.Forms.Label();
            this.rdIncludeEnergySpectrum = new System.Windows.Forms.PictureBox();
            this.lblEnergyResolution = new System.Windows.Forms.Label();
            this.rdEnergyResolution = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtBinSize = new System.Windows.Forms.TextBox();
            this.lblBinSize = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMaxEnergyCountPerPixel = new System.Windows.Forms.TextBox();
            this.lblMaxEnergyCountPerPixel = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.btnSaveFactoryConfig = new System.Windows.Forms.Button();
            this.btnConfigRefresh = new System.Windows.Forms.Button();
            this.grpReadOnlyParams.SuspendLayout();
            this.pnMFGName.SuspendLayout();
            this.grpWriteParams.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gpConfigVbias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdEnableAutoVbiasAdjust)).BeginInit();
            this.grpReportCustomize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeCountGreyPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeEnergyGreyPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeResolutionGreyPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdUseDifferentRanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeEnergyCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeEnergySpectrum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdEnergyResolution)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpReadOnlyParams
            // 
            this.grpReadOnlyParams.BackColor = System.Drawing.Color.Transparent;
            this.grpReadOnlyParams.Controls.Add(this.pnMFGName);
            this.grpReadOnlyParams.Controls.Add(this.lblMFGName);
            this.grpReadOnlyParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpReadOnlyParams.Location = new System.Drawing.Point(16, 15);
            this.grpReadOnlyParams.Margin = new System.Windows.Forms.Padding(4);
            this.grpReadOnlyParams.Name = "grpReadOnlyParams";
            this.grpReadOnlyParams.Padding = new System.Windows.Forms.Padding(4);
            this.grpReadOnlyParams.Size = new System.Drawing.Size(935, 174);
            this.grpReadOnlyParams.TabIndex = 0;
            this.grpReadOnlyParams.TabStop = false;
            this.grpReadOnlyParams.Text = "Factory Default Parameters(Read Only)";
            // 
            // pnMFGName
            // 
            this.pnMFGName.BackColor = System.Drawing.Color.Black;
            this.pnMFGName.Controls.Add(this.txtMFGName);
            this.pnMFGName.Location = new System.Drawing.Point(103, 25);
            this.pnMFGName.Margin = new System.Windows.Forms.Padding(4);
            this.pnMFGName.Name = "pnMFGName";
            this.pnMFGName.Size = new System.Drawing.Size(135, 25);
            this.pnMFGName.TabIndex = 6;
            // 
            // txtMFGName
            // 
            this.txtMFGName.BackColor = System.Drawing.SystemColors.Window;
            this.txtMFGName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMFGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMFGName.Location = new System.Drawing.Point(1, 1);
            this.txtMFGName.Margin = new System.Windows.Forms.Padding(4);
            this.txtMFGName.Name = "txtMFGName";
            this.txtMFGName.Size = new System.Drawing.Size(133, 22);
            this.txtMFGName.TabIndex = 5;
            this.txtMFGName.Text = "TOFTEK";
            // 
            // lblMFGName
            // 
            this.lblMFGName.AutoSize = true;
            this.lblMFGName.Location = new System.Drawing.Point(8, 28);
            this.lblMFGName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMFGName.Name = "lblMFGName";
            this.lblMFGName.Size = new System.Drawing.Size(92, 17);
            this.lblMFGName.TabIndex = 0;
            this.lblMFGName.Text = "MFG Name:";
            // 
            // grpWriteParams
            // 
            this.grpWriteParams.Controls.Add(this.cbEnablePixelReverse);
            this.grpWriteParams.Controls.Add(this.lblEnablePixelReverse);
            this.grpWriteParams.Controls.Add(this.panel4);
            this.grpWriteParams.Controls.Add(this.lblQualifiedResolutionRangeFile);
            this.grpWriteParams.Controls.Add(this.panel3);
            this.grpWriteParams.Controls.Add(this.lblQualifiedPeakRangeFile);
            this.grpWriteParams.Controls.Add(this.cbDefaultQualifiedType);
            this.grpWriteParams.Controls.Add(this.lblDefaultQualifiedType);
            this.grpWriteParams.Controls.Add(this.gpConfigVbias);
            this.grpWriteParams.Controls.Add(this.grpReportCustomize);
            this.grpWriteParams.Controls.Add(this.panel2);
            this.grpWriteParams.Controls.Add(this.lblBinSize);
            this.grpWriteParams.Controls.Add(this.panel1);
            this.grpWriteParams.Controls.Add(this.lblMaxEnergyCountPerPixel);
            this.grpWriteParams.Controls.Add(this.cbLanguage);
            this.grpWriteParams.Controls.Add(this.lblLanguage);
            this.grpWriteParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpWriteParams.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpWriteParams.Location = new System.Drawing.Point(16, 251);
            this.grpWriteParams.Margin = new System.Windows.Forms.Padding(4);
            this.grpWriteParams.Name = "grpWriteParams";
            this.grpWriteParams.Padding = new System.Windows.Forms.Padding(4);
            this.grpWriteParams.Size = new System.Drawing.Size(935, 399);
            this.grpWriteParams.TabIndex = 1;
            this.grpWriteParams.TabStop = false;
            this.grpWriteParams.Text = "Factory Default Parameters(Configurable)";
            // 
            // cbEnablePixelReverse
            // 
            this.cbEnablePixelReverse.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.cbEnablePixelReverse.FormattingEnabled = true;
            this.cbEnablePixelReverse.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbEnablePixelReverse.Location = new System.Drawing.Point(501, 260);
            this.cbEnablePixelReverse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbEnablePixelReverse.Name = "cbEnablePixelReverse";
            this.cbEnablePixelReverse.Size = new System.Drawing.Size(133, 25);
            this.cbEnablePixelReverse.TabIndex = 83;
            this.cbEnablePixelReverse.SelectionChangeCommitted += new System.EventHandler(this.cbEnablePixelReverse_SelectionChangeCommitted);
            // 
            // lblEnablePixelReverse
            // 
            this.lblEnablePixelReverse.AutoSize = true;
            this.lblEnablePixelReverse.Location = new System.Drawing.Point(500, 238);
            this.lblEnablePixelReverse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnablePixelReverse.Name = "lblEnablePixelReverse";
            this.lblEnablePixelReverse.Size = new System.Drawing.Size(220, 17);
            this.lblEnablePixelReverse.TabIndex = 82;
            this.lblEnablePixelReverse.Text = "Enable Pixel Display Reverse";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Controls.Add(this.txtQualifiedResolutionRangeFile);
            this.panel4.Location = new System.Drawing.Point(500, 191);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(222, 25);
            this.panel4.TabIndex = 81;
            // 
            // txtQualifiedResolutionRangeFile
            // 
            this.txtQualifiedResolutionRangeFile.BackColor = System.Drawing.SystemColors.Window;
            this.txtQualifiedResolutionRangeFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQualifiedResolutionRangeFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQualifiedResolutionRangeFile.Location = new System.Drawing.Point(1, 1);
            this.txtQualifiedResolutionRangeFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtQualifiedResolutionRangeFile.Name = "txtQualifiedResolutionRangeFile";
            this.txtQualifiedResolutionRangeFile.Size = new System.Drawing.Size(219, 22);
            this.txtQualifiedResolutionRangeFile.TabIndex = 5;
            this.txtQualifiedResolutionRangeFile.Text = "100";
            // 
            // lblQualifiedResolutionRangeFile
            // 
            this.lblQualifiedResolutionRangeFile.AutoSize = true;
            this.lblQualifiedResolutionRangeFile.Location = new System.Drawing.Point(500, 170);
            this.lblQualifiedResolutionRangeFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQualifiedResolutionRangeFile.Name = "lblQualifiedResolutionRangeFile";
            this.lblQualifiedResolutionRangeFile.Size = new System.Drawing.Size(237, 17);
            this.lblQualifiedResolutionRangeFile.TabIndex = 81;
            this.lblQualifiedResolutionRangeFile.Text = "Resolution Qualified File Name:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.txtQualifiedPeakRangeFile);
            this.panel3.Location = new System.Drawing.Point(500, 127);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(222, 25);
            this.panel3.TabIndex = 80;
            // 
            // txtQualifiedPeakRangeFile
            // 
            this.txtQualifiedPeakRangeFile.BackColor = System.Drawing.SystemColors.Window;
            this.txtQualifiedPeakRangeFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQualifiedPeakRangeFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQualifiedPeakRangeFile.Location = new System.Drawing.Point(1, 1);
            this.txtQualifiedPeakRangeFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtQualifiedPeakRangeFile.Name = "txtQualifiedPeakRangeFile";
            this.txtQualifiedPeakRangeFile.Size = new System.Drawing.Size(219, 22);
            this.txtQualifiedPeakRangeFile.TabIndex = 5;
            this.txtQualifiedPeakRangeFile.Text = "100";
            // 
            // lblQualifiedPeakRangeFile
            // 
            this.lblQualifiedPeakRangeFile.AutoSize = true;
            this.lblQualifiedPeakRangeFile.Location = new System.Drawing.Point(500, 103);
            this.lblQualifiedPeakRangeFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQualifiedPeakRangeFile.Name = "lblQualifiedPeakRangeFile";
            this.lblQualifiedPeakRangeFile.Size = new System.Drawing.Size(247, 17);
            this.lblQualifiedPeakRangeFile.TabIndex = 79;
            this.lblQualifiedPeakRangeFile.Text = "Energy Peak Qualified FileName:";
            // 
            // cbDefaultQualifiedType
            // 
            this.cbDefaultQualifiedType.AutoCompleteCustomSource.AddRange(new string[] {
            "English",
            "简体中文"});
            this.cbDefaultQualifiedType.FormattingEnabled = true;
            this.cbDefaultQualifiedType.Items.AddRange(new object[] {
            "EnergyPeak",
            "EnergyResolution"});
            this.cbDefaultQualifiedType.Location = new System.Drawing.Point(500, 56);
            this.cbDefaultQualifiedType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbDefaultQualifiedType.Name = "cbDefaultQualifiedType";
            this.cbDefaultQualifiedType.Size = new System.Drawing.Size(133, 25);
            this.cbDefaultQualifiedType.TabIndex = 78;
            // 
            // lblDefaultQualifiedType
            // 
            this.lblDefaultQualifiedType.AutoSize = true;
            this.lblDefaultQualifiedType.Location = new System.Drawing.Point(500, 28);
            this.lblDefaultQualifiedType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultQualifiedType.Name = "lblDefaultQualifiedType";
            this.lblDefaultQualifiedType.Size = new System.Drawing.Size(172, 17);
            this.lblDefaultQualifiedType.TabIndex = 77;
            this.lblDefaultQualifiedType.Text = "Defaut Qualified Type:";
            // 
            // gpConfigVbias
            // 
            this.gpConfigVbias.Controls.Add(this.lblAutoAdjustVbias);
            this.gpConfigVbias.Controls.Add(this.rdEnableAutoVbiasAdjust);
            this.gpConfigVbias.Location = new System.Drawing.Point(500, 304);
            this.gpConfigVbias.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpConfigVbias.Name = "gpConfigVbias";
            this.gpConfigVbias.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpConfigVbias.Size = new System.Drawing.Size(359, 59);
            this.gpConfigVbias.TabIndex = 76;
            this.gpConfigVbias.TabStop = false;
            this.gpConfigVbias.Text = "Configuration for VBIAS";
            // 
            // lblAutoAdjustVbias
            // 
            this.lblAutoAdjustVbias.AutoSize = true;
            this.lblAutoAdjustVbias.Location = new System.Drawing.Point(53, 28);
            this.lblAutoAdjustVbias.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoAdjustVbias.Name = "lblAutoAdjustVbias";
            this.lblAutoAdjustVbias.Size = new System.Drawing.Size(189, 17);
            this.lblAutoAdjustVbias.TabIndex = 71;
            this.lblAutoAdjustVbias.Text = "Auto Temperature Adjust";
            // 
            // rdEnableAutoVbiasAdjust
            // 
            this.rdEnableAutoVbiasAdjust.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.rdEnableAutoVbiasAdjust.Location = new System.Drawing.Point(11, 30);
            this.rdEnableAutoVbiasAdjust.Margin = new System.Windows.Forms.Padding(4);
            this.rdEnableAutoVbiasAdjust.Name = "rdEnableAutoVbiasAdjust";
            this.rdEnableAutoVbiasAdjust.Size = new System.Drawing.Size(16, 16);
            this.rdEnableAutoVbiasAdjust.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rdEnableAutoVbiasAdjust.TabIndex = 70;
            this.rdEnableAutoVbiasAdjust.TabStop = false;
            this.rdEnableAutoVbiasAdjust.Tag = "on";
            this.rdEnableAutoVbiasAdjust.Click += new System.EventHandler(this.rdEnableAutoVbiasAdjust_Click);
            // 
            // grpReportCustomize
            // 
            this.grpReportCustomize.Controls.Add(this.lblIncludeCountGreyPic);
            this.grpReportCustomize.Controls.Add(this.lblIncludeEnergyGreyPic);
            this.grpReportCustomize.Controls.Add(this.rdIncludeCountGreyPic);
            this.grpReportCustomize.Controls.Add(this.rdIncludeEnergyGreyPic);
            this.grpReportCustomize.Controls.Add(this.lblIncludeResolutionGreyPic);
            this.grpReportCustomize.Controls.Add(this.rdIncludeResolutionGreyPic);
            this.grpReportCustomize.Controls.Add(this.lblUseDifferentRanges);
            this.grpReportCustomize.Controls.Add(this.lblIncludeEnergyCount);
            this.grpReportCustomize.Controls.Add(this.rdUseDifferentRanges);
            this.grpReportCustomize.Controls.Add(this.rdIncludeEnergyCount);
            this.grpReportCustomize.Controls.Add(this.lblIncludeEnergySpectrum);
            this.grpReportCustomize.Controls.Add(this.rdIncludeEnergySpectrum);
            this.grpReportCustomize.Controls.Add(this.lblEnergyResolution);
            this.grpReportCustomize.Controls.Add(this.rdEnergyResolution);
            this.grpReportCustomize.Location = new System.Drawing.Point(9, 140);
            this.grpReportCustomize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpReportCustomize.Name = "grpReportCustomize";
            this.grpReportCustomize.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpReportCustomize.Size = new System.Drawing.Size(363, 242);
            this.grpReportCustomize.TabIndex = 14;
            this.grpReportCustomize.TabStop = false;
            this.grpReportCustomize.Text = "Options In Report";
            // 
            // lblIncludeCountGreyPic
            // 
            this.lblIncludeCountGreyPic.AutoSize = true;
            this.lblIncludeCountGreyPic.Location = new System.Drawing.Point(53, 210);
            this.lblIncludeCountGreyPic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncludeCountGreyPic.Name = "lblIncludeCountGreyPic";
            this.lblIncludeCountGreyPic.Size = new System.Drawing.Size(247, 17);
            this.lblIncludeCountGreyPic.TabIndex = 81;
            this.lblIncludeCountGreyPic.Text = "Include Energy Count Grey Level";
            // 
            // lblIncludeEnergyGreyPic
            // 
            this.lblIncludeEnergyGreyPic.AutoSize = true;
            this.lblIncludeEnergyGreyPic.Location = new System.Drawing.Point(53, 180);
            this.lblIncludeEnergyGreyPic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncludeEnergyGreyPic.Name = "lblIncludeEnergyGreyPic";
            this.lblIncludeEnergyGreyPic.Size = new System.Drawing.Size(241, 17);
            this.lblIncludeEnergyGreyPic.TabIndex = 80;
            this.lblIncludeEnergyGreyPic.Text = "Include Energy Peak Grey Level";
            // 
            // rdIncludeCountGreyPic
            // 
            this.rdIncludeCountGreyPic.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.rdIncludeCountGreyPic.Location = new System.Drawing.Point(11, 210);
            this.rdIncludeCountGreyPic.Margin = new System.Windows.Forms.Padding(4);
            this.rdIncludeCountGreyPic.Name = "rdIncludeCountGreyPic";
            this.rdIncludeCountGreyPic.Size = new System.Drawing.Size(16, 16);
            this.rdIncludeCountGreyPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rdIncludeCountGreyPic.TabIndex = 79;
            this.rdIncludeCountGreyPic.TabStop = false;
            this.rdIncludeCountGreyPic.Tag = "on";
            this.rdIncludeCountGreyPic.Click += new System.EventHandler(this.rdIncludeCountGreyPic_Click);
            // 
            // rdIncludeEnergyGreyPic
            // 
            this.rdIncludeEnergyGreyPic.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.rdIncludeEnergyGreyPic.Location = new System.Drawing.Point(11, 180);
            this.rdIncludeEnergyGreyPic.Margin = new System.Windows.Forms.Padding(4);
            this.rdIncludeEnergyGreyPic.Name = "rdIncludeEnergyGreyPic";
            this.rdIncludeEnergyGreyPic.Size = new System.Drawing.Size(16, 16);
            this.rdIncludeEnergyGreyPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rdIncludeEnergyGreyPic.TabIndex = 78;
            this.rdIncludeEnergyGreyPic.TabStop = false;
            this.rdIncludeEnergyGreyPic.Tag = "on";
            this.rdIncludeEnergyGreyPic.Click += new System.EventHandler(this.rdIncludeEnergyGreyPic_Click);
            // 
            // lblIncludeResolutionGreyPic
            // 
            this.lblIncludeResolutionGreyPic.AutoSize = true;
            this.lblIncludeResolutionGreyPic.Location = new System.Drawing.Point(53, 150);
            this.lblIncludeResolutionGreyPic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncludeResolutionGreyPic.Name = "lblIncludeResolutionGreyPic";
            this.lblIncludeResolutionGreyPic.Size = new System.Drawing.Size(282, 17);
            this.lblIncludeResolutionGreyPic.TabIndex = 77;
            this.lblIncludeResolutionGreyPic.Text = "Include Energy Resolution Grey Level";
            // 
            // rdIncludeResolutionGreyPic
            // 
            this.rdIncludeResolutionGreyPic.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.rdIncludeResolutionGreyPic.Location = new System.Drawing.Point(11, 150);
            this.rdIncludeResolutionGreyPic.Margin = new System.Windows.Forms.Padding(4);
            this.rdIncludeResolutionGreyPic.Name = "rdIncludeResolutionGreyPic";
            this.rdIncludeResolutionGreyPic.Size = new System.Drawing.Size(16, 16);
            this.rdIncludeResolutionGreyPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rdIncludeResolutionGreyPic.TabIndex = 76;
            this.rdIncludeResolutionGreyPic.TabStop = false;
            this.rdIncludeResolutionGreyPic.Tag = "on";
            this.rdIncludeResolutionGreyPic.Click += new System.EventHandler(this.rdIncludeResolutionGreyPic_Click);
            // 
            // lblUseDifferentRanges
            // 
            this.lblUseDifferentRanges.AutoSize = true;
            this.lblUseDifferentRanges.Location = new System.Drawing.Point(53, 120);
            this.lblUseDifferentRanges.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUseDifferentRanges.Name = "lblUseDifferentRanges";
            this.lblUseDifferentRanges.Size = new System.Drawing.Size(240, 17);
            this.lblUseDifferentRanges.TabIndex = 73;
            this.lblUseDifferentRanges.Text = "Use Different Ranges For Pixels";
            // 
            // lblIncludeEnergyCount
            // 
            this.lblIncludeEnergyCount.AutoSize = true;
            this.lblIncludeEnergyCount.Location = new System.Drawing.Point(53, 90);
            this.lblIncludeEnergyCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncludeEnergyCount.Name = "lblIncludeEnergyCount";
            this.lblIncludeEnergyCount.Size = new System.Drawing.Size(106, 17);
            this.lblIncludeEnergyCount.TabIndex = 75;
            this.lblIncludeEnergyCount.Text = "Energy Count";
            // 
            // rdUseDifferentRanges
            // 
            this.rdUseDifferentRanges.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.rdUseDifferentRanges.Location = new System.Drawing.Point(11, 120);
            this.rdUseDifferentRanges.Margin = new System.Windows.Forms.Padding(4);
            this.rdUseDifferentRanges.Name = "rdUseDifferentRanges";
            this.rdUseDifferentRanges.Size = new System.Drawing.Size(16, 16);
            this.rdUseDifferentRanges.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rdUseDifferentRanges.TabIndex = 72;
            this.rdUseDifferentRanges.TabStop = false;
            this.rdUseDifferentRanges.Tag = "on";
            this.rdUseDifferentRanges.Click += new System.EventHandler(this.rdUseDifferentRanges_Click);
            // 
            // rdIncludeEnergyCount
            // 
            this.rdIncludeEnergyCount.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.rdIncludeEnergyCount.Location = new System.Drawing.Point(11, 90);
            this.rdIncludeEnergyCount.Margin = new System.Windows.Forms.Padding(4);
            this.rdIncludeEnergyCount.Name = "rdIncludeEnergyCount";
            this.rdIncludeEnergyCount.Size = new System.Drawing.Size(16, 16);
            this.rdIncludeEnergyCount.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rdIncludeEnergyCount.TabIndex = 74;
            this.rdIncludeEnergyCount.TabStop = false;
            this.rdIncludeEnergyCount.Tag = "on";
            this.rdIncludeEnergyCount.Click += new System.EventHandler(this.rdIncludeEnergyCount_Click);
            // 
            // lblIncludeEnergySpectrum
            // 
            this.lblIncludeEnergySpectrum.AutoSize = true;
            this.lblIncludeEnergySpectrum.Location = new System.Drawing.Point(53, 60);
            this.lblIncludeEnergySpectrum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncludeEnergySpectrum.Name = "lblIncludeEnergySpectrum";
            this.lblIncludeEnergySpectrum.Size = new System.Drawing.Size(132, 17);
            this.lblIncludeEnergySpectrum.TabIndex = 73;
            this.lblIncludeEnergySpectrum.Text = "Energy Spectrum";
            // 
            // rdIncludeEnergySpectrum
            // 
            this.rdIncludeEnergySpectrum.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.rdIncludeEnergySpectrum.Location = new System.Drawing.Point(11, 60);
            this.rdIncludeEnergySpectrum.Margin = new System.Windows.Forms.Padding(4);
            this.rdIncludeEnergySpectrum.Name = "rdIncludeEnergySpectrum";
            this.rdIncludeEnergySpectrum.Size = new System.Drawing.Size(16, 16);
            this.rdIncludeEnergySpectrum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rdIncludeEnergySpectrum.TabIndex = 72;
            this.rdIncludeEnergySpectrum.TabStop = false;
            this.rdIncludeEnergySpectrum.Tag = "on";
            this.rdIncludeEnergySpectrum.Click += new System.EventHandler(this.rdIncludeEnergySpectrum_Click);
            // 
            // lblEnergyResolution
            // 
            this.lblEnergyResolution.AutoSize = true;
            this.lblEnergyResolution.Location = new System.Drawing.Point(53, 30);
            this.lblEnergyResolution.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnergyResolution.Name = "lblEnergyResolution";
            this.lblEnergyResolution.Size = new System.Drawing.Size(141, 17);
            this.lblEnergyResolution.TabIndex = 71;
            this.lblEnergyResolution.Text = "Energy Resolution";
            // 
            // rdEnergyResolution
            // 
            this.rdEnergyResolution.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.rdEnergyResolution.Location = new System.Drawing.Point(11, 30);
            this.rdEnergyResolution.Margin = new System.Windows.Forms.Padding(4);
            this.rdEnergyResolution.Name = "rdEnergyResolution";
            this.rdEnergyResolution.Size = new System.Drawing.Size(16, 16);
            this.rdEnergyResolution.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rdEnergyResolution.TabIndex = 70;
            this.rdEnergyResolution.TabStop = false;
            this.rdEnergyResolution.Tag = "on";
            this.rdEnergyResolution.Click += new System.EventHandler(this.rdEnergyResolution_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.txtBinSize);
            this.panel2.Location = new System.Drawing.Point(237, 95);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(135, 25);
            this.panel2.TabIndex = 12;
            // 
            // txtBinSize
            // 
            this.txtBinSize.BackColor = System.Drawing.SystemColors.Window;
            this.txtBinSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBinSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBinSize.Location = new System.Drawing.Point(1, 1);
            this.txtBinSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtBinSize.Name = "txtBinSize";
            this.txtBinSize.Size = new System.Drawing.Size(133, 22);
            this.txtBinSize.TabIndex = 5;
            this.txtBinSize.Text = "100";
            // 
            // lblBinSize
            // 
            this.lblBinSize.AutoSize = true;
            this.lblBinSize.Location = new System.Drawing.Point(8, 103);
            this.lblBinSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBinSize.Name = "lblBinSize";
            this.lblBinSize.Size = new System.Drawing.Size(67, 17);
            this.lblBinSize.TabIndex = 12;
            this.lblBinSize.Text = "BinSize:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.txtMaxEnergyCountPerPixel);
            this.panel1.Location = new System.Drawing.Point(236, 58);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(135, 25);
            this.panel1.TabIndex = 11;
            // 
            // txtMaxEnergyCountPerPixel
            // 
            this.txtMaxEnergyCountPerPixel.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaxEnergyCountPerPixel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxEnergyCountPerPixel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxEnergyCountPerPixel.Location = new System.Drawing.Point(1, 1);
            this.txtMaxEnergyCountPerPixel.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxEnergyCountPerPixel.Name = "txtMaxEnergyCountPerPixel";
            this.txtMaxEnergyCountPerPixel.Size = new System.Drawing.Size(133, 22);
            this.txtMaxEnergyCountPerPixel.TabIndex = 5;
            this.txtMaxEnergyCountPerPixel.Text = "65536";
            // 
            // lblMaxEnergyCountPerPixel
            // 
            this.lblMaxEnergyCountPerPixel.AutoSize = true;
            this.lblMaxEnergyCountPerPixel.Location = new System.Drawing.Point(8, 66);
            this.lblMaxEnergyCountPerPixel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxEnergyCountPerPixel.Name = "lblMaxEnergyCountPerPixel";
            this.lblMaxEnergyCountPerPixel.Size = new System.Drawing.Size(220, 17);
            this.lblMaxEnergyCountPerPixel.TabIndex = 9;
            this.lblMaxEnergyCountPerPixel.Text = "Max Energy Count(Per Pixel):";
            // 
            // cbLanguage
            // 
            this.cbLanguage.AutoCompleteCustomSource.AddRange(new string[] {
            "English",
            "简体中文"});
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Items.AddRange(new object[] {
            "简体中文",
            "English"});
            this.cbLanguage.Location = new System.Drawing.Point(237, 28);
            this.cbLanguage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(133, 25);
            this.cbLanguage.TabIndex = 8;
            this.cbLanguage.SelectionChangeCommitted += new System.EventHandler(this.cbLanguage_SelectionChangeCommitted);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(8, 28);
            this.lblLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(85, 17);
            this.lblLanguage.TabIndex = 7;
            this.lblLanguage.Text = "Language:";
            // 
            // btnSaveFactoryConfig
            // 
            this.btnSaveFactoryConfig.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveFactoryConfig.FlatAppearance.BorderColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnSaveFactoryConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveFactoryConfig.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveFactoryConfig.Image = global::DemoTool.Properties.Resources.SaveWhiteBlack;
            this.btnSaveFactoryConfig.Location = new System.Drawing.Point(816, 657);
            this.btnSaveFactoryConfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveFactoryConfig.Name = "btnSaveFactoryConfig";
            this.btnSaveFactoryConfig.Size = new System.Drawing.Size(64, 53);
            this.btnSaveFactoryConfig.TabIndex = 2;
            this.btnSaveFactoryConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveFactoryConfig.UseVisualStyleBackColor = false;
            this.btnSaveFactoryConfig.Click += new System.EventHandler(this.btnSaveFactoryConfig_Click);
            // 
            // btnConfigRefresh
            // 
            this.btnConfigRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnConfigRefresh.FlatAppearance.BorderColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnConfigRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConfigRefresh.Image = global::DemoTool.Properties.Resources.Refresh256x256WhiteBlack;
            this.btnConfigRefresh.Location = new System.Drawing.Point(16, 657);
            this.btnConfigRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfigRefresh.Name = "btnConfigRefresh";
            this.btnConfigRefresh.Size = new System.Drawing.Size(64, 53);
            this.btnConfigRefresh.TabIndex = 0;
            this.btnConfigRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConfigRefresh.UseVisualStyleBackColor = false;
            // 
            // ConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(967, 742);
            this.Controls.Add(this.btnSaveFactoryConfig);
            this.Controls.Add(this.btnConfigRefresh);
            this.Controls.Add(this.grpWriteParams);
            this.Controls.Add(this.grpReadOnlyParams);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConfigEditor";
            this.Text = "ConfigEditor";
            this.grpReadOnlyParams.ResumeLayout(false);
            this.grpReadOnlyParams.PerformLayout();
            this.pnMFGName.ResumeLayout(false);
            this.pnMFGName.PerformLayout();
            this.grpWriteParams.ResumeLayout(false);
            this.grpWriteParams.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.gpConfigVbias.ResumeLayout(false);
            this.gpConfigVbias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdEnableAutoVbiasAdjust)).EndInit();
            this.grpReportCustomize.ResumeLayout(false);
            this.grpReportCustomize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeCountGreyPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeEnergyGreyPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeResolutionGreyPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdUseDifferentRanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeEnergyCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdIncludeEnergySpectrum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdEnergyResolution)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpReadOnlyParams;
        private System.Windows.Forms.GroupBox grpWriteParams;
        private System.Windows.Forms.Button btnConfigRefresh;
        private System.Windows.Forms.Label lblMFGName;
        private System.Windows.Forms.TextBox txtMFGName;
        private System.Windows.Forms.Panel pnMFGName;
        private System.Windows.Forms.Button btnSaveFactoryConfig;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMaxEnergyCountPerPixel;
        private System.Windows.Forms.Label lblMaxEnergyCountPerPixel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtBinSize;
        private System.Windows.Forms.Label lblBinSize;
        private System.Windows.Forms.GroupBox grpReportCustomize;
        private System.Windows.Forms.Label lblEnergyResolution;
        private System.Windows.Forms.PictureBox rdEnergyResolution;
        private System.Windows.Forms.Label lblIncludeEnergySpectrum;
        private System.Windows.Forms.PictureBox rdIncludeEnergySpectrum;
        private System.Windows.Forms.Label lblIncludeEnergyCount;
        private System.Windows.Forms.PictureBox rdIncludeEnergyCount;
        private System.Windows.Forms.GroupBox gpConfigVbias;
        private System.Windows.Forms.Label lblAutoAdjustVbias;
        private System.Windows.Forms.PictureBox rdEnableAutoVbiasAdjust;
        private System.Windows.Forms.ComboBox cbDefaultQualifiedType;
        private System.Windows.Forms.Label lblDefaultQualifiedType;
        private System.Windows.Forms.Label lblUseDifferentRanges;
        private System.Windows.Forms.PictureBox rdUseDifferentRanges;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtQualifiedResolutionRangeFile;
        private System.Windows.Forms.Label lblQualifiedResolutionRangeFile;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtQualifiedPeakRangeFile;
        private System.Windows.Forms.Label lblQualifiedPeakRangeFile;
        private System.Windows.Forms.ComboBox cbEnablePixelReverse;
        private System.Windows.Forms.Label lblEnablePixelReverse;
        private System.Windows.Forms.Label lblIncludeCountGreyPic;
        private System.Windows.Forms.Label lblIncludeEnergyGreyPic;
        private System.Windows.Forms.PictureBox rdIncludeCountGreyPic;
        private System.Windows.Forms.PictureBox rdIncludeEnergyGreyPic;
        private System.Windows.Forms.Label lblIncludeResolutionGreyPic;
        private System.Windows.Forms.PictureBox rdIncludeResolutionGreyPic;
    }
}