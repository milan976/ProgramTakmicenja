<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="korprofil.aspx.cs" Inherits="ProgramTakmicenja.korprofil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">

                <div class="card text-bg-light" style="width: 40rem;">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100" src="imgs/admin-settings.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Поздрав админ...</h4>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Име и презиме</label>
                                <div class="form mb-2">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Име и презиме"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Емаил адреса</label>
                                <div class="form mb-2">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Емаил адреса" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>
                            <center>
                                <span class="badge rounded-pill text-bg-warning">Подаци за улазак</span>
                            </center>
                            <div class="col-md-4">
                                <label>Корисничко име</label>
                                <div class="form mb-2">
                                    <asp:TextBox class="form-control" ID="TextBox8" runat="server" placeholder="Корисничко име" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Нова лозинка</label>
                                <div class="form mb-2">
                                    <asp:TextBox class="form-control" ID="TextBox9" runat="server" placeholder="Лозинка" TextMode="Password" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Потврди лозинку</label>
                                <div class="form mb-2">
                                    <asp:TextBox class="form-control" ID="TextBox1" runat="server" CssClass="form-control" placeholder="Лозинка" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="d-grid gap-2 col-6 mx-auto">
                            <asp:Button ID="Button2" runat="server" Text="Promeni lozinku" OnClick="Button2_Click" OnClientClick="return validatePassword();" />
                        </div>
                        <div>
                            <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                        </div>

                    </div>



                </div>
                <a href="default.aspx"><< Назад на почетну</a><br />
                <br />



            </div>

        </div>

    </div>
    <script>
        function validatePassword() {
            var password = document.getElementById("TextBox9").value;
            var confirmPassword = document.getElementById("TextBox1").value;

            console.log("Password: " + password);
            console.log("Confirm Password: " + confirmPassword);

            if (password.length < 8) {
                alert("Lozinka mora imati najmanje 8 karaktera.");
                return false;
            }
            if (!/[0-9]/.test(password)) {
                alert("Lozinka mora sadržati bar jedan broj.");
                return false;
            }
            if (!/[A-Z]/.test(password)) {
                alert("Lozinka mora sadržati bar jedno veliko slovo.");
                return false;
            }
            if (password !== confirmPassword) {
                alert("Lozinke se ne poklapaju.");
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
