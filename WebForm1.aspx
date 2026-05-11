<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ProgramTakmicenja.WebForm1" Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center">Dobrodošli na sajt takmičenja Vatrogasnog Saveza Srbije.</h1>
    <hr />
    <div class="container">
        <div class="text-start">
            <p>Dobro došli na stranicu koja je osmišljena da vam pruži najbolje informacije o takmičenjima Vatrogasnog Saveza Srbije. Pored informacija kada se održavaju takmičenja ovde možete pogledati i ona takmičenja koja su održana u ranijim periodima. Isto tako možete pogledati i aktuelna pravila za Vatrogasna takmičenja koja se svake godine usklađuju sa pravilima CTIF-a.</p>
            <p>Takođe možete pogledati i spisak aktivnih sudija za vatrogasna takmičenja, članove odbora kao i predstavnike Vatrogasnog saveza Srbije u CTIF-u.</p>
        </div>
    </div>
</asp:Content>
