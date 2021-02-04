using System;

namespace SISPE_MIGRACION.formularios.CONSTANCIAS.REGISTRO
{
    partial class frmmodificar2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmmodificar2));
            this.panel4 = new System.Windows.Forms.Panel();
            this.fecha2 = new System.Windows.Forms.MaskedTextBox();
            this.fecha1 = new System.Windows.Forms.MaskedTextBox();
            this.txtfecha = new System.Windows.Forms.MaskedTextBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.txtfolio = new System.Windows.Forms.TextBox();
            this.txtrfc = new System.Windows.Forms.TextBox();
            this.txtprestamos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.btnRfc1 = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.fecha2);
            this.panel4.Controls.Add(this.fecha1);
            this.panel4.Controls.Add(this.txtfecha);
            this.panel4.Controls.Add(this.cmbTipo);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.txtnombre);
            this.panel4.Controls.Add(this.txtfolio);
            this.panel4.Controls.Add(this.txtrfc);
            this.panel4.Controls.Add(this.txtprestamos);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label32);
            this.panel4.Controls.Add(this.label48);
            this.panel4.Controls.Add(this.btnRfc1);
            this.panel4.Controls.Add(this.label34);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(14, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(934, 173);
            this.panel4.TabIndex = 23;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // fecha2
            // 
            this.fecha2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fecha2.Font = new System.Drawing.Font("Calibri", 9.7F, System.Drawing.FontStyle.Bold);
            this.fecha2.Location = new System.Drawing.Point(567, 84);
            this.fecha2.Mask = "00/00/0000";
            this.fecha2.Name = "fecha2";
            this.fecha2.Size = new System.Drawing.Size(100, 16);
            this.fecha2.TabIndex = 64;
            this.fecha2.ValidatingType = typeof(System.DateTime);
            // 
            // fecha1
            // 
            this.fecha1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fecha1.Font = new System.Drawing.Font("Calibri", 9.7F, System.Drawing.FontStyle.Bold);
            this.fecha1.Location = new System.Drawing.Point(566, 55);
            this.fecha1.Mask = "00/00/0000";
            this.fecha1.Name = "fecha1";
            this.fecha1.Size = new System.Drawing.Size(100, 16);
            this.fecha1.TabIndex = 63;
            this.fecha1.ValidatingType = typeof(System.DateTime);
            this.fecha1.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.fecha1_MaskInputRejected);
            // 
            // txtfecha
            // 
            this.txtfecha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtfecha.Font = new System.Drawing.Font("Calibri", 9.7F, System.Drawing.FontStyle.Bold);
            this.txtfecha.Location = new System.Drawing.Point(716, 17);
            this.txtfecha.Mask = "00/00/0000";
            this.txtfecha.Name = "txtfecha";
            this.txtfecha.Size = new System.Drawing.Size(100, 16);
            this.txtfecha.TabIndex = 62;
            this.txtfecha.ValidatingType = typeof(System.DateTime);
            this.txtfecha.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtfecha_MaskInputRejected);
            // 
            // cmbTipo
            // 
            this.cmbTipo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTipo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "NO ADEUDO",
            "ADEUDO",
            "CON PAGOS"});
            this.cmbTipo.Location = new System.Drawing.Point(331, 19);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(121, 23);
            this.cmbTipo.TabIndex = 61;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label6.Location = new System.Drawing.Point(658, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 18);
            this.label6.TabIndex = 59;
            this.label6.Text = "FECHA:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label4.Location = new System.Drawing.Point(509, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 18);
            this.label4.TabIndex = 57;
            this.label4.Text = "HASTA:";
            // 
            // txtnombre
            // 
            this.txtnombre.BackColor = System.Drawing.Color.White;
            this.txtnombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtnombre.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtnombre.Enabled = false;
            this.txtnombre.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnombre.ForeColor = System.Drawing.Color.Black;
            this.txtnombre.Location = new System.Drawing.Point(99, 83);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(304, 16);
            this.txtnombre.TabIndex = 54;
            // 
            // txtfolio
            // 
            this.txtfolio.BackColor = System.Drawing.Color.White;
            this.txtfolio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtfolio.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtfolio.Enabled = false;
            this.txtfolio.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfolio.ForeColor = System.Drawing.Color.Black;
            this.txtfolio.Location = new System.Drawing.Point(99, 23);
            this.txtfolio.Name = "txtfolio";
            this.txtfolio.Size = new System.Drawing.Size(112, 16);
            this.txtfolio.TabIndex = 53;
            this.txtfolio.TextChanged += new System.EventHandler(this.txtfolio_TextChanged);
            // 
            // txtrfc
            // 
            this.txtrfc.BackColor = System.Drawing.Color.White;
            this.txtrfc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtrfc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtrfc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrfc.ForeColor = System.Drawing.Color.Black;
            this.txtrfc.Location = new System.Drawing.Point(99, 59);
            this.txtrfc.Name = "txtrfc";
            this.txtrfc.Size = new System.Drawing.Size(147, 16);
            this.txtrfc.TabIndex = 52;
            this.txtrfc.TextChanged += new System.EventHandler(this.txtrfc_TextChanged);
            // 
            // txtprestamos
            // 
            this.txtprestamos.BackColor = System.Drawing.Color.White;
            this.txtprestamos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtprestamos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtprestamos.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtprestamos.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprestamos.ForeColor = System.Drawing.Color.Black;
            this.txtprestamos.Location = new System.Drawing.Point(181, 114);
            this.txtprestamos.Name = "txtprestamos";
            this.txtprestamos.Size = new System.Drawing.Size(535, 16);
            this.txtprestamos.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label3.Location = new System.Drawing.Point(288, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 18);
            this.label3.TabIndex = 49;
            this.label3.Text = "TIPO";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label32.Location = new System.Drawing.Point(28, 81);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(64, 18);
            this.label32.TabIndex = 47;
            this.label32.Text = "NOMBRE";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label48.Location = new System.Drawing.Point(509, 53);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(51, 18);
            this.label48.TabIndex = 45;
            this.label48.Text = "DESDE:";
            // 
            // btnRfc1
            // 
            this.btnRfc1.BackColor = System.Drawing.Color.LightBlue;
            this.btnRfc1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRfc1.ForeColor = System.Drawing.Color.LightBlue;
            this.btnRfc1.Image = ((System.Drawing.Image)(resources.GetObject("btnRfc1.Image")));
            this.btnRfc1.Location = new System.Drawing.Point(252, 55);
            this.btnRfc1.Name = "btnRfc1";
            this.btnRfc1.Size = new System.Drawing.Size(29, 23);
            this.btnRfc1.TabIndex = 43;
            this.btnRfc1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRfc1.UseVisualStyleBackColor = false;
            this.btnRfc1.Click += new System.EventHandler(this.btnRfc1_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label34.Location = new System.Drawing.Point(61, 57);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(31, 18);
            this.label34.TabIndex = 41;
            this.label34.Text = "RFC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(-2, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 40;
            this.label1.Text = "SOLICITUD";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label5.Location = new System.Drawing.Point(28, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 18);
            this.label5.TabIndex = 29;
            this.label5.Text = "PRESTAMOS QUE DEBE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label2.Location = new System.Drawing.Point(52, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "FOLIO";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.button1);
            this.panel6.Controls.Add(this.button2);
            this.panel6.Controls.Add(this.label45);
            this.panel6.Controls.Add(this.label47);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(963, 38);
            this.panel6.TabIndex = 24;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(36)))), ((int)(((byte)(0)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.button1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(857, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 32);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cerrar   X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(36)))), ((int)(((byte)(0)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.button2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.button2.Location = new System.Drawing.Point(1200, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cerrar   X";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label45.Location = new System.Drawing.Point(221, 8);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(123, 26);
            this.label45.TabIndex = 1;
            this.label45.Text = "Actualizando";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.Black;
            this.label47.Location = new System.Drawing.Point(3, 9);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(219, 26);
            this.label47.TabIndex = 0;
            this.label47.Text = "Constancias > Registro >";
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(789, 229);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(159, 34);
            this.btnAceptar.TabIndex = 69;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.aceptar);
            // 
            // frmmodificar2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(963, 273);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmmodificar2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmmodificar2";
            this.Load += new System.EventHandler(this.frmmodifica2_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

       

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.TextBox txtfolio;
        private System.Windows.Forms.TextBox txtrfc;
        private System.Windows.Forms.TextBox txtprestamos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Button btnRfc1;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.MaskedTextBox txtfecha;
        private System.Windows.Forms.MaskedTextBox fecha2;
        private System.Windows.Forms.MaskedTextBox fecha1;
    }
}