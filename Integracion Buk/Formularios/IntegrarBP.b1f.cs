using Integracion_Buk.Clases;
using Integracion_Buk.Helper;
using Newtonsoft.Json;
using RestSharp;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Integracion_Buk.Formularios
{
    [FormAttribute("Integracion_Buk.Formularios.IntegrarBP", "Formularios/IntegrarBP.b1f")]
    class IntegrarBP : UserFormBase
    {
        public IntegrarBP()
        {
        }


        List<ColaboradorBUK.Datum> employees;
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("btnProc").Specific));
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }


        private string URL_EMP;
        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {
            SAPbobsCOM.Recordset oRS = ((SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
            try
            {

                string strSQL = "";
                oRS = ((SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));

                strSQL = "SELECT \"U_EXX_VALOR\" FROM \"@BUK_CONF\" WHERE \"Code\" = 'URL_EMP' ";
                oRS.DoQuery(strSQL);

                if (oRS.RecordCount > 0)
                {
                    for (int i = 0; i < oRS.RecordCount; i++)
                    {
                        URL_EMP = oRS.Fields.Item(0).Value.ToString();
                        oRS.MoveNext();
                    }
                }

                if (oRS != null)
                {
                    Marshal.ReleaseComObject(oRS);
                    oRS = null;
                    GC.Collect();
                }

                ConsultarEmpleadosPendientes();
            }
            catch (Exception ex)
            {
                if (oRS != null)
                {
                    Marshal.ReleaseComObject(oRS);
                    oRS = null;
                    GC.Collect();
                }
                Application.SBO_Application.StatusBar.SetText(ex.StackTrace, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }


        }

        private void ConsultarEmpleadosPendientes()
        {
            try
            {
                var url = URL_EMP;// "https://mired.buk.pe/api/v1/peru/employees"; //URL_XSJS+ "https://10.100.80.7:4300/WS/services/businesspartners/getEmployees.xsjs";
                employees = new List<ColaboradorBUK.Datum>();
                //Application.SBO_Application.StatusBar.SetText(url, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                request.AddHeader("auth_token", "PihpLPT6c7UcnkExVTvcBDhs");
                var response = client.Execute(request);

                //WebRequest request = WebRequest.Create(url);
                //request.Headers.Add("auth_token", "PihpLPT6c7UcnkExVTvcBDhs");


                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content;
                var pages = 0;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //using (var reader = new StreamReader(response.GetResponseStream()))
                    //{
                    //    content = reader.ReadToEnd();
                    //}
                    var result = JsonConvert.DeserializeObject<ColaboradorBUK>(response.Content);

                    pages = result.pagination.total_pages - 1;
                    employees.AddRange(result.data);
                    //Application.SBO_Application.StatusBar.SetText("total " + employees.Count.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                }
                else
                {
                    Application.SBO_Application.StatusBar.SetText(response.Content, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                }
                int cont = 2;
                if (pages > 0)
                {

                    do
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                        url = url + "?page=" + cont.ToString();
                        client = new RestClient(url);
                        request = new RestRequest(Method.GET);
                        request.AddHeader("auth_token", "PihpLPT6c7UcnkExVTvcBDhs");
                        response = client.Execute(request);
                        //Application.SBO_Application.StatusBar.SetText("cont " + cont, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        //Application.SBO_Application.StatusBar.SetText("pages " + pages, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = JsonConvert.DeserializeObject<ColaboradorBUK>(response.Content);

                            employees.AddRange(result.data);
                            //Application.SBO_Application.StatusBar.SetText("total " + employees.Count.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                            cont++;
                            pages--;
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText(response.Content, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                        }

                    }
                    while (pages > 0);
                }



                employees = employees.Where(t => t.status == "activo").ToList();
                EditText0.Value = employees.Count.ToString();
            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(ex.StackTrace, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

            }
        }

        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;

        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                var url = Globals.ServiceLayerUrl + Globals.BusinessPartners;
                int cont = 0;
                if (employees.Count > 0)
                {
                    ServiceLayerHelper.Connect();
                    foreach (var item in employees)
                    {

                        BusinessPartnerSL bp = new BusinessPartnerSL();

                        bp.CardName = item.full_name;
                        bp.EmailAddress = item.email;

                        //string documentNumber = item.document_number.ToString();
                        //int totalLength = 12;
                        //int prefixLength = totalLength - documentNumber.Length;
                        //string prefix = "E" + new string('0', prefixLength - 1); // -1 porque la "E" ocupa un espacio

                        bp.CardCode = "E000" + item.document_number;
                        bp.CardType = "S";
                        bp.GroupCode = 104;
                        bp.Currency = "##";
                        bp.FederalTaxID = item.document_number;
                        bp.Phone2 = item.phone;

                        //Campos de Usuario
                        bp.U_EXX_APELLMAT = item.second_surname;
                        bp.U_EXX_APELLPAT = item.surname;
                        bp.U_EXX_PRIMERNO = item.first_name.Split(' ')[0].ToString();
                        try
                        {
                            bp.U_EXX_SEGUNDNO = item.first_name.Split(' ')[1].ToString();
                        }
                        catch (Exception)
                        {

                        }
                   
                        bp.U_EXX_TIPOPERS = "TPN";

                        switch (item.document_type)
                        {
                            case "dni":
                                bp.U_EXX_TIPODOCU = "1";
                                break;
                            case "carnet_extranjeria":
                                bp.U_EXX_TIPODOCU = "4";
                                break;
                            default:
                                bp.U_EXX_TIPODOCU = "0";
                                break;
                        }

                        switch (item.account_type)
                        {
                            case "Cuenta de Ahorros":
                                bp.U_ModPago = "2";
                                break;
                            case "Cuenta Corriente":
                                bp.U_ModPago = "3";
                                break;
                            default:

                                break;
                        }


                        BPAddress address = new BPAddress();
                        address.AddressName = "FISCAL";
                        address.ZipCode = item.distrito;
                        address.County = item.provincia;
                        address.State = getCodState(item.departamento);
                        address.Country = "PE";
                        address.AddressType = "bo_BillTo";
                        bp.BPAddresses = new List<BPAddress>();
                        bp.BPAddresses.Add(address);


                        bp.U_EXO_SEGURO = "N";
                        bp.U_EXO_CLASIFICA = "0007";


                        var jsonBody = JsonConvert.SerializeObject(bp, new JsonSerializerSettings
                        {
                            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                        });
                        Application.SBO_Application.StatusBar.SetText("json " + jsonBody, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        
                        var response = ServiceLayerHelper.PostSL(url, jsonBody);

                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            Application.SBO_Application.StatusBar.SetText(bp.CardCode +" creado", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                            cont++;

                            
                        }
                        else
                        {
                             response = ServiceLayerHelper.PatchSL(url+"('"+bp.CardCode+ "')", jsonBody);

                            if (response.StatusCode == HttpStatusCode.Created)
                            {
                                Application.SBO_Application.StatusBar.SetText(bp.CardCode + " creado", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                                cont++;


                            }
                            else
                            {


                            }
                            //Application.SBO_Application.StatusBar.SetText("error sl " + response.StatusCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                            // Application.SBO_Application.StatusBar.SetText("error sl "+response.Content , SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                            cont++;

                        }

                    }

                }

                EditText1.Value = cont.ToString();
                Application.SBO_Application.StatusBar.SetText("Proceso Terminado", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                Application.SBO_Application.StatusBar.SetText(ex.StackTrace, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

            }

        }

        private string getCodState(string departamento)
        {
            string code = "";
            SAPbobsCOM.Recordset oRS = ((SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
            try
            {

                string strSQL = "";
                oRS = ((SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));


                strSQL = "SELECT \"Code\" FROM \"OCST\" WHERE \"Name\" = '"+ departamento.ToUpper()+"'";
                oRS.DoQuery(strSQL);

                if (oRS.RecordCount > 0)
                {
                    for (int i = 0; i < oRS.RecordCount; i++)
                    {
                        code = oRS.Fields.Item(0).Value.ToString();
                        oRS.MoveNext();
                    }
                }

                if (oRS != null)
                {
                    Marshal.ReleaseComObject(oRS);
                    oRS = null;
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                if (oRS != null)
                {
                    Marshal.ReleaseComObject(oRS);
                    oRS = null;
                    GC.Collect();
                }
                Application.SBO_Application.StatusBar.SetText(ex.StackTrace, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }

            return code = "";

        }
    }
}
