<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="regkorisnika.aspx.cs" Inherits="ProgramTakmicenja.regkorisnika" %>
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
                                    <img width="100" src="imgs/user.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Регистрација корисника</h4>
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
                            <div class="col-md-6">
                                <label>Корисничко име</label>
                                <div class="form mb-2">
                                    <asp:TextBox class="form-control" ID="TextBox8" runat="server" placeholder="Корисничко име"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Лозинка</label>
                                <div class="form mb-2">
                                    <asp:TextBox class="form-control" ID="TextBox9" runat="server" placeholder="Лозинка" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form mb-2 d-grid gap-2">
                            <input class="btn btn-info" id="Button2" type="button" value="Регистрација" />
                        </div>

                    </div>



                </div>
                <a href="default.aspx"><< Назад на почетну</a><br />
                <br />



            </div>

        </div>

    </div>



</asp:Content>
