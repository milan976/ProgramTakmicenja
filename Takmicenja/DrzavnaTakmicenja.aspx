<%@ Page Title="Državna Takmičenja" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DrzavnaTakmicenja.aspx.cs" Inherits="ProgramTakmicenja.DrzavnaTakmicenja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            display: flex;
        }
        .sidebar {
            width: 250px;
            padding: 15px;
            background-color: #f8f9fa;
            border-right: 2px solid #ddd;
        }
        .content {
            flex-grow: 1;
            padding: 20px;
        }
        .sidebar a {
            display: block;
            padding: 10px;
            margin-bottom: 5px;
            background-color: #28a745;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            text-align: center;
        }
        .sidebar a:hover {
            background-color: #218838;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Sidebar -->
        <div class="sidebar">
            <h3>Državna Takmičenja</h3>
            <a href="Uskoro.aspx" id="Uskoro">Uskoro</a>
            <a href="ProslaTakmicenja.aspx">Prošla takmičenja</a>

            <!-- Link za unos rezultata se prikazuje samo ako je korisnik admin ili adminx -->
            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                <a href="RezultatiTakmicenja.aspx">Unos rezultata takmičenja</a>
            </asp:Panel>
        </div>

        <!-- Glavni sadržaj -->
        <div class="content">
            <h1>Državna Takmičenja</h1>
            <p>
                Na ovoj stranici možete pronaći informacije o državnim vatrogasnim takmičenjima.
                Pogledajte predstojeća takmičenja, rezultate prošlih događaja ili unesite nove rezultate.
            </p>
            <img src="Images/drzavna_takmicenja.jpg" alt="Državna takmičenja" style="max-width:100%; height:auto; border-radius:10px;">
        </div>
    </div>
</asp:Content>
