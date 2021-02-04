namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    partial class frmreposaldos
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btngenerar = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.fec1 = new System.Windows.Forms.MaskedTextBox();
            this.fec2 = new System.Windows.Forms.MaskedTextBox();
            this.chk_movimiento = new System.Windows.Forms.CheckBox();
            this.cmbmovimiento = new System.Windows.Forms.ComboBox();
            this.chkAnt = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(450, 161);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(9, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "DESDE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label2.Location = new System.Drawing.Point(9, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "HASTA:";
            // 
            // btngenerar
            // 
            this.btngenerar.BackColor = System.Drawing.SystemColors.Control;
            this.btngenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btngenerar.FlatAppearance.BorderSize = 0;
            this.btngenerar.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenerar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.btngenerar.Location = new System.Drawing.Point(274, 88);
            this.btngenerar.Name = "btngenerar";
            this.btngenerar.Size = new System.Drawing.Size(139, 30);
            this.btngenerar.TabIndex = 5;
            this.btngenerar.Text = "GENERAR";
            this.btngenerar.UseVisualStyleBackColor = false;
            this.btngenerar.Click += new System.EventHandler(this.btngenerar_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.label16);
            this.panel6.Controls.Add(this.button3);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(450, 38);
            this.panel6.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(3, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 26);
            this.label16.TabIndex = 3;
            this.label16.Text = "Reportes >";
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
            this.button3.Location = new System.Drawing.Point(336, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 32);
            this.button3.TabIndex = 1;
            this.button3.Text = "Cerrar   X";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label4.Location = new System.Drawing.Point(103, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 26);
            this.label4.TabIndex = 1;
            this.label4.Text = "Saldos";
            // 
            // fec1
            // 
            this.fec1.Location = new System.Drawing.Point(73, 57);
            this.fec1.Mask = "00/00/0000";
            this.fec1.Name = "fec1";
            this.fec1.Size = new System.Drawing.Size(195, 20);
            this.fec1.TabIndex = 1;
            this.fec1.Text = "01011922";
            this.fec1.ValidatingType = typeof(System.DateTime);
            this.fec1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fec1_KeyDown);
            this.fec1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fec1_MouseDown);
            // 
            // fec2
            // 
            this.fec2.Location = new System.Drawing.Point(73, 94);
            this.fec2.Mask = "00/00/0000";
            this.fec2.Name = "fec2";
            this.fec2.Size = new System.Drawing.Size(195, 20);
            this.fec2.TabIndex = 2;
            this.fec2.ValidatingType = typeof(System.DateTime);
            this.fec2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fec1_KeyDown);
            this.fec2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fec1_MouseDown);
            // 
            // chk_movimiento
            // 
            this.chk_movimiento.AutoSize = true;
            this.chk_movimiento.BackColor = System.Drawing.Color.White;
            this.chk_movimiento.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.chk_movimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.chk_movimiento.Location = new System.Drawing.Point(13, 126);
            this.chk_movimiento.Name = "chk_movimiento";
            this.chk_movimiento.Size = new System.Drawing.Size(139, 23);
            this.chk_movimiento.TabIndex = 21;
            this.chk_movimiento.Text = "Por movimiento";
            this.chk_movimiento.UseVisualStyleBackColor = false;
            this.chk_movimiento.CheckedChanged += new System.EventHandler(this.chk_movimiento_CheckedChanged);
            // 
            // cmbmovimiento
            // 
            this.cmbmovimiento.FormattingEnabled = true;
            this.cmbmovimiento.Items.AddRange(new object[] {
            "AD",
            "AN",
            "AR",
            "AP",
            "DP",
            "DF",
            "DC",
            "MJ",
            "MV"});
            this.cmbmovimiento.Location = new System.Drawing.Point(158, 128);
            this.cmbmovimiento.Name = "cmbmovimiento";
            this.cmbmovimiento.Size = new System.Drawing.Size(110, 21);
            this.cmbmovimiento.TabIndex = 22;
            this.cmbmovimiento.Visible = false;
            // 
            // chkAnt
            // 
            this.chkAnt.AutoSize = true;
            this.chkAnt.BackColor = System.Drawing.Color.White;
            this.chkAnt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.chkAnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.chkAnt.Location = new System.Drawing.Point(274, 55);
            this.chkAnt.Name = "chkAnt";
            this.chkAnt.Size = new System.Drawing.Size(133, 23);
            this.chkAnt.TabIndex = 23;
            this.chkAnt.Text = "Por antiguedad";
            this.chkAnt.UseVisualStyleBackColor = false;
            // 
            // frmreposaldos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 161);
            this.Controls.Add(this.chkAnt);
            this.Controls.Add(this.cmbmovimiento);
            this.Controls.Add(this.chk_movimiento);
            this.Controls.Add(this.fec2);
            this.Controls.Add(this.fec1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.btngenerar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmreposaldos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmreposaldos";
            this.Load += new System.EventHandler(this.frmreposaldos_Load);
            this.Shown += new System.EventHandler(this.frmreposaldos_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmreposaldos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btngenerar;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MaskedTextBox fec1;
        private System.Windows.Forms.MaskedTextBox fec2;
        private System.Windows.Forms.CheckBox chk_movimiento;
        private System.Windows.Forms.ComboBox cmbmovimiento;
        private System.Windows.Forms.CheckBox chkAnt;
    }
}