<%@ Page Title="Trenutna Takmičenja Vatrogasnog Saveza Srbije" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RezultatiTakmicenja.aspx.cs" Inherits="ProgramTakmicenja.RezultatiTakmicenja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            display: flex;
            gap: 10px; /* Dodaje razmak između bočnih traka i glavnog sadržaja */
        }
        .sidebar {
            width: 150px;
            padding: 15px;
            background-color: #f8f9fa;
            border-right: 2px solid #ddd;
            box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1); /* Dodaje senku za bolji vizualni efekat */
        }
        .content {
            flex-grow: 1;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1); /* Dodaje senku glavnom sadržaju */
            border-radius: 8px; /* Zaobljeni uglovi */
            width: 200px;
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
        <!-- Sidebar left -->
        <div class="sidebar">
            <h4>Takmičenja</h4>
            <a href="#" >Pioniri</a>
            <a href="Kategorije/Juniori.aspx">Juniori.aspx</a>
            <a href="#" >Seniori</a>
        </div>
        

        <!-- Glavni sadržaj -->
        <div class="content" id="mainContent">
            <h2>Trenutna Takmičenja Vatrogasnog Saveza Srbije</h2>

            <!-- Slika Takmičenja -->
            <img src="putanja/do/slike/takmicenje.jpg" alt="Takmičenje Vatrogasnog Saveza" style="width:100%; height:auto; margin-bottom:20px;"/>

            <!-- Glavni sadržaj koji će se menjati -->
            <div>
                <p>Detalji o trenutnim takmičenjima...</p>
            </div>
        </div>
                
    </div>
    
</asp:Content>

