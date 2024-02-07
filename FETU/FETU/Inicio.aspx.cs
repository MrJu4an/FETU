using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FETU.Querys;
using System.Data;
using System.Drawing;
using System.Net;
using System.Configuration;
using System.IO;

namespace FETU
{
    public partial class Inicio : System.Web.UI.Page
    {
        QryFunciones Funciones = new QryFunciones();
        QryTasas Tasas = new QryTasas();
        QryFactura Factura = new QryFactura();
        QryTerminal Terminal = new QryTerminal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CodUsuario"] == null || Session["CodUsuario"].ToString() == "")
            {
                Response.Redirect("Salir.aspx");
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    //Mostramos los datos de Tasas Pendientes
                    DataTable Tabla = new DataTable();
                    string fecha1;
                    string fecha2;
                    Tabla = Tasas.SelectTasasPendientes();
                    if (Tabla != null)
                    {
                        TasasPen.Visible = true;
                        tituloPendientes.Visible = true;
                        grdTasasPen.DataSource = Tabla;
                        grdTasasPen.DataBind();
                        estTabla.Visible = false;
                    }
                    else
                    {
                        TasasPen.Visible = false;
                        tituloPendientes.Visible = false;
                        grdTasasPen.DataSource = null;
                        grdTasasPen.DataBind();
                        estTabla.Visible = true;
                    }

                    //Mostramos los datos de las Últimas transacciones
                    Tabla = Factura.SelectUltimasTransacciones();
                    if (Tabla != null)
                    {
                        UltFacturas.Visible = true;
                        tituloTransacciones.Visible = true;
                        grdUltTransacciones.DataSource = Tabla;
                        grdUltTransacciones.DataBind();
                        estTabla2.Visible = false;
                    }
                    else
                    {
                        UltFacturas.Visible = false;
                        tituloTransacciones.Visible = false;
                        grdUltTransacciones.DataSource = null;
                        grdUltTransacciones.DataBind();
                        estTabla2.Visible = true;
                    }

                    //Llenamos la comparativa con INTEGRA
                    fecha1 = DateTime.Now.ToString("MM/dd/yyyy");
                    fecha2 = DateTime.Now.ToString("MM/dd/yyyy");
                    llenarTablaFETU(fecha1, fecha2, "0", "0");
                    llenarTablaIntegra(fecha1, fecha2);
                    colorearTabla();
                }
                catch (Exception ex)
                {
                    Alert("Error", ex.Message, 3, "Aceptar");
                }
            }
        }

        protected void llenarTablaFETU(string fecIni, string fecFin, string nit, string sede)
        {
            DataTable Tabla = new DataTable();
            //Llenamos la tabla de FETU
            //Si la sede es 0 traemos todas las terminales
            if (sede != "0" && sede != "")
            {
                Tabla = Factura.SelectFacturasSede(fecIni, fecFin, sede);
            }
            else if (nit != "0" && nit != "")
            {
                Tabla = Factura.SelectFacturasNit(fecIni, fecFin, nit);
            }
            else
            {
                Tabla = Factura.SelectFacturas(fecIni, fecFin);
            }

            if (Tabla != null)
            {
                tituloFetu.Visible = true;
                ComparativoFetu.Visible = true;
                grdFetu.DataSource = Tabla;
                grdFetu.DataBind();
                estTablaFetu.Visible = false;
            }
            else
            {
                tituloFetu.Visible = false;
                ComparativoFetu.Visible = false;
                grdFetu.DataSource = null;
                grdFetu.DataBind();
                estTablaFetu.Visible = true;
            }
        }
        protected void llenarTablaIntegra(string fecIni, string fecFin)
        {
            DataTable Tabla = new DataTable();
            DataRow row;
            string total = "";
            //Llenamos la tabla de Integra
            //Llenamos los datos según la grilla de FETU
            Tabla.Columns.Add("DSDES", typeof(string));
            Tabla.Columns.Add("TOTAL", typeof(string));
            for (int i = 0; i < grdFetu.Rows.Count; i++)
            {
                //Consultamos el número de la sede
                string sede = grdFetu.Rows[i].Cells[0].Text.ToString();
                row = Terminal.SelectSede(sede);
                //Teniendo la sede, consumimos el API de Integra
                total = consultarIntegra(fecIni, fecFin, row["DSCODDET"].ToString());
                if (total != null && total != String.Empty)
                {
                    Tabla.Rows.Add(sede, total.Replace("\x022", ""));
                }
            }
            //Si hay datos en la tabla llenamos el datagrid
            if (Tabla != null)
            {
                tituloIntegra.Visible = true;
                ComparativoIntegra.Visible = true;
                grdIntegra.DataSource = Tabla;
                grdIntegra.DataBind();
                estTablaIntegra.Visible = false;
            }
            else
            {
                tituloIntegra.Visible = false;
                ComparativoIntegra.Visible = false;
                grdIntegra.DataSource = null;
                grdIntegra.DataBind();
                estTablaIntegra.Visible = true;
            }
        }
        protected void colorearTabla()
        {
            //Recorremos ambas tablas
            for (int i = 0; i < grdFetu.Rows.Count; i++)
            {
                if (int.Parse(grdFetu.Rows[i].Cells[1].Text.ToString()) != double.Parse(grdIntegra.Rows[i].Cells[1].Text.ToString()))
                {
                    grdFetu.Rows[i].BackColor = Color.Red;
                    grdFetu.Rows[i].ForeColor = Color.White;
                    grdIntegra.Rows[i].BackColor = Color.Red;
                    grdIntegra.Rows[i].ForeColor = Color.White;
                }
            }
        }

        protected string consultarIntegra(string fecini, string fecfin, string sede)
        {
            WebResponse Response;
            Stream Stream;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string url = Convert.ToString(ConfigurationManager.AppSettings["urlIntegra"] + $"?sede={sede}&fechaDesde={fecini}&fechaHasta={fecfin}");
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.Method = "GET";
            Request.ContentType = "application/json";
            Request.Accept = "application/json";

            //Make the web request and get the response
            Response = Request.GetResponse();
            Stream = Response.GetResponseStream();
            StreamReader reader = new StreamReader(Stream);
            var result_post = reader.ReadToEnd();
            if (result_post == "")
            {
                return null;
            }
            return result_post;
        }

        protected void Alert(string title, string message, int type, string buttonText, string foco = "")
        {
            string _type = "";
            switch (type)
            {
                case 1:
                    _type = "success";
                    break;
                case 2:
                    _type = "error";
                    break;
                case 3:
                    _type = "warning";
                    break;
                case 4:
                    _type = "info";
                    break;
                case 5:
                    _type = "question";
                    break;
                default:
                    _type = "info";
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "Alert", $"<script>mostraAlerta('{ title }', '{ message }', '{ _type }', '{ buttonText }', '{ foco }') </script>", false);
        }
    }
}