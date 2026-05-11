<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="/adminkorisnika.aspx.cs" Inherits="ProgramTakmicenja.adminkorisnikaaspx" %>
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
                                    <img width="120" src="imgs/business-people.png" />
                                </center>
                             </div>
                         </div>
                         
                         <div class="row">
                             <div class="col">
                                <center>
                                    <h4>Унос Администратора</h4>
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
                                    <asp:RequiredFieldValidator 
                                        ID="rfvPunoime" 
                                        runat="server" 
                                        ControlToValidate="TextBox3" 
                                        ErrorMessage="Ово поље је обавезно." 
                                        ForeColor="Red" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Емаил адреса</label>
                                <div class="form mb-2">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Емаил адреса" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="rfvEmail" 
                                        runat="server" 
                                        ControlToValidate="TextBox2" 
                                        ErrorMessage="Ово поље је обавезно." 
                                        ForeColor="Red" />
                                    <asp:RegularExpressionValidator 
                                        ID="revEmail" 
                                        runat="server" 
                                        ControlToValidate="TextBox2" 
                                        ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" 
                                        ErrorMessage="Неисправан формат емаил адресе." 
                                        ForeColor="Red" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Корисничко име</label>
                                <div class="form mb-2">
                                    <asp:TextBox class="form-control" ID="TextBox8" runat="server" placeholder="Корисничко име"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="rfvUsername" 
                                        runat="server" 
                                        ControlToValidate="TextBox8" 
                                        ErrorMessage="Ово поље је обавезно." 
                                        ForeColor="Red" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Приступ</label>
                                <div class="form mb-2">
                                    <asp:TextBox class="form-control" ID="TextBox1" runat="server" placeholder="Врста приступа..."></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator1" 
                                        runat="server" 
                                        ControlToValidate="TextBox1" 
                                        ErrorMessage="Ово поље је обавезно." 
                                        ForeColor="Red" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Лозинка</label>
                                <div class="form mb-2">
                                    <asp:TextBox class="form-control" ID="TextBox9" runat="server" placeholder="Лозинка" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="rfvPassword" 
                                        runat="server" 
                                        ControlToValidate="TextBox9" 
                                        ErrorMessage="Ово поље је обавезно." 
                                        ForeColor="Red" />
                                </div>
                            </div>

                        </div>

                        <div class="form mb-2 d-grid gap-2">
                            <asp:Button CssClass="btn btn-info btn-lg" ID="Button2" runat="server" Text="Регистрација" OnClick="Button2_Click" />
                        </div>

                     </div>


                 </div>
                <a href="default.aspx"><< Назад на почетну</a><br />
                <br />

             </div>
         </div>
     </div>

</asp:Content>
