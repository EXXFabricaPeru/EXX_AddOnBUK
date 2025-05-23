using System;
using System.Collections.Generic;
using System.Text;
using Integracion_Buk.Formularios;
using SAPbouiCOM.Framework;

namespace Integracion_Buk
{
    class Menu
    {
        public void AddMenuItems()
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));           

            try
            {

                // Get the menu collection of the newly added pop-up item
                oMenuItem = Application.SBO_Application.Menus.Item("43544");
                //oMenuItem = Application.SBO_Application.Menus.Item("1536");
                oMenus = oMenuItem.SubMenus;

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "Integracion_Buk.Integrar";
                oCreationPackage.String = "Centralización BUK";
                oCreationPackage.Position = 4;
                oMenus.AddEx(oCreationPackage);


                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "Integracion_Buk.IntegrarBP";
                oCreationPackage.String = "Centralización BUK Empleados";
                oCreationPackage.Position = 5;
                oMenus.AddEx(oCreationPackage);

                Mensajes.EnviarMensaje("Menú Principal cargado de forma satisfactoria.", TipoMensaje.tm_Advertencia, BandejaSalida.bs_MensajeSBOApplication);
                //  If the manu already exists this code will fail
                ////oMenus.AddEx(oCreationPackage);
            }
            catch (Exception e)
            {

            }

            try
            {
                //////////// Get the menu collection of the newly added pop-up item
                //////////oMenuItem = Application.SBO_Application.Menus.Item("Integracion_Buk");
                //////////oMenus = oMenuItem.SubMenus;

                //////////// Create s sub menu
                //////////oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                //////////oCreationPackage.UniqueID = "Integracion_Buk.Form1";
                //////////oCreationPackage.String = "Form1";
                //////////oMenus.AddEx(oCreationPackage);
            }
            catch (Exception er)
            { //  Menu already exists
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (pVal.BeforeAction && pVal.MenuUID == "Integracion_Buk.Integrar")
                {
                    Form1 activeForm = new Form1();
                    activeForm.Show();
                }

                if (pVal.BeforeAction && pVal.MenuUID == "Integracion_Buk.IntegrarBP")
                {
                    IntegrarBP activeForm = new IntegrarBP();
                    activeForm.Show();
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}
