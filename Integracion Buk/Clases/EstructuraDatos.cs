using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integracion_Buk
{
    class EstructuraDatos
    {
        #region Atributos

        private SAPbobsCOM.Company oCompany;
        public string sErrMsg;
        public int lErrCode;
        public int lRetCode;
        private const string C = "\"";
        #endregion

        #region Constructor

        public EstructuraDatos(SAPbobsCOM.Company oCmpny)
        {
            oCompany = oCmpny;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidVersion()
        {

            SAPbobsCOM.UserTablesMD oUserTableMD = null;
            SAPbobsCOM.Recordset oRecordSet = null;

            try
            {
                oUserTableMD = (SAPbobsCOM.UserTablesMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);

                if (!oUserTableMD.GetByKey("CMP_SETUP"))
                {
                    Program.LiberarObjetos(oUserTableMD);
                    MDCrearTabla("CMP_SETUP", "Setup de AddOns", SAPbobsCOM.BoUTBTableType.bott_NoObject);
                    MDCrearCampo("@CMP_SETUP", "CMP_ADDN", "Nombre del AddOn", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100);
                    MDCrearCampo("@CMP_SETUP", "CMP_VERS", "Versión del AddOn", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100);
                    return false;
                }
                else
                {
                    oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    if (Program.Motor == "dst_HANADB")
                        oRecordSet.DoQuery("SELECT * FROM " + C + "" + Program.SQLBaseDatos + "" + C + "." + C + "@CMP_SETUP" + C + " WHERE " + C + "U_CMP_ADDN" + C + " = '" + Program.AddOnName + "' ORDER BY " + C + "U_CMP_VERS" + C + " DESC");
                    else
                        oRecordSet.DoQuery("SELECT * FROM [@CMP_SETUP] WHERE U_CMP_ADDN = '" + Program.AddOnName + "' ORDER BY U_CMP_VERS DESC");
                    if (oRecordSet.RecordCount > 0)
                    {
                        if (Convert.ToInt32(oRecordSet.Fields.Item("U_CMP_VERS").Value.ToString().Replace(".", "")) < Convert.ToInt32(Program.AddOnVersion.ToString().Replace(".", "")))
                        {
                            Mensajes.EnviarMensaje("Se actualizará la estructura de datos del AddOn " + Program.AddOnName + ".", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplicationForm);
                            return false;
                        }
                        else if (Convert.ToInt32(oRecordSet.Fields.Item("U_CMP_VERS").Value.ToString().Replace(".", "")) > Convert.ToInt32(Program.AddOnVersion.ToString().Replace(".", "")))
                        {
                            Mensajes.EnviarMensaje("Se detectó una versión del AddOn " + Program.AddOnName + " más actualizada instalada en la sociedad. No se recomienda el uso de la versión que está intentando ejecutar.", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplicationForm);
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Mensajes.EnviarMensaje("Se creará la estructura de datos del AddOn " + Program.AddOnName + ".", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplicationForm);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return false;
            }
            finally
            {
                Program.LiberarObjetos(oRecordSet);
                Program.LiberarObjetos(oUserTableMD);
            }
        }

        /// <summary>
        /// Método que inicia la carga de la estructura de datos de la aplicación.
        /// </summary>
        public void CargarEstructuraDatos()
        {
            #region Region de try
            SAPbobsCOM.Recordset oRecordSet = null;
            oRecordSet = ((SAPbobsCOM.Recordset)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
            try
            {
                #region Region Crear tablas y campos

                ////MDCrearTabla("RACTUAL1", "Recurso Actual", SAPbobsCOM.BoUTBTableType.bott_NoObject);

                //////Orden de fabricacion
                ////string[] Codigo = { "Y", "N" };
                ////string[] Descripcion = { "Si", "No" };
                ////MDCrearCampo("OWOR", "EXX_TalCol", "Talla color?", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, SAPbobsCOM.BoYesNoEnum.tNO, Codigo, Descripcion, "N");
                ////MDCrearCampo("WOR1", "EXX_GenCos", "Generacion de costo", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity);

                ////MDCrearCampo("OWOR", "Default", "Default", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1);

                MDCrearTabla("BUK_LOG", "Log Integracion Buk", SAPbobsCOM.BoUTBTableType.bott_NoObjectAutoIncrement);
                MDCrearCampo("@BUK_LOG", "TransId", "Numero Asiento", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 20);
                MDCrearCampo("@BUK_LOG", "MenErr", "Mensaje Error", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 250);                
                MDCrearCampo("@BUK_LOG", "FecAr", "Fecha del archivo", SAPbobsCOM.BoFieldTypes.db_Date);
                MDCrearCampo("@BUK_LOG", "EstAr", "Estado", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 2); 

                #endregion Region Crear tablas y campos

                #region Region Crear Busqueda Formateada

                ////if (Program.Motor == "dst_HANADB")
                ////    MDCrearBusquedaFM("Exxis: Produccion - Recurso Actual1", "SELECT \"Code\", \"Name\" FROM " + Program.SQLBaseDatos + ".\"@RACTUAL1\" ", "TallayColor.MaestroModelos", "Item_142", "U_EXX_Recurso", false, "", true);
                ////else
                ////    MDCrearBusquedaFM("Exxis: Produccion - Recurso Actual1", "SELECT Code, Name FROM [@RACTUAL1] ", "TallayColor.MaestroModelos", "Item_142", "U_EXX_Recurso", false, "", true);

                #endregion Region Crear Busqueda Formateada

            }
            #endregion Region de try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
            }
            finally
            {
                Program.LiberarObjetos(oRecordSet);
            }
            #endregion Region de catch
        }


        
        /// <summary>
        /// Metodo para crear la UDO
        /// </summary>
        private void AddUDO()
        {
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;
            oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
            oUserObjectMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tYES;
            oUserObjectMD.CanClose = SAPbobsCOM.BoYesNoEnum.tYES;
            oUserObjectMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO;
            oUserObjectMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
            oUserObjectMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
            oUserObjectMD.CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tYES;
            oUserObjectMD.ChildTables.TableName = "NXLINEDP";
            oUserObjectMD.Code = "NXCABEDP";
            oUserObjectMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tYES;
            oUserObjectMD.Name = "Estado de Pago";
            oUserObjectMD.ObjectType = SAPbobsCOM.BoUDOObjType.boud_Document;
            oUserObjectMD.TableName = "NXCABEDP";
            lRetCode = oUserObjectMD.Add();
            // check for errors in the process
            if (lRetCode != 0)
            {
                oCompany.GetLastError(out lRetCode, out sErrMsg);
            }
            oUserObjectMD = null;
            GC.Collect(); // Release the handle to the table
        }

        /// <summary>
        /// Función para crear tablas de usuarios en SB1, del tipo Documents, Master Data o No Object.
        /// </summary>
        /// <param name="sCodeTabla">Código de la tabla.</param>
        /// <param name="sDescTabla">Descripción de la tabla.</param>
        /// <param name="eTipoTabla">Tipo de tabla.</param>
        /// <returns>Retorna TRUE o FALSE si la ejecución de la función es satisfactoria o no.</returns>
        /// <remarks></remarks>
        public bool MDCrearTabla(String sCodeTabla, String sDescTabla, SAPbobsCOM.BoUTBTableType eTipoTabla)
        {
            SAPbobsCOM.UserTablesMD oUserTablesMD = null;
            SAPbobsCOM.UserTablesMD oUserTablesMDAux = null;
            int iResult = 0;
            String sResult = "";

            try
            {
                oUserTablesMD = ((SAPbobsCOM.UserTablesMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables));

                if (!oUserTablesMD.GetByKey(sCodeTabla))
                {
                    oUserTablesMDAux = ((SAPbobsCOM.UserTablesMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables));
                    oUserTablesMDAux.TableName = String.Format(sCodeTabla);
                    oUserTablesMDAux.TableDescription = String.Format(sDescTabla);
                    oUserTablesMDAux.TableType = eTipoTabla;
                    iResult = oUserTablesMDAux.Add();

                    if (iResult != 0)
                    {
                        oCompany.GetLastError(out iResult, out sResult);

                        Mensajes.EnviarMensaje("Error: " + sResult + ", creando la tabla " + sCodeTabla, TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);

                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return false;
            }
            finally
            {
                Program.LiberarObjetos(oUserTablesMDAux);
                Program.LiberarObjetos(oUserTablesMD);
            }
        }

        /// <summary>
        /// Función para crear campos de usuarios en SB1.
        /// </summary>
        /// <param name="sNameTable">Nombre de la tabla donde se creara el campo de usuario.</param>
        /// <param name="sNameField">Nombre del campo de usuario.</param>
        /// <param name="sDescField">Descripción del campo de usuario.</param>
        /// <param name="eFieldTypes">Tipo de campo.</param>
        /// <param name="eFldSubTypes">Subtipo de campo.</param>
        /// <param name="iSize">Tamaño del campo.</param>
        /// <param name="eMandatory">Campo obligatorio.</param>
        /// <param name="sValidValues">Valores validos.</param>
        /// <param name="sValidDescript">Descripción de valores validos.</param>
        /// <param name="sDefaultValue">Valor por defecto.</param>
        /// <param name="sLinkedTable">Tabla vinculada.</param>
        /// <returns>Retorna TRUE o FALSE si la ejecución de la función es satisfactoria o no.</returns>
        /// <remarks></remarks>
        public bool MDCrearCampo(String sNameTable, String sNameField, String sDescField, SAPbobsCOM.BoFieldTypes eFieldTypes, SAPbobsCOM.BoFldSubTypes eFldSubTypes = SAPbobsCOM.BoFldSubTypes.st_None, int iSize = 10, SAPbobsCOM.BoYesNoEnum eMandatory = SAPbobsCOM.BoYesNoEnum.tNO, String[] sValidValues = null, String[] sValidDescript = null, String sDefaultValue = "", String sLinkedTable = "", String sLinkedTableOb = "")
        {

            SAPbobsCOM.UserFieldsMD oUserFieldsMD = null;

            try
            {
                oUserFieldsMD = ((SAPbobsCOM.UserFieldsMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields));

                if (!FieldExists(sNameTable, sNameField))
                {
                    oUserFieldsMD.TableName = sNameTable;
                    oUserFieldsMD.Name = sNameField;
                    oUserFieldsMD.Description = sDescField;
                    oUserFieldsMD.Type = eFieldTypes;

                    if (eFieldTypes == SAPbobsCOM.BoFieldTypes.db_Date)
                    {
                        oUserFieldsMD.SubType = eFldSubTypes;
                        oUserFieldsMD.EditSize = iSize;
                    }

                    if (eFieldTypes != SAPbobsCOM.BoFieldTypes.db_Date)
                        oUserFieldsMD.EditSize = iSize;

                    if (eFieldTypes == SAPbobsCOM.BoFieldTypes.db_Float)
                        oUserFieldsMD.SubType = eFldSubTypes;

                    if (sLinkedTableOb == "OITM")
                        oUserFieldsMD.LinkedSystemObject = SAPbobsCOM.UDFLinkedSystemObjectTypesEnum.ulItems;

                    if (sLinkedTable != "")
                        oUserFieldsMD.LinkedTable = sLinkedTable;
                    else
                    {
                        if (sValidValues != null)
                        {
                            for (int i = 0; i <= sValidValues.Length - 1; i++)
                            {
                                if (sValidDescript == null)
                                    oUserFieldsMD.ValidValues.Description = sValidValues[i];
                                else
                                    oUserFieldsMD.ValidValues.Description = sValidDescript[i];

                                oUserFieldsMD.ValidValues.Value = sValidValues[i];
                                oUserFieldsMD.ValidValues.Add();
                            }

                            if (sDefaultValue != "")
                            {
                                oUserFieldsMD.DefaultValue = sDefaultValue;
                                oUserFieldsMD.Mandatory = eMandatory;
                            }
                        }
                    }

                    int iResult = 0;
                    string sResult = "";
                    iResult = oUserFieldsMD.Add();

                    if (iResult != 0)
                    {
                        oCompany.GetLastError(out iResult, out sResult);
                        Mensajes.EnviarMensaje("Error: " + sResult + ", creando el campo " + sNameField, TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return false;

            }
            finally
            {
                Program.LiberarObjetos(oUserFieldsMD);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sCode"></param>
        /// <param name="sName"></param>
        /// <param name="sTableName"></param>
        /// <param name="sFindColumn1"></param>
        /// <param name="sFindColumn2"></param>
        /// <param name="eCanCancel"></param>
        /// <param name="eCanClose"></param>
        /// <param name="eCanDelete"></param>
        /// <param name="eCanCreateDefaultForm"></param>
        /// <param name="eCanFind"></param>
        /// <param name="eCanLog"></param>
        /// <param name="eObjectType"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool MDCrearUDO(String sCode, String sName, String sTableName, string sChildTable, String sFindColumn1, SAPbobsCOM.BoYesNoEnum eCanCancel = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanClose = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanDelete = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanFind = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanLog = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoUDOObjType eObjectType = SAPbobsCOM.BoUDOObjType.boud_MasterData, SAPbobsCOM.BoYesNoEnum eManageSeries = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanNewForm = SAPbobsCOM.BoYesNoEnum.tNO)
        {
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;
            int iResult = 0;
            String sResult = "";

            try
            {
                oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD));

                if (!oUserObjectMD.GetByKey(sCode))
                {
                    oUserObjectMD.Code = sCode;
                    oUserObjectMD.Name = sName;
                    oUserObjectMD.ObjectType = eObjectType;
                    oUserObjectMD.TableName = sTableName;
                    if (sChildTable != null)
                        oUserObjectMD.ChildTables.TableName = sChildTable;
                    oUserObjectMD.CanCancel = eCanCancel;
                    oUserObjectMD.CanClose = eCanClose;
                    oUserObjectMD.CanDelete = eCanDelete;
                    oUserObjectMD.CanCreateDefaultForm = eCanCreateDefaultForm;
                    oUserObjectMD.CanFind = eCanFind;
                    oUserObjectMD.CanLog = eCanLog;
                    oUserObjectMD.ManageSeries = eManageSeries;
                    oUserObjectMD.EnableEnhancedForm = eCanNewForm;

                    if (sFindColumn1 != "")
                    {
                        oUserObjectMD.FindColumns.ColumnAlias = sFindColumn1;
                        oUserObjectMD.FindColumns.Add();
                    }
                    //if (sFindColumn2 != "")
                    //{
                    //    oUserObjectMD.FindColumns.ColumnAlias = sFindColumn2;
                    //    oUserObjectMD.FindColumns.Add();
                    //}

                    //if ((sFindColumn2 != "") | (sFindColumn1 != ""))
                    //    oUserObjectMD.FindColumns.Add();

                    iResult = oUserObjectMD.Add();

                    if (iResult != 0)
                    {
                        oCompany.GetLastError(out iResult, out sResult);
                        Mensajes.EnviarMensaje("Error: " + sResult.ToString() + ", creando el UDO " + sCode.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return false;
            }
            finally
            {
                Program.LiberarObjetos(oUserObjectMD);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sCode"></param>
        /// <param name="sName"></param>
        /// <param name="sTableName"></param>
        /// <param name="sFindColumn1"></param>
        /// <param name="sFindColumn2"></param>
        /// <param name="eCanCancel"></param>
        /// <param name="eCanClose"></param>
        /// <param name="eCanDelete"></param>
        /// <param name="eCanCreateDefaultForm"></param>
        /// <param name="eCanFind"></param>
        /// <param name="eCanLog"></param>
        /// <param name="eObjectType"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool MDCrearUDO(String sCode, String sName, String sTableName, String sFindColumn1, SAPbobsCOM.BoYesNoEnum eCanCancel = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanClose = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanDelete = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanFind = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanLog = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoUDOObjType eObjectType = SAPbobsCOM.BoUDOObjType.boud_MasterData, SAPbobsCOM.BoYesNoEnum eManageSeries = SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum eCanNewForm = SAPbobsCOM.BoYesNoEnum.tNO)
        {
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;
            int iResult = 0;
            String sResult = "";

            try
            {
                oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD));

                if (!oUserObjectMD.GetByKey(sCode))
                {
                    oUserObjectMD.Code = sCode;
                    oUserObjectMD.Name = sName;
                    oUserObjectMD.ObjectType = eObjectType;
                    oUserObjectMD.TableName = sTableName;
                    oUserObjectMD.CanCancel = eCanCancel;
                    oUserObjectMD.CanClose = eCanClose;
                    oUserObjectMD.CanDelete = eCanDelete;
                    oUserObjectMD.CanCreateDefaultForm = eCanCreateDefaultForm;
                    oUserObjectMD.CanFind = eCanFind;
                    oUserObjectMD.CanLog = eCanLog;
                    oUserObjectMD.ManageSeries = eManageSeries;
                    oUserObjectMD.EnableEnhancedForm = eCanNewForm;

                    if (sFindColumn1 != "")
                    {
                        oUserObjectMD.FindColumns.ColumnAlias = sFindColumn1;
                        oUserObjectMD.FindColumns.Add();
                    }

                    //if (sFindColumn2 != "")
                    //{
                    //    oUserObjectMD.FindColumns.ColumnAlias = sFindColumn2;
                    //    oUserObjectMD.FindColumns.Add();
                    //}


                    //if ((sFindColumn2 != "") | (sFindColumn1 != ""))
                    //    oUserObjectMD.FindColumns.Add();

                    iResult = oUserObjectMD.Add();

                    if (iResult != 0)
                    {
                        oCompany.GetLastError(out iResult, out sResult);
                        Mensajes.EnviarMensaje("Error: " + sResult.ToString() + ", creando el UDO " + sCode.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return false;
            }
            finally
            {
                Program.LiberarObjetos(oUserObjectMD);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sQueryName"></param>
        /// <param name="sQuery"></param>
        /// <param name="sFormID"></param>
        /// <param name="sItemUID"></param>
        /// <param name="colUID"></param>
        /// <param name="autoRefresh"></param>
        /// <param name="autoRefreshField"></param>
        /// <param name="borrarSiExiste"></param>
        /// <returns></returns>
        private bool MDCrearBusquedaFM(String sQueryName, String sQuery, String sFormID, String sItemUID, String colUID = "-1", Boolean autoRefresh = false, String autoRefreshField = "", Boolean borrarSiExiste = true)
        {
            Boolean fR = false;
            String sResult = "";

            try
            {
                // creo el query
                ImportConsulta(sQueryName, sQuery, "Busquedas Formateadas", true);

                // Eliminación de la BF
                int ret = 0;
                SAPbobsCOM.FormattedSearches fUserBusFor2;
                fUserBusFor2 = ((SAPbobsCOM.FormattedSearches)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oFormattedSearches));
                Boolean existe = fUserBusFor2.GetByKey(GetIDBusquedaFM(sFormID, sItemUID, colUID));
                if ((existe) && (borrarSiExiste))
                {
                    SAPbobsCOM.Recordset oRecordSet = null;
                    string strQuery;

                    oRecordSet = ((SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));

                    strQuery = null;
                    if (Program.Motor == "dst_HANADB")
                        strQuery = "UPDATE " + C + "" + Program.SQLBaseDatos + "" + C + "." + C + "OUQR" + C + "  SET " + C + "QString" + C + " =' " + sQuery + "' where  " + C + "QName" + C + " = '" + sQueryName + "'";
                    else
                        strQuery = "UPDATE OUQR  SET QString =' " + sQuery + "' where  QName = '" + sQueryName + "'";
                    oRecordSet.DoQuery(strQuery);
                    Program.LiberarObjetos(oRecordSet);
                    existe = false;
                    ImportConsulta(sQueryName, sQuery, "Busquedas Formateadas", true);
                    //ret = fUserBusFor2.Remove()
                    //existe = False
                }
                Program.LiberarObjetos(fUserBusFor2);

                // creación de la BF
                if (!(existe))
                {
                    SAPbobsCOM.FormattedSearches fUserBusFor;
                    fUserBusFor = ((SAPbobsCOM.FormattedSearches)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oFormattedSearches));
                    fUserBusFor.FormID = sFormID;
                    fUserBusFor.ItemID = sItemUID;
                    if (colUID != "-1")
                        fUserBusFor.ColumnID = colUID;

                    fUserBusFor.Action = SAPbobsCOM.BoFormattedSearchActionEnum.bofsaQuery;

                    fUserBusFor.QueryID = GetIDQuery(sQueryName, GetIDQueryCategory("Busquedas Formateadas"));
                    if ((autoRefresh) && (autoRefreshField != ""))
                    {
                        fUserBusFor.Refresh = SAPbobsCOM.BoYesNoEnum.tYES;
                        if (colUID == "-1")
                            fUserBusFor.ByField = SAPbobsCOM.BoYesNoEnum.tYES;
                        else
                            fUserBusFor.ByField = SAPbobsCOM.BoYesNoEnum.tNO;

                        fUserBusFor.FieldID = autoRefreshField;
                        fUserBusFor.ForceRefresh = SAPbobsCOM.BoYesNoEnum.tYES;
                    }
                    else
                        fUserBusFor.Refresh = SAPbobsCOM.BoYesNoEnum.tNO;

                    ret = fUserBusFor.Add();
                    if (ret != 0)
                    {
                        oCompany.GetLastError(out ret, out sResult);
                        Mensajes.EnviarMensaje("Error: " + sResult.ToString() + ", creando la busqueda Formateada" + sQueryName.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                    }
                    Program.LiberarObjetos(fUserBusFor);
                }

                fR = true;
            }
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
            }
            return fR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormID_o_TYPE"></param>
        /// <param name="ItemID"></param>
        /// <param name="ColID"></param>
        /// <returns></returns>
        public int GetIDBusquedaFM(String FormID_o_TYPE, String ItemID, String ColID = "-1")
        {
            SAPbobsCOM.Recordset oRecordSet = null;
            String strSQLBF = null;

            #region Region de try
            try
            {
                oRecordSet = ((SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));

                strSQLBF = "SELECT IndexID FROM CSHS WHERE FormID='" + FormID_o_TYPE + "' and ItemID='" + ItemID + "' and ColID='" + ColID + "'";
                oRecordSet.DoQuery(strSQLBF);

                if (oRecordSet.EoF == false)
                    return Convert.ToInt32(oRecordSet.Fields.Item(0).Value);
                else
                    return -1;

            }
            #endregion Region de try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return -1;
            }
            #endregion Region de try

            #region Region de finally
            finally
            {
                Program.LiberarObjetos(oRecordSet);
            }
            #endregion Region de finally
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sNombre"></param>
        /// <param name="sQuery"></param>
        /// <param name="sQueryCategoty"></param>
        /// <param name="bCreaCategory"></param>
        /// <param name="borrarSiExiste"></param>
        public void ImportConsulta(String sNombre, String sQuery, String sQueryCategoty, Boolean bCreaCategory = false, Boolean borrarSiExiste = false)
        {
            SAPbobsCOM.UserQueries oUserQuery = null;
            int ret = 0;
            String sResult = "";
            #region Region de try
            try
            {
                if (borrarSiExiste)
                    EliminarQuery(sNombre, sQueryCategoty);


                if (bCreaCategory)
                    CrearQueryCategory(sQueryCategoty);


                oUserQuery = ((SAPbobsCOM.UserQueries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserQueries));

                oUserQuery.Query = sQuery;
                oUserQuery.QueryCategory = GetIDQueryCategory(sQueryCategoty);
                oUserQuery.QueryDescription = sNombre;

                ret = oUserQuery.Add();

                if (ret != 0)
                {
                    oCompany.GetLastError(out ret, out sResult);
                    Mensajes.EnviarMensaje("Error: " + sResult.ToString() + ", No se puede " + sNombre.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                }
            }
            #endregion Region de try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
            }
            #endregion Region de try

            #region Region de finally
            finally
            {
                Program.LiberarObjetos(oUserQuery);
            }
            #endregion Region de finally
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreQuery"></param>
        /// <param name="idCat"></param>
        /// <returns></returns>
        public int GetIDQuery(String nombreQuery, int idCat)
        {
            int iQueryID = -1;
            SAPbobsCOM.Recordset oRecordSet = null;

            #region Region de try
            try
            {
                oRecordSet = ((SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
                oRecordSet.DoQuery("SELECT IntrnalKey as Id FROM OUQR WHERE QName = '" + nombreQuery + "'  AND QCategory = '" + idCat + "'");

                if (oRecordSet.EoF == false)
                    iQueryID = Convert.ToInt32(oRecordSet.Fields.Item("Id").Value);

                return iQueryID;
            }

            #endregion Region de try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return -1;
            }
            #endregion Region de try

            #region Region de finally
            finally
            {
                Program.LiberarObjetos(oRecordSet);
            }
            #endregion Region de finally
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryName"></param>
        /// <param name="grupoName"></param>
        public void EliminarQuery(String queryName, String grupoName)
        {
            SAPbobsCOM.UserQueries oUQ = null;
            int categID = 0;
            int queryID = 0;
            #region try
            try
            {
                oUQ = ((SAPbobsCOM.UserQueries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserQueries));

                categID = GetIDQueryCategory(grupoName);
                queryID = GetIDQuery(queryName, categID);
                oUQ.GetByKey(queryID, categID);
                oUQ.Remove();
            }
            #endregion try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
            }
            #endregion Region de try

            #region Region de finally
            finally
            {
                Program.LiberarObjetos(oUQ);
            }
            #endregion Region de finally
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreCat"></param>
        /// <returns></returns>
        public int GetIDQueryCategory(String nombreCat)
        {
            int iQueryId = -1;
            SAPbobsCOM.Recordset oRecordSet = null;

            #region Region de try
            try
            {
                oRecordSet = ((SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
                oRecordSet.DoQuery("SELECT CategoryId as 'Id' FROM OQCN WHERE CatName = '" + nombreCat + "'");

                if (oRecordSet.EoF == false)
                    iQueryId = Convert.ToInt32(oRecordSet.Fields.Item("Id").Value);

                return iQueryId;
            }
            #endregion Region de try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return -1;
            }
            #endregion Region de try

            #region Region de finally
            finally
            {
                Program.LiberarObjetos(oRecordSet);
            }
            #endregion Region de finally
        }

        public void CrearQueryCategory(String grupoQuery, String permisos = "YYYYYYYYYYYYYYYYYYYY")
        {
            SAPbobsCOM.QueryCategories gQ = null;

            #region Region de try
            try
            {
                if ((GetIDQueryCategory(grupoQuery)) == -1)
                {
                    gQ = ((SAPbobsCOM.QueryCategories)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQueryCategories));
                    gQ.Name = grupoQuery;
                    gQ.Permissions = permisos;
                    gQ.Add();
                }
            }
            #endregion Region de try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
            }
            #endregion Region de try

            #region Region de finally
            finally
            {
                Program.LiberarObjetos(gQ);
            }
            #endregion Region de finally
        }

        /// <summary>
        /// Función que permite validar si el campo que se desea crear existe en la BD.
        /// </summary>
        /// <param name="sTableName">Nombre de la tabla donde se desea crear el campo.</param>
        /// <param name="sFieldName">Nombre del campo de usuario.</param>
        /// <returns>Retorna TRUE o FALSE dependiendo si el campo existe o no.</returns>
        /// <remarks></remarks>
        public bool FieldExists(String sTableName, String sFieldName)
        {
            #region Declaracion de variables
            String sSQLQuery = null;
            SAPbobsCOM.Recordset oRecordSet = null;
            SAPbobsCOM.UserFieldsMD oUserFieldsMD = null;
            #endregion Declaracion de variables

            #region Region de try
            try
            {
                oUserFieldsMD = ((SAPbobsCOM.UserFieldsMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields));
                if (Program.Motor == "dst_HANADB")
                    sSQLQuery = "SELECT " + C + "FieldID" + C + " FROM " + Program.SQLBaseDatos + "." + C + "CUFD" + C + " WHERE " + C + "TableID" + C + " = '" + sTableName + "' AND " + C + "AliasID" + C + " = '" + sFieldName + "'";
                else
                    sSQLQuery = "SELECT FieldID FROM CUFD WHERE TableID = '" + sTableName + "' AND AliasID = '" + sFieldName + "'";
                oRecordSet = ((SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
                oRecordSet.DoQuery(sSQLQuery);

                if (oRecordSet.RecordCount > 0)
                    return oUserFieldsMD.GetByKey(sTableName, Convert.ToInt32(oRecordSet.Fields.Item("FieldID").Value));
                else
                    return false;
            }
            #endregion Region de try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
                return false;
            }
            #endregion Region de catch

            #region Region de finally
            finally
            {
                Program.LiberarObjetos(oRecordSet);
                Program.LiberarObjetos(oUserFieldsMD);
            }
            #endregion Region de finally
        }

        /// <summary>
        /// Método para confirmar la versión actual del AddOn.
        /// </summary>
        /// <remarks></remarks>
        public void ConfirVersion()
        {
            #region Declaracion de variables
            SAPbobsCOM.UserTable oUserTable = null;
            SAPbobsCOM.Recordset oRecordSet = null;
            int iResult = 0;
            String sResult = "";
            #endregion Declaracion de variables

            #region Region de try
            try
            {
                oRecordSet = ((SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
                if (Program.Motor == "dst_HANADB")
                    oRecordSet.DoQuery("SELECT IFNULL(MAX(CAST(" + C + "Code" + C + " AS INT)), 0) + 1 AS " + C + "NewCode" + C + " FROM " + C + "" + Program.SQLBaseDatos + "" + C + "." + C + "@CMP_SETUP" + C + "");
                else
                    oRecordSet.DoQuery("SELECT ISNULL(MAX(CONVERT(INT, Code)), 0) + 1 AS 'NewCode' FROM [@CMP_SETUP]");

                oUserTable = oCompany.UserTables.Item("CMP_SETUP");
                oUserTable.Code = oRecordSet.Fields.Item("NewCode").Value.ToString();
                oUserTable.Name = oRecordSet.Fields.Item("NewCode").Value.ToString();
                oUserTable.UserFields.Fields.Item("U_CMP_ADDN").Value = Program.AddOnName.ToString();
                oUserTable.UserFields.Fields.Item("U_CMP_VERS").Value = Program.AddOnVersion.ToString();

                iResult = oUserTable.Add();

                oCompany.GetLastError(out iResult, out sResult);

                if (iResult != 0)
                    Mensajes.EnviarMensaje("Error " + sResult + ", registrando la versión del AddOn.", TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
            }
            #endregion Region de try

            #region Region de catch
            catch (Exception ex)
            {
                Mensajes.EnviarMensaje(ex.Message.ToString(), TipoMensaje.tm_Error, BandejaSalida.bs_MensajeSBOApplication);
            }
            #endregion Region de try

            #region Region de finally
            finally
            {
                Program.LiberarObjetos(oUserTable);
            }
            #endregion Region de finally
        }

        #endregion
    }
}
