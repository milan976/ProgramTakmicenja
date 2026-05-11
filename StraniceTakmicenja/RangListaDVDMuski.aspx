<%@ Page Title="Rang Lista - DVD" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RangListaDVDMuski.aspx.cs" Inherits="ProgramTakmicenja.StraniceTakmicenja.RangListaDVDMuski" %>

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
        
        .print-btn, .refresh-btn {
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-bottom: 20px;
            margin-right: 10px;
        }
        
        .print-btn {
            background: #e74c3c;
            color: white;
        }
        
        .refresh-btn {
            background: #3498db;
            color: white;
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
            <strong>DVD - Rang Lista</strong>
        </div>

        <!-- Dugmad -->
        <div class="no-print" style="text-align: right;">
            <asp:Button ID="btnOsvezi" runat="server" CssClass="refresh-btn" Text="⟳ Osveži Rang Listu" OnClick="btnOsvezi_Click" />
            <button class="print-btn" onclick="window.print()">
                🖨️ Štampaj Rang Listu
            </button>
        </div>

        <!-- Naslov za štampu -->
        <h1 class="page-title">RANG LISTA - DVD<br/>
        <small>Vatrogasni Savez Srbije</small></h1>

        <!-- Rang lista -->
        <div class="print-optimized">
            <asp:GridView ID="GridViewDVD" runat="server" CssClass="rang-table" AutoGenerateColumns="false" ShowHeader="true">
                <Columns>
                    <asp:TemplateField HeaderText="#" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NazivEkipe" HeaderText="Naziv Ekipe" HeaderStyle-Width="25%" ItemStyle-CssClass="naziv-ekipe" />
                    <asp:BoundField DataField="PocetniBodovi" HeaderText="Početni Bodovi" HeaderStyle-Width="10%" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="SrednjaVrednostMVP" HeaderText="Srednje Vreme MVP" HeaderStyle-Width="10%" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="ZbirGresakaMVP" HeaderText="Greške MVP" HeaderStyle-Width="10%" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="SrednjaVrednostStafeta" HeaderText="Srednje Vreme Štafeta" HeaderStyle-Width="10%" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="ZbirGresakaStafeta" HeaderText="Greške Štafeta" HeaderStyle-Width="10%" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="UkupanRezultat" HeaderText="Ukupan Rezultat" HeaderStyle-Width="10%" DataFormatString="{0:0.00}" ItemStyle-Font-Bold="true" />
                </Columns>
            </asp:GridView>
        </div>
        
        <!-- Poruka ako nema podataka -->
        <asp:Label ID="lblPoruka" runat="server" CssClass="text-info" Visible="false"></asp:Label>
        
        <!-- Podnožje za štampu -->
        <div style="margin-top: 30px; font-size: 10pt; text-align: center;">
            Generisano: <%= DateTime.Now.ToString("dd.MM.yyyy HH:mm") %>
        </div>
    </div>
</asp:Content>