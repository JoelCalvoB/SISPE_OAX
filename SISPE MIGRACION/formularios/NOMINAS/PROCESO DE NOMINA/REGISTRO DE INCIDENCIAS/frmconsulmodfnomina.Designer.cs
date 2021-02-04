namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS
{
    partial class frmconsulmodfnomina
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmconsulmodfnomina));
            this.rfclabel = new System.Windows.Forms.Label();
            this.txtrfc = new System.Windows.Forms.TextBox();
            this.txtnom = new System.Windows.Forms.TextBox();
            this.dataNom = new System.Windows.Forms.DataGridView();
            this.clav = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.N_Pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T_Pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipopago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Secuen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelcant = new System.Windows.Forms.Label();
            this.txtletraImport = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtaport = new System.Windows.Forms.TextBox();
            this.txtdedu = new System.Windows.Forms.TextBox();
            this.txtsueld = new System.Windows.Forms.TextBox();
            this.btnguardar = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnFolio = new System.Windows.Forms.Button();
            this.comboBoxnom = new System.Windows.Forms.ComboBox();
            this.txtnumemp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataNom)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // rfclabel
            // 
            this.rfclabel.AutoSize = true;
            this.rfclabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.rfclabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.rfclabel.Location = new System.Drawing.Point(5, 132);
            this.rfclabel.Name = "rfclabel";
            this.rfclabel.Size = new System.Drawing.Size(42, 18);
            this.rfclabel.TabIndex = 2;
            this.rfclabel.Text = "R.F.C:";
            // 
            // txtrfc
            // 
            this.txtrfc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrfc.Location = new System.Drawing.Point(54, 131);
            this.txtrfc.Name = "txtrfc";
            this.txtrfc.Size = new System.Drawing.Size(199, 20);
            this.txtrfc.TabIndex = 3;
            // 
            // txtnom
            // 
            this.txtnom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnom.Location = new System.Drawing.Point(383, 131);
            this.txtnom.Name = "txtnom";
            this.txtnom.Size = new System.Drawing.Size(402, 20);
            this.txtnom.TabIndex = 4;
            // 
            // dataNom
            // 
            this.dataNom.AllowUserToAddRows = false;
            this.dataNom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataNom.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataNom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataNom.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataNom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataNom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataNom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clav,
            this.descrip,
            this.Importe,
            this.N_Pago,
            this.T_Pago,
            this.id,
            this.tipopago,
            this.leyen,
            this.Secuen,
            this.Folio});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataNom.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataNom.Location = new System.Drawing.Point(11, 26);
            this.dataNom.Name = "dataNom";
            this.dataNom.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataNom.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataNom.Size = new System.Drawing.Size(792, 271);
            this.dataNom.TabIndex = 9;
            this.dataNom.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataNom_CellEndEdit);
            this.dataNom.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataNom_CellEnter);
            this.dataNom.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataNom.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataNom_EditingControlShowing);
            this.dataNom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataNom_KeyDown);
            // 
            // clav
            // 
            this.clav.FillWeight = 40F;
            this.clav.HeaderText = "Clave";
            this.clav.Name = "clav";
            // 
            // descrip
            // 
            this.descrip.HeaderText = "Descripción";
            this.descrip.Name = "descrip";
            this.descrip.ReadOnly = true;
            // 
            // Importe
            // 
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            // 
            // N_Pago
            // 
            this.N_Pago.FillWeight = 30F;
            this.N_Pago.HeaderText = "N_Pago";
            this.N_Pago.Name = "N_Pago";
            // 
            // T_Pago
            // 
            this.T_Pago.FillWeight = 30F;
            this.T_Pago.HeaderText = "T_Pago";
            this.T_Pago.Name = "T_Pago";
            // 
            // id
            // 
            this.id.HeaderText = "Column1";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // tipopago
            // 
            this.tipopago.FillWeight = 25F;
            this.tipopago.HeaderText = "Tipo Pago";
            this.tipopago.Name = "tipopago";
            // 
            // leyen
            // 
            this.leyen.FillWeight = 80.58376F;
            this.leyen.HeaderText = "Leyenda";
            this.leyen.Name = "leyen";
            // 
            // Secuen
            // 
            this.Secuen.HeaderText = "Secuencia";
            this.Secuen.Name = "Secuen";
            this.Secuen.Visible = false;
            // 
            // Folio
            // 
            this.Folio.FillWeight = 40F;
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            // 
            // labelcant
            // 
            this.labelcant.AutoSize = true;
            this.labelcant.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelcant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.labelcant.Location = new System.Drawing.Point(55, 520);
            this.labelcant.Name = "labelcant";
            this.labelcant.Size = new System.Drawing.Size(148, 18);
            this.labelcant.TabIndex = 12;
            this.labelcant.Text = "CANTIDAD CON LETRA:";
            // 
            // txtletraImport
            // 
            this.txtletraImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtletraImport.ForeColor = System.Drawing.Color.Red;
            this.txtletraImport.Location = new System.Drawing.Point(211, 517);
            this.txtletraImport.Name = "txtletraImport";
            this.txtletraImport.Size = new System.Drawing.Size(520, 20);
            this.txtletraImport.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(14, 480);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "PERCEPCIONES:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label2.Location = new System.Drawing.Point(271, 480);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "DEDUCCIONES:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label3.Location = new System.Drawing.Point(526, 484);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "SUELDO NETO:";
            // 
            // txtaport
            // 
            this.txtaport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtaport.ForeColor = System.Drawing.Color.Red;
            this.txtaport.Location = new System.Drawing.Point(127, 477);
            this.txtaport.Name = "txtaport";
            this.txtaport.Size = new System.Drawing.Size(100, 20);
            this.txtaport.TabIndex = 17;
            // 
            // txtdedu
            // 
            this.txtdedu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdedu.ForeColor = System.Drawing.Color.Red;
            this.txtdedu.Location = new System.Drawing.Point(377, 477);
            this.txtdedu.Name = "txtdedu";
            this.txtdedu.Size = new System.Drawing.Size(100, 20);
            this.txtdedu.TabIndex = 18;
            // 
            // txtsueld
            // 
            this.txtsueld.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsueld.ForeColor = System.Drawing.Color.Red;
            this.txtsueld.Location = new System.Drawing.Point(631, 481);
            this.txtsueld.Name = "txtsueld";
            this.txtsueld.Size = new System.Drawing.Size(100, 20);
            this.txtsueld.TabIndex = 19;
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.White;
            this.btnguardar.Image = ((System.Drawing.Image)(resources.GetObject("btnguardar.Image")));
            this.btnguardar.Location = new System.Drawing.Point(769, 2);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(34, 24);
            this.btnguardar.TabIndex = 23;
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // btnFolio
            // 
            this.btnFolio.BackColor = System.Drawing.Color.LightBlue;
            this.btnFolio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFolio.Image = ((System.Drawing.Image)(resources.GetObject("btnFolio.Image")));
            this.btnFolio.Location = new System.Drawing.Point(412, 62);
            this.btnFolio.Name = "btnFolio";
            this.btnFolio.Size = new System.Drawing.Size(29, 23);
            this.btnFolio.TabIndex = 101;
            this.btnFolio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFolio.UseVisualStyleBackColor = false;
            this.btnFolio.Click += new System.EventHandler(this.btnFolio_Click);
            // 
            // comboBoxnom
            // 
            this.comboBoxnom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxnom.FormattingEnabled = true;
            this.comboBoxnom.Items.AddRange(new object[] {
            "JUB",
            "PDO",
            "PEA",
            "PTA"});
            this.comboBoxnom.Location = new System.Drawing.Point(141, 64);
            this.comboBoxnom.Name = "comboBoxnom";
            this.comboBoxnom.Size = new System.Drawing.Size(76, 21);
            this.comboBoxnom.TabIndex = 98;
            // 
            // txtnumemp
            // 
            this.txtnumemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnumemp.Location = new System.Drawing.Point(364, 64);
            this.txtnumemp.Name = "txtnumemp";
            this.txtnumemp.Size = new System.Drawing.Size(42, 20);
            this.txtnumemp.TabIndex = 99;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label12.Location = new System.Drawing.Point(259, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 18);
            this.label12.TabIndex = 100;
            this.label12.Text = "No. Empleado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label4.Location = new System.Drawing.Point(34, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 18);
            this.label4.TabIndex = 97;
            this.label4.Text = "Tipo de Nomina:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label5.Location = new System.Drawing.Point(302, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 102;
            this.label5.Text = "NOMBRE:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.rfclabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtrfc);
            this.panel1.Controls.Add(this.btnFolio);
            this.panel1.Controls.Add(this.txtnom);
            this.panel1.Controls.Add(this.comboBoxnom);
            this.panel1.Controls.Add(this.labelcant);
            this.panel1.Controls.Add(this.txtnumemp);
            this.panel1.Controls.Add(this.txtletraImport);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtaport);
            this.panel1.Controls.Add(this.txtdedu);
            this.panel1.Controls.Add(this.txtsueld);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(838, 562);
            this.panel1.TabIndex = 103;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.radioButton7);
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.groupBox2.Location = new System.Drawing.Point(489, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 77);
            this.groupBox2.TabIndex = 105;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OPCIONES";
            this.groupBox2.Visible = false;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(21, 60);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(84, 17);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "CANASTA-2";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(111, 15);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(133, 17);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "DÍA DE LAS MADRES";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(111, 38);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(130, 17);
            this.radioButton4.TabIndex = 2;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "ÚTILES ESCOLARES";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(21, 38);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(75, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "CANASTA";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(21, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "AGUINALDO";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btnguardar);
            this.panel2.Controls.Add(this.dataNom);
            this.panel2.Location = new System.Drawing.Point(8, 161);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 310);
            this.panel2.TabIndex = 104;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label6.Location = new System.Drawing.Point(305, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 19);
            this.label6.TabIndex = 103;
            this.label6.Text = "DETALLE DE NÓMINA";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.button3);
            this.panel6.Controls.Add(this.label45);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(838, 38);
            this.panel6.TabIndex = 104;
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
            this.button3.Location = new System.Drawing.Point(646, 3);
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
            this.label45.Size = new System.Drawing.Size(335, 26);
            this.label45.TabIndex = 1;
            this.label45.Text = "Administrador Inteligente de Nómina";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.checkBox1.Location = new System.Drawing.Point(763, 67);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 22);
            this.checkBox1.TabIndex = 103;
            this.checkBox1.Text = "N.E";
            this.toolTip1.SetToolTip(this.checkBox1, "ACTIVAR SI DESEA VER LA NÓMINA ESPECIAL");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(111, 61);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(110, 17);
            this.radioButton7.TabIndex = 8;
            this.radioButton7.Text = "PRESUP-FONDO";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // frmconsulmodfnomina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(838, 562);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmconsulmodfnomina";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CAPTURA DE NOMINAS";
            this.Shown += new System.EventHandler(this.frmconsulmodfnomina_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmconsulmodfnomina_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmconsulmodfnomina_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dataNom)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label rfclabel;
        private System.Windows.Forms.TextBox txtrfc;
        private System.Windows.Forms.TextBox txtnom;
        private System.Windows.Forms.DataGridView dataNom;
        private System.Windows.Forms.Label labelcant;
        private System.Windows.Forms.TextBox txtletraImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtaport;
        private System.Windows.Forms.TextBox txtdedu;
        private System.Windows.Forms.TextBox txtsueld;
        private System.Windows.Forms.Button btnguardar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnFolio;
        private System.Windows.Forms.ComboBox comboBoxnom;
        private System.Windows.Forms.TextBox txtnumemp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.DataGridViewTextBoxColumn clav;
        private System.Windows.Forms.DataGridViewTextBoxColumn descrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn N_Pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn T_Pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipopago;
        private System.Windows.Forms.DataGridViewTextBoxColumn leyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Secuen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.RadioButton radioButton7;
    }
}