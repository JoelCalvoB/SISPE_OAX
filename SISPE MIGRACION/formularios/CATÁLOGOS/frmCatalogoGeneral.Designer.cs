namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    partial class frmCatalogoGeneral
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btncerrar = new System.Windows.Forms.Button();
            this.btnseleccionar = new System.Windows.Forms.Button();
            this.datos = new System.Windows.Forms.DataGridView();
            this.Colum1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.datos);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 509);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btncerrar);
            this.panel2.Controls.Add(this.btnseleccionar);
            this.panel2.Location = new System.Drawing.Point(12, 461);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(383, 36);
            this.panel2.TabIndex = 9;
            // 
            // btncerrar
            // 
            this.btncerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncerrar.Location = new System.Drawing.Point(251, 3);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Size = new System.Drawing.Size(115, 27);
            this.btncerrar.TabIndex = 3;
            this.btncerrar.Text = "CERRAR";
            this.btncerrar.UseVisualStyleBackColor = true;
            // 
            // btnseleccionar
            // 
            this.btnseleccionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnseleccionar.Location = new System.Drawing.Point(114, 3);
            this.btnseleccionar.Name = "btnseleccionar";
            this.btnseleccionar.Size = new System.Drawing.Size(120, 27);
            this.btnseleccionar.TabIndex = 2;
            this.btnseleccionar.Text = "SELECCIONAR ";
            this.btnseleccionar.UseVisualStyleBackColor = true;
            this.btnseleccionar.Click += new System.EventHandler(this.btnseleccionar_Click);
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
            this.datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Colum1,
            this.Column2});
            this.datos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.datos.Location = new System.Drawing.Point(12, 28);
            this.datos.MultiSelect = false;
            this.datos.Name = "datos";
            this.datos.ReadOnly = true;
            this.datos.RowHeadersVisible = false;
            this.datos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datos.Size = new System.Drawing.Size(383, 427);
            this.datos.TabIndex = 7;
            this.datos.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.datos_RowEnter);
            this.datos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datos_KeyDown);
            // 
            // Colum1
            // 
            this.Colum1.FillWeight = 40F;
            this.Colum1.HeaderText = "FINALIDAD";
            this.Colum1.Name = "Colum1";
            this.Colum1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "DESCRIPCION";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "CATÁLOGO DE  FINALIDAD";
            // 
            // frmFinalidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 509);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFinalidad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo finalidad";
            this.Load += new System.EventHandler(this.frmFinalidad_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btncerrar;
        private System.Windows.Forms.Button btnseleccionar;
        private System.Windows.Forms.DataGridView datos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colum1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}