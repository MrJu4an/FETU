using Ctec;
using System.Data;
using static Ctec.Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FETU.Querys
{
    public class QryTasas
    {
        public DataTable SelectTasasPendientes()
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT TPNITTT, TPCODTERMINAL, DSDES, MIN(TO_CHAR(TPFECTASA, 'MM/DD/YYYY')) AS TPFECTASA, TPCANTIDAD, TPTIPFAC " +
                        "FROM FETASASPEN " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TPCODTERMINAL " +
                        "WHERE TPCANTIDAD <> 0 " +
                        "GROUP BY TPNITTT, TPCODTERMINAL, DSDES, TPCANTIDAD, TPTIPFAC " +
                        "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectTasasPendientesCodTerminal(string codTerminal)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT TPNITTT, TPCODTERMINAL, DSDES, MIN(TO_CHAR(TPFECTASA, 'MM/DD/YYYY')) AS TPFECTASA, TPCANTIDAD, TPTIPFAC " +
                        "FROM FETASASPEN " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TPCODTERMINAL " +
                        $"WHERE TPCODTERMINAL = '{codTerminal}' " +
                        "AND TPCANTIDAD <> 0 " +
                        "GROUP BY TPNITTT, TPCODTERMINAL, DSDES, TPCANTIDAD, TPTIPFAC " +
                        "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectTasasPendientesNitTerminal(string nitTerminal)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT TPNITTT, TPCODTERMINAL, DSDES, MIN(TO_CHAR(TPFECTASA, 'MM/DD/YYYY')) AS TPFECTASA, TPCANTIDAD, TPTIPFAC " +
                        "FROM FETASASPEN " +
                        "INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                        "INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = TPCODTERMINAL " +
                        $"WHERE TPNITTT = '{nitTerminal}' " +
                        "AND TPCANTIDAD <> 0 " +
                        "GROUP BY TPNITTT, TPCODTERMINAL, DSDES, TPCANTIDAD, TPTIPFAC " +
                        "ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
    }
}