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
                        "ORDER BY DSDES ";
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
                        "ORDER BY DSDES ";
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
                        "ORDER BY DSDES ";
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
                        $"AND EVSEDTER = '{sede}' " +
                        "ORDER BY EVFECMON DESC ";
            return dbs.OpenData(QRY);
        }

        public DataTable selectHorasEventos(string fechaIni, string fechaFin, string sede)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT DISTINCT(TO_CHAR(EVHORMON, 'HH24:MI')) AS EVHORMON " +
                        "FROM EVENTOS " +
                        $"WHERE EVFECMON BETWEEN TO_DATE('{fechaIni}','MM/DD/YYYY') " +
                        $"AND TO_DATE('{fechaFin}','MM/DD/YYYY') " +
                        $"AND EVSEDTER = '{sede}' " +
                        "ORDER BY EVHORMON ";
            return dbs.OpenData(QRY);
        }

        public DataTable selectEventos(string fecha, string hora, string sede)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT TO_CHAR(EVFECTU, 'MM/DD/YYYY') AS EVFECTU, EVCANPEN, EVCANOK " +
                        "FROM EVENTOS " +
                        $"WHERE EVFECMON = TO_DATE('{fecha}','MM/DD/YYYY') " +
                        $"AND EVHORMON = TO_DATE('{hora}','HH24:MI') " +
                        $"AND EVSEDTER = '{sede}' " + 
                        "ORDER BY EVFECTU";
            return dbs.OpenData(QRY);
        }

        public DataTable selectUltimosEventos()
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT EVSEDTER, DSDES, EVCANPEN, EVCANOK " +
                        "FROM EVENTOS EV1 " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = EVSEDTER " +
                        "WHERE EVFECMON = TO_DATE((SELECT TO_CHAR(MAX(EVFECMON), 'MM/DD/YYYY') AS EVFECMON " +
                                                    "FROM EVENTOS EV2 " +
                                                    "WHERE EV1.EVSEDTER = EV2.EVSEDTER), 'MM/DD/YYYY') " +
                        "AND EVHORMON = TO_DATE((SELECT TO_CHAR(MAX(EVHORMON), 'HH24:MI') AS EVFECMON " +
                                                    "FROM EVENTOS EV3 " +
                                                    "WHERE EV1.EVSEDTER = EV3.EVSEDTER " +
                                                    "AND EV1.EVFECMON = EV3.EVFECMON), 'HH24:MI') " +
                        "GROUP BY EVSEDTER, DSDES, EVCANPEN, EVCANOK ";
            return dbs.OpenData(QRY);
        }
    }
}