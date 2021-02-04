using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//===============================================================
//==         SECCIÓN CREADA PARA HACER REFERENCIA              ==
//==         A  LA BASE DE DATOS                               ==
//===============================================================

namespace SISPE_MIGRACION.codigo.baseDatos.repositorios
{

    //===============================================================
    //==                    TABLA P_QUIROG                         ==
    //===============================================================
    class p_quirog
    {
        public double folio { get; set; }
        public string rfc { get; set; }
        public string nombre_em { get; set; }
        public string sexo { get; set; }
        public string fecha_nac { get; set; }
        public string direccion { get; set; }
        public string proyecto { get; set; }
        public string cve_categ { get; set; }
        public double sueldo_base { get; set; }
        public string tipo_rel { get; set; }
        public string secretaria { get; set; }
        public string descripcion { get; set; }
        public double nap { get; set; }
        public string nue { get; set; }
        public string f_solicitud { get; set; }
        public string f_ultpresta { get; set; }
        public string f_emischeq { get; set; }
        public string f_primdesc { get; set; }
        public string f_reprogra { get; set; }
        public string f_ultmode { get; set; }
        public int ant_a { get; set; }
        public int ant_m { get; set; }
        public int ant_q { get; set; }
        public int antig_q { get; set; }
        public double sueldo_m { get; set; }
        public double meses_corres { get; set; }
        public string telefono { get; set; }
        public string extencion { get; set; }
        public char tipo_pago { get; set; }
        public double plazo { get; set; }
        public double pagos { get; set; }
        public double imp_unit { get; set; }
        public double importe { get; set; }
        public double interes { get; set; }
        public double fondo_g { get; set; }
        public double otros_desc { get; set; }
        public string otros_conc { get; set; }
        public double liquido { get; set; }
        public char status_ { get; set; }
        public char trel { get; set; }
        public char aceptado { get; set; }
        public double secuen { get; set; }
        public double porc { get; set; }
        public char carta { get; set; }
        public DateTime f_elabora { get; set; }
        public List<d_quirog> lista { get; set; }
    }
    //===============================================================
    //==                    TABLA D_QUIROG                         ==
    //===============================================================

    class d_quirog
    {
        public double folio { get; set; }
        public string rfc { get; set; }
        public string nombre_em { get; set; }
        public string direccion { get; set; }
        public string proyecto { get; set; }
        public double nap { get; set; }
        public string nue { get; set; }
        public int antig { get; set; }
        public string _null { get; set; }
    }

    public class p_cajaQ
    {
        public string folio { get; set; }
        public string f_descuento { get; set; }
        public string rfc { get; set; }
        public string nombre_em { get; set; }
        public string secretaria { get; set; }
        public string descripcion { get; set; }
        public int descuentos { get; set; }
        public int deldescuentos { get; set; }
        public int numdesc { get; set; }
        public int plazo { get; set; }
        public string imp_unit_capl { get; set; }
        public string imp_unit_intl { get; set; }
        public string status { get; set; }
        public string hum { get; set; }
        public string fum { get; set; }
        public double imp_unit{ get; set; }
        public double imp_unit_cap { get; set; }
        public double imp_unit_int { get; set; }
        public double total { get; set;
        }
    }
}
