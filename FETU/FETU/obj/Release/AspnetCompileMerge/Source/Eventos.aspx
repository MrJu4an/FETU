<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Eventos.aspx.cs" Inherits="FETU.Eventos" Culture="en-US" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="FETU.Querys" %>
<%@ Import Namespace="Microsoft.VisualBasic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
    <link href="WebComponents/Select2/select2.min.css" rel="stylesheet" />
    <script src="WebComponents/Select2/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#<%= cmbTerminal.ClientID %>').select2();
            $('#<%= cmbSede.ClientID %>').select2();
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
        <h5><b>CONSULTA DE EVENTOS</b></h5>
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
    <div id="EventosCaja" runat="server" visible="true" style="display: flex; justify-content: center;">
        <div class="accordion" id="acordionEventos" style="width: 55%;">
            <% 
                QryEventos Eventos = new QryEventos();
                DataTable dt = new DataTable();
                string fechaInicio;
                string fechaFinal;
                string fechaActual = DateTime.Now.ToString("MM/dd/yyyy");

                if (txtFecIni.Text == "")
                {
                    fechaInicio = fechaActual;
                }
                else
                {
                    fechaInicio = txtFecIni.Text;
                }

                if (txtFecFin.Text == "")
                {
                    fechaFinal = fechaActual;
                }
                else
                {
                    fechaFinal = txtFecFin.Text;
                }

                if (cmbTerminal.SelectedIndex != 0 && cmbSede.SelectedIndex != 0)
                {
                    dt = Eventos.selectSedeEventos(fechaInicio, fechaFinal, cmbSede.SelectedValue.ToString());
                }
                else if (cmbTerminal.SelectedIndex != 0 && cmbSede.SelectedIndex == 0)
                {
                    dt = Eventos.selectTerminalEventos(fechaInicio, fechaFinal, cmbTerminal.SelectedValue.ToString());
                }
                else
                {
                    dt = Eventos.selectSedesEventos(fechaInicio, fechaFinal);
                }

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
            %>
            <div class="card" style="padding: 1px;">
                <div class="card-header" id="heading-One">
                    <button class="btn btn-outline-primary btn-lg btn-block text-left" type="button" data-toggle="collapse" data-target="#sede<%=dr["EVSEDTER"].ToString() %>" aria-expanded="false" aria-controls="sede<%=dr["EVSEDTER"].ToString() %>">
                        <%=dr["DSDES"].ToString() %>
                    </button>
                </div>

                <div id="sede<%=dr["EVSEDTER"].ToString() %>" class="collapse" aria-labelledby="heading-One" data-parent="acordionEventos">
                    <div class="card-body">
                        <% 
                            DataTable dt2 = new DataTable();
                            int contFechas = 1;
                            dt2 = Eventos.selectFechasEventos(fechaInicio, fechaFinal, dr["EVSEDTER"].ToString());
                            if (dt2 != null)
                            {
                                foreach (DataRow dr2 in dt2.Rows)
                                {
                                    string nomDivFechas = "fecha_" + contFechas + "_" + dr["EVSEDTER"].ToString();
                        %>
                        <div class="card" style="padding: 1px;">
                            <div class="card-header" id="heading-Two">
                                <h2 class="mb-0">
                                    <button class="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#<%=nomDivFechas %>" aria-expanded="false" aria-controls="<%=nomDivFechas %>">
                                        <%=dr2["EVFECMON"].ToString() %>
                                    </button>
                                </h2>
                            </div>

                            <div id="<%=nomDivFechas%>" class="collapse" aria-labelledby="heading-Two" data-parent="acordionFechas">
                                <div class="card-body">
                                    <%
                                        DataTable dt3 = new DataTable();
                                        int contHoras = 1;
                                        dt3 = Eventos.selectHorasEventos(dr2["EVFECMON"].ToString(), dr2["EVFECMON"].ToString(), dr["EVSEDTER"].ToString());
                                        if (dt3 != null)
                                        {
                                            foreach (DataRow dr3 in dt3.Rows)
                                            {
                                                string nomDivHoras = "hora_" + contHoras + "_" + dr["EVSEDTER"].ToString();
                                    %>
                                    <div class="card" style="padding: 1px;">
                                        <div class="card-header" id="heading-Three">
                                            <h2 class="mb-0">
                                                <button class="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#<%=nomDivHoras %>" aria-expanded="false" aria-controls="<%=nomDivHoras %>">
                                                    <%=dr3["EVHORMON"].ToString() %>
                                                </button>
                                            </h2>
                                        </div>

                                        <div id="<%=nomDivHoras%>" class="collapse" aria-labelledby="heading-Three" data-parent="acordionHoras">
                                            <div class="card-body">
                                                <table class="table">
                                                    <thead>
                                                        <tr>
                                                            <th scope="col">#</th>
                                                            <th scope="col">Fecha tasa</th>
                                                            <th scope="col">Pendientes</th>
                                                            <th scope="col">OK</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <%
                                                            DataTable dt4 = new DataTable();
                                                            string fechaBusqueda = dr2["EVFECMON"].ToString();
                                                            string horaBusqueda = dr3["EVHORMON"].ToString();
                                                            int contEventos = 1;
                                                            dt4 = Eventos.selectEventos(fechaBusqueda, horaBusqueda, dr["EVSEDTER"].ToString());
                                                            if (dt4 != null)
                                                            {
                                                                foreach (DataRow dr4 in dt4.Rows)
                                                                {
                                                        %>
                                                        <tr>
                                                            <th scope="col"><%=contEventos %></th>
                                                            <td><%=dr4["EVFECTU"].ToString()%></td>
                                                            <td><%=dr4["EVCANPEN"].ToString()%></td>
                                                            <td><%=dr4["EVCANOK"].ToString()%></td>
                                                        </tr>
                                                        <%
                                                                    contEventos++;
                                                                }
                                                            }
                                                        %>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <%
                                                    contHoras++;
                                                }
                                            }
                                        %>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%
                                    contFechas++;
                                }
                            }
                        %>
                    </div>
                </div>
            </div>
            <%
                    }
                }
                else
                {
                    Alert("Error", "No se encontraron registros", 3, "Aceptar");
                }
            %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenedor3" runat="server">
    <script type="text/javascript">
        $("#dtpFecIni").datetimepicker({
            format: 'MM/DD/YYYY',
            locale: 'es'
        });

        $("#dtpFecFin").datetimepicker({
            format: 'MM/DD/YYYY',
            locale: 'es'
        });
    </script>
</asp:Content>
