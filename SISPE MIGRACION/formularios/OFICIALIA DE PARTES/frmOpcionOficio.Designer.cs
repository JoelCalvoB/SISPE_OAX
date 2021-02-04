namespace SISPE_MIGRACION.formularios.OFICIALIA_DE_PARTES
{
    partial class frmOpcionOficio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNotif = new System.Windows.Forms.Button();
            this.btnCarga = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.combDepto = new System.Windows.Forms.ComboBox();
            this.checkTermino = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtResponde = new System.Windows.Forms.TextBox();
            this.txtFec_respuesta = new System.Windows.Forms.MaskedTextBox();
            this.txtAsuntoR = new System.Windows.Forms.TextBox();
            this.checkhlistCopias = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maskeFechalim = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbHistorial = new System.Windows.Forms.RadioButton();
            this.rbResponder = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTipoDoc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.dataOficios = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.open2 = new System.Windows.Forms.OpenFileDialog();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataOficios)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNotif);
            this.panel1.Controls.Add(this.btnCarga);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.combDepto);
            this.panel1.Controls.Add(this.checkTermino);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtResponde);
            this.panel1.Controls.Add(this.txtFec_respuesta);
            this.panel1.Controls.Add(this.txtAsuntoR);
            this.panel1.Controls.Add(this.checkhlistCopias);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.maskeFechalim);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTipoDoc);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtFolio);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dataOficios);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1140, 571);
            this.panel1.TabIndex = 0;
            // 
            // btnNotif
            // 
            this.btnNotif.BackColor = System.Drawing.Color.White;
            this.btnNotif.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotif.FlatAppearance.BorderSize = 0;
            this.btnNotif.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNotif.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotif.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnNotif.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNotif.Location = new System.Drawing.Point(91, 509);
            this.btnNotif.Name = "btnNotif";
            this.btnNotif.Size = new System.Drawing.Size(91, 34);
            this.btnNotif.TabIndex = 96;
            this.btnNotif.Text = "Notificar";
            this.btnNotif.UseVisualStyleBackColor = false;
            this.btnNotif.Click += new System.EventHandler(this.btnNotif_Click);
            // 
            // btnCarga
            // 
            this.btnCarga.BackColor = System.Drawing.Color.White;
            this.btnCarga.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCarga.FlatAppearance.BorderSize = 0;
            this.btnCarga.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCarga.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(173)))), ((int)(((byte)(104)))));
            this.btnCarga.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCarga.Location = new System.Drawing.Point(535, 444);
            this.btnCarga.Name = "btnCarga";
            this.btnCarga.Size = new System.Drawing.Size(119, 34);
            this.btnCarga.TabIndex = 95;
            this.btnCarga.Text = "✓ Responder";
            this.btnCarga.UseVisualStyleBackColor = false;
            this.btnCarga.Visible = false;
            this.btnCarga.Click += new System.EventHandler(this.btnCarga_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label9.Location = new System.Drawing.Point(196, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 83;
            this.label9.Text = "Detalle";
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label8.Location = new System.Drawing.Point(46, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 82;
            this.label8.Text = "Depto.";
            // 
            // combDepto
            // 
            this.combDepto.Enabled = false;
            this.combDepto.FormattingEnabled = true;
            this.combDepto.Location = new System.Drawing.Point(91, 64);
            this.combDepto.Name = "combDepto";
            this.combDepto.Size = new System.Drawing.Size(245, 21);
            this.combDepto.TabIndex = 81;
            // 
            // checkTermino
            // 
            this.checkTermino.AutoSize = true;
            this.checkTermino.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkTermino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.checkTermino.Location = new System.Drawing.Point(282, 400);
            this.checkTermino.Name = "checkTermino";
            this.checkTermino.Size = new System.Drawing.Size(516, 22);
            this.checkTermino.TabIndex = 78;
            this.checkTermino.Text = "MARCAR SI YA QUIERE CERRAR EL CASO DE ESTA OFICIALIA";
            this.toolTip1.SetToolTip(this.checkTermino, "MARCAR SOLO SI YA SE DARÁ POR TERMINADO O CERRADO EL SEGUIMIENTO A ESTA FOLIO.");
            this.checkTermino.UseVisualStyleBackColor = true;
            this.checkTermino.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label6.Location = new System.Drawing.Point(532, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 76;
            this.label6.Text = "Responde";
            this.label6.Visible = false;
            // 
            // txtResponde
            // 
            this.txtResponde.Location = new System.Drawing.Point(615, 362);
            this.txtResponde.Name = "txtResponde";
            this.txtResponde.Size = new System.Drawing.Size(377, 20);
            this.txtResponde.TabIndex = 75;
            this.txtResponde.Visible = false;
            // 
            // txtFec_respuesta
            // 
            this.txtFec_respuesta.Location = new System.Drawing.Point(385, 362);
            this.txtFec_respuesta.Mask = "00/00/0000";
            this.txtFec_respuesta.Name = "txtFec_respuesta";
            this.txtFec_respuesta.Size = new System.Drawing.Size(100, 20);
            this.txtFec_respuesta.TabIndex = 74;
            this.txtFec_respuesta.ValidatingType = typeof(System.DateTime);
            this.txtFec_respuesta.Visible = false;
            // 
            // txtAsuntoR
            // 
            this.txtAsuntoR.Location = new System.Drawing.Point(258, 199);
            this.txtAsuntoR.Multiline = true;
            this.txtAsuntoR.Name = "txtAsuntoR";
            this.txtAsuntoR.Size = new System.Drawing.Size(734, 123);
            this.txtAsuntoR.TabIndex = 71;
            this.txtAsuntoR.Visible = false;
            // 
            // checkhlistCopias
            // 
            this.checkhlistCopias.FormattingEnabled = true;
            this.checkhlistCopias.Location = new System.Drawing.Point(12, 199);
            this.checkhlistCopias.Name = "checkhlistCopias";
            this.checkhlistCopias.Size = new System.Drawing.Size(280, 304);
            this.checkhlistCopias.TabIndex = 68;
            this.checkhlistCopias.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label5.Location = new System.Drawing.Point(279, 365);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 73;
            this.label5.Text = "Fecha Respuesta";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label4.Location = new System.Drawing.Point(273, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "Nota:";
            this.label4.Visible = false;
            // 
            // maskeFechalim
            // 
            this.maskeFechalim.Location = new System.Drawing.Point(866, 67);
            this.maskeFechalim.Mask = "00/00/0000";
            this.maskeFechalim.Name = "maskeFechalim";
            this.maskeFechalim.ReadOnly = true;
            this.maskeFechalim.Size = new System.Drawing.Size(100, 20);
            this.maskeFechalim.TabIndex = 67;
            this.maskeFechalim.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label3.Location = new System.Drawing.Point(795, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "Fecha límite";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbHistorial);
            this.groupBox1.Controls.Add(this.rbResponder);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1088, 53);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones";
            // 
            // rbHistorial
            // 
            this.rbHistorial.AutoSize = true;
            this.rbHistorial.Location = new System.Drawing.Point(757, 20);
            this.rbHistorial.Name = "rbHistorial";
            this.rbHistorial.Size = new System.Drawing.Size(65, 17);
            this.rbHistorial.TabIndex = 2;
            this.rbHistorial.TabStop = true;
            this.rbHistorial.Text = "Historial ";
            this.rbHistorial.UseVisualStyleBackColor = true;
            this.rbHistorial.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbResponder
            // 
            this.rbResponder.AutoSize = true;
            this.rbResponder.Location = new System.Drawing.Point(429, 19);
            this.rbResponder.Name = "rbResponder";
            this.rbResponder.Size = new System.Drawing.Size(77, 17);
            this.rbResponder.TabIndex = 1;
            this.rbResponder.TabStop = true;
            this.rbResponder.Text = "Responder";
            this.rbResponder.UseVisualStyleBackColor = true;
            this.rbResponder.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(101, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Asignar";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(555, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Tipo Docto.";
            // 
            // txtTipoDoc
            // 
            this.txtTipoDoc.Location = new System.Drawing.Point(624, 64);
            this.txtTipoDoc.Name = "txtTipoDoc";
            this.txtTipoDoc.ReadOnly = true;
            this.txtTipoDoc.Size = new System.Drawing.Size(141, 20);
            this.txtTipoDoc.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label2.Location = new System.Drawing.Point(342, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Folio";
            // 
            // txtFolio
            // 
            this.txtFolio.Location = new System.Drawing.Point(377, 64);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.ReadOnly = true;
            this.txtFolio.Size = new System.Drawing.Size(141, 20);
            this.txtFolio.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1140, 38);
            this.panel2.TabIndex = 23;
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
            this.button1.Location = new System.Drawing.Point(1025, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 32);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cerrar   X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.button3.Location = new System.Drawing.Point(1232, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "Cerrar   X";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label16.Location = new System.Drawing.Point(3, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(316, 26);
            this.label16.TabIndex = 0;
            this.label16.Text = "OFICIALÍA DE PARTES > RESPUESTA";
            // 
            // dataOficios
            // 
            this.dataOficios.AllowUserToAddRows = false;
            this.dataOficios.AllowUserToDeleteRows = false;
            this.dataOficios.AllowUserToResizeColumns = false;
            this.dataOficios.AllowUserToResizeRows = false;
            this.dataOficios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataOficios.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataOficios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataOficios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column3,
            this.Column7,
            this.Column6,
            this.Column4});
            this.dataOficios.Location = new System.Drawing.Point(28, 177);
            this.dataOficios.Name = "dataOficios";
            this.dataOficios.Size = new System.Drawing.Size(1057, 382);
            this.dataOficios.TabIndex = 70;
            this.dataOficios.Visible = false;
            this.dataOficios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataOficios_CellDoubleClick);
            this.dataOficios.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataOficios_CellEnter);
            // 
            // open2
            // 
            this.open2.FileName = "openFileDialog1";
            // 
            // Column1
            // 
            this.Column1.FillWeight = 38F;
            this.Column1.HeaderText = "No. Folio";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 45F;
            this.Column2.HeaderText = "Fecha Registro";
            this.Column2.Name = "Column2";
            // 
            // Column5
            // 
            this.Column5.FillWeight = 60F;
            this.Column5.HeaderText = "Tipo";
            this.Column5.Name = "Column5";
            // 
            // Column3
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column3.HeaderText = "Responsable";
            this.Column3.Name = "Column3";
            // 
            // Column7
            // 
            this.Column7.FillWeight = 150F;
            this.Column7.HeaderText = "Detalle";
            this.Column7.Name = "Column7";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "id";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 30F;
            this.Column4.HeaderText = "Descarga";
            this.Column4.Image = global::SISPE_MIGRACION.Properties.Resources.devueltas;
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // frmOpcionOficio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 571);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOpcionOficio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOpcionOficio";
            this.Load += new System.EventHandler(this.frmOpcionOficio_Load);
            this.Shown += new System.EventHandler(this.frmOpcionOficio_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataOficios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTipoDoc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbHistorial;
        private System.Windows.Forms.RadioButton rbResponder;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtResponde;
        private System.Windows.Forms.MaskedTextBox txtFec_respuesta;
        private System.Windows.Forms.TextBox txtAsuntoR;
        private System.Windows.Forms.CheckedListBox checkhlistCopias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataOficios;
        private System.Windows.Forms.CheckBox checkTermino;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox combDepto;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MaskedTextBox maskeFechalim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog open2;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button btnCarga;
        internal System.Windows.Forms.Button btnNotif;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
    }
}