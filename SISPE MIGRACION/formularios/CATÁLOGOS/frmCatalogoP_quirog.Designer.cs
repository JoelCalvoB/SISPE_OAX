using System;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    partial class frmCatalogoP_quirog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCatalogoP_quirog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btncerrar = new System.Windows.Forms.Button();
            this.btnseleccionar = new System.Windows.Forms.Button();
            this.BUSQUEDA = new System.Windows.Forms.Label();
            this.datos = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Colum1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMBRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.BUSQUEDA);
            this.panel1.Controls.Add(this.datos);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txtBusqueda);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 503);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Menu;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btncerrar);
            this.panel2.Controls.Add(this.btnseleccionar);
            this.panel2.Location = new System.Drawing.Point(12, 433);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(519, 64);
            this.panel2.TabIndex = 9;
            // 
            // btncerrar
            // 
            this.btncerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncerrar.Location = new System.Drawing.Point(379, 17);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Size = new System.Drawing.Size(115, 27);
            this.btncerrar.TabIndex = 3;
            this.btncerrar.Text = "CERRAR";
            this.btncerrar.UseVisualStyleBackColor = true;
            this.btncerrar.Click += new System.EventHandler(this.btncerrar_Click);
            // 
            // btnseleccionar
            // 
            this.btnseleccionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnseleccionar.Location = new System.Drawing.Point(244, 17);
            this.btnseleccionar.Name = "btnseleccionar";
            this.btnseleccionar.Size = new System.Drawing.Size(120, 27);
            this.btnseleccionar.TabIndex = 2;
            this.btnseleccionar.Text = "SELECCIONAR ";
            this.btnseleccionar.UseVisualStyleBackColor = true;
            this.btnseleccionar.Click += new System.EventHandler(this.btnseleccionar_Click);
            // 
            // BUSQUEDA
            // 
            this.BUSQUEDA.AutoSize = true;
            this.BUSQUEDA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BUSQUEDA.Location = new System.Drawing.Point(12, 75);
            this.BUSQUEDA.Name = "BUSQUEDA";
            this.BUSQUEDA.Size = new System.Drawing.Size(257, 16);
            this.BUSQUEDA.TabIndex = 8;
            this.BUSQUEDA.Text = "Busqueda (FOLIO, RFC O NOMBRE)";
            // 
            // datos
            // 
            this.datos.AllowUserToAddRows = false;
            this.datos.AllowUserToDeleteRows = false;
            this.datos.AllowUserToResizeColumns = false;
            this.datos.AllowUserToResizeRows = false;
            this.datos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.datos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.datos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Colum1,
            this.Column2,
            this.NOMBRE});
            this.datos.Location = new System.Drawing.Point(15, 142);
            this.datos.MultiSelect = false;
            this.datos.Name = "datos";
            this.datos.ReadOnly = true;
            this.datos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.datos.Size = new System.Drawing.Size(516, 263);
            this.datos.TabIndex = 7;
            this.datos.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.datos_CellStateChanged);
            this.datos.QueryAccessibilityHelp += new System.Windows.Forms.QueryAccessibilityHelpEventHandler(this.datos_QueryAccessibilityHelp);
            this.datos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datos_KeyDown);
            this.datos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.datos_KeyPress);
            this.datos.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.datos_PreviewKeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(410, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusqueda.Location = new System.Drawing.Point(12, 94);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(519, 20);
            this.txtBusqueda.TabIndex = 4;
            this.txtBusqueda.TextChanged += new System.EventHandler(this.txtBusqueda_TextChanged);
            this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyDown);
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(153, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "CATÁLOGO DE BUSQUEDA";
            // 
            // Colum1
            // 
            this.Colum1.FillWeight = 40F;
            this.Colum1.HeaderText = "FOLIO";
            this.Colum1.Name = "Colum1";
            this.Colum1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "RFC";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // NOMBRE
            // 
            this.NOMBRE.HeaderText = "NOMBRE";
            this.NOMBRE.Name = "NOMBRE";
            this.NOMBRE.ReadOnly = true;
            // 
            // frmCatalogoP_quirog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 503);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCatalogoP_quirog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cátalogos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCatalogoP_quirog_FormClosing);
            this.Load += new System.EventHandler(this.frmCatalogoP_quirog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCatalogoP_quirog_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

       

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btncerrar;
        private System.Windows.Forms.Button btnseleccionar;
        private System.Windows.Forms.Label BUSQUEDA;
        private System.Windows.Forms.DataGridView datos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label label1;
        private DataGridViewTextBoxColumn Colum1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn NOMBRE;
    }
}