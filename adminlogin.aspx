<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminlogin.aspx.cs" Inherits="ProgramTakmicenja.adminlogin" %>
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
                                    <img width="120" src="imgs/admin-settings.png" />
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Улаз Администратора</h3>
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

                                <label>ИД Администратора</label>
                                <div class="form mb-2">
                                    <asp:TextBox CssClass="form-control" ID="AdminBox" runat="server" placeholder="ИД Администратора"></asp:TextBox>
                                </div>
                            </div>
                            <label>Лозинка</label>
                            <div class="form mb-2">
                                <asp:TextBox CssClass="form-control" ID="AdminPass" runat="server" placeholder="Лозинка" TextMode="Password"></asp:TextBox>

                            </div>
                            <div class="form mb-2 d-grid gap-2">
                                <asp:Button class="btn btn-success" ID="AdminUlaz" runat="server" Text="Улаз" OnClick="AdminUlaz_Click" />

                            </div>
                        </div>
                    </div>
                </div>
                <a href="default.aspx"><< Назад на почетну</a><br /><br />
            </div>
        </div>
    </div>


</asp:Content>
