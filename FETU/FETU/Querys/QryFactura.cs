using Ctec;
using System.Data;
using static Ctec.Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FETU.Querys
{
    public class QryFactura
    {
        public DataTable SelectUltimasTransacciones()
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT TFNITTT, TFCODTERMINAL, DSDES, TO_CHAR(MAX(TFFECDIAN), 'MM/DD/YYYY HH24:MI') AS TFFECDIAN " +
                        "FROM FETASASFA " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TFCODTERMINAL " +
                        "GROUP BY TFNITTT, TFCODTERMINAL, DSDES " +
                        "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectUltimaTransaccionCodTerminal(string codTerminal)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT TFNITTT, TFCODTERMINAL, DSDES, TO_CHAR(MAX(TFFECDIAN), 'MM/DD/YYYY HH24:MI') AS TFFECDIAN " +
                        "FROM FETASASFA " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TFCODTERMINAL " +
                        $"WHERE TFCODTERMINAL = '{codTerminal}' " +
                        "GROUP BY TFNITTT, TFCODTERMINAL, DSDES " +
                        "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectUltimaTransaccionNitTerminal(string nitTerminal)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT TFNITTT, TFCODTERMINAL, DSDES, TO_CHAR(MAX(TFFECDIAN), 'MM/DD/YYYY HH24:MI') AS TFFECDIAN " +
                        "FROM FETASASFA " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TFCODTERMINAL " +
                        $"WHERE TFNITTT = '{nitTerminal}' " +
                        "GROUP BY TFNITTT, TFCODTERMINAL, DSDES " +
                        "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectFacturas(string fecini, string fecfin)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT TFCODTERMINAL, DSDES, COUNT(*) - (SELECT COUNT(*) FROM FETASASFA T2 " +
                                                    $"WHERE TFFECFAC BETWEEN TO_DATE('{fecini} 00:00','MM/DD/YYYY HH24:MI') " +
                                                    $"AND TO_DATE('{fecfin} 23:59','MM/DD/YYYY HH24:MI') " +
                                                    $"AND T2.TFCODTERMINAL = T1.TFCODTERMINAL " +
                                                    "AND TFESTADODIAN = 'OK' " +
                                                    "AND TFTIPFAC = 'NC') AS TOTAL " +
                    "FROM FETASASFA T1 " +
                    "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                    "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = T1.TFCODTERMINAL " +
                    $"WHERE TFFECFAC BETWEEN TO_DATE('{fecini} 00:00','MM/DD/YYYY HH24:MI') " +
                    $"AND TO_DATE('{fecfin} 23:59','MM/DD/YYYY HH24:MI') " +
                    "AND TFESTADODIAN = 'OK' " +
                    "AND TFTIPFAC = 'FE' " +
                    "GROUP BY TFCODTERMINAL, DSDES " +
                    "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectFacturasSede(string fecini, string fecfin, string codTerminal)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT TFCODTERMINAL, DSDES, COUNT(*) - (SELECT COUNT(*) FROM FETASASFA " +
                                                    $"WHERE TFFECFAC BETWEEN TO_DATE('{fecini} 00:00','MM/DD/YYYY HH24:MI') " +
                                                    $"AND TO_DATE('{fecfin} 23:59','MM/DD/YYYY HH24:MI') " +
                                                    $"AND TFCODTERMINAL = '{codTerminal}' " +
                                                    "AND TFESTADODIAN = 'OK' " +
                                                    "AND TFTIPFAC = 'NC') AS TOTAL " +
                    "FROM FETASASFA " +
                    "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                    "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TFCODTERMINAL " +
                    $"WHERE TFFECFAC BETWEEN TO_DATE('{fecini} 00:00','MM/DD/YYYY HH24:MI') " +
                    $"AND TO_DATE('{fecfin} 23:59','MM/DD/YYYY HH24:MI') " +
                    $"AND TFCODTERMINAL = '{codTerminal}' " +
                    "AND TFESTADODIAN = 'OK' " +
                    "AND TFTIPFAC = 'FE' " +
                    "GROUP BY TFCODTERMINAL, DSDES " +
                    "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectFacturasNit(string fecini, string fecfin, string nitTerminal)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = "SELECT TFCODTERMINAL, DSDES, COUNT(*) - (SELECT COUNT(*) FROM FETASASFA T2 " +
                                                    $"WHERE TFFECFAC BETWEEN TO_DATE('{fecini} 00:00','MM/DD/YYYY HH24:MI') " +
                                                    $"AND TO_DATE('{fecfin} 23:59','MM/DD/YYYY HH24:MI') " +
                                                    $"AND TFNITTT = '{nitTerminal}' " +
                                                    "AND T2.TFCODTERMINAL = T1.TFCODTERMINAL " +
                                                    "AND TFESTADODIAN = 'OK' " +
                                                    "AND TFTIPFAC = 'NC') AS TOTAL " +
                    "FROM FETASASFA T1 " +
                    "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                    "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = T1.TFCODTERMINAL " +
                    $"WHERE TFFECFAC BETWEEN TO_DATE('{fecini} 00:00','MM/DD/YYYY HH24:MI') " +
                    $"AND TO_DATE('{fecfin} 23:59','MM/DD/YYYY HH24:MI') " +
                    $"AND TFNITTT = '{nitTerminal}' " +
                    "AND TFESTADODIAN = 'OK' " +
                    "AND TFTIPFAC = 'FE' " +
                    "GROUP BY TFCODTERMINAL, DSDES " +
                    "ORDER BY DSDES";

            return dbs.OpenData(QRY);
        }
    }
}