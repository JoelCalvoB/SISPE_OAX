namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES.PRESUPUESTO
{
    partial class frmaltapresupuesto
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
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.txtcuentacancel = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.data = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblstatus = new System.Windows.Forms.Label();
            this.btnbaja = new System.Windows.Forms.Button();
            this.letramonto = new System.Windows.Forms.Label();
            this.fec1 = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtpoliza = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtelaboro = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtconcepto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtmonto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbenefic = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtchequera = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbanco = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtnumchq = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabel2);
            this.panel1.Controls.Add(this.txtcuentacancel);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.data);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1056, 542);
            this.panel1.TabIndex = 0;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(511, 517);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(266, 16);
            this.linkLabel2.TabIndex = 5;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "[ F9 ] NUEVO / ACTUALIZA DETALLE";
            // 
            // txtcuentacancel
            // 
            this.txtcuentacancel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcuentacancel.Location = new System.Drawing.Point(294, 341);
            this.txtcuentacancel.Name = "txtcuentacancel";
            this.txtcuentacancel.Size = new System.Drawing.Size(549, 20);
            this.txtcuentacancel.TabIndex = 4;
            this.txtcuentacancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(77, 342);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(208, 16);
            this.label14.TabIndex = 3;
            this.label14.Text = "CUENTA DE CANCELACIÓN:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(22, 517);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(336, 16);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "[ F2 ] SALIR               [ F12 ]  LIMPIAR CAPTURA";
            // 
            // data
            // 
            this.data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.data.Location = new System.Drawing.Point(25, 356);
            this.data.MultiSelect = false;
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(1019, 145);
            this.data.TabIndex = 1;
            this.data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_CellClick);
            this.data.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_CellEndEdit);
            this.data.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_CellEnter);
            this.data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.data_KeyDown);
            this.data.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.data_KeyPress);
            this.data.KeyUp += new System.Windows.Forms.KeyEventHandler(this.data_KeyUp);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "CUENTA";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "CONCEPTO";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "REFERENCIA";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "DEBE";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "HABER";
            this.Column5.Name = "Column5";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.lblstatus);
            this.panel2.Controls.Add(this.btnbaja);
            this.panel2.Controls.Add(this.letramonto);
            this.panel2.Controls.Add(this.fec1);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtpoliza);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtelaboro);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtconcepto);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtmonto);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtbenefic);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtchequera);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtbanco);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtnumchq);
            this.panel2.Location = new System.Drawing.Point(25, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1019, 228);
            this.panel2.TabIndex = 0;
            // 
            // lblstatus
            // 
            this.lblstatus.AutoSize = true;
            this.lblstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstatus.ForeColor = System.Drawing.Color.Red;
            this.lblstatus.Location = new System.Drawing.Point(72, 37);
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(11, 13);
            this.lblstatus.TabIndex = 13;
            this.lblstatus.Text = "-";
            // 
            // btnbaja
            // 
            this.btnbaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnbaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnbaja.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnbaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbaja.ForeColor = System.Drawing.Color.White;
            this.btnbaja.Location = new System.Drawing.Point(188, 14);
            this.btnbaja.Name = "btnbaja";
            this.btnbaja.Size = new System.Drawing.Size(106, 20);
            this.btnbaja.TabIndex = 12;
            this.btnbaja.Text = "BAJA";
            this.btnbaja.UseVisualStyleBackColor = false;
            this.btnbaja.Click += new System.EventHandler(this.btnbaja_Click);
            // 
            // letramonto
            // 
            this.letramonto.AutoSize = true;
            this.letramonto.Location = new System.Drawing.Point(228, 126);
            this.letramonto.Name = "letramonto";
            this.letramonto.Size = new System.Drawing.Size(10, 13);
            this.letramonto.TabIndex = 0;
            this.letramonto.Text = "-";
            // 
            // fec1
            // 
            this.fec1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fec1.Location = new System.Drawing.Point(95, 60);
            this.fec1.Mask = "00/00/0000";
            this.fec1.Name = "fec1";
            this.fec1.Size = new System.Drawing.Size(137, 20);
            this.fec1.TabIndex = 4;
            this.fec1.ValidatingType = typeof(System.DateTime);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(869, 147);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 16);
            this.label13.TabIndex = 10;
            this.label13.Text = "FMPC";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(869, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 16);
            this.label12.TabIndex = 8;
            this.label12.Text = "NGGM";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(771, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "AUTORIZO:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(771, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "REVISÓ:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(771, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "PÓLIZA:";
            // 
            // txtpoliza
            // 
            this.txtpoliza.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtpoliza.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtpoliza.Location = new System.Drawing.Point(860, 169);
            this.txtpoliza.Name = "txtpoliza";
            this.txtpoliza.Size = new System.Drawing.Size(100, 20);
            this.txtpoliza.TabIndex = 11;
            this.txtpoliza.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpoliza_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(771, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "ELABORÓ:";
            // 
            // txtelaboro
            // 
            this.txtelaboro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtelaboro.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtelaboro.Location = new System.Drawing.Point(860, 93);
            this.txtelaboro.Name = "txtelaboro";
            this.txtelaboro.Size = new System.Drawing.Size(100, 20);
            this.txtelaboro.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "CONCEPTO:";
            // 
            // txtconcepto
            // 
            this.txtconcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtconcepto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtconcepto.Location = new System.Drawing.Point(128, 157);
            this.txtconcepto.Multiline = true;
            this.txtconcepto.Name = "txtconcepto";
            this.txtconcepto.Size = new System.Drawing.Size(335, 55);
            this.txtconcepto.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "MONTO:";
            // 
            // txtmonto
            // 
            this.txtmonto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtmonto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtmonto.Location = new System.Drawing.Point(101, 122);
            this.txtmonto.Name = "txtmonto";
            this.txtmonto.Size = new System.Drawing.Size(108, 20);
            this.txtmonto.TabIndex = 7;
            this.txtmonto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtmonto_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "A FAVOR DE:";
            // 
            // txtbenefic
            // 
            this.txtbenefic.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtbenefic.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtbenefic.Location = new System.Drawing.Point(147, 90);
            this.txtbenefic.Name = "txtbenefic";
            this.txtbenefic.Size = new System.Drawing.Size(559, 20);
            this.txtbenefic.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "FECHA:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(601, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "CHEQUERA:";
            // 
            // txtchequera
            // 
            this.txtchequera.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtchequera.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtchequera.Location = new System.Drawing.Point(702, 18);
            this.txtchequera.Name = "txtchequera";
            this.txtchequera.Size = new System.Drawing.Size(297, 20);
            this.txtchequera.TabIndex = 3;
            this.txtchequera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtchequera_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(310, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "BANCO:";
            // 
            // txtbanco
            // 
            this.txtbanco.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtbanco.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtbanco.Location = new System.Drawing.Point(386, 17);
            this.txtbanco.Name = "txtbanco";
            this.txtbanco.Size = new System.Drawing.Size(196, 20);
            this.txtbanco.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "N°-";
            // 
            // txtnumchq
            // 
            this.txtnumchq.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtnumchq.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnumchq.Location = new System.Drawing.Point(67, 14);
            this.txtnumchq.Name = "txtnumchq";
            this.txtnumchq.Size = new System.Drawing.Size(100, 20);
            this.txtnumchq.TabIndex = 1;
            this.txtnumchq.TextChanged += new System.EventHandler(this.txtnumchq_TextChanged);
            this.txtnumchq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnumchq_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SISPE_MIGRACION.Properties.Resources.logo_pensiones;
            this.pictureBox1.Location = new System.Drawing.Point(373, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(338, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmaltapresupuesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 542);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmaltapresupuesto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ALTAS Y BAJAS CHEQUES ";
            this.Load += new System.EventHandler(this.frmaltaschq_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmaltaschq_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmaltaschq_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtmonto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbenefic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtchequera;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbanco;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtnumchq;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtpoliza;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtelaboro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtconcepto;
        private System.Windows.Forms.MaskedTextBox fec1;
        private System.Windows.Forms.Label letramonto;
        private System.Windows.Forms.DataGridView data;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnbaja;
        private System.Windows.Forms.TextBox txtcuentacancel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label lblstatus;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}