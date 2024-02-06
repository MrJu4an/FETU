using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FETU.Querys;

namespace FETU
{
    public partial class Salir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClientScript.RegisterStartupScript(GetType(), "Script", "<script>removeLocalStorage('" + Session["CodUsuario"] + "')</script>");
                QryLogin Delete = new QryLogin();
                try
                {
                    Delete.insertGelogAud(Session["CodUsuario"].ToString(), Session["ModuloApp"].ToString() + "-SALIR", "SE CIERRA SESIÓN EN GOPETT ONLINE");
                }
                catch
                {
                    Session["CodUsuario"] = null;
                    Session["NomUsuario"] = null;
                    Session["FechaSistema"] = null;
                    Session["ModuloApp"] = null;
                    Session["TipoUsuario"] = null;
                    Session["NombreTipo"] = null;
                    Session["UsuEmail"] = null;
                    Response.Redirect("Login.aspx", false);
                }
            }
        }
    }
}