using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FETU.Querys;
using System.Configuration;
using System.Net;
using System.IO;
using System.Drawing;

namespace FETU
{
    public partial class IntegraVSGo : System.Web.UI.Page
    {
        QryFunciones Funciones = new QryFunciones();
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
                    DataTable Tabla = new DataTable();
                    // Se inicializan los campos y combos
                    Tabla = Funciones.SelectTerminales();
                    cmbTerminal.Items.Clear();
                    cmbTerminal.DataTextField = "DTRAZONSOCIAL";
                    cmbTerminal.DataValueField = "DTNIT";
                    cmbTerminal.DataSource = Tabla;
                    cmbTerminal.DataBind();
                    cmbTerminal.Items.Insert(0, new ListItem("--- SELECCIONE ---", ""));
                    cmbTerminal_SelectedIndexChanged(null, null);
                }
                catch (Exception ex)
                {
                    Alert("Error", ex.Message, 3, "Aceptar");
                }
            }
        }

        protected void cmbTerminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTerminal.SelectedValue != "")
                {
                    DataTable Tabla = new DataTable();
                    Tabla = Funciones.ConsultarSedesTerminal(cmbTerminal.SelectedValue);
                    if (Tabla != null)
                    {
                        Sedes.Visible = true;
                        cmbSede.Items.Clear();
                        cmbSede.DataTextField = "DSDES";
                        cmbSede.DataValueField = "STCODTERMINAL";
                        cmbSede.DataSource = Tabla;
                        cmbSede.DataBind();
                        cmbSede.Items.Insert(0, new ListItem("--- SELECCIONE ---", ""));
                    }
                    else
                    {
                        Sedes.Visible = false;
                        cmbSede.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFecIni.Text == "")
                {
                    Alert("Error", "Seleccione la fecha inicial de búsqueda", 3, "Aceptar");
                    return;
                }
                if (txtFecFin.Text == "")
                {
                    Alert("Error", "Seleccione la fecha final de búsqueda", 3, "Aceptar");
                    return;
                }

                if (DateTime.Parse(txtFecFin.Text) < DateTime.Parse(txtFecIni.Text))
                {
                    Alert("Error", "La fecha final no puede ser inferior a la inicial", 3, "Aceptar");
                    return;
                }
                if ((DateTime.Parse(txtFecFin.Text) - DateTime.Parse(txtFecIni.Text)).TotalDays > 30)
                {
                    Alert("Error", "El rango de consulta no puede sobre pasar 30 días", 3, "Aceptar");
                    return;
                }
                //if (cmbTerminal.SelectedIndex == 0)
                //{
                //    Alert("Error", "Seleccione una terminal", 3, "Aceptar");
                //    return;
                //}
                //if (cmbSede.SelectedIndex == 0)
                //{
                //    Alert("Error", "Seleccione una sede", 3, "Aceptar");
                //    return;
                //}

                //Llenamos las tablas
                llenarTablaFETU(txtFecIni.Text, txtFecFin.Text, cmbTerminal.SelectedValue, cmbSede.SelectedValue);
                llenarTablaIntegra(txtFecIni.Text, txtFecFin.Text);

                //Coloreamos la tabla
                colorearTabla();
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }
        protected void llenarTablaFETU(string fecIni, string fecFin, string nit, string sede)
        {
            DataTable Tabla = new DataTable();
            //Llenamos la tabla de FETU
            //Si la sede es 0 traemos todas las terminales
            if (sede != "0" && sede != "")
            {
                Tabla = Factura.SelectFacturasSede(txtFecIni.Text, txtFecFin.Text, sede);
            }
            else if(nit != "0" && nit != "")
            {
                Tabla = Factura.SelectFacturasNit(txtFecIni.Text, txtFecFin.Text, nit);
            }
            else
            {
                Tabla = Factura.SelectFacturas(txtFecIni.Text, txtFecFin.Text);
            }

            if (Tabla != null)
            {
                tituloFetu.Visible = true;
                ComparativoFetu.Visible = true;
                grdFetu.DataSource = Tabla;
                grdFetu.DataBind();
                estTabla2.Visible = false;
            }
            else
            {
                tituloFetu.Visible = false;
                ComparativoFetu.Visible = false;
                grdFetu.DataSource = null;
                grdFetu.DataBind();
                estTabla2.Visible = true;
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
                total = consultarIntegra(txtFecIni.Text, txtFecFin.Text, row["DSCODDET"].ToString());
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
                estTabla.Visible = false;
            }
            else
            {
                tituloIntegra.Visible = false;
                ComparativoIntegra.Visible = false;
                grdIntegra.DataSource = null;
                grdIntegra.DataBind();
                estTabla.Visible = true;
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

        protected string getData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Terminales", typeof(string)));
                dt.Columns.Add(new DataColumn("Integra", typeof(int)));
                dt.Columns.Add(new DataColumn("GO", typeof(int)));
                string strData = string.Empty;

                if (grdFetu.Rows.Count > 0)
                {
                    for (int i = 0; i < grdFetu.Rows.Count; i++)
                    {
                        dt.Rows.Add(new object[] { grdFetu.Rows[i].Cells[0].Text.ToString(), grdIntegra.Rows[i].Cells[1].Text.ToString(), grdFetu.Rows[i].Cells[1].Text.ToString() });
                    }
                    strData = "[['Terminales', 'Integra', 'GO'],";

                    foreach (DataRow dr in dt.Rows)
                    {
                        strData += "[";
                        strData += $"'{dr[0]}',{dr[1]},{dr[2]}";
                        strData += "],";
                    }

                    strData += "]";

                    GraficoComparativo.Visible = true;
                }
                else
                {
                    GraficoComparativo.Visible = false;
                }
                
                return strData;
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
                return null;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("IntegraVsGo.aspx");
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
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