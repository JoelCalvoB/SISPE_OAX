namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA
{
    partial class frmestados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmestados));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dtggrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PAGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.group = new System.Windows.Forms.GroupBox();
            this.btbuscar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.fec_falle = new System.Windows.Forms.DateTimePicker();
            this.checFallecio = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtcheque = new System.Windows.Forms.MaskedTextBox();
            this.txttotal = new System.Windows.Forms.TextBox();
            this.txtpagocuenta = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtplazo = new System.Windows.Forms.TextBox();
            this.txtubicacion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtimporte = new System.Windows.Forms.TextBox();
            this.txtfechasolicitud = new System.Windows.Forms.TextBox();
            this.txtpago = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtdirec = new System.Windows.Forms.TextBox();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.txtsecretaria = new System.Windows.Forms.TextBox();
            this.txtproyecto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtrfc = new System.Windows.Forms.TextBox();
            this.txtfolio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtggrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.group.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.group);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1322, 570);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.dtggrid);
            this.panel6.Location = new System.Drawing.Point(17, 295);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1020, 253);
            this.panel6.TabIndex = 16;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // dtggrid
            // 
            this.dtggrid.AllowUserToAddRows = false;
            this.dtggrid.AllowUserToResizeColumns = false;
            this.dtggrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.dtggrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtggrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtggrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dtggrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dtggrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtggrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtggrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtggrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtggrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.PAGO,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dtggrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtggrid.EnableHeadersVisualStyles = false;
            this.dtggrid.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dtggrid.Location = new System.Drawing.Point(0, 0);
            this.dtggrid.MultiSelect = false;
            this.dtggrid.Name = "dtggrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtggrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtggrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.dtggrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtggrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtggrid.Size = new System.Drawing.Size(1018, 251);
            this.dtggrid.TabIndex = 0;
            this.dtggrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtggrid_CellEndEdit);
            this.dtggrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtggrid_CellEnter);
            this.dtggrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtggrid_CellValueChanged);
            this.dtggrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtggrid_EditingControlShowing);
            this.dtggrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datosgb_KeyDown_1);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "FECHA DE PAGO";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "PAGO";
            this.Column2.Name = "Column2";
            // 
            // PAGO
            // 
            this.PAGO.HeaderText = "DE";
            this.PAGO.Name = "PAGO";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "IMPORTE";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "RFC";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "DEPENDENCIA";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "PROYECTO";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "T.R.L";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "ID";
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "FUM";
            this.Column9.Name = "Column9";
            this.Column9.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1322, 38);
            this.panel2.TabIndex = 15;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(422, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quirografario";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(3, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(421, 26);
            this.label16.TabIndex = 0;
            this.label16.Text = "Prestaciones económicas > Estados de Cuenta >";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // group
            // 
            this.group.BackColor = System.Drawing.SystemColors.Control;
            this.group.Controls.Add(this.btbuscar);
            this.group.Controls.Add(this.btnNuevo);
            this.group.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group.Location = new System.Drawing.Point(1120, 40);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(202, 564);
            this.group.TabIndex = 2;
            this.group.TabStop = false;
            this.group.Text = "Opciones";
            this.group.Enter += new System.EventHandler(this.group_Enter);
            // 
            // btbuscar
            // 
            this.btbuscar.BackColor = System.Drawing.Color.White;
            this.btbuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btbuscar.Image = ((System.Drawing.Image)(resources.GetObject("btbuscar.Image")));
            this.btbuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btbuscar.Location = new System.Drawing.Point(6, 82);
            this.btbuscar.Name = "btbuscar";
            this.btbuscar.Size = new System.Drawing.Size(184, 39);
            this.btbuscar.TabIndex = 2;
            this.btbuscar.Text = "GUARDAR ";
            this.btbuscar.UseVisualStyleBackColor = false;
            this.btbuscar.Visible = false;
            this.btbuscar.Click += new System.EventHandler(this.btbuscar_Click_1);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(6, 34);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(184, 39);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "BUSCAR  ";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.fec_falle);
            this.panel5.Controls.Add(this.checFallecio);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.txtcheque);
            this.panel5.Controls.Add(this.txttotal);
            this.panel5.Controls.Add(this.txtpagocuenta);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.txtplazo);
            this.panel5.Controls.Add(this.txtubicacion);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.txtimporte);
            this.panel5.Controls.Add(this.txtfechasolicitud);
            this.panel5.Controls.Add(this.txtpago);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.txtdirec);
            this.panel5.Controls.Add(this.txtnombre);
            this.panel5.Controls.Add(this.txtsecretaria);
            this.panel5.Controls.Add(this.txtproyecto);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.txtrfc);
            this.panel5.Controls.Add(this.txtfolio);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.btn1);
            this.panel5.Controls.Add(this.btn2);
            this.panel5.Location = new System.Drawing.Point(17, 44);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1020, 229);
            this.panel5.TabIndex = 0;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // fec_falle
            // 
            this.fec_falle.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.fec_falle.CustomFormat = "dd-MM-yyyy";
            this.fec_falle.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fec_falle.Location = new System.Drawing.Point(824, 192);
            this.fec_falle.Name = "fec_falle";
            this.fec_falle.Size = new System.Drawing.Size(79, 20);
            this.fec_falle.TabIndex = 103;
            this.fec_falle.Visible = false;
            // 
            // checFallecio
            // 
            this.checFallecio.AutoSize = true;
            this.checFallecio.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.checFallecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.checFallecio.Location = new System.Drawing.Point(735, 192);
            this.checFallecio.Name = "checFallecio";
            this.checFallecio.Size = new System.Drawing.Size(83, 22);
            this.checFallecio.TabIndex = 102;
            this.checFallecio.Text = "FALLECIÓ";
            this.checFallecio.UseVisualStyleBackColor = true;
            this.checFallecio.Visible = false;
            this.checFallecio.CheckedChanged += new System.EventHandler(this.checFallecio_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(312, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 30);
            this.button1.TabIndex = 101;
            this.button1.Text = "aux";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txtcheque
            // 
            this.txtcheque.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcheque.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtcheque.Location = new System.Drawing.Point(870, 141);
            this.txtcheque.Mask = "00/00/0000";
            this.txtcheque.Name = "txtcheque";
            this.txtcheque.Size = new System.Drawing.Size(126, 19);
            this.txtcheque.TabIndex = 100;
            this.txtcheque.ValidatingType = typeof(System.DateTime);
            // 
            // txttotal
            // 
            this.txttotal.BackColor = System.Drawing.Color.White;
            this.txttotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txttotal.Cursor = System.Windows.Forms.Cursors.No;
            this.txttotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txttotal.Location = new System.Drawing.Point(869, 116);
            this.txttotal.Name = "txttotal";
            this.txttotal.ReadOnly = true;
            this.txttotal.Size = new System.Drawing.Size(127, 19);
            this.txttotal.TabIndex = 99;
            // 
            // txtpagocuenta
            // 
            this.txtpagocuenta.BackColor = System.Drawing.Color.White;
            this.txtpagocuenta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpagocuenta.Cursor = System.Windows.Forms.Cursors.No;
            this.txtpagocuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpagocuenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtpagocuenta.Location = new System.Drawing.Point(870, 90);
            this.txtpagocuenta.Name = "txtpagocuenta";
            this.txtpagocuenta.ReadOnly = true;
            this.txtpagocuenta.Size = new System.Drawing.Size(127, 19);
            this.txtpagocuenta.TabIndex = 98;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label14.Location = new System.Drawing.Point(813, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 18);
            this.label14.TabIndex = 97;
            this.label14.Text = "TOTAL:";
            // 
            // txtplazo
            // 
            this.txtplazo.BackColor = System.Drawing.Color.White;
            this.txtplazo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtplazo.Cursor = System.Windows.Forms.Cursors.No;
            this.txtplazo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtplazo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtplazo.Location = new System.Drawing.Point(547, 139);
            this.txtplazo.Name = "txtplazo";
            this.txtplazo.ReadOnly = true;
            this.txtplazo.Size = new System.Drawing.Size(82, 19);
            this.txtplazo.TabIndex = 96;
            // 
            // txtubicacion
            // 
            this.txtubicacion.BackColor = System.Drawing.Color.White;
            this.txtubicacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtubicacion.Cursor = System.Windows.Forms.Cursors.No;
            this.txtubicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtubicacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtubicacion.Location = new System.Drawing.Point(547, 114);
            this.txtubicacion.Name = "txtubicacion";
            this.txtubicacion.ReadOnly = true;
            this.txtubicacion.Size = new System.Drawing.Size(127, 19);
            this.txtubicacion.TabIndex = 95;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label10.Location = new System.Drawing.Point(680, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 18);
            this.label10.TabIndex = 93;
            this.label10.Text = "PRIMER PAGO EDO. CUENTA:";
            // 
            // txtimporte
            // 
            this.txtimporte.BackColor = System.Drawing.Color.White;
            this.txtimporte.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtimporte.Cursor = System.Windows.Forms.Cursors.No;
            this.txtimporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtimporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtimporte.Location = new System.Drawing.Point(547, 89);
            this.txtimporte.Name = "txtimporte";
            this.txtimporte.ReadOnly = true;
            this.txtimporte.Size = new System.Drawing.Size(127, 19);
            this.txtimporte.TabIndex = 92;
            // 
            // txtfechasolicitud
            // 
            this.txtfechasolicitud.BackColor = System.Drawing.Color.White;
            this.txtfechasolicitud.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtfechasolicitud.Cursor = System.Windows.Forms.Cursors.No;
            this.txtfechasolicitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfechasolicitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtfechasolicitud.Location = new System.Drawing.Point(161, 113);
            this.txtfechasolicitud.Name = "txtfechasolicitud";
            this.txtfechasolicitud.ReadOnly = true;
            this.txtfechasolicitud.Size = new System.Drawing.Size(195, 19);
            this.txtfechasolicitud.TabIndex = 91;
            // 
            // txtpago
            // 
            this.txtpago.BackColor = System.Drawing.Color.White;
            this.txtpago.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpago.Cursor = System.Windows.Forms.Cursors.No;
            this.txtpago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtpago.Location = new System.Drawing.Point(294, 88);
            this.txtpago.Name = "txtpago";
            this.txtpago.ReadOnly = true;
            this.txtpago.Size = new System.Drawing.Size(84, 19);
            this.txtpago.TabIndex = 90;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label13.Location = new System.Drawing.Point(732, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 18);
            this.label13.TabIndex = 89;
            this.label13.Text = "FECHA DEL CHEQUE:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label12.Location = new System.Drawing.Point(485, 138);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 18);
            this.label12.TabIndex = 88;
            this.label12.Text = "PLAZO:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label11.Location = new System.Drawing.Point(380, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(161, 18);
            this.label11.TabIndex = 87;
            this.label11.Text = "UBICACIÓN DEL PAGARE:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label9.Location = new System.Drawing.Point(408, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 18);
            this.label9.TabIndex = 85;
            this.label9.Text = "IMPORTE UNITARIO:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label8.Location = new System.Drawing.Point(16, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 18);
            this.label8.TabIndex = 84;
            this.label8.Text = "FECHA DE SOLICITUD:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label7.Location = new System.Drawing.Point(16, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(272, 18);
            this.label7.TabIndex = 83;
            this.label7.Text = "FORMA DE PAGO (MENSUAL/QUINCENAL):";
            // 
            // txtdirec
            // 
            this.txtdirec.BackColor = System.Drawing.Color.White;
            this.txtdirec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdirec.Cursor = System.Windows.Forms.Cursors.No;
            this.txtdirec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdirec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtdirec.Location = new System.Drawing.Point(395, 62);
            this.txtdirec.Name = "txtdirec";
            this.txtdirec.ReadOnly = true;
            this.txtdirec.Size = new System.Drawing.Size(602, 19);
            this.txtdirec.TabIndex = 82;
            // 
            // txtnombre
            // 
            this.txtnombre.BackColor = System.Drawing.Color.White;
            this.txtnombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtnombre.Cursor = System.Windows.Forms.Cursors.No;
            this.txtnombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtnombre.Location = new System.Drawing.Point(395, 37);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.ReadOnly = true;
            this.txtnombre.Size = new System.Drawing.Size(602, 19);
            this.txtnombre.TabIndex = 81;
            // 
            // txtsecretaria
            // 
            this.txtsecretaria.BackColor = System.Drawing.Color.White;
            this.txtsecretaria.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsecretaria.Cursor = System.Windows.Forms.Cursors.No;
            this.txtsecretaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsecretaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtsecretaria.Location = new System.Drawing.Point(395, 10);
            this.txtsecretaria.Name = "txtsecretaria";
            this.txtsecretaria.ReadOnly = true;
            this.txtsecretaria.Size = new System.Drawing.Size(602, 19);
            this.txtsecretaria.TabIndex = 80;
            // 
            // txtproyecto
            // 
            this.txtproyecto.BackColor = System.Drawing.Color.White;
            this.txtproyecto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtproyecto.Cursor = System.Windows.Forms.Cursors.No;
            this.txtproyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtproyecto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtproyecto.Location = new System.Drawing.Point(91, 60);
            this.txtproyecto.Name = "txtproyecto";
            this.txtproyecto.ReadOnly = true;
            this.txtproyecto.Size = new System.Drawing.Size(195, 19);
            this.txtproyecto.TabIndex = 79;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label6.Location = new System.Drawing.Point(309, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 78;
            this.label6.Text = "DIRECCION:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label5.Location = new System.Drawing.Point(321, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 77;
            this.label5.Text = "NOMBRE:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label4.Location = new System.Drawing.Point(304, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 18);
            this.label4.TabIndex = 76;
            this.label4.Text = "SECRETARIA:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label3.Location = new System.Drawing.Point(7, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 18);
            this.label3.TabIndex = 75;
            this.label3.Text = "PROYECTO:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label15.Location = new System.Drawing.Point(50, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 18);
            this.label15.TabIndex = 74;
            this.label15.Text = "RFC:";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // txtrfc
            // 
            this.txtrfc.BackColor = System.Drawing.Color.White;
            this.txtrfc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtrfc.Cursor = System.Windows.Forms.Cursors.No;
            this.txtrfc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrfc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtrfc.Location = new System.Drawing.Point(91, 34);
            this.txtrfc.Name = "txtrfc";
            this.txtrfc.ReadOnly = true;
            this.txtrfc.Size = new System.Drawing.Size(195, 19);
            this.txtrfc.TabIndex = 73;
            this.txtrfc.TextChanged += new System.EventHandler(this.txtrfc_TextChanged);
            // 
            // txtfolio
            // 
            this.txtfolio.BackColor = System.Drawing.Color.White;
            this.txtfolio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtfolio.Cursor = System.Windows.Forms.Cursors.No;
            this.txtfolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfolio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.txtfolio.Location = new System.Drawing.Point(91, 9);
            this.txtfolio.Name = "txtfolio";
            this.txtfolio.ReadOnly = true;
            this.txtfolio.Size = new System.Drawing.Size(195, 19);
            this.txtfolio.TabIndex = 72;
            this.txtfolio.TextChanged += new System.EventHandler(this.txtfolio_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label2.Location = new System.Drawing.Point(36, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 18);
            this.label2.TabIndex = 71;
            this.label2.Text = "FOLIO:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn1.Enabled = false;
            this.btn1.FlatAppearance.BorderSize = 0;
            this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.btn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn1.Location = new System.Drawing.Point(19, 140);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(121, 30);
            this.btn1.TabIndex = 70;
            this.btn1.Text = "Anterior";
            this.btn1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn2.Enabled = false;
            this.btn2.FlatAppearance.BorderSize = 0;
            this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.btn2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn2.Location = new System.Drawing.Point(156, 140);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(121, 30);
            this.btn2.TabIndex = 69;
            this.btn2.Text = "Siguiente";
            this.btn2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new System.EventHandler(this.btbuscar_Click);
            // 
            // frmestados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 610);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmestados";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta";
            this.Load += new System.EventHandler(this.frmconsulta_Load);
            this.Shown += new System.EventHandler(this.frmestados_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmconsulta_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmestados_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtggrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.group.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox group;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dtggrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtfolio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtrfc;
        private System.Windows.Forms.TextBox txtsecretaria;
        private System.Windows.Forms.TextBox txtproyecto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtimporte;
        private System.Windows.Forms.TextBox txtfechasolicitud;
        private System.Windows.Forms.TextBox txtpago;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtdirec;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.TextBox txttotal;
        private System.Windows.Forms.TextBox txtpagocuenta;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtplazo;
        private System.Windows.Forms.TextBox txtubicacion;
        private System.Windows.Forms.MaskedTextBox txtcheque;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PAGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btbuscar;
        private System.Windows.Forms.CheckBox checFallecio;
        private System.Windows.Forms.DateTimePicker fec_falle;
    }
}