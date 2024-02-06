<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IntegraVSGo.aspx.cs" Inherits="FETU.IntegraVSGo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
    <link href="WebComponents/Select2/select2.min.css" rel="stylesheet" />
    <script src="WebComponents/Select2/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#<%= cmbTerminal.ClientID %>').select2();
        });
    </script>
    <script type="text/javascript">

        function mayus(e) {
            e.value = e.value.toUpperCase();
        }

        function validarCharts(campo) {
            var string = campo.value;
            string = string.replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '');
            campo.value = string;
        };
    </script>
    <style type="text/css">
        .spinner-container {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(255, 255, 255, 0.8);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 9999;
        }

        .spinner {
            border: 4px solid rgba(255, 255, 255, 0.3);
            border-top: 4px solid #6871C8;
            border-radius: 50%;
            width: 40px;
            height: 40px;
            animation: spin 1s linear infinite;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .modalCenter {
            margin-top: 5%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">
    <div class="card-header text-center">
        <h5><b>COMPARATIVO INTEGRA VS GOPETT ONLINE</b></h5>
    </div>
    <!-- Principal -->
    <div class="card-body">
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group">
                            <span class="placeholder">Fecha inicial (*)</span>
                        </div>
                        <div id="dtpFecIni" class="input-group date" data-provide="datepicker">
                            <asp:TextBox ID="txtFecIni" Style="text-transform: uppercase; pointer-events: none"
                                class="form-control form-control-sm input-border-bottom" runat="server">
                            </asp:TextBox>
                            <div id="calendar1" runat="server" class="input-group-addon form-control-sm" style="border: 1px solid #b3b0b0;">
                                <span class="la flaticon-calendar"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group">
                            <span class="placeholder">Fecha final (*)</span>
                        </div>
                        <div id="dtpFecFin" class="input-group date" data-provide="datepicker">
                            <asp:TextBox ID="txtFecFin" Style="text-transform: uppercase; pointer-events: none"
                                class="form-control form-control-sm input-border-bottom" runat="server">
                            </asp:TextBox>
                            <div id="calendar2" runat="server" class="input-group-addon form-control-sm" style="border: 1px solid #b3b0b0;">
                                <span class="la flaticon-calendar"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group">
                            <span class="placeholder">Terminal de transporte (*)</span>
                        </div>
                        <asp:DropDownList ID="cmbTerminal" class="form-control form-control-sm" runat="server" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="cmbTerminal_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-12" id="Sedes" runat="server">
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group">
                            <span class="placeholder">Sede</span>
                        </div>
                        <asp:DropDownList ID="cmbSede" class="form-control form-control-sm" runat="server" TabIndex="2"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Principal -->
    <div class="row" style="padding-top: 10px;">
        <div class="center">
            <asp:LinkButton class="btn btn-primary radius" runat="server" ID="btnBuscar" CausesValidation="true" title="Buscar" OnClick="btnBuscar_Click"><i class="fa fa-search"></i></asp:LinkButton>
            <asp:LinkButton class="btn btn-primary radius" runat="server" ID="btnLimpiar" CausesValidation="false" title="Limpiar" OnClick="btnLimpiar_Click"><i class="fa fa-trash"></i></asp:LinkButton>
            <asp:LinkButton class="btn btn-primary radius" runat="server" ID="btnSalir" CausesValidation="false" title="Salir" OnClick="btnSalir_Click"><i class="fa fa-reply"></i></asp:LinkButton>
        </div>
    </div>
    <br />
    <br />
    <!-- Tablas -->
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12 col-12">
            <div class="card-header text-center" id="tituloIntegra" runat="server" visible="false">
                <h6><b>INTEGRA</b></h6>
            </div>
            <div id="ComparativoIntegra" runat="server" visible="false" class="row" style="padding-top: 20px; display: flex; justify-content: center">
                <div class="" style="overflow-x: auto;">
                    <asp:GridView ID="grdIntegra" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="DSDES" HeaderText="Nombre Sede" />
                            <asp:BoundField DataField="TOTAL" HeaderText="Cantidad" />
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="estTabla" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12 col-12">
            <div class="card-header text-center" id="tituloFetu" runat="server" visible="false">
                <h6><b>GOPETT ONLINE</b></h6>
            </div>
            <div id="ComparativoFetu" runat="server" visible="false" class="row" style="padding-top: 20px; display: flex; justify-content: center">
                <div class="" style="overflow-x: auto;">
                    <asp:GridView ID="grdFetu" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="DSDES" HeaderText="Nombre Sede" />
                            <asp:BoundField DataField="TOTAL" HeaderText="Cantidad" />
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="estTabla2" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenedor3" runat="server">
    <script type="text/javascript">
        $("#dtpFecIni").datetimepicker({
            format: 'MMM DD/YYYY',
            locale: 'es'
        });

        $("#dtpFecFin").datetimepicker({
            format: 'MMM DD/YYYY',
            locale: 'es'
        });
    </script>
</asp:Content>
