namespace DemoTool {
    partial class EnergyDiagram {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( EnergyDiagram ) );
            this.lblPixelNoValue = new System.Windows.Forms.Label( );
            this.lblPixelTotalEcentCounts = new System.Windows.Forms.Label( );
            this.lblPixelTotalEcentCountsValue = new System.Windows.Forms.Label( );
            this.lblPixelNoLabel = new System.Windows.Forms.Label( );
            this.btnCloseAll = new System.Windows.Forms.Button( );
            this.btnSetPeak = new System.Windows.Forms.Button( );
            this.lblEnergyResolution = new System.Windows.Forms.Label( );
            this.lblEnergyResolutionValue = new System.Windows.Forms.Label( );
            this.lblFittedPeak = new System.Windows.Forms.Label( );
            this.lblFittingPeakValue = new System.Windows.Forms.Label( );
            this.lblG2CenterError = new System.Windows.Forms.Label( );
            this.zGraph1 = new ZhengJuyin.UI.ZGraph( );
            this.SuspendLayout( );
            // 
            // lblPixelNoValue
            // 
            this.lblPixelNoValue.AutoSize = true;
            this.lblPixelNoValue.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPixelNoValue.Location = new System.Drawing.Point( 100, 9 );
            this.lblPixelNoValue.Name = "lblPixelNoValue";
            this.lblPixelNoValue.Size = new System.Drawing.Size( 60, 13 );
            this.lblPixelNoValue.TabIndex = 6;
            this.lblPixelNoValue.Text = "Unknown";
            // 
            // lblPixelTotalEcentCounts
            // 
            this.lblPixelTotalEcentCounts.AutoSize = true;
            this.lblPixelTotalEcentCounts.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPixelTotalEcentCounts.Location = new System.Drawing.Point( 204, 9 );
            this.lblPixelTotalEcentCounts.Name = "lblPixelTotalEcentCounts";
            this.lblPixelTotalEcentCounts.Size = new System.Drawing.Size( 87, 13 );
            this.lblPixelTotalEcentCounts.TabIndex = 8;
            this.lblPixelTotalEcentCounts.Text = "Event Counts:";
            // 
            // lblPixelTotalEcentCountsValue
            // 
            this.lblPixelTotalEcentCountsValue.AutoSize = true;
            this.lblPixelTotalEcentCountsValue.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPixelTotalEcentCountsValue.Location = new System.Drawing.Point( 319, 9 );
            this.lblPixelTotalEcentCountsValue.Name = "lblPixelTotalEcentCountsValue";
            this.lblPixelTotalEcentCountsValue.Size = new System.Drawing.Size( 14, 13 );
            this.lblPixelTotalEcentCountsValue.TabIndex = 9;
            this.lblPixelTotalEcentCountsValue.Text = "0";
            // 
            // lblPixelNoLabel
            // 
            this.lblPixelNoLabel.AutoSize = true;
            this.lblPixelNoLabel.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblPixelNoLabel.Location = new System.Drawing.Point( 12, 9 );
            this.lblPixelNoLabel.Name = "lblPixelNoLabel";
            this.lblPixelNoLabel.Size = new System.Drawing.Size( 58, 13 );
            this.lblPixelNoLabel.TabIndex = 10;
            this.lblPixelNoLabel.Text = "Pixel No:";
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.Location = new System.Drawing.Point( 373, 3 );
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size( 99, 33 );
            this.btnCloseAll.TabIndex = 11;
            this.btnCloseAll.Text = "Close All";
            this.btnCloseAll.UseVisualStyleBackColor = true;
            this.btnCloseAll.Click += new System.EventHandler( this.btnCloseAll_Click );
            // 
            // btnSetPeak
            // 
            this.btnSetPeak.Location = new System.Drawing.Point( 478, 3 );
            this.btnSetPeak.Name = "btnSetPeak";
            this.btnSetPeak.Size = new System.Drawing.Size( 92, 33 );
            this.btnSetPeak.TabIndex = 12;
            this.btnSetPeak.Text = "Set Peak";
            this.btnSetPeak.UseVisualStyleBackColor = true;
            this.btnSetPeak.Click += new System.EventHandler( this.btnFitting_Click );
            // 
            // lblEnergyResolution
            // 
            this.lblEnergyResolution.AutoSize = true;
            this.lblEnergyResolution.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblEnergyResolution.Location = new System.Drawing.Point( 231, 39 );
            this.lblEnergyResolution.Name = "lblEnergyResolution";
            this.lblEnergyResolution.Size = new System.Drawing.Size( 114, 13 );
            this.lblEnergyResolution.TabIndex = 13;
            this.lblEnergyResolution.Text = "Energy Resolution:";
            // 
            // lblEnergyResolutionValue
            // 
            this.lblEnergyResolutionValue.AutoSize = true;
            this.lblEnergyResolutionValue.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblEnergyResolutionValue.Location = new System.Drawing.Point( 361, 39 );
            this.lblEnergyResolutionValue.Name = "lblEnergyResolutionValue";
            this.lblEnergyResolutionValue.Size = new System.Drawing.Size( 60, 13 );
            this.lblEnergyResolutionValue.TabIndex = 14;
            this.lblEnergyResolutionValue.Text = "Unknown";
            // 
            // lblFittedPeak
            // 
            this.lblFittedPeak.AutoSize = true;
            this.lblFittedPeak.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblFittedPeak.Location = new System.Drawing.Point( 12, 39 );
            this.lblFittedPeak.Name = "lblFittedPeak";
            this.lblFittedPeak.Size = new System.Drawing.Size( 76, 13 );
            this.lblFittedPeak.TabIndex = 15;
            this.lblFittedPeak.Text = "Fitted Peak:";
            // 
            // lblFittingPeakValue
            // 
            this.lblFittingPeakValue.AutoSize = true;
            this.lblFittingPeakValue.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblFittingPeakValue.Location = new System.Drawing.Point( 113, 39 );
            this.lblFittingPeakValue.Name = "lblFittingPeakValue";
            this.lblFittingPeakValue.Size = new System.Drawing.Size( 60, 13 );
            this.lblFittingPeakValue.TabIndex = 16;
            this.lblFittingPeakValue.Text = "Unknown";
            // 
            // lblG2CenterError
            // 
            this.lblG2CenterError.AutoSize = true;
            this.lblG2CenterError.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblG2CenterError.Location = new System.Drawing.Point( 451, 39 );
            this.lblG2CenterError.Name = "lblG2CenterError";
            this.lblG2CenterError.Size = new System.Drawing.Size( 35, 13 );
            this.lblG2CenterError.TabIndex = 17;
            this.lblG2CenterError.Text = "+/- 0";
            // 
            // zGraph1
            // 
            this.zGraph1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.zGraph1.BackColor = System.Drawing.Color.LemonChiffon;
            this.zGraph1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zGraph1.Location = new System.Drawing.Point( -2, 76 );
            this.zGraph1.m_backColorH = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 251 ) ) ) ), ( ( int )( ( ( byte )( 208 ) ) ) ) );
            this.zGraph1.m_backColorL = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 251 ) ) ) ), ( ( int )( ( ( byte )( 208 ) ) ) ) );
            this.zGraph1.m_BigXYBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ) );
            this.zGraph1.m_BigXYButtonBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 200 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ) );
            this.zGraph1.m_BigXYButtonForeColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ) );
            this.zGraph1.m_ControlButtonBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ) );
            this.zGraph1.m_ControlButtonForeColorH = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 100 ) ) ) ), ( ( int )( ( ( byte )( 100 ) ) ) ), ( ( int )( ( ( byte )( 100 ) ) ) ) );
            this.zGraph1.m_ControlButtonForeColorL = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 250 ) ) ) ), ( ( int )( ( ( byte )( 250 ) ) ) ), ( ( int )( ( ( byte )( 250 ) ) ) ) );
            this.zGraph1.m_ControlItemBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ) );
            this.zGraph1.m_coordinateLineColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ) );
            this.zGraph1.m_coordinateStringColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ) );
            this.zGraph1.m_coordinateStringTitleColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ) );
            this.zGraph1.m_DirectionBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 32 ) ) ) ), ( ( int )( ( ( byte )( 32 ) ) ) ), ( ( int )( ( ( byte )( 32 ) ) ) ) );
            this.zGraph1.m_DirectionForeColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ) );
            this.zGraph1.m_fXBeginSYS = 0F;
            this.zGraph1.m_fXEndSYS = 2400F;
            this.zGraph1.m_fYBeginSYS = 0F;
            this.zGraph1.m_fYEndSYS = 100F;
            this.zGraph1.m_GraphBackColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ) );
            this.zGraph1.m_iLineShowColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ), ( ( int )( ( ( byte )( 255 ) ) ) ) );
            this.zGraph1.m_iLineShowColorAlpha = 100;
            this.zGraph1.m_SySnameX = "X(Energy)";
            this.zGraph1.m_SySnameY = "Y(Count)";
            this.zGraph1.m_SyStitle = "Energy Spectrum";
            this.zGraph1.m_titleBorderColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 250 ) ) ) ), ( ( int )( ( ( byte )( 250 ) ) ) ), ( ( int )( ( ( byte )( 250 ) ) ) ) );
            this.zGraph1.m_titleColor = System.Drawing.Color.FromArgb( ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ), ( ( int )( ( ( byte )( 0 ) ) ) ) );
            this.zGraph1.m_titlePosition = 0.4F;
            this.zGraph1.m_titleSize = 14;
            this.zGraph1.Margin = new System.Windows.Forms.Padding( 0 );
            this.zGraph1.MinimumSize = new System.Drawing.Size( 400, 300 );
            this.zGraph1.Name = "zGraph1";
            this.zGraph1.Size = new System.Drawing.Size( 588, 333 );
            this.zGraph1.TabIndex = 7;
            this.zGraph1.Load += new System.EventHandler( this.zGraph1_Load );
            // 
            // EnergyDiagram
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size( 582, 403 );
            this.Controls.Add( this.zGraph1 );
            this.Controls.Add( this.lblG2CenterError );
            this.Controls.Add( this.lblFittingPeakValue );
            this.Controls.Add( this.lblFittedPeak );
            this.Controls.Add( this.lblEnergyResolutionValue );
            this.Controls.Add( this.lblEnergyResolution );
            this.Controls.Add( this.btnSetPeak );
            this.Controls.Add( this.btnCloseAll );
            this.Controls.Add( this.lblPixelNoLabel );
            this.Controls.Add( this.lblPixelTotalEcentCountsValue );
            this.Controls.Add( this.lblPixelTotalEcentCounts );
            this.Controls.Add( this.lblPixelNoValue );
            this.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "$this.Icon" ) ) );
            this.Margin = new System.Windows.Forms.Padding( 4 );
            this.Name = "EnergyDiagram";
            this.Text = "Energy Spectrum";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.EnergyDiagram_FormClosing );
            this.Load += new System.EventHandler( this.EnergyDiagram_Load );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Label lblPixelNoValue;
        private System.Windows.Forms.Label lblPixelTotalEcentCounts;
        private System.Windows.Forms.Label lblPixelTotalEcentCountsValue;
        private System.Windows.Forms.Label lblPixelNoLabel;
        private System.Windows.Forms.Button btnCloseAll;
        private System.Windows.Forms.Button btnSetPeak;
        private System.Windows.Forms.Label lblEnergyResolution;
        private System.Windows.Forms.Label lblEnergyResolutionValue;
        private System.Windows.Forms.Label lblFittedPeak;
        private System.Windows.Forms.Label lblFittingPeakValue;
        private System.Windows.Forms.Label lblG2CenterError;
        private ZhengJuyin.UI.ZGraph zGraph1;
    }
}