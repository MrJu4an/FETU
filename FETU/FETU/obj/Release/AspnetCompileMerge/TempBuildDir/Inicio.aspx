<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="FETU.Inicio" Culture="en-US" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="FETU.Querys" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Timer ID="Timer1" OnTick="Page_Load" runat="server" Interval="300000" Enabled="true" />
    <div class="card-header text-center">
        <h5><b>DASHBOARD FETU</b></h5>
    </div>
    <br />
    <br />
    <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
            <button class="nav-link active" id="nav-tu-pen-tab" data-toggle="tab" data-target="#nav-tu-pen" type="button" role="tab" aria-controls="nav-tu-pen" aria-selected="true">TU Pendientes</button>
            <button class="nav-link" id="nav-ult-tra-tab" data-toggle="tab" data-target="#nav-ult-tra" type="button" role="tab" aria-controls="nav-ult-tra" aria-selected="false">Últimas transacciones</button>
            <button class="nav-link" id="nav-int-go-tab" data-toggle="tab" data-target="#nav-int-go" type="button" role="tab" aria-controls="nav-int-go" aria-selected="false">Integra VS Gopett Online</button>
            <button class="nav-link" id="nav-tu-even-tab" data-toggle="tab" data-target="#nav-tu-even" type="button" role="tab" aria-controls="nav-tu-even" aria-selected="false">Eventos Día de Hoy</button>
        </div>
    </nav>
    <div class="tab-content" id="nav-tabContent">
        <div class="tab-pane fade show active" id="nav-tu-pen" role="tabpanel" aria-labelledby="nav-tu-pen-tab">
            <%--<div class="card-header text-center" id="tituloPendientes" runat="server" visible="false">
                <h6><b>TU Pendientes</b></h6>
            </div>--%>
            <div id="TasasPen" runat="server" visible="false" class="row" style="padding-top: 20px; display: flex; justify-content: center">
                <div class="" style="overflow-x: auto;">
                    <asp:GridView ID="grdTasasPen" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="DSDES" HeaderText="Nombre Terminal" />
                            <asp:BoundField DataField="TPFECTASA" HeaderText="Fecha" />
                            <asp:BoundField DataField="TPTIPFAC" HeaderText="Tipo de factura" />
                            <asp:BoundField DataField="TPCANTIDAD" HeaderText="Cantidad" />
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="estTabla" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                </div>
            </div>
        </div>
        <div class="tab-pane fade" id="nav-ult-tra" role="tabpanel" aria-labelledby="nav-ult-tra-tab">
            <%--<div class="card-header text-center" id="tituloTransacciones" runat="server" visible="false">
                <h6><b>Últimas transacciones</b></h6>
            </div>--%>
            <div id="UltFacturas" runat="server" visible="false" class="row" style="padding-top: 20px; display: flex; justify-content: center">
                <div class="" style="overflow-x: auto;">
                    <asp:GridView ID="grdUltTransacciones" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="DSDES" HeaderText="Nombre Terminal" />
                            <asp:BoundField DataField="TFFECDIAN" HeaderText="Fecha" />
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="estTabla2" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                </div>
            </div>
        </div>
        <div class="tab-pane fade" id="nav-int-go" role="tabpanel" aria-labelledby="nav-int-go-tab">
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                    <br />
                    <br />
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

                            <asp:Label ID="estTablaIntegra" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                    <br />
                    <br />
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

                            <asp:Label ID="estTablaFetu" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade" id="nav-tu-even" role="tabpanel" aria-labelledby="nav-tu-even-tab">
            <br />
            <br />
            <div id="Eventos" runat="server" visible="true" style="display: flex; justify-content: center;">
                <div class="accordion" id="acordionEventos" style="width: 55%;">
                    <% 
                        QryEventos Eventos = new QryEventos();
                        DataTable dt = new DataTable();
                        string fechaActual = DateTime.Now.ToString("MM/dd/yyyy");

                        dt = Eventos.selectSedesEventos(fechaActual, fechaActual);
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
                                    dt2 = Eventos.selectFechasEventos(fechaActual, fechaActual, dr["EVSEDTER"].ToString());
                                    if (dt2 != null)
                                    {
                                        foreach (DataRow dr2 in dt2.Rows)
                                        {
                                            string nomDiv = "fecha_" + contFechas + "_" + dr["EVSEDTER"].ToString();
                                %>
                                <div class="card" style="padding: 1px;">
                                    <div class="card-header" id="heading-Two">
                                        <h2 class="mb-0">
                                            <button class="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#<%=nomDiv %>" aria-expanded="false" aria-controls="<%=nomDiv %>">
                                                <%=dr2["EVFECMON"].ToString() %>
                                            </button>
                                        </h2>
                                    </div>

                                    <div id="<%=nomDiv%>" class="collapse" aria-labelledby="heading-Two" data-parent="acordionFechas">
                                        <div class="card-body">
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">#</th>
                                                        <th scope="col">Hora Evento</th>
                                                        <th scope="col">Pendientes</th>
                                                        <th scope="col">OK</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%
                                                        DataTable dt3 = new DataTable();
                                                        string fechaBusqueda = dr2["EVFECMON"].ToString();
                                                        int contHoras = 1;
                                                        dt3 = Eventos.selectEventos(fechaBusqueda, dr["EVSEDTER"].ToString());
                                                        if (dt3 != null)
                                                        {
                                                            foreach (DataRow dr3 in dt3.Rows)
                                                            {
                                                    %>
                                                    <tr>
                                                        <th scope="col"><%=contHoras %></th>
                                                        <td><%=dr3["EVHORMON"].ToString() %></td>
                                                        <td><%=dr3["EVCANPEN"].ToString() %></td>
                                                        <td><%=dr3["EVCANOK"].ToString() %></td>
                                                    </tr>
                                                    <%
                                                                contHoras++;
                                                            }
                                                        }
                                                    %>
                                                </tbody>
                                            </table>
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
                    %>
                </div>
            </div>
        </div>
    </div>
    <%--<div class="card-body">
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                <div class="card-header text-center" id="tituloPendientes" runat="server" visible="false">
                    <h6><b>TU Pendientes</b></h6>
                </div>
                <div id="TasasPen" runat="server" visible="false" class="row" style="padding-top: 20px; display: flex; justify-content: center">
                    <div class="" style="overflow-x: auto;">
                        <asp:GridView ID="grdTasasPen" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="DSDES" HeaderText="Nombre Terminal" />
                                <asp:BoundField DataField="TPFECTASA" HeaderText="Fecha" />
                                <asp:BoundField DataField="TPTIPFAC" HeaderText="Tipo de factura" />
                                <asp:BoundField DataField="TPCANTIDAD" HeaderText="Cantidad" />
                            </Columns>
                        </asp:GridView>

                        <asp:Label ID="estTabla" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                <div class="card-header text-center" id="tituloTransacciones" runat="server" visible="false">
                    <h6><b>Últimas transacciones</b></h6>
                </div>
                <div id="UltFacturas" runat="server" visible="false" class="row" style="padding-top: 20px; display: flex; justify-content: center">
                    <div class="" style="overflow-x: auto;">
                        <asp:GridView ID="grdUltTransacciones" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="DSDES" HeaderText="Nombre Terminal" />
                                <asp:BoundField DataField="TFFECDIAN" HeaderText="Fecha" />
                            </Columns>
                        </asp:GridView>

                        <asp:Label ID="estTabla2" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="card-header text-center">
            <h6><b>COMPARATIVA INTEGRA</b></h6>
        </div>
        <br />
        <br />
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

                        <asp:Label ID="estTablaIntegra" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
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

                        <asp:Label ID="estTablaFetu" Visible="false" runat="server" Text="No se encontraron registros de facturación."></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenedor3" runat="server">
</asp:Content>
