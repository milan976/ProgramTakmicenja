<%@ Page Title="Unos rezultata - Podmlatka" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Podmladak.aspx.cs" Inherits="ProgramTakmicenja.StraniceTakmicenja.Kategorije.Podmladak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <style type="text/css">
        .readonly-field {
            background-color: #f8f9fa;
            cursor: not-allowed;
            border: 1px solid #ced4da;
            color: #495057;
        }
        
        .statistics-panel {
            background-color: #f8f9fa;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
        }
        
        @media (max-width: 768px) {
            .table-responsive {
                font-size: 14px;
            }
            .input-field {
                width: 100% !important;
            }
        }
    </style>

    <div class="container mt-4">
        <h2 class="text-center mb-4">Унос резултата - Подмладтка</h2>

        <!-- Filter za ekipe -->
        <div class="row mb-4">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblFilterEkipa" runat="server" Text="Изабери екипу: " AssociatedControlID="ddlFilterEkipa" CssClass="font-weight-bold"></asp:Label>
                    <asp:DropDownList ID="ddlFilterEkipa" runat="server" 
                        CssClass="form-control" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlFilterEkipa_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <!-- Statistics Panel -->
        <asp:Panel ID="pnlStatistics" runat="server" CssClass="statistics-panel" Visible="true">
            <div class="card mt-0">
                <div class="card-header bg-info text-white">
                    <h5>Статистика тима</h5>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblPocetniBodovi" runat="server"
                                ClientIDMode="Static"
                                CssClass="font-weight-bold" Text="Почетни бодови: -"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblSrednjaVrednost" runat="server"
                                ClientIDMode="Static"
                                CssClass="font-weight-bold" Text="Средња вредност: -"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblZbirGresaka" runat="server"
                                ClientIDMode="Static"
                                CssClass="font-weight-bold text-danger" Text="Збир грешака брентача: 0"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblZbirGresakaStafeta" runat="server"
                                ClientIDMode="Static"
                                CssClass="font-weight-bold text-info" Text="Збир грешака стафета: 0"></asp:Label>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblUkupanPlasman" runat="server"
                                ClientIDMode="Static"
                                CssClass="font-weight-bold h5" Text="Укупан резултат: 0"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Tabela za Brentace -->
        <div class="row mt-4">
            <div class="col-md-12">
                <h4>Вежба са брентачама</h4>
                <asp:Panel ID="pnlBrentace" runat="server" CssClass="table-responsive">
                    <table class="table table-bordered table-striped">
                        <thead class="thead-dark">
                            <tr>
                                <th scope="col">Негативни бодови</th>
                                <th scope="col">Судија стартер</th>
                                <th scope="col">Судија мерилац времена</th>
                                <th scope="col">Главни судија</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Трајање вежбе са брентачама у 0.00 сек</td>
                                <td>
                                    <asp:TextBox ID="VremeBrentaca_SST" runat="server" 
                                        ClientIDMode="Static" 
                                        CssClass="form-control text-center input-field"
                                        AutoPostBack="true"
                                        OnTextChanged="IzracunajSrednjuVrednost">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="VremeBrentaca_SMV" runat="server" 
                                        ClientIDMode="Static" 
                                        CssClass="form-control text-center input-field"
                                        AutoPostBack="true"
                                        OnTextChanged="IzracunajSrednjuVrednost">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="VremeBrentaca_GS" runat="server" 
                                        ClientIDMode="Static" 
                                        CssClass="form-control text-center input-field"
                                        AutoPostBack="true"
                                        OnTextChanged="IzracunajSrednjuVrednost">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Прерани старт</td>
                                <td><asp:TextBox ID="Greska2_Brentaca_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Brentaca_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Brentaca_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Обарање туђе мете</td>
                                <td><asp:TextBox ID="Greska3_Brentaca_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Brentaca_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Brentaca_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Гажење или прекорачење линије напада</td>
                                <td><asp:TextBox ID="Greska4_Brentaca_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Brentaca_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Brentaca_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Разговор за време рада</td>
                                <td><asp:TextBox ID="Greska5_Brentaca_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_Brentaca_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_Brentaca_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Пумпање воде пре команде "Воду дај"</td>
                                <td><asp:TextBox ID="Greska6_Brentaca_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_Brentaca_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_Brentaca_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Неправилно преношење кофе са водом</td>
                                <td><asp:TextBox ID="Greska7_Brentaca_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska7_Brentaca_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska7_Brentaca_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Неправилан рад</td>
                                <td><asp:TextBox ID="Greska8_Brentaca_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska8_Brentaca_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska8_Brentaca_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Превртање брентаче</td>
                                <td><asp:TextBox ID="Greska9_Brentaca_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska9_Brentaca_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska9_Brentaca_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-12">
                <h4 class="mb-3">Штафетно преношење воде</h4>
                <asp:Panel ID="pnlPrenosenjeVode" runat="server" CssClass="table-responsive">
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th scope="col">Негативни бодови</th>
                                <th scope="col">Судија Стартер</th>
                                <th scope="col">Судија мер. времена</th>
                                <th scope="col">Главни Судија</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>За сваки започети cm насуте воде</td>
                                <td><asp:TextBox ID="Greska1_PV_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska1_PV_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska1_PV_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Полазак пре примљене кофе</td>
                                <td><asp:TextBox ID="Greska2_PV_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_PV_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_PV_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Погрешно кретање стазом</td>
                                <td><asp:TextBox ID="Greska3_PV_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_PV_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_PV_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Неправилно заобилажење бурета</td>
                                <td><asp:TextBox ID="Greska4_PV_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_PV_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_PV_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Учествовање мимо распореда</td>
                                <td><asp:TextBox ID="Greska5_PV_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_PV_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_PV_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Такмичар по предаји кофе није стао иза...</td>
                                <td><asp:TextBox ID="Greska6_PV_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_PV_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_PV_GS" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSacuvaj" runat="server" CssClass="btn btn-primary mt-3" Text="Sačuvaj" OnClick="btnSacuvaj_Click" />
                <asp:Label ID="lblPoruka" runat="server" CssClass="text-success d-block mt-2"></asp:Label>
            </div>
        </div>
    </div>

    <!-- Dodaj referencu na eksterni JavaScript fajl -->
    <script src="<%= ResolveUrl("~/Scripts/Podmladak.js") %>"></script>
</asp:Content>