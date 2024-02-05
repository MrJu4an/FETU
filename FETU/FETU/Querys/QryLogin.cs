using Ctec;
using System.Data;
using static Ctec.Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FETU.Querys
{
    public class QryLogin
    {
        public DataRow SelectGEUSUSIS(string USCODUSU)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT USCODUSU, USNOM, USCLA, USEST, USFECSEG, USMODFEC FROM GEUSUSIS WHERE USCODUSU = '{ USCODUSU }'";
            return dbs.OpenRow(QRY);
        }
        public DataRow SelectFEUSUSIS(string USCODUSU)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT USCODUSU, USNOMBRE, USPASSWD, USESTADO, USTOKEN, USTIPO, USEMAIL, USCODEMP, USNITTT FROM FEUSUSIS WHERE USCODUSU = '{ USCODUSU }'";
            return dbs.OpenRow(QRY);
        }
        public DataRow ValidarModUsu(string USCODUSU, string UMCODMOD)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT UMCODUSU, UMCODMOD, UMCODROL FROM SEUSMOROL WHERE UMCODMOD = '{ UMCODMOD }' AND UMCODUSU = '{ USCODUSU }'";
            return dbs.OpenRow(QRY);
        }
        public string ValidarModulo(string Usuario)
        {
            return "GTW";
        }
        public DataTable SelectMenus(string rol)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT PMCODMOD, PMDES, PMORDEN FROM SEPAMEWEB " +
                    "JOIN SEOBJMOD ON PMCODMOD = OMCODMOD " +
                    "JOIN SEESTOBJ ON OMCODMOD = EOCODMOD " +
                  "WHERE PMDES = OMNOMFRM AND OMNOMFRM = EONOMFRM " +
                    "AND OMNOMOBJ = EONOMOBJ " +
                    "AND OMNOMFRM <> OMDESOBJ " +
                    "AND PMEST ='A' " +
                    "AND EOCODROL = '" + rol + "' " +
                    "AND EOCODMOD = 'FTU' " +
                    "AND EOEST='Prendido' " +
                  "GROUP BY PMCODMOD, PMDES, PMORDEN ORDER BY PMORDEN ASC";
            return dbs.OpenData(QRY);
        }
        public bool VerificaHijos(string rol, string padre)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT OMDESOBJ, OMNOMOBJ, OMORDEN FROM SEPAMEWEB " +
                    "JOIN SEOBJMOD ON PMCODMOD = OMCODMOD " +
                    "JOIN SEESTOBJ ON EOCODMOD = OMCODMOD " +
                  "WHERE PMDES = OMNOMFRM AND EONOMFRM = OMNOMFRM " +
                    "AND EONOMOBJ = OMNOMOBJ " +
                    "AND EOCODROL = '" + rol + "' " +
                    "AND EOCODMOD = 'FTU' " +
                    "AND EONOMFRM = '" + padre + "' " +
                    "AND EOEST='Prendido' " +
                  "ORDER BY OMORDEN ASC";
            DataTable table = dbs.OpenData(QRY);
            if (table != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectHijos(string rol, string padre)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT OMDESOBJ, OMNOMOBJ, OMORDEN FROM SEPAMEWEB " +
                    "JOIN SEOBJMOD ON PMCODMOD = OMCODMOD " +
                    "JOIN SEESTOBJ ON EOCODMOD = OMCODMOD " +
                  "WHERE PMDES = OMNOMFRM AND EONOMFRM = OMNOMFRM " +
                    "AND EONOMOBJ = OMNOMOBJ " +
                    "AND EOCODROL = '" + rol + "' " +
                    "AND EOCODMOD = 'FTU' " +
                    "AND EONOMFRM = '" + padre + "' " +
                    "AND EOEST='Prendido' " +
                  "ORDER BY OMORDEN ASC";
            return dbs.OpenData(QRY);
        }
        public int updateUsuario(string USCODUSU, string USNOMBRE, string USEMAIL, string USPASSWD, string USTOKEN)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"UPDATE FEUSUSIS SET USNOMBRE = '{ USNOMBRE }', USEMAIL = '{ USEMAIL }', USPASSWD = '{ USPASSWD }', USTOKEN = '{ USTOKEN }' WHERE USCODUSU= '{ USCODUSU }'";
            return dbs.Execute(QRY);
        }
        public int updateUsuarioContra(string USCODUSU, string nuevaContra, string nuevoToken)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"UPDATE FEUSUSIS SET USPASSWD = '{ nuevaContra }', USTOKEN = '{ nuevoToken }' WHERE USCODUSU= '{ USCODUSU }'";
            return dbs.Execute(QRY);
        }
        public int insertGelogAud(string Usuario, string Modulo, string Comentario)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"INSERT INTO GELOGAUD (LOFECACT, LOFECNUE, LOFUNCIO, LOFECHA, LOVALINI, LOVALFIN, LOMODULO, LOOBSER) " +
                    "VALUES (SYSDATE, SYSDATE, '" + Usuario + "', SYSDATE, '', '', '" + Modulo + "', '" + Comentario + "')";
            return dbs.Execute(QRY);
        }
    }
}