using System;
using System.Collections.Generic;
using SAPbouiCOM.Framework;

namespace Integracion_Buk
{
    class Program
    {

        #region Variables

        public static SAPbobsCOM.Company oCompany;
        public static string SQLBaseDatos;
        public static string Motor;
       
        #endregion Variables

        #region Propiedades

        public static string AddOnName
        {
            get
            {
                return "Centralización Buk";
            }
        }

        public static string AddOnVersion
        {
            get
            {
                return "1.0.7";
            }
        }

        public static string SB1Version
        {
            get
            {
                return "10.0";
            }
        }

        #endregion

        #region Main

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            EstructuraDatos oEstructuraDatos = null;
            try
            {
                Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new Application();
                }
                else
                {
                    oApp = new Application(args[0]);
                }
                oCompany = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();
                SQLBaseDatos = oCompany.CompanyDB;
                Motor = Convert.ToString(oCompany.DbServerType);
                Mensajes.SBOApplication = Application.SBO_Application;
                Mensajes.EnviarMensaje("Conectando el AddOn " + AddOnName + "...", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplication);
                oEstructuraDatos = new EstructuraDatos((SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany());
                //Creación de la Estructura de Datos necesaria para el Addon
                Mensajes.EnviarMensaje("Validando Versión del AddOn " + Program.AddOnName + ".", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplication);
                if (!oEstructuraDatos.ValidVersion())
                {
                    oEstructuraDatos.CargarEstructuraDatos();
                    oEstructuraDatos.ConfirVersion();
                    Mensajes.EnviarMensaje("Se creó la Estructura de Datos del AddOn " + Program.AddOnName + ", de forma satisfactoria.", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplication);
                }
                Menu MyMenu = new Menu();
                MyMenu.AddMenuItems();
                oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);
                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);                             
                Mensajes.EnviarMensaje("El AddOn " + AddOnName + " se ha conectado con éxito.", TipoMensaje.tm_Exito, BandejaSalida.bs_MensajeSBOApplication);
                //string fff = Application.SBO_Application.Company.SystemId;
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Métodos

        public static void LiberarObjetos(Object oObject)
        {
            try
            {
                if (oObject != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oObject);

                oObject = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                oObject = null;
                GC.Collect();
            }
        }

        #endregion
    }
}
