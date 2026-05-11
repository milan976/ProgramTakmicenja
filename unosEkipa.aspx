<%@ Page Title="Пријава такмичара" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="unosEkipa.aspx.cs" Inherits="ProgramTakmicenja.unosEkipaPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Label ID="Label2" runat="server" Text="" />

<div class="container">
    <div class="row">
        <div class="col-md-12 mx-auto">
            <div class="card text-bg-light mb-4">
                <div class="card-header">
                    <h4 class="text-center">Пријава Екипа за ватрогасна такмичења</h4>
                </div>
                <div class="card-body">

                    <!-- Podaci o ekipi koja se unosi u bazu -->
                    <div class="form-group mb-3">
                        <label>Датум уноса:</label>
                        <asp:TextBox ID="TextBoxDatumUnosa" CssClass="form-control" runat="server" Width="300"></asp:TextBox>
                    </div>
                    <div class="form-group mb-3">
                        <label>DVD/PVJ:</label>
                        <asp:TextBox ID="TextBoxNazivEkipe" CssClass="form-control" runat="server" placeholder="Назив екипе..." Width="500"></asp:TextBox>
                    </div>
                    <div class="form-group mb-3">
                        <label>ОВС:</label>
                        <asp:TextBox ID="TextBoxOVS" CssClass="form-control" runat="server" placeholder="ОВС Екипе" Width="300"></asp:TextBox>
                    </div>

                    <!-- Podaci o ekipi -->
                    <h5>Подаци о екипи</h5>
                    <div class="form-group mb-3">
                        <label>Категорија и класа:</label>
                        <asp:DropDownList ID="DropDownListKategorija" 
                                         runat="server"
                                         CssClass="form-control"
                                         Width="350px"
                                         AutoPostBack="True"
                                         OnSelectedIndexChanged="DropDownListKategorija_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="Изаберите категорију"></asp:ListItem>
                            <asp:ListItem Text="Ватрогасни подмладак - мушка" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Ватрогасни подмладак - жене" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Јуниори - мушка" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Јуниори - жене" Value="4"></asp:ListItem>
                            <asp:ListItem Text="ДВД класа А - мушка" Value="5"></asp:ListItem>
                            <asp:ListItem Text="ДВД класа А - жене" Value="6"></asp:ListItem>
                            <asp:ListItem Text="ДВД класа Б - мушка" Value="7"></asp:ListItem>
                            <asp:ListItem Text="ДВД класа Б - жене" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Професионалци класа А - мушка" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Професионалци класа А - жене" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Професионалци класа Б - мушка" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Професионалци класа Б - жене" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                         <asp:Table ID="TableClanovi" runat="server" CssClass="table table-striped table-bordered" ></asp:Table>
                        <div class="form-group mb-3">
                            <asp:Label ID="Label1" runat="server" CssClass="text-info"></asp:Label>
                            <h5>Додавање члана</h5>
                            <div class="row">
                                
                                <div class="col-md-2">
                                    <label>Улога:</label>
                                    <asp:DropDownList ID="DropDownListUloga" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="Изаберите улогу" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Командир" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Курир" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Моториста" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Навални 1" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Навални 2" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Водни 1" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Водни 2" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Цевни 1" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Цевни 2" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Резерва" Value="10"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>Име и презиме:</label>
                                    <asp:TextBox ID="TextBoxImePrezime" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label>Датум рођења:</label>
                                    <asp:TextBox ID="TextBoxDatumRodjenja" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                
                            </div>
                        </div>
                    <asp:Label ID="LabelMessage" runat="server" CssClass="text-success" />
                    </div>
                    <div class="form-group mb-3 text-center">
                        <asp:Button ID="ButtonDodajClana" runat="server" CssClass="btn btn-secondary" Text="Додај члана" OnClick="ButtonDodajClana_Click" />
                    </div>


                    <!-- Odgovorna lica -->
                    <h5>Одговорна лица</h5>
                    <div class="form-group mb-3">
                        <label>Тренер екипе:</label>
                        <asp:TextBox ID="TextBoxTrener" CssClass="form-control" runat="server" Width="300"></asp:TextBox>
                    </div>
                    <div class="form-group mb-3">
                        <label>Вођа екипе:</label>
                        <asp:TextBox ID="TextBoxVodjaEkipe" CssClass="form-control" runat="server" Width="300"></asp:TextBox>
                    </div>

                    <!-- Submit -->
                    <div class="form-group text-center">
                        <asp:Button ID="ButtonUnosEkipe" runat="server" Text="Сачувај екипу у бази..." CssClass="btn btn-primary" OnClick="ButtonUnosEkipe_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
