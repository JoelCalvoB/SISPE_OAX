using SISPE_MIGRACION.codigo.repositorios.cg_fondos;
using SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CUENTAS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CONSULTAS
{
    public partial class frmLibroDiarioDetalle : Form
    {
        private int polanio;
        private string poltipo, polmes, polnumero;
        private Dictionary<string, object> datos;
        private int row;
        private bool teclaEnter;
        private bool esInsertar;
        private bool esConsulta;
        private string prefijo;
        private int columna;
        private bool cargado;

        private bool abierto { get; set; }

        public void rellenar(Dictionary<string, object> obj, bool externo = false)
        {

            this.dtggrid.Rows[this.row].Cells[0].Value = Convert.ToString(obj["cuenta"]);
            this.dtggrid.Rows[this.row].Cells[1].Value = Convert.ToString(obj["descripcion"]);
            this.dtggrid.Rows[this.row].Cells[7].Value = Convert.ToString(obj["naturaleza"]);
            if (Convert.ToString(obj["naturaleza"]) == "D")
            {
                this.dtggrid.Rows[this.row].Cells[2].ReadOnly = false;
                this.dtggrid.Rows[this.row].Cells[3].ReadOnly = true;
            }
            else if (Convert.ToString(obj["naturaleza"]) == "H")
            {
                this.dtggrid.Rows[this.row].Cells[2].ReadOnly = true;
                this.dtggrid.Rows[this.row].Cells[3].ReadOnly = false;
            }
            else {
                globales.MessageBoxExclamation("Verificar que la cuenta no sea de cuenta mayor o tenga definido si es debe o haber","Aviso",globales.menuPrincipal);
            }
        }

        private void frmLibroDiarioDetalle_Load(object sender, EventArgs e)
        {
          
            string query = $"select * from cg_fondos.pol where poltipo = '{this.poltipo}' and polmes = '{this.polmes}' and polnumero = '{this.polnumero}' and anio = {this.polanio} order by  naturaleza";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            double debe = 0;
            double haber = 0;

            resultado.ForEach(o=>{
                if (Convert.ToString(o["naturaleza"]).ToUpper().Contains("D"))
                {
                    int modificado = this.dtggrid.Rows.Add(o["cuenta"], o["nombrecuenta"], string.Format("{0:c}", o["importe"]), "", o["grupo"], $"{o["doctipo"]}-{o["docnumero"]}", o["id"], o["naturaleza"], o["doctipo"], o["docrfc"], o["docgrupo"],o["doccomentario"], string.Format("{0:dd/MM/yyyy}", o["docfecha"]), o["docnumero"]);
                    debe += Convert.ToDouble(o["importe"]);

                    this.dtggrid.Rows[modificado].Cells[2].ReadOnly = false;
                    this.dtggrid.Rows[modificado].Cells[3].ReadOnly = true;
                    this.dtggrid.Rows[modificado].Cells[3].Style.BackColor = Color.LightGray;

                }
                else {
                    int modificado = this.dtggrid.Rows.Add(o["cuenta"], o["nombrecuenta"], "", string.Format("{0:c}", o["importe"]), o["grupo"], $"{o["doctipo"]}-{o["docnumero"]}", o["id"], o["naturaleza"], o["doctipo"], o["docrfc"], o["docgrupo"], o["doccomentario"], string.Format("{0:dd/MM/yyyy}", o["docfecha"]), o["docnumero"]);
                    haber += Convert.ToDouble(o["importe"]);
                    this.dtggrid.Rows[modificado].Cells[3].ReadOnly = false;
                    this.dtggrid.Rows[modificado].Cells[2].ReadOnly = true;
                    this.dtggrid.Rows[modificado].Cells[2].Style.BackColor = Color.LightGray;
                }
            });



            txtTipoPoliza.Text = Convert.ToString(datos["tipopol"]);
            prefijo = Convert.ToString(datos["prefijo"]);
            if ((Convert.ToString(datos["prefijo"])).ToUpper().Contains("B")) {
                txtPrefijo.Text = "NORMAL";
            }else if ((Convert.ToString(datos["prefijo"])).ToUpper().Contains("R"))
            {
                txtPrefijo.Text = "AJUSTE DE RESULTADOS";
            }else if ((Convert.ToString(datos["prefijo"])).ToUpper().Contains("P"))
            {
                txtPrefijo.Text = "AJUSTE PRESUPUESTAL";
            }else if ((Convert.ToString(datos["prefijo"])).ToUpper().Contains("X"))
            {
                txtPrefijo.Text = "CANCELADA";
            }
            txtNumero.Text = polnumero;
            txtFecha.Text = Convert.ToString(datos["fecha"]);
            txtComentario.Text = Convert.ToString(datos["concepto"]);
            txtElaborada.Text = Convert.ToString(datos["elaboro"]);
            txtIdPoliza.Text = (Convert.ToString(datos["prefijo"])).ToUpper().Contains("B") ? "NORMAL" : "CANCELADO";

            txtDebe.Text = string.Format("{0:c}",debe);
            txtHaber.Text = string.Format("{0:c}", haber);
        }

        private void dtggrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!abierto) globales.MessageBoxExclamation("Mes se encuentra ya cerrado, no se puede modificar","Aviso",this);
        }

        private void dtggrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.row + 1;
                var y = dtggrid.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                if (this.columna == 5) {
                    if (!this.abierto) {
                        return;
                    }


                   // this.ejecutarMetodo();

                }

                this.teclaEnter = false;
            }
        }

        private void ejecutarMetodo()
        {
            if (!abierto)
            {
                globales.MessageBoxExclamation("Mes cerrado, no se puede modificar", "Aviso", globales.menuPrincipal);

                return;
            }


            p1.Visible = true;


            DataGridViewRow item = this.dtggrid.Rows[this.row];
            string tipo = Convert.ToString(item.Cells[8].Value);
            switch (tipo)
            {
                case "CLC":
                    auxtipo.SelectedIndex = 0;
                    break;
                case "CH":
                    auxtipo.SelectedIndex = 1;
                    break;
                case "EO":
                    auxtipo.SelectedIndex = 2;
                    break;
                case "FACT":
                    auxtipo.SelectedIndex = 3;
                    break;
                case "FD":
                    auxtipo.SelectedIndex = 4;
                    break;
                case "REM":
                    auxtipo.SelectedIndex = 5;
                    break;
                case "NOTA":
                    auxtipo.SelectedIndex = 6;
                    break;
                case "OTB":
                    auxtipo.SelectedIndex = 7;
                    break;
                case "OTRO":
                    auxtipo.SelectedIndex = 8;
                    break;
                case "RBO":
                    auxtipo.SelectedIndex = 9;
                    break;
                case "SPEI":
                    auxtipo.SelectedIndex = 10;
                    break;
                default:
                    auxtipo.SelectedIndex = -1;
                    break;

            }


            auxrfc.Text = Convert.ToString(item.Cells[9].Value);
            auxgrupo.Text = auxrfc.Text = Convert.ToString(item.Cells[10].Value).Contains("000")?"NORMAL":"";
            auxcomentario.Text = Convert.ToString(item.Cells[11].Value);
            auxfecha.Text = Convert.ToString(item.Cells[12].Value);
            auxnumero.Text = Convert.ToString(item.Cells[13].Value);



        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.columna = e.ColumnIndex;

            if (this.columna == 5) {
               // this.ejecutarMetodo();
            }
        }

        private void dtggrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (!cargado) return;

            if (!abierto) {
                globales.MessageBoxExclamation("Mes cerrado, no se puede modificar","Aviso",globales.menuPrincipal);

                return;
            }

            if (e.RowIndex == -1)
                return;

            DataGridViewRow item = this.dtggrid.Rows[this.row];

            pol pol = new dbaseORM().queryForMap<pol>($"select * from cg_fondos.pol where id = {item.Cells[6].Value}");

            pol.cuenta = Convert.ToString(item.Cells[0].Value);
            pol.nombrecuenta = Convert.ToString(item.Cells[1].Value);
            if (Convert.ToString(item.Cells[7].Value) == "D")
            {
                pol.importe = globales.convertDouble(Convert.ToString(item.Cells[2].Value));
            }
            else if (Convert.ToString(item.Cells[7].Value) == "H")
            {
                pol.importe = globales.convertDouble(Convert.ToString(item.Cells[3].Value));
            }

            pol.grupo = Convert.ToString(item.Cells[4].Value);
            pol.doctipo = Convert.ToString(item.Cells[8].Value);
            pol.docrfc = Convert.ToString(item.Cells[9].Value);
            pol.docgrupo = Convert.ToString(item.Cells[10].Value);
            pol.doccomentario = Convert.ToString(item.Cells[11].Value);
            pol.docfecha = globales.convertDatetime(Convert.ToString(item.Cells[12].Value));
            pol.docnumero = Convert.ToString(item.Cells[13].Value);
            pol.naturaleza = Convert.ToString(item.Cells[7].Value);


            pol.cuenta = pol.cuenta.Replace(".","");

            new dbaseORM().update<pol>(pol);




            if (columna == 2  || columna == 3) {
                double debe = 0;
                double haber = 0;
                foreach (DataGridViewRow ii in this.dtggrid.Rows) {
                    debe += globales.convertDouble(Convert.ToString(ii.Cells[2].Value));
                    haber += globales.convertDouble(Convert.ToString(ii.Cells[3].Value));
                }


                txtDebe.Text = globales.convertMoneda(debe);
                txtHaber.Text = globales.convertMoneda(haber);
            }
        }

        private void dtggrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);
            
        }
        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void dtggrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (dtggrid.Rows.Count == 0) return;

            if (!this.abierto) {
                dtggrid.AllowUserToDeleteRows = false;
                return;
            };

            

            if (this.esConsulta)
            {
                return;
            }

            try
            {
                if (e.KeyCode == Keys.Insert)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nueva poliza?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;
                    string cuenta = "";
                    string descripcion = "";
                    string debe = "0.00";
                    string haber = "0.00";
                    string grupo = "";
                    string docto = "";

                    int rowfinal = row + 1;




                    dtggrid.Rows.Insert(rowfinal);
                    dtggrid.Rows[rowfinal].Cells[0].Value = cuenta;
                    dtggrid.Rows[rowfinal].Cells[1].Value = descripcion;
                    dtggrid.Rows[rowfinal].Cells[2].Value = debe;
                    dtggrid.Rows[rowfinal].Cells[3].Value = haber;
                    dtggrid.Rows[rowfinal].Cells[4].Value = grupo;
                    dtggrid.Rows[rowfinal].Cells[5].Value = docto;
                    dtggrid.Rows[rowfinal].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);


                    codigo.repositorios.cg_fondos.pol pol = new codigo.repositorios.cg_fondos.pol {
                        entidad = "801",
                        subsistema = "P",
                        polmes = this.polmes,
                        poltipo = this.poltipo,
                        polnumero = this.polnumero,
                        fecha = DateTime.Parse(txtFecha.Text),
                        cuenta = "",
                        naturaleza = "",
                        grupo = "000",
                        anio = this.polanio,
                        prefijo = this.prefijo
                    };

                    dbaseORM orm = new dbaseORM();
                    pol = orm.insert<codigo.repositorios.cg_fondos.pol>(pol,true);

                    //query = string.Format(query, txtrfc.Text, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(inicio)), string.Format("{0:yyyy-MM-dd}", DateTime.Parse(final)), cuenta, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fecha)), globales.id_usuario);

                    
                    dtggrid.Rows[row + 1].Cells[6].Value = pol.id;
                    dtggrid.CurrentCell = dtggrid.Rows[row + 1].Cells[0];

                }

                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Desea eliminar la poliza?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    string id = dtggrid.Rows[row].Cells[6].Value.ToString();
                    dtggrid.Rows.RemoveAt(row);
                    string query = "delete from cg_fondos.pol where id={0}";
                    query = string.Format(query,id);
                    if (globales.consulta(query, true))
                    {
                        globales.MessageBoxSuccess("Poliza eliminada correctamente", "Aviso", globales.menuPrincipal);
                    }
                }

                if (e.KeyCode == Keys.F1) {
                    frmCuentas cuenta = new frmCuentas(true);
                    cuenta.enviar = rellenar;
                    cuenta.ShowDialog();
                }

            
            }
            catch (Exception ee)
            {

            }

            this.esInsertar = false;
            if (e.KeyCode == Keys.Enter)
            {

                if (this.columna == 5) {
                   this.ejecutarMetodo();
                }
                try
                {
                    e.SuppressKeyPress = true;
                    int iColumn = dtggrid.CurrentCell.ColumnIndex;
                    int iRow = dtggrid.CurrentCell.RowIndex;
                    if (iColumn == dtggrid.ColumnCount - 1)
                    {
                        if (dtggrid.RowCount > (iRow + 1))
                        {
                            dtggrid.CurrentCell = dtggrid[1, iRow + 1];
                        }
                        else
                        {
                            //focus next control
                        }
                    }
                    else
                        dtggrid.CurrentCell = dtggrid[iColumn + 1, iRow];
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            p1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            p1.Visible = false;
            DataGridViewRow item = this.dtggrid.Rows[this.row];
            string doctipo = string.Empty;

            switch (auxtipo.SelectedIndex) {
                case 0:
                    doctipo = "CLC";
                    break;
                case 1:
                    doctipo = "CH";
                    break;
                case 2:
                    doctipo = "EO";
                    break;
                case 3:
                    doctipo = "FACT";
                    break;
                case 4:
                    doctipo = "FD";
                    break;
                case 5:
                    doctipo = "REM";
                    break;
                case 6:
                    doctipo = "NOTA";
                    break;
                case 7:
                    doctipo = "OTB";
                    break;
                case 8:
                    doctipo = "OTRO";
                    break;
                case 9:
                    doctipo = "RBO";
                    break;
                case 10:
                    doctipo = "SPEI";
                    break;

            }


            string rfc = auxrfc.Text;
            string grupo = (auxgrupo.SelectedIndex == 0)?"000":"";
            string doccomentario = auxcomentario.Text;
            string docfecha = auxfecha.Text;
            string docnumero = auxnumero.Text;


            item.Cells[5].Value = $"{doctipo}-{docnumero}";
            item.Cells[8].Value = doctipo;
            item.Cells[9].Value = rfc;
            item.Cells[10].Value = grupo;
            item.Cells[11].Value = doccomentario;
            item.Cells[12].Value = docfecha;
            item.Cells[13].Value = docnumero;


        }

        private void frmLibroDiarioDetalle_Shown(object sender, EventArgs e)
        {
            this.cargado = true;
        }

        public frmLibroDiarioDetalle(string polmes, int polanio,string polnumero,string poltipo,Dictionary<string,object> datos,bool abierto)
        {
            InitializeComponent();
            this.polmes = polmes;
            this.polanio = polanio;
            this.polnumero = polnumero;
            this.poltipo = poltipo;
            this.datos = datos;
            this.abierto = abierto;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}

