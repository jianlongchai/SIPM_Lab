using System.Windows.Forms;

namespace DemoTool {

    partial class Demo {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if ( disposing && ( components != null ) ) {
                components.Dispose ( );
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( ) {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Demo));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.factoryDefaultConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanProtocol扫描协议ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMainControl = new System.Windows.Forms.TabControl();
            this.tbpgMainControl = new System.Windows.Forms.TabPage();
            this.grpReportNote = new System.Windows.Forms.GroupBox();
            this.lblArrayNo = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtArrayNo = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.btnMergeReport = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.picProgress = new System.Windows.Forms.PictureBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.grpTempStatus = new System.Windows.Forms.GroupBox();
            this.lblAverage = new System.Windows.Forms.Label();
            this.lbl4thSensor = new System.Windows.Forms.Label();
            this.lbl3rdSensor = new System.Windows.Forms.Label();
            this.lbl2ndSensor = new System.Windows.Forms.Label();
            this.vprgBar4thSensor = new VerticalProgressBar.VerticalProgressBar();
            this.vprgBar3rdSensor = new VerticalProgressBar.VerticalProgressBar();
            this.vprgBarAveTemp = new VerticalProgressBar.VerticalProgressBar();
            this.vprgBar2rdSensor = new VerticalProgressBar.VerticalProgressBar();
            this.lbl1stSensor = new System.Windows.Forms.Label();
            this.vprgBar1stSensor = new VerticalProgressBar.VerticalProgressBar();
            this.txtPixelAndCount = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDontTouch = new System.Windows.Forms.Button();
            this.btnBoxStatus = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.cmModule = new System.Windows.Forms.ComboBox();
            this.grpboxAcqOption = new System.Windows.Forms.GroupBox();
            this.lblQuilifiedType = new System.Windows.Forms.Label();
            this.cbQualifiedType = new System.Windows.Forms.ComboBox();
            this.lblLightSharePixelCount = new System.Windows.Forms.Label();
            this.panelLighSharePixelCount = new System.Windows.Forms.Panel();
            this.txtLightSharePixelCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtFittingUpBand = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtFittingLowBand = new System.Windows.Forms.TextBox();
            this.lblRangeTo = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtPeakHighLimit = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPeakLowLimit = new System.Windows.Forms.TextBox();
            this.lblPeakValue = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtPRPATriggerValue = new System.Windows.Forms.TextBox();
            this.lblPRPATriggerValue = new System.Windows.Forms.Label();
            this.lblPRPATriggerType = new System.Windows.Forms.Label();
            this.cbboxPRPATriggerType = new System.Windows.Forms.ComboBox();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.cbboxPRPRProtocolList = new System.Windows.Forms.ComboBox();
            this.picBarDisplayEnergy = new System.Windows.Forms.PictureBox();
            this.lblDisplayEnergyLine = new System.Windows.Forms.Label();
            this.grpDataAnalysis = new System.Windows.Forms.GroupBox();
            this.picLightDivideDisplay = new System.Windows.Forms.PictureBox();
            this.pcBoxPixel = new System.Windows.Forms.PictureBox();
            this.btnStartDataCollection = new System.Windows.Forms.Button();
            this.grpboxEventData = new System.Windows.Forms.GroupBox();
            this.lblTimePeriod = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTimePeriod = new System.Windows.Forms.TextBox();
            this.lblEventCount = new System.Windows.Forms.Label();
            this.pnEventCount = new System.Windows.Forms.Panel();
            this.txtEventCount = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbDataInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCmdType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ttStatusTooltops = new System.Windows.Forms.ToolTip(this.components);
            this.lblLinkLable = new System.Windows.Forms.LinkLabel();
            this.backgroundWorkerFittingProgress = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.tbMainControl.SuspendLayout();
            this.tbpgMainControl.SuspendLayout();
            this.grpReportNote.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).BeginInit();
            this.grpTempStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpboxAcqOption.SuspendLayout();
            this.panelLighSharePixelCount.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarDisplayEnergy)).BeginInit();
            this.grpDataAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLightDivideDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBoxPixel)).BeginInit();
            this.grpboxEventData.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnEventCount.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LemonChiffon;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(909, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.factoryDefaultConfigToolStripMenuItem,
            this.scanProtocol扫描协议ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.fileToolStripMenuItem.Text = "File 文件";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.closeToolStripMenuItem.Text = "Close 关闭";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // factoryDefaultConfigToolStripMenuItem
            // 
            this.factoryDefaultConfigToolStripMenuItem.Name = "factoryDefaultConfigToolStripMenuItem";
            this.factoryDefaultConfigToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.factoryDefaultConfigToolStripMenuItem.Text = "Factory Default Setting 配置";
            this.factoryDefaultConfigToolStripMenuItem.Click += new System.EventHandler(this.factoryDefaultConfigToolStripMenuItem_Click);
            // 
            // scanProtocol扫描协议ToolStripMenuItem
            // 
            this.scanProtocol扫描协议ToolStripMenuItem.Name = "scanProtocol扫描协议ToolStripMenuItem";
            this.scanProtocol扫描协议ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.scanProtocol扫描协议ToolStripMenuItem.Text = "Scan Protocol 扫描协议";
            this.scanProtocol扫描协议ToolStripMenuItem.Click += new System.EventHandler(this.scanProtocolToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.updateToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.helpToolStripMenuItem.Text = "Help 帮助";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.aboutToolStripMenuItem.Text = "About 关于";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.updateToolStripMenuItem.Text = "Update 升级";
            // 
            // tbMainControl
            // 
            this.tbMainControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMainControl.Controls.Add(this.tbpgMainControl);
            this.tbMainControl.Controls.Add(this.tabPage2);
            this.tbMainControl.Location = new System.Drawing.Point(0, 25);
            this.tbMainControl.Margin = new System.Windows.Forms.Padding(2);
            this.tbMainControl.Name = "tbMainControl";
            this.tbMainControl.SelectedIndex = 0;
            this.tbMainControl.Size = new System.Drawing.Size(900, 551);
            this.tbMainControl.TabIndex = 2;
            // 
            // tbpgMainControl
            // 
            this.tbpgMainControl.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbpgMainControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbpgMainControl.Controls.Add(this.grpReportNote);
            this.tbpgMainControl.Controls.Add(this.btnMergeReport);
            this.tbpgMainControl.Controls.Add(this.lblLoading);
            this.tbpgMainControl.Controls.Add(this.picProgress);
            this.tbpgMainControl.Controls.Add(this.btnGenerateReport);
            this.tbpgMainControl.Controls.Add(this.grpTempStatus);
            this.tbpgMainControl.Controls.Add(this.txtPixelAndCount);
            this.tbpgMainControl.Controls.Add(this.pictureBox1);
            this.tbpgMainControl.Controls.Add(this.btnDontTouch);
            this.tbpgMainControl.Controls.Add(this.btnBoxStatus);
            this.tbpgMainControl.Controls.Add(this.btnExport);
            this.tbpgMainControl.Controls.Add(this.cmModule);
            this.tbpgMainControl.Controls.Add(this.grpboxAcqOption);
            this.tbpgMainControl.Controls.Add(this.grpDataAnalysis);
            this.tbpgMainControl.Controls.Add(this.btnStartDataCollection);
            this.tbpgMainControl.Controls.Add(this.grpboxEventData);
            this.tbpgMainControl.ForeColor = System.Drawing.Color.LemonChiffon;
            this.tbpgMainControl.Location = new System.Drawing.Point(4, 22);
            this.tbpgMainControl.Margin = new System.Windows.Forms.Padding(2);
            this.tbpgMainControl.Name = "tbpgMainControl";
            this.tbpgMainControl.Padding = new System.Windows.Forms.Padding(2);
            this.tbpgMainControl.Size = new System.Drawing.Size(892, 525);
            this.tbpgMainControl.TabIndex = 0;
            this.tbpgMainControl.Text = "MainControl";
            // 
            // grpReportNote
            // 
            this.grpReportNote.Controls.Add(this.lblArrayNo);
            this.grpReportNote.Controls.Add(this.lblOrderNo);
            this.grpReportNote.Controls.Add(this.panel8);
            this.grpReportNote.Controls.Add(this.panel7);
            this.grpReportNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpReportNote.Location = new System.Drawing.Point(736, 362);
            this.grpReportNote.Margin = new System.Windows.Forms.Padding(2);
            this.grpReportNote.Name = "grpReportNote";
            this.grpReportNote.Padding = new System.Windows.Forms.Padding(2);
            this.grpReportNote.Size = new System.Drawing.Size(150, 84);
            this.grpReportNote.TabIndex = 21;
            this.grpReportNote.TabStop = false;
            this.grpReportNote.Text = "Report Notes";
            // 
            // lblArrayNo
            // 
            this.lblArrayNo.AutoSize = true;
            this.lblArrayNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArrayNo.ForeColor = System.Drawing.Color.Black;
            this.lblArrayNo.Location = new System.Drawing.Point(7, 59);
            this.lblArrayNo.Name = "lblArrayNo";
            this.lblArrayNo.Size = new System.Drawing.Size(44, 13);
            this.lblArrayNo.TabIndex = 84;
            this.lblArrayNo.Text = "Array#";
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNo.ForeColor = System.Drawing.Color.Black;
            this.lblOrderNo.Location = new System.Drawing.Point(7, 28);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(46, 13);
            this.lblOrderNo.TabIndex = 83;
            this.lblOrderNo.Text = "Order#";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Black;
            this.panel8.Controls.Add(this.txtArrayNo);
            this.panel8.Location = new System.Drawing.Point(57, 54);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(88, 21);
            this.panel8.TabIndex = 81;
            // 
            // txtArrayNo
            // 
            this.txtArrayNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtArrayNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArrayNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArrayNo.Location = new System.Drawing.Point(0, 0);
            this.txtArrayNo.Name = "txtArrayNo";
            this.txtArrayNo.Size = new System.Drawing.Size(88, 20);
            this.txtArrayNo.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Black;
            this.panel7.Controls.Add(this.txtOrderNo);
            this.panel7.Location = new System.Drawing.Point(57, 24);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(88, 21);
            this.panel7.TabIndex = 80;
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNo.Location = new System.Drawing.Point(0, 0);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(88, 20);
            this.txtOrderNo.TabIndex = 4;
            // 
            // btnMergeReport
            // 
            this.btnMergeReport.BackColor = System.Drawing.Color.Transparent;
            this.btnMergeReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnMergeReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMergeReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMergeReport.ForeColor = System.Drawing.Color.Black;
            this.btnMergeReport.Image = ((System.Drawing.Image)(resources.GetObject("btnMergeReport.Image")));
            this.btnMergeReport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMergeReport.Location = new System.Drawing.Point(736, 135);
            this.btnMergeReport.Name = "btnMergeReport";
            this.btnMergeReport.Size = new System.Drawing.Size(113, 92);
            this.btnMergeReport.TabIndex = 20;
            this.btnMergeReport.Text = "Merge Reports";
            this.btnMergeReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMergeReport.UseVisualStyleBackColor = false;
            this.btnMergeReport.Click += new System.EventHandler(this.btnMergeReport_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.ForeColor = System.Drawing.Color.Black;
            this.lblLoading.Location = new System.Drawing.Point(101, 459);
            this.lblLoading.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(57, 13);
            this.lblLoading.TabIndex = 19;
            this.lblLoading.Text = "Loading....";
            // 
            // picProgress
            // 
            this.picProgress.Location = new System.Drawing.Point(22, 431);
            this.picProgress.Margin = new System.Windows.Forms.Padding(2);
            this.picProgress.Name = "picProgress";
            this.picProgress.Size = new System.Drawing.Size(73, 65);
            this.picProgress.TabIndex = 18;
            this.picProgress.TabStop = false;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerateReport.FlatAppearance.BorderColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateReport.ForeColor = System.Drawing.Color.Black;
            this.btnGenerateReport.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateReport.Image")));
            this.btnGenerateReport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGenerateReport.Location = new System.Drawing.Point(736, 240);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(93, 94);
            this.btnGenerateReport.TabIndex = 17;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGenerateReport.UseVisualStyleBackColor = false;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // grpTempStatus
            // 
            this.grpTempStatus.BackColor = System.Drawing.Color.Transparent;
            this.grpTempStatus.Controls.Add(this.lblAverage);
            this.grpTempStatus.Controls.Add(this.lbl4thSensor);
            this.grpTempStatus.Controls.Add(this.lbl3rdSensor);
            this.grpTempStatus.Controls.Add(this.lbl2ndSensor);
            this.grpTempStatus.Controls.Add(this.vprgBar4thSensor);
            this.grpTempStatus.Controls.Add(this.vprgBar3rdSensor);
            this.grpTempStatus.Controls.Add(this.vprgBarAveTemp);
            this.grpTempStatus.Controls.Add(this.vprgBar2rdSensor);
            this.grpTempStatus.Controls.Add(this.lbl1stSensor);
            this.grpTempStatus.Controls.Add(this.vprgBar1stSensor);
            this.grpTempStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTempStatus.Location = new System.Drawing.Point(136, 12);
            this.grpTempStatus.Margin = new System.Windows.Forms.Padding(2);
            this.grpTempStatus.Name = "grpTempStatus";
            this.grpTempStatus.Padding = new System.Windows.Forms.Padding(2);
            this.grpTempStatus.Size = new System.Drawing.Size(134, 135);
            this.grpTempStatus.TabIndex = 9;
            this.grpTempStatus.TabStop = false;
            this.grpTempStatus.Text = "Temperature";
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverage.ForeColor = System.Drawing.Color.Black;
            this.lblAverage.Location = new System.Drawing.Point(16, 108);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(30, 13);
            this.lblAverage.TabIndex = 23;
            this.lblAverage.Text = "N/A";
            // 
            // lbl4thSensor
            // 
            this.lbl4thSensor.AutoSize = true;
            this.lbl4thSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl4thSensor.ForeColor = System.Drawing.Color.Black;
            this.lbl4thSensor.Location = new System.Drawing.Point(124, 108);
            this.lbl4thSensor.Name = "lbl4thSensor";
            this.lbl4thSensor.Size = new System.Drawing.Size(25, 13);
            this.lbl4thSensor.TabIndex = 17;
            this.lbl4thSensor.Text = "4th";
            // 
            // lbl3rdSensor
            // 
            this.lbl3rdSensor.AutoSize = true;
            this.lbl3rdSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3rdSensor.ForeColor = System.Drawing.Color.Black;
            this.lbl3rdSensor.Location = new System.Drawing.Point(99, 108);
            this.lbl3rdSensor.Name = "lbl3rdSensor";
            this.lbl3rdSensor.Size = new System.Drawing.Size(25, 13);
            this.lbl3rdSensor.TabIndex = 17;
            this.lbl3rdSensor.Text = "3rd";
            // 
            // lbl2ndSensor
            // 
            this.lbl2ndSensor.AutoSize = true;
            this.lbl2ndSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2ndSensor.ForeColor = System.Drawing.Color.Black;
            this.lbl2ndSensor.Location = new System.Drawing.Point(71, 108);
            this.lbl2ndSensor.Name = "lbl2ndSensor";
            this.lbl2ndSensor.Size = new System.Drawing.Size(28, 13);
            this.lbl2ndSensor.TabIndex = 22;
            this.lbl2ndSensor.Text = "2nd";
            // 
            // vprgBar4thSensor
            // 
            this.vprgBar4thSensor.BackColor = System.Drawing.Color.Transparent;
            this.vprgBar4thSensor.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.vprgBar4thSensor.Color = System.Drawing.Color.SpringGreen;
            this.vprgBar4thSensor.Location = new System.Drawing.Point(126, 23);
            this.vprgBar4thSensor.Margin = new System.Windows.Forms.Padding(2);
            this.vprgBar4thSensor.Maximum = 100;
            this.vprgBar4thSensor.Minimum = 0;
            this.vprgBar4thSensor.Name = "vprgBar4thSensor";
            this.vprgBar4thSensor.Size = new System.Drawing.Size(13, 83);
            this.vprgBar4thSensor.Step = 10;
            this.vprgBar4thSensor.Style = VerticalProgressBar.Styles.Classic;
            this.vprgBar4thSensor.TabIndex = 20;
            this.vprgBar4thSensor.Value = 50;
            // 
            // vprgBar3rdSensor
            // 
            this.vprgBar3rdSensor.BackColor = System.Drawing.Color.Transparent;
            this.vprgBar3rdSensor.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.vprgBar3rdSensor.Color = System.Drawing.Color.SpringGreen;
            this.vprgBar3rdSensor.Location = new System.Drawing.Point(101, 23);
            this.vprgBar3rdSensor.Margin = new System.Windows.Forms.Padding(2);
            this.vprgBar3rdSensor.Maximum = 100;
            this.vprgBar3rdSensor.Minimum = 0;
            this.vprgBar3rdSensor.Name = "vprgBar3rdSensor";
            this.vprgBar3rdSensor.Size = new System.Drawing.Size(13, 83);
            this.vprgBar3rdSensor.Step = 10;
            this.vprgBar3rdSensor.Style = VerticalProgressBar.Styles.Classic;
            this.vprgBar3rdSensor.TabIndex = 19;
            this.vprgBar3rdSensor.Value = 50;
            // 
            // vprgBarAveTemp
            // 
            this.vprgBarAveTemp.BackColor = System.Drawing.Color.Transparent;
            this.vprgBarAveTemp.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.vprgBarAveTemp.Color = System.Drawing.Color.SpringGreen;
            this.vprgBarAveTemp.Location = new System.Drawing.Point(18, 23);
            this.vprgBarAveTemp.Margin = new System.Windows.Forms.Padding(2);
            this.vprgBarAveTemp.Maximum = 100;
            this.vprgBarAveTemp.Minimum = 0;
            this.vprgBarAveTemp.Name = "vprgBarAveTemp";
            this.vprgBarAveTemp.Size = new System.Drawing.Size(13, 83);
            this.vprgBarAveTemp.Step = 10;
            this.vprgBarAveTemp.Style = VerticalProgressBar.Styles.Classic;
            this.vprgBarAveTemp.TabIndex = 21;
            this.vprgBarAveTemp.Value = 50;
            // 
            // vprgBar2rdSensor
            // 
            this.vprgBar2rdSensor.BackColor = System.Drawing.Color.Transparent;
            this.vprgBar2rdSensor.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.vprgBar2rdSensor.Color = System.Drawing.Color.SpringGreen;
            this.vprgBar2rdSensor.Location = new System.Drawing.Point(74, 23);
            this.vprgBar2rdSensor.Margin = new System.Windows.Forms.Padding(2);
            this.vprgBar2rdSensor.Maximum = 100;
            this.vprgBar2rdSensor.Minimum = 0;
            this.vprgBar2rdSensor.Name = "vprgBar2rdSensor";
            this.vprgBar2rdSensor.Size = new System.Drawing.Size(13, 83);
            this.vprgBar2rdSensor.Step = 10;
            this.vprgBar2rdSensor.Style = VerticalProgressBar.Styles.Classic;
            this.vprgBar2rdSensor.TabIndex = 18;
            this.vprgBar2rdSensor.Value = 50;
            // 
            // lbl1stSensor
            // 
            this.lbl1stSensor.AutoSize = true;
            this.lbl1stSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1stSensor.ForeColor = System.Drawing.Color.Black;
            this.lbl1stSensor.Location = new System.Drawing.Point(45, 108);
            this.lbl1stSensor.Name = "lbl1stSensor";
            this.lbl1stSensor.Size = new System.Drawing.Size(24, 13);
            this.lbl1stSensor.TabIndex = 9;
            this.lbl1stSensor.Text = "1st";
            // 
            // vprgBar1stSensor
            // 
            this.vprgBar1stSensor.BackColor = System.Drawing.Color.Transparent;
            this.vprgBar1stSensor.BorderStyle = VerticalProgressBar.BorderStyles.Classic;
            this.vprgBar1stSensor.Color = System.Drawing.Color.SpringGreen;
            this.vprgBar1stSensor.Location = new System.Drawing.Point(47, 23);
            this.vprgBar1stSensor.Margin = new System.Windows.Forms.Padding(2);
            this.vprgBar1stSensor.Maximum = 100;
            this.vprgBar1stSensor.Minimum = 0;
            this.vprgBar1stSensor.Name = "vprgBar1stSensor";
            this.vprgBar1stSensor.Size = new System.Drawing.Size(13, 83);
            this.vprgBar1stSensor.Step = 10;
            this.vprgBar1stSensor.Style = VerticalProgressBar.Styles.Classic;
            this.vprgBar1stSensor.TabIndex = 17;
            this.vprgBar1stSensor.Value = 50;
            // 
            // txtPixelAndCount
            // 
            this.txtPixelAndCount.Location = new System.Drawing.Point(338, 477);
            this.txtPixelAndCount.Margin = new System.Windows.Forms.Padding(2);
            this.txtPixelAndCount.MaximumSize = new System.Drawing.Size(751, 1000);
            this.txtPixelAndCount.MinimumSize = new System.Drawing.Size(5, 24);
            this.txtPixelAndCount.Name = "txtPixelAndCount";
            this.txtPixelAndCount.ShortcutsEnabled = false;
            this.txtPixelAndCount.Size = new System.Drawing.Size(387, 20);
            this.txtPixelAndCount.TabIndex = 15;
            this.ttStatusTooltops.SetToolTip(this.txtPixelAndCount, "PixelNo, EnergyCount, EnergyResolution, EngergyPeak");
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(729, 457);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // btnDontTouch
            // 
            this.btnDontTouch.BackColor = System.Drawing.Color.Transparent;
            this.btnDontTouch.FlatAppearance.BorderColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnDontTouch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDontTouch.Image = ((System.Drawing.Image)(resources.GetObject("btnDontTouch.Image")));
            this.btnDontTouch.Location = new System.Drawing.Point(279, 327);
            this.btnDontTouch.Margin = new System.Windows.Forms.Padding(2);
            this.btnDontTouch.Name = "btnDontTouch";
            this.btnDontTouch.Size = new System.Drawing.Size(51, 48);
            this.btnDontTouch.TabIndex = 11;
            this.btnDontTouch.UseVisualStyleBackColor = false;
            // 
            // btnBoxStatus
            // 
            this.btnBoxStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnBoxStatus.FlatAppearance.BorderColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnBoxStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBoxStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnBoxStatus.Image")));
            this.btnBoxStatus.Location = new System.Drawing.Point(279, 386);
            this.btnBoxStatus.Margin = new System.Windows.Forms.Padding(2);
            this.btnBoxStatus.Name = "btnBoxStatus";
            this.btnBoxStatus.Size = new System.Drawing.Size(51, 52);
            this.btnBoxStatus.TabIndex = 10;
            this.btnBoxStatus.UseVisualStyleBackColor = false;
            this.btnBoxStatus.Click += new System.EventHandler(this.btnBoxStatus_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExport.Location = new System.Drawing.Point(736, 29);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(104, 93);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Export Raw Data";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cmModule
            // 
            this.cmModule.Enabled = false;
            this.cmModule.FormattingEnabled = true;
            this.cmModule.Items.AddRange(new object[] {
            "1",
            "64",
            "256"});
            this.cmModule.Location = new System.Drawing.Point(338, 477);
            this.cmModule.Margin = new System.Windows.Forms.Padding(2);
            this.cmModule.Name = "cmModule";
            this.cmModule.Size = new System.Drawing.Size(92, 21);
            this.cmModule.TabIndex = 3;
            this.cmModule.Visible = false;
            this.cmModule.SelectedIndexChanged += new System.EventHandler(this.cmModule_SelectedIndexChanged);
            // 
            // grpboxAcqOption
            // 
            this.grpboxAcqOption.BackColor = System.Drawing.Color.Transparent;
            this.grpboxAcqOption.Controls.Add(this.lblQuilifiedType);
            this.grpboxAcqOption.Controls.Add(this.cbQualifiedType);
            this.grpboxAcqOption.Controls.Add(this.lblLightSharePixelCount);
            this.grpboxAcqOption.Controls.Add(this.panelLighSharePixelCount);
            this.grpboxAcqOption.Controls.Add(this.label4);
            this.grpboxAcqOption.Controls.Add(this.panel5);
            this.grpboxAcqOption.Controls.Add(this.panel6);
            this.grpboxAcqOption.Controls.Add(this.lblRangeTo);
            this.grpboxAcqOption.Controls.Add(this.panel4);
            this.grpboxAcqOption.Controls.Add(this.panel1);
            this.grpboxAcqOption.Controls.Add(this.lblPeakValue);
            this.grpboxAcqOption.Controls.Add(this.panel3);
            this.grpboxAcqOption.Controls.Add(this.lblPRPATriggerValue);
            this.grpboxAcqOption.Controls.Add(this.lblPRPATriggerType);
            this.grpboxAcqOption.Controls.Add(this.cbboxPRPATriggerType);
            this.grpboxAcqOption.Controls.Add(this.lblProtocol);
            this.grpboxAcqOption.Controls.Add(this.cbboxPRPRProtocolList);
            this.grpboxAcqOption.Controls.Add(this.picBarDisplayEnergy);
            this.grpboxAcqOption.Controls.Add(this.lblDisplayEnergyLine);
            this.grpboxAcqOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpboxAcqOption.Location = new System.Drawing.Point(6, 150);
            this.grpboxAcqOption.Margin = new System.Windows.Forms.Padding(2);
            this.grpboxAcqOption.Name = "grpboxAcqOption";
            this.grpboxAcqOption.Padding = new System.Windows.Forms.Padding(2);
            this.grpboxAcqOption.Size = new System.Drawing.Size(264, 267);
            this.grpboxAcqOption.TabIndex = 2;
            this.grpboxAcqOption.TabStop = false;
            this.grpboxAcqOption.Text = "Acquisition Options";
            // 
            // lblQuilifiedType
            // 
            this.lblQuilifiedType.AutoSize = true;
            this.lblQuilifiedType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuilifiedType.ForeColor = System.Drawing.Color.Black;
            this.lblQuilifiedType.Location = new System.Drawing.Point(8, 122);
            this.lblQuilifiedType.Name = "lblQuilifiedType";
            this.lblQuilifiedType.Size = new System.Drawing.Size(89, 13);
            this.lblQuilifiedType.TabIndex = 82;
            this.lblQuilifiedType.Text = "Qualified Type";
            // 
            // cbQualifiedType
            // 
            this.cbQualifiedType.AutoCompleteCustomSource.AddRange(new string[] {
            "English",
            "简体中文"});
            this.cbQualifiedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQualifiedType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbQualifiedType.FormattingEnabled = true;
            this.cbQualifiedType.Items.AddRange(new object[] {
            "EnergyPeak",
            "EnergyResolution"});
            this.cbQualifiedType.Location = new System.Drawing.Point(10, 138);
            this.cbQualifiedType.Margin = new System.Windows.Forms.Padding(2);
            this.cbQualifiedType.Name = "cbQualifiedType";
            this.cbQualifiedType.Size = new System.Drawing.Size(101, 21);
            this.cbQualifiedType.TabIndex = 0;
            this.cbQualifiedType.SelectedIndexChanged += new System.EventHandler(this.cbQualifiedType_SelectedIndexChanged);
            // 
            // lblLightSharePixelCount
            // 
            this.lblLightSharePixelCount.AutoSize = true;
            this.lblLightSharePixelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLightSharePixelCount.ForeColor = System.Drawing.Color.Black;
            this.lblLightSharePixelCount.Location = new System.Drawing.Point(146, 212);
            this.lblLightSharePixelCount.Name = "lblLightSharePixelCount";
            this.lblLightSharePixelCount.Size = new System.Drawing.Size(71, 13);
            this.lblLightSharePixelCount.TabIndex = 81;
            this.lblLightSharePixelCount.Text = "Pixel Count";
            // 
            // panelLighSharePixelCount
            // 
            this.panelLighSharePixelCount.BackColor = System.Drawing.Color.Black;
            this.panelLighSharePixelCount.Controls.Add(this.txtLightSharePixelCount);
            this.panelLighSharePixelCount.Location = new System.Drawing.Point(144, 230);
            this.panelLighSharePixelCount.Name = "panelLighSharePixelCount";
            this.panelLighSharePixelCount.Size = new System.Drawing.Size(100, 21);
            this.panelLighSharePixelCount.TabIndex = 76;
            // 
            // txtLightSharePixelCount
            // 
            this.txtLightSharePixelCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtLightSharePixelCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLightSharePixelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLightSharePixelCount.Location = new System.Drawing.Point(0, 0);
            this.txtLightSharePixelCount.Name = "txtLightSharePixelCount";
            this.txtLightSharePixelCount.Size = new System.Drawing.Size(100, 20);
            this.txtLightSharePixelCount.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(114, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 80;
            this.label4.Text = "To";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Controls.Add(this.txtFittingUpBand);
            this.panel5.Location = new System.Drawing.Point(144, 40);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(100, 21);
            this.panel5.TabIndex = 79;
            // 
            // txtFittingUpBand
            // 
            this.txtFittingUpBand.BackColor = System.Drawing.SystemColors.Window;
            this.txtFittingUpBand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFittingUpBand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFittingUpBand.Location = new System.Drawing.Point(0, 0);
            this.txtFittingUpBand.Name = "txtFittingUpBand";
            this.txtFittingUpBand.Size = new System.Drawing.Size(100, 20);
            this.txtFittingUpBand.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Black;
            this.panel6.Controls.Add(this.txtFittingLowBand);
            this.panel6.Location = new System.Drawing.Point(10, 41);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(100, 21);
            this.panel6.TabIndex = 78;
            // 
            // txtFittingLowBand
            // 
            this.txtFittingLowBand.BackColor = System.Drawing.SystemColors.Window;
            this.txtFittingLowBand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFittingLowBand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFittingLowBand.Location = new System.Drawing.Point(0, 0);
            this.txtFittingLowBand.Name = "txtFittingLowBand";
            this.txtFittingLowBand.Size = new System.Drawing.Size(100, 20);
            this.txtFittingLowBand.TabIndex = 4;
            // 
            // lblRangeTo
            // 
            this.lblRangeTo.AutoSize = true;
            this.lblRangeTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRangeTo.ForeColor = System.Drawing.Color.Black;
            this.lblRangeTo.Location = new System.Drawing.Point(116, 186);
            this.lblRangeTo.Name = "lblRangeTo";
            this.lblRangeTo.Size = new System.Drawing.Size(22, 13);
            this.lblRangeTo.TabIndex = 76;
            this.lblRangeTo.Text = "To";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Controls.Add(this.txtPeakHighLimit);
            this.panel4.Location = new System.Drawing.Point(146, 184);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 21);
            this.panel4.TabIndex = 75;
            // 
            // txtPeakHighLimit
            // 
            this.txtPeakHighLimit.BackColor = System.Drawing.SystemColors.Window;
            this.txtPeakHighLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPeakHighLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPeakHighLimit.Location = new System.Drawing.Point(0, 0);
            this.txtPeakHighLimit.Name = "txtPeakHighLimit";
            this.txtPeakHighLimit.Size = new System.Drawing.Size(100, 20);
            this.txtPeakHighLimit.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.txtPeakLowLimit);
            this.panel1.Location = new System.Drawing.Point(11, 185);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 21);
            this.panel1.TabIndex = 74;
            // 
            // txtPeakLowLimit
            // 
            this.txtPeakLowLimit.BackColor = System.Drawing.SystemColors.Window;
            this.txtPeakLowLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPeakLowLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPeakLowLimit.Location = new System.Drawing.Point(0, 0);
            this.txtPeakLowLimit.Name = "txtPeakLowLimit";
            this.txtPeakLowLimit.Size = new System.Drawing.Size(100, 20);
            this.txtPeakLowLimit.TabIndex = 4;
            // 
            // lblPeakValue
            // 
            this.lblPeakValue.AutoSize = true;
            this.lblPeakValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeakValue.ForeColor = System.Drawing.Color.Black;
            this.lblPeakValue.Location = new System.Drawing.Point(8, 168);
            this.lblPeakValue.Name = "lblPeakValue";
            this.lblPeakValue.Size = new System.Drawing.Size(98, 13);
            this.lblPeakValue.TabIndex = 73;
            this.lblPeakValue.Text = "Qualified Range";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.txtPRPATriggerValue);
            this.panel3.Location = new System.Drawing.Point(146, 89);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(101, 21);
            this.panel3.TabIndex = 72;
            // 
            // txtPRPATriggerValue
            // 
            this.txtPRPATriggerValue.BackColor = System.Drawing.SystemColors.Window;
            this.txtPRPATriggerValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPRPATriggerValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRPATriggerValue.Location = new System.Drawing.Point(0, 1);
            this.txtPRPATriggerValue.Name = "txtPRPATriggerValue";
            this.txtPRPATriggerValue.Size = new System.Drawing.Size(101, 19);
            this.txtPRPATriggerValue.TabIndex = 4;
            // 
            // lblPRPATriggerValue
            // 
            this.lblPRPATriggerValue.AutoSize = true;
            this.lblPRPATriggerValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRPATriggerValue.ForeColor = System.Drawing.Color.Black;
            this.lblPRPATriggerValue.Location = new System.Drawing.Point(142, 72);
            this.lblPRPATriggerValue.Name = "lblPRPATriggerValue";
            this.lblPRPATriggerValue.Size = new System.Drawing.Size(83, 13);
            this.lblPRPATriggerValue.TabIndex = 71;
            this.lblPRPATriggerValue.Text = "Trigger Value";
            // 
            // lblPRPATriggerType
            // 
            this.lblPRPATriggerType.AutoSize = true;
            this.lblPRPATriggerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRPATriggerType.ForeColor = System.Drawing.Color.Black;
            this.lblPRPATriggerType.Location = new System.Drawing.Point(8, 72);
            this.lblPRPATriggerType.Name = "lblPRPATriggerType";
            this.lblPRPATriggerType.Size = new System.Drawing.Size(79, 13);
            this.lblPRPATriggerType.TabIndex = 69;
            this.lblPRPATriggerType.Text = "Trigger Type";
            // 
            // cbboxPRPATriggerType
            // 
            this.cbboxPRPATriggerType.AutoCompleteCustomSource.AddRange(new string[] {
            "English",
            "简体中文"});
            this.cbboxPRPATriggerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxPRPATriggerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbboxPRPATriggerType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbboxPRPATriggerType.FormattingEnabled = true;
            this.cbboxPRPATriggerType.Items.AddRange(new object[] {
            "TimeTrigger",
            "TotalEventCountTrigger",
            "SinglePixelEventCountTrigger"});
            this.cbboxPRPATriggerType.Location = new System.Drawing.Point(10, 90);
            this.cbboxPRPATriggerType.Margin = new System.Windows.Forms.Padding(2);
            this.cbboxPRPATriggerType.Name = "cbboxPRPATriggerType";
            this.cbboxPRPATriggerType.Size = new System.Drawing.Size(101, 21);
            this.cbboxPRPATriggerType.TabIndex = 70;
            // 
            // lblProtocol
            // 
            this.lblProtocol.AutoSize = true;
            this.lblProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProtocol.ForeColor = System.Drawing.Color.Black;
            this.lblProtocol.Location = new System.Drawing.Point(8, 212);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(54, 13);
            this.lblProtocol.TabIndex = 13;
            this.lblProtocol.Text = "Prorocol";
            // 
            // cbboxPRPRProtocolList
            // 
            this.cbboxPRPRProtocolList.AutoCompleteCustomSource.AddRange(new string[] {
            "English",
            "简体中文"});
            this.cbboxPRPRProtocolList.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbboxPRPRProtocolList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxPRPRProtocolList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbboxPRPRProtocolList.FormattingEnabled = true;
            this.cbboxPRPRProtocolList.Location = new System.Drawing.Point(10, 230);
            this.cbboxPRPRProtocolList.Margin = new System.Windows.Forms.Padding(2);
            this.cbboxPRPRProtocolList.Name = "cbboxPRPRProtocolList";
            this.cbboxPRPRProtocolList.Size = new System.Drawing.Size(100, 21);
            this.cbboxPRPRProtocolList.TabIndex = 12;
            this.cbboxPRPRProtocolList.SelectedIndexChanged += new System.EventHandler(this.cbboxPRPRProtocolList_SelectedIndexChanged);
            this.cbboxPRPRProtocolList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbboxPRPRProtocolList_MouseClick);
            // 
            // picBarDisplayEnergy
            // 
            this.picBarDisplayEnergy.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.picBarDisplayEnergy.Location = new System.Drawing.Point(13, 22);
            this.picBarDisplayEnergy.Name = "picBarDisplayEnergy";
            this.picBarDisplayEnergy.Size = new System.Drawing.Size(16, 16);
            this.picBarDisplayEnergy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBarDisplayEnergy.TabIndex = 68;
            this.picBarDisplayEnergy.TabStop = false;
            this.picBarDisplayEnergy.Tag = "on";
            this.picBarDisplayEnergy.Click += new System.EventHandler(this.picBarDisplayEnergy_Click);
            // 
            // lblDisplayEnergyLine
            // 
            this.lblDisplayEnergyLine.AutoSize = true;
            this.lblDisplayEnergyLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayEnergyLine.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblDisplayEnergyLine.Location = new System.Drawing.Point(32, 22);
            this.lblDisplayEnergyLine.Name = "lblDisplayEnergyLine";
            this.lblDisplayEnergyLine.Size = new System.Drawing.Size(72, 13);
            this.lblDisplayEnergyLine.TabIndex = 7;
            this.lblDisplayEnergyLine.Text = "Auto Fitting";
            // 
            // grpDataAnalysis
            // 
            this.grpDataAnalysis.BackColor = System.Drawing.Color.Transparent;
            this.grpDataAnalysis.Controls.Add(this.picLightDivideDisplay);
            this.grpDataAnalysis.Controls.Add(this.pcBoxPixel);
            this.grpDataAnalysis.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDataAnalysis.Location = new System.Drawing.Point(338, 21);
            this.grpDataAnalysis.Margin = new System.Windows.Forms.Padding(2);
            this.grpDataAnalysis.Name = "grpDataAnalysis";
            this.grpDataAnalysis.Padding = new System.Windows.Forms.Padding(2);
            this.grpDataAnalysis.Size = new System.Drawing.Size(387, 453);
            this.grpDataAnalysis.TabIndex = 1;
            this.grpDataAnalysis.TabStop = false;
            this.grpDataAnalysis.Text = "Data Analysis";
            // 
            // picLightDivideDisplay
            // 
            this.picLightDivideDisplay.BackColor = System.Drawing.Color.Black;
            this.picLightDivideDisplay.Location = new System.Drawing.Point(0, 31);
            this.picLightDivideDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.picLightDivideDisplay.Name = "picLightDivideDisplay";
            this.picLightDivideDisplay.Size = new System.Drawing.Size(384, 416);
            this.picLightDivideDisplay.TabIndex = 1;
            this.picLightDivideDisplay.TabStop = false;
            this.picLightDivideDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picLightDivideDisplay_MouseDown);
            // 
            // pcBoxPixel
            // 
            this.pcBoxPixel.BackColor = System.Drawing.Color.Black;
            this.pcBoxPixel.Location = new System.Drawing.Point(0, 31);
            this.pcBoxPixel.Margin = new System.Windows.Forms.Padding(2);
            this.pcBoxPixel.Name = "pcBoxPixel";
            this.pcBoxPixel.Size = new System.Drawing.Size(384, 416);
            this.pcBoxPixel.TabIndex = 0;
            this.pcBoxPixel.TabStop = false;
            this.pcBoxPixel.Paint += new System.Windows.Forms.PaintEventHandler(this.pcBoxPixel_Paint);
            this.pcBoxPixel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pcBoxPixel_MouseClick);
            this.pcBoxPixel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pcBoxPixel_MouseMove);
            // 
            // btnStartDataCollection
            // 
            this.btnStartDataCollection.BackColor = System.Drawing.Color.Transparent;
            this.btnStartDataCollection.FlatAppearance.BorderColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnStartDataCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartDataCollection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartDataCollection.ForeColor = System.Drawing.Color.Black;
            this.btnStartDataCollection.Image = ((System.Drawing.Image)(resources.GetObject("btnStartDataCollection.Image")));
            this.btnStartDataCollection.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStartDataCollection.Location = new System.Drawing.Point(17, 440);
            this.btnStartDataCollection.Name = "btnStartDataCollection";
            this.btnStartDataCollection.Size = new System.Drawing.Size(78, 61);
            this.btnStartDataCollection.TabIndex = 0;
            this.btnStartDataCollection.Text = "Start";
            this.btnStartDataCollection.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStartDataCollection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStartDataCollection.UseVisualStyleBackColor = false;
            this.btnStartDataCollection.Click += new System.EventHandler(this.btnStartDataCollection_Click);
            // 
            // grpboxEventData
            // 
            this.grpboxEventData.BackColor = System.Drawing.Color.Transparent;
            this.grpboxEventData.Controls.Add(this.lblTimePeriod);
            this.grpboxEventData.Controls.Add(this.panel2);
            this.grpboxEventData.Controls.Add(this.lblEventCount);
            this.grpboxEventData.Controls.Add(this.pnEventCount);
            this.grpboxEventData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpboxEventData.Location = new System.Drawing.Point(6, 12);
            this.grpboxEventData.Margin = new System.Windows.Forms.Padding(2);
            this.grpboxEventData.Name = "grpboxEventData";
            this.grpboxEventData.Padding = new System.Windows.Forms.Padding(2);
            this.grpboxEventData.Size = new System.Drawing.Size(126, 135);
            this.grpboxEventData.TabIndex = 0;
            this.grpboxEventData.TabStop = false;
            this.grpboxEventData.Text = "Event Data";
            // 
            // lblTimePeriod
            // 
            this.lblTimePeriod.AutoSize = true;
            this.lblTimePeriod.BackColor = System.Drawing.Color.Transparent;
            this.lblTimePeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimePeriod.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblTimePeriod.Location = new System.Drawing.Point(6, 81);
            this.lblTimePeriod.Name = "lblTimePeriod";
            this.lblTimePeriod.Size = new System.Drawing.Size(74, 13);
            this.lblTimePeriod.TabIndex = 8;
            this.lblTimePeriod.Text = "Time Period";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.txtTimePeriod);
            this.panel2.Location = new System.Drawing.Point(10, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(101, 21);
            this.panel2.TabIndex = 7;
            // 
            // txtTimePeriod
            // 
            this.txtTimePeriod.BackColor = System.Drawing.SystemColors.Window;
            this.txtTimePeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimePeriod.Enabled = false;
            this.txtTimePeriod.Location = new System.Drawing.Point(1, 1);
            this.txtTimePeriod.Name = "txtTimePeriod";
            this.txtTimePeriod.Size = new System.Drawing.Size(100, 20);
            this.txtTimePeriod.TabIndex = 4;
            // 
            // lblEventCount
            // 
            this.lblEventCount.AutoSize = true;
            this.lblEventCount.BackColor = System.Drawing.Color.Transparent;
            this.lblEventCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventCount.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblEventCount.Location = new System.Drawing.Point(8, 26);
            this.lblEventCount.Name = "lblEventCount";
            this.lblEventCount.Size = new System.Drawing.Size(77, 13);
            this.lblEventCount.TabIndex = 6;
            this.lblEventCount.Text = "Event Count";
            // 
            // pnEventCount
            // 
            this.pnEventCount.BackColor = System.Drawing.Color.Black;
            this.pnEventCount.Controls.Add(this.txtEventCount);
            this.pnEventCount.Location = new System.Drawing.Point(10, 43);
            this.pnEventCount.Name = "pnEventCount";
            this.pnEventCount.Size = new System.Drawing.Size(101, 21);
            this.pnEventCount.TabIndex = 5;
            // 
            // txtEventCount
            // 
            this.txtEventCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtEventCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEventCount.Enabled = false;
            this.txtEventCount.Location = new System.Drawing.Point(2, 1);
            this.txtEventCount.Name = "txtEventCount";
            this.txtEventCount.Size = new System.Drawing.Size(99, 20);
            this.txtEventCount.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.rtbLog);
            this.tabPage2.Controls.Add(this.btnSend);
            this.tabPage2.Controls.Add(this.tbDataInput);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cbCmdType);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(892, 525);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debug Tool";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(20, 139);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "Recv Log";
            // 
            // rtbLog
            // 
            this.rtbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLog.Location = new System.Drawing.Point(25, 180);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(523, 279);
            this.rtbLog.TabIndex = 5;
            this.rtbLog.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(471, 87);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(76, 35);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbDataInput
            // 
            this.tbDataInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDataInput.Location = new System.Drawing.Point(240, 87);
            this.tbDataInput.Margin = new System.Windows.Forms.Padding(2);
            this.tbDataInput.Name = "tbDataInput";
            this.tbDataInput.Size = new System.Drawing.Size(196, 32);
            this.tbDataInput.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(20, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data(2 Bytes Hex)";
            // 
            // cbCmdType
            // 
            this.cbCmdType.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCmdType.FormattingEnabled = true;
            this.cbCmdType.Items.AddRange(new object[] {
            "Hello",
            "Check Status",
            "Get Therm Info",
            "Mode Reg Access",
            "A/D Config",
            "Power Access",
            "ACQ Access",
            "Board Reset",
            "Enable Crr-Table Wr",
            "Baseline query"});
            this.cbCmdType.Location = new System.Drawing.Point(240, 38);
            this.cbCmdType.Margin = new System.Windows.Forms.Padding(2);
            this.cbCmdType.Name = "cbCmdType";
            this.cbCmdType.Size = new System.Drawing.Size(196, 34);
            this.cbCmdType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Package Type";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ttStatusTooltops
            // 
            this.ttStatusTooltops.Popup += new System.Windows.Forms.PopupEventHandler(this.ttStatusTooltops_Popup);
            // 
            // lblLinkLable
            // 
            this.lblLinkLable.AutoSize = true;
            this.lblLinkLable.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblLinkLable.LinkColor = System.Drawing.Color.Black;
            this.lblLinkLable.Location = new System.Drawing.Point(705, 20);
            this.lblLinkLable.Name = "lblLinkLable";
            this.lblLinkLable.Size = new System.Drawing.Size(105, 13);
            this.lblLinkLable.TabIndex = 4;
            this.lblLinkLable.TabStop = true;
            this.lblLinkLable.Text = "通透光电 | TOFTEK";
            this.lblLinkLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLinkLable.UseMnemonic = false;
            this.lblLinkLable.VisitedLinkColor = System.Drawing.Color.Gray;
            this.lblLinkLable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // backgroundWorkerFittingProgress
            // 
            this.backgroundWorkerFittingProgress.WorkerReportsProgress = true;
            this.backgroundWorkerFittingProgress.WorkerSupportsCancellation = true;
            this.backgroundWorkerFittingProgress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerFittingProgress_DoWork);
            this.backgroundWorkerFittingProgress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerFittingProgress_ProgressChanged);
            this.backgroundWorkerFittingProgress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerFittingProgress_RunWorkerCompleted);
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(909, 585);
            this.Controls.Add(this.lblLinkLable);
            this.Controls.Add(this.tbMainControl);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.LemonChiffon;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Demo";
            this.Text = "闪烁晶体性能检测实验室 | Scintillator Performance Measurement Lab";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Demo_FormClosing);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Demo_MouseWheel);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tbMainControl.ResumeLayout(false);
            this.tbpgMainControl.ResumeLayout(false);
            this.tbpgMainControl.PerformLayout();
            this.grpReportNote.ResumeLayout(false);
            this.grpReportNote.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).EndInit();
            this.grpTempStatus.ResumeLayout(false);
            this.grpTempStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpboxAcqOption.ResumeLayout(false);
            this.grpboxAcqOption.PerformLayout();
            this.panelLighSharePixelCount.ResumeLayout(false);
            this.panelLighSharePixelCount.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarDisplayEnergy)).EndInit();
            this.grpDataAnalysis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLightDivideDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBoxPixel)).EndInit();
            this.grpboxEventData.ResumeLayout(false);
            this.grpboxEventData.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnEventCount.ResumeLayout(false);
            this.pnEventCount.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl tbMainControl;
        public System.Windows.Forms.TabPage tbpgMainControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grpboxAcqOption;
        private System.Windows.Forms.GroupBox grpDataAnalysis;
        private System.Windows.Forms.GroupBox grpboxEventData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCmdType;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbDataInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.PictureBox pcBoxPixel;
        private System.Windows.Forms.ComboBox cmModule;
        private Label lblEventCount;
        private Panel pnEventCount;
        private TextBox txtEventCount;
        private Button btnExport;
        private ToolStripMenuItem factoryDefaultConfigToolStripMenuItem;
        private Label lblDisplayEnergyLine;
        private PictureBox picBarDisplayEnergy;
        private ImageList imageList1;
        private ToolTip ttStatusTooltops;
        private Button btnBoxStatus;
        private Button btnDontTouch;
        private ToolStripMenuItem updateToolStripMenuItem;
        private ToolStripMenuItem scanProtocol扫描协议ToolStripMenuItem;
        private Label lblProtocol;
        private ComboBox cbboxPRPRProtocolList;
        private PictureBox pictureBox1;
        private Label lblTimePeriod;
        private Panel panel3;
        private TextBox txtPRPATriggerValue;
        private Label lblPRPATriggerValue;
        private Label lblPRPATriggerType;
        public TextBox txtPixelAndCount;
        private VerticalProgressBar.VerticalProgressBar vprgBar1stSensor;
        private GroupBox grpTempStatus;
        private Label lbl4thSensor;
        private Label lbl3rdSensor;
        private Label lbl2ndSensor;
        private VerticalProgressBar.VerticalProgressBar vprgBarAveTemp;
        private VerticalProgressBar.VerticalProgressBar vprgBar4thSensor;
        private VerticalProgressBar.VerticalProgressBar vprgBar3rdSensor;
        private VerticalProgressBar.VerticalProgressBar vprgBar2rdSensor;
        private Label lbl1stSensor;
        private Label lblAverage;
        private Button btnGenerateReport;
        private Label lblRangeTo;
        private Panel panel4;
        private TextBox txtPeakHighLimit;
        private Panel panel1;
        private TextBox txtPeakLowLimit;
        private Label lblPeakValue;
        private Panel panel2;
        private TextBox txtTimePeriod;
        private System.ComponentModel.BackgroundWorker backgroundWorkerFittingProgress;
        private Label lblLoading;
        private PictureBox picProgress;
        private Button btnStartDataCollection;
        private ComboBox cbboxPRPATriggerType;
        private LinkLabel lblLinkLable;
        private Button btnMergeReport;
        private Label label4;
        private Panel panel5;
        private TextBox txtFittingUpBand;
        private Panel panel6;
        private TextBox txtFittingLowBand;
        private PictureBox picLightDivideDisplay;
        private Label lblLightSharePixelCount;
        private Panel panelLighSharePixelCount;
        private TextBox txtLightSharePixelCount;
        private Label lblQuilifiedType;
        private ComboBox cbQualifiedType;
        private GroupBox grpReportNote;
        private Label lblArrayNo;
        private Label lblOrderNo;
        private Panel panel8;
        private TextBox txtArrayNo;
        private Panel panel7;
        private TextBox txtOrderNo;
    }




}

