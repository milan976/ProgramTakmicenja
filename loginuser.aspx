<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="loginuser.aspx.cs" Inherits="ProgramTakmicenja.loginuser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">

                <div class="card text-bg-light" style="width: 25rem;">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="120" src="imgs/user.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Улаз корисника</h3>
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

                                <label>ИД корисника</label>
                                <div class="form mb-2">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="ИД Корисника"></asp:TextBox>
                                </div>

                            </div>

                            <label>Лозинка</label>
                            <div class="form mb-2">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Лозинка" TextMode="Password"></asp:TextBox>

                            </div>
                            <div class="form mb-2 d-grid gap-2">
                                <asp:Button class="btn btn-success" ID="Button1" runat="server" Text="Улаз" />

                            </div>
                            <div class="form mb-2 d-grid gap-2">
                                <asp:Button class="btn btn-info" ID="Button2" runat="server" Text="Регистрација" href="regkorisnika.aspx"/>

                            </div>
                        </div>



                    </div>




                </div>

                <a href="default.aspx"><< Назад на почетну</a><br /><br />
            </div>

        </div>
    </div>
            
</asp:Content>
