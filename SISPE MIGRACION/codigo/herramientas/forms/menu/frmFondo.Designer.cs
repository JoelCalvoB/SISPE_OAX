namespace SISPE_MIGRACION.codigo.herramientas.forms.menu
{
    partial class frmFondo
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

        

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmFondo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(614, 359);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmFondo";
            this.Opacity = 0.4D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmFondo";
            this.Load += new System.EventHandler(this.frmFondo_Load);
            this.Shown += new System.EventHandler(this.frmFondo_Shown);
            this.Click += new System.EventHandler(this.frmFondo_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFondo_KeyDown);
            this.ResumeLayout(false);

        }
    }
}