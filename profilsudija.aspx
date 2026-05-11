<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="profilsudija.aspx.cs" Inherits="ProgramTakmicenja.profilsudija" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .gridview-style {
            font-family: "Segoe UI", "Arial", sans-serif;
            font-size: 14px;
            width: 100%;
        }
        .gridview-style th {
            background-color: #198754;
            color: white;
            text-align: center;
        }
        .gridview-style tr:hover {
            background-color: #e8f5e8;
            cursor: pointer;
        }
        .selected-row {
            background-color: #c8e6c9 !important;
            font-weight: bold;
        }
        .toast-notification {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 1000;
            min-width: 250px;
        }
        .hidden-message {
            display: none;
        }
        .empty-form {
            color: #6c757d;
            background-color: #f8f9fa;
        }
        .badge-success {
            background-color: #198754 !important;
            color: white !important;
        }
        .badge-danger {
            background-color: #dc3545 !important;
            color: white !important;
        }
        .badge-info {
            background-color: #0dcaf0 !important;
            color: black !important;
        }
        .table-container {
            display: flex;
            justify-content: center;
        }
        .gridview-container {
            width: 100%;
            max-width: 600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="toastContainer" class="toast-notification"></div>

    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <asp:UpdatePanel ID="UpdatePanelForm" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="card text-bg-light w-auto mb-2" style="width: 30rem;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <img width="100" src="imgs/user.png" />
                                        </center>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <h4>Профил судије</h4>
                                            <span>Статус судије - </span>
                                            <asp:Label class="badge rounded-pill text-bg-info" ID="lblStatus" runat="server" Text="Није изабрано"></asp:Label>
                                        </center>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col">
                                        <hr />
                                    </div>
                                </div>

                                <div class="row mb-2">
                                    <div class="col">
                                        <asp:Label ID="lblPoruka" runat="server" Text="" CssClass="hidden-message"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Име и презиме</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtImePrezime" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Датум рођења</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtDatumRodjenja" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Број телефона</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtBrojTelefona" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Емаил адреса</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtEmail" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Град/Место</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtGradMesto" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Ватрогасни Савез</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtVatrogasniSavez" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>ДВД друштво</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtDvdDrustvo" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Судија од</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtSudijaOd" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Важност лиценце</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtVaznostLicence" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Врста лиценце</label>
                                        <div class="form mb-2">
                                            <asp:TextBox CssClass="form-control empty-form" ID="txtVrstaLicence" runat="server" placeholder="Изаберите судију из листе" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <!-- ДОДАТО: Дугме за измену -->
                                <div class="row mt-3">
                                    <div class="col">
                                        <div class="d-grid gap-2">
                                            <asp:Button ID="btnIzmeniSudiju" runat="server" Text="Измени податке судије" 
                                                CssClass="btn btn-warning" OnClick="btnIzmeniSudiju_Click" 
                                                OnClientClick="return potvrdaIzmene();" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSudije" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <a href="default.aspx"><< Назад на почетну</a><br />
                <br />
            </div>

            <div class="col-md-5 mx-auto">
                 <div class="card text-bg-info" style="width: 30rem;">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100" src="imgs/whistle.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Листа судија</h4>
                                    <asp:Label class="badge rounded-pill text-bg-success" ID="lblUkupnoSudija" runat="server" Text=""></asp:Label>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col table-container">
                                <div class="gridview-container">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView class="table table-success table-striped" ID="gvSudije" runat="server" 
                                                CssClass="gridview-style" AutoGenerateColumns="false" 
                                                DataKeyNames="SudijeID" OnSelectedIndexChanged="gvSudije_SelectedIndexChanged"
                                                OnRowDataBound="gvSudije_RowDataBound" EnableViewState="True">
                                                <Columns>
                                                    <asp:BoundField DataField="imePrezime" HeaderText="Име и презиме" SortExpression="imePrezime" />
                                                    <asp:BoundField DataField="gradMesto" HeaderText="Град/Место" SortExpression="gradMesto" />
                                                    <asp:ButtonField Text="Одабери" CommandName="Select" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
                                                </Columns>
                                                <HeaderStyle BackColor="#198754" ForeColor="White" />
                                                <SelectedRowStyle CssClass="selected-row" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
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

        function removeEmptyFormClass() {
            var inputs = document.querySelectorAll('.empty-form');
            inputs.forEach(function (input) {
                input.classList.remove('empty-form');
                input.style.color = '#212529';
                input.style.backgroundColor = '#fff';
                input.placeholder = "";
            });
        }

        function potvrdaIzmene() {
            var imePrezime = document.getElementById('<%= txtImePrezime.ClientID %>').value;
            if (!imePrezime || imePrezime.trim() === "" || imePrezime.includes("Изаберите")) {
                alert("Молимо изаберите судију пре него што кликнете на измену.");
                return false;
            }
            return confirm("Да ли сте сигурни да желите да измените податке ове судије?");
        }
    </script>

</asp:Content>