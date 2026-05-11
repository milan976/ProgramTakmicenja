<%@ Page Title="Rang Lista - Podmladak" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RangListaPodmladak.aspx.cs" Inherits="ProgramTakmicenja.StraniceTakmicenja.RangListaPodmladak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Tab kontrola stilovi */
        .tab-container { 
            margin: 20px 0 30px 0;
            background: #fff;
        }
        
        .tab-buttons { 
            display: flex; 
            border-bottom: 3px solid #007bff;
            margin-bottom: 20px;
        }
        
        .tab-button { 
            padding: 12px 25px; 
            background: #f8f9fa; 
            border: 1px solid #ddd;
            border-bottom: none;
            margin-right: 5px;
            cursor: pointer;
            text-decoration: none;
            color: #333;
            border-radius: 8px 8px 0 0;
            font-weight: 500;
            font-size: 16px;
            transition: all 0.3s ease;
        }
        
        .tab-button:hover {
            background: #e9ecef;
            color: #007bff;
        }
        
        .tab-button.active { 
            background: #007bff; 
            color: white;
            border-color: #007bff;
            font-weight: bold;
            position: relative;
        }
        
        .tab-button.active:after {
            content: '';
            position: absolute;
            bottom: -3px;
            left: 0;
            right: 0;
            height: 3px;
            background: #007bff;
        }
        
        /* Ostali stilovi */
        .print-optimized {
            width: 100%;
        }
        
        .rang-table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 14px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        
        .rang-table, .rang-table th, .rang-table td {
            border: 1px solid #dee2e6;
        }
        
        .rang-table th, .rang-table td {
            padding: 12px;
            text-align: center;
            vertical-align: middle;
        }
        
        .rang-table th {
            background-color: #343a40;
            color: white;
            font-weight: bold;
            border-color: #454d55;
        }
        
        .rang-table tr:nth-child(even) {
            background-color: #f8f9fa;
        }
        
        .rang-table tr:hover {
            background-color: #e9ecef;
        }
        
        .naziv-ekipe {
            text-align: left;
            font-weight: bold;
            color: #2c3e50;
        }
        
        /* Boje za prva tri mesta */
        .rang-1 { 
            background-color: #ffd700 !important; 
            font-weight: bold;
            color: #000 !important;
        }
        
        .rang-2 { 
            background-color: #c0c0c0 !important; 
            font-weight: bold;
            color: #000 !important;
        }
        
        .rang-3 { 
            background-color: #cd7f32 !important; 
            font-weight: bold;
            color: white !important;
        }
        
        /* Stilovi za štampu */
        @media print {
            .no-print, nav, header, footer, .breadcrumb, .tab-buttons {
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
                font-size: 18pt;
                color: #000 !important;
            }
            
            .rang-table {
                font-size: 11pt;
                box-shadow: none !important;
            }
            
            .rang-table th {
                background-color: #ddd !important;
                color: #000 !important;
                -webkit-print-color-adjust: exact;
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
            font-weight: bold;
            transition: background 0.3s;
            float: right;
        }
        
        .print-btn:hover {
            background: #c0392b;
        }
        
        .breadcrumb {
            margin-bottom: 20px;
            font-size: 16px;
            padding: 10px;
            background: #f8f9fa;
            border-radius: 5px;
        }
        
        .breadcrumb a {
            color: #007bff;
            text-decoration: none;
            font-weight: 500;
        }
        
        .breadcrumb a:hover {
            text-decoration: underline;
        }
        
        .breadcrumb strong {
            color: #495057;
        }
        
        .status-poruka {
            padding: 15px;
            margin: 20px 0;
            border-radius: 5px;
            text-align: center;
            font-weight: bold;
            clear: both;
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
        
        /* Clearfix za float elemente */
        .clearfix:after {
            content: "";
            display: table;
            clear: both;
        }
        
        .page-title {
            float: left;
            margin: 0;
            padding: 0;
            color: #2c3e50;
        }
        
        .page-title small {
            display: block;
            font-size: 14px;
            color: #6c757d;
            margin-top: 5px;
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
        
        <!-- Naslov i dugme za štampu -->
        <div class="clearfix">
            <h1 class="page-title">Ватрогасни Савез Србије<br/>
            <small>Ранг листа - Подмладак</small></h1>
            
            <!-- Dugme za štampu -->
            <div class="no-print">
                <button class="print-btn" onclick="window.print()">
                    🖨️ Штампај ранг листу
                </button>
            </div>
        </div>

        <!-- Tab kontrola -->
        <div class="tab-container no-print">
            <div class="tab-buttons">
                <asp:LinkButton ID="lnkMuski" runat="server" 
                    CssClass="tab-button active" 
                    OnClick="lnkMuski_Click"
                    CommandArgument="muski">
                    <i class="fas fa-mars" style="margin-right: 8px;"></i>Мушки
                </asp:LinkButton>
                
                <asp:LinkButton ID="lnkZenski" runat="server" 
                    CssClass="tab-button" 
                    OnClick="lnkZenski_Click"
                    CommandArgument="zenski">
                    <i class="fas fa-venus" style="margin-right: 8px;"></i>Женски
                </asp:LinkButton>                
            </div>
        </div>

        <!-- Rang lista -->
        <div class="print-optimized">
            <asp:GridView ID="GridViewPodmladak" runat="server" 
                CssClass="rang-table" 
                AutoGenerateColumns="false" 
                ShowHeader="true"
                OnRowDataBound="GridViewPodmladak_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Rang" HeaderText="#" 
                        HeaderStyle-Width="5%" 
                        ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-HorizontalAlign="Center" />
                        
                    <asp:BoundField DataField="NazivEkipe" HeaderText="Назив Екипе" 
                        HeaderStyle-Width="25%" 
                        ItemStyle-CssClass="naziv-ekipe" 
                        HeaderStyle-HorizontalAlign="Center" />
                        
                    <asp:BoundField DataField="Kategorija" HeaderText="Категорија" 
                        HeaderStyle-Width="15%"
                        ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-HorizontalAlign="Center" />
                        
                    <asp:BoundField DataField="PocetniBodovi" HeaderText="Почетни Бодови" 
                        HeaderStyle-Width="10%" 
                        ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-HorizontalAlign="Center" />
                        
                    <asp:BoundField DataField="VremeVezba" HeaderText="Време Вежбе" 
                        HeaderStyle-Width="10%" 
                        ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-HorizontalAlign="Center" />
                        
                    <asp:BoundField DataField="GreskeVezba" HeaderText="Грешке Вежбе" 
                        HeaderStyle-Width="10%" 
                        ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-HorizontalAlign="Center" />
                        
                    <asp:BoundField DataField="VremeStafeta" HeaderText="Време Штафете" 
                        HeaderStyle-Width="10%" 
                        ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-HorizontalAlign="Center" />
                        
                    <asp:BoundField DataField="GreskeStafeta" HeaderText="Грешке Штафете" 
                        HeaderStyle-Width="10%" 
                        ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-HorizontalAlign="Center" />
                        
                    <asp:BoundField DataField="KonacniBodovi" HeaderText="Коначни Бодови" 
                        HeaderStyle-Width="10%" 
                        ItemStyle-Font-Bold="true" 
                        ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-HorizontalAlign="Center" />
                </Columns>
        
                <EmptyDataTemplate>
                    <tr>
                        <td colspan="9" style="text-align: center; padding: 40px; color: #6c757d; font-style: italic;">
                            <div style="font-size: 18px; margin-bottom: 10px;">
                                <i class="fas fa-info-circle" style="font-size: 48px; color: #6c757d; margin-bottom: 15px;"></i>
                            </div>
                            <strong style="font-size: 16px;">Нема података за приказ</strong><br />
                            <span style="font-size: 14px;">
                                Тренутно нема екипа у овој категорији или нема унетих резултата.
                            </span>
                        </td>
                    </tr>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        
        <!-- Podnožje za štampu -->
        <div style="margin-top: 30px; font-size: 10pt; text-align: center;" class="no-print">
            Генерисано: <%= DateTime.Now.ToString("dd.MM.yyyy. HH:mm") %> |
            Корисник: <%= User.Identity.Name %>
        </div>
    </div>

    <!-- Font Awesome za ikonice (ako već nemate) -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
</asp:Content>