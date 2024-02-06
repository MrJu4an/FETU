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
    public partial class Inicio : System.Web.UI.Page
    {
        QryFunciones Funciones = new QryFunciones();
        QryTasas Tasas = new QryTasas();
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
                    //Mostramos los datos de Tasas Pendientes
                    DataTable Tabla = new DataTable();
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
                }
                catch (Exception ex)
                {
                    Alert("Error", ex.Message, 3, "Aceptar");
                }
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