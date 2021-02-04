namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    partial class frmFirmas
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblobsev = new System.Windows.Forms.Label();
            this.lblinic = new System.Windows.Forms.Label();
            this.txtobserv = new System.Windows.Forms.TextBox();
            this.txtinic = new System.Windows.Forms.TextBox();
            this.lbldesc = new System.Windows.Forms.Label();
            this.lblnomb = new System.Windows.Forms.Label();
            this.lblclave = new System.Windows.Forms.Label();
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.txtclave = new System.Windows.Forms.TextBox();
            this.dbfirmas = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbfirmas)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1322, 710);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.dbfirmas);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1322, 672);
            this.panel3.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.lblobsev);
            this.groupBox2.Controls.Add(this.lblinic);
            this.groupBox2.Controls.Add(this.txtobserv);
            this.groupBox2.Controls.Add(this.txtinic);
            this.groupBox2.Controls.Add(this.lbldesc);
            this.groupBox2.Controls.Add(this.lblnomb);
            this.groupBox2.Controls.Add(this.lblclave);
            this.groupBox2.Controls.Add(this.txtdesc);
            this.groupBox2.Controls.Add(this.txtnombre);
            this.groupBox2.Controls.Add(this.txtclave);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1144, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 672);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OPCIONES";
            // 
            // lblobsev
            // 
            this.lblobsev.AutoSize = true;
            this.lblobsev.Enabled = false;
            this.lblobsev.Location = new System.Drawing.Point(49, 331);
            this.lblobsev.Name = "lblobsev";
            this.lblobsev.Size = new System.Drawing.Size(91, 13);
            this.lblobsev.TabIndex = 20;
            this.lblobsev.Text = "Observaciones";
            // 
            // lblinic
            // 
            this.lblinic.AutoSize = true;
            this.lblinic.Enabled = false;
            this.lblinic.Location = new System.Drawing.Point(61, 273);
            this.lblinic.Name = "lblinic";
            this.lblinic.Size = new System.Drawing.Size(54, 13);
            this.lblinic.TabIndex = 19;
            this.lblinic.Text = "Iniciales";
            // 
            // txtobserv
            // 
            this.txtobserv.Enabled = false;
            this.txtobserv.Location = new System.Drawing.Point(2, 347);
            this.txtobserv.Name = "txtobserv";
            this.txtobserv.Size = new System.Drawing.Size(175, 20);
            this.txtobserv.TabIndex = 18;
            this.txtobserv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtobserv_KeyDown);
            // 
            // txtinic
            // 
            this.txtinic.Enabled = false;
            this.txtinic.Location = new System.Drawing.Point(2, 289);
            this.txtinic.Name = "txtinic";
            this.txtinic.Size = new System.Drawing.Size(175, 20);
            this.txtinic.TabIndex = 17;
            this.txtinic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtinic_KeyDown);
            // 
            // lbldesc
            // 
            this.lbldesc.AutoSize = true;
            this.lbldesc.Enabled = false;
            this.lbldesc.Location = new System.Drawing.Point(49, 216);
            this.lbldesc.Name = "lbldesc";
            this.lbldesc.Size = new System.Drawing.Size(74, 13);
            this.lbldesc.TabIndex = 16;
            this.lbldesc.Text = "Descripción";
            // 
            // lblnomb
            // 
            this.lblnomb.AutoSize = true;
            this.lblnomb.Enabled = false;
            this.lblnomb.Location = new System.Drawing.Point(61, 158);
            this.lblnomb.Name = "lblnomb";
            this.lblnomb.Size = new System.Drawing.Size(50, 13);
            this.lblnomb.TabIndex = 15;
            this.lblnomb.Text = "Nombre";
            // 
            // lblclave
            // 
            this.lblclave.AutoSize = true;
            this.lblclave.Enabled = false;
            this.lblclave.Location = new System.Drawing.Point(61, 108);
            this.lblclave.Name = "lblclave";
            this.lblclave.Size = new System.Drawing.Size(39, 13);
            this.lblclave.TabIndex = 14;
            this.lblclave.Text = "Clave";
            // 
            // txtdesc
            // 
            this.txtdesc.Enabled = false;
            this.txtdesc.Location = new System.Drawing.Point(2, 232);
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(175, 20);
            this.txtdesc.TabIndex = 13;
            this.txtdesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtdesc_KeyDown);
            // 
            // txtnombre
            // 
            this.txtnombre.Enabled = false;
            this.txtnombre.Location = new System.Drawing.Point(2, 174);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(175, 20);
            this.txtnombre.TabIndex = 12;
            this.txtnombre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnombre_KeyDown);
            // 
            // txtclave
            // 
            this.txtclave.Enabled = false;
            this.txtclave.Location = new System.Drawing.Point(2, 124);
            this.txtclave.Name = "txtclave";
            this.txtclave.Size = new System.Drawing.Size(175, 20);
            this.txtclave.TabIndex = 11;
            this.txtclave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtclave_KeyDown);
            // 
            // dbfirmas
            // 
            this.dbfirmas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dbfirmas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dbfirmas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbfirmas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dbfirmas.Location = new System.Drawing.Point(35, 39);
            this.dbfirmas.MultiSelect = false;
            this.dbfirmas.Name = "dbfirmas";
            this.dbfirmas.Size = new System.Drawing.Size(1031, 511);
            this.dbfirmas.TabIndex = 6;
            this.dbfirmas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfirmas_CellClick);
            this.dbfirmas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfirmas_CellContentClick);
            this.dbfirmas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfirmas_CellEndEdit);
            this.dbfirmas.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfirmas_CellValueChanged);
            this.dbfirmas.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbfirmas_RowEnter);
            this.dbfirmas.RowErrorTextChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.dbfirmas_RowErrorTextChanged);
            this.dbfirmas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dbfirmas_KeyDown);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Clave";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nombre";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Descripción P/ Firma";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Iniciales";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Observaciones";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "orden";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1322, 38);
            this.panel2.TabIndex = 6;
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
            this.button2.Location = new System.Drawing.Point(1216, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cerrar   X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(166, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 26);
            this.label3.TabIndex = 1;
            this.label3.Text = "Firmas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Administración >";
            // 
            // frmFirmas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 750);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmFirmas";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Firmas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFirmas_FormClosing);
            this.Load += new System.EventHandler(this.frmFirmas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFirmas_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbfirmas)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblobsev;
        private System.Windows.Forms.Label lblinic;
        private System.Windows.Forms.TextBox txtobserv;
        private System.Windows.Forms.TextBox txtinic;
        private System.Windows.Forms.Label lbldesc;
        private System.Windows.Forms.Label lblnomb;
        private System.Windows.Forms.Label lblclave;
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.TextBox txtclave;
        private System.Windows.Forms.DataGridView dbfirmas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}