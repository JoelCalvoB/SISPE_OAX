namespace SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS
{
    partial class frmPerded
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
            this.txtClaves = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDescri = new System.Windows.Forms.TextBox();
            this.txtClavePART = new System.Windows.Forms.TextBox();
            this.txtPartida = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RbPer = new System.Windows.Forms.RadioButton();
            this.RbDed = new System.Windows.Forms.RadioButton();
            this.txtSat = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFolio = new System.Windows.Forms.Button();
            this.btnnext = new System.Windows.Forms.Button();
            this.btnback = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RbPrestamo = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtClaves
            // 
            this.txtClaves.Enabled = false;
            this.txtClaves.Location = new System.Drawing.Point(127, 20);
            this.txtClaves.Name = "txtClaves";
            this.txtClaves.Size = new System.Drawing.Size(58, 20);
            this.txtClaves.TabIndex = 0;
            this.txtClaves.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaves_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "CLAVE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "PARTIDA:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "DESCRIPCIÓN:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "CLAVE PARTIDA:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "CLAVE SAT:";
            // 
            // txtDescri
            // 
            this.txtDescri.Enabled = false;
            this.txtDescri.Location = new System.Drawing.Point(127, 48);
            this.txtDescri.Name = "txtDescri";
            this.txtDescri.Size = new System.Drawing.Size(348, 20);
            this.txtDescri.TabIndex = 1;
            // 
            // txtClavePART
            // 
            this.txtClavePART.Enabled = false;
            this.txtClavePART.Location = new System.Drawing.Point(127, 78);
            this.txtClavePART.Name = "txtClavePART";
            this.txtClavePART.Size = new System.Drawing.Size(199, 20);
            this.txtClavePART.TabIndex = 2;
            // 
            // txtPartida
            // 
            this.txtPartida.Enabled = false;
            this.txtPartida.Location = new System.Drawing.Point(127, 111);
            this.txtPartida.Name = "txtPartida";
            this.txtPartida.Size = new System.Drawing.Size(199, 20);
            this.txtPartida.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "PRESTAMO:";
            // 
            // RbPer
            // 
            this.RbPer.AutoSize = true;
            this.RbPer.Location = new System.Drawing.Point(6, 13);
            this.RbPer.Name = "RbPer";
            this.RbPer.Size = new System.Drawing.Size(94, 17);
            this.RbPer.TabIndex = 0;
            this.RbPer.TabStop = true;
            this.RbPer.Text = "PERCEPCIÓN";
            this.RbPer.UseVisualStyleBackColor = true;
            // 
            // RbDed
            // 
            this.RbDed.AutoSize = true;
            this.RbDed.Location = new System.Drawing.Point(117, 13);
            this.RbDed.Name = "RbDed";
            this.RbDed.Size = new System.Drawing.Size(89, 17);
            this.RbDed.TabIndex = 15;
            this.RbDed.TabStop = true;
            this.RbDed.Text = "DEDUCCIÓN";
            this.RbDed.UseVisualStyleBackColor = true;
            // 
            // txtSat
            // 
            this.txtSat.Enabled = false;
            this.txtSat.Location = new System.Drawing.Point(127, 211);
            this.txtSat.Name = "txtSat";
            this.txtSat.Size = new System.Drawing.Size(75, 20);
            this.txtSat.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(426, 178);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 49);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFolio
            // 
            this.btnFolio.BackColor = System.Drawing.Color.LightBlue;
            this.btnFolio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFolio.Location = new System.Drawing.Point(195, 18);
            this.btnFolio.Name = "btnFolio";
            this.btnFolio.Size = new System.Drawing.Size(29, 23);
            this.btnFolio.TabIndex = 22;
            this.btnFolio.Text = "/";
            this.btnFolio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFolio.UseVisualStyleBackColor = false;
            this.btnFolio.Click += new System.EventHandler(this.btnFolio_Click_1);
            // 
            // btnnext
            // 
            this.btnnext.Location = new System.Drawing.Point(472, 178);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(49, 49);
            this.btnnext.TabIndex = 23;
            this.btnnext.Text = ">";
            this.btnnext.UseVisualStyleBackColor = true;
            this.btnnext.Click += new System.EventHandler(this.btnnext_Click);
            // 
            // btnback
            // 
            this.btnback.Location = new System.Drawing.Point(381, 178);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(49, 49);
            this.btnback.TabIndex = 24;
            this.btnback.Text = "<";
            this.btnback.UseVisualStyleBackColor = true;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightBlue;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(240, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 25);
            this.button1.TabIndex = 25;
            this.button1.Text = "+";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RbPer);
            this.groupBox1.Controls.Add(this.RbDed);
            this.groupBox1.Location = new System.Drawing.Point(59, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 36);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo";
            // 
            // RbPrestamo
            // 
            this.RbPrestamo.AutoSize = true;
            this.RbPrestamo.Location = new System.Drawing.Point(130, 146);
            this.RbPrestamo.Name = "RbPrestamo";
            this.RbPrestamo.Size = new System.Drawing.Size(86, 17);
            this.RbPrestamo.TabIndex = 4;
            this.RbPrestamo.Text = "PRESTAMO";
            this.RbPrestamo.UseVisualStyleBackColor = true;
            // 
            // frmPerded
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 244);
            this.Controls.Add(this.RbPrestamo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.btnnext);
            this.Controls.Add(this.btnFolio);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSat);
            this.Controls.Add(this.txtPartida);
            this.Controls.Add(this.txtClavePART);
            this.Controls.Add(this.txtDescri);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClaves);
            this.KeyPreview = true;
            this.Name = "frmPerded";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo de Percepciones y Deducciones";
            this.Load += new System.EventHandler(this.frmPerDed_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPerded_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmPerded_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtClaves;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDescri;
        private System.Windows.Forms.TextBox txtClavePART;
        private System.Windows.Forms.TextBox txtPartida;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton RbPer;
        private System.Windows.Forms.RadioButton RbDed;
        private System.Windows.Forms.TextBox txtSat;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnFolio;
        private System.Windows.Forms.Button btnnext;
        private System.Windows.Forms.Button btnback;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox RbPrestamo;
    }
}