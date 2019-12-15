namespace PharmacyManagement
{
    partial class BarcodeScanner
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarcodeScanner));
            this.barcodeCam = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barcodeCam)).BeginInit();
            this.SuspendLayout();
            // 
            // barcodeCam
            // 
            this.barcodeCam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barcodeCam.Location = new System.Drawing.Point(12, 16);
            this.barcodeCam.Name = "barcodeCam";
            this.barcodeCam.Size = new System.Drawing.Size(423, 338);
            this.barcodeCam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.barcodeCam.TabIndex = 0;
            this.barcodeCam.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BarcodeScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 364);
            this.Controls.Add(this.barcodeCam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BarcodeScanner";
            this.Text = "BarcodeScanner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BarcodeScanner_FormClosed);
            this.Load += new System.EventHandler(this.BarcodeScanner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barcodeCam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox barcodeCam;
        private System.Windows.Forms.Timer timer1;
    }
}