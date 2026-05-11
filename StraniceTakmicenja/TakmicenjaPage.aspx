<%@ Page Title="Takmičenja Vatrogasnog saveza Srbije" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TakmicenjaPage.aspx.cs" Inherits="ProgramTakmicenja.StraniceTakmicenja.TakmicenjaPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        
        <!-- Glavni sadržaj -->
        <div class="content">
            <h1>Такмичења Ватрогасног савеза Србије</h1>
            <p>
                Ватрогасни савез Србије организује низ такмичења на државном и међународном нивоу.
                Циљ ових такмичења је унапређење вештина ватрогасаца, повећање тимског духа и промоције заштите од пожара.
            </p>

            <div class="image-container">
                <img src="../pictures/Borgo.jpg" style="width: 900px; height: auto" alt="Takmičenje vatrogasaca" />
            </div>
        </div>
    </div>
</asp:Content>
