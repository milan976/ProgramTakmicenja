<%@ Page Title="Unos rezultata - Dobrovoljci" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Dobrovoljci.aspx.cs" Inherits="ProgramTakmicenja.StraniceTakmicenja.Kategorije.Dobrovoljci" 
  ClientIDMode="Static" %>

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
    <div class="container mt-12">
    <h2 class="text-center">Унос резултата - ДВД</h2>

    <!-- Filter za ekipe -->
    <div class="row mb-4">
        <div class="col-md-6">
            <div class="form-group">
                <asp:Label ID="lblFilterEkipaDvd" runat="server" Text="Изабери екипу: " AssociatedControlID="ddlFilterEkipaDvd" CssClass="font-weight-bold"></asp:Label>
                <asp:DropDownList ID="ddlFilterEkipaDvd" runat="server" ClientIDMode="Static"
                    CssClass="form-control" 
                    AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlFilterEkipaDvd_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
    </div>
                <!-- Statistics Panel -->
    <asp:Panel ID="pnlStatisticsDobrovoljci" runat="server" ClientIDMode="Static"
CssClass="statistics-panel" Visible="true">
        <div class="card mt-0">
            <div class="card-header bg-info text-white">
                <h5>Статистика тима</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblPocetniBodoviDobrovoljci" runat="server" ClientIDMode="Static"
                            CssClass="font-weight-bold" Text="Почетни бодови: "></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblSrednjaVrednostDobrovoljciMvp" runat="server" ClientIDMode="Static"
                            CssClass="font-weight-bold text-success" Text="Средња време препреке: "></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblSrednjaVrednostDobrovoljciStafeta" runat="server" ClientIDMode="Static"
                            CssClass="font-weight-bold text-success" Text="Средња вредност штафете: "></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblUkupanPlasmanDobrovoljci" runat="server" ClientIDMode="Static"
                            CssClass="font-weight-bold text-success" Text="Укупан резултат: "></asp:Label>
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-md-6">
                        <asp:Label ID="lblZbirGresakaDobrovoljciMvp" runat="server" ClientIDMode="Static"
                            CssClass="font-weight-bold text-info" Text="Збир грешака препреке: 0.00"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblZbirGresakaDobrovoljciStafeta" runat="server" ClientIDMode="Static"
                            CssClass="font-weight-bold text-info" Text="Збир грешака штафете: 0.00"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
        <div class="card mt-4">
            <div class="card-header bg-primary text-white">
                <h5>Вежба са препрекама</h5>
            </div>
            <div class="card-body"> 
                <asp:Panel ID="pnlUnosVezba" runat="server" CssClass="table-responsive mt-3">
                     <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>Негативни бодови</th>
                        <th>Судија 1</th>
                        <th>Судија 2</th>
                        <th>Судија 3</th>
                        <th>Главни Судија</th>
                        
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Трајање вежбе са МВП у 0.00 сек</td>
                        <td><asp:TextBox ID="VremeMVP_Sudija1" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-75"></asp:TextBox></td>
                        <td><asp:TextBox ID="VremeMVP_Sudija2" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-75"></asp:TextBox></td>
                        <td><asp:TextBox ID="VremeMVP_Sudija3" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-75" Enabled="false"></asp:TextBox></td>
                        <td><asp:TextBox ID="VremeMVP_GlavniSudija" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-75"></asp:TextBox></td>
                        
                    </tr>
                    <tr>
                        <td>Превремени старт</td>
                        <td><asp:TextBox ID="Greska2_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska2_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska2_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska2_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        
                    </tr>
                    <tr>
                        <td>Пуштање спојки да падну</td>
                        <td><asp:TextBox ID="Greska3_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska3_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska3_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska3_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                       
                    </tr>
                    <tr>
                        <td>Погрешно одложена резервна црева</td>
                        <td><asp:TextBox ID="Greska4_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska4_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska4_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska4_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Остављена или изгубљена опрема</td>
                        <td><asp:TextBox ID="Greska5_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska5_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska5_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska5_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Неправилно положена потисна црева</td>
                        <td><asp:TextBox ID="Greska6_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska6_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska6_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska6_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Вучење развијеног црева по тлу</td>
                        <td><asp:TextBox ID="Greska7_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska7_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska7_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska7_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        
                    </tr>
                    <tr>
                        <td>Погрешно постављено уже повратног вентила</td>
                        <td><asp:TextBox ID="Greska8_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska8_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska8_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska8_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                       
                    </tr>
                    <tr>
                        <td>Погрешан коначан положај такмичара</td>
                        <td><asp:TextBox ID="Greska9_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska9_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska9_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska9_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Погрешан рад чланова екипе</td>
                        <td><asp:TextBox ID="Greska10_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska10_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska10_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska10_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Погрешна или неразумљива команда</td>
                        <td><asp:TextBox ID="Greska11_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska11_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska11_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska11_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Непрописно отворени потисни вентили</td>
                        <td><asp:TextBox ID="Greska12_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska12_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska12_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska12_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Разговор за време рада</td>
                        <td><asp:TextBox ID="Greska13_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska13_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska13_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska13_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Нефункционално постављено уже усисног вода</td>
                        <td><asp:TextBox ID="Greska14_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska14_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska14_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska14_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Отворен пар спојки</td>
                        <td><asp:TextBox ID="Greska15_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska15_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska15_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska15_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td>Полазак В/Ц групе пре команде готово</td>
                        <td><asp:TextBox ID="Greska16_MVP_Sudija1" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska16_MVP_Sudija2" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska16_MVP_Sudija3" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        <td><asp:TextBox ID="Greska16_MVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>                       
                    </tr>
                </tbody>
            </table>
                </asp:Panel>
            <div class="card mt-4">
                <div class="card-header bg-primary text-white">
                    <h5>Штафетна трка</h5>
                </div>
                <div class="card-body"> 
                <asp:Panel ID="Panel1" runat="server" CssClass="table-responsive mt-3">
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th>Негативни бодови</th>
                                <th>Судија Стартер</th>
                                <th>Судија мер. времена</th>
                                <th>Судија Стазни</th>
                                <th>Главни Судија</th>
                        
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Време трајања штафетне трке у 0.00 сек</td>
                                <td><asp:TextBox ID="VremeStafetaMVP_SST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-75"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremeStafetaMVP_SMV" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-75"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremeStafetaMVP_ST" runat="server" ClientIDMode="Static" CssClass="form-control text-xl-center w-25" Enabled="false"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremeStafetaMVP_GlavniSudija" ClientIDMode="Static" runat="server" CssClass="form-control text-xl-center w-75"></asp:TextBox></td>
                        
                            </tr>
                            <tr>
                                <td>Превремени старт</td>
                                <td><asp:TextBox ID="Greska2_StafetaMVP_SST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_StafetaMVP_SMV" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_StafetaMVP_ST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_StafetaMVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                        
                            </tr>
                            <tr>
                                <td>Погрешна примопредаја млазнице</td>
                                <td><asp:TextBox ID="Greska3_StafetaMVP_SST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_StafetaMVP_SMV" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_StafetaMVP_ST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_StafetaMVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                       
                            </tr>
                            <tr>
                                <td>Изгубљена лична опрема</td>
                                <td><asp:TextBox ID="Greska4_StafetaMVP_SST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_StafetaMVP_SMV" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_StafetaMVP_ST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_StafetaMVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Неправилно савладана препрека</td>
                                <td><asp:TextBox ID="Greska5_StafetaMVP_SST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_StafetaMVP_SMV" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_StafetaMVP_ST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_StafetaMVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Није донета млазница на циљ</td>
                                <td><asp:TextBox ID="Greska6_StafetaMVP_SST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_StafetaMVP_SMV" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_StafetaMVP_ST" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_StafetaMVP_GlavniSudija" runat="server" CssClass="form-control text-xl-center w-25"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                </div>
            </div>
        <asp:Button ID="btnSacuvaj" runat="server" CssClass="btn btn-primary mt-3" Text="Sačuvaj" OnClick="btnSacuvaj_Click" />
        <asp:Label ID="lblPoruka" runat="server" CssClass="text-success d-block mt-2"></asp:Label>
        </div>

    <!-- Uključivanje JavaScript fajla -->
    <script src="<%= ResolveUrl("~/Scripts/Dobrovoljci.js") %>"></script>
</asp:Content>
