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

namespace FETU
{
    public partial class IntegraVSGo : System.Web.UI.Page
    {
        QryFunciones Funciones = new QryFunciones();
        QryFactura Factura = new QryFactura();
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
                DataTable Tabla;
                string total = "";
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
                if (Convert.ToDateTime(txtFecFin.Text) > Convert.ToDateTime(txtFecIni.Text))
                {
                    Alert("Error", "La fecha final no puede ser inferior a la inicial", 3, "Aceptar");
                    return;
                }
                if (cmbTerminal.SelectedIndex == 0)
                {
                    Alert("Error", "Seleccione una terminal", 3, "Aceptar");
                    return;
                }
                if(cmbSede.SelectedIndex == 0)
                {
                    Alert("Error", "Seleccione una sede", 3, "Aceptar");
                    return;
                }

                //Llenamos la tabla de FETU
                Tabla = Factura.SelectFacturas(txtFecIni.Text, txtFecFin.Text, cmbSede.SelectedValue);
                if (Tabla != null)
                {
                    tituloFetu.Visible = true;
                    ComparativoFetu.Visible = true;
                    grdFetu.DataSource = Tabla;
                    grdFetu.DataBind();
                }
                else
                {
                    tituloFetu.Visible = false;
                    ComparativoFetu.Visible = false;
                    grdFetu.DataSource = null;
                    grdFetu.DataBind();
                    estTabla.Visible = true;
                }

                //Llenamos la tabla de Integra
                total = consultarIntegra(txtFecIni.Text, txtFecFin.Text, cmbSede.SelectedValue);
                if (total != null && total != String.Empty)
                {
                    Tabla.Columns.Clear();
                    Tabla.Clear();
                    Tabla.Columns.Add("DSDES", typeof(string));
                    Tabla.Columns.Add("TOTAL", typeof(string));
                    Tabla.Rows.Add(cmbSede.SelectedItem, total.Replace("\x022", ""));

                    tituloIntegra.Visible = true;
                    ComparativoIntegra.Visible = true;
                    grdIntegra.DataSource = Tabla;
                    grdIntegra.DataBind();
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
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
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