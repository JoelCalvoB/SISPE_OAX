namespace SISPE_MIGRACION.formularios.OFICIALIA_DE_PARTES
{
    partial class frmPrincipalOficialia
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt1ruta = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnP_hipote = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.ListDependencias = new System.Windows.Forms.CheckedListBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtFechaRecepcion = new System.Windows.Forms.MaskedTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDependRemit = new System.Windows.Forms.ComboBox();
            this.txtFolioDoc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFcehaLimite = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTipo = new System.Windows.Forms.ComboBox();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDestinario = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNomRemitente = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboPrioridad = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboArea = new System.Windows.Forms.ComboBox();
            this.txtAsunto = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.open1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1316, 550);
            this.panel1.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label15.Location = new System.Drawing.Point(11, 311);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(390, 23);
            this.label15.TabIndex = 1;
            this.label15.Text = "DATOS DE RECEPCIÓN > RECEPCIÓN Y REGISTRO ";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.txt1ruta);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.btnP_hipote);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.ListDependencias);
            this.panel4.Controls.Add(this.txtRuta);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.txtFechaRecepcion);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Location = new System.Drawing.Point(12, 334);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1266, 184);
            this.panel4.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(528, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "RUTA DEL ARCHIVO";
            // 
            // txt1ruta
            // 
            this.txt1ruta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt1ruta.Location = new System.Drawing.Point(531, 101);
            this.txt1ruta.Multiline = true;
            this.txt1ruta.Name = "txt1ruta";
            this.txt1ruta.ReadOnly = true;
            this.txt1ruta.Size = new System.Drawing.Size(589, 23);
            this.txt1ruta.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Gray;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(668, 130);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 34);
            this.button2.TabIndex = 96;
            this.button2.Text = "⤒ Subir archivo";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.bunifuThinButton22_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Gray;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(531, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 34);
            this.button1.TabIndex = 95;
            this.button1.Text = "⎙ Escanear";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.bunifuThinButton21_Click);
            // 
            // btnP_hipote
            // 
            this.btnP_hipote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(173)))), ((int)(((byte)(104)))));
            this.btnP_hipote.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnP_hipote.FlatAppearance.BorderSize = 0;
            this.btnP_hipote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnP_hipote.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP_hipote.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnP_hipote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnP_hipote.Location = new System.Drawing.Point(943, 130);
            this.btnP_hipote.Name = "btnP_hipote";
            this.btnP_hipote.Size = new System.Drawing.Size(177, 34);
            this.btnP_hipote.TabIndex = 94;
            this.btnP_hipote.Text = "✓ Guardar";
            this.btnP_hipote.UseVisualStyleBackColor = false;
            this.btnP_hipote.Click += new System.EventHandler(this.bunifuFlatButton1_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label20.Location = new System.Drawing.Point(7, 96);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(93, 13);
            this.label20.TabIndex = 65;
            this.label20.Text = "COPIAS PARA ";
            // 
            // ListDependencias
            // 
            this.ListDependencias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListDependencias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListDependencias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(90)))), ((int)(((byte)(86)))));
            this.ListDependencias.FormattingEnabled = true;
            this.ListDependencias.Location = new System.Drawing.Point(106, 34);
            this.ListDependencias.Name = "ListDependencias";
            this.ListDependencias.Size = new System.Drawing.Size(402, 135);
            this.ListDependencias.TabIndex = 0;
            this.ListDependencias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListDependencias_KeyPress);
            // 
            // txtRuta
            // 
            this.txtRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRuta.Location = new System.Drawing.Point(531, 51);
            this.txtRuta.Multiline = true;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(438, 23);
            this.txtRuta.TabIndex = 2;
            this.txtRuta.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label19.Location = new System.Drawing.Point(951, 51);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 13);
            this.label19.TabIndex = 60;
            // 
            // txtFechaRecepcion
            // 
            this.txtFechaRecepcion.Location = new System.Drawing.Point(737, 24);
            this.txtFechaRecepcion.Mask = "00/00/0000 00:00";
            this.txtFechaRecepcion.Name = "txtFechaRecepcion";
            this.txtFechaRecepcion.ReadOnly = true;
            this.txtFechaRecepcion.Size = new System.Drawing.Size(106, 20);
            this.txtFechaRecepcion.TabIndex = 1;
            this.txtFechaRecepcion.ValidatingType = typeof(System.DateTime);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label18.Location = new System.Drawing.Point(528, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(194, 13);
            this.label18.TabIndex = 58;
            this.label18.Text = "FECHA Y HORA DE RECEPCIÓN";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1316, 38);
            this.panel2.TabIndex = 22;
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
            this.button3.Location = new System.Drawing.Point(1210, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "Cerrar   X";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label16.Location = new System.Drawing.Point(3, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(430, 26);
            this.label16.TabIndex = 0;
            this.label16.Text = "OFICIALÍA DE PARTES > RECEPCIÓN Y REGISTRO ";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.txtDependRemit);
            this.panel3.Controls.Add(this.txtFolioDoc);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtFcehaLimite);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtTipo);
            this.panel3.Controls.Add(this.txtFolio);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.txtDestinario);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtNomRemitente);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.comboPrioridad);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.comboArea);
            this.panel3.Controls.Add(this.txtAsunto);
            this.panel3.Location = new System.Drawing.Point(12, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1266, 274);
            this.panel3.TabIndex = 27;
            // 
            // txtDependRemit
            // 
            this.txtDependRemit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDependRemit.FormattingEnabled = true;
            this.txtDependRemit.Location = new System.Drawing.Point(387, 110);
            this.txtDependRemit.Name = "txtDependRemit";
            this.txtDependRemit.Size = new System.Drawing.Size(438, 21);
            this.txtDependRemit.TabIndex = 8;
            this.txtDependRemit.DropDown += new System.EventHandler(this.txtDependRemit_DropDown);
            this.txtDependRemit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDependRemit_KeyPress_1);
            // 
            // txtFolioDoc
            // 
            this.txtFolioDoc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFolioDoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFolioDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFolioDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolioDoc.Location = new System.Drawing.Point(247, 70);
            this.txtFolioDoc.Name = "txtFolioDoc";
            this.txtFolioDoc.Size = new System.Drawing.Size(207, 19);
            this.txtFolioDoc.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label5.Location = new System.Drawing.Point(244, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "FOLIO DOCUMENTO ";
            // 
            // txtFcehaLimite
            // 
            this.txtFcehaLimite.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFcehaLimite.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFcehaLimite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFcehaLimite.Location = new System.Drawing.Point(713, 70);
            this.txtFcehaLimite.Mask = "00/00/0000";
            this.txtFcehaLimite.Name = "txtFcehaLimite";
            this.txtFcehaLimite.Size = new System.Drawing.Size(152, 19);
            this.txtFcehaLimite.TabIndex = 6;
            this.txtFcehaLimite.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label4.Location = new System.Drawing.Point(710, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "FECHA LIMITE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label3.Location = new System.Drawing.Point(467, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "TIPO:";
            // 
            // txtTipo
            // 
            this.txtTipo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTipo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipo.FormattingEnabled = true;
            this.txtTipo.Items.AddRange(new object[] {
            "OFICIO",
            "CIRCULAR",
            "NOTA ",
            "INVITACIÓN"});
            this.txtTipo.Location = new System.Drawing.Point(470, 26);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(435, 24);
            this.txtTipo.TabIndex = 1;
            this.txtTipo.Enter += new System.EventHandler(this.txtTipo_Enter);
            this.txtTipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTipo_KeyPress);
            // 
            // txtFolio
            // 
            this.txtFolio.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFolio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFolio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFolio.Enabled = false;
            this.txtFolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.Location = new System.Drawing.Point(14, 70);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.ReadOnly = true;
            this.txtFolio.Size = new System.Drawing.Size(207, 19);
            this.txtFolio.TabIndex = 3;
            this.txtFolio.Text = "AUTOGENERADO";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label14.Location = new System.Drawing.Point(11, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "FOLIO INTERNO:";
            // 
            // txtDestinario
            // 
            this.txtDestinario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDestinario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestinario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDestinario.Enabled = false;
            this.txtDestinario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinario.Location = new System.Drawing.Point(492, 70);
            this.txtDestinario.Name = "txtDestinario";
            this.txtDestinario.Size = new System.Drawing.Size(207, 19);
            this.txtDestinario.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label11.Location = new System.Drawing.Point(489, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "DESTINATARIO";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label10.Location = new System.Drawing.Point(362, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "DEPENDENCIA";
            this.label10.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label9.Location = new System.Drawing.Point(11, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "NOMBRE REMITENTE";
            this.label9.Visible = false;
            // 
            // txtNomRemitente
            // 
            this.txtNomRemitente.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNomRemitente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNomRemitente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomRemitente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomRemitente.Location = new System.Drawing.Point(14, 108);
            this.txtNomRemitente.Name = "txtNomRemitente";
            this.txtNomRemitente.Size = new System.Drawing.Size(308, 19);
            this.txtNomRemitente.TabIndex = 7;
            this.txtNomRemitente.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label7.Location = new System.Drawing.Point(914, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "PRIORIDAD";
            // 
            // comboPrioridad
            // 
            this.comboPrioridad.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboPrioridad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboPrioridad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPrioridad.FormattingEnabled = true;
            this.comboPrioridad.Items.AddRange(new object[] {
            "ALTA",
            "MEDIA",
            "BAJA"});
            this.comboPrioridad.Location = new System.Drawing.Point(917, 26);
            this.comboPrioridad.Name = "comboPrioridad";
            this.comboPrioridad.Size = new System.Drawing.Size(207, 24);
            this.comboPrioridad.TabIndex = 2;
            this.comboPrioridad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboPrioridad_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label6.Location = new System.Drawing.Point(11, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "ASUNTO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(108)))));
            this.label2.Location = new System.Drawing.Point(11, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "ÁREA DE ATENCIÓN:";
            // 
            // comboArea
            // 
            this.comboArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboArea.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboArea.FormattingEnabled = true;
            this.comboArea.Items.AddRange(new object[] {
            "DIRECCIÓN GENERAL",
            "UNIDAD ADMINISTRACIÓN Y FINANZAS DE LOS FONDOS DE PENSIONES",
            "JURÍDICO",
            "DIRECCIÓN DE PRESTACIONES ECONÓMICAS",
            "CONTABILIDAD Y FINANZAS",
            " NORMATIVIDAD Y SEGUIMIENTO",
            "SERVICIOS GENERALES Y RECURSOS MATERIALES",
            " RECURSOS HUMANOS Y NÓMINAS DE PENSIONES",
            "TECNOLOGÍAS DE LA INFORMACIÓN",
            "PRESTACIONES ECONÓMICAS",
            "PRESTACIONES SOCIALES Y CULTURALES"});
            this.comboArea.Location = new System.Drawing.Point(14, 27);
            this.comboArea.Name = "comboArea";
            this.comboArea.Size = new System.Drawing.Size(425, 24);
            this.comboArea.TabIndex = 0;
            this.comboArea.SelectedIndexChanged += new System.EventHandler(this.comboArea_SelectedIndexChanged);
            this.comboArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboArea_KeyPress);
            // 
            // txtAsunto
            // 
            this.txtAsunto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAsunto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAsunto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAsunto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAsunto.Location = new System.Drawing.Point(10, 146);
            this.txtAsunto.Multiline = true;
            this.txtAsunto.Name = "txtAsunto";
            this.txtAsunto.Size = new System.Drawing.Size(1052, 99);
            this.txtAsunto.TabIndex = 9;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // open1
            // 
            this.open1.FileName = "openFileDialog1";
            // 
            // frmPrincipalOficialia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 590);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmPrincipalOficialia";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPrincipalOficialia_Load);
            this.Shown += new System.EventHandler(this.frmPrincipalOficialia_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPrincipalOficialia_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmPrincipalOficialia_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboArea;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNomRemitente;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboPrioridad;
        private System.Windows.Forms.TextBox txtAsunto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDestinario;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.MaskedTextBox txtFechaRecepcion;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox txtFcehaLimite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox txtTipo;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckedListBox ListDependencias;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.OpenFileDialog open1;
        internal System.Windows.Forms.Button btnP_hipote;
        internal System.Windows.Forms.Button button2;
        internal System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt1ruta;
        private System.Windows.Forms.TextBox txtFolioDoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txtDependRemit;
    }
}