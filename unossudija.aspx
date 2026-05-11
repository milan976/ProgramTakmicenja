<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" 
    CodeBehind="unossudija.aspx.cs" Inherits="ProgramTakmicenja.unossudija" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .table-header {
        background-color: #f8f9fa;
        font-weight: bold;
    }
    
    .table-footer {
        background-color: #f1f1f1;
    }
    
    .table-bordered {
        border: 1px solid #dee2e6;
    }
    
    .table-bordered th, 
    .table-bordered td {
        border: 1px solid #dee2e6;
        padding: 8px;
        vertical-align: middle;
    }
    
    .btn-sm {
        padding: 0.25rem 0.5rem;
        font-size: 0.875rem;
    }
    
    .required-field::after {
        content: " *";
        color: red;
    }
    
    .toast-notification {
        position: fixed;
        top: 20px;
        right: 20px;
        z-index: 1000;
        min-width: 250px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="toastContainer" class="toast-notification"></div>
    
    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card text-bg-light w-auto mb-5" style="width: 30rem;">
                    <div class="card-body">
                        <!-- Zaglavlje kartice -->
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100" src="imgs/user.png" />
                                    <h4>
                                        <asp:Label ID="lblNaslov" runat="server" Text="Унос судија"></asp:Label>
                                    </h4>
                                    <span>Статус налога - </span>
                                    <asp:Label class="badge rounded-pill text-bg-info" ID="lblStatus" runat="server" Text="Ваш статус"></asp:Label>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>

                        <!-- Osnovni podaci -->
                        <div class="row">
                            <div class="col-md-6">
                                <label class="required-field">Име и презиме</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtImePrezime" runat="server" 
                                    placeholder="Име и презиме" required="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvImePrezime" runat="server" 
                                    ControlToValidate="txtImePrezime" ErrorMessage="Обавезно поље" 
                                    Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label>Датум рођења</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtDatumRodjenja" runat="server" 
                                    placeholder="дд.мм.гггг"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label class="required-field">Број телефона</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtBrojTelefona" runat="server" 
                                    placeholder="Број телефона" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvBrojTelefona" runat="server" 
                                    ControlToValidate="txtBrojTelefona" ErrorMessage="Обавезно поље" 
                                    Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label class="required-field">Емаил адреса</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtEmail" runat="server" 
                                    placeholder="Емаил адреса" TextMode="Email" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                                    ControlToValidate="txtEmail" ErrorMessage="Обавезно поље" 
                                    Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                                    ControlToValidate="txtEmail" ErrorMessage="Неисправан формат емаила"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                    Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <!-- Dodatni podaci -->
                        <div class="row">
                            <div class="col-md-6">
                                <label>Град/Место</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtGrad" runat="server" placeholder="Град/Место"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Ватрогасни Савез</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtVatrogasniSavez" runat="server" placeholder="Ватрогасни Савез"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>ДВД друштво</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtDvd" runat="server" placeholder="ДВД друштво"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Број легитимације</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtBrojLegitimacije" runat="server" placeholder="Број легитимације"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>Судија Од</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtSudijaOd" runat="server" placeholder="дд.мм.гггг"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>Важност лиценце</label>
                                <asp:TextBox CssClass="form-control mb-2" ID="txtVaznostLicence" runat="server" placeholder="дд.мм.гггг"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>Врста лиценце</label>
                                <asp:DropDownList CssClass="form-select mb-2" ID="ddlVrstaLicence" runat="server">
                                    <asp:ListItem Enabled="true" Text="Изаберите" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Судија" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Судија инструктор" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- Tabela za takmičenja -->
                        <div class="row mt-3">
                            <div class="col">
                                <h5>Републичка такмичења и улоге</h5>
                                <asp:GridView ID="gvTakmicenja" runat="server" AutoGenerateColumns="False" 
                                    CssClass="table table-bordered" ShowFooter="True" 
                                    OnRowCommand="gvTakmicenja_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Републичка такмичења">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTakmicenje" runat="server" Text='<%# Eval("RepublickaTakmicenja") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNovoTakmicenje" runat="server" 
                                                    CssClass="form-control" placeholder="Унесите такмичење"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Улоге на такмичењима">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUloga" runat="server" Text='<%# Eval("UlogaNaTakmicenju") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNovaUloga" runat="server" 
                                                    CssClass="form-control" placeholder="Унесите улогу"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnObrisi" runat="server" Text="Обриши" 
                                                    CssClass="btn btn-danger btn-sm" CommandName="ObrisiRed" 
                                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                                    OnClientClick="return confirm('Да ли сте сигурни да желите да обришете овај унос?');" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnDodaj" runat="server" Text="Додај" 
                                                    CssClass="btn btn-primary btn-sm" CommandName="DodajRed" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <!-- Dugme za čuvanje -->
                        <div class="d-grid gap-2 col-6 mx-auto mt-3">
                            <asp:Button ID="btnSacuvaj" runat="server" Text="Сачувај промене" 
                                CssClass="btn btn-primary" OnClick="btnSacuvaj_Click" CausesValidation="true" />
                        </div>
                    </div>
                </div>
                <a href="default.aspx"><< Назад на почетну</a><br /><br />
            </div>

            <!-- Desna kartica sa pregledom takmičenja -->
            <div class="col-md-5 mx-auto">
                <div class="card text-bg-info" style="width: 40rem;">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100" src="imgs/whistle.png" />
                                    <h4>Такмичења</h4>
                                    <asp:Label class="badge rounded-pill text-bg-success" ID="lblTakmicenja" runat="server" Text="Суђења и улоге"></asp:Label>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView ID="gvPregledTakmicenja" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-success table-striped" OnRowCommand="gvPregledTakmicenja_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="RepublickaTakmicenja" HeaderText="Републичка такмичења" />
                                        <asp:BoundField DataField="UlogaNaTakmicenju" HeaderText="Улога на такмичењу" />
                                        <asp:TemplateField HeaderText="Акција">
                                            <ItemTemplate>
                                                <asp:Button ID="btnObrisiIzPregleda" runat="server" Text="Обриши" 
                                                    CssClass="btn btn-sm btn-danger" CommandName="ObrisiRed" 
                                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                                    OnClientClick="return confirm('Да ли сте сигурни да желите да обришете овај унос?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showToastMessage(message, type) {
            var toastContainer = document.getElementById('toastContainer');

            var toast = document.createElement('div');
            toast.className = 'alert alert-' + type + ' alert-dismissible fade show';
            toast.innerHTML = `
                <strong>${message}</strong>
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            `;

            toastContainer.appendChild(toast);

            setTimeout(function () {
                if (toast.parentNode) {
                    toast.parentNode.removeChild(toast);
                }
            }, 3000);
        }
    </script>

</asp:Content>