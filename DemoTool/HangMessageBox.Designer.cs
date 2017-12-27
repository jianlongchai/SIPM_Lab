namespace DemoTool {
    partial class HangMessageBox {
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
            this.txtMessageBox = new System.Windows.Forms.TextBox();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtMessageBox
            // 
            this.txtMessageBox.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtMessageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageBox.Location = new System.Drawing.Point(1, -3);
            this.txtMessageBox.Name = "txtMessageBox";
            this.txtMessageBox.Size = new System.Drawing.Size(447, 27);
            this.txtMessageBox.TabIndex = 0;
            this.txtMessageBox.Text = "Saving File...";
            // 
            // prgBar
            // 
            this.prgBar.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.prgBar.Location = new System.Drawing.Point(1, 30);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(447, 23);
            this.prgBar.TabIndex = 1;
            // 
            // HangMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ClientSize = new System.Drawing.Size(446, 49);
            this.Controls.Add(this.prgBar);
            this.Controls.Add(this.txtMessageBox);
            this.Name = "HangMessageBox";
            this.Text = "Warning!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HangMessageBox_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessageBox;
        private System.Windows.Forms.ProgressBar prgBar;

    }
}