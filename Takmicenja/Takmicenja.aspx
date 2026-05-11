<%@ Page Title="Takmičenja Vatrogasnog saveza Srbije" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Takmicenja.aspx.cs" Inherits="ProgramTakmicenja.Takmicenja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            display: flex;
        }
        .sidebar {
            width: 250px;
            padding: 20px;
            background-color: #f8f9fa;
            border-right: 2px solid #ddd;
        }
        .content {
            flex-grow: 1;
            padding: 20px;
        }
        .image-container img {
            max-width: 100%;
            height: auto;
            border-radius: 10px;
            box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.2);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Sidebar sa linkovima -->
        <div class="sidebar">
            <h3>Такмичења</h3>
            <ul>
                <li><asp:HyperLink ID="lnkDrzavna" runat="server" NavigateUrl="DrzavnaTakmicenja.aspx">Државна Такмичења</asp:HyperLink></li>
                <li><asp:HyperLink ID="lnkInostrana" runat="server" NavigateUrl="InostranaTakmicenja.aspx">Инострана Такмичења</asp:HyperLink></li>
            </ul>
        </div>

        <!-- Glavni sadržaj -->
        <div class="content">
            <h1>Такмичења Ватрогасног савеза Србије</h1>
            <p>
                Ватрогасни савез Србије организује низ такмичења на државном и међународном нивоу.
                Циљ ових такмичења је унапређење вештина ватрогасаца, повећање тимског духа и промоције заштите од пожара.
            </p>

            <div class="image-container">
                <img src="pictures/Borgo.jpg" alt="Takmičenje vatrogasaca" />
            </div>
        </div>
    </div>
</asp:Content>
