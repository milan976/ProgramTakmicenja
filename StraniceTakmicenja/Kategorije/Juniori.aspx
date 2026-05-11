<%@ Page Title="Unos rezultata - Juniori" Language="C#" MasterPageFile="~/Site1.Master" 
    AutoEventWireup="true" CodeBehind="Juniori.aspx.cs" 
    Inherits="ProgramTakmicenja.StraniceTakmicenja.Kategorije.Juniori" 
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
        <h2 class="text-center">Унос резултата - Јуниори</h2>

        <!-- Filter za ekipe -->
        <div class="row mb-4">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblFilterEkipaJun" runat="server" Text="Изабери екипу: " AssociatedControlID="ddlFilterEkipaJun" CssClass="font-weight-bold"></asp:Label>
                    <asp:DropDownList ID="ddlFilterEkipaJun" runat="server" 
                        CssClass="form-control" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlFilterEkipaJun_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <!-- Statistics Panel -->
<asp:Panel ID="pnlStatisticsJun" runat="server" CssClass="statistics-panel" Visible="true">
    <div class="card mt-0">
        <div class="card-header bg-info text-white">
            <h5>Статистика тима</h5>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblPocetniBodoviJuniori" runat="server" ClientIDMode="Static"
                        CssClass="font-weight-bold" Text="Почетни бодови: "></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblSrednjaVrednostJunioriPrepreke" runat="server" ClientIDMode="Static"
                        CssClass="font-weight-bold text-success" Text="Средња време препреке: "></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblSrednjaVrednostJunioriStafeta" runat="server" ClientIDMode="Static"
                        CssClass="font-weight-bold text-success" Text="Средња вредност штафете: "></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblUkupanPlasmanJuniori" runat="server" ClientIDMode="Static"
                        CssClass="font-weight-bold text-success" Text="Укупан резултат: "></asp:Label>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-6">
                    <asp:Label ID="lblZbirGresakaJunioriPrepreke" runat="server" ClientIDMode="Static"
                        CssClass="font-weight-bold text-info" Text="Збир грешака препреке: 0.00"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblZbirGresakaStafetaJuniori" runat="server" ClientIDMode="Static"
                        CssClass="font-weight-bold text-info" Text="Збир грешака штафете: 0.00"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>

        <!-- Vežba sa preprekama -->
        <div class="card mt-4">
            <div class="card-header bg-primary text-white">
                <h5>Вежба са препрекама</h5>
            </div>
            <div class="card-body">
                <asp:Panel ID="pnlPrepreke" runat="server" CssClass="table-responsive">
                    <table class="table table-bordered table-striped">
                        <thead class="thead-dark">
                            <tr>
                                <th>Негативни бодови</th>
                                <th>Судија 1</th>
                                <th>Судија 2</th>
                                <th>Судија 3</th>
                                <th>Судија 4</th>
                                <th>Судија 5</th>
                                <th>Главни Судија</th>
                            </tr>
                        </thead>
                        <tbody>
                           <!-- Vreme prepreke -->
                            <tr>
                                <td><strong>Трајање вежбе са препрекама (сек)</strong></td>
                                <td><asp:TextBox ID="VremePrepreke_Sudija1" runat="server" ClientIDMode="Static" CssClass="form-control text-center" onkeyup="izracunajSrednjuVrednostJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremePrepreke_Sudija2" runat="server" ClientIDMode="Static" CssClass="form-control text-center" onkeyup="izracunajSrednjuVrednostJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremePrepreke_Sudija3" runat="server" ClientIDMode="Static" CssClass="form-control text-center readonly-field" Enabled="false"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremePrepreke_Sudija4" runat="server" ClientIDMode="Static" CssClass="form-control text-center readonly-field" Enabled="false"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremePrepreke_Sudija5" runat="server" ClientIDMode="Static" CssClass="form-control text-center readonly-field" Enabled="false"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremePrepreke_GlavniSudija" runat="server" ClientIDMode="Static" CssClass="form-control text-center" onkeyup="izracunajSrednjuVrednostJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <!-- Greške -->
                            <tr>
                                <td>Неправилно савладана препрека (×10)</td>
                                <td><asp:TextBox ID="Greska2_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Увијеност црева за пун круг (×5)</td>
                                <td><asp:TextBox ID="Greska3_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Отворена или не спојена спојница (×20)</td>
                                <td><asp:TextBox ID="Greska4_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Погрешно полагање "Ц" вода (×10)</td>
                                <td><asp:TextBox ID="Greska5_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska5_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Заборављена или изгубљена опрема (×5)</td>
                                <td><asp:TextBox ID="Greska6_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska6_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Не активирање звучног сигнала на мензури (×10)</td>
                                <td><asp:TextBox ID="Greska7_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska7_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska7_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska7_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska7_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska7_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Погрешно постављена справа/арматура (×10)</td>
                                <td><asp:TextBox ID="Greska8_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska8_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska8_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska8_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska8_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska8_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Неправилно направљен - везан чвор (×10)</td>
                                <td><asp:TextBox ID="Greska9_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska9_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska9_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska9_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska9_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska9_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Неправилан рад (×10)</td>
                                <td><asp:TextBox ID="Greska10_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska10_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska10_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska10_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska10_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska10_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Разговор у току рада (×10)</td>
                                <td><asp:TextBox ID="Greska11_Prep_Sudija1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska11_Prep_Sudija2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska11_Prep_Sudija3" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska11_Prep_Sudija4" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska11_Prep_Sudija5" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska11_Prep_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaJunioriPrepreke()"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>
        </div>

        <!-- Štafeta -->
        <div class="card mt-4">
            <div class="card-header bg-success text-white">
                <h5>Штафета</h5>
            </div>
            <div class="card-body">
                <asp:Panel ID="pnlStafeta" runat="server" CssClass="table-responsive">
                    <table class="table table-bordered table-striped">
                        <thead class="thead-dark">
                            <tr>
                                <th>Негативни бодови</th>
                                <th>Стартер</th>
                                <th>Мерилац времена 1</th>
                                <th>Мерилац времена 2</th>
                                <th>Стазни судија</th>
                                <th>Главни судија</th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- Vreme stafete -->
                            <tr>
                                <td><strong>Трајање вежбе штафете (сек)</strong></td>
                                <td><asp:TextBox ID="VremeStafete_Starter" runat="server" ClientIDMode="Static" CssClass="form-control text-center readonly-field" Enabled="false"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremeStafete_Merilac1" runat="server" ClientIDMode="Static" CssClass="form-control text-center" onkeyup="izracunajSrednjuVrednostJunioriStafeta()"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremeStafete_Merilac2" runat="server" ClientIDMode="Static" CssClass="form-control text-center" onkeyup="izracunajSrednjuVrednostJunioriStafeta()"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremeStafete_StazniSudija" runat="server" ClientIDMode="Static" CssClass="form-control text-center readonly-field" Enabled="false"></asp:TextBox></td>
                                <td><asp:TextBox ID="VremeStafete_GlavniSudija" runat="server" ClientIDMode="Static" CssClass="form-control text-center" onkeyup="izracunajSrednjuVrednostJunioriStafeta()"></asp:TextBox></td>
                            </tr>
                            <!-- Greške stafete -->
                            <tr>
                                <td>Отворена спојница по случају (×10)</td>
                                <td><asp:TextBox ID="Greska2_Stafeta_Starter" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Stafeta_Merilac1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Stafeta_Merilac2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Stafeta_StazniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska2_Stafeta_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Неправилно извршен задатак (×10)</td>
                                <td><asp:TextBox ID="Greska3_Stafeta_Starter" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Stafeta_Merilac1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Stafeta_Merilac2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Stafeta_StazniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska3_Stafeta_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Неправилно савладана препрека (×10)</td>
                                <td><asp:TextBox ID="Greska4_Stafeta_Starter" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Stafeta_Merilac1" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Stafeta_Merilac2" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Stafeta_StazniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                                <td><asp:TextBox ID="Greska4_Stafeta_GlavniSudija" runat="server" CssClass="form-control text-center" onkeyup="izracunajZbirGresakaStafetaJuniori()"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>
        </div>

       <!-- Dugmad za akciju -->
        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSacuvaj" runat="server" CssClass="btn btn-primary btn-lg" Text="Сачувај резултате" OnClick="btnSacuvaj_Click" />
            </div>
        </div>

        <asp:Label ID="lblPoruka" runat="server" CssClass="text-success d-block mt-3 text-center font-weight-bold"></asp:Label>
    </div>

    <!-- Uključivanje JavaScript fajla -->
    <script src="<%= ResolveUrl("~/Scripts/Juniori.js") %>"></script>
</asp:Content>