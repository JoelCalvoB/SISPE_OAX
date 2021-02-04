namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.NOMINAS_ESPECIALES
{
    partial class frmMasivoClave
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
            this.comboJpp = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checdelete = new System.Windows.Forms.CheckBox();
            this.checInsert = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.maskedMonto = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checEspecial = new System.Windows.Forms.CheckBox();
            this.comboNom = new System.Windows.Forms.ComboBox();
            this.checNormal = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checupdate = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(28, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "CLAVE";
            // 
            // comboJpp
            // 
            this.comboJpp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboJpp.FormattingEnabled = true;
            this.comboJpp.Items.AddRange(new object[] {
            "JUB",
            "PDO",
            "PTA",
            "PEA"});
            this.comboJpp.Location = new System.Drawing.Point(91, 98);
            this.comboJpp.Name = "comboJpp";
            this.comboJpp.Size = new System.Drawing.Size(121, 21);
            this.comboJpp.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label2.Location = new System.Drawing.Point(19, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "TIPO JPP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label3.Location = new System.Drawing.Point(24, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "MONTO";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.button1.Location = new System.Drawing.Point(22, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "EJECUTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checdelete
            // 
            this.checdelete.AutoSize = true;
            this.checdelete.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.checdelete.Location = new System.Drawing.Point(6, 76);
            this.checdelete.Name = "checdelete";
            this.checdelete.Size = new System.Drawing.Size(88, 22);
            this.checdelete.TabIndex = 9;
            this.checdelete.Text = "ELIMINAR";
            this.checdelete.UseVisualStyleBackColor = true;
            this.checdelete.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checdelete.Click += new System.EventHandler(this.checdelete_Click);
            // 
            // checInsert
            // 
            this.checInsert.AutoSize = true;
            this.checInsert.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.checInsert.Location = new System.Drawing.Point(6, 19);
            this.checInsert.Name = "checInsert";
            this.checInsert.Size = new System.Drawing.Size(86, 22);
            this.checInsert.TabIndex = 10;
            this.checInsert.Text = "INSERTAR";
            this.checInsert.UseVisualStyleBackColor = true;
            this.checInsert.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            this.checInsert.Click += new System.EventHandler(this.checkBox2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.maskedMonto);
            this.panel1.Controls.Add(this.txtClave);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboJpp);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 282);
            this.panel1.TabIndex = 11;
            // 
            // maskedMonto
            // 
            this.maskedMonto.Location = new System.Drawing.Point(91, 136);
            this.maskedMonto.Name = "maskedMonto";
            this.maskedMonto.Size = new System.Drawing.Size(121, 20);
            this.maskedMonto.TabIndex = 111;
            this.maskedMonto.Leave += new System.EventHandler(this.maskedMonto_Leave);
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(91, 62);
            this.txtClave.Mask = "999";
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(45, 20);
            this.txtClave.TabIndex = 0;
            this.txtClave.ValidatingType = typeof(int);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.maskedTextBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.checEspecial);
            this.groupBox2.Controls.Add(this.comboNom);
            this.groupBox2.Controls.Add(this.checNormal);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.groupBox2.Location = new System.Drawing.Point(183, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 103);
            this.groupBox2.TabIndex = 110;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UBICACIÓN";
            // 
            // checEspecial
            // 
            this.checEspecial.AutoSize = true;
            this.checEspecial.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.checEspecial.Location = new System.Drawing.Point(6, 47);
            this.checEspecial.Name = "checEspecial";
            this.checEspecial.Size = new System.Drawing.Size(83, 22);
            this.checEspecial.TabIndex = 11;
            this.checEspecial.Text = "ESPECIAL";
            this.checEspecial.UseVisualStyleBackColor = true;
            this.checEspecial.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            this.checEspecial.Click += new System.EventHandler(this.checEspecial_Click);
            // 
            // comboNom
            // 
            this.comboNom.FormattingEnabled = true;
            this.comboNom.Items.AddRange(new object[] {
            "AGUINALDO",
            "CANASTA",
            "DIA DE LAS MADRES",
            "UTILES",
            "CANASTA -2",
            "NUEVA"});
            this.comboNom.Location = new System.Drawing.Point(93, 47);
            this.comboNom.Name = "comboNom";
            this.comboNom.Size = new System.Drawing.Size(121, 21);
            this.comboNom.TabIndex = 10;
            this.comboNom.SelectedIndexChanged += new System.EventHandler(this.comboNom_SelectedIndexChanged);
            // 
            // checNormal
            // 
            this.checNormal.AutoSize = true;
            this.checNormal.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.checNormal.Location = new System.Drawing.Point(6, 19);
            this.checNormal.Name = "checNormal";
            this.checNormal.Size = new System.Drawing.Size(83, 22);
            this.checNormal.TabIndex = 9;
            this.checNormal.Text = "NORMAL";
            this.checNormal.UseVisualStyleBackColor = true;
            this.checNormal.Click += new System.EventHandler(this.checkBox4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checupdate);
            this.groupBox1.Controls.Add(this.checInsert);
            this.groupBox1.Controls.Add(this.checdelete);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.groupBox1.Location = new System.Drawing.Point(283, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 107);
            this.groupBox1.TabIndex = 109;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OPCIONES";
            // 
            // checupdate
            // 
            this.checupdate.AutoSize = true;
            this.checupdate.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.checupdate.Location = new System.Drawing.Point(6, 48);
            this.checupdate.Name = "checupdate";
            this.checupdate.Size = new System.Drawing.Size(104, 22);
            this.checupdate.TabIndex = 11;
            this.checupdate.Text = "ACTUALIZAR";
            this.checupdate.UseVisualStyleBackColor = true;
            this.checupdate.Click += new System.EventHandler(this.checupdate_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.button3);
            this.panel6.Controls.Add(this.label45);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(419, 38);
            this.panel6.TabIndex = 106;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(36)))), ((int)(((byte)(0)))));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.button3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Gainsboro;
            this.button3.Location = new System.Drawing.Point(300, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "Cerrar   X";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label45.Location = new System.Drawing.Point(3, 6);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(269, 26);
            this.label45.TabIndex = 1;
            this.label45.Text = "Herramienta> Actualizaciones";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label4.Location = new System.Drawing.Point(26, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "IDENTIFICADOR";
            this.label4.Visible = false;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(136, 77);
            this.maskedTextBox1.Mask = "###";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(74, 20);
            this.maskedTextBox1.TabIndex = 14;
            // 
            // frmMasivoClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 285);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMasivoClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACTUALIZACIÓN MASIVA";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboJpp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checdelete;
        private System.Windows.Forms.CheckBox checInsert;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checEspecial;
        private System.Windows.Forms.ComboBox comboNom;
        private System.Windows.Forms.CheckBox checNormal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox txtClave;
        private System.Windows.Forms.CheckBox checupdate;
        private System.Windows.Forms.TextBox maskedMonto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
    }
}