using SISPE_MIGRACION.codigo.herramientas.forms.controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    delegate void enviando(DialogResult resultado);
    public partial class frmAvisoPregunta : Form
    {
        internal DialogResult resultado;
        private string mensaje = string.Empty;
        private string titulo = string.Empty;
        private yesno p;
        private frmInformacion informacion;
        private string tipo = string.Empty;
        private frmExclamacion exclamation;
        private frmError error;
        private frmSuccess success;
        private frmQuestion question;
        private Form ventanaModal = null;
        private bool left_button = false;
    
        public frmAvisoPregunta(Form ventana,string mensaje,string titulo = "",string tipo = "",Form ventanaModal = null,bool left_button = false)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Owner = ventana;
            this.Size = ventana.Size;
            this.Location = ventana.Location;
            this.mensaje = mensaje;
            this.titulo = titulo;
            this.tipo = tipo;
            this.ventanaModal = ventanaModal;
            this.ShowInTaskbar = false;
            this.left_button = left_button;

        }
        

        private void frmAvisoPregunta_Load(object sender, EventArgs e)
        {
            if (tipo == "modal") {
                this.BackColor = Color.Black;
                this.ventanaModal.Owner = this;
                this.ventanaModal.Show();
                return;
            }

            
            if (tipo == "")
            {
                p = new yesno(this, this.mensaje);
                p.enviando = recibiendo;
                p.Show();
            }
            else if (tipo == "informacion")
            {
                informacion = new frmInformacion(this, this.mensaje, this.titulo);
                informacion.enviando = recibiendo;
                informacion.Show();
            }
            else if (tipo == "error")
            {
                error = new frmError(this.mensaje, this.titulo, this);
                error.enviando = recibiendo;
                error.Show();
            }
            else if (tipo == "exclamation")
            {
                exclamation = new frmExclamacion(this.mensaje, this.titulo, this);
                exclamation.enviando = recibiendo;
                exclamation.Show();
            }
            else if (tipo == "question") {
                question = new frmQuestion(this.mensaje,this.titulo,this);
                question.enviando = recibiendo;
                question.Show();
            }
            else {
                success = new frmSuccess(this.mensaje, this.titulo, this);
                success.enviando = recibiendo;
                success.Show();
            }
           
        }

        private void sa(object sender, DoWorkEventArgs e)
        {
         
        }

        private void recibiendo(DialogResult resultado) {
            this.resultado  = resultado;

            this.Close();
        }

        private void frmAvisoPregunta_Click(object sender, EventArgs e)
        {
            this.resultado = DialogResult.No;
            this.Close();
        }

        private void frmAvisoPregunta_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.tipo == "") {
                p.Focus();
                if (e.KeyCode == Keys.Enter)
                {
                    p.btn1_Click(null, null);
                }

                if (e.KeyCode == Keys.Right)
                {
                    p.btn2.Focus();
                }
            }

            if (this.tipo == "informacion") {
                informacion.Focus();
                if (e.KeyCode == Keys.Enter) {
                    informacion.button1_Click(null,null);
                }
            }

            if (this.tipo == "exclamation")
            {
                exclamation.Focus();
                if (e.KeyCode == Keys.Enter)
                {
                    exclamation.btn1_Click(null, null);
                }
            }

            if (this.tipo == "error")
            {
                error.Focus();
                if (e.KeyCode == Keys.Enter)
                {
                    error.btn1_Click(null, null);
                }
            }

            if (this.tipo == "success")
            {
                success.Focus();
                if (e.KeyCode == Keys.Enter)
                {
                    success.btn1_Click(null, null);
                }
            }

            if (this.tipo == "question")
            {
                question.Focus();
                if (e.KeyCode == Keys.Enter)
                {
                    question.btn1_Click(null, null);
                }

                if (e.KeyCode == Keys.Right)
                {
                    question.button1.Focus();
                }
            }

        }
    }
}
