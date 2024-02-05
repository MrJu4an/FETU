<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FETU.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>FETU</title>
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link rel="icon" href="Images/iconoPrincipal.jpg" type="image/x-icon" />

    <!-- Bootstrap -->
    <link href="WebComponents/css/bootstrap.min.css" rel="stylesheet" />
    <link href="WebComponents/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <script src="WebComponents/js/plugin/webfont/webfont.min.js"></script>
    <!-- NProgress -->
    <link href="WebComponents/nprogress/nprogress.css" rel="stylesheet" />

    <!-- Sweetalert2 -->
    <script src="WebComponents/sweetAlert2/sweetalert2.all.js"></script>
    <script src="WebComponents/sweetAlert2/sweetalert2.all.min.js"></script>

    <!-- Custom Theme Style -->
    <link href="WebComponents/css/custom.css" rel="stylesheet" />

    <link href="WebComponents/Canvas/style.css" rel="stylesheet" />

    <script type="text/javascript">
        function Enter(field, event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById("btnInicioSesion").click()
            };
        };
    </script>
    <%--<script type="text/javascript">
        setTimeout(function () { location.href = "https://efactonline.fitcloud.com.co/gopettonline/" }, 100);
    </script>--%>
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
    </script>
</head>

<body class="login" id="particles-js">
    <div>
        <div class="login_wrapper">
            <div class="animate form login_form">
                <section class="login_content">
                    <form runat="server">
                        <h1>Inicio de Sesión</h1>
                        <div style="display: flex; flex-wrap: wrap;">
                            <div style="width: 10%; padding-right: 5px; padding-top: 2px;">
                                <img src="Images/iconUsu.png" />
                            </div>
                            <input type="text" class="form-control" id="txtUsu" style="width: 90%;" placeholder="Usuario" required="" runat="server" autocomplete="off" onkeypress="return Enter(this,event)" />
                        </div>
                        <div style="display: flex; flex-wrap: wrap;">
                            <div style="width: 10%; padding-right: 5px; padding-top: 5px;">
                                <img src="Images/iconCon.png" />
                            </div>
                            <input type="password" class="form-control" id="txtPss" style="width: 90%;" placeholder="Contraseña" required="" runat="server" autocomplete="off" onkeypress="return Enter(this,event)" />
                        </div>
                        <div class="btnInicioSesion">
                            <asp:LinkButton class="btn btn-default" runat="server" ID="btnInicioSesion" autopostback="true" OnClick="btnInicioSesion_Click"><span style="position:relative; top:3px !important;"><img src="Images/iconLog.png" /></span>&nbsp;&nbsp;Iniciar Sesión</asp:LinkButton>
                        </div>
                        <br />
                        <asp:HyperLink ID="hipperOlviaContra" href="OlvidaCredenciales.aspx" runat="server" TabIndex="6"><b>¿Olvidó su contraseña?</b></asp:HyperLink>
                        <div class="clearfix"></div>
                        <br />
                        <div class="separator">
                            <div>
                                <h1>FETU</h1>
                            </div>
                        </div>
                    </form>
                </section>
            </div>
        </div>
    </div>
    <div class="logoLogin">
        Desarrollado por - <a href="https://consultorestecnologicos.net/">
            <img src="Images/ByCtec.png" style="width: 80px" /></a>
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
