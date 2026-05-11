<%@ Page Title="Rang Lista - Podmladak" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RangListaPodmladakMuski.aspx.cs" Inherits="ProgramTakmicenja.StraniceTakmicenja.RangListaPodmladakMuski" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .print-optimized {
            width: 100%;
        }
        
        .rang-table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 14px;
        }
        
        .rang-table, .rang-table th, .rang-table td {
            border: 1px solid #333;
        }
        
        .rang-table th, .rang-table td {
            padding: 12px;
            text-align: center;
        }
        
        .rang-table th {
            background-color: #f2f2f2;
            font-weight: bold;
            color: #333;
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
            
            .rang-table {
                font-size: 11pt;
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
            font-size: 16px;
        }
        
        .print-btn:hover {
            background: #c0392b;
        }
        
        .breadcrumb {
            margin-bottom: 20px;
            font-size: 16px;
        }
        
        .breadcrumb a {
            color: #007bff;
            text-decoration: none;
        }
        
        .breadcrumb a:hover {
            text-decoration: underline;
        }
        
        .status-poruka {
            padding: 15px;
            margin: 20px 0;
            border-radius: 5px;
            text-align: center;
            font-weight: bold;
        }
        
        .status-greska {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
        
        .status-upozorenje {
            background-color: #fff3cd;
            color: #856404;
            border: 1px solid #ffeaa7;
        }
        
        .status-info {
            background-color: #d1ecf1;
            color: #0c5460;
            border: 1px solid #bee5eb;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Navigacija -->
        <div class="breadcrumb no-print">
            <a href="RezultatiTakmicenja.aspx">Такмичења</a> > 
            <strong>Подмладак - Ранг листа</strong>
        </div>

        <!-- Dugme za štampu -->
        <div class="no-print" style="text-align: right;">
            <button class="print-btn" onclick="window.print()">
                🖨️ Штампај ранг листу
            </button>
        </div>

        <!-- Status poruke -->
        <asp:Panel ID="pnlStatus" runat="server" CssClass="status-poruka status-info" Visible="false">
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </asp:Panel>

        <!-- Naslov za štampu -->
        <h1 class="page-title">Ватрогасни Савез Србије<br/>
        <small>Ранг листа - Подмладак</small></h1>

       <!-- Rang lista -->
        <div class="print-optimized">
            <asp:GridView ID="GridViewPodmladakMuski" runat="server" CssClass="rang-table" AutoGenerateColumns="false" ShowHeader="true">
                <Columns>
                    <asp:BoundField DataField="Rang" HeaderText="#" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="NazivEkipe" HeaderText="Назив Екипе" HeaderStyle-Width="25%" ItemStyle-CssClass="naziv-ekipe" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PocetniBodovi" HeaderText="Почетни Бодови" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="VremeVezba" HeaderText="Време Вежба" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="GreskeVezba" HeaderText="Грешке Вежба" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="VremeStafeta" HeaderText="Време Штафета" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="GreskeStafeta" HeaderText="Грешке Штафета" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="KonacniBodovi" HeaderText="Коначни Бодови" HeaderStyle-Width="10%" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                </Columns>
        
                <EmptyDataTemplate>
                    <tr>
                        <td colspan="8" style="text-align: center; padding: 30px; color: #666; font-style: italic;">
                            <strong>Нема података за приказ</strong><br />
                            Тренутно нема екипа у категорији "Подмладак" или нема унетих резултата.
                        </td>
                    </tr>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        
        <!-- Podnožje za štampu -->
        <div style="margin-top: 30px; font-size: 10pt; text-align: center;" class="no-print">
            Генерисано: <%= DateTime.Now.ToString("dd.MM.yyyy. HH:mm") %>
        </div>
    </div>

    <script type="text/javascript">
        function showAlert(message, type) {
            alert(message);
        }

        // Automatski sakrij status poruku nakon 5 sekundi
        setTimeout(function () {
            var statusPanel = document.getElementById('<%= pnlStatus.ClientID %>');
            if (statusPanel) {
                statusPanel.style.display = 'none';
            }
        }, 5000);
    </script>
</asp:Content>