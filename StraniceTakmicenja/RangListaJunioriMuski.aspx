<%@ Page Title="Rang Lista - Juniori" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RangListaJunioriMuski.aspx.cs" Inherits="ProgramTakmicenja.StraniceTakmicenja.RangListaJunioriMuski" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .print-optimized {
            width: 100%;
        }
        
        .rang-table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }
        
        .rang-table, .rang-table th, .rang-table td {
            border: 1px solid #333;
        }
        
        .rang-table th, .rang-table td {
            padding: 10px;
            text-align: center;
        }
        
        .rang-table th {
            background-color: #f2f2f2;
            font-weight: bold;
        }
        
        .naziv-ekipe {
            text-align: left;
            font-weight: bold;
        }
        
        /* Boje za prva tri mesta */
        .rang-1 { background-color: #ffd700 !important; }
        .rang-2 { background-color: #c0c0c0 !important; }
        .rang-3 { background-color: #cd7f32 !important; color: white !important; }
        
        /* Stilovi za štampu */
        @media print {
            .no-print, nav, header, footer, .breadcrumb {
                display: none !important;
            }
            
            body {
                margin: 0 !important;
                padding: 20px !important;
                font-size: 12pt;
            }
            
            .container {
                width: 100% !important;
                max-width: 100% !important;
            }
            
            .page-title {
                text-align: center;
                margin-bottom: 20px;
                font-size: 16pt;
            }
        }
        
        .print-btn {
            padding: 10px 20px;
            background: #e74c3c;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-bottom: 20px;
        }
        
        .breadcrumb {
            margin-bottom: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Navigacija -->
        <div class="breadcrumb no-print">
            <a href="RezultatiTakmicenja.aspx">Takmičenja</a> > 
            <strong>Juniori - Rang Lista</strong>
        </div>

        <!-- Dugme za štampu -->
        <div class="no-print" style="text-align: right;">
            <button class="print-btn" onclick="window.print()">
                🖨️ Štampaj Rang Listu
            </button>
        </div>

        <!-- Naslov za štampu -->
        <h1 class="page-title">RANG LISTA - JUNIORI<br/>
        <small>Vatrogasni Savez Srbije</small></h1>

        <!-- Rang lista -->
        <div class="print-optimized">
            <asp:GridView ID="GridViewJuniori" runat="server" CssClass="rang-table" AutoGenerateColumns="false" ShowHeader="true">
                <Columns>
                    <asp:TemplateField HeaderText="#" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NazivEkipe" HeaderText="Naziv Ekipe" HeaderStyle-Width="25%" ItemStyle-CssClass="naziv-ekipe" />
                    <asp:BoundField DataField="PocetniBodovi" HeaderText="Početni Bodovi" HeaderStyle-Width="10%" />
                    <asp:BoundField DataField="VremeVezba" HeaderText="Vreme Vežba" HeaderStyle-Width="10%" />
                    <asp:BoundField DataField="GreskeVezba" HeaderText="Greške Vežba" HeaderStyle-Width="10%" />
                    <asp:BoundField DataField="VremeStafeta" HeaderText="Vreme Štafeta" HeaderStyle-Width="10%" />
                    <asp:BoundField DataField="GreskeStafeta" HeaderText="Greške Štafeta" HeaderStyle-Width="10%" />
                    <asp:BoundField DataField="KonacniBodovi" HeaderText="Konačni Bodovi" HeaderStyle-Width="10%" ItemStyle-Font-Bold="true" />
                </Columns>
            </asp:GridView>
        </div>
        
        <!-- Podnožje za štampu -->
        <div style="margin-top: 30px; font-size: 10pt; text-align: center;">
            Generisano: <%= DateTime.Now.ToString("dd.MM.yyyy HH:mm") %>
        </div>
    </div>
</asp:Content>