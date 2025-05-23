using System;
using System.Collections.Generic;
using System.Xml;
using SAPbouiCOM.Framework;
using System.Net;
using System.IO;
using System.Text.Json;
using RestSharp;
using Integracion_Buk.Clases;
using Newtonsoft.Json;
using Integracion_Buk.Helper;
using Newtonsoft.Json.Linq;

namespace Integracion_Buk
{
    [FormAttribute("Integracion_Buk.Form1", "Formularios/Integrar.b1f")]
    class Form1 : UserFormBase
    {



        #region Constructor
        public Form1()
        {
        }
        #endregion Constructor

        #region Definiciones

        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.ComboBox ComboBox1;
        private SAPbouiCOM.ComboBox ComboBox0;

        private string URL_XSJS;
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.EditText0.PressedAfter += new SAPbouiCOM._IEditTextEvents_PressedAfterEventHandler(this.EditText0_PressedAfter);
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.EditText1.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText1_ChooseFromListAfter);
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("cmbBranch").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_4").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {
            SAPbobsCOM.Recordset oRS = ((SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
            string strSQL = "";
            strSQL = "SELECT \"BPLId\", \"BPLName\" FROM OBPL WHERE \"Disabled\" = 'N' ";
            oRS.DoQuery(strSQL);
            //  int control = 0;

            if (ComboBox0.ValidValues.Count > 1)
            {
                for (int i = ComboBox0.ValidValues.Count - 1; i == 0; i--)
                {
                    ComboBox0.ValidValues.Remove(i);
                }
            }
            ComboBox0.ValidValues.Add("", "");
            if (oRS.RecordCount > 0)
            {
                for (int i = 0; i < oRS.RecordCount; i++)
                {
                    ComboBox0.ValidValues.Add(oRS.Fields.Item(0).Value.ToString(), oRS.Fields.Item(1).Value.ToString());
                    oRS.MoveNext();
                }
            }


            strSQL = "SELECT \"U_EXX_VALOR\" FROM \"@BUK_CONF\" WHERE \"Code\" = 'URL_XSJS' ";
            oRS.DoQuery(strSQL);

            if (oRS.RecordCount > 0)
            {
                for (int i = 0; i < oRS.RecordCount; i++)
                {
                    URL_XSJS = oRS.Fields.Item(0).Value.ToString();
                    oRS.MoveNext();
                }
            }

        }

        #endregion Definiciones

        #region Eventos

        private void EditText0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            string valorFecha = EditText0.Value.ToString();
            this.UIAPIRawForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE;
            EditText0.Value = valorFecha;
            //EditText1.Value = "-1";
            ComboBox0.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
        }



        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if (Application.SBO_Application.MessageBox(" Desea crear la centralizacion con la fecha " + EditText0.Value.ToString(), 1, "Sí", "No") == 1)
                {
                    if ((EditText0.Value.ToString() != "") && (EditText2.Value.ToString() != ""))
                    {
                        Mensajes.EnviarMensaje("Buscando datos para integrar... " + Program.AddOnName + ".", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplication);

                        string valorFecha = EditText0.Value.ToString();
                        string anio = valorFecha.Substring(0, 4);
                        string mes = valorFecha.Substring(4, 2);
                        string dia = valorFecha.Substring(6, 2);
                        string rut = EditText2.Value.ToString();
                        string empresa = ComboBox0.Selected.Value;
                        //Application.SBO_Application.Company.ServerName;
                        if ((mes != "") && (anio != "") && (rut != ""))
                        {
                            CenterCostProcess(mes, anio, rut, dia);
                            /*
                            var url = URL_XSJS+ "/WS/services/documents/addJournalEntriesAPI.xsjs?month=" + mes + "&year=" + anio + "&day=" + dia + "&company_id=" + empresa + "&rut=" + rut;
                            Mensajes.EnviarMensaje("Validando informacion... " + Program.AddOnName + ".", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplication);
                            //////////////////Aqui Logica del Nico ////////
                            ///   Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                            Application.SBO_Application.StatusBar.SetText(url, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                            WebRequest request = WebRequest.Create(url);
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 ;
                            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            string content;
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                content = reader.ReadToEnd();
                            }
                            if (content.Contains("Error"))
                            {
                                guardarLog(content, "NoTransID", "Er");
                                Mensajes.EnviarMensaje("Ha ocurrido un error favor validar la tabla de logs ", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplicationForm);
                            }
                            else
                            {
                                guardarLog(content, "NoTransID", "Ok");
                                EditText1.Value = "";
                                ComboBox0.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                                Mensajes.EnviarMensaje("Se creo el Asiento " + content, TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplicationForm);

                            }*/
                        }
                        else
                        {
                            Application.SBO_Application.StatusBar.SetText("Faltan dados de fecha, sucursal o rut de sucursal para realizar la centralización", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                            Application.SBO_Application.MessageBox("Faltan dados de fecha, sucursal o rut de sucursal para realizar la centralización", 1, "OK");
                        }
                    }
                    else
                    {
                        Application.SBO_Application.StatusBar.SetText("Faltan dados de fecha, sucursal o rut de sucursal para realizar la centralización", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        Application.SBO_Application.MessageBox("Faltan dados de fecha, sucursal o rut de sucursal para realizar la centralización", 1, "OK");
                    }
                }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                Application.SBO_Application.StatusBar.SetText(ex.StackTrace, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

            }

        }

        private void CenterCostProcess(string month, string year, string rut, string day)
        {
            try
            {
                //var listAccount = GetAccountExport(month, year, rut);

                string ruta = "C:\\Log\\mired-contabilidadm.json";
                string json = File.ReadAllText(ruta);
                List<Registro> listAccount = new List<Registro>();
                JObject jsonObject = JObject.Parse(json);
                var cuenta = jsonObject["data"]["20454814178"];
                foreach (var item in cuenta)
                {

                    var rootObject = JsonConvert.DeserializeObject<List<Registro>>(item.ToString());
                    listAccount.AddRange(rootObject);
                }
           

                var url = Globals.ServiceLayerUrl + Globals.JournalEntries;
                ServiceLayerHelper.Connect();
                var JournalEntry = new JournalEntrySL();
                var fullDateSLFormat = year + "-" + month + "-" + day;

                JournalEntry.Memo = "CENTRALIZACION MES " + month + "/" + year;
                //JournalEntry.BPLID = rut;
                JournalEntry.Reference = year + " - " + month;
                JournalEntry.Reference2 = month;
                JournalEntry.Reference3 = year;

                JournalEntry.ReferenceDate = fullDateSLFormat;
                JournalEntry.TaxDate = fullDateSLFormat;
                JournalEntry.DueDate = fullDateSLFormat;

                JournalEntry.JournalEntryLines = new List<JournalEntrySL.JournalEntryLine>();

                var TotalDebit = 0.00;
                var TotalCredit = 0.00;
                //var TotalLines = listAccount.data.Values.Count;

                var lines = new List<JournalEntrySL.JournalEntryLine>();
                //Application.SBO_Application.StatusBar.SetText("json " +  JsonConvert.SerializeObject(listAccount.data), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                var palotes2 = "";
                palotes2 += "cuenta_de_mayor_codigo_sn|cuenta_asociada|debito|credito";
                foreach (var registro in listAccount)
                {
                    var oJournalEntryLines = new JournalEntrySL.JournalEntryLine();
                    palotes2 += registro.cuenta_de_mayor_codigo_sn + "|" + registro.cuenta_asociada + "|" + registro.debito + "|" + registro.credito + "\n ";



                    // Application.SBO_Application.StatusBar.SetText("json " + registro.cuenta_asociada, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    if (!string.IsNullOrEmpty(registro.cuenta_de_mayor_codigo_sn))
                    {
                        if (registro.cuenta_de_mayor_codigo_sn.StartsWith("E"))
                        {
                            string texto = registro.cuenta_de_mayor_codigo_sn;
                            string sinE = texto.Substring(1); // Elimina la "E"
                            string ultimos8 = sinE.Substring(sinE.Length - 8);
                            if (!string.IsNullOrEmpty(registro.cuenta_asociada))
                            {
                                var account = registro.cuenta_asociada.Replace('-', ' ').Trim();
                                account = account.Replace(" ", "");
                                //account = account.Substring(0, account.Length - 2);
                                oJournalEntryLines.AccountCode = getSYSBYAccount(account);
                                oJournalEntryLines.ShortName = registro.cuenta_de_mayor_codigo_sn;//"E000" + ultimos8;

                            }


                        }
                        else
                        {

                            oJournalEntryLines.AccountCode = getSYSBYAccount(registro.cuenta_de_mayor_codigo_sn.Replace('-', ' '));
                        }
                    }

                    
                    

                    //else
                    //    oJournalEntryLines.AccountCode = "_SYS00000017148";
                    // Application.SBO_Application.StatusBar.SetText("json " + registro.cuenta_de_mayor_codigo_sn, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                    //Application.SBO_Application.StatusBar.SetText("json " + oJournalEntryLines.AccountCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                    if (!string.IsNullOrEmpty(oJournalEntryLines.AccountCode))
                    {
                        if (registro.debito != null)
                        {
                            oJournalEntryLines.Debit = registro.debito.Value;
                            TotalDebit += registro.debito.Value;
                        }

                        if (registro.credito != null)
                        {
                            oJournalEntryLines.Credit = registro.credito.Value;
                            TotalCredit += registro.credito.Value;
                        }
                        lines.Add(oJournalEntryLines);
                    }

                }

                JournalEntry.JournalEntryLines = lines;
                Application.SBO_Application.StatusBar.SetText("Total Débito: " + TotalDebit.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                Application.SBO_Application.StatusBar.SetText("Total Crédito: " + TotalCredit.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                var jsonBody = JsonConvert.SerializeObject(JournalEntry, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                });

                var palotes = "";
                palotes += "AccountCode" + "|" + "Debit" + "|" + "Credit" + "\n ";
                foreach (var item in JournalEntry.JournalEntryLines)
                {
                    palotes += item.AccountCode + "|" + item.Debit + "|" + item.Credit+"\n ";
                }


                GenerarJson(month, year, jsonBody);
                GenerarJson2(month, year, palotes);
                GenerarJson3(month, year, palotes2);
                //Application.SBO_Application.MessageBox(jsonBody);
                ServiceLayerHelper.Connect();

                //Application.SBO_Application.StatusBar.SetText("Asiento  creado", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                var response = ServiceLayerHelper.PostSL(url, jsonBody);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    Application.SBO_Application.StatusBar.SetText("Asiento  creado", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);


                }
                else
                {
                    Application.SBO_Application.StatusBar.SetText("error sl " + response.StatusCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    Application.SBO_Application.StatusBar.SetText("error sl " + response.Content, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);


                }


            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(ex.StackTrace, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                throw;
            }
        }
        private string pathIndexLog = "C:\\Log";
        private void GenerarJson(string month, string year, string json)
        {
            try
            {
                string rutaArchivo = pathIndexLog + "\\" + year + "-" + month + "-" + "asiento.txt";


                try
                {
                    // Escribir contenido en el archivo
                    File.WriteAllText(rutaArchivo, json);
                    Console.WriteLine("Archivo creado y contenido escrito correctamente.");
                }
                catch (Exception ex)
                {
                    // Manejar errores
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GenerarJson2(string month, string year, string json)
        {
            try
            {
                string rutaArchivo = pathIndexLog + "\\" + year + "-" + month + "-" + "asiento2.txt";


                try
                {
                    // Escribir contenido en el archivo
                    File.WriteAllText(rutaArchivo, json);
                    Console.WriteLine("Archivo creado y contenido escrito correctamente.");
                }
                catch (Exception ex)
                {
                    // Manejar errores
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }  
        private void GenerarJson3(string month, string year, string json)
        {
            try
            {
                string rutaArchivo = pathIndexLog + "\\" + year + "-" + month + "-" + "asiento3.txt";


                try
                {
                    // Escribir contenido en el archivo
                    File.WriteAllText(rutaArchivo, json);
                    Console.WriteLine("Archivo creado y contenido escrito correctamente.");
                }
                catch (Exception ex)
                {
                    // Manejar errores
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //20454814178
        private string getSYSBYAccount(string account)
        {
            account = account.Replace(" ", "");
            var url = Globals.ServiceLayerUrl + Globals.ChartOfAccounts + "?$filter=FormatCode eq '" + account.Trim() + "'";


            var cuenta = "";

            var response = ServiceLayerHelper.GetSL(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JObject jsonObject = JObject.Parse(response.Content);
                try
                {
                    //cuenta = jsonObject["value"]?[0]?["Code"]?.ToString();
                    //Application.SBO_Application.StatusBar.SetText("getSYSBYAccount " + account, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                    var res = JsonConvert.DeserializeObject<ChartOfAccounts>(response.Content);

                    if (res.value.Count > 0)
                    {
                        cuenta = res.value[0].Code;
                    }


                }
                catch (Exception ex)
                {
                    Application.SBO_Application.StatusBar.SetText("error sl " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                    cuenta = "";
                }

            }
            else
            {
                Application.SBO_Application.StatusBar.SetText("error sl " + response.StatusCode, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                Application.SBO_Application.StatusBar.SetText("error sl " + response.Content, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                //new throw Exception("Error en")

            }

            return cuenta;
        }

        private List<Registro> GetAccountExport(string month, string year, string rut)
        {
            List<Registro> listAccount = new List<Registro>();
            var client = new RestClient("https://mired.buk.pe/api/v1/peru/accounting/export?month=" + month + "&year=" + year + "&company_id=" + rut);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("auth_token", "PihpLPT6c7UcnkExVTvcBDhs");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                JObject jsonObject = JObject.Parse(response.Content);
                var cuenta = jsonObject["data"]["20454814178"];//?["Code"]?.ToString();

                foreach (var item in cuenta)
                {

                    var rootObject = JsonConvert.DeserializeObject<List<Registro>>(item.ToString());
                    listAccount.AddRange(rootObject);
                }
                //var rootObject = JsonConvert.DeserializeObject<AccountExportClass>(response.Content);
            }
            else
            {
                throw new Exception("Error GetAccountExport: " + response.Content);
            }

            return listAccount;

        }
        #endregion Eventos

        #region Metodos
        public class JsonResult
        {
            public int TransId { get; set; }
        }
        /// <summary>
        /// Metodo guardar todos los registros Estado E = Error, P = Procesado
        /// </summary>
        private void guardarLog(string Error = null, string TransId = null, string Estado = null, string Payload = null)
        {
            #region variables

            SAPbobsCOM.UserTable oUserTable = null;
            string MsgErrSBO = "";
            int Respuesta = 0;
            int iResult = 0;
            int iAttEntry = -1;

            #endregion variables

            #region try
            try
            {
                oUserTable = Program.oCompany.UserTables.Item("BUK_LOG");
                //oUserTable.Code = oRecordSet.Fields.Item("NewCode").Value.ToString();
                //oUserTable.Name = Program.TallasGrupo.Rows[j]["Name"].ToString();//oEditTex2.GetText(i);
                //if (Error.Length > 250)
                //    Error = Error.Substring(0, 250);

                oUserTable.Name = EditText0.Value.ToString();
                oUserTable.UserFields.Fields.Item("U_TransId").Value = TransId;
                //oUserTable.UserFields.Fields.Item("U_MenResp").Value = Error;
                oUserTable.UserFields.Fields.Item("U_MenErr").Value = Error;
                //oUserTable.UserFields.Fields.Item("U_MenPayload").Value = Error;
                oUserTable.UserFields.Fields.Item("U_FecAr").Value = EditText0.Value.ToString();
                oUserTable.UserFields.Fields.Item("U_EstAr").Value = Estado;

                Respuesta = oUserTable.Add();
                if (Respuesta != 0)
                {
                    Program.oCompany.GetLastError(out Respuesta, out MsgErrSBO);
                    Application.SBO_Application.MessageBox(Program.AddOnName + ": Error:" + MsgErrSBO);
                }
            }
            #endregion try

            #region Catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(Program.AddOnName + ": Error:" + ex.Message, TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
            }
            #endregion Catch
        }


        #endregion Metodos

        private void EditText1_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            string val = null;
            string val2 = null;
            string val3 = null;
            SAPbouiCOM.DBDataSource TaxIdNum = this.UIAPIRawForm.DataSources.DBDataSources.Item("OBPL");
            //  TaxIdNum.Fields.Item("OBPL");

            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg chflarg = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string uidChose = chflarg.ChooseFromListUID;
                System.Console.WriteLine("ChooseFromListUID:" + uidChose);
                SAPbouiCOM.DataTable dt = chflarg.SelectedObjects;
                val3 = System.Convert.ToString(dt.GetValue("BPLName", 0));
                val2 = System.Convert.ToString(dt.GetValue("TaxIdNum", 0));
                val = System.Convert.ToString(dt.GetValue(0, 0));
                //oRecordSet = ((SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
                //oRecordSet.DoQuery(Consultas.nombreEmpleado(val));
                //EditText1.Value = oRecordSet.Fields.Item("Nombre").Value.ToString();
                if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                    UIAPIRawForm.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;


                //string strSQL = "SELECT * FROM " + Comas(BaseDt) + ".OJDT A WHERE A." + Comas("Ref2") + " = '" + MesText + "' AND A." + Comas("Ref3") + " = '" + Anho + "' AND A." + Comas("StornoToTr") + " IS NULL  AND  A." + Comas("TransId") + " NOT IN (SELECT  B." + Comas("StornoToTr") + " FROM " + Comas(BaseDt) + ".OJDT B WHERE B." + Comas("StornoToTr") + "= A." + Comas("TransId") + ")";
                //DataTable DTValida = CONEX.OdbcConexion.ObtenerDataTable(strSQL);
                //if (DTValida.Rows.Count > 0)
                //{
                //    WriteErrorLog("Centralización ya ejecutada para el mes " + MesText.Trim() + ", año" + AnhoText.Trim() + ".Fin del proceso de integracion GYMSA en SAP Business One para la sociedad '" + Sociedad + "' ");

                //    Console.WriteLine("Centralización ya ejecutada para el mes " + MesText.Trim() + ", año" + AnhoText.Trim() + ".Fin del proceso de integracion GYMSA en SAP Business One para la sociedad '" + Sociedad + "' ");

                //    String str2 = xcfg.Settings["GrabarArchLog"].Value.ToString();

                //    if (xcfg.Settings["GrabarArchLog"].Value.ToString() == "Si")
                //    {
                //        Console.SetOut(oldOut);
                //        writer.Close();
                //        ostrm.Close();
                //    }
                //}
                if (EditText0.Value.ToString() != "")
                {
                    int Mes = 0;
                    int Anho = 0;
                    string MesText = "";
                    string AnhoText = "";
                    //Mes =Convert.ToDateTime(EditText0.Value).Month;
                    MesText = EditText0.Value.Substring(4, 2);
                    //Anho = Convert.ToDateTime(EditText0.Value).Year;
                    AnhoText = EditText0.Value.Substring(0, 4);


                    if (ValidaDatoCentralizacionesAnteriores(MesText, AnhoText, Program.oCompany, val, Program.oCompany.CompanyDB) == false)
                    {
                        Application.SBO_Application.StatusBar.SetText(" Validando datos de centralizacion para mes  " + MesText + ", año " + AnhoText + " y sucursal " + val3, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                        TaxIdNum.SetValue("TaxIdNum", 0, val2);
                        //EditText1.Value = val2;
                        ComboBox0.Select(val, SAPbouiCOM.BoSearchKey.psk_ByValue);
                        Application.SBO_Application.StatusBar.SetText(" Validación correcta", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                    }
                    else
                    {
                        Application.SBO_Application.MessageBox(" Existen datos de centralizacion para mes  " + MesText + ", año " + AnhoText + " y sucursal " + val3, 1, "OK");
                    }
                }


            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return;
            }
            finally
            {
                //Program.LiberarObjetos(oRecordSet);
            }

        }

        public static string Comas(string texto)
        {
            string Com = "";
            return Com = Convert.ToChar(34) + texto + Convert.ToChar(34);
        }

        public static Boolean ValidaDatoCentralizacionesAnteriores(string Mes, String Anho, SAPbobsCOM.Company oCmp, string branch, string BaseDt)
        {

            // Configuración de opciones de sesión
            //SAPbobsCOM.BusinessPartners oBP = Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
            SAPbobsCOM.Recordset oRS = ((SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
            string strSQL = "";
            strSQL = "SELECT * FROM " + Comas(BaseDt) + ".OJDT A INNER JOIN JDT1 B ON B." + Comas("TransId") + " = A." + Comas("TransId") + "WHERE A." + Comas("Ref1") + " = '" + Anho + " - " + Mes + "' AND B." + Comas("BPLId") + " = '" + branch + "' AND A." + Comas("StornoToTr") + " IS NULL  AND  A." + Comas("TransId") + " NOT IN (SELECT  B." + Comas("StornoToTr") + " FROM " + Comas(BaseDt) + ".OJDT B WHERE B." + Comas("StornoToTr") + "= A." + Comas("TransId") + ")";

            //strSQL = "SELECT * FROM " + Comas(BaseDt) + ".OJDT A INNER JOIN JDT1 B ON B." + Comas("TransId") + " = A." + Comas("TransId") + "WHERE A." + Comas("Ref2") + " = '" + Mes + "' AND A." + Comas("Ref3") + " = '" + Anho + "' AND B." + Comas("BPLId") + " = '" + branch + "' AND A." + Comas("StornoToTr") + " IS NULL  AND  A." + Comas("TransId") + " NOT IN (SELECT  B." + Comas("StornoToTr") + " FROM " + Comas(BaseDt) + ".OJDT B WHERE B." + Comas("StornoToTr") + "= A." + Comas("TransId") + ")";






            oRS.DoQuery(strSQL);

            if (oRS.RecordCount > 0)
            {

                return true;
            }


            return false;
        }

        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText2;
    }
}