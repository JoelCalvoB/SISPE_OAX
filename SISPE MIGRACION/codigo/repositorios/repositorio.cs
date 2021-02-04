using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISPE_MIGRACION.codigo.repositorios
{
    //Esquemas de base de datos de postgresql
    public class autoincrementable : Attribute { }
    public class primeryKey : Attribute { }
    #region ESQUEMA POSTGRESQL DATOS

    namespace datos
    {
        public class empleados {
            [primeryKey]
            public string rfc { get; set; }
            public string nombre_em { get; set; }
            public string sexo { get; set; }
            public DateTime fecha_nac { get; set; }
            public string direccion { get; set; }
            public string proyecto { get; set; }
            public DateTime fecha_ing { get; set; }
            public string cve_categ { get; set; }
            public double sueldo_base { get; set; }
            public string tipo_rel { get; set; }
            public string depe { get; set; }
            public string status { get; set; }
            public double nap { get; set; }
            public string uuqm { get; set; }
            public DateTime fum { get; set; }
            public string hum { get; set; }
            public string rpe { get; set; }
            public Int32 antig_q { get; set; }
            public string unif { get; set; }
            public string nue { get; set; }
            [primeryKey]
            [autoincrementable]
            public Int32 id { get; set; }
            public bool pendiente { get; set; }
            public string curp { get; set; }
            public DateTime faclaracion { get; set; }
            public string taclaracion { get; set; }
            public Int32 idusuarioa { get; set; }
            public string modalidad { get; set; }
            public bool fallecio { get; set; }
            public DateTime fec_fallecio { get; set; }

            public string telefono { get; set; }

        }
        public class p_edocta {
            [primeryKey]
            public Int32 folio { get; set; }
            public string rfc { get; set; }
            public string nombre_em { get; set; }
            public string direccion { get; set; }
            public string proyecto { get; set; }
            public string secretaria { get; set; }
            public string descripcion { get; set; }
            public DateTime f_solicitud { get; set; }
            public DateTime f_emischeq { get; set; }
            public DateTime f_primdesc { get; set; }
            public Int32 antig_q { get; set; }
            public string tipo_pago { get; set; }
            public double plazo { get; set; }
            public double imp_unit { get; set; }
            public double importe { get; set; }
            public string ubic_pagare { get; set; }
            public DateTime f_convenio { get; set; }
            public string convenio { get; set; }
            public double int_convenio { get; set; }
            public double int_morat { get; set; }
            public string t_prestamo { get; set; }
            public bool historial_pagado { get; set; }
            public string fallecio { get; set; }



        }

        public class p_edocth {
            [primeryKey]
            public Int32 folio { get; set; }
            public string rfc { get; set; }
            public string nombre_em { get; set; }
            public string direccion { get; set; }
            public string proyecto { get; set; }
            public string secretaria { get; set; }
            public string descripcion { get; set; }
            public DateTime f_solicitud { get; set; }
            public DateTime f_emischeq { get; set; }
            public DateTime f_primdesc { get; set; }
            public Int32 antig_q { get; set; }
            public string tipo_pago { get; set; }
            public double plazo { get; set; }
            public double imp_unit { get; set; }
            public double importe { get; set; }
            public string ubic_pagare { get; set; }
            public double int_morat { get; set; }
            public DateTime f_convenio { get; set; }
            public string convenio { get; set; }
            public double int_conv { get; set; }

            public DateTime fallecio { get; set; }



        }

        public class descuentos {
            [primeryKey]
            public Int32 folio { get; set; }
            [primeryKey]
            [autoincrementable]
            public Int32 id { get; set; }
            [primeryKey]
            public string t_prestamo { get; set; }
            public DateTime f_descuento { get; set; }
            public string rfc { get; set; }
            public double numdesc { get; set; }
            public double totdesc { get; set; }
            public double importe { get; set; }
            public string cuenta { get; set; }
            public string proyecto { get; set; }
            public string tipo_rel { get; set; }
            public string usuario { get; set; }
            public DateTime fum { get; set; }
            public string hum { get; set; }
            public DateTime fac { get; set; }
            public string mac { get; set; }
            public DateTime f_registro { get; set; }
            public string archivo { get; set; }
            public DateTime desde { get; set; }
            public DateTime hasta { get; set; }
            public string curp { get; set; }
            public string status { get; set; }
            public Int32 idusuario { get; set; }
        }

        public class p_cajaq {
            [primeryKey]
            public double folio { get; set; }
            [primeryKey]
            [autoincrementable]
            public Int32 id { get; set; }
            [primeryKey]
            public string t_prestamo { get; set; }
            public DateTime f_descuento { get; set; }
            public string rfc { get; set; }
            public string nombre_em { get; set; }
            public string secretaria { get; set; }
            public string descripcion { get; set; }
            public Int32 descuentos { get; set; }
            public double deldesc { get; set; }
            public double numdesc { get; set; }
            public double plazo { get; set; }
            public double imp_unit { get; set; }
            public double imp_unit_cap { get; set; }
            public double imp_unit_int { get; set; }
            public string imp_unit_capl { get; set; }
            public string imp_unit_intl { get; set; }
            public double total { get; set; }
            public string status { get; set; }
            public DateTime fum { get; set; }
            public string hum { get; set; }
            public DateTime fac { get; set; }
           
        }
        public class p_marcha {
            [primeryKey]
            public Int32 folio { get; set; }
            public string rfc { get; set; }
            public string nombre_em { get; set; }
            public string proyecto { get; set; }
            public double sueldo_base { get; set; }
            public string depe { get; set; }
            public string descripcion { get; set; }
            public DateTime f_acaec { get; set; }
            public string pers_cobro { get; set; }
            public string parentesco { get; set; }
            public DateTime f_cobro { get; set; }
            public Int32 meses { get; set; }
            public double monto { get; set; }
            public double descuentos { get; set; }
            public double liquido { get; set; }
            public string concepto_desc { get; set; }
            public string n_cheque { get; set; }
            public string liq_letra { get; set; }
            public DateTime f_recibo { get; set; }
        }

        public class solicitud_dependencias {
            [primeryKey]
            public Int32 folio { get; set; }
            [primeryKey]
            public string tipo_mov { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public string tipo_rel { get; set; }
            public DateTime f_descuento { get; set; }
            public double numdesc { get; set; }
            public double totdesc { get; set; }
            public double imp_unit { get; set; }
            public Int32 folio_ { get; set; }
            public string tipo_rel_ { get; set; }
            public double numdesc_ { get; set; }
            public double totdesc_ { get; set; }
            public double imp_unit_ { get; set; }
            public string rfc { get; set; }
            public string nombre_em { get; set; }
            public string proyecto { get; set; }
            [primeryKey]
            public string t_prestamo { get; set; }
        }
        public class p_hipote
        {
            [primeryKey]
            public Int32 folio { get; set; }
            public string rfc { get; set; }
            public string nombre_em { get; set; }
            public string sexo { get; set; }
            public DateTime fecha_nac { get; set; }
            public string direccion { get; set; }
            public string proyecto { get; set; }
            public string nombre_cony { get; set; }
            public string cve_categ { get; set; }
            public double sueldo_base { get; set; }
            public string tipo_rel { get; set; }
            public string nomina { get; set; }
            public string secretaria { get; set; }
            public string descripcion { get; set; }
            public Int32 edad { get; set; }
            public string edo_civil { get; set; }
            public string tel_partic { get; set; }
            public string tel_ofici { get; set; }
            public string direc_inmu { get; set; }
            public DateTime f_nombram { get; set; }
            public Int32 ant_a { get; set; }
            public string ccatdes { get; set; }
        }

        public class h_solici
        {
            [autoincrementable]
            [primeryKey]
            public Int32 id { get; set; }
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public DateTime f_solicitud { get; set; }
            public string finalidad { get; set; }
            public string descri_finalid { get; set; }
            public Int32 plazoa { get; set; }
            public DateTime f_autorizacion { get; set; }
            public DateTime f_rev_exped { get; set; }
            public double cap_prest { get; set; }
            public double int_prest { get; set; }
            public double tot_prest { get; set; }
            public double cap_prim { get; set; }
            public double int_prim { get; set; }
            public double tot_prim { get; set; }
            public double cap_unit { get; set; }
            public double int_unit { get; set; }
            public double tot_unit { get; set; }
            public string plazo { get; set; }
            public string status { get; set; }
            public string trel { get; set; }
        }


        public class h_sdepec
        {
            [autoincrementable]
            [primeryKey]
            public Int32 id { get; set; }
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }

            public string nom_depec { get; set; }
            public Int32 edad { get; set; }
            public string parentesco { get; set; }
            public string escolaridad { get; set; }
            public string ocupacion { get; set; }
        }

        public class h_sestse
        {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public DateTime f_elab { get; set; }
            public string a_condic { get; set; }
            public Int32 a_ncuartos { get; set; }
            public string a_pisos { get; set; }
            public string a_ilumin { get; set; }
            public string a_ventil { get; set; }
            public string a_pared { get; set; }
            public string a_techo { get; set; }
            public string a_servsanit { get; set; }
            public string a_patio { get; set; }
            public string b_estufa { get; set; }
            public string b_refri { get; set; }
            public string b_comedor { get; set; }
            public string b_sala { get; set; }
            public string b_gabinete { get; set; }
            public string b_roperos { get; set; }
            public string b_camas { get; set; }
            public string b_licuad { get; set; }
            public string b_tv { get; set; }
            public string b_radio { get; set; }
            public string b_lavad { get; set; }
            public string b_videoc { get; set; }
            public string b_ventil { get; set; }
            public string b_maqcoser { get; set; }
            public string b_vehic { get; set; }
            public string b_otros { get; set; }
            public double difer_i_e { get; set; }
            public string observ { get; set; }
            public string conclus { get; set; }
            public double i_sueldo { get; set; }
            public double i_ayuda { get; set; }
            public double i_quinq { get; set; }
            public double i_otros { get; set; }
            public double i_conyu { get; set; }
            public double i_hijos { get; set; }
            public double i_otrosf { get; set; }
            public double i_total { get; set; }
            public double e_quiro { get; set; }
            public double e_hipo { get; set; }
            public double e_direc { get; set; }
            public double e_lbca { get; set; }
            public double e_fpens { get; set; }
            public double e_ss { get; set; }
            public double e_csindic { get; set; }
            public double e_ispt { get; set; }
            public double e_otros { get; set; }
            public double e_alim { get; set; }
            public double e_vestu { get; set; }
            public double e_transp { get; set; }
            public double e_renta { get; set; }
            public double e_agua { get; set; }
            public double e_luz { get; set; }
            public double e_gas { get; set; }
            public double e_tel { get; set; }
            public double e_coleg { get; set; }
            public double e_servid { get; set; }
            public double e_otrosf { get; set; }
            public double e_total { get; set; }
        }

        public class h_sretec
        {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public DateTime f_elab { get; set; }
            public double areapredio { get; set; }
            public Int32 niveles { get; set; }
            public Int32 habitac { get; set; }
            public string desghabit { get; set; }
            public double area_const { get; set; }
            public double avaobneg { get; set; }
            public double avaacab { get; set; }
            public double imp_estimt { get; set; }
            public double imp_avance { get; set; }
            public double imp_faltante { get; set; }
            public string observ { get; set; }
            public string diagnostico { get; set; }
        }

        public class h_enotar
        {
            [primeryKey]
            public Int32 expediente { get; set; }
            public Int32 n_notario { get; set; }
            public string nombre_not { get; set; }
            public DateTime f_inscr_n { get; set; }
            public double n_acta_n { get; set; }
            public double n_volu_n { get; set; }
        }

        public class h_sconvm
        {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public DateTime f_convm { get; set; }
            public string nombre_t1 { get; set; }
            public string nombre_t2 { get; set; }
            public string direc_1 { get; set; }
            public string direc_2 { get; set; }
        }

        public class h_snotif
        {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public string numemis { get; set; }
            public DateTime f_notif { get; set; }
            public string n_notif { get; set; }
            public string c_notif { get; set; }
            public string t_notif { get; set; }
        }

        public class h_simpro {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public DateTime f_noproc { get; set; }
            public string t_noproc { get; set; }
        }

        public class h_semisi {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public string n_emision { get; set; }
            public DateTime f_recibo { get; set; }
            public double importe { get; set; }
        }

        public class h_santec {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public Int32 tomo_inscr { get; set; }
            public Int32 libr_inscr { get; set; }
            public string dist_judic { get; set; }
            public DateTime f_inscr_rp { get; set; }
        }

        public class h_spagar {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public DateTime f_pagare { get; set; }
            public DateTime f_primdesc { get; set; }
        }

        public class h_sconsj {
            [primeryKey]
            public Int32 expediente { get; set; }
            [primeryKey]
            public string sec { get; set; }
            public string proyecto { get; set; }
            public string cred_ant { get; set; }
            public string avance_o { get; set; }
            public string necesida { get; set; }
            public string solvencia { get; set; }
            public string observacion { get; set; }
        }
    }
    #endregion

    #region ESQUEMA POSTGRESQL catalogos

    namespace catalogos
    {
        public class movimientos
        {
            public string tipo_mov { get; set; }
            [primeryKey]
            [autoincrementable]
            public Int32 id { get; set; }
            public string tipo { get; set; }
            public string movimiento { get; set; }

        }

        public class cuentas {
            public string proy { get; set; }
            public  string cuenta { get; set; }
            public string descripcion { get; set; }
            public string secretar { get; set; }
            public string x { get; set; }
            public string tipo_dep { get; set; }
            public string proy_99 { get; set; }
        }
    }
    #endregion

    namespace financieros {
        public class pol {
            public string entidad { get; set; }
            public string subsistema { get; set; }
            public string polmes { get; set; }
            public string poltipo { get; set; }
            public string polnumero { get; set; }
            public DateTime fecha { get; set; }
            public string cuenta { get; set; }
            public string naturaleza { get; set; }
            public double importe { get; set; }
            public string grupo { get; set; }
            public string prefijo { get; set; }
            public string doctipo { get; set; }
            public string docnumero { get; set; }
            public DateTime docfecha { get; set; }
            public string docrfc { get; set; }
            public string doccomentario { get; set; }
            public string docgrupo { get; set; }
            public string status { get; set; }
            public string linea { get; set; }
        }

        public class cuentas { //repor

            public string cuenta { get; set; }
            public string descripcion { get; set; }

            [primeryKey]
            public Int32 id { get; set; }
            public string naturaleza { get; set; }
            public Int32 nivel { get; set; }
            public Int32 tipo { get; set; }
            public String posicion { get; set; }
            public String estado { get; set; }
            public String ue { get; set; }
            public Int32 estructura  { get; set; }



        }
    }

    namespace nominas_catalogos {
        public class maestro {

            [primeryKey]
            public string jpp { get; set; }
            public string clave { get; set; }
            [primeryKey]
            public Int32 num { get; set; }
            public string rfc { get; set; }
            public string nombre { get; set; }
            public DateTime fnacimien { get; set; }
            public string categ { get; set; }
            public string curp { get; set; }
            public string sexo { get; set; }
            public string proyecto { get; set; }
            public string domicilio { get; set; }
            public Int32 codigop { get; set; }
            public string telefono { get; set; }
            public string imss { get; set; }
            public DateTime fching { get; set; }
            public string nivel { get; set; }
            public string nomelec { get; set; }
            public string leyen { get; set; }
            public string tiporel { get; set; }
            public string superviven { get; set; }
            public string dire_super { get; set; }
            public DateTime fsupervive { get; set; }
            public string cuentabanc { get; set; }
            public string banco { get; set; }
            public string tipopen { get; set; }
            public string rfc_homo { get; set; }
            public string nosuspen { get; set; }
            public DateTime fechasus { get; set; }
            public DateTime fechaeje { get; set; }
            public string fcapsus { get; set; }
            public DateTime fcedula { get; set; }
            public Int32 anios { get; set; }
            public Int32 meses { get; set; }
            public Int32 quinquenios { get; set; }
            public string f_baja { get; set; }
            public string motivo { get; set; }
            public Int32 ley { get; set; }
            public string usuario_sis { get; set; }
            public string f_captura { get; set; }
            public string cve_reg { get; set; }
            public string cve_dto { get; set; }
            public string cve_mun { get; set; }
            public string cve_loc { get; set; }
            public string correo { get; set; }
            public Int32 dias_aguinaldo { get; set; }
            public string huella { get; set; }
            public string f_impresion { get; set; }
            public string num_susp_seguro { get; set; }
            public DateTime fec_susp_seguro { get; set; }
            public DateTime fec_susp_ejec { get; set; }
            public Int32 id { get; set; }
            public bool d_madres { get; set; }

            public string llave_qr { get; set; }

        }

        public class supervive {
            [primeryKey]
            public string jpp { get; set; }
            [primeryKey]
            public Int32 numjpp { get; set; }
            public Int32 anio { get; set; }
            public Int32 periodo { get; set; }
            public string tomafirma { get; set; }
            public string observaciones { get; set; }
            public Int32 tipo { get; set; }
            public DateTime fecha { get; set; }
            public Int32 firma { get; set; }
            public string q_captura { get; set; }
            public DateTime f_captura { get; set; }
        }
        public class pension_alimenticia {
            [primeryKey]
            public string jpp { get; set; }
            [primeryKey]
            public Int32 numjpp { get; set; }
            [primeryKey]
            public Int32 numpea { get; set; }
            public Int32 id_enlace { get; set; }
            public double descuento { get; set; }
            [primeryKey]
            [autoincrementable]
            public Int32 id { get; set; }
            public double total { get; set; }
            public Int32 id_enlacepea { get; set; }
            public string claves_vinculadas { get; set; }
        }

        public class nominew {
            [primeryKey]
            public string jpp { get; set; }
            [primeryKey]
            public Int32 numjpp { get; set; }
            [primeryKey]
            public Int32 clave { get; set; }
            public Int32 secuen { get; set; }
            public string descri { get; set; }
            public double monto { get; set; }
            public Int32 pagon { get; set; }
            public Int32 pagot { get; set; }
            public string tipopago { get; set; }
            public string leyen { get; set; }
            public string q_captura { get; set; }
            public DateTime f_captura { get; set; }
            public Int32 folio { get; set; }
            public DateTime fechaini { get; set; }
            public DateTime fechafin { get; set; }
            public Int32 cheque { get; set; }
            public string tipo_nomina { get; set; }
            [primeryKey]
            [autoincrementable]
            public Int32 id { get; set; }

        }
    }

    namespace cg_fondos {
        public class pol {
            public string entidad { get; set; }
            public string subsistema { get; set; }
            public string polmes { get; set; }
            public string poltipo { get; set; }
            public string polnumero { get; set; }
            public DateTime fecha { get; set; }
            public string cuenta { get; set; }
            public string naturaleza { get; set; }
            public double importe { get; set; }
            public string grupo { get; set; }
            public string prefijo { get; set; }
            public string doctipo { get; set; }
            public string docnumero { get; set; }
            public DateTime docfecha { get; set; }
            public string docrfc { get; set; }
            public string doccomentario { get; set; }
            public string docgrupo { get; set; }
            public string status { get; set; }
            public string linea { get; set; }
            public Int32 anio { get; set; }

            [primeryKey]
            [autoincrementable]
            public Int32 id { get; set; }
            public string nombrecuenta { get; set; }
        }
    }
   
}
