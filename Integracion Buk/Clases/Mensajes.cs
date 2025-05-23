using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Integracion_Buk
{
    class Mensajes
    {

        #region Atributos

        private static SAPbouiCOM.Application SBO_Application;

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad aplicación de la clase.
        /// </summary>
        /// <value>Objeto Application del tipo SAPbouiCOM.Application</value>
        /// <returns>Retorna el objeto aplicación cargado.</returns>
        /// <remarks></remarks>
        public static SAPbouiCOM.Application SBOApplication
        {
            get
            {
                return SBO_Application;
            }

            set
            {
                SBO_Application = value;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que permite enviar un mensaje al usuario mediante un tipo de interface.
        /// </summary>
        /// <param name="sMensaje">String con el mensaje que se desea mostrar.</param>
        /// <param name="eTipoMensaje">Enumerador que identifica el tipo de Mensajes.</param>
        /// <param name="eBandejaSalida">Enumerador que identifica el buzón por donde saldrá el Mensajes.</param>
        /// <remarks></remarks>
        public static void EnviarMensaje(String sMensaje, TipoMensaje eTipoMensaje, BandejaSalida eBandejaSalida)
        {
            switch (eBandejaSalida)
            {
                case BandejaSalida.bs_MensajeSBOApplication:
                    switch (eTipoMensaje)
                    {
                        case TipoMensaje.tm_Exito:
                            SBO_Application.StatusBar.SetText(sMensaje.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                            break;

                        case TipoMensaje.tm_Advertencia:
                            SBO_Application.StatusBar.SetText(sMensaje.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                            break;

                        case TipoMensaje.tm_Error:
                            SBO_Application.StatusBar.SetText(sMensaje.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            break;
                    }
                    break;

                case BandejaSalida.bs_MensajeWindowsForm:
                    switch (eTipoMensaje)
                    {
                        case TipoMensaje.tm_Exito:
                            MessageBox.Show(sMensaje.ToString(), Program.AddOnName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case TipoMensaje.tm_Advertencia:
                            MessageBox.Show(sMensaje.ToString(), Program.AddOnName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case TipoMensaje.tm_Error:
                            MessageBox.Show(sMensaje.ToString(), Program.AddOnName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                    }
                    break;

                case BandejaSalida.bs_MensajeSBOApplicationForm:
                    switch (eTipoMensaje)
                    {
                        case TipoMensaje.tm_Exito:
                            SBO_Application.MessageBox(sMensaje.ToString());
                            break;

                        case TipoMensaje.tm_Advertencia:
                            SBO_Application.MessageBox(sMensaje.ToString());
                            break;

                        case TipoMensaje.tm_Error:
                            SBO_Application.MessageBox(sMensaje.ToString());
                            break;
                    }
                    break;
            }
        }

        #endregion
    }
}
