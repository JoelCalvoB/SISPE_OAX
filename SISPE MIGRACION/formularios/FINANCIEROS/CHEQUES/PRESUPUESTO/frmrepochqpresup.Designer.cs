namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES.PRESUPUESTO
{
    partial class frmrepochqpresup
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
            this.btngenera = new System.Windows.Forms.Button();
            this.txtini = new System.Windows.Forms.TextBox();
            this.txtfinal = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtchequera = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btngenera
            // 
            this.btngenera.Location = new System.Drawing.Point(190, 278);
            this.btngenera.Name = "btngenera";
            this.btngenera.Size = new System.Drawing.Size(155, 39);
            this.btngenera.TabIndex = 0;
            this.btngenera.Text = "GENERAR";
            this.btngenera.UseVisualStyleBackColor = true;
            this.btngenera.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtini
            // 
            this.txtini.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtini.ForeColor = System.Drawing.Color.Red;
            this.txtini.Location = new System.Drawing.Point(286, 183);
            this.txtini.Name = "txtini";
            this.txtini.Size = new System.Drawing.Size(160, 24);
            this.txtini.TabIndex = 2;
            this.txtini.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtini_KeyDown);
            // 
            // txtfinal
            // 
            this.txtfinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfinal.ForeColor = System.Drawing.Color.Red;
            this.txtfinal.Location = new System.Drawing.Point(286, 235);
            this.txtfinal.Name = "txtfinal";
            this.txtfinal.Size = new System.Drawing.Size(160, 24);
            this.txtfinal.TabIndex = 3;
            this.txtfinal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtfinal_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SISPE_MIGRACION.Properties.Resources.logo_pensiones;
            this.pictureBox1.Location = new System.Drawing.Point(82, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(347, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "NÚMERO DE CHEQUE INICIAL :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "NÚMERO DE CHEQUE FINAL:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(174, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "CHEQUERA:";
            // 
            // txtchequera
            // 
            this.txtchequera.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchequera.ForeColor = System.Drawing.Color.Red;
            this.txtchequera.Location = new System.Drawing.Point(286, 136);
            this.txtchequera.Name = "txtchequera";
            this.txtchequera.Size = new System.Drawing.Size(160, 24);
            this.txtchequera.TabIndex = 8;
            this.txtchequera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtchequera_KeyDown);
            this.txtchequera.Leave += new System.EventHandler(this.txtchequera_Leave);
            // 
            // frmrepochqpresup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(506, 356);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtchequera);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtfinal);
            this.Controls.Add(this.txtini);
            this.Controls.Add(this.btngenera);
            this.Name = "frmrepochqpresup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTE";
            this.Load += new System.EventHandler(this.frmrepocheques_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btngenera;
        private System.Windows.Forms.TextBox txtini;
        private System.Windows.Forms.TextBox txtfinal;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtchequera;
    }
}