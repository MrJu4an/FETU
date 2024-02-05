using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FETU.Querys;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Configuration;

namespace FETU
{
    public partial class SiteMaster : MasterPage
    {
        QryLogin Log = new QryLogin();
        QryTerminal Terminal = new QryTerminal();
        QryEmpresa Empresa = new QryEmpresa();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtNombre.Text = Session["NomUsuario"].ToString();
                    txtEmail.Text = Session["UsuEmail"].ToString();
                }
                
                //
                switch (Session["TipoUsuario"].ToString())
                {
                    case "TT":
                    case "CTT":
                        DataRow drTerminal = Terminal.SelectDatosTerminal(Session["NitTerminal"].ToString());
                        lblUsuario.Text = $"Terminal de transporte asignada: { drTerminal["DTNIT"].ToString() } - { drTerminal["DTRAZONSOCIAL"].ToString() }";
                        break;
                    case "ET":
                        DataRow drEmpresa = Empresa.SelectEmpresaByID(Session["CodEmpresa"].ToString(), Session["NitTerminal"].ToString());
                        lblUsuario.Text = $"Empresa de transporte asignada: { drEmpresa["EMNITFAC"].ToString() } - { drEmpresa["EMRAZONSOCIAL"].ToString() }";
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }
        public string EliminarFormato(string numeroFormateado)
        {
            string pattern = @"[\'\&\""\*\|\/\ \=\;\<\>\(\)]";
            string replacement = String.Empty;
            Regex regex = new Regex(pattern);

            return regex.Replace(numeroFormateado, replacement);
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int update = 0;
                string nuevaContra = EliminarFormato(txtContraN.Text);
                string verifiContra = EliminarFormato(txtContraV.Text);
                string nuevoToken = EliminarFormato(txtTokenN.Text);
                string verifiToken = EliminarFormato(txtTokenV.Text);
                if (txtNombre.Text == "")
                {
                    Alert("Error", "Digite el nombre del usuario", 3, "Aceptar");
                    return;
                }
                if (txtEmail.Text == "")
                {
                    Alert("Error", "Digite el e-mail del usuario", 3, "Aceptar");
                    return;
                }
                if (nuevaContra == "")
                {
                    Alert("Error", "Digite la nueva contraseña", 3, "Aceptar");
                    return;
                }
                if (nuevaContra.Length < 8)
                {
                    Alert("Error", "La contraseña debe contener mínimo 8 caracteres. [Entre minúsculas, mayúsculas y números]", 3, "Aceptar");
                    return;
                }
                if (verifiContra == "")
                {
                    Alert("Error", "Confirme su nueva contraseña", 3, "Aceptar");
                    return;
                }
                if (nuevoToken == "")
                {
                    Alert("Error", "Digite el nuevo token", 3, "Aceptar");
                    return;
                }
                if (!IsNumeric(nuevoToken))
                {
                    Alert("Error", "El token debe de ser numérico", 3, "Aceptar");
                    return;
                }
                if (nuevoToken.Length < 6)
                {
                    Alert("Error", "El nuevo token debe de contener 6 digitos", 3, "Aceptar");
                    return;
                }
                if (verifiToken == "")
                {
                    Alert("Error", "Confirme su nuevo token", 3, "Aceptar");
                    return;
                }
                if (nuevaContra == verifiContra && nuevoToken == verifiToken)
                {
                    Regex rx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
                    if (rx.IsMatch(nuevaContra))
                    {
                        update = Log.updateUsuario(Session["CodUsuario"].ToString(), txtNombre.Text, txtEmail.Text,
                            encrypt(nuevaContra), encrypt(nuevoToken));
                        if (update > 0)
                        {
                            Session["NomUsuario"] = txtNombre.Text;
                            Session["UsuEmail"] = txtEmail.Text;
                            txtContraN.Text = string.Empty;
                            txtContraV.Text = string.Empty;
                            txtTokenN.Text = string.Empty;
                            txtTokenV.Text = string.Empty;
                            EnvioMail(txtEmail.Text, txtNombre.Text);
                            Alert("Operación exitosa", "Se han actualizado correctamente las credenciales", 1, "Aceptar");
                        }
                        else
                        {
                            Alert("Error", "Ocurrió un error al momento de actualizar las credenciales", 3, "Aceptar");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ocultaVerfi", "ocultaFrmVerifi();", true);
                        Alert("Error", "La contraseña nueva debe contener mínimo 8 caracteres. [Entre minúsculas, mayúsculas y números ]", 3, "Aceptar");
                        return;
                    }
                    //Cumple las condiciones, por lo tanto actualiza
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ocultaVerfi", "ocultaFrmVerifi();", true);
                    Alert("Error", "Verifique que los datos coincidan", 3, "Aceptar");
                }
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }
        public void EnvioMail(string EmailReceptor, string Usuario)
        {
            try
            {
                MailMessage correo = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                string URL = ConfigurationManager.AppSettings.Get("url");
                string asunto = "ACTUALIZACIÓN DE DATOS Y CREDENCIALES";

                string htmlBody = "<head>"
                + "<Style>"
                + "body{ margin: 0; padding: 0; background-color: #ECECEC; }"
                + ".tablaPrincipal{ width: 100%; background-color: #ECECEC } "
                + ".tablaBordeada{  width: 100%; background-color: #ffffff; width: 70%; margin-top: 12px;  border-radius: 15px !important; border: 2px !important; box-shadow: 5px 5px black !important; margin-bottom: 12px; } "
                + ".TdPrincipal{ align-content:center; } "
                + ".divImagen{ margin-top: 5px; margin-bottom: 10px; }"
                + ".imgLogo{ width: 200px; height: 112px; display: block; }"
                + ".divTitulo{ color: #153643; font-family: Arial, sans-serif; }"
                + ".divTextoCli{ color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; width: 80%; }"
                + ".divDatos{ color: #153643; font-family: Arial, sans-serif; font-size: 15px; line-height: 20px; width: 80%; }"
                + "p{ text-align:justify; }"
                + ".tdTextoPequeño{ padding-left:15px; padding-right:15px; }"
                + ".TextoPequeño{ background-color:#ffffff; font-size: 13px; text-align:justify; color: black; }"
                + "</style>"
                + "</head>"
                + "<body style=\"margin: 0; padding: 0;\" bgcolor=\"#ECECEC\">"
                + "<table  cellpadding=\"0\" cellspacing=\"0\" class=\"tablaPrincipal\"> "
                + "<tr>"
                + "<td > "
                + "<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"tablaBordeada\">"
                + "<tr>"
                + "<td>"
                + "<table>"
                + "<tr>"
                + "<td>"
                + "<div align=\"center\" class=\"divImagen\">"
                + "<img class=\"imgLogo\" src=\"https://efactonline.fitcloud.com.co/GopettOnline/Images/logo.png\" alt=\"Creating Email Magic\"  />"
                + "</div>"
                + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td>"
                + "<div align=\"center\" class=\"divTitulo\" > "
                + "<h2> Actualización de usuario </h2>"
                + "</div>"
                + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td>"
                + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td align=\"center\">"
                + "<div class=\"divTextoCli\">"
                + "<p>"
                + "Se ha actualizado correctamente los datos del usuario <b>" + Usuario + "</b> a tráves de Gopett Online. <br />"
                + "</p>"
                + "</div>"
                + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td align=\"center\">"
                + "<div Class=\"divDatos\">"
                + "<p>"
                + "Cordialmente, <br>"
                + "Plataforma Gopett Online "
                + "</p>"
                + "</div>"
                + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td Class=\"tdTextoPequeño\">"
                + "<hr />"
                + "<p Class=\"TextoPequeño\">"
                + ""
                + "</p>"
                + "</td>"
                + "</tr>"
                + "</table>"
                + "</td>"
                + " </tr>"
                + "</table>"
                + "</td>"
                + "</tr>"
                + "</table>"
                + "</body>";

                string Correo = ConfigurationManager.AppSettings.Get("correo");
                correo.From = new MailAddress(Correo);

                correo.To.Add(EmailReceptor);
                correo.SubjectEncoding = System.Text.Encoding.UTF8;
                correo.Subject = asunto;
                correo.Body = htmlBody;
                correo.BodyEncoding = System.Text.Encoding.UTF8;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.High;
                string Clave = ConfigurationManager.AppSettings.Get("clave");
                smtp.Credentials = new System.Net.NetworkCredential(Correo, Clave);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("puerto"));
                smtp.Host = ConfigurationManager.AppSettings.Get("host");
                smtp.EnableSsl = true;
                smtp.Send(correo);
                correo.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public static Boolean IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
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