namespace SISPE_MIGRACION.codigo.herramientas.forms.huella
{
    partial class frmEnrollar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.imagenDedo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imagenDedo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 409);
            this.panel1.TabIndex = 0;
            // 
            // imagenDedo
            // 
            this.imagenDedo.Location = new System.Drawing.Point(644, 12);
            this.imagenDedo.Name = "imagenDedo";
            this.imagenDedo.Size = new System.Drawing.Size(304, 385);
            this.imagenDedo.TabIndex = 1;
            this.imagenDedo.TabStop = false;
            // 
            // frmEnrollar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 409);
            this.Controls.Add(this.imagenDedo);
            this.Controls.Add(this.panel1);
            this.Name = "frmEnrollar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEnrollar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEnrollar_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEnrollar_FormClosed);
            this.Load += new System.EventHandler(this.frmEnrollar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imagenDedo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox imagenDedo;
    }
}