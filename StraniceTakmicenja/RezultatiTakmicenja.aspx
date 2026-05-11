<%@ Page Title="Trenutna Takmičenja Vatrogasnog Saveza Srbije" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RezultatiTakmicenja.aspx.cs" Inherits="ProgramTakmicenja.RezultatiTakmicenja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .kategorije-container {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin-top: 30px;
        }
        
        .kategorija-card {
            border: 2px solid #e74c3c;
            border-radius: 10px;
            padding: 25px;
            text-align: center;
            text-decoration: none;
            color: #333;
            background: white;
            transition: all 0.3s ease;
            cursor: pointer;
        }
        
        .kategorija-card:hover {
            background: #e74c3c;
            color: white;
            transform: translateY(-5px);
            box-shadow: 0 5px 15px rgba(0,0,0,0.2);
        }
        
        .kategorija-icon {
            font-size: 40px;
            margin-bottom: 15px;
        }
        
        .kategorija-naziv {
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="content" id="mainContent">
            <h2>Trenutna Takmičenja Vatrogasnog Saveza Srbije</h2>

            <img src="../pictures/Uzice.jpg" alt="Takmičenje Vatrogasnog Saveza" style="width:100%; height:auto; margin-bottom:20px;"/>

            <h3>Odaberite kategoriju za pregled rang liste:</h3>
            
            <div class="kategorije-container">
                <!-- Podmladak -->
                <a href="RangListaPodmladak.aspx" class="kategorija-card">
                    <div class="kategorija-icon">🔰</div>
                    <div class="kategorija-naziv">PODMLADAK</div>
                    <div>Pregled rang liste ekipa</div>
                </a>

                <!-- Juniori -->
                <a href="RangListaJuniori.aspx" class="kategorija-card">
                    <div class="kategorija-icon">🔥</div>
                    <div class="kategorija-naziv">JUNIORI</div>
                    <div>Pregled rang liste ekipa</div>
                </a>

                <!-- DVD -->
                <a href="RangListaDVD.aspx" class="kategorija-card">
                    <div class="kategorija-icon">👥</div>
                    <div class="kategorija-naziv">DVD</div>
                    <div>Pregled rang liste ekipa</div>
                </a>

                <!-- Profesionalci -->
                <a href="RangListaProfesionalci.aspx" class="kategorija-card">
                    <div class="kategorija-icon">👨‍🚒</div>
                    <div class="kategorija-naziv">PROFESIONALCI</div>
                    <div>Pregled rang liste ekipa</div>
                </a>
            </div>
        </div>
    </div>
</asp:Content>