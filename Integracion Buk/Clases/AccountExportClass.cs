using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracion_Buk.Clases
{
    public class AccountExportClass
    {
        public Data data { get; set; }

    }

    public class Data
    {

        public Dictionary<string, List<List<Registro>>> Values { get; set; }
    }

    public class Registro
    {
        public string correlativo { get; set; }
        public string cuenta_de_mayor_codigo_sn { get; set; }
        public string cuenta_de_mayor_nombre_sn { get; set; }
        public string cuenta_asociada { get; set; }
        public string debito_moneda { get; set; }
        public double? debito { get; set; }
        public string credito_moneda { get; set; }
        public double? credito { get; set; }
        public string debito_ms_moneda { get; set; }
        public double? debito_ms { get; set; }
        public string credito_ms_moneda { get; set; }
        public double? credito_ms { get; set; }
        public string posicion_del_formulario_principal { get; set; }
        public string sede { get; set; }
        public string tipo_paciente { get; set; }
        public string unidad_de_negocio { get; set; }
        public string centro_de_costo { get; set; }
        public string bloqueo_de_pago { get; set; }
        public string motivo_del_bloqueo { get; set; }
        public string ejecucion_de_orden_de_pago { get; set; }
        public string cuenta_patrimonial { get; set; }
        public string sujeto_a_percepcion { get; set; }
        public string incluye_percepcion { get; set; }
        public string porcentaje_percepcion { get; set; }
        public string monto_percepcion { get; set; }
        public string monto_afecto_percepcion { get; set; }
        public string tipo_contabilizacion { get; set; }
        public string canal_sn { get; set; }
        public string tipo_documento { get; set; }
        public string numero_documento { get; set; }
        public string razon_social { get; set; }
        public string prefactura { get; set; }
        public string total_doc { get; set; }
        public string fecha_doc { get; set; }
        public string fecha_vencimiento { get; set; }
        public string estado_doc_pago { get; set; }
        public string descr_grupo { get; set; }
        public string cod_sub_dimension_3 { get; set; }
        public string descr_sub_grupo { get; set; }
        public string cod_tarifa { get; set; }
        public string descr_tarifa { get; set; }
        public string monto_de_linea { get; set; }
        public string grupo_medico { get; set; }
        public string cod_medico { get; set; }
        public string nombre_medico { get; set; }
        public string tipo_honorario { get; set; }
        public string boekey { get; set; }
        public string docentry { get; set; }
        public string codigo_proveedor { get; set; }
        public string serie_documento { get; set; }
        public string posicion_flujo_efectivo { get; set; }
        public string exx_deduccion { get; set; }
        public string nombre_de_paciente { get; set; }
    }
}