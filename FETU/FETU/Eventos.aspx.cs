using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FETU.Querys;
using System.Data;

namespace FETU
{
    public partial class Eventos : System.Web.UI.Page
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

        protected void cmbSede_SelectedIndexChanged()
        {

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

                if (Convert.ToDateTime(txtFecFin.Text) < Convert.ToDateTime(txtFecIni.Text))
                {
                    Alert("Error", "La fecha final no puede ser inferior a la inicial", 3, "Aceptar");
                    return;
                }
                if ((DateTime.Parse(txtFecFin.Text) - DateTime.Parse(txtFecIni.Text)).TotalDays > 30)
                {
                    Alert("Error", "El rango de consulta no puede sobre pasar 30 días", 3, "Aceptar");
                    return;
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
                txtFecIni.Text = "";
                txtFecFin.Text = "";
                cmbSede.SelectedIndex = 0;
                cmbTerminal.SelectedIndex = 0;
                Response.Redirect("Eventos.aspx");
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
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