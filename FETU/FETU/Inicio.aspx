<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="FETU.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">
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
    </div>
    <div id="IntGo" class="tab-content">
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
