using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FETU.Querys;

namespace FETU
{
    public partial class Login : System.Web.UI.Page
    {
        QryLogin Log = new QryLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["CodUsuario"] = null;
                Session["NomUsuario"] = null;
                Session["FechaSistema"] = null;
                Session["ModuloApp"] = null;
                Session["TipoUsuario"] = null;
                Session["NombreTipo"] = null;
                Session["UsuEmail"] = null;
                Session["NitTerminal"] = null;
                txtUsu.Focus();
            }
            catch
            {
                Alert("Error", "Error en la conexión", 2, "Aceptar");
            }
        }
        protected void btnInicioSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsu.Value == "")
                {
                    Alert("Advertencia", "Ingrese el nombre del usuario", 3, "Aceptar");
                    return;
                }
                if (txtPss.Value == "")
                {
                    Alert("Advertencia", "Ingrese la contraseña", 3, "Aceptar");
                    return;
                }
                DataRow usua = Log.SelectFEUSUSIS(txtUsu.Value);
                if (usua != null)
                {
                    if (usua["USESTADO"].ToString() == "A")
                    {
                        string Modulo = Log.ValidarModulo(usua["USCODUSU"].ToString());
                        string NombreTipo = "";
                        switch (usua["USTIPO"].ToString())
                        {
                            case "CTEC":
                                NombreTipo = "ADMINISTRADOR";
                                break;
                            case "TT":
                            case "CTT":
                                NombreTipo = "TERMINAL TRANS";
                                break;
                            case "ET":
                                NombreTipo = "EMPRESA TRANS";
                                break;
                        }
                        DateTime fechaActual = DateTime.Now;
                        if (encrypt(txtPss.Value) == usua["USPASSWD"].ToString())
                        {
                            Session["CodUsuario"] = usua["USCODUSU"].ToString();
                            Session["NomUsuario"] = usua["USNOMBRE"].ToString();
                            Session["FechaSistema"] = fechaActual;
                            Session["ModuloApp"] = Modulo;
                            Session["TipoUsuario"] = usua["USTIPO"].ToString();
                            Session["NombreTipo"] = NombreTipo;
                            Session["UsuEmail"] = usua["USEMAIL"].ToString();
                            Log.insertGelogAud(Session["CodUsuario"].ToString(), Session["ModuloApp"].ToString() + "-LOGIN", "SE INICIA SESIÓN EN FETU");
                            Response.Redirect("Inicio.aspx");
                        }
                        else
                        {
                            Alert("Advertencia", "Combinación de datos incorrecta", 3, "Aceptar", "txtPss");
                        }
                    }
                    else
                    {
                        Alert("Advertencia", "Combinación de datos incorrecta", 3, "Aceptar", "txtUsu");
                    }
                }
                else
                {
                    Alert("Advertencia", "Combinación de datos incorrecta", 3, "Aceptar", "txtUsu");
                }
            }
            catch (Exception ex)
            {
                Alert("Advertencia", ex.Message, 3, "Aceptar");
            }
        }
        //Función para encriptar la contraseña y el token
        private string encrypt(string data)
        {
            //:::Instanciamos al metodo Hash de la libreria OC.Core.Crypto
            OC.Core.Crypto.Hash has = new OC.Core.Crypto.Hash();
            //:::Obtenemos el valor del string ingresado por el usuario
            string text = data;
            //:::Encriptamos el string recibido al metodo SHA256 y los motramos en el TextBox en minúsculas
            return has.Sha256(text).ToLower();
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
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", $"<script>mostraAlerta('{ title }', '{ message }', '{ _type }', '{ buttonText }', '{ foco }') </script>", false);
        }
    }
}