using Ctec;
using System.Data;
using static Ctec.Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FETU.Querys
{
    public class QryFunciones
    {
        public DataTable SelectDepartamentos()
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT DISTINCT CDCODDPTO, UPPER(CDDPTO) AS CDDPTO FROM FECIUDDPTO ORDER BY CDDPTO";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectCiudades(string CDCODDPTO)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT CDCODCIUD, CDCIUDAD FROM FECIUDDPTO WHERE CDCODDPTO = '{ CDCODDPTO }' ORDER BY CDCIUDAD";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectCatalogo(string STDES)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT DSCODDET, DSDES FROM GESUPTIP INNER JOIN GEDETSUPTIP ON STCODTIP = DSCODTIP WHERE STDES = '{ STDES }' ORDER BY DSDES";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectTerminales()
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT DTNIT, DTRAZONSOCIAL FROM FEDATOSTERMINAL ORDER BY DTRAZONSOCIAL ASC";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectTerminal(string DTNIT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT DTNIT, DTRAZONSOCIAL FROM FEDATOSTERMINAL WHERE DTNIT = '{ DTNIT }'";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectEmpresas(string EMNITTT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT EMCODIGO, EMRAZONSOCIAL FROM FEEMPRESAS WHERE EMNITTT = '{ EMNITTT }' ORDER BY EMRAZONSOCIAL ASC";
            return dbs.OpenData(QRY);
        }
        public DataTable SelectEmpresa(string EMNITTT, string EMCODIGO)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT EMCODIGO, EMRAZONSOCIAL FROM FEEMPRESAS WHERE EMNITTT = '{ EMNITTT }' AND EMCODIGO = '{ EMCODIGO }'";
            return dbs.OpenData(QRY);
        }
        public int SelectSumBolsa(string BTNIT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT NVL(SUM(BTCANTIDAD), 0) FROM FEBOLSASTERMINAL WHERE BTNIT = '{ BTNIT }'";
            return Convert.ToInt32(dbs.Scalar(QRY));
        }
        public int SelectCountTasas(string TFNITTT)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT NVL(COUNT(*), 0) FROM FETASASFA WHERE TFNITTT = '{ TFNITTT }'";
            return Convert.ToInt32(dbs.Scalar(QRY));
        }
        public DataRow SelectCodCatalogo(string Catalogo)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT * FROM gesuptip WHERE stdes = '{ Catalogo }'";
            return dbs.OpenRow(QRY);
        }
        public bool ValDetCatalogo(string CodCatalogo, string CodDetalle)
        {
            try
            {
                App.Motor = DatabaseType.Oracle;
                dbs.TypeData = DatabaseType.Oracle;
                dbs.ValidarConexion();
                QRY = $"SELECT * FROM GEDETSUPTIP WHERE DSCODTIP = '{ CodCatalogo }' AND DSCODDET = '{ CodDetalle }'";
                dr1 = dbs.OpenRow(QRY);
                if (dr1 != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public void InsertDetalle(string Catalogo, string Detalle, string Descrip, string Estado)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"INSERT INTO GEDETSUPTIP (DSCODTIP, DSCODDET, DSDES, DSEST) VALUES ('{ Catalogo }', '{ Detalle }', '{ Descrip }', '{ Estado }')";
            dbs.Execute(QRY);
        }
        public void UpdateDetalle(string Catalogo, string Detalle, string Estado)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"UPDATE GEDETSUPTIP SET DSEST = '{ Estado }' WHERE DSCODTIP = '{ Catalogo}' AND DSCODDET = '{ Detalle }'";
            dbs.Execute(QRY);
        }
        public DataTable ConsultarSedesTerminal(string Terminal)
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT STCODTERMINAL, DSDES FROM FESEDESTERMINAL " +
                    $"INNER JOIN GESUPTIP ON STDES = 'TERMINALES' " +
                    $"INNER JOIN GEDETSUPTIP ON DSCODTIP = STCODTIP AND DSCODDET = STCODTERMINAL " +
                  $"WHERE STNIT = '{ Terminal }' " +
                  $"ORDER BY STCODTERMINAL";
            return dbs.OpenData(QRY);
        }
        public DataTable ConsultarCajas(string Terminal, string Sede = "")
        {
            App.Motor = DatabaseType.Oracle;
            dbs.TypeData = DatabaseType.Oracle;
            dbs.ValidarConexion();
            QRY = $"SELECT DISTINCT TFCAJA AS TFCODCAJ, TFCAJA FROM FETASASFA WHERE TFNITTT = '{Terminal}'";
            if (Sede != "")
            {
                QRY += $"AND TFCODTERMINAL = '{ Sede }'";
            }
            return dbs.OpenData(QRY);
        }
    }
}