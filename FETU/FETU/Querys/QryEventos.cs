using Ctec;
using System.Data;
using static Ctec.Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FETU.Querys
{
    public class QryEventos
    {
        public DataTable selectSedesEventos(string fechaIni, string fechaFin)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT DISTINCT(EVSEDTER) AS EVSEDTER, DSDES " +
                        "FROM EVENTOS " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = EVSEDTER " +
                        $"WHERE EVFECMON BETWEEN TO_DATE('{fechaIni}','MM/DD/YYYY') " +
                        $"AND TO_DATE('{fechaFin}','MM/DD/YYYY') " +
                        "ORDER BY EVSEDTER";
            return dbs.OpenData(QRY);
        }

        public DataTable selectSedeEventos(string fechaIni, string fechaFin, string sede)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT DISTINCT(EVSEDTER) AS EVSEDTER, DSDES " +
                        "FROM EVENTOS " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = EVSEDTER " +
                        $"WHERE EVFECMON BETWEEN TO_DATE('{fechaIni}','MM/DD/YYYY') " +
                        $"AND TO_DATE('{fechaFin}','MM/DD/YYYY') " +
                        $"AND EVSEDTER = '{sede}' " +
                        "ORDER BY EVSEDTER";
            return dbs.OpenData(QRY);
        }

        public DataTable selectTerminalEventos(string fechaIni, string fechaFin, string nit)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT DISTINCT(EVSEDTER) AS EVSEDTER, DSDES " +
                        "FROM EVENTOS " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = EVSEDTER " +
                        $"WHERE EVFECMON BETWEEN TO_DATE('{fechaIni}','MM/DD/YYYY') " +
                        $"AND TO_DATE('{fechaFin}','MM/DD/YYYY') " +
                        $"AND EVNITTT = '{nit}' " +
                        "ORDER BY EVSEDTER";
            return dbs.OpenData(QRY);
        }

        public DataTable selectFechasEventos(string fechaIni, string fechaFin, string sede)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT DISTINCT(TO_CHAR(EVFECMON, 'MM/DD/YYYY')) AS EVFECMON " +
                        "FROM EVENTOS " +
                        $"WHERE EVFECMON BETWEEN TO_DATE('{fechaIni}','MM/DD/YYYY') " +
                        $"AND TO_DATE('{fechaFin}','MM/DD/YYYY') " + 
                        $"AND EVSEDTER = '{sede}' ";
            return dbs.OpenData(QRY);
        }

        public DataTable selectEventos(string fecha, string sede)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT TO_CHAR(EVHORMON, 'HH24:MI') AS EVHORMON, EVCANPEN, EVCANOK " +
                    "FROM EVENTOS " +
                    $"WHERE EVFECMON = TO_DATE('{fecha}','MM/DD/YYYY') " +
                    $"AND EVSEDTER = '{sede}' " +
                    "ORDER BY EVHORMON ";
            return dbs.OpenData(QRY);
        }
    }
}