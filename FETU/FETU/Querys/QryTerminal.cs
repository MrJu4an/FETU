using Ctec;
using System.Data;
using static Ctec.Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FETU.Querys
{
    public class QryTerminal
    {
        public DataRow SelectDatosTerminal(string DTNIT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT * FROM FEDATOSTERMINAL WHERE DTNIT = '{ DTNIT }'";
            return dbs.OpenRow(QRY);
        }
        public DataRow SelectCertificadoTerminal(string FENIT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT * FROM FEDATOSCERT WHERE FENIT = '{ FENIT }'";
            return dbs.OpenRow(QRY);
        }
        public DataRow SelectMailDataTerminal(string DTNIT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT DTEMAILSEND, DTPASSEMAIL, DTEMAILFAC FROM FEDATOSTERMINAL WHERE DTNIT = '{ DTNIT }'";
            return dbs.OpenRow(QRY);
        }
        public DataTable SelectSedesTerminal(string STNIT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT DSDES AS SEDETER FROM FESEDESTERMINAL " +
                    "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                    "INNER JOIN GEDETSUPTIP ON STCODTIP = DSCODTIP AND DSCODDET = STCODTERMINAL " +
                  $"WHERE STNIT = '{ STNIT }'";
            return dbs.OpenData(QRY);
        }
        public int InsertDatosTerminal(string DTNIT, string DTRAZONSOCIAL, string DTDIRECCION,
            string DTTELEFONO, string DTCODDEPTO, string DTCODCIUDAD,
            string DTEMAIL, string DTEMAILFAC, string DTSOFTWAREID,
            string DTTESTSETID, string DTPIN, string DTLLAVE,
            string DTCIIU, string DTCANTUDIA, string DTRESFIS,
            string DTLOGO, string DTRUTACER, string DTPASSCER,
            string DTMSJTRIBUTARIO1, string DTMSJTRIBUTARIO2)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "INSERT INTO FEDATOSTERMINAL (DTNIT, DTRAZONSOCIAL, DTDIRECCION, DTTELEFONO, " +
                    "DTCODDEPTO, DTCODCIUDAD, DTEMAIL, DTEMAILFAC, DTSOFTWAREID, DTTESTSETID, DTPIN, " +
                    "DTLLAVE, DTCIIU, DTCANTUDIA, DTRESFIS, DTLOGO, DTRUTACER, DTPASSCER, " +
                    "DTMSJTRIBUTARIO1, DTMSJTRIBUTARIO2) " +
                  $"VALUES ('{ DTNIT }', '{ DTRAZONSOCIAL }', '{ DTDIRECCION }', " +
                    $"'{ DTTELEFONO }', '{ DTCODDEPTO }', '{ DTCODCIUDAD }', " +
                    $"'{ DTEMAIL }', '{ DTEMAILFAC }', '{ DTSOFTWAREID }', " +
                    $"'{ DTTESTSETID }', '{ DTPIN }', '{ DTLLAVE }', '{ DTCIIU }', " +
                    $"{DTCANTUDIA }, '{ DTRESFIS }'";
            if (DTLOGO != "")
            {
                QRY += $", '{ DTLOGO }'";
            }
            else
            {
                QRY += ", NULL";
            }
            if (DTRUTACER != "")
            {
                QRY += $", '{ DTRUTACER }'";
            }
            else
            {
                QRY += ", NULL";
            }
            if (DTPASSCER != "")
            {
                QRY += $", '{ DTPASSCER }'";
            }
            else
            {
                QRY += ", NULL";
            }
            QRY += $", '{ DTMSJTRIBUTARIO1 }', '{ DTMSJTRIBUTARIO2 }')";
            return dbs.Execute(QRY);
        }
        public int UpdateDatosTerminal(string DTNIT, string DTRAZONSOCIAL, string DTDIRECCION,
            string DTTELEFONO, string DTCODDEPTO, string DTCODCIUDAD,
            string DTEMAIL, string DTEMAILFAC, string DTSOFTWAREID,
            string DTTESTSETID, string DTPIN, string DTLLAVE,
            string DTCIIU, string DTCANTUDIA, string DTRESFIS,
            string DTLOGO, string DTRUTACER, string DTPASSCER,
            string DTMSJTRIBUTARIO1, string DTMSJTRIBUTARIO2)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "UPDATE FEDATOSTERMINAL " +
                  $"SET DTRAZONSOCIAL = '{ DTRAZONSOCIAL }', " +
                    $"DTDIRECCION = '{ DTDIRECCION }', " +
                    $"DTTELEFONO = '{ DTTELEFONO }', " +
                    $"DTCODDEPTO = '{ DTCODDEPTO }', " +
                    $"DTCODCIUDAD = '{ DTCODCIUDAD }', " +
                    $"DTEMAIL = '{ DTEMAIL }', " +
                    $"DTEMAILFAC = '{ DTEMAILFAC }', " +
                    $"DTSOFTWAREID = '{ DTSOFTWAREID }', " +
                    $"DTTESTSETID = '{ DTTESTSETID }', " +
                    $"DTPIN = '{ DTPIN }', " +
                    $"DTLLAVE = '{ DTLLAVE }', " +
                    $"DTCIIU = '{ DTCIIU }', " +
                    $"DTCANTUDIA = { DTCANTUDIA }, " +
                    $"DTRESFIS = '{ DTRESFIS }' ";
            if (DTLOGO != "")
            {
                QRY += $", DTLOGO = '{ DTLOGO }'";
            }
            if (DTRUTACER != "")
            {
                QRY += $", DTRUTACER = '{ DTRUTACER }'";
            }
            if (DTPASSCER != "")
            {
                QRY += $", DTPASSCER = '{ DTPASSCER }'";
            }
            QRY += $", DTMSJTRIBUTARIO1 = '{ DTMSJTRIBUTARIO1 }', " +
                   $"DTMSJTRIBUTARIO2 = '{ DTMSJTRIBUTARIO2 }' " +
                  $"WHERE DTNIT = '{ DTNIT }'";
            return dbs.Execute(QRY);
        }
        public int InsertDatosCertificado(string FENIT, string FENOMBRE, string FERUTACER,
            string FEPASSCER, string FEPIN, string FELLAVE,
            string FESOFID, string FESETPRU)
        {

            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "INSERT INTO FEDATOSCERT (FENIT, FENOMBRE, FERUTACER, FEPASSCER, FEPIN, FELLAVE, " +
                    "FESOFID, FESETPRU) " +
                  $"VALUES ('{ FENIT }', '{ FENOMBRE }', '{ FERUTACER }', '{ FEPASSCER }', " +
                    $"'{ FEPIN }', '{ FELLAVE }', '{ FESOFID }', '{ FESETPRU }'";
            return dbs.Execute(QRY);
        }
        public int UpdateDatosCertificado(string FENIT, string FERUTACER, string FEPASSCER,
            string FEPIN, string FELLAVE, string FESOFID,
            string FESETPRU)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "UPDATE FEDATOSCERT " +
                  $"SET FERUTACER = '{ FERUTACER }', " +
                    $"FEPASSCER = '{ FEPASSCER }', " +
                    $"FEPIN = '{ FEPIN }', " +
                    $"FELLAVE = '{ FELLAVE }', " +
                    $"FESOFID = '{ FESOFID }', " +
                    $"FESETPRU = '{ FESETPRU }' " +
                  $" WHERE FENIT = '{ FENIT }'";
            return dbs.Execute(QRY);
        }
        public int UpdateDatosCerTerminal(string DTNIT, string DTSOFTWAREID, string DTTESTSETID,
            string DTPIN, string DTLLAVE)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "UPDATE FEDATOSTERMINAL " +
                  $"SET DTSOFTWAREID = '{ DTSOFTWAREID }', " +
                    $"DTTESTSETID = '{ DTTESTSETID }', " +
                    $"DTPIN = '{ DTPIN }', " +
                    $"DTLLAVE = '{ DTLLAVE }' " +
                  $" WHERE DTNIT = '{ DTNIT }'";
            return dbs.Execute(QRY);
        }
    }
}