﻿namespace DemoTool {
    partial class ScanProtocolConfig {
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ScanProtocolConfig ) );
            this.cbboxPRPRProtocolList = new System.Windows.Forms.ComboBox( );
            this.btnPRPALoadProtocol = new System.Windows.Forms.Button( );
            this.pnTriggerCount = new System.Windows.Forms.Panel( );
            this.grpPRPAProtocolParameters = new System.Windows.Forms.GroupBox( );
            this.groupBox6 = new System.Windows.Forms.GroupBox( );
            this.lblPRRPASourceType = new System.Windows.Forms.Label( );
            this.cmboxPRPASourceType = new System.Windows.Forms.ComboBox( );
            this.groupBox5 = new System.Windows.Forms.GroupBox( );
            this.panel7 = new System.Windows.Forms.Panel( );
            this.txtPRPYAxis = new System.Windows.Forms.TextBox( );
            this.panel6 = new System.Windows.Forms.Panel( );
            this.txtPRPXAxis = new System.Windows.Forms.TextBox( );
            this.lblPRPYAxis = new System.Windows.Forms.Label( );
            this.lblPRPXAxis = new System.Windows.Forms.Label( );
            this.groupBox4 = new System.Windows.Forms.GroupBox( );
            this.lblPRPAVref = new System.Windows.Forms.Label( );
            this.panel4 = new System.Windows.Forms.Panel( );
            this.txtPRPAVref = new System.Windows.Forms.TextBox( );
            this.groupBox3 = new System.Windows.Forms.GroupBox( );
            this.lblPRPAVbias = new System.Windows.Forms.Label( );
            this.panel3 = new System.Windows.Forms.Panel( );
            this.txtPRPAVbias = new System.Windows.Forms.TextBox( );
            this.groupBox2 = new System.Windows.Forms.GroupBox( );
            this.lblPRPAADCRange = new System.Windows.Forms.Label( );
            this.cmboxPRPAADCRange = new System.Windows.Forms.ComboBox( );
            this.groupBox1 = new System.Windows.Forms.GroupBox( );
            this.lblPRPAADCTriggerThreshod = new System.Windows.Forms.Label( );
            this.cmboxPRPAADCTriggerThreshod = new System.Windows.Forms.ComboBox( );
            this.grpPRPAEncodingMode = new System.Windows.Forms.GroupBox( );
            this.lblPRPAEncodingMode = new System.Windows.Forms.Label( );
            this.cmboxEncodingMode = new System.Windows.Forms.ComboBox( );
            this.grpPRPAIntegralTime = new System.Windows.Forms.GroupBox( );
            this.lblPRRPAIntegralTime = new System.Windows.Forms.Label( );
            this.cbboxPRPAIntegralTime = new System.Windows.Forms.ComboBox( );
            this.grpPRPAEnableCorrection = new System.Windows.Forms.GroupBox( );
            this.picBarPRPAEnableCorrection = new System.Windows.Forms.PictureBox( );
            this.lblDisplayEnergyLine = new System.Windows.Forms.Label( );
            this.grpPRPAArraySize = new System.Windows.Forms.GroupBox( );
            this.lblPRPAArraySize = new System.Windows.Forms.Label( );
            this.cbboxArraySize = new System.Windows.Forms.ComboBox( );
            this.grpPRPATriggerCondition = new System.Windows.Forms.GroupBox( );
            this.panel2 = new System.Windows.Forms.Panel( );
            this.txtPRPATriggerValue = new System.Windows.Forms.TextBox( );
            this.lblPRPATriggerValue = new System.Windows.Forms.Label( );
            this.lblPRPATriggerType = new System.Windows.Forms.Label( );
            this.cbboxPRPATriggerType = new System.Windows.Forms.ComboBox( );
            this.panelCreateNewProtocol = new System.Windows.Forms.Panel( );
            this.btnCreateNewProtocol = new System.Windows.Forms.Button( );
            this.panelRenameProtocol = new System.Windows.Forms.Panel( );
            this.btnRenameProtocol = new System.Windows.Forms.Button( );
            this.pnTriggerCount.SuspendLayout( );
            this.grpPRPAProtocolParameters.SuspendLayout( );
            this.groupBox6.SuspendLayout( );
            this.groupBox5.SuspendLayout( );
            this.panel7.SuspendLayout( );
            this.panel6.SuspendLayout( );
            this.groupBox4.SuspendLayout( );
            this.panel4.SuspendLayout( );
            this.groupBox3.SuspendLayout( );
            this.panel3.SuspendLayout( );
            this.groupBox2.SuspendLayout( );
            this.groupBox1.SuspendLayout( );
            this.grpPRPAEncodingMode.SuspendLayout( );
            this.grpPRPAIntegralTime.SuspendLayout( );
            this.grpPRPAEnableCorrection.SuspendLayout( );
            ( ( System.ComponentModel.ISupportInitialize )( this.picBarPRPAEnableCorrection ) ).BeginInit( );
            this.grpPRPAArraySize.SuspendLayout( );
            this.grpPRPATriggerCondition.SuspendLayout( );
            this.panel2.SuspendLayout( );
            this.panelCreateNewProtocol.SuspendLayout( );
            this.panelRenameProtocol.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // cbboxPRPRProtocolList
            // 
            this.cbboxPRPRProtocolList.AutoCompleteCustomSource.AddRange( new string[] {
            "English",
            "简体中文"} );
            this.cbboxPRPRProtocolList.FormattingEnabled = true;
            this.cbboxPRPRProtocolList.Location = new System.Drawing.Point( 23, 26 );
            this.cbboxPRPRProtocolList.Margin = new System.Windows.Forms.Padding( 2 );
            this.cbboxPRPRProtocolList.Name = "cbboxPRPRProtocolList";
            this.cbboxPRPRProtocolList.Size = new System.Drawing.Size( 127, 21 );
            this.cbboxPRPRProtocolList.TabIndex = 9;
            this.cbboxPRPRProtocolList.SelectedIndexChanged += new System.EventHandler( this.cbboxPRPRProtocolList_SelectedIndexChanged );
            this.cbboxPRPRProtocolList.MouseClick += new System.Windows.Forms.MouseEventHandler( this.cbboxPRPRProtocolList_MouseClick );
            // 
            // btnPRPALoadProtocol
            // 
            this.btnPRPALoadProtocol.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPRPALoadProtocol.Location = new System.Drawing.Point( 1, 1 );
            this.btnPRPALoadProtocol.Margin = new System.Windows.Forms.Padding( 2 );
            this.btnPRPALoadProtocol.Name = "btnPRPALoadProtocol";
            this.btnPRPALoadProtocol.Size = new System.Drawing.Size( 126, 24 );
            this.btnPRPALoadProtocol.TabIndex = 10;
            this.btnPRPALoadProtocol.Text = "Load Protocol";
            this.btnPRPALoadProtocol.UseVisualStyleBackColor = true;
            this.btnPRPALoadProtocol.Click += new System.EventHandler( this.btnPRPALoadProtocol_Click );
            // 
            // pnTriggerCount
            // 
            this.pnTriggerCount.BackColor = System.Drawing.Color.Black;
            this.pnTriggerCount.Controls.Add( this.btnPRPALoadProtocol );
            this.pnTriggerCount.Location = new System.Drawing.Point( 22, 81 );
            this.pnTriggerCount.Name = "pnTriggerCount";
            this.pnTriggerCount.Size = new System.Drawing.Size( 128, 26 );
            this.pnTriggerCount.TabIndex = 12;
            // 
            // grpPRPAProtocolParameters
            // 
            this.grpPRPAProtocolParameters.Controls.Add( this.groupBox6 );
            this.grpPRPAProtocolParameters.Controls.Add( this.groupBox5 );
            this.grpPRPAProtocolParameters.Controls.Add( this.groupBox4 );
            this.grpPRPAProtocolParameters.Controls.Add( this.groupBox3 );
            this.grpPRPAProtocolParameters.Controls.Add( this.groupBox2 );
            this.grpPRPAProtocolParameters.Controls.Add( this.groupBox1 );
            this.grpPRPAProtocolParameters.Controls.Add( this.grpPRPAEncodingMode );
            this.grpPRPAProtocolParameters.Controls.Add( this.grpPRPAIntegralTime );
            this.grpPRPAProtocolParameters.Controls.Add( this.grpPRPAEnableCorrection );
            this.grpPRPAProtocolParameters.Controls.Add( this.grpPRPAArraySize );
            this.grpPRPAProtocolParameters.Controls.Add( this.grpPRPATriggerCondition );
            this.grpPRPAProtocolParameters.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.grpPRPAProtocolParameters.Location = new System.Drawing.Point( 180, 10 );
            this.grpPRPAProtocolParameters.Margin = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAProtocolParameters.Name = "grpPRPAProtocolParameters";
            this.grpPRPAProtocolParameters.Padding = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAProtocolParameters.Size = new System.Drawing.Size( 764, 498 );
            this.grpPRPAProtocolParameters.TabIndex = 13;
            this.grpPRPAProtocolParameters.TabStop = false;
            this.grpPRPAProtocolParameters.Text = "Protocol Parameters";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add( this.lblPRRPASourceType );
            this.groupBox6.Controls.Add( this.cmboxPRPASourceType );
            this.groupBox6.Location = new System.Drawing.Point( 289, 249 );
            this.groupBox6.Margin = new System.Windows.Forms.Padding( 2 );
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding( 2 );
            this.groupBox6.Size = new System.Drawing.Size( 230, 55 );
            this.groupBox6.TabIndex = 83;
            this.groupBox6.TabStop = false;
            // 
            // lblPRRPASourceType
            // 
            this.lblPRRPASourceType.AutoSize = true;
            this.lblPRRPASourceType.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRRPASourceType.Location = new System.Drawing.Point( 4, 27 );
            this.lblPRRPASourceType.Name = "lblPRRPASourceType";
            this.lblPRRPASourceType.Size = new System.Drawing.Size( 79, 13 );
            this.lblPRRPASourceType.TabIndex = 15;
            this.lblPRRPASourceType.Text = "SourceType:";
            // 
            // cmboxPRPASourceType
            // 
            this.cmboxPRPASourceType.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.cmboxPRPASourceType.FormattingEnabled = true;
            this.cmboxPRPASourceType.Items.AddRange( new object[] {
            "Ge_68",
            "Na_22",
            "F_18_511kev",
            "CsBgo",
            "Lu_307kev",
            "Lu_SinglePixel",
            "CsLyso",
            "UseMaxPeak",
            "Light Share",
            "LightShare196Version",
            "LightShareSiglePixel"} );
            this.cmboxPRPASourceType.Location = new System.Drawing.Point( 118, 21 );
            this.cmboxPRPASourceType.Margin = new System.Windows.Forms.Padding( 2 );
            this.cmboxPRPASourceType.Name = "cmboxPRPASourceType";
            this.cmboxPRPASourceType.Size = new System.Drawing.Size( 108, 21 );
            this.cmboxPRPASourceType.TabIndex = 15;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add( this.panel7 );
            this.groupBox5.Controls.Add( this.panel6 );
            this.groupBox5.Controls.Add( this.lblPRPYAxis );
            this.groupBox5.Controls.Add( this.lblPRPXAxis );
            this.groupBox5.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.groupBox5.Location = new System.Drawing.Point( 287, 157 );
            this.groupBox5.Margin = new System.Windows.Forms.Padding( 2 );
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding( 2 );
            this.groupBox5.Size = new System.Drawing.Size( 237, 76 );
            this.groupBox5.TabIndex = 86;
            this.groupBox5.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Black;
            this.panel7.Controls.Add( this.txtPRPYAxis );
            this.panel7.Location = new System.Drawing.Point( 129, 40 );
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size( 101, 20 );
            this.panel7.TabIndex = 81;
            // 
            // txtPRPYAxis
            // 
            this.txtPRPYAxis.BackColor = System.Drawing.SystemColors.Window;
            this.txtPRPYAxis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPRPYAxis.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.txtPRPYAxis.Location = new System.Drawing.Point( 1, 1 );
            this.txtPRPYAxis.Name = "txtPRPYAxis";
            this.txtPRPYAxis.Size = new System.Drawing.Size( 100, 19 );
            this.txtPRPYAxis.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Black;
            this.panel6.Controls.Add( this.txtPRPXAxis );
            this.panel6.Location = new System.Drawing.Point( 129, 15 );
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size( 101, 20 );
            this.panel6.TabIndex = 80;
            // 
            // txtPRPXAxis
            // 
            this.txtPRPXAxis.BackColor = System.Drawing.SystemColors.Window;
            this.txtPRPXAxis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPRPXAxis.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.txtPRPXAxis.Location = new System.Drawing.Point( 1, 1 );
            this.txtPRPXAxis.Name = "txtPRPXAxis";
            this.txtPRPXAxis.Size = new System.Drawing.Size( 100, 19 );
            this.txtPRPXAxis.TabIndex = 4;
            // 
            // lblPRPYAxis
            // 
            this.lblPRPYAxis.AutoSize = true;
            this.lblPRPYAxis.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPYAxis.Location = new System.Drawing.Point( 4, 46 );
            this.lblPRPYAxis.Name = "lblPRPYAxis";
            this.lblPRPYAxis.Size = new System.Drawing.Size( 42, 13 );
            this.lblPRPYAxis.TabIndex = 13;
            this.lblPRPYAxis.Text = "YAxis:";
            // 
            // lblPRPXAxis
            // 
            this.lblPRPXAxis.AutoSize = true;
            this.lblPRPXAxis.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPXAxis.Location = new System.Drawing.Point( 4, 21 );
            this.lblPRPXAxis.Name = "lblPRPXAxis";
            this.lblPRPXAxis.Size = new System.Drawing.Size( 42, 13 );
            this.lblPRPXAxis.TabIndex = 12;
            this.lblPRPXAxis.Text = "XAxis:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add( this.lblPRPAVref );
            this.groupBox4.Controls.Add( this.panel4 );
            this.groupBox4.Location = new System.Drawing.Point( 288, 87 );
            this.groupBox4.Margin = new System.Windows.Forms.Padding( 2 );
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding( 2 );
            this.groupBox4.Size = new System.Drawing.Size( 236, 55 );
            this.groupBox4.TabIndex = 82;
            this.groupBox4.TabStop = false;
            // 
            // lblPRPAVref
            // 
            this.lblPRPAVref.AutoSize = true;
            this.lblPRPAVref.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPAVref.Location = new System.Drawing.Point( 4, 27 );
            this.lblPRPAVref.Name = "lblPRPAVref";
            this.lblPRPAVref.Size = new System.Drawing.Size( 67, 13 );
            this.lblPRPAVref.TabIndex = 78;
            this.lblPRPAVref.Text = "Vref(0-2v):";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Controls.Add( this.txtPRPAVref );
            this.panel4.Location = new System.Drawing.Point( 129, 22 );
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size( 101, 20 );
            this.panel4.TabIndex = 79;
            // 
            // txtPRPAVref
            // 
            this.txtPRPAVref.BackColor = System.Drawing.SystemColors.Window;
            this.txtPRPAVref.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPRPAVref.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.txtPRPAVref.Location = new System.Drawing.Point( 1, 1 );
            this.txtPRPAVref.Name = "txtPRPAVref";
            this.txtPRPAVref.Size = new System.Drawing.Size( 100, 19 );
            this.txtPRPAVref.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add( this.lblPRPAVbias );
            this.groupBox3.Controls.Add( this.panel3 );
            this.groupBox3.Location = new System.Drawing.Point( 288, 27 );
            this.groupBox3.Margin = new System.Windows.Forms.Padding( 2 );
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding( 2 );
            this.groupBox3.Size = new System.Drawing.Size( 236, 55 );
            this.groupBox3.TabIndex = 81;
            this.groupBox3.TabStop = false;
            // 
            // lblPRPAVbias
            // 
            this.lblPRPAVbias.AutoSize = true;
            this.lblPRPAVbias.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPAVbias.Location = new System.Drawing.Point( 4, 27 );
            this.lblPRPAVbias.Name = "lblPRPAVbias";
            this.lblPRPAVbias.Size = new System.Drawing.Size( 89, 13 );
            this.lblPRPAVbias.TabIndex = 15;
            this.lblPRPAVbias.Text = "Vbias(20-30v):";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add( this.txtPRPAVbias );
            this.panel3.Location = new System.Drawing.Point( 130, 22 );
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size( 101, 20 );
            this.panel3.TabIndex = 77;
            // 
            // txtPRPAVbias
            // 
            this.txtPRPAVbias.BackColor = System.Drawing.SystemColors.Window;
            this.txtPRPAVbias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPRPAVbias.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.txtPRPAVbias.Location = new System.Drawing.Point( 1, 1 );
            this.txtPRPAVbias.Name = "txtPRPAVbias";
            this.txtPRPAVbias.Size = new System.Drawing.Size( 100, 19 );
            this.txtPRPAVbias.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add( this.lblPRPAADCRange );
            this.groupBox2.Controls.Add( this.cmboxPRPAADCRange );
            this.groupBox2.Location = new System.Drawing.Point( 26, 408 );
            this.groupBox2.Margin = new System.Windows.Forms.Padding( 2 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding( 2 );
            this.groupBox2.Size = new System.Drawing.Size( 230, 55 );
            this.groupBox2.TabIndex = 85;
            this.groupBox2.TabStop = false;
            // 
            // lblPRPAADCRange
            // 
            this.lblPRPAADCRange.AutoSize = true;
            this.lblPRPAADCRange.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPAADCRange.Location = new System.Drawing.Point( 4, 27 );
            this.lblPRPAADCRange.Name = "lblPRPAADCRange";
            this.lblPRPAADCRange.Size = new System.Drawing.Size( 77, 13 );
            this.lblPRPAADCRange.TabIndex = 75;
            this.lblPRPAADCRange.Text = "ADC Range:";
            // 
            // cmboxPRPAADCRange
            // 
            this.cmboxPRPAADCRange.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.cmboxPRPAADCRange.FormattingEnabled = true;
            this.cmboxPRPAADCRange.Items.AddRange( new object[] {
            "Option_LSBP49",
            "Option_LSBP56",
            "Option_LSBP65 ",
            "Option_LSBP78",
            "Option_LSBP98"} );
            this.cmboxPRPAADCRange.Location = new System.Drawing.Point( 82, 21 );
            this.cmboxPRPAADCRange.Margin = new System.Windows.Forms.Padding( 2 );
            this.cmboxPRPAADCRange.Name = "cmboxPRPAADCRange";
            this.cmboxPRPAADCRange.Size = new System.Drawing.Size( 144, 21 );
            this.cmboxPRPAADCRange.TabIndex = 76;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.lblPRPAADCTriggerThreshod );
            this.groupBox1.Controls.Add( this.cmboxPRPAADCTriggerThreshod );
            this.groupBox1.Location = new System.Drawing.Point( 26, 348 );
            this.groupBox1.Margin = new System.Windows.Forms.Padding( 2 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding( 2 );
            this.groupBox1.Size = new System.Drawing.Size( 230, 55 );
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            // 
            // lblPRPAADCTriggerThreshod
            // 
            this.lblPRPAADCTriggerThreshod.AutoSize = true;
            this.lblPRPAADCTriggerThreshod.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPAADCTriggerThreshod.Location = new System.Drawing.Point( 4, 27 );
            this.lblPRPAADCTriggerThreshod.Name = "lblPRPAADCTriggerThreshod";
            this.lblPRPAADCTriggerThreshod.Size = new System.Drawing.Size( 108, 13 );
            this.lblPRPAADCTriggerThreshod.TabIndex = 73;
            this.lblPRPAADCTriggerThreshod.Text = "Trigger Threshod:";
            // 
            // cmboxPRPAADCTriggerThreshod
            // 
            this.cmboxPRPAADCTriggerThreshod.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.cmboxPRPAADCTriggerThreshod.FormattingEnabled = true;
            this.cmboxPRPAADCTriggerThreshod.Items.AddRange( new object[] {
            "Option_40",
            "Option_80",
            "Option_160",
            "Option_320"} );
            this.cmboxPRPAADCTriggerThreshod.Location = new System.Drawing.Point( 118, 21 );
            this.cmboxPRPAADCTriggerThreshod.Margin = new System.Windows.Forms.Padding( 2 );
            this.cmboxPRPAADCTriggerThreshod.Name = "cmboxPRPAADCTriggerThreshod";
            this.cmboxPRPAADCTriggerThreshod.Size = new System.Drawing.Size( 108, 21 );
            this.cmboxPRPAADCTriggerThreshod.TabIndex = 74;
            // 
            // grpPRPAEncodingMode
            // 
            this.grpPRPAEncodingMode.Controls.Add( this.lblPRPAEncodingMode );
            this.grpPRPAEncodingMode.Controls.Add( this.cmboxEncodingMode );
            this.grpPRPAEncodingMode.Location = new System.Drawing.Point( 26, 288 );
            this.grpPRPAEncodingMode.Margin = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAEncodingMode.Name = "grpPRPAEncodingMode";
            this.grpPRPAEncodingMode.Padding = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAEncodingMode.Size = new System.Drawing.Size( 230, 55 );
            this.grpPRPAEncodingMode.TabIndex = 83;
            this.grpPRPAEncodingMode.TabStop = false;
            // 
            // lblPRPAEncodingMode
            // 
            this.lblPRPAEncodingMode.AutoSize = true;
            this.lblPRPAEncodingMode.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPAEncodingMode.Location = new System.Drawing.Point( 4, 27 );
            this.lblPRPAEncodingMode.Name = "lblPRPAEncodingMode";
            this.lblPRPAEncodingMode.Size = new System.Drawing.Size( 99, 13 );
            this.lblPRPAEncodingMode.TabIndex = 71;
            this.lblPRPAEncodingMode.Text = "Encoding Mode:";
            // 
            // cmboxEncodingMode
            // 
            this.cmboxEncodingMode.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.cmboxEncodingMode.FormattingEnabled = true;
            this.cmboxEncodingMode.Items.AddRange( new object[] {
            "Option_SignalPxiel",
            "Option_XDirection",
            "Option_YDirection",
            "Option_NoEncoding"} );
            this.cmboxEncodingMode.Location = new System.Drawing.Point( 118, 21 );
            this.cmboxEncodingMode.Margin = new System.Windows.Forms.Padding( 2 );
            this.cmboxEncodingMode.Name = "cmboxEncodingMode";
            this.cmboxEncodingMode.Size = new System.Drawing.Size( 108, 21 );
            this.cmboxEncodingMode.TabIndex = 72;
            // 
            // grpPRPAIntegralTime
            // 
            this.grpPRPAIntegralTime.Controls.Add( this.lblPRRPAIntegralTime );
            this.grpPRPAIntegralTime.Controls.Add( this.cbboxPRPAIntegralTime );
            this.grpPRPAIntegralTime.Location = new System.Drawing.Point( 26, 228 );
            this.grpPRPAIntegralTime.Margin = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAIntegralTime.Name = "grpPRPAIntegralTime";
            this.grpPRPAIntegralTime.Padding = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAIntegralTime.Size = new System.Drawing.Size( 230, 55 );
            this.grpPRPAIntegralTime.TabIndex = 82;
            this.grpPRPAIntegralTime.TabStop = false;
            // 
            // lblPRRPAIntegralTime
            // 
            this.lblPRRPAIntegralTime.AutoSize = true;
            this.lblPRRPAIntegralTime.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRRPAIntegralTime.Location = new System.Drawing.Point( 4, 27 );
            this.lblPRRPAIntegralTime.Name = "lblPRRPAIntegralTime";
            this.lblPRRPAIntegralTime.Size = new System.Drawing.Size( 110, 13 );
            this.lblPRRPAIntegralTime.TabIndex = 15;
            this.lblPRRPAIntegralTime.Text = "Intergral Time(ns):";
            // 
            // cbboxPRPAIntegralTime
            // 
            this.cbboxPRPAIntegralTime.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.cbboxPRPAIntegralTime.FormattingEnabled = true;
            this.cbboxPRPAIntegralTime.Items.AddRange( new object[] {
            "Option_500ns",
            "Option_1000ns",
            "Option_1500ns",
            "Option_2000ns"} );
            this.cbboxPRPAIntegralTime.Location = new System.Drawing.Point( 118, 21 );
            this.cbboxPRPAIntegralTime.Margin = new System.Windows.Forms.Padding( 2 );
            this.cbboxPRPAIntegralTime.Name = "cbboxPRPAIntegralTime";
            this.cbboxPRPAIntegralTime.Size = new System.Drawing.Size( 108, 21 );
            this.cbboxPRPAIntegralTime.TabIndex = 15;
            // 
            // grpPRPAEnableCorrection
            // 
            this.grpPRPAEnableCorrection.Controls.Add( this.picBarPRPAEnableCorrection );
            this.grpPRPAEnableCorrection.Controls.Add( this.lblDisplayEnergyLine );
            this.grpPRPAEnableCorrection.Location = new System.Drawing.Point( 26, 167 );
            this.grpPRPAEnableCorrection.Margin = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAEnableCorrection.Name = "grpPRPAEnableCorrection";
            this.grpPRPAEnableCorrection.Padding = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAEnableCorrection.Size = new System.Drawing.Size( 230, 55 );
            this.grpPRPAEnableCorrection.TabIndex = 81;
            this.grpPRPAEnableCorrection.TabStop = false;
            // 
            // picBarPRPAEnableCorrection
            // 
            this.picBarPRPAEnableCorrection.Image = global::DemoTool.Properties.Resources.GreenOn;
            this.picBarPRPAEnableCorrection.Location = new System.Drawing.Point( 4, 22 );
            this.picBarPRPAEnableCorrection.Name = "picBarPRPAEnableCorrection";
            this.picBarPRPAEnableCorrection.Size = new System.Drawing.Size( 16, 16 );
            this.picBarPRPAEnableCorrection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBarPRPAEnableCorrection.TabIndex = 70;
            this.picBarPRPAEnableCorrection.TabStop = false;
            this.picBarPRPAEnableCorrection.Tag = "on";
            // 
            // lblDisplayEnergyLine
            // 
            this.lblDisplayEnergyLine.AutoSize = true;
            this.lblDisplayEnergyLine.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblDisplayEnergyLine.Location = new System.Drawing.Point( 23, 21 );
            this.lblDisplayEnergyLine.Name = "lblDisplayEnergyLine";
            this.lblDisplayEnergyLine.Size = new System.Drawing.Size( 108, 13 );
            this.lblDisplayEnergyLine.TabIndex = 69;
            this.lblDisplayEnergyLine.Text = "Enable Correction";
            // 
            // grpPRPAArraySize
            // 
            this.grpPRPAArraySize.Controls.Add( this.lblPRPAArraySize );
            this.grpPRPAArraySize.Controls.Add( this.cbboxArraySize );
            this.grpPRPAArraySize.Location = new System.Drawing.Point( 26, 27 );
            this.grpPRPAArraySize.Margin = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAArraySize.Name = "grpPRPAArraySize";
            this.grpPRPAArraySize.Padding = new System.Windows.Forms.Padding( 2 );
            this.grpPRPAArraySize.Size = new System.Drawing.Size( 208, 55 );
            this.grpPRPAArraySize.TabIndex = 80;
            this.grpPRPAArraySize.TabStop = false;
            // 
            // lblPRPAArraySize
            // 
            this.lblPRPAArraySize.AutoSize = true;
            this.lblPRPAArraySize.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPAArraySize.Location = new System.Drawing.Point( 4, 27 );
            this.lblPRPAArraySize.Name = "lblPRPAArraySize";
            this.lblPRPAArraySize.Size = new System.Drawing.Size( 68, 13 );
            this.lblPRPAArraySize.TabIndex = 9;
            this.lblPRPAArraySize.Text = "Array Size:";
            // 
            // cbboxArraySize
            // 
            this.cbboxArraySize.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.cbboxArraySize.FormattingEnabled = true;
            this.cbboxArraySize.Items.AddRange( new object[] {
            "16x16",
            "8x8"} );
            this.cbboxArraySize.Location = new System.Drawing.Point( 76, 21 );
            this.cbboxArraySize.Margin = new System.Windows.Forms.Padding( 2 );
            this.cbboxArraySize.Name = "cbboxArraySize";
            this.cbboxArraySize.Size = new System.Drawing.Size( 101, 21 );
            this.cbboxArraySize.TabIndex = 10;
            // 
            // grpPRPATriggerCondition
            // 
            this.grpPRPATriggerCondition.Controls.Add( this.panel2 );
            this.grpPRPATriggerCondition.Controls.Add( this.lblPRPATriggerValue );
            this.grpPRPATriggerCondition.Controls.Add( this.lblPRPATriggerType );
            this.grpPRPATriggerCondition.Controls.Add( this.cbboxPRPATriggerType );
            this.grpPRPATriggerCondition.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.grpPRPATriggerCondition.Location = new System.Drawing.Point( 26, 87 );
            this.grpPRPATriggerCondition.Margin = new System.Windows.Forms.Padding( 2 );
            this.grpPRPATriggerCondition.Name = "grpPRPATriggerCondition";
            this.grpPRPATriggerCondition.Padding = new System.Windows.Forms.Padding( 2 );
            this.grpPRPATriggerCondition.Size = new System.Drawing.Size( 230, 76 );
            this.grpPRPATriggerCondition.TabIndex = 11;
            this.grpPRPATriggerCondition.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add( this.txtPRPATriggerValue );
            this.panel2.Location = new System.Drawing.Point( 111, 41 );
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size( 106, 20 );
            this.panel2.TabIndex = 14;
            // 
            // txtPRPATriggerValue
            // 
            this.txtPRPATriggerValue.BackColor = System.Drawing.SystemColors.Window;
            this.txtPRPATriggerValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPRPATriggerValue.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.txtPRPATriggerValue.Location = new System.Drawing.Point( 1, 1 );
            this.txtPRPATriggerValue.Name = "txtPRPATriggerValue";
            this.txtPRPATriggerValue.Size = new System.Drawing.Size( 103, 19 );
            this.txtPRPATriggerValue.TabIndex = 4;
            // 
            // lblPRPATriggerValue
            // 
            this.lblPRPATriggerValue.AutoSize = true;
            this.lblPRPATriggerValue.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPATriggerValue.Location = new System.Drawing.Point( 4, 46 );
            this.lblPRPATriggerValue.Name = "lblPRPATriggerValue";
            this.lblPRPATriggerValue.Size = new System.Drawing.Size( 87, 13 );
            this.lblPRPATriggerValue.TabIndex = 13;
            this.lblPRPATriggerValue.Text = "Trigger Value:";
            // 
            // lblPRPATriggerType
            // 
            this.lblPRPATriggerType.AutoSize = true;
            this.lblPRPATriggerType.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPRPATriggerType.Location = new System.Drawing.Point( 4, 21 );
            this.lblPRPATriggerType.Name = "lblPRPATriggerType";
            this.lblPRPATriggerType.Size = new System.Drawing.Size( 83, 13 );
            this.lblPRPATriggerType.TabIndex = 12;
            this.lblPRPATriggerType.Text = "Trigger Type:";
            // 
            // cbboxPRPATriggerType
            // 
            this.cbboxPRPATriggerType.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.cbboxPRPATriggerType.FormattingEnabled = true;
            this.cbboxPRPATriggerType.Items.AddRange( new object[] {
            "TimeTrigger",
            "TotalEventCountTrigger",
            "SinglePixelEventCountTrigger",
            "AnalysisResultTrigger"} );
            this.cbboxPRPATriggerType.Location = new System.Drawing.Point( 110, 15 );
            this.cbboxPRPATriggerType.Margin = new System.Windows.Forms.Padding( 2 );
            this.cbboxPRPATriggerType.Name = "cbboxPRPATriggerType";
            this.cbboxPRPATriggerType.Size = new System.Drawing.Size( 108, 21 );
            this.cbboxPRPATriggerType.TabIndex = 12;
            // 
            // panelCreateNewProtocol
            // 
            this.panelCreateNewProtocol.BackColor = System.Drawing.Color.Black;
            this.panelCreateNewProtocol.Controls.Add( this.btnCreateNewProtocol );
            this.panelCreateNewProtocol.Location = new System.Drawing.Point( 22, 124 );
            this.panelCreateNewProtocol.Name = "panelCreateNewProtocol";
            this.panelCreateNewProtocol.Size = new System.Drawing.Size( 128, 26 );
            this.panelCreateNewProtocol.TabIndex = 13;
            // 
            // btnCreateNewProtocol
            // 
            this.btnCreateNewProtocol.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCreateNewProtocol.Location = new System.Drawing.Point( 1, 1 );
            this.btnCreateNewProtocol.Margin = new System.Windows.Forms.Padding( 2 );
            this.btnCreateNewProtocol.Name = "btnCreateNewProtocol";
            this.btnCreateNewProtocol.Size = new System.Drawing.Size( 126, 24 );
            this.btnCreateNewProtocol.TabIndex = 10;
            this.btnCreateNewProtocol.Text = "Create New Protocol";
            this.btnCreateNewProtocol.UseVisualStyleBackColor = true;
            this.btnCreateNewProtocol.Click += new System.EventHandler( this.btnCreateNewProtocol_Click );
            // 
            // panelRenameProtocol
            // 
            this.panelRenameProtocol.BackColor = System.Drawing.Color.Black;
            this.panelRenameProtocol.Controls.Add( this.btnRenameProtocol );
            this.panelRenameProtocol.Location = new System.Drawing.Point( 22, 166 );
            this.panelRenameProtocol.Name = "panelRenameProtocol";
            this.panelRenameProtocol.Size = new System.Drawing.Size( 128, 26 );
            this.panelRenameProtocol.TabIndex = 14;
            // 
            // btnRenameProtocol
            // 
            this.btnRenameProtocol.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRenameProtocol.Location = new System.Drawing.Point( 1, 1 );
            this.btnRenameProtocol.Margin = new System.Windows.Forms.Padding( 2 );
            this.btnRenameProtocol.Name = "btnRenameProtocol";
            this.btnRenameProtocol.Size = new System.Drawing.Size( 126, 24 );
            this.btnRenameProtocol.TabIndex = 10;
            this.btnRenameProtocol.Text = "Rename Protocol";
            this.btnRenameProtocol.UseVisualStyleBackColor = true;
            // 
            // ScanProtocolConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size( 946, 509 );
            this.Controls.Add( this.panelRenameProtocol );
            this.Controls.Add( this.panelCreateNewProtocol );
            this.Controls.Add( this.grpPRPAProtocolParameters );
            this.Controls.Add( this.pnTriggerCount );
            this.Controls.Add( this.cbboxPRPRProtocolList );
            this.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "$this.Icon" ) ) );
            this.Margin = new System.Windows.Forms.Padding( 2 );
            this.Name = "ScanProtocolConfig";
            this.Text = "ScanProtocolConfig";
            this.pnTriggerCount.ResumeLayout( false );
            this.grpPRPAProtocolParameters.ResumeLayout( false );
            this.groupBox6.ResumeLayout( false );
            this.groupBox6.PerformLayout( );
            this.groupBox5.ResumeLayout( false );
            this.groupBox5.PerformLayout( );
            this.panel7.ResumeLayout( false );
            this.panel7.PerformLayout( );
            this.panel6.ResumeLayout( false );
            this.panel6.PerformLayout( );
            this.groupBox4.ResumeLayout( false );
            this.groupBox4.PerformLayout( );
            this.panel4.ResumeLayout( false );
            this.panel4.PerformLayout( );
            this.groupBox3.ResumeLayout( false );
            this.groupBox3.PerformLayout( );
            this.panel3.ResumeLayout( false );
            this.panel3.PerformLayout( );
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout( );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout( );
            this.grpPRPAEncodingMode.ResumeLayout( false );
            this.grpPRPAEncodingMode.PerformLayout( );
            this.grpPRPAIntegralTime.ResumeLayout( false );
            this.grpPRPAIntegralTime.PerformLayout( );
            this.grpPRPAEnableCorrection.ResumeLayout( false );
            this.grpPRPAEnableCorrection.PerformLayout( );
            ( ( System.ComponentModel.ISupportInitialize )( this.picBarPRPAEnableCorrection ) ).EndInit( );
            this.grpPRPAArraySize.ResumeLayout( false );
            this.grpPRPAArraySize.PerformLayout( );
            this.grpPRPATriggerCondition.ResumeLayout( false );
            this.grpPRPATriggerCondition.PerformLayout( );
            this.panel2.ResumeLayout( false );
            this.panel2.PerformLayout( );
            this.panelCreateNewProtocol.ResumeLayout( false );
            this.panelRenameProtocol.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ComboBox cbboxPRPRProtocolList;
        private System.Windows.Forms.Button btnPRPALoadProtocol;
        private System.Windows.Forms.Panel pnTriggerCount;
        private System.Windows.Forms.GroupBox grpPRPAProtocolParameters;
        private System.Windows.Forms.Panel panelCreateNewProtocol;
        private System.Windows.Forms.Button btnCreateNewProtocol;
        private System.Windows.Forms.GroupBox grpPRPATriggerCondition;
        private System.Windows.Forms.ComboBox cbboxArraySize;
        private System.Windows.Forms.Label lblPRPAArraySize;
        private System.Windows.Forms.Label lblPRPATriggerValue;
        private System.Windows.Forms.Label lblPRPATriggerType;
        private System.Windows.Forms.ComboBox cbboxPRPATriggerType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPRPATriggerValue;
        private System.Windows.Forms.PictureBox picBarPRPAEnableCorrection;
        private System.Windows.Forms.Label lblDisplayEnergyLine;
        private System.Windows.Forms.Label lblPRPAVbias;
        private System.Windows.Forms.ComboBox cmboxPRPAADCRange;
        private System.Windows.Forms.Label lblPRPAADCRange;
        private System.Windows.Forms.ComboBox cmboxPRPAADCTriggerThreshod;
        private System.Windows.Forms.Label lblPRPAADCTriggerThreshod;
        private System.Windows.Forms.ComboBox cmboxEncodingMode;
        private System.Windows.Forms.Label lblPRPAEncodingMode;
        private System.Windows.Forms.ComboBox cbboxPRPAIntegralTime;
        private System.Windows.Forms.Label lblPRRPAIntegralTime;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtPRPAVref;
        private System.Windows.Forms.Label lblPRPAVref;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtPRPAVbias;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpPRPAEncodingMode;
        private System.Windows.Forms.GroupBox grpPRPAIntegralTime;
        private System.Windows.Forms.GroupBox grpPRPAEnableCorrection;
        private System.Windows.Forms.GroupBox grpPRPAArraySize;
        private System.Windows.Forms.Panel panelRenameProtocol;
        private System.Windows.Forms.Button btnRenameProtocol;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtPRPYAxis;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtPRPXAxis;
        private System.Windows.Forms.Label lblPRPYAxis;
        private System.Windows.Forms.Label lblPRPXAxis;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblPRRPASourceType;
        private System.Windows.Forms.ComboBox cmboxPRPASourceType;
    }
}