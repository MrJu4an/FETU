<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OlvidaCredenciales.aspx.cs" Inherits="FETU.OlvidaCredenciales" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>Gopett Online</title>
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    
	<link rel="icon" href="Images/iconoPrincipal.jpg" type="image/x-icon"/>

    <!-- Bootstrap -->
    <link href="WebComponents/css/bootstrap.min.css" rel="stylesheet">
    <link href="WebComponents/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <script src="WebComponents/js/plugin/webfont/webfont.min.js"></script>
    <!-- NProgress -->
    <link href="WebComponents/nprogress/nprogress.css" rel="stylesheet">

    <!-- Sweetalert2 -->
    <script src="WebComponents/sweetAlert2/sweetalert2.all.js"></script>
    <script src="WebComponents/sweetAlert2/sweetalert2.all.min.js"></script>

    <!-- Custom Theme Style -->
    <link href="WebComponents/css/custom.css" rel="stylesheet">

    <link href="WebComponents/Canvas/style.css" rel="stylesheet" />

    <script type="text/javascript">
        function Enter(field, event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("btnInicioSesion").click()
            };
        };
    </script>
    <style>
        .swal2-popup {
            font-size: 1rem !important;
        }
    </style> 
    <script type="text/javascript">

        function mostraAlerta(titulo, mensaje, tipo, textoBoton, controlFoco) {

            if (controlFoco) {
                swal({
                    title: titulo,
                    html: mensaje,
                    type: tipo,
                    confirmButtonText: textoBoton
                })
                .then((result) => { if (result.value) { document.getElementById(controlFoco).focus(); } });
            } else {
                swal({
                    title: titulo,
                    html: mensaje,
                    type: tipo,
                    confirmButtonText: textoBoton
                });
            }
        }

        function ocultaFrmIni() {
            document.getElementById("frmOlvidaInicial").style.display = "none";
            document.getElementById("frmOlvidaCodigo").style.display = "block";
        };
        function ocultaFrmVerifi() {
            document.getElementById("frmOlvidaInicial").style.display = "none";
            document.getElementById("frmOlvidaCodigo").style.display = "none";
            document.getElementById("frmOlvidaFinal").style.display = "block";
        };
        function verFrmEnvia() {
            document.getElementById("divCorreoExiste").style.display = "block";
            document.getElementById("divFaltaCorreo").style.display = "none";
        };
        function ocultaFrmEnvia() {
            document.getElementById("divCorreoExiste").style.display = "none";
            document.getElementById("divFaltaCorreo").style.display = "block";
        };
    </script>
</head>

<body class="login" id="particles-js">
    <div>
        <div class="login_wrapper">
            <div class="animate form login_form">
                <section class="login_content" id="EnvioCorreo" runat="server">
                    <form runat="server">
                        <h1>Credenciales</h1>
                        <div id="frmOlvidaInicial">
                            <div style="display:flex; flex-wrap:wrap;">
                                <div style="width:10%; padding-right:5px; padding-top:2px;"><img src="Images/iconUsu.png" /></div>
                                <asp:TextBox class="form-control" id="txtUsu"  style="width:90%;" placeholder="Usuario" required="" runat="server" autocomplete="off" onKeypress="return Enter(this,event)" TabIndex="1" ></asp:TextBox>
                            </div>
                             <div class="btnInicioSesion">
                                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSiguiente" autopostback="true" OnClick="btnSiguiente_Click"><span style="position:relative; top:3px !important;"><img src="Images/flechaTbl.png" style="width:15px;" TabIndex="2"/></span>&nbsp;&nbsp;Siguiente</asp:LinkButton>                            
                            </div>
                            <br />
                            <div id="divCorreoExiste" style="display:none">
                                <div style="display:flex; flex-wrap:wrap;">
                                <div style="width:10%; padding-right:5px; padding-top:5px;">
                                    <img src="Images/iconsEmail.png" />
                                </div>
                                    <input type="text" class="form-control" id="txtEmail" style="width:90%;" readonly="readonly" placeholder="e-mail destino" required="" runat="server" autocomplete="off"  onKeypress="return Enter(this,event)" tabindex="3" />
                                </div>
                               
                                <div class="btnInicioSesion">
                                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnEnviaCorreo" autopostback="true" TabIndex="4" OnClick="btnEnviaCorreo_Click"><span style="position:relative; top:3px !important;"><img src="Images/enviado.png" /></span>&nbsp;&nbsp;Enviar código de recuperación</asp:LinkButton>                            
                                </div>
                            </div>                            
                            <div id="divFaltaCorreo" style="display:none">
                                <h1 style="color:red;">No se ha asignado un correo electrónico a este usuario.</h1>
                            </div>
                        </div>
                        <div id="frmOlvidaCodigo" style="display:none;">
                            <div style="display:flex; flex-wrap:wrap;">
                                <div style="width:10%; padding-right:5px; padding-top:2px;"><img src="Images/iconCon.png" /></div>
                                <asp:TextBox class="form-control" id="txtCodVerifi"  style="width:90%;" TabIndex="5" placeholder="Digite código de verificación" required="" runat="server" autocomplete="off" onKeypress="return Enter(this,event)"></asp:TextBox>
                                <asp:HiddenField ID="hddCodigoVerifi" runat="server" />
                            </div>
                            <div class="btnInicioSesion">
                                <asp:LinkButton class="btn btn-default" runat="server" ID="btnVerificaCod" TabIndex="6" autopostback="true" OnClick="btnVerificaCod_Click"><span style="position:relative; top:3px !important;"><img src="Images/verifica.png" /></span>&nbsp;&nbsp;Verificar código de recuperación</asp:LinkButton>                            
                            </div>
                        </div>
                        <div id="frmOlvidaFinal" style="display:none;">
                            <div style="display:flex; flex-wrap:wrap;">
                                <div style="width:10%; padding-right:5px; padding-top:2px;"><img src="Images/iconCon.png"/></div>                                
                                <input type="password" class="form-control" id="txtNuevaContra" style="width:90%;" tabindex="7" placeholder="Digite nueva contraseña" required="" runat="server" autocomplete="off" onKeypress="return Enter(this,event)" />
                            </div>
                            <div style="display:flex; flex-wrap:wrap;">
                                <div style="width:10%; padding-right:5px; padding-top:2px;"><img src="Images/iconCon.png"/></div>
                                <input type="password" class="form-control" id="txtVerficaContra" style="width:90%;" tabindex="8" placeholder="Verifique nueva contraseña" required="" runat="server" autocomplete="off" onKeypress="return Enter(this,event)" />                                
                            </div>
                            <div style="display:flex; flex-wrap:wrap;">
                                <div style="width:10%; padding-right:5px; padding-top:2px;"><img src="Images/iconCon.png"/></div>                                
                                <input maxlength="6" type="password" class="form-control" id="txtNuevoToken" style="width:90%;" tabindex="9" placeholder="Digite nuevo token" required="" runat="server" autocomplete="off" onKeypress="return Enter(this,event)" />
                            </div>
                            <div style="display:flex; flex-wrap:wrap;">
                                <div style="width:10%; padding-right:5px; padding-top:2px;"><img src="Images/iconCon.png"/></div>
                                <input maxlength="6" type="password" class="form-control" id="txtVerificaToken" style="width:90%;" tabindex="10" placeholder="Verifique nuevo token" required="" runat="server" autocomplete="off" onKeypress="return Enter(this,event)" />                                
                            </div>
                            <div class="btnInicioSesion">
                                <asp:LinkButton class="btn btn-default" runat="server" ID="btnGuardaNuevaContra" TabIndex="11" autopostback="true" OnClick="btnGuardaNuevaContra_Click"><span style="position:relative; top:3px !important;"><img src="Images/iconLog.png" /></span>&nbsp;&nbsp;Guardar datos</asp:LinkButton>                            
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                        <div class="separator">
                            <div>
                                <h1>Gopett Online</h1>
                            </div>
                        </div>
                    </form>
                </section>
            </div>
        </div>
    </div>
    <div class="logoLogin">
            Desarrollado por -  <a href="https://consultorestecnologicos.com.co/"><img src="Images/ByCtec.png" style="width:80px" /></a>
    </div>
    <script src="WebComponents/js/core/jquery.3.2.1.min.js"></script>
    <script src="WebComponents/js/core/bootstrap.min.js"></script>
    <script src="WebComponents/Canvas/particles.js"></script>
    <script src="WebComponents/Canvas/app.js"></script>
    
     <!-- DataTables jQuery -->
    <script src="WebComponents/DataTables-1.10.16/js/jquery.dataTables.min.js"></script>

    <!-- Bootstrap -->
    <script src="WebComponents/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="WebComponents/DataTables-1.10.16/js/dataTables.bootstrap.min.js"></script>

    <script src="WebComponents/DataTables-1.10.16/js/scriptsDataTable.js"></script>

    <!-- NProgress -->
    <script src="WebComponents/nprogress/nprogress.js"></script>

    <!-- Bootstrap datepicker -->
    <script src="WebComponents/moment/min/moment.min.js"></script>
    <script src="WebComponents/moment/locale/es.js"></script>
    <script src="WebComponents/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js"></script>
</body>
</html>
