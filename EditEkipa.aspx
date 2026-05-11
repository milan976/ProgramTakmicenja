<%@ Page Title="Izmena podataka ekipe" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditEkipa.aspx.cs" CodeFile="EditEkipa.aspx.cs" Inherits="ProgramTakmicenja.EditEkipaPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .info-panel {
            background-color: #f8f9fa;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
        }
        .clanovi-table {
            width: 100%;
            margin-bottom: 20px;
        }
        .clanovi-table th {
            background-color: #343a40;
            color: white;
        }
        .table {
        width: 100%;
        margin-bottom: 1rem;
        background-color: transparent;
        border-collapse: collapse;
        }
        .table-header {
            background-color: #f8f9fa;
            font-weight: bold;
        }
        .table-row {
            border-bottom: 1px solid #dee2e6;
        }
        .table-footer {
            background-color: #f8f9fa;
            font-weight: bold;
            border-top: 2px solid #dee2e6;
        }
        .table th {
            background-color: #343a40;
            color: white;
            text-align: center;
        }
        .table-footer td {
            padding: 10px;
        }

        .card {
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        .card-header {
            font-weight: 500;
        }
        .btn-danger {
            background-color: #dc3545;
            border-color: #dc3545;
            margin-left: 10px;
        }

        .btn-danger:hover {
            background-color: #c82333;
            border-color: #bd2130;
        }
        .table-footer {
            background-color: #f8f9fa;
            font-weight: bold;
        }

        .table-footer td {
            padding: 10px;
            border-top: 2px solid #dee2e6;
        }
        .btn-sm {
            padding: 0.25rem 0.5rem;
            font-size: 0.875rem;
            line-height: 1.5;
            border-radius: 0.2rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12 mx-auto">
                <div class="card text-bg-light mb-4">
                    <div class="card-header">
                        <h4 class="text-center">Измена података екипе</h4>
                    </div>
                    <div class="card-body">
                        <asp:HiddenField ID="hfEkipaID" runat="server" />
                        
                        <div class="filter-section">
                            <asp:DropDownList ID="ddlFilterKategorija" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlFilterKategorija_SelectedIndexChanged" CssClass="form-control" Width="350">
                            </asp:DropDownList>
    
                            <asp:DropDownList ID="ddlFilterEkipa" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlFilterEkipa_SelectedIndexChanged" CssClass="form-control" Width="350">
                            </asp:DropDownList>
                        </div>

                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <!-- Osnovni podaci ekipe -->
                        <div class="info-panel">
                            <h5>Основни подаци екипе</h5>
                            <div class="form-group mb-3">
                                <label>Датум уноса:</label>
                                <asp:TextBox ID="txtDatumUnosa" CssClass="form-control" runat="server" Width="300"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>DVD/PVJ:</label>
                                <asp:TextBox ID="txtNazivEkipe" CssClass="form-control" runat="server" Width="400"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>ОВС:</label>
                                <asp:TextBox ID="txtOVS" CssClass="form-control" runat="server" Width="300"></asp:TextBox>
                            </div>
                            
                        </div>

                        <!-- Odgovorna lica -->
                        <div class="info-panel">
                            <h5>Одговорна лица</h5>
                            <div class="form-group mb-3">
                                <label>Тренер екипе:</label>
                                <asp:TextBox ID="txtTrener" CssClass="form-control" runat="server" Width="300"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>Вођа екипе:</label>
                                <asp:TextBox ID="txtVodjaEkipe" CssClass="form-control" runat="server" Width="300"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Tabela clanova -->
                        <div class="info-panel">
                            <h5>Чланови екипе</h5>
                            <asp:GridView ID="gvClanovi" runat="server" AutoGenerateColumns="False" 
                                CssClass="table table-bordered" GridLines="None"
                                OnRowEditing="gvClanovi_RowEditing" OnRowUpdating="gvClanovi_RowUpdating"
                                OnRowCancelingEdit="gvClanovi_RowCancelingEdit" OnRowDataBound="gvClanovi_RowDataBound"
                                DataKeyNames="ClanID" ShowFooter="true">

                                <HeaderStyle CssClass="table-header" />
                                <RowStyle CssClass="table-row" />
                                <FooterStyle CssClass="table-footer" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Ред. број">
                                        <ItemTemplate><%# Eval("RedniBroj") %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditRedniBroj" runat="server" Text='<%# Bind("RedniBroj") %>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Улога">
                                    <ItemTemplate><%# Eval("Uloga") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlEditUloga" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Изаберите улогу" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Командир" Value="Командир"></asp:ListItem>
                                            <asp:ListItem Text="Курир" Value="Курир"></asp:ListItem>
                                            <asp:ListItem Text="Моториста" Value="Моториста"></asp:ListItem>
                                            <asp:ListItem Text="Навални 1" Value="Навални 1"></asp:ListItem>
                                            <asp:ListItem Text="Навални 2" Value="Навални 2"></asp:ListItem>
                                            <asp:ListItem Text="Цевни 1" Value="Цевни 1"></asp:ListItem>
                                            <asp:ListItem Text="Цевни 2" Value="Цевни 2"></asp:ListItem>
                                            <asp:ListItem Text="Водни 1" Value="Водни 1"></asp:ListItem>
                                            <asp:ListItem Text="Водни 2" Value="Водни 2"></asp:ListItem>
                                            <asp:ListItem Text="Резерва" Value="Резерва"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <div style="text-align: center; font-weight: bold;">
                                                    <asp:Label ID="lblFooterCalculations" runat="server"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Име и презиме">
                                    <ItemTemplate><%# Eval("ImePrezime") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditImePrezime" runat="server" Text='<%# Bind("ImePrezime") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Датум рођења">
                                    <ItemTemplate><%# Eval("DatumRodjenja", "{0:dd.MM.yyyy}") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditDatumRodjenja" runat="server" Text='<%# Bind("DatumRodjenja", "{0:dd.MM.yyyy}") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Године">
                                    <ItemTemplate><%# Eval("Godine") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="true" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-primary" />
                            </Columns>
                        </asp:GridView>
                            
                            

                        <!-- Dugmad za akcije -->
                        <div class="form-group text-center">
                            <asp:Button ID="btnSacuvajIzmene" runat="server" Text="Сачувај измене" CssClass="btn btn-primary" OnClick="btnSacuvajIzmene_Click" />
                            <asp:Button ID="btnOdustani" runat="server" Text="Одустани" CssClass="btn btn-secondary" OnClick="btnOdustani_Click" />
                            <asp:Button ID="btnObrisiEkipu" runat="server" Text="Обриши екипу" 
                                       CssClass="btn btn-danger" OnClick="btnObrisiEkipu_Click" 
                                       OnClientClick="return confirm('Да ли сте сигурни да желите да обришете целу екипу? Ова акција је неповратна!');" />
                        </div>
                        
                        <asp:Label ID="lblPoruka" runat="server" CssClass="text-success"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>