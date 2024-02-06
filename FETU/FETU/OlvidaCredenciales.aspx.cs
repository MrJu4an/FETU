using FETU.Querys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FETU
{
    public partial class OlvidaCredenciales : System.Web.UI.Page
    {
        QryLogin Log = new QryLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtUsu.Focus();
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }

        public string EliminarFormato(string numeroFormateado)
        {
            string pattern = @"[\'\&\""\*\|\/\ \=\;\<\>\(\)]";
            string replacement = string.Empty;
            Regex regex = new Regex(pattern);

            return regex.Replace(numeroFormateado, replacement);
        }
        public void EnvioMail(string email, string codigo)
        {
            MailMessage correo = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            try
            {
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
                + "<h2> Recuperación de contraseña </h2>"
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
                + "El siguiente es su código de recuperación de contraseña, es único e intransferible: <br />"
                + "<h2> " + codigo + "</h2>"
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

                //AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
                //LinkedResource pic1 = new LinkedResource(Server.MapPath(@"\imgs\principal.jpg"), MediaTypeNames.Image.Jpeg);
                //pic1.ContentId = "Pic1";
                //avHtml.LinkedResources.Add(pic1);
                //correo.AlternateViews.Add(avHtml);
                string Correo = ConfigurationManager.AppSettings.Get("correo");
                correo.From = new MailAddress(Correo);

                correo.To.Add(email);
                correo.SubjectEncoding = System.Text.Encoding.UTF8;
                correo.Subject = "RECUPERACIÓN DE CREDENCIALES";
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
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }
        public string codigoAlfaNum()
        {
            string s = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            Random r = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 8; i++)
            {
                int idx = r.Next(0, 61);
                sb.Append(s.Substring(idx, 1));
            }
            return sb.ToString();
        }
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsu.Text != "")
                {
                    DataRow usua = Log.SelectFEUSUSIS(EliminarFormato(txtUsu.Text));
                    if (usua != null)
                    {
                        if (usua["USEMAIL"].ToString() != "")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraEnvia", "verFrmEnvia();", true);
                            txtEmail.Value = usua["USEMAIL"].ToString().Trim();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ocultaEnvia", "ocultarFrmEnvia();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ocultaEnvia", "ocultarFrmEnvia();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }
        protected void btnEnviaCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = codigoAlfaNum();
                EnvioMail(txtEmail.Value.ToString(), codigo);
                hddCodigoVerifi.Value = codigo;
                ScriptManager.RegisterStartupScript(this, GetType(), "ocultaIni", "ocultaFrmIni();", true);
                Alert("Operación exitosa", "Se le ha enviado a su correo electrónico un código de verificación", 1, "Aceptar");
            }
            catch
            {
                Alert("Error", "No fue posible el envío del correo", 3, "Aceptar");
            }
        }
        protected void btnVerificaCod_Click(object sender, EventArgs e)
        {
            try
            {
                string codigoClie = txtCodVerifi.Text;
                string codigoReal = hddCodigoVerifi.Value.ToString();
                if (codigoClie == codigoReal)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ocultaVerfi", "ocultaFrmVerifi();", true);
                    Alert("Operación exitosa", "Código correcto", 1, "Aceptar");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ocultaIni", "ocultaFrmIni();", true);
                    Alert("Error", "Código erróneo", 3, "Aceptar");
                }
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
            }
        }
        protected void btnGuardaNuevaContra_Click(object sender, EventArgs e)
        {
            try
            {
                int update = 0;
                string nuevaContra = EliminarFormato(txtNuevaContra.Value);
                string verifiContra = EliminarFormato(txtVerficaContra.Value);
                string nuevoToken = EliminarFormato(txtNuevoToken.Value);
                string verifiToken = EliminarFormato(txtVerificaToken.Value);
                if (nuevaContra == "")
                {
                    Alert("Error", "Digite la nueva contraseña", 3, "Aceptar");
                    return;
                }
                if (nuevaContra.Length < 8)
                {
                    Alert("Error", "La contraseña debe contener mínimo 8 caracteres. [Entre minúsculas, mayúsculas y números ]", 3, "Aceptar");
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
                if (nuevaContra == verifiContra & nuevoToken == verifiToken)
                {
                    Regex rx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
                    if (rx.IsMatch(nuevaContra))
                    {
                        update = Log.updateUsuarioContra(txtUsu.Text, encrypt(nuevaContra), encrypt(nuevoToken));
                        if (update > 0)
                        {
                            AlertDireccion("Operación exitosa", "Se han actualizado correctamente las credenciales", "success", "InicioSesion.aspx");
                        }
                        else
                        {
                            Alert("Error", "Ocurrió un error al momento de actualizar las credenciales", 3, "Aceptar");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "ocultaVerfi", "ocultaFrmVerifi();", true);
                        Alert("Error", "La contraseña nueva debe contener mínimo 8 caracteres. [Entre minúsculas, mayúsculas y números ]", 3, "Aceptar");
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ocultaVerfi", "ocultaFrmVerifi();", true);
                    Alert("Error", "Verifique los datos coincidan", 3, "Aceptar");
                }
            }
            catch (Exception ex)
            {
                Alert("Error", ex.Message, 3, "Aceptar");
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
        public void AlertDireccion(string mensaje, string subMensaje, string tipo, string url)
        {
            string Script = "<script type='text/javascript'> swal({title:'" + mensaje.Replace("'", " | ") + "', text:'" + subMensaje.Replace("'", " | ") + "' , type:'" + tipo + "', confirmButtonText:'OK'})";
            Script += ".then((result) => {if (result.value) { window.location.href='" + url + "';}});";
            Script += " </script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "swal", Script, false);
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