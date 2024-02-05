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
            QRY = $"SELECT TFNITTT, TFCODTERMINAL, DSDES, MAX(TFFECDIAN) AS TFFECDIAN " +
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
            QRY = $"SELECT TFNITTT, TFCODTERMINAL, DSDES, MAX(TFFECDIAN) AS TFFECDIAN " +
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
            QRY = $"SELECT TFNITTT, TFCODTERMINAL, DSDES, MAX(TFFECDIAN) AS TFFECDIAN " +
                        "FROM FETASASFA " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TFCODTERMINAL " +
                        $"WHERE TFNITTT = '{nitTerminal}' " +
                        "GROUP BY TFNITTT, TFCODTERMINAL, DSDES " +
                        "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectFacturas(string fecini, string fecfin, string codTerminal)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT DSDES, COUNT(*) AS TOTAL " +
                        "FROM FETASASFA " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TFCODTERMINAL " +
                        $"WHERE TFFECFAC BETWEEN TO_DATE('{fecini} 00:00','MM/DD/YYYY HH24:MI') " +
                        $"AND TO_DATE('{fecfin} 23:59','MM/DD/YYYY HH24:MI') " +
                        $"AND TFCODTERMINAL = '{codTerminal}' " +
                        "AND TFESTADODIAN = 'OK' " +
                        "GROUP BY DSDES " +
                        "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
    }
}