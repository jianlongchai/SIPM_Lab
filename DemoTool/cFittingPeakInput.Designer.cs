namespace DemoTool {
    partial class cFittingPeakInput {
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
            this.lblEnergyPeak = new System.Windows.Forms.Label( );
            this.txtPeakValue = new System.Windows.Forms.TextBox( );
            this.btnOK = new System.Windows.Forms.Button( );
            this.btnCancel = new System.Windows.Forms.Button( );
            this.SuspendLayout( );
            // 
            // lblEnergyPeak
            // 
            this.lblEnergyPeak.AutoSize = true;
            this.lblEnergyPeak.Font = new System.Drawing.Font( "Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.lblEnergyPeak.Location = new System.Drawing.Point( 27, 23 );
            this.lblEnergyPeak.Name = "lblEnergyPeak";
            this.lblEnergyPeak.Size = new System.Drawing.Size( 105, 17 );
            this.lblEnergyPeak.TabIndex = 0;
            this.lblEnergyPeak.Text = "Energy Peak:";
            // 
            // txtPeakValue
            // 
            this.txtPeakValue.Location = new System.Drawing.Point( 150, 18 );
            this.txtPeakValue.Name = "txtPeakValue";
            this.txtPeakValue.Size = new System.Drawing.Size( 120, 22 );
            this.txtPeakValue.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point( 30, 66 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 76, 33 );
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 165, 66 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 76, 33 );
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
            // 
            // cFittingPeakInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 8F, 16F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 325, 111 );
            this.Controls.Add( this.btnCancel );
            this.Controls.Add( this.btnOK );
            this.Controls.Add( this.txtPeakValue );
            this.Controls.Add( this.lblEnergyPeak );
            this.Name = "cFittingPeakInput";
            this.Text = "cFittingPeakInput";
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Label lblEnergyPeak;
        private System.Windows.Forms.TextBox txtPeakValue;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}