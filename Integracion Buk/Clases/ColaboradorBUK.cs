using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracion_Buk.Clases
{
    public class ColaboradorBUK
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Boss
        {
            public int id { get; set; }
            public string document_type { get; set; }
            public string document_number { get; set; }
        }

        public class CostCenter
        {
            public int id { get; set; }
            public int job_id { get; set; }
            public double weight { get; set; }
            public string cost_center { get; set; }
        }

        public class CurrentJob
        {
            public string periodicity { get; set; }
            public string frequency { get; set; }
            public object working_schedule_type { get; set; }
            public bool zone_assignment { get; set; }
            public object union { get; set; }
            public object project { get; set; }
            public List<string> days { get; set; }
            public int? previous_job_id { get; set; }
            public int id { get; set; }
            public int company_id { get; set; }
            public string start_date { get; set; }
            public object end_date { get; set; }
            public int area_id { get; set; }
            public string contract_term { get; set; }
            public object notice_date { get; set; }
            public string contract_type { get; set; }
            public string employment_regimen { get; set; }
            public object worker_type { get; set; }
            public object fola_insurance { get; set; }
            public object fola_plan { get; set; }
            public bool ria_regimen { get; set; }
            public string t_register_kind { get; set; }
            public string t_register_special_regimen { get; set; }
            public double regular_hours { get; set; }
            public string cost_center { get; set; }
            public object active_until { get; set; }
            public Boss boss { get; set; }
            public List<string> dias_laborales { get; set; }
            public string currency_code { get; set; }
            public Role role { get; set; }
            public List<CostCenter> cost_centers { get; set; }
            public CustomAttributes custom_attributes { get; set; }
            public RecintoPrimario recinto_primario { get; set; }
            public List<object> recintos_secundarios { get; set; }
        }

        public class CustomAttributes
        {
            [JsonProperty("Carnet de Extranjeria")]
            public string CarnetdeExtranjeria { get; set; }

            [JsonProperty("Celular asignado")]
            public string Celularasignado { get; set; }
            public string Cochera { get; set; }
            public string DNI { get; set; }

            [JsonProperty("DNI derecho habientes")]
            public string DNIderechohabientes { get; set; }
            public string Especialidad { get; set; }

            [JsonProperty("Grado de Instrucción")]
            public string GradodeInstruccin { get; set; }

            [JsonProperty("Horas plame")]
            public double? Horasplame { get; set; }

            [JsonProperty("Uniforme asignado")]
            public string Uniformeasignado { get; set; }

            [JsonProperty("Nueva Tasa Vida ley")]
            public string NuevaTasaVidaley { get; set; }

            [JsonProperty("Tabla vida ley")]
            public string Tablavidaley { get; set; }
        }

        public class Datum
        {
            public int person_id { get; set; }
            public int id { get; set; }
            public string picture_url { get; set; }
            public string first_name { get; set; }
            public string surname { get; set; }
            public string second_surname { get; set; }
            public string full_name { get; set; }
            public string document_type { get; set; }
            public string document_number { get; set; }
            public string code_sheet { get; set; }
            public string email { get; set; }
            public string personal_email { get; set; }
            public string address { get; set; }
            public int location_id { get; set; }
            public string provincia { get; set; }
            public string distrito { get; set; }
            public string departamento { get; set; }
            public string office_phone { get; set; }
            public string phone { get; set; }
            public string gender { get; set; }
            public string birthday { get; set; }
            public string university { get; set; }
            public string degree { get; set; }
            public string education_end_date { get; set; }
            public string education_status { get; set; }
            public string active_since { get; set; }
            public string status { get; set; }
            public string payment_method { get; set; }
            public string bank { get; set; }
            public string account_type { get; set; }
            public string account_number { get; set; }
            public object company_payment_bank { get; set; }
            public string company_bank_account_number { get; set; }
            public bool private_role { get; set; }
            public object progressive_vacations_start { get; set; }
            public string nationality { get; set; }
            public string country_code { get; set; }
            public string civil_status { get; set; }
            public string health_company { get; set; }
            public string pension_regime { get; set; }
            public string pension_fund { get; set; }
            public object afc { get; set; }
            public bool retired { get; set; }
            public object retirement_regime { get; set; }
            public CustomAttributes custom_attributes { get; set; }
            public object active_until { get; set; }
            public object termination_reason { get; set; }
            public CurrentJob current_job { get; set; }
            public List<Job> jobs { get; set; }
            public List<FamilyResponsability> family_responsabilities { get; set; }
        }

        public class FamilyResponsability
        {
            public int id { get; set; }
            public string family_allowance_section { get; set; }
            public int simple_family_responsability { get; set; }
            public int maternity_family_responsability { get; set; }
            public int invalid_family_responsability { get; set; }
            public string start_date { get; set; }
            public object end_date { get; set; }
            public List<object> responsability_details { get; set; }
        }

        public class Job
        {
            public int id { get; set; }
            public int company_id { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public int area_id { get; set; }
            public string contract_term { get; set; }
            public object notice_date { get; set; }
            public string contract_type { get; set; }
            public string employment_regimen { get; set; }
            public object worker_type { get; set; }
            public object fola_insurance { get; set; }
            public object fola_plan { get; set; }
            public string periodicity { get; set; }
            public bool ria_regimen { get; set; }
            public string t_register_kind { get; set; }
            public string t_register_special_regimen { get; set; }
            public double regular_hours { get; set; }
            public string cost_center { get; set; }
            public object active_until { get; set; }
            public object project { get; set; }
            public List<string> days { get; set; }
            public Boss boss { get; set; }
            public List<string> dias_laborales { get; set; }
            public string currency_code { get; set; }
            public Role role { get; set; }
            public List<CostCenter> cost_centers { get; set; }
            public CustomAttributes custom_attributes { get; set; }
        }

        public class Pagination
        {
            public string next { get; set; }
            public object previous { get; set; }
            public int count { get; set; }
            public int total_pages { get; set; }
        }

        public class RecintoPrimario
        {
            public int id { get; set; }
            public string code { get; set; }
        }

        public class Role
        {
            public int id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string requirements { get; set; }
            public List<int> area_ids { get; set; }
            public RoleFamily role_family { get; set; }
        }

        public class RoleFamily
        {
            public int id { get; set; }
            public string name { get; set; }
            public int quantity_of_roles { get; set; }
        }


        public Pagination pagination { get; set; }
        public List<Datum> data { get; set; }





    }
}
