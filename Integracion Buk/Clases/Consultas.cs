using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integracion_Buk
{
    class Consultas
    {
        #region Atributos
        private static StringBuilder m_sSQL = new StringBuilder(); //Variable para la construccion de strings        
        //private const string C = "\"";
        public string ID;
        #endregion Atributos

        #region Metodos


        public static string deleteRetencion(string Articulo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE FROM " + Program.SQLBaseDatos + ".\"@ZITMRET\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_ItemCode\"  = '{0}' ", Articulo.ToString());
            }
            else
            {
                m_sSQL.Append("DELETE FROM [@ZITMRET] ");
                m_sSQL.AppendFormat("WHERE U_EXX_ItemCode  = '{0}' ", Articulo.ToString());
            }

            return m_sSQL.ToString();
        }

        public static string TraerRetencion(string Articulo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_EXX_WTCode\", \"U_EXX_WTName\" FROM " + Program.SQLBaseDatos + ".\"@ZITMRET\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_ItemCode\"  = '{0}' ", Articulo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT U_EXX_WTCode, U_EXX_WTName FROM [@ZITMRET] ");
                m_sSQL.AppendFormat("WHERE U_EXX_ItemCode  = '{0}' ", Articulo.ToString());
            }

            return m_sSQL.ToString();
        }

        public static string ultimoRetArt()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
                m_sSQL.Append("SELECT IFNULL(MAX(CAST(\"Code\" AS INT)), 0) + 1 AS \"NewCode\" FROM " + Program.SQLBaseDatos + ".\"@ZITMRET\" ");
            else
                m_sSQL.Append("SELECT ISNULL(MAX(CONVERT(INT, Code)), 0) + 1 AS 'NewCode' FROM [@ZITMRET] ");

            return m_sSQL.ToString();
        }


        public static string registrarColor(string Name, string Codigo, string Color)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
                m_sSQL.Append("insert into " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" (\"Code\" ,\"Name\" ,\"U_Codigo\",\"U_Color\") " + " VALUES ( (SELECT IFNULL(MAX(CAST(\"Code\" AS INT)), 0) + 1 AS \"NewCode\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\"),'" + Name + "','" + Codigo + "', '" + Color + "')");
            else
                m_sSQL.Append("insert into [@EXX_MCOLORES] (Name ,U_Codigo,U_Color) " + " VALUES ('" + Name + "','" + Codigo + "', '" + Color + "')");
            //m_sSQL.Append("insert into [@EXX_MCOLORES] (Code ,Name ,U_Codigo,U_Color) " + " VALUES ( SELECT ISNULL(MAX(CONVERT(INT, Code)), 0) + 1 AS 'NewCode' FROM [@EXX_MCOLORES] ,'" + Name + "','" + Codigo + "', '" + Color + "')");


            return m_sSQL.ToString();
        }

        public static string registrarTalla(string Name, string Codigo, string Agrupar)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
                m_sSQL.Append("insert into " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" (\"Code\" ,\"Name\" ,\"U_Codigo\",\"U_Agrupar\") " + " VALUES ( (SELECT IFNULL(MAX(CAST(\"Code\" AS INT)), 0) + 1 AS \"NewCode\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\"),'" + Name + "','" + Codigo + "', '" + Agrupar + "')");
            else
                m_sSQL.Append("insert into [@EXX_MTALLAS] (Name ,U_Codigo,U_Agrupar) " + " VALUES ('" + Name + "','" + Codigo + "', '" + Agrupar + "')");
            //m_sSQL.Append("insert into [@EXX_MTALLAS] (Code ,Name ,U_Codigo,U_Agrupar) " + " VALUES ( SELECT ISNULL(MAX(CONVERT(INT, Code)), 0) + 1 AS 'NewCode' FROM [@EXX_MTALLAS] ,'" + Name + "','" + Codigo + "', '" + Agrupar + "')");


            return m_sSQL.ToString();
        }


        public static string insertRetencion(string Articulo, string ret, string nom, string code)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
                m_sSQL.Append("insert into " + Program.SQLBaseDatos + ".\"@ZITMRET\" (\"Code\" ,\"Name\" ,\"U_EXX_ItemCode\",\"U_EXX_WTCode\",\"U_EXX_WTName\" ,\"U_EXX_Activo\") " + " VALUES ( '" + code + "','" + code + "','" + Articulo + "', '" + ret + "', '" + nom + "', 'Y' )");
            else
                m_sSQL.Append("insert into [@ZITMRET] (Code ,Name ,U_EXX_ItemCode,U_EXX_WTCode,U_EXX_WTName ,U_EXX_Activo) " + " VALUES ( '" + code + "','" + code + "','" + Articulo + "', '" + ret + "', '" + nom + "', 'Y' )");

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string traerLineNum(string DocEntry, string ItemCode, string Recurso)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Recurso == "")
                {
                    m_sSQL.Append("SELECT \"LineNum\",\"VisOrder\" FROM " + Program.SQLBaseDatos + ".\"WOR1\" ");
                    m_sSQL.AppendFormat("WHERE \"DocEntry\" = '{0}' ", DocEntry.ToString());
                    m_sSQL.AppendFormat("AND \"ItemCode\" = '{0}' ", ItemCode.ToString());
                }
                else
                {
                    m_sSQL.Append("SELECT \"LineNum\",\"VisOrder\" FROM " + Program.SQLBaseDatos + ".\"WOR1\"  ");
                    m_sSQL.AppendFormat("WHERE \"DocEntry\" = '{0}' ", DocEntry.ToString());
                    m_sSQL.AppendFormat("AND \"ItemCode\" = '{0}' ", ItemCode.ToString());
                    m_sSQL.AppendFormat("AND \"U_Recurso\" = '{0}' ", Recurso.ToString());
                }
            }
            else
            {
                if (Recurso == "")
                {
                    m_sSQL.Append("SELECT LineNum, VisOrder FROM WOR1 ");
                    m_sSQL.AppendFormat("WHERE DocEntry = '{0}' ", DocEntry.ToString());
                    m_sSQL.AppendFormat("AND ItemCode = '{0}' ", ItemCode.ToString());
                }
                else
                {
                    m_sSQL.Append("SELECT LineNum, VisOrder FROM WOR1 ");
                    m_sSQL.AppendFormat("WHERE DocEntry = '{0}' ", DocEntry.ToString());
                    m_sSQL.AppendFormat("AND ItemCode = '{0}' ", ItemCode.ToString());
                    m_sSQL.AppendFormat("AND U_Recurso = '{0}' ", Recurso.ToString());
                }
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string traerPlanificacion(string Recurso, string Fecha, string Dia)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT IFNULL(T0.\"Capacity\" / T1.\"CapFactor2\", 0)  AS \"CapFactor1\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ORCJ\" T0 INNER JOIN \"RSC6\" T1 ON T1.\"ResCode\" = T0.\"ResCode\" ");
                m_sSQL.AppendFormat("WHERE T0.\"ResCode\" = '{0}' ", Recurso.ToString());
                m_sSQL.AppendFormat("AND T0.\"CapDate\" = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND \"WeekDay\" = '{0}' ", Dia.ToString());
                m_sSQL.Append("AND \"CapType\" = 'I' ");
            }
            else
            {
                m_sSQL.Append("SELECT ISNULL(T0.Capacity / T1.CapFactor2, 0)  AS CapFactor1 ");
                m_sSQL.Append("FROM ORCJ T0 INNER JOIN RSC6 T1  ON  T1.ResCode = T0.ResCode ");
                m_sSQL.AppendFormat("WHERE T0.ResCode = '{0}' ", Recurso.ToString());
                m_sSQL.AppendFormat("AND T0.CapDate = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND WeekDay = '{0}' ", Dia.ToString());
                m_sSQL.Append("AND CapType = 'I' ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string traerRetencionArt(string Articulo)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"WTCode\" AS \"Codigo\", \"WTName\" AS \"Descripcion\", IFNULL((Select \"U_EXX_Activo\" from " + Program.SQLBaseDatos + ".\"@ZITMRET\" where \"U_EXX_WTCode\" = \"WTCode\" ");
                m_sSQL.AppendFormat("AND \"U_EXX_ItemCode\" = '{0}' ", Articulo.ToString());
                m_sSQL.Append(" ), 'N') AS \"Activo\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OWHT\" ");
            }
            else
            {
                m_sSQL.Append("SELECT WTCode, WTName, ISNULL((Select U_EXX_Activo from @ZITMRET where U_EXX_WTCode = WTCode ");
                m_sSQL.AppendFormat("AND U_EXX_ItemCode = '{0}' ", Articulo.ToString());
                m_sSQL.Append(" ), 'N') AS Activo ");
                m_sSQL.Append("FROM OWHT ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string validarLocalizacion()
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"Country\" from " + Program.SQLBaseDatos + ".\"OADM\" ");
            }
            else
            {
                m_sSQL.Append("SELECT Country FROM OADM ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string buscarRetencion()
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"WTLiable\" from " + Program.SQLBaseDatos + ".\"OADM\" ");
            }
            else
            {
                m_sSQL.Append("SELECT WTLiable FROM OADM ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string stockAlmacen()
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"WarnByWhs\" from " + Program.SQLBaseDatos + ".\"OADM\" ");
            }
            else
            {
                m_sSQL.Append("SELECT WarnByWhs FROM OADM ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string buscarMrp(string Recurso, string Fecha, string Dia)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT IFNULL(T0.\"Capacity\" / T1.\"CapFactor2\", 0)  AS \"CapFactor1\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ORCJ\" T0 INNER JOIN \"RSC6\" T1 ON T1.\"ResCode\" = T0.\"ResCode\" ");
                m_sSQL.AppendFormat("WHERE T0.\"ResCode\" = '{0}' ", Recurso.ToString());
                m_sSQL.AppendFormat("AND T0.\"CapDate\" = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND \"WeekDay\" = '{0}' ", Dia.ToString());
                m_sSQL.Append("AND \"CapType\" = 'I' ");
            }
            else
            {
                m_sSQL.Append("SELECT ISNULL(T0.Capacity / T1.CapFactor2, 0)  AS CapFactor1 ");
                m_sSQL.Append("FROM ORCJ T0 INNER JOIN RSC6 T1  ON  T1.ResCode = T0.ResCode ");
                m_sSQL.AppendFormat("WHERE T0.ResCode = '{0}' ", Recurso.ToString());
                m_sSQL.AppendFormat("AND T0.CapDate = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND WeekDay = '{0}' ", Dia.ToString());
                m_sSQL.Append("AND CapType = 'I' ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string traerOrdenFechaHora(string Orden, string Linea = null)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T1.\"LineNum\", T1.\"ItemCode\", T1.\"U_HorIni\" AS \"HoraInicio\", T1.\"U_HorFin\" AS \"HoraFin\", T1.\"StartDate\" AS \"FechaInicio\", T1.\"EndDate\" AS \"FechaFin\", T1.\"PlannedQty\" AS \"Cantidad\", T1.\"ItemType\" AS \"Tipo\", T1.\"U_Overlap\" AS \"Overlapping\", T1.\"U_Recurso\" AS \"Recurso\", VisOrder ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OWOR\" T0 INNER JOIN \"WOR1\" T1 ON T1.\"DocEntry\" = T0.\"DocEntry\" ");
                m_sSQL.AppendFormat("WHERE T0.\"DocEntry\" = '{0}' ", Orden.ToString());
                if (Linea != null)
                    m_sSQL.AppendFormat("AND \"LineNum\" = '{0}' ", Linea.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT T1.LineNum,T1.ItemCode,T1.U_HorIni AS HoraInicio,T1.U_HorFin AS HoraFin,T1.StartDate AS FechaInicio,T1.EndDate AS FechaFin,T1.PlannedQty AS Cantidad,T1.ItemType AS Tipo,T1.U_Overlap AS Overlapping, T1.U_Recurso AS Recurso, VisOrder ");
                m_sSQL.Append("FROM OWOR T0 INNER JOIN WOR1 T1 ON T0.DocEntry=T1.DocEntry  ");
                m_sSQL.AppendFormat("WHERE T0.DocEntry = '{0}' ", Orden.ToString());
                if (Linea != null)
                    m_sSQL.AppendFormat("AND \"LineNum\" = '{0}' ", Linea.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string horaInicio()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
                m_sSQL.Append("SELECT \"U_BodegaMP\", \"U_BodegaPL\", \"Code\" FROM " + Program.SQLBaseDatos + ".\"@PAPROD\" WHERE \"U_Codigo\" = 'HoraInicio' ");
            else
                m_sSQL.Append("SELECT U_BodegaMP, U_BodegaPL, Code FROM [@PAPROD] WHERE U_Codigo = 'HoraInicio' ");

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string listaAnexos(string Codigo)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"srcPath\",\"FileName\",\"Date\",\"FileExt\" FROM " + Program.SQLBaseDatos + ".\"ATC1\" ");
                m_sSQL.AppendFormat("WHERE \"AbsEntry\" = '{0}' ", Codigo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT srcPath,FileName,Date,FileExt FROM ATC1 ");
                m_sSQL.AppendFormat("WHERE AbsEntry = '{0}' ", Codigo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string anexos(string Codigo)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT IFNULL(\"U_AtcEntry\",0) AS \"U_AtcEntry\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MMODELOS\" ");
                m_sSQL.AppendFormat("WHERE \"Code\" = '{0}' ", Codigo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT isnull(U_AtcEntry,0) AS U_AtcEntry FROM [@EXX_MMODELOS] ");
                m_sSQL.AppendFormat("WHERE Code = '{0}' ", Codigo.ToString());
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string validarTalla(string Codigo, string agrupar)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT IFNULL(\"U_Codigo\",0) AS \"U_Codigo\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
                m_sSQL.AppendFormat("WHERE \"U_Codigo\" = '{0}' ", Codigo.ToString());
                m_sSQL.AppendFormat("AND \"U_Agrupar\" = '{0}' ", agrupar.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT isnull(U_Codigo,0) AS U_Codigo FROM [@EXX_MTALLAS] ");
                m_sSQL.AppendFormat("WHERE U_Codigo = '{0}' ", Codigo.ToString());
                m_sSQL.AppendFormat("AND U_Agrupar = '{0}' ", agrupar.ToString());
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string validarColor(string Codigo)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT IFNULL(\"U_Codigo\",'0') AS \"U_Codigo\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" ");
                m_sSQL.AppendFormat("WHERE \"U_Codigo\" = '{0}' ", Codigo.ToString());

            }
            else
            {
                m_sSQL.Append("SELECT isnull(U_Codigo,0) AS U_Codigo FROM [@EXX_MCOLORES] ");
                m_sSQL.AppendFormat("WHERE U_Codigo = '{0}' ", Codigo.ToString());

            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string traerMrp(string Escenario, string Almacen)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT CASE WHEN \"U_Default\" = 'A' THEN 'Y' ELSE 'N' END AS \"Marcar\", T0.\"ItemCode\" AS \"Articulo\", \"ItemName\" AS \"Nombre\", \"Quantity\" AS \"Cantidad\", \"DueDate\" AS \"Fecha Vencimiento\", \"ReleasDate\" AS \"Fecha Liberacion\", T1.\"U_EXX_CodMod\" AS \"Modelo\", T0.\"DocEntry\", ");
                m_sSQL.Append(" (SELECT \"OnHand\" FROM " + Program.SQLBaseDatos + ".\"OITW\"  WHERE \"ItemCode\" = T0.\"ItemCode\" ");
                m_sSQL.AppendFormat("AND \"WhsCode\" = '{0}' ) AS \"Stock\" ", Almacen.ToString());
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ORCM\" T0 ");
                m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"OITM\" T1 ON T0.\"ItemCode\"=T1.\"ItemCode\" ");
                m_sSQL.AppendFormat("WHERE \"ObjAbs\" = '{0}' ", Escenario.ToString());
                m_sSQL.Append("AND \"OrderType\" = 'W' AND T0.\"Status\" = 'O' AND \"U_EXX_CodMod\" IS NOT NULL AND (T0.\"U_DocGen\" <> 'Y' OR T0.\"U_DocGen\" IS NULL) ");
            }
            else
            {
                m_sSQL.Append("SELECT CASE WHEN U_Default = 'A' THEN 'Y' ELSE 'N' END AS Marcar, T0.ItemCode AS Articulo, ItemName AS Nombre, Quantity AS Cantidad, DueDate AS 'Fecha Vencimiento', ReleasDate AS 'Fecha Liberacion', T1.U_EXX_CodMod AS 'Modelo', T0.DocEntry, ");
                m_sSQL.Append(" (SELECT OnHand FROM OITW  WHERE ItemCode = T0.ItemCode ");
                m_sSQL.AppendFormat("AND WhsCode = '{0}' ) AS Stock ", Almacen.ToString());
                m_sSQL.Append(" FROM ORCM T0 INNER JOIN OITM T1 ON T0.ItemCode=T1.ItemCode ");
                m_sSQL.AppendFormat("WHERE ObjAbs = '{0}' ", Escenario.ToString());
                m_sSQL.Append("AND OrderType = 'W' AND T0.Status = 'O' AND U_EXX_CodMod IS NOT NULL AND (T0.U_DocGen <> 'Y' OR T0.U_DocGen IS NULL) ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string OrdenMrp(string Escenario, string Fecha, string Modelo)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T2.\"Code\", T2.\"Type\", SUM((T2.\"Quantity\"/T1.\"Qauntity\") * T0.\"Quantity\") AS \"Cantidad\", T2.\"ChildNum\", \"U_Recurso\", T2.\"Warehouse\"  ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ORCM\" T0 INNER JOIN " + Program.SQLBaseDatos + ".\"OITT\" T1 ON T0.\"ItemCode\"=T1.\"Code\" INNER JOIN " + Program.SQLBaseDatos + ".\"ITT1\" T2 ON T1.\"Code\"=T2.\"Father\" INNER JOIN " + Program.SQLBaseDatos + ".\"OITM\" T3 ON T3.\"ItemCode\"=T0.\"ItemCode\" ");
                m_sSQL.AppendFormat("WHERE \"ObjAbs\" = '{0}' ", Escenario.ToString());
                m_sSQL.AppendFormat("AND T0.\"DueDate\" = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.Append("AND \"OrderType\" = 'W' AND T0.\"Status\" = 'O' ");
                m_sSQL.Append("GROUP BY T2.\"Code\", T2.\"Type\", T2.\"ChildNum\", \"U_Recurso\", T2.\"Warehouse\" ");
                m_sSQL.Append("ORDER BY T2.\"ChildNum\" ");
            }
            else
            {
                m_sSQL.Append("SELECT T2.Code, T2.Type, SUM((T2.Quantity/T1.Qauntity) * T0.Quantity) AS Cantidad, T2.ChildNum, U_Recurso, T2.Warehouse ");
                m_sSQL.Append("FROM ORCM T0 INNER JOIN OITT T1 ON T0.ItemCode=T1.Code INNER JOIN ITT1 T2 ON T1.Code=T2.Father INNER JOIN OITM T3 ON T3.ItemCode=T0.ItemCode ");
                m_sSQL.AppendFormat("WHERE ObjAbs = '{0}' ", Escenario.ToString());
                m_sSQL.AppendFormat("AND T0.DueDate = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.Append("AND OrderType = 'W' AND T0.Status = 'O' ");
                m_sSQL.Append("GROUP BY T2.Code, T2.Type, T2.ChildNum, U_Recurso, T2.Warehouse ");
                m_sSQL.Append("ORDER BY T2.ChildNum ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string OrdenMrpNuevo(string Escenario, string Fecha, string Modelo, string Articulo)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T2.\"Code\", T2.\"Type\", SUM((T2.\"Quantity\"/T1.\"Qauntity\") * T0.\"Quantity\") AS \"Cantidad\", T2.\"ChildNum\", \"U_Recurso\", T2.\"Warehouse\"  ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ORCM\" T0 INNER JOIN " + Program.SQLBaseDatos + ".\"OITT\" T1 ON T0.\"ItemCode\"=T1.\"Code\" INNER JOIN " + Program.SQLBaseDatos + ".\"ITT1\" T2 ON T1.\"Code\"=T2.\"Father\" INNER JOIN " + Program.SQLBaseDatos + ".\"OITM\" T3 ON T3.\"ItemCode\"=T0.\"ItemCode\" ");
                m_sSQL.AppendFormat("WHERE \"ObjAbs\" = '{0}' ", Escenario.ToString());
                m_sSQL.AppendFormat("AND T0.\"DueDate\" = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND T1.\"Code\" = '{0}' ", Articulo.ToString());
                m_sSQL.Append("AND \"OrderType\" = 'W' AND T0.\"Status\" = 'O' ");
                m_sSQL.Append("GROUP BY T2.\"Code\", T2.\"Type\", T2.\"ChildNum\", \"U_Recurso\", T2.\"Warehouse\" ");
                m_sSQL.Append("ORDER BY T2.\"ChildNum\" ");
            }
            else
            {
                m_sSQL.Append("SELECT T2.Code, T2.Type, SUM((T2.Quantity/T1.Qauntity) * T0.Quantity) AS Cantidad, T2.ChildNum, U_Recurso, T2.Warehouse ");
                m_sSQL.Append("FROM ORCM T0 INNER JOIN OITT T1 ON T0.ItemCode=T1.Code INNER JOIN ITT1 T2 ON T1.Code=T2.Father INNER JOIN OITM T3 ON T3.ItemCode=T0.ItemCode ");
                m_sSQL.AppendFormat("WHERE ObjAbs = '{0}' ", Escenario.ToString());
                m_sSQL.AppendFormat("AND T0.DueDate = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND T1.Code = '{0}' ", Articulo.ToString());
                m_sSQL.Append("AND OrderType = 'W' AND T0.Status = 'O' ");
                m_sSQL.Append("GROUP BY T2.Code, T2.Type, T2.ChildNum, U_Recurso, T2.Warehouse ");
                m_sSQL.Append("ORDER BY T2.ChildNum ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string ActualizarMrp(string DocEntry)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.AppendFormat("UPDATE " + Program.SQLBaseDatos + ".\"ORCM\" SET \"U_DocGen\" = 'Y' WHERE \"DocEntry\" = '{0}' ", DocEntry.ToString());
            }
            else
            {
                m_sSQL.AppendFormat("UPDATE ORCM SET U_DocGen = 'Y' WHERE DocEntry = '{0}' ", DocEntry.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Orden"></param>
        /// <param name="Cantidad"></param>
        /// <returns></returns>
        public static string cargarOrdenEmisionCosto(string Orden)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T0.\"DocEntry\", (IFNULL((SELECT SUM(\"Quantity\" * \"StockPrice\") AS \"Costo\" FROM " + Program.SQLBaseDatos + ".\"IGE1\" WHERE \"BaseType\" = 202 ");
                m_sSQL.AppendFormat("AND \"BaseEntry\" = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) - ");
                m_sSQL.Append(" IFNULL((SELECT SUM(\"Quantity\" * \"StockPrice\") AS \"Costo\" FROM " + Program.SQLBaseDatos + ".\"IGN1\" WHERE \"BaseType\" = 202  ");
                m_sSQL.AppendFormat("AND \"BaseEntry\" = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0)) AS \"Costo\", \"U_EXX_TalCol\", ");
                m_sSQL.Append(" (SELECT  SUM(\"PlannedQty\") FROM " + Program.SQLBaseDatos + ".\"WOR1\" T0 WHERE \"PlannedQty\" < 0 ");
                m_sSQL.AppendFormat("AND T0.\"DocEntry\" = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ) AS \"Cantidad\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OWOR\" T0 ");
                m_sSQL.AppendFormat("WHERE T0.\"DocEntry\" = '{0}' ", Orden.ToString());
                //if (System.Configuration.ConfigurationManager.AppSettings["TallaColor"].ToString() == "SI")
                m_sSQL.Append("AND T0.\"U_EXX_TalCol\" = 'Y' ");
            }
            else
            {
                m_sSQL.Append("SELECT T0.DocEntry, (ISNULL((SELECT SUM(Quantity * StockPrice) AS Costo FROM IGE1 WHERE BaseType = 202 ");
                m_sSQL.AppendFormat("AND BaseEntry = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) - ");
                m_sSQL.Append(" ISNULL((SELECT SUM(Quantity * StockPrice) AS Costo FROM IGN1 WHERE BaseType = 202  ");
                m_sSQL.AppendFormat("AND BaseEntry = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0)) AS Costo, U_EXX_TalCol, ");
                m_sSQL.Append(" (SELECT  SUM(PlannedQty) FROM WOR1 T0 WHERE PlannedQty < 0 ");
                m_sSQL.AppendFormat("AND T0.DocEntry = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ) AS Cantidad ");
                m_sSQL.Append("FROM OWOR T0 ");
                m_sSQL.AppendFormat("WHERE T0.DocEntry = '{0}' ", Orden.ToString());
                //if (System.Configuration.ConfigurationManager.AppSettings["TallaColor"].ToString() == "SI")
                m_sSQL.Append("AND U_EXX_TalCol = 'Y' ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Orden"></param>
        /// <param name="Cantidad"></param>
        /// <returns></returns>
        public static string cargarRevalorizacion(string Orden)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {

                m_sSQL.Append("SELECT T0.\"DocEntry\", IFNULL((SELECT SUM(\"Quantity\" * \"StockPrice\") AS \"Costo\" FROM " + Program.SQLBaseDatos + ".\"IGN1\" WHERE \"BaseType\" = 202 ");
                m_sSQL.AppendFormat("AND \"BaseEntry\" = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) - ");
                m_sSQL.Append(" (IFNULL((SELECT SUM(\"Quantity\" * \"StockPrice\") AS \"Costo\" FROM " + Program.SQLBaseDatos + ".\"IGE1\" WHERE \"BaseType\" = 202 AND \"ItemType\" = '290'  ");
                m_sSQL.AppendFormat("AND \"BaseEntry\" = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) + IFNULL((SELECT SUM(\"TransValue\") * -1  AS \"Costo\" FROM " + Program.SQLBaseDatos + ".\"OINM\" WHERE \"TransType\"='60' ");
                m_sSQL.AppendFormat("AND \"AppObjAbs\" = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0)) AS \"Costo\", ");
                m_sSQL.Append(" IFNULL((SELECT COUNT(\"IssuedQty\") FROM " + Program.SQLBaseDatos + ".\"WOR1\" WHERE T1.\"PlannedQty\" < 0 AND T1.\"IssuedQty\" < 0  ");
                m_sSQL.AppendFormat("AND T1.\"DocEntry\" = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) AS \"Linea\", ");

                m_sSQL.Append(" IFNULL((SELECT SUM(\"IssuedQty\") FROM " + Program.SQLBaseDatos + ".\"WOR1\" WHERE \"PlannedQty\" < 0 AND \"IssuedQty\" < 0  ");
                m_sSQL.AppendFormat("AND \"DocEntry\" = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) AS \"Cantidad\", T1.\"ItemCode\", T1.\"IssuedQty\",T1.\"wareHouse\", ");

                m_sSQL.Append(" IFNULL((SELECT \"OnHand\" FROM " + Program.SQLBaseDatos + ".\"OITW\" WHERE \"WhsCode\" = T1.\"wareHouse\" AND \"ItemCode\" = T1.\"ItemCode\"  ");
                m_sSQL.Append(" ),0) AS \"Stock\" ");

                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OWOR\" T0 ");
                m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"WOR1\" T1 ON T0.\"DocEntry\"=T1.\"DocEntry\" ");
                m_sSQL.AppendFormat("WHERE T0.\"DocEntry\" = '{0}' ", Orden.ToString());
                m_sSQL.Append("AND T1.\"PlannedQty\" < 0 AND T1.\"IssuedQty\" < 0 ");
                //if (System.Configuration.ConfigurationManager.AppSettings["TallaColor"].ToString() == "SI")
                //    m_sSQL.Append("AND T0.\"U_EXX_TalCol\" = 'Y' ");
            }
            else
            {

                m_sSQL.Append("SELECT T0.DocEntry, ISNULL((SELECT SUM(Quantity * StockPrice) AS Costo FROM IGN1 WHERE BaseType = 202 ");
                m_sSQL.AppendFormat("AND BaseEntry = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) - ");
                m_sSQL.Append(" ( ISNULL((SELECT SUM(Quantity * StockPrice) AS Costo FROM IGE1 WHERE BaseType = 202 AND ItemType = '290' ");
                m_sSQL.AppendFormat("AND BaseEntry = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) + ISNULL((SELECT SUM(TransValue) * -1  AS Costo FROM OINM WHERE TransType='60' ");
                m_sSQL.AppendFormat("AND AppObjAbs = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0)) AS Costo,  ");
                m_sSQL.Append(" ISNULL((SELECT COUNT (IssuedQty) FROM WOR1 T1 WHERE T1.PlannedQty < 0 AND T1.IssuedQty < 0 ");
                m_sSQL.AppendFormat("AND T1.DocEntry = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) AS Linea,  ");

                m_sSQL.Append(" ISNULL((SELECT SUM(IssuedQty)  FROM WOR1 T1 WHERE PlannedQty < 0 AND IssuedQty < 0  ");
                m_sSQL.AppendFormat("AND DocEntry = '{0}' ", Orden.ToString());
                m_sSQL.Append(" ),0) AS Cantidad, T1.ItemCode, T1.IssuedQty,T1.wareHouse,  ");

                m_sSQL.Append(" ISNULL((SELECT OnHand FROM OITW WHERE WhsCode = T1.wareHouse AND ItemCode = T1.ItemCode),0) AS Stock ");

                m_sSQL.Append("FROM OWOR T0 ");
                m_sSQL.Append("INNER JOIN WOR1 T1 ON T0.DocEntry=T1.DocEntry ");
                m_sSQL.AppendFormat("WHERE T0.DocEntry = '{0}' ", Orden.ToString());
                m_sSQL.Append("AND T1.PlannedQty < 0 AND T1.IssuedQty < 0 ");
                //if (System.Configuration.ConfigurationManager.AppSettings["TallaColor"].ToString() == "SI")
                //    m_sSQL.Append("AND U_EXX_TalCol = 'Y' ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string traerDocEntry(string DocNum)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
                m_sSQL.AppendFormat(" SELECT \"DocEntry\", \"TransId\" FROM " + Program.SQLBaseDatos + ".\"OWOR\" WHERE \"DocNum\" = '{0}' ", DocNum.ToString());
            else
                m_sSQL.AppendFormat("SELECT DocEntry, TransId FROM OWOR WHERE DocNum = '{0}' ", DocNum.ToString());

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string traerAsientoLinea(string DocNum, string Debe = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.AppendFormat(" SELECT \"Account\", \"Debit\", \"Credit\" FROM " + Program.SQLBaseDatos + ".\"JDT1\" WHERE \"TransId\" = '{0}' ", DocNum.ToString());
                if (Debe == "SI")
                    m_sSQL.Append(" AND \"Debit\" > 0 ");
                else
                    m_sSQL.Append(" AND \"Credit\" > 0 ");
            }
            else
            {
                m_sSQL.AppendFormat("SELECT \"Account\", \"Debit\", \"Credit\" FROM JDT1  WHERE TransId = '{0}' ", DocNum.ToString());
                if (Debe == "SI")
                    m_sSQL.Append(" AND Debit > 0 ");
                else
                    m_sSQL.Append(" AND Credit > 0 ");
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Orden"></param>
        /// <param name="Cantidad"></param>
        /// <returns></returns>
        public static string buscarDocEntry(string Orden)
        {
            m_sSQL.Length = 0;

            if (Orden == "")
                Orden = "0";

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T0.\"DocEntry\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OWOR\" T0 ");
                m_sSQL.AppendFormat("WHERE T0.\"DocNum\" = '{0}' ", Orden.ToString());
                //if (System.Configuration.ConfigurationManager.AppSettings["TallaColor"].ToString() == "SI")
                m_sSQL.Append("AND T0.\"U_EXX_TalCol\" = 'Y' ");
            }
            else
            {
                m_sSQL.Append("SELECT T0.DocEntry ");
                m_sSQL.Append("FROM OWOR T0 ");
                m_sSQL.AppendFormat("WHERE T0.DocNum = '{0}' ", Orden.ToString());
                //if (System.Configuration.ConfigurationManager.AppSettings["TallaColor"].ToString() == "SI")
                m_sSQL.Append("AND U_EXX_TalCol = 'Y' ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Orden"></param>
        /// <param name="Cantidad"></param>
        /// <returns></returns>
        public static string existeRecibo(string Orden)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"DocEntry\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OIGN\" ");
                m_sSQL.AppendFormat("WHERE \"DocNum\" = '{0}' ", Orden.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT DocEntry FROM OIGN ");
                m_sSQL.AppendFormat("WHERE DocNum = '{0}' ", Orden.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static string OrdenMrpSubProducto(string Escenario, string Fecha, string Modelo, string DocEntry)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T0.\"ItemCode\" AS \"Articulo\", \"Quantity\" AS \"Cantidad\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ORCM\" T0 INNER JOIN " + Program.SQLBaseDatos + ".\"OITM\" T1 ON T0.\"ItemCode\"=T1.\"ItemCode\" ");
                m_sSQL.AppendFormat("WHERE \"ObjAbs\" = '{0}' ", Escenario.ToString());
                m_sSQL.AppendFormat("AND T0.\"DueDate\" = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND T0.\"DocEntry\" = '{0}' ", DocEntry.ToString());
                m_sSQL.Append("AND \"OrderType\" = 'W' AND T0.\"Status\" = 'O' ");
            }
            else
            {
                m_sSQL.Append("SELECT T0.ItemCode AS Articulo, Quantity AS Cantidad ");
                m_sSQL.Append("FROM ORCM T0 INNER JOIN OITM T1 ON T0.ItemCode=T1.ItemCode ");
                m_sSQL.AppendFormat("WHERE ObjAbs = '{0}' ", Escenario.ToString());
                m_sSQL.AppendFormat("AND T0.DueDate = '{0}' ", Fecha.ToString());
                m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND T0.DocEntry = '{0}' ", DocEntry.ToString());
                m_sSQL.Append("AND OrderType = 'W' AND T0.Status = 'O' ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// Consulta la informacion del articulo
        /// </summary>
        /// <param name="Articulo"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static string recurso(string Recurso)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"ResCode\"  ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ORSC\" ");
                m_sSQL.AppendFormat("WHERE \"ResCode\" = '{0}' ", Recurso.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT ResCode ");
                m_sSQL.Append("FROM ORSC ");
                m_sSQL.AppendFormat("WHERE ResCode = '{0}' ", Recurso.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// Consulta la informacion del articulo
        /// </summary>
        /// <param name="Articulo"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static string recursoCampo()
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"Code\"  ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@RACTUAL1\" ");
                //m_sSQL.AppendFormat("WHERE \"ResCode\" = '{0}' ", Recurso.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT Code ");
                m_sSQL.Append("FROM [@RACTUAL1] ");
                //m_sSQL.AppendFormat("WHERE ResCode = '{0}' ", Recurso.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// Consulta la informacion del articulo
        /// </summary>
        /// <param name="Articulo"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static string borrarTabla()
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE FROM \"@RACTUAL1\" ");
            }
            else
            {
                m_sSQL.Append("DELETE [@RACTUAL1] ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string eliminarMaestro1(string Tipo, string Estado)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"DocEntry\" AS \"Num Orden\", \"U_TipDoc\" AS \"Tipo Transaccion\", \"U_FecDoc\" AS \"Fecha\", \"U_NumDoc\" AS \"Num Documento\", \"U_OrdPla\" AS \"Plan Calidad\", \"U_OrdLot\" AS \"Lote\", \"U_ArtDes\" AS \"Descripcion\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_OCAL\" ");
                m_sSQL.AppendFormat("WHERE \"U_OrdEst\" = '{0}' ", Estado.ToString());
                m_sSQL.AppendFormat("AND \"U_TipDoc\" = '{0}' ", Tipo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT DocEntry AS 'Num Orden', U_TipDoc AS 'Tipo Transaccion', U_FecDoc AS 'Fecha', U_NumDoc AS 'Num Documento', U_OrdPla AS 'Plan Calidad', U_OrdLot AS 'Lote', U_ArtDes AS 'Descripcion' ");
                m_sSQL.Append("FROM [@EXX_OCAL] ");
                m_sSQL.AppendFormat("WHERE U_OrdEst = '{0}' ", Estado.ToString());
                m_sSQL.AppendFormat("AND U_TipDoc = '{0}' ", Tipo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerImagenes(string Code)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_PicturName1\", \"U_PicturName2\", \"U_PicturName3\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MMODELOS\" ");
                m_sSQL.AppendFormat("WHERE \"Code\" = '{0}' ", Code.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT U_PicturName1, U_PicturName2, U_PicturName3 ");
                m_sSQL.Append("FROM [@EXX_MMODELOS] ");
                m_sSQL.AppendFormat("WHERE Code = '{0}' ", Code.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerListaMateriales(string Talla, string Color = null, string Modelo = null, string Valida = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT * FROM ( ");
                m_sSQL.Append("SELECT DISTINCT T2.\"U_EXX_Tipo\", T2.\"U_EXX_Numero\", T2.\"U_EXX_Cant\", T2.\"U_EXX_UniMed\", T2.\"U_EXX_CodAlm\", T2.\"U_EXX_MetEmi\", T2.\"U_EXX_Recurso\", T2.\"Code\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_LISCOLORE\" T0 ");
                m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"@EXX_LISTALLAS\" T1 ON T0.\"U_EXX_CodArt\"=T1.\"U_EXX_CodArt\" AND T0.\"U_EXX_LinNum\"=T1.\"U_EXX_LinNum\" AND T0.\"U_EXX_CodMod\"=T1.\"U_EXX_CodMod\" ");
                m_sSQL.Append("LEFT JOIN " + Program.SQLBaseDatos + ".\"@EXX_MODLISTMAT\" T2 ON T2.\"U_EXX_Numero\"=T0.\"U_EXX_CodArt\" AND T0.\"U_EXX_LinNum\"=T2.\"U_EXX_LinNum\" AND T0.\"U_EXX_CodMod\"=T2.\"U_EXX_CodMod\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodTal\" = '{0}' ", Talla.ToString());
                if (Color != null)
                    m_sSQL.AppendFormat("AND \"U_EXX_CodCol\" = '{0}' ", Color.ToString());
                m_sSQL.Append("AND T2.\"U_EXX_Tipo\" IS NOT NULL ");
                m_sSQL.AppendFormat("AND T2.\"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.Append("UNION ");
                m_sSQL.Append("SELECT DISTINCT T2.\"U_EXX_Tipo\", T2.\"U_EXX_Numero\", T2.\"U_EXX_Cant\", T2.\"U_EXX_UniMed\", T2.\"U_EXX_CodAlm\", T2.\"U_EXX_MetEmi\", T2.\"U_EXX_Recurso\", T2.\"Code\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_LISCOLORE\" T0 ");
                m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"@EXX_LISTALLAS\" T1 ON T0.\"U_EXX_CodArt\"=T1.\"U_EXX_CodArt\" ");
                m_sSQL.Append("RIGHT JOIN " + Program.SQLBaseDatos + ".\"@EXX_MODLISTMAT\" T2 ON T2.\"U_EXX_Numero\"=T0.\"U_EXX_CodArt\" ");
                m_sSQL.AppendFormat("WHERE T2.\"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                if (Valida == "C")
                    m_sSQL.Append("AND T1.\"Code\" IS NULL ");
                m_sSQL.Append(") TC ORDER BY \"Code\" ");
            }
            else
            {
                m_sSQL.Append("SELECT * FROM ( ");
                m_sSQL.Append("SELECT DISTINCT T2.U_EXX_Tipo, T2.U_EXX_Numero, T2.U_EXX_Cant, T2.U_EXX_UniMed, T2.U_EXX_CodAlm, T2.U_EXX_MetEmi, T2.U_EXX_Recurso, T2.Code ");
                m_sSQL.Append("FROM [@EXX_LISCOLORE] T0 ");
                m_sSQL.Append("INNER JOIN [@EXX_LISTALLAS] T1 ON T0.U_EXX_CodArt=T1.U_EXX_CodArt AND T0.U_EXX_LinNum=T1.U_EXX_LinNum AND T0.U_EXX_CodMod=T1.U_EXX_CodMod ");
                m_sSQL.Append("LEFT JOIN [@EXX_MODLISTMAT] T2 ON T2.U_EXX_Numero=T0.U_EXX_CodArt AND T0.U_EXX_LinNum=T2.U_EXX_LinNum AND T0.U_EXX_CodMod=T2.U_EXX_CodMod ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodTal = '{0}' ", Talla.ToString());
                if (Color != null)
                    m_sSQL.AppendFormat("AND U_EXX_CodCol = '{0}' ", Color.ToString());
                m_sSQL.Append("AND T2.U_EXX_Tipo IS NOT NULL ");
                m_sSQL.AppendFormat("AND T2.U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.Append("UNION ");
                m_sSQL.Append("SELECT DISTINCT T2.U_EXX_Tipo, T2.U_EXX_Numero, T2.U_EXX_Cant, T2.U_EXX_UniMed, T2.U_EXX_CodAlm, T2.U_EXX_MetEmi, T2.U_EXX_Recurso, T2.Code ");
                m_sSQL.Append("FROM [@EXX_LISCOLORE] T0 ");
                m_sSQL.Append("INNER JOIN [@EXX_LISTALLAS] T1 ON T0.U_EXX_CodArt=T1.U_EXX_CodArt ");
                m_sSQL.Append("RIGHT JOIN [@EXX_MODLISTMAT] T2 ON T2.U_EXX_Numero=T0.U_EXX_CodArt ");
                m_sSQL.AppendFormat("WHERE T2.U_EXX_CodMod = '{0}' ", Modelo.ToString());
                if (Valida == "C")
                    m_sSQL.Append("AND T1.Code IS NULL ");
                m_sSQL.Append(") TC ORDER BY Code ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerListaMaterialesValida(string Talla, string Color = null, string Modelo = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT * FROM ( ");
                m_sSQL.Append("SELECT DISTINCT T2.\"U_EXX_Tipo\", T2.\"U_EXX_Numero\", T2.\"U_EXX_Cant\", T2.\"U_EXX_UniMed\", T2.\"U_EXX_CodAlm\", T2.\"U_EXX_MetEmi\", T2.\"U_EXX_Recurso\", T2.\"Code\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_LISCOLORE\" T0 ");
                m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"@EXX_LISTALLAS\" T1 ON T0.\"U_EXX_CodArt\"=T1.\"U_EXX_CodArt\" AND T0.\"U_EXX_LinNum\"=T1.\"U_EXX_LinNum\" AND T0.\"U_EXX_CodMod\"=T1.\"U_EXX_CodMod\" ");
                m_sSQL.Append("LEFT JOIN " + Program.SQLBaseDatos + ".\"@EXX_MODLISTMAT\" T2 ON T2.\"U_EXX_Numero\"=T0.\"U_EXX_CodArt\" AND T0.\"U_EXX_LinNum\"=T2.\"U_EXX_LinNum\" AND T0.\"U_EXX_CodMod\"=T2.\"U_EXX_CodMod\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodTal\" = '{0}' ", Talla.ToString());
                if (Color != null)
                    m_sSQL.AppendFormat(" AND \"U_EXX_CodCol\" = '{0}' ", Color.ToString());
                m_sSQL.Append("AND T2.\"U_EXX_Tipo\" IS NOT NULL ");
                m_sSQL.AppendFormat("AND T2.\"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                //m_sSQL.Append("UNION ");
                //m_sSQL.Append("SELECT DISTINCT T2.\"U_EXX_Tipo\", T2.\"U_EXX_Numero\", T2.\"U_EXX_Cant\", T2.\"U_EXX_UniMed\", T2.\"U_EXX_CodAlm\", T2.\"U_EXX_MetEmi\", T2.\"U_EXX_Recurso\", T2.\"Code\" ");
                //m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_LISCOLORE\" T0 ");
                //m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"@EXX_LISTALLAS\" T1 ON T0.\"U_EXX_CodArt\"=T1.\"U_EXX_CodArt\" ");
                //m_sSQL.Append("RIGHT JOIN " + Program.SQLBaseDatos + ".\"@EXX_MODLISTMAT\" T2 ON T2.\"U_EXX_Numero\"=T0.\"U_EXX_CodArt\" ");
                //m_sSQL.AppendFormat("WHERE T2.\"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.Append(") TC ORDER BY \"Code\" ");
            }
            else
            {
                m_sSQL.Append("SELECT * FROM ( ");
                m_sSQL.Append("SELECT DISTINCT T2.U_EXX_Tipo, T2.U_EXX_Numero, T2.U_EXX_Cant, T2.U_EXX_UniMed, T2.U_EXX_CodAlm, T2.U_EXX_MetEmi, T2.U_EXX_Recurso, T2.Code ");
                m_sSQL.Append("FROM [@EXX_LISCOLORE] T0 ");
                m_sSQL.Append("INNER JOIN [@EXX_LISTALLAS] T1 ON T0.U_EXX_CodArt=T1.U_EXX_CodArt AND T0.U_EXX_LinNum=T1.U_EXX_LinNum AND T0.U_EXX_CodMod=T1.U_EXX_CodMod ");
                m_sSQL.Append("LEFT JOIN [@EXX_MODLISTMAT] T2 ON T2.U_EXX_Numero=T0.U_EXX_CodArt AND T0.U_EXX_LinNum=T2.U_EXX_LinNum AND T0.U_EXX_CodMod=T2.U_EXX_CodMod ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodTal = '{0}' ", Talla.ToString());
                if (Color != null)
                    m_sSQL.AppendFormat("AND U_EXX_CodCol = '{0}' ", Color.ToString());
                m_sSQL.Append("AND T2.U_EXX_Tipo IS NOT NULL ");
                m_sSQL.AppendFormat("AND T2.U_EXX_CodMod = '{0}' ", Modelo.ToString());
                //m_sSQL.Append("UNION ");
                //m_sSQL.Append("SELECT DISTINCT T2.U_EXX_Tipo, T2.U_EXX_Numero, T2.U_EXX_Cant, T2.U_EXX_UniMed, T2.U_EXX_CodAlm, T2.U_EXX_MetEmi, T2.U_EXX_Recurso, T2.Code ");
                //m_sSQL.Append("FROM [@EXX_LISCOLORE] T0 ");
                //m_sSQL.Append("INNER JOIN [@EXX_LISTALLAS] T1 ON T0.U_EXX_CodArt=T1.U_EXX_CodArt ");
                //m_sSQL.Append("RIGHT JOIN [@EXX_MODLISTMAT] T2 ON T2.U_EXX_Numero=T0.U_EXX_CodArt ");
                //m_sSQL.AppendFormat("WHERE T2.U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.Append(") TC ORDER BY Code ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string eliminarMaestro(string Tipo, string Code = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE ");
                if (Tipo == "T")
                {
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
                    m_sSQL.AppendFormat(" WHERE \"U_Codigo\" = '{0}' ", Code.ToString());
                }
                else if (Tipo == "C")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLECCION\" ");
                else
                {
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" ");
                    m_sSQL.AppendFormat(" WHERE \"U_Codigo\" = '{0}' ", Code.ToString());
                }
            }
            else
            {
                m_sSQL.Append("DELETE ");
                if (Tipo == "T")
                {
                    m_sSQL.Append("FROM [@EXX_MTALLAS] ");
                    m_sSQL.AppendFormat(" WHERE U_Codigo = '{0}' ", Code.ToString());
                }
                else if (Tipo == "C")
                    m_sSQL.Append("FROM [@EXX_MCOLECCION] ");
                else
                {
                    m_sSQL.Append("FROM [@EXX_MCOLORES] ");
                    m_sSQL.AppendFormat(" WHERE U_Codigo = '{0}' ", Code.ToString());
                }
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string actualizarMaestro(string Tipo, string Code = null, string Name = null, string Agrupar = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo == "T")
                {
                    m_sSQL.Append("UPDATE " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\"  ");
                    m_sSQL.AppendFormat(" SET \"Name\" = '{0}' ", Name.ToString());
                    m_sSQL.AppendFormat(", \"U_Agrupar\" = '{0}' ", Agrupar.ToString());
                    m_sSQL.AppendFormat(" WHERE \"U_Codigo\" = '{0}' ", Code.ToString());
                }
                else if (Tipo == "C")
                    m_sSQL.Append("UPDATE " + Program.SQLBaseDatos + ".\"@EXX_MCOLECCION\" ");
                else
                {
                    m_sSQL.Append("UPDATE " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" ");
                    m_sSQL.AppendFormat(" SET \"Name\" = '{0}' ", Name.ToString());
                    m_sSQL.AppendFormat(" WHERE \"U_Codigo\" = '{0}' ", Code.ToString());
                }
            }
            else
            {

                if (Tipo == "T")
                {
                    m_sSQL.Append("UPDATE [@EXX_MTALLAS] ");
                    m_sSQL.AppendFormat(" SET Name = '{0}' ", Name.ToString());
                    m_sSQL.AppendFormat(", U_Agrupar = '{0}' ", Agrupar.ToString());
                    m_sSQL.AppendFormat(" WHERE U_Codigo = '{0}' ", Code.ToString());
                }
                else if (Tipo == "C")
                    m_sSQL.Append("UPDATE [@EXX_MCOLECCION] ");
                else
                {
                    m_sSQL.Append("UPDATE [@EXX_MCOLORES] ");
                    m_sSQL.AppendFormat(" SET Name = '{0}' ", Name.ToString());
                    m_sSQL.AppendFormat(" WHERE U_Codigo = '{0}' ", Code.ToString());
                }
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string buscarMaestro(string Tipo, string Code = null, string Name = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo == "T")
                {
                    m_sSQL.Append("SELECT \"U_Codigo\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\"  ");
                    m_sSQL.AppendFormat(" WHERE \"U_Codigo\" = '{0}' ", Code.ToString());
                }
                else if (Tipo == "C")
                    m_sSQL.Append("UPDATE " + Program.SQLBaseDatos + ".\"@EXX_MCOLECCION\" ");
                else
                {
                    m_sSQL.Append(" SELECT \"U_Codigo\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" ");
                    m_sSQL.AppendFormat(" WHERE \"U_Codigo\" = '{0}' ", Code.ToString());
                }
            }
            else
            {

                if (Tipo == "T")
                {
                    m_sSQL.Append("FROM [@EXX_MTALLAS] ");
                    m_sSQL.AppendFormat(" SET Name = '{0}' ", Name.ToString());
                    m_sSQL.AppendFormat(" WHERE U_Codigo = '{0}' ", Code.ToString());
                }
                else if (Tipo == "C")
                    m_sSQL.Append("FROM [@EXX_MCOLECCION] ");
                else
                {
                    m_sSQL.Append("FROM [@EXX_MCOLORES] ");
                    m_sSQL.AppendFormat(" SET Name = '{0}' ", Name.ToString());
                    m_sSQL.AppendFormat(" WHERE U_Codigo = '{0}' ", Code.ToString());
                }
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string eliminarModelo(string Tipo, string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE ");
                if (Tipo == "T")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODTALLAS\" ");
                else
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODCOLORES\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("DELETE ");
                if (Tipo == "T")
                    m_sSQL.Append("FROM [@EXX_MODTALLAS] ");
                else
                    m_sSQL.Append("FROM [@EXX_MODCOLORES] ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());

            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string eliminarModeloData(string Tipo, string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE ");
                if (Tipo == "T")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODTALLAS\" ");
                else
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_COLDATA\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("DELETE ");
                if (Tipo == "T")
                    m_sSQL.Append("FROM [@EXX_MODTALLAS] ");
                else
                    m_sSQL.Append("FROM [@EXX_COLDATA] ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());

            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string eliminarPropiedad(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODPROPIE\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("DELETE ");
                m_sSQL.Append("FROM [@EXX_MODPROPIE] ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());

            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string eliminarModeloLista(string Tipo, string Modelo, string Articulo, string Linea)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE ");
                if (Tipo == "T")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_LISTALLAS\" ");
                else
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_LISCOLORE\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND \"U_EXX_CodArt\" = '{0}' ", Articulo.ToString());
                m_sSQL.AppendFormat("AND \"U_EXX_LinNum\" = '{0}' ", Linea.ToString());
            }
            else
            {
                m_sSQL.Append("DELETE ");
                if (Tipo == "T")
                    m_sSQL.Append("FROM [@EXX_LISTALLAS] ");
                else
                    m_sSQL.Append("FROM [@EXX_LISCOLORE] ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND U_EXX_CodArt = '{0}' ", Articulo.ToString());
                m_sSQL.AppendFormat("AND U_EXX_LinNum = '{0}' ", Linea.ToString());

            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string eliminarModeloOF(string Talla, string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_OFCOLORES\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND \"U_EXX_CodTal\" = '{0}' ", Talla.ToString());
            }
            else
            {
                m_sSQL.Append("DELETE ");
                m_sSQL.Append("FROM [@EXX_OFCOLORES] ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND U_EXX_CodTal = '{0}' ", Talla.ToString());

            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string eliminarModeloListaMaterial(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("DELETE ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODLISTMAT\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("DELETE ");
                m_sSQL.Append("FROM [@EXX_MODLISTMAT] ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadMaestro(string Tipo, string Agrupar = null, string Agregar = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo == "T")
                {
                    if ((Agrupar != null) && (Agregar == "SI"))
                        m_sSQL.Append("SELECT 'Y' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                    else
                        m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                }
                else if (Tipo != "")
                    m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\", \"Name\" ");
                else
                    m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Color\" AS \"Color\" ");

                if (Tipo == "T")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
                else if (Tipo == "C")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLECCION\" ");
                else
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" ");

                if ((Tipo == "T") && (Agrupar != null))
                {
                    m_sSQL.AppendFormat("WHERE \"U_Agrupar\" = '{0}' ", Agrupar.ToString());
                    m_sSQL.Append("UNION ALL ");
                    m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
                    m_sSQL.AppendFormat("WHERE \"U_Agrupar\" <> '{0}' ", Agrupar.ToString());
                }
            }
            else
            {
                if (Tipo == "T")
                {
                    if ((Agrupar != null) && (Agregar == "SI"))
                        m_sSQL.Append("SELECT 'Y' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar ");
                    else
                        m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar ");
                }
                else if (Tipo != "")
                    m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo, Name ");
                else
                    m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Color AS Color ");

                if (Tipo == "T")
                    m_sSQL.Append("FROM [@EXX_MTALLAS] ");
                else if (Tipo == "C")
                    m_sSQL.Append("FROM [@EXX_MCOLECCION] ");
                else
                    m_sSQL.Append("FROM [@EXX_MCOLORES] ");

                if ((Tipo == "T") && (Agrupar != null))
                {
                    m_sSQL.AppendFormat("WHERE U_Agrupar = '{0}' ", Agrupar.ToString());
                    m_sSQL.Append("UNION ALL ");
                    m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar ");
                    m_sSQL.Append("FROM [@EXX_MTALLAS] ");
                    m_sSQL.AppendFormat("WHERE U_Agrupar <> '{0}' ", Agrupar.ToString());
                }
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadMaestroColor(string Tipo, string Valor, string Agrupar = null, string Agregar = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo == "T")
                {
                    if ((Agrupar != null) && (Agregar == "SI"))
                        m_sSQL.Append("SELECT 'Y' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                    else
                        m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                }
                else if (Tipo != "")
                    m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\", \"Name\" ");
                else
                    m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Color\" AS \"Color\" ");

                if (Tipo == "T")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
                else if (Tipo == "C")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLECCION\" ");
                else
                {
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" ");
                    m_sSQL.AppendFormat("WHERE \"U_Codigo\" LIKE '%{0}%' ", Valor.ToString());
                }

                if ((Tipo == "T") && (Agrupar != null))
                {
                    m_sSQL.AppendFormat("WHERE \"U_Agrupar\" = '{0}' ", Agrupar.ToString());
                    m_sSQL.Append("UNION ALL ");
                    m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
                    m_sSQL.AppendFormat("WHERE \"U_Agrupar\" <> '{0}' ", Agrupar.ToString());
                }
            }
            else
            {
                if (Tipo == "T")
                {
                    if ((Agrupar != null) && (Agregar == "SI"))
                        m_sSQL.Append("SELECT 'Y' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar ");
                    else
                        m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar ");
                }
                else if (Tipo != "")
                    m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo, Name ");
                else
                    m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Color AS Color ");

                if (Tipo == "T")
                    m_sSQL.Append("FROM [@EXX_MTALLAS] ");
                else if (Tipo == "C")
                    m_sSQL.Append("FROM [@EXX_MCOLECCION] ");
                else
                {
                    m_sSQL.Append("FROM [@EXX_MCOLORES] ");
                    m_sSQL.AppendFormat("WHERE U_Codigo LIKE '%{0}%' ", Valor.ToString());
                }

                if ((Tipo == "T") && (Agrupar != null))
                {
                    m_sSQL.AppendFormat("WHERE U_Agrupar = '{0}' ", Agrupar.ToString());
                    m_sSQL.Append("UNION ALL ");
                    m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar ");
                    m_sSQL.Append("FROM [@EXX_MTALLAS] ");
                    m_sSQL.AppendFormat("WHERE U_Agrupar <> '{0}' ", Agrupar.ToString());
                }
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadMaestroGrilla(string Tipo, string Agrupar = null, string Agregar = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo == "T")
                {
                    if ((Agrupar != null) && (Agregar == "SI"))
                        m_sSQL.Append("SELECT \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\", 'Y' AS \"Marcar\"  ");
                    else
                        m_sSQL.Append("SELECT \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\", 'N' AS \"Eliminar\"  ");
                }
                else if (Tipo != "")
                    m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\", \"Name\" ");
                else
                    m_sSQL.Append("SELECT \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Color\" AS \"Color\", '' AS \"Column\", 'N' AS \"Eliminar\" ");

                if (Tipo == "T")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
                else if (Tipo == "C")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLECCION\" ");
                else
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" ");

                if ((Tipo == "T") && (Agrupar != null))
                {
                    m_sSQL.AppendFormat("WHERE \"U_Agrupar\" = '{0}' ", Agrupar.ToString());
                    m_sSQL.Append("UNION ALL ");
                    m_sSQL.Append("SELECT 'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
                    m_sSQL.AppendFormat("WHERE \"U_Agrupar\" <> '{0}' ", Agrupar.ToString());
                }
            }
            else
            {
                if (Tipo == "T")
                {
                    if ((Agrupar != null) && (Agregar == "SI"))
                        m_sSQL.Append("SELECT  U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar, 'Y' AS 'Marcar' ");
                    else
                        m_sSQL.Append("SELECT  U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar, 'N' AS 'Eliminar' ");
                }
                else if (Tipo != "")
                    m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo, Name ");
                else
                    m_sSQL.Append("SELECT  U_Codigo AS Codigo, Name AS Descripcion, U_Color AS Color, '' AS 'Column','N' AS 'Eliminar' ");

                if (Tipo == "T")
                    m_sSQL.Append("FROM [@EXX_MTALLAS] ");
                else if (Tipo == "C")
                    m_sSQL.Append("FROM [@EXX_MCOLECCION] ");
                else
                    m_sSQL.Append("FROM [@EXX_MCOLORES] ");

                if ((Tipo == "T") && (Agrupar != null))
                {
                    m_sSQL.AppendFormat("WHERE U_Agrupar = '{0}' ", Agrupar.ToString());
                    m_sSQL.Append("UNION ALL ");
                    m_sSQL.Append("SELECT 'N' AS 'Marcar', U_Codigo AS Codigo, Name AS Descripcion, U_Agrupar AS Agrupar ");
                    m_sSQL.Append("FROM [@EXX_MTALLAS] ");
                    m_sSQL.AppendFormat("WHERE U_Agrupar <> '{0}' ", Agrupar.ToString());
                }
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadMaestroUpdate(string Tipo, string Modelo, string Agrupar = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo != "")
                {
                    m_sSQL.Append("SELECT IFNULL((SELECT \"U_EXX_Defaul\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MODTALLAS\" WHERE \"U_EXX_CodTal\" = \"U_Codigo\" ");
                    m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                    if (Tipo == "T")
                        m_sSQL.Append(" ), 'N') AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                    else
                        m_sSQL.Append(" ), 'N') AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\" ");
                }
                else
                {
                    m_sSQL.Append("SELECT  IFNULL((SELECT \"U_EXX_Defaul\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MODCOLORES\" WHERE \"U_EXX_CodCol\" = \"U_Codigo\" ");
                    m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                    m_sSQL.Append(" ), 'N') AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Color\" AS \"C\" ");
                }

                if (Tipo == "T")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" T0 ");
                else if (Tipo == "C")
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLECCION\" ");
                else
                {
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MCOLORES\" T0 ");
                }

                if ((Tipo == "T") && (Agrupar != null))
                {
                    m_sSQL.AppendFormat("WHERE \"U_Agrupar\" = '{0}' ", Agrupar.ToString());
                    m_sSQL.Append("UNION ALL ");
                    m_sSQL.Append("SELECT IFNULL((SELECT \"U_EXX_Defaul\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MODTALLAS\" WHERE \"U_EXX_CodTal\" = \"U_Codigo\" ");
                    m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                    m_sSQL.AppendFormat("AND \"U_EXX_Agrupa\" <> '{0}' ", Agrupar.ToString());
                    m_sSQL.Append(" ), 'N') AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" T0 ");
                    m_sSQL.AppendFormat("WHERE \"U_Agrupar\" <> '{0}' ", Agrupar.ToString());
                }

            }
            else
            {
                if (Tipo != "")
                {
                    m_sSQL.Append("SELECT isnull((Select U_EXX_Defaul from [@EXX_MODTALLAS] where U_EXX_CodTal = U_Codigo ");
                    m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                    if (Tipo == "T")
                        m_sSQL.Append(" ), 'N') AS 'Marcar', U_Codigo AS 'Codigo', Name AS 'Descripcion', U_Agrupar AS Agrupar ");
                    else
                        m_sSQL.Append(" ), 'N') AS 'Marcar', U_Codigo AS 'Codigo', Name AS 'Descripcion' ");
                }
                else
                {
                    m_sSQL.Append("SELECT isnull((Select U_EXX_Defaul from [@EXX_MODCOLORES] where U_EXX_CodCol = U_Codigo ");
                    m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                    m_sSQL.Append(" ), 'N') AS 'Marcar', U_Codigo AS 'Codigo', Name AS 'Descripcion', U_Color AS 'C' ");
                }

                if (Tipo == "T")
                    m_sSQL.Append("FROM [@EXX_MTALLAS] T0 ");
                else if (Tipo == "C")
                    m_sSQL.Append("FROM [@EXX_MCOLECCION] ");
                else
                    m_sSQL.Append("FROM [@EXX_MCOLORES] T0 ");


                if ((Tipo == "T") && (Agrupar != null))
                {
                    m_sSQL.AppendFormat("WHERE U_Agrupar = '{0}' ", Agrupar.ToString());
                    m_sSQL.Append("UNION ALL ");
                    m_sSQL.Append("SELECT isnull((Select U_EXX_Defaul from [@EXX_MODTALLAS] where U_EXX_CodTal = U_Codigo ");
                    m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                    m_sSQL.AppendFormat("AND U_EXX_Agrupa <> '{0}' ", Agrupar.ToString());
                    m_sSQL.Append(" ), 'N') AS 'Marcar', U_Codigo AS 'Codigo', Name AS 'Descripcion', U_Agrupar AS Agrupar ");
                    m_sSQL.Append("FROM [@EXX_MTALLAS] T0 ");
                    m_sSQL.AppendFormat("WHERE U_Agrupar <> '{0}' ", Agrupar.ToString());
                }
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadCambioCombo(string Tipo, string Modelo, string Agrupar = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {

                m_sSQL.Append(" SELECT 'Y' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" T0 ");
                m_sSQL.AppendFormat(" WHERE \"U_Agrupar\" = '{0}' ", Agrupar.ToString());
                m_sSQL.Append("UNION ALL ");
                m_sSQL.Append(" SELECT 'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Agrupar\" AS \"Agrupar\" FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" T0 ");
                m_sSQL.AppendFormat(" WHERE \"U_Agrupar\" <> '{0}' ", Agrupar.ToString());
            }
            else
            {

                m_sSQL.Append(" SELECT 'Y' AS 'Marcar', U_Codigo AS 'Codigo', Name AS 'Descripcion', U_Agrupar AS Agrupar FROM [@EXX_MTALLAS] T0 ");
                m_sSQL.AppendFormat(" WHERE U_Agrupar = '{0}' ", Agrupar.ToString());
                m_sSQL.Append("UNION ALL ");
                m_sSQL.Append(" SELECT 'N' AS 'Marcar', U_Codigo AS 'Codigo', Name AS 'Descripcion', U_Agrupar AS Agrupar FROM [@EXX_MTALLAS] T0 ");
                m_sSQL.AppendFormat(" WHERE U_Agrupar <> '{0}' ", Agrupar.ToString());
            }

            return m_sSQL.ToString();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadPropiedades(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T1.\"ItmsGrpNam\" AS \"Nombre de la propiedad\", \"U_EXX_Defaul\" AS \"Valor\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODPROPIE\" T0 INNER JOIN " + Program.SQLBaseDatos + ".\"OITG\" T1 ON T0.\"Name\"=T1.\"ItmsTypCod\" ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT T1.ItmsGrpNam AS 'Nombre de la propiedad', U_EXX_Defaul AS 'Valor' ");
                m_sSQL.Append("FROM [@EXX_MODPROPIE] T0 INNER JOIN OITG T1 ON T0.Name=T1.ItmsTypCod ");
                m_sSQL.AppendFormat(" WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadPropiedadesNuevo(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T1.\"ItmsGrpNam\" AS \"Nombre de la propiedad\", \"U_EXX_Defaul\" AS \"Valor\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODPROPIE\" T0 INNER JOIN " + Program.SQLBaseDatos + ".\"OITG\" T1 ON T0.\"Name\"=T1.\"ItmsTypCod\" ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT T1.ItmsGrpNam AS 'Nombre de la propiedad', U_EXX_Defaul AS 'Valor' ");
                m_sSQL.Append("FROM [@EXX_MODPROPIE] T0 INNER JOIN OITG T1 ON T0.Name=T1.ItmsTypCod ");
                m_sSQL.AppendFormat(" WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadColoresData(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"Name\",\"U_EXX_CodCol\", \"U_EXX_CodMod\" ,\"U_EXX_Defaul\", \"U_EXX_Color\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODCOLORES\" T0  ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT Name, U_EXX_CodCol, U_EXX_CodMod , U_EXX_Defaul, U_EXX_Color ");
                m_sSQL.Append("FROM [@EXX_MODCOLORES] T0  ");
                m_sSQL.AppendFormat(" WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadColoresDataSource(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT 'Y' AS \"Marcar\",\"U_EXX_CodCol\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_EXX_Color\" AS \"Color\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_COLDATA\" T0  ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());

                //'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Color\" AS \"Color\"
            }
            else
            {
                m_sSQL.Append("SELECT 'Y' AS 'Marcar', U_EXX_CodCol AS 'Codigo', Name AS 'Descripcion', U_EXX_Color AS 'Color' ");
                m_sSQL.Append("FROM [@EXX_COLDATA] T0  ");
                m_sSQL.AppendFormat(" WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());

                //'N' AS \"Marcar\", \"U_Codigo\" AS \"Codigo\", \"Name\" AS \"Descripcion\", \"U_Color\" AS \"Color\"
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string loadTallasData(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"Name\",\"U_EXX_CodTal\", \"U_EXX_CodMod\" ,\"U_EXX_Defaul\", \"U_EXX_Agrupa\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODTALLAS\" T0  ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT Name, U_EXX_CodTal, U_EXX_CodMod , U_EXX_Defaul, U_EXX_Agrupa ");
                m_sSQL.Append("FROM [@EXX_MODTALLAS] T0  ");
                m_sSQL.AppendFormat(" WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboFabricante()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"FirmCode\", \"FirmName\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OMRC\" ORDER BY \"FirmName\" ASC ");
            }
            else
            {
                m_sSQL.Append("SELECT FirmCode, FirmName ");
                m_sSQL.Append("FROM OMRC ORDER BY FirmName ASC ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboEscenario()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT 0 AS \"AbsEntry\", '' AS \"MsnCode\" FROM " + Program.SQLBaseDatos + ".\"OMSN\" UNION SELECT \"AbsEntry\",\"MsnCode\" FROM " + Program.SQLBaseDatos + ".\"OMSN\" ");
            }
            else
            {
                m_sSQL.Append("SELECT '' AS AbsEntry, '' AS MsnCode FROM OMSN UNION SELECT AbsEntry,MsnCode FROM OMSN ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public static string traerCampoUsuario()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT CONCAT ('U_',\"AliasID\") AS \"Campo\", (SELECT COUNT (\"AliasID\") FROM " + Program.SQLBaseDatos + ".\"CUFD\" T0 WHERE \"TableID\" = 'OITM' AND \"AliasID\" NOT IN ('EXX_Agrupar', 'EXX_CodMod','EXX_Colecc','EXX_Color','EXX_Talla') ) AS \"Cantidad\" ");
                //m_sSQL.Append("SELECT CONCAT ('U_',\"AliasID\") AS \"Campo\", \"Descr\", \"TableID\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"CUFD\" T0  ");
                m_sSQL.AppendFormat("WHERE \"TableID\" = 'OITM' ");
                m_sSQL.AppendFormat(" AND \"AliasID\" NOT IN ('EXX_Agrupar', 'EXX_CodMod','EXX_Colecc','EXX_Color','EXX_Talla') ");
            }
            else
            {
                m_sSQL.Append("SELECT CONCAT ('U_', AliasID) AS Campo, (SELECT COUNT(AliasID) FROM CUFD T0 WHERE TableID = 'OITM' AND AliasID NOT IN ('EXX_Agrupar', 'EXX_CodMod','EXX_Colecc','EXX_Color','EXX_Talla') ) AS Cantidad ");
                //m_sSQL.Append("SELECT CONCAT ('U_', AliasID) AS Campo, Descr, TableID ");
                m_sSQL.Append("FROM CUFD T0  ");
                m_sSQL.AppendFormat("WHERE TableID = 'OITM' ");
                m_sSQL.AppendFormat(" AND AliasID NOT IN ('EXX_Agrupar', 'EXX_CodMod','EXX_Colecc','EXX_Color','EXX_Talla') ");

            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public static string traerCampoUsuarioValor(string Columnas, string Item)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT " + Columnas);

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.AppendFormat(" FROM " + Program.SQLBaseDatos + ".\"OITM\" T0 ");
                m_sSQL.AppendFormat(" WHERE \"ItemCode\" = '{0}' ", Item.ToString());
            }
            else
            {
                m_sSQL.AppendFormat(" FROM OITM T0 ");
                m_sSQL.AppendFormat(" WHERE ItemCode = '{0}' ", Item.ToString());
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public static string traerCampoUsuarioArticulo(string Item)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"ItemCode\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OITM\" T0  ");
                m_sSQL.AppendFormat("WHERE \"ItemCode\" <> '{0}' ", Item.ToString());
                m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Item.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT ItemCode ");
                m_sSQL.Append("FROM OITM T0  ");
                m_sSQL.AppendFormat("WHERE ItemCode <> '{0}' ", Item.ToString());
                m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Item.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public static string traerPrecioLista(string Item, string Lista)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"Price\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ITM1\" T0  ");
                m_sSQL.AppendFormat("WHERE \"ItemCode\" = '{0}' ", Item.ToString());
                m_sSQL.AppendFormat("AND \"PriceList\" = '{0}' ", Lista.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT Price ");
                m_sSQL.Append("FROM ITM1 T0  ");
                m_sSQL.AppendFormat("WHERE ItemCode = '{0}' ", Item.ToString());
                m_sSQL.AppendFormat("AND PriceList = '{0}' ", Lista.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboSerie()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT 0 AS \"Series\", '' AS \"SeriesName\" FROM " + Program.SQLBaseDatos + ".\"NNM1\" UNION SELECT \"Series\",\"SeriesName\" FROM " + Program.SQLBaseDatos + ".\"NNM1\" WHERE \"ObjectCode\" = 202 ");
            }
            else
            {
                m_sSQL.Append("SELECT '' AS Series, '' AS SeriesName FROM NNM1 UNION SELECT Series, SeriesName FROM NNM1 WHERE ObjectCode = 202 ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerBodegas(string Articulo, string Bodeda)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"ItemCode\", \"WhsCode\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OITW\" ");
                m_sSQL.AppendFormat("WHERE \"ItemCode\" = '{0}' ", Articulo.ToString());
                m_sSQL.AppendFormat("AND \"WhsCode\" = '{0}' ", Bodeda.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT ItemCode, WhsCode ");
                m_sSQL.Append("FROM OITW ");
                m_sSQL.AppendFormat("WHERE ItemCode = '{0}' ", Articulo.ToString());
                m_sSQL.AppendFormat("AND WhsCode = '{0}' ", Bodeda.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerDatos(string Tipo, string Articulo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo == "R")
                {
                    m_sSQL.Append("SELECT \"ResName\", \"IssueMthd\", \"UnitOfMsr\", \"DfltWH\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"ORSC\" ");
                    m_sSQL.AppendFormat("WHERE \"ResCode\" = '{0}' ", Articulo.ToString());
                }
                else
                {
                    m_sSQL.Append("SELECT \"ItemName\", \"IssueMthd\", \"InvntryUom\", \"DfltWH\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OITM\" ");
                    m_sSQL.AppendFormat("WHERE \"ItemCode\" = '{0}' ", Articulo.ToString());
                }
            }
            else
            {
                if (Tipo == "R")
                {
                    m_sSQL.Append("SELECT ResName, IssueMthd, UnitOfMsr, DfltWH ");
                    m_sSQL.Append("FROM ORSC ");
                    m_sSQL.AppendFormat("WHERE ResCode = '{0}' ", Articulo.ToString());
                }
                else
                {
                    m_sSQL.Append("SELECT ItemName, IssueMthd, InvntryUom, DfltWH ");
                    m_sSQL.Append("FROM OITM ");
                    m_sSQL.AppendFormat("WHERE ItemCode = '{0}' ", Articulo.ToString());
                }
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerTallaColorListaExiste(string Modelo, string Tipo, string Articulo, string nombre, string linea)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo != "")
                {
                    m_sSQL.Append("SELECT \"U_EXX_CodTal\", \"Name\", \"U_EXX_Defaul\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_LISTALLAS\" ");
                }
                else
                {
                    m_sSQL.Append("SELECT \"U_EXX_CodCol\", \"Name\", \"U_EXX_Defaul\", \"U_EXX_Color\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_LISCOLORE\" ");
                }

                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND \"U_EXX_CodArt\" = '{0}' ", Articulo.ToString());
                if (Tipo != "")
                {
                    m_sSQL.AppendFormat("AND \"U_EXX_CodTal\" = '{0}' ", nombre.ToString());
                    m_sSQL.AppendFormat("AND \"U_EXX_LinNum\" = '{0}' ", linea.ToString());
                }
                else
                {
                    m_sSQL.AppendFormat("AND \"U_EXX_CodCol\" = '{0}' ", nombre.ToString());
                    m_sSQL.AppendFormat("AND \"U_EXX_LinNum\" = '{0}' ", linea.ToString());
                }
            }
            else
            {
                if (Tipo != "")
                {
                    m_sSQL.Append("SELECT U_EXX_CodTal, Name, U_EXX_Defaul ");
                    m_sSQL.Append("FROM [@EXX_LISTALLAS] ");
                }
                else
                {
                    m_sSQL.Append("SELECT U_EXX_CodCol, Name, U_EXX_Defaul, U_EXX_Color ");
                    m_sSQL.Append("FROM [@EXX_LISCOLORE] ");
                }

                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat("AND U_EXX_CodArt = '{0}' ", Articulo.ToString());
                if (Tipo != "")
                {
                    m_sSQL.AppendFormat("AND U_EXX_CodTal = '{0}' ", nombre.ToString());
                    m_sSQL.AppendFormat("AND U_EXX_LinNum = '{0}' ", linea.ToString());
                }
                else
                {
                    m_sSQL.AppendFormat("AND U_EXX_CodCol = '{0}' ", nombre.ToString());
                    m_sSQL.AppendFormat("AND U_EXX_LinNum = '{0}' ", linea.ToString());
                }
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerTallaColorLista(string Modelo, string Tipo, string Articulo, string linea)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo != "")
                {
                    m_sSQL.Append("SELECT \"U_EXX_CodTal\", \"Name\", IFNULL((SELECT \"U_EXX_Defaul\" FROM " + Program.SQLBaseDatos + ".\"@EXX_LISTALLAS\" WHERE \"U_EXX_CodTal\" = T0.\"U_EXX_CodTal\" ");
                    m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                    m_sSQL.AppendFormat("AND \"U_EXX_CodArt\" = '{0}' ", Articulo.ToString());
                    m_sSQL.AppendFormat("AND \"U_EXX_LinNum\" = '{0}' ", linea.ToString());
                    m_sSQL.Append(" ), 'N') AS \"Activo\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODTALLAS\" T0 ");
                }
                else
                {
                    m_sSQL.Append("SELECT \"U_EXX_CodCol\", \"Name\", \"U_EXX_Color\", IFNULL((SELECT \"U_EXX_Defaul\" FROM " + Program.SQLBaseDatos + ".\"@EXX_LISCOLORE\" WHERE \"U_EXX_CodCol\" = T0.\"U_EXX_CodCol\" ");
                    m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                    m_sSQL.AppendFormat("AND \"U_EXX_CodArt\" = '{0}' ", Articulo.ToString());
                    m_sSQL.AppendFormat("AND \"U_EXX_LinNum\" = '{0}' ", linea.ToString());
                    m_sSQL.Append(" ), 'N') AS \"Activo\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODCOLORES\" T0 ");
                }
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                if (Tipo != "")
                {
                    m_sSQL.Append("SELECT U_EXX_CodTal, Name, isnull((Select U_EXX_Defaul from [@EXX_LISTALLAS] where U_EXX_CodTal = T0.U_EXX_CodTal ");
                    m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                    m_sSQL.AppendFormat("AND U_EXX_CodArt = '{0}' ", Articulo.ToString());
                    m_sSQL.AppendFormat("AND U_EXX_LinNum = '{0}' ", linea.ToString());
                    m_sSQL.Append(" ), 'N') AS 'Activo' ");
                    m_sSQL.Append("FROM [@EXX_MODTALLAS] T0 ");
                }
                else
                {
                    m_sSQL.Append("SELECT U_EXX_CodCol, Name, U_EXX_Color, isnull((Select U_EXX_Defaul from [@EXX_LISCOLORE] where U_EXX_CodCol = T0.U_EXX_CodCol ");
                    m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                    m_sSQL.AppendFormat("AND U_EXX_CodArt = '{0}' ", Articulo.ToString());
                    m_sSQL.AppendFormat("AND U_EXX_LinNum = '{0}' ", linea.ToString());
                    m_sSQL.Append(" ), 'N') AS 'Activo' ");
                    m_sSQL.Append("FROM [@EXX_MODCOLORES] T0 ");
                }
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerTallaColorListaOrden(string Modelo, string Tipo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (Tipo != "")
                {
                    m_sSQL.Append("SELECT \"U_EXX_CodTal\", \"Name\", IFNULL((SELECT \"U_EXX_Defaul\" FROM " + Program.SQLBaseDatos + ".\"@EXX_LISTALLAS\" WHERE \"U_EXX_CodTal\" = T0.\"U_EXX_CodTal\" ");
                    m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                    m_sSQL.Append(" ), 'N') AS \"Activo\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODTALLAS\" T0 ");
                }
                else
                {
                    m_sSQL.Append("SELECT \"U_EXX_CodCol\", \"Name\", \"U_EXX_Color\", IFNULL((SELECT DISTINCT(\"U_EXX_CodCol\") \"U_EXX_Defaul\" FROM " + Program.SQLBaseDatos + ".\"@EXX_LISCOLORE\" WHERE \"U_EXX_CodCol\" = T0.\"U_EXX_CodCol\" ");
                    m_sSQL.AppendFormat("AND \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                    m_sSQL.Append(" ), 'N') AS \"Activo\" ");
                    m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODCOLORES\" T0 ");
                }
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                if (Tipo != "")
                {
                    m_sSQL.Append("SELECT U_EXX_CodTal, Name, isnull((Select U_EXX_Defaul from [@EXX_LISTALLAS] where U_EXX_CodTal = T0.U_EXX_CodTal ");
                    m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                    m_sSQL.Append(" ), 'N') AS 'Activo' ");
                    m_sSQL.Append("FROM [@EXX_MODTALLAS] T0 ");
                }
                else
                {
                    m_sSQL.Append("SELECT U_EXX_CodCol, Name, U_EXX_Color, isnull((Select DISTINCT(U_EXX_CodCol) U_EXX_Defaul from [@EXX_LISCOLORE] where U_EXX_CodCol = T0.U_EXX_CodCol ");
                    m_sSQL.AppendFormat("AND U_EXX_CodMod = '{0}' ", Modelo.ToString());
                    m_sSQL.Append(" ), 'N') AS 'Activo' ");
                    m_sSQL.Append("FROM [@EXX_MODCOLORES] T0 ");
                }
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerDatosModelo(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_EXX_ParSep\", \"U_EXX_TalCol\", \"U_EXX_ColTal\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MMODELOS\" T0 ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT U_EXX_ParSep, U_EXX_TalCol, U_EXX_ColTal ");
                m_sSQL.Append("FROM [@EXX_MMODELOS] T0 ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerTallaOrdenFabricacion(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_EXX_CodTal\", \"Name\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODTALLAS\" ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT U_EXX_CodTal, Name ");
                m_sSQL.Append("FROM [@EXX_MODTALLAS] ");
                m_sSQL.AppendFormat(" WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerOrdenFabricacion(string Modelo, string Separador, string ColTal = null, string TalCol = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T2.\"Code\", T2.\"Type\", SUM((T2.\"Quantity\"/T1.\"Qauntity\") * T0.\"U_EXX_Cant\") AS \"Cantidad\", T2.\"ChildNum\", \"U_Recurso\", T2.\"Warehouse\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_OFCOLORES\" T0 ");
                if (ColTal == "Y")
                    m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"OITT\" T1 ON '" + Modelo + Separador + "' || T0.\"U_EXX_CodCol\" || '" + Separador + "' || T0.\"U_EXX_CodTal\" = T1.\"Code\" ");
                else
                    m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"OITT\" T1 ON '" + Modelo + Separador + "' || T0.\"U_EXX_CodTal\" || '" + Separador + "' || T0.\"U_EXX_CodCol\" = T1.\"Code\" ");
                m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"ITT1\" T2 ON T1.\"Code\"=T2.\"Father\" ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.Append("GROUP BY T2.\"Code\", T2.\"Type\", T2.\"ChildNum\", \"U_Recurso\", T2.\"Warehouse\" ");
                m_sSQL.Append("ORDER BY T2.\"ChildNum\" ");
            }
            else
            {
                m_sSQL.Append("SELECT T2.Code, T2.Type, SUM((T2.Quantity/T1.Qauntity) * T0.U_EXX_Cant) AS Cantidad, T2.ChildNum, U_Recurso, T2.Warehouse ");
                m_sSQL.Append("FROM [@EXX_OFCOLORES] T0 ");
                if (ColTal == "Y")
                    m_sSQL.Append("INNER JOIN OITT T1 ON '" + Modelo + Separador + "' + T0.U_EXX_CodCol + '" + Separador + "' + T0.U_EXX_CodTal = T1.Code ");
                else
                    m_sSQL.Append("INNER JOIN OITT T1 ON '" + Modelo + Separador + "' + T0.U_EXX_CodTal + '" + Separador + "' + T0.U_EXX_CodCol = T1.Code ");
                m_sSQL.Append("INNER JOIN ITT1 T2 ON T1.Code=T2.Father ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.Append("GROUP BY T2.Code, T2.Type, T2.ChildNum, U_Recurso, T2.Warehouse ");
                m_sSQL.Append("ORDER BY T2.ChildNum ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerSeparador(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_EXX_ParSep\", \"U_EXX_ColTal\", \"U_EXX_TalCol\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MMODELOS\" T0 ");
                m_sSQL.AppendFormat(" WHERE \"Code\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT U_EXX_ParSep, U_EXX_ColTal, U_EXX_TalCol ");
                m_sSQL.Append("FROM [@EXX_MMODELOS] T0 ");
                m_sSQL.AppendFormat("WHERE Code = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string recursoMaquinaTop(string Articulo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT TOP 1 \"U_Maquina\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MAQ\" ");
                m_sSQL.AppendFormat("WHERE \"U_Recurso\" = '{0}' ", Articulo.ToString());
                m_sSQL.Append("ORDER BY \"Code\" LIMIT 1 ");
            }
            else
            {
                m_sSQL.Append("SELECT TOP 1 U_Maquina ");
                m_sSQL.Append("FROM [@EXX_MAQ] ");
                m_sSQL.AppendFormat("WHERE U_Recurso = '{0}' ", Articulo.ToString());
                m_sSQL.Append("ORDER BY Code ");
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string recursoPersonaTop(string Articulo)
        {
            m_sSQL.Length = 0;
            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_Emple\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_PER\" T0 INNER JOIN " + Program.SQLBaseDatos + ".\"OHEM\" T1 ON T0.\"U_Emple\"=T1.\"empID\" ");
                m_sSQL.AppendFormat("WHERE \"U_Recurso\" = '{0}' ", Articulo.ToString());
                m_sSQL.Append("ORDER BY T0.\"Code\" LIMIT 1 ");
            }
            else
            {
                m_sSQL.Append("SELECT TOP 1 U_Emple ");
                m_sSQL.Append("FROM [@EXX_PER] T0 INNER JOIN OHEM T1 ON T0.U_Emple=T1.empID ");
                m_sSQL.AppendFormat("WHERE U_Recurso = '{0}' ", Articulo.ToString());
                m_sSQL.Append("ORDER BY T0.Code ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerSubProducto(string Modelo, string Separador, string ColTal = null, string TalCol = null)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                if (ColTal == "Y")
                    m_sSQL.Append("SELECT '" + Modelo + Separador + "' || '' || T0.\"U_EXX_CodCol\" || '" + Separador + "' || T0.\"U_EXX_CodTal\" AS \"Codigo\", \"U_EXX_Cant\" ");
                else
                    m_sSQL.Append("SELECT '" + Modelo + Separador + "' || '' || T0.\"U_EXX_CodTal\" || '" + Separador + "' || T0.\"U_EXX_CodCol\" AS \"Codigo\", \"U_EXX_Cant\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_OFCOLORES\" T0 ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                if (ColTal == "Y")
                    m_sSQL.Append("SELECT '" + Modelo + Separador + "' + '' + T0.U_EXX_CodCol + '" + Separador + "' + T0.U_EXX_CodTal AS Codigo, U_EXX_Cant ");
                else
                    m_sSQL.Append("SELECT '" + Modelo + Separador + "' + '' + T0.U_EXX_CodTal + '" + Separador + "' + T0.U_EXX_CodCol AS Codigo, U_EXX_Cant ");
                m_sSQL.Append("FROM [@EXX_OFCOLORES] T0 ");
                m_sSQL.AppendFormat(" WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerCantidadOrdenFabricacion(string Modelo, string Talla, string Color)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_EXX_Cant\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_OFCOLORES\" ");
                m_sSQL.AppendFormat(" WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat(" AND \"U_EXX_CodTal\" = '{0}' ", Talla.ToString());
                m_sSQL.AppendFormat(" AND \"U_EXX_CodCol\" = '{0}' ", Color.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT U_EXX_Cant ");
                m_sSQL.Append("FROM [@EXX_OFCOLORES] ");
                m_sSQL.AppendFormat(" WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
                m_sSQL.AppendFormat(" AND U_EXX_CodTal = '{0}' ", Talla.ToString());
                m_sSQL.AppendFormat(" AND U_EXX_CodCol = '{0}' ", Color.ToString());
            }

            return m_sSQL.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string verStock(string Modelo, string Talla, string Color, string Bodega, string Parametro, string talCol, string colTal)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"OnHand\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OITW\" ");
                m_sSQL.AppendFormat(" WHERE \"WhsCode\" = '{0}' ", Bodega.ToString());
                if (colTal == "Y")
                    m_sSQL.AppendFormat(" AND \"ItemCode\" = '{0}' ", Modelo.ToString() + Parametro.ToString() + Color.ToString() + Parametro.ToString() + Talla.ToString());
                else
                    m_sSQL.AppendFormat(" AND \"ItemCode\" = '{0}' ", Modelo.ToString() + Parametro.ToString() + Talla.ToString() + Parametro.ToString() + Color.ToString());

            }
            else
            {
                m_sSQL.Append("SELECT OnHand ");
                m_sSQL.Append("FROM OITW ");
                m_sSQL.AppendFormat(" WHERE WhsCode = '{0}' ", Bodega.ToString());
                if (colTal == "Y")
                    m_sSQL.AppendFormat(" AND ItemCode = '{0}' ", Modelo.ToString() + Parametro.ToString() + Color.ToString() + Parametro.ToString() + Talla.ToString());
                else
                    m_sSQL.AppendFormat(" AND ItemCode = '{0}' ", Modelo.ToString() + Parametro.ToString() + Talla.ToString() + Parametro.ToString() + Color.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerImagen(string Code)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_PicturName1\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MMODELOS\" ");
                m_sSQL.AppendFormat("WHERE \"Code\" = '{0}' ", Code.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT U_PicturName1 ");
                m_sSQL.Append("FROM [@EXX_MMODELOS] ");
                m_sSQL.AppendFormat("WHERE Code = '{0}' ", Code.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cuentaMayor()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"GLMethod\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OADM\" ");
            }
            else
            {
                m_sSQL.Append("SELECT GLMethod ");
                m_sSQL.Append("FROM OADM ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string metodoValoracion()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"InvntSystm\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OADM\" ");
            }
            else
            {
                m_sSQL.Append("SELECT InvntSystm ");
                m_sSQL.Append("FROM OADM ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string traerListaMateriales(string Modelo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"U_EXX_Tipo\", \"U_EXX_Numero\", \"U_EXX_NumDes\", \"U_EXX_Cant\", \"U_EXX_UniMed\", \"U_EXX_CodAlm\", \"U_EXX_MetEmi\", \"U_EXX_Recurso\", \"U_EXX_CanOrd\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MODLISTMAT\" ");
                m_sSQL.AppendFormat("WHERE \"U_EXX_CodMod\" = '{0}' ", Modelo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT U_EXX_Tipo, U_EXX_Numero, U_EXX_NumDes, U_EXX_Cant, U_EXX_UniMed, U_EXX_CodAlm, U_EXX_MetEmi, U_EXX_Recurso, U_EXX_CanOrd ");
                m_sSQL.Append("FROM [@EXX_MODLISTMAT] ");
                m_sSQL.AppendFormat("WHERE U_EXX_CodMod = '{0}' ", Modelo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboIntervaloPedido()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"Code\", \"Name\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OCYC\" ");
            }
            else
            {
                m_sSQL.Append("SELECT Code, Name ");
                m_sSQL.Append("FROM OCYC ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboGrupoArticulo()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"ItmsGrpCod\", \"ItmsGrpNam\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OITB\" ");
            }
            else
            {
                m_sSQL.Append("SELECT ItmsGrpCod, ItmsGrpNam ");
                m_sSQL.Append("FROM OITB ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboGrupoAgrupar()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT DISTINCT \"U_Agrupar\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"@EXX_MTALLAS\" ");
            }
            else
            {
                m_sSQL.Append("SELECT DISTINCT U_Agrupar ");
                m_sSQL.Append("FROM [@EXX_MTALLAS] ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboListaPrecio()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"ListNum\", \"ListName\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OPLN\" ");
            }
            else
            {
                m_sSQL.Append("SELECT ListNum, ListName ");
                m_sSQL.Append("FROM OPLN ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarBodegas(string Articulo)
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT T0.\"WhsCode\", \"WhsName\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OWHS\" T0 ");//
                m_sSQL.Append("INNER JOIN " + Program.SQLBaseDatos + ".\"OITW\" T1  ON T0.\"WhsCode\"=T1.\"WhsCode\" ");
                m_sSQL.AppendFormat("WHERE \"ItemCode\" = '{0}' ", Articulo.ToString());
            }
            else
            {
                m_sSQL.Append("SELECT T0.WhsCode, WhsName ");
                m_sSQL.Append("FROM OWHS T0 ");
                m_sSQL.Append("INNER JOIN OITW T1  ON T0.WhsCode=T1.WhsCode ");
                m_sSQL.AppendFormat("WHERE ItemCode = '{0}' ", Articulo.ToString());
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboExpedicion()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"TrnspCode\", \"TrnspName\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OSHP\" ");
            }
            else
            {
                m_sSQL.Append("SELECT TrnspCode, TrnspName ");
                m_sSQL.Append("FROM OSHP ");
            }

            return m_sSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string cargarComboAduana()
        {
            m_sSQL.Length = 0;

            if (Program.Motor == "dst_HANADB")
            {
                m_sSQL.Append("SELECT \"CstGrpCode\", \"CstGrpName\" ");
                m_sSQL.Append("FROM " + Program.SQLBaseDatos + ".\"OARG\" ");
            }
            else
            {
                m_sSQL.Append("SELECT CstGrpCode, CstGrpName ");
                m_sSQL.Append("FROM OARG ");
            }

            return m_sSQL.ToString();
        }

        #endregion Metodos
    }
}
