namespace SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS
{
    partial class frmFirmasNomina
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtElaboro = new System.Windows.Forms.TextBox();
            this.txtReviso = new System.Windows.Forms.TextBox();
            this.txtAutorizo = new System.Windows.Forms.TextBox();
            this.txtRecursos = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(212, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "CATÁLOGO DE FIRMAS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ELABORÓ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(74, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "REVISÓ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(74, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "AUTORIZÓ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(74, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "RECURSOS HUMANOS";
            // 
            // txtElaboro
            // 
            this.txtElaboro.Location = new System.Drawing.Point(251, 96);
            this.txtElaboro.Name = "txtElaboro";
            this.txtElaboro.Size = new System.Drawing.Size(364, 20);
            this.txtElaboro.TabIndex = 5;
            this.txtElaboro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtElaboro_KeyDown);
            // 
            // txtReviso
            // 
            this.txtReviso.Location = new System.Drawing.Point(251, 140);
            this.txtReviso.Name = "txtReviso";
            this.txtReviso.Size = new System.Drawing.Size(364, 20);
            this.txtReviso.TabIndex = 6;
            // 
            // txtAutorizo
            // 
            this.txtAutorizo.Location = new System.Drawing.Point(251, 184);
            this.txtAutorizo.Name = "txtAutorizo";
            this.txtAutorizo.Size = new System.Drawing.Size(364, 20);
            this.txtAutorizo.TabIndex = 7;
            // 
            // txtRecursos
            // 
            this.txtRecursos.Location = new System.Drawing.Point(251, 226);
            this.txtRecursos.Name = "txtRecursos";
            this.txtRecursos.Size = new System.Drawing.Size(364, 20);
            this.txtRecursos.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(306, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmFirmasNomina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 321);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRecursos);
            this.Controls.Add(this.txtAutorizo);
            this.Controls.Add(this.txtReviso);
            this.Controls.Add(this.txtElaboro);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmFirmasNomina";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIRMAS";
            this.Load += new System.EventHandler(this.frmFirmas_Load);
            this.Shown += new System.EventHandler(this.frmFirmas_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtElaboro;
        private System.Windows.Forms.TextBox txtReviso;
        private System.Windows.Forms.TextBox txtAutorizo;
        private System.Windows.Forms.TextBox txtRecursos;
        private System.Windows.Forms.Button button1;
    }
}