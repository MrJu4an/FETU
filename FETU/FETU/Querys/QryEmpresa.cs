using Ctec;
using System.Data;
using static Ctec.Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FETU.Querys
{
    public class QryEmpresa
    {
        public DataTable SelectEmpresas(string EMNITTT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT * FROM FEEMPRESAS WHERE EMNITTT = '{ EMNITTT }' ORDER BY EMRAZONSOCIAL ASC";
            return dbs.OpenData(QRY);
        }
        public DataRow SelectEmpresaByID(string EMCODIGO, string EMNITTT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT * FROM FEEMPRESAS WHERE EMCODIGO = '{ EMCODIGO }' AND EMNITTT = '{ EMNITTT }'";
            return dbs.OpenRow(QRY);
        }
        public DataRow SelectEmpresaByNIT(string EMNITFAC, string EMNITTT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT * FROM FEEMPRESAS WHERE EMNITFAC = '{ EMNITFAC }' AND EMNITTT = '{ EMNITTT }'";
            return dbs.OpenRow(QRY);
        }
        public int InsertEmpresa(string EMCODIGO, string EMNITTT, string EMRAZONSOCIAL,
            string EMNITFAC, string EMTIPDOC, string EMEMAIL,
            string EMRESRUT, string EMCODDEPTO, string EMCODCIUDAD,
            string EMDIRECCION, string EMESTFAC, string EMENVIOCORREO)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "INSERT INTO FEEMPRESAS (EMCODIGO, EMNITTT, EMRAZONSOCIAL, EMNITFAC, EMTIPDOC, " +
                    "EMEMAIL, EMRESRUT, EMCODDEPTO, EMCODCIUDAD, EMDIRECCION, EMESTFAC, EMENVIOCORREO) " +
                  $"VALUES ('{ EMCODIGO }', '{ EMNITTT }', '{ EMRAZONSOCIAL }', '{ EMNITFAC }', " +
                    $"'{ EMTIPDOC }', '{ EMEMAIL }', '{ EMRESRUT }', '{ EMCODDEPTO }', '{ EMCODCIUDAD }', " +
                    $"'{ EMDIRECCION }', '{ EMESTFAC }', '{ EMENVIOCORREO }')";
            return dbs.Execute(QRY);
        }
        public int UpdateEmpresa(string EMCODIGO, string EMNITTT, string EMRAZONSOCIAL,
            string EMNITFAC, string EMTIPDOC, string EMEMAIL,
            string EMRESRUT, string EMCODDEPTO, string EMCODCIUDAD,
            string EMDIRECCION, string EMESTFAC, string EMENVIOCORREO)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"UPDATE FEEMPRESAS SET EMRAZONSOCIAL = '{ EMRAZONSOCIAL }', " +
                    $"EMNITFAC = '{ EMNITFAC }', " +
                    $"EMTIPDOC = '{ EMTIPDOC }', " +
                    $"EMEMAIL = '{ EMEMAIL }', " +
                    $"EMRESRUT = '{ EMRESRUT }', " +
                    $"EMCODDEPTO = '{ EMCODDEPTO }', " +
                    $"EMCODCIUDAD = '{ EMCODCIUDAD }', " +
                    $"EMDIRECCION = '{ EMDIRECCION }', " +
                    $"EMESTFAC = '{ EMESTFAC }', " +
                    $"EMENVIOCORREO = '{ EMENVIOCORREO }' " +
                  $"WHERE EMCODIGO = '{ EMCODIGO }' AND EMNITTT = '{ EMNITTT }'";
            return dbs.Execute(QRY);
        }
        public DataTable SelectEmpresasDet(string EDCODIGO, string EDNITTT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT EDCODTERMINAL || ' - ' || DSDES AS EDSEDE, EDFPAGO " +
                  "FROM FEEMPRESASDET " +
                    "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                    "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = EDCODTERMINAL " +
                  $"WHERE EDCODIGO = '{ EDCODIGO }' AND EDNITTT = '{ EDNITTT }'";
            return dbs.OpenData(QRY);
        }
        public int InsertEmpresasDet(string EDCODIGO, string EDNITTT, string EDCODTERMINAL,
            string EDFPAGO)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "INSERT INTO FEEMPRESASDET " +
                    $"VALUES ('{ EDCODIGO }', '{ EDNITTT }', '{ EDCODTERMINAL }', '{ EDFPAGO }')";
            return dbs.Execute(QRY);
        }
        public int DeleteEmpresasDet(string EDCODIGO, string EDNITTT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"DELETE FROM FEEMPRESASDET WHERE EDCODIGO = '{ EDCODIGO }' AND EDNITTT = '{ EDNITTT }'";
            return dbs.Execute(QRY);
        }
    }
}