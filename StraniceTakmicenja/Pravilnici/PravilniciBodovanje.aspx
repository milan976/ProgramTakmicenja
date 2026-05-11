<%@ Page Title="Правилници и бодовање" Language="C#" MasterPageFile="~/Site1.Master" 
    AutoEventWireup="true" CodeBehind="PravilniciBodovanje.aspx.cs" 
    Inherits="ProgramTakmicenja.PravilniciBodovanje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pravilnici-container {
            padding: 20px;
            max-width: 1400px;
            margin: 0 auto;
        }
        
        .sidebar {
            background-color: #f8f9fa;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            height: fit-content;
        }
        
        .content-area {
            background-color: white;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            min-height: 800px;
        }
        
        .btn-pravilnik {
            width: 100%;
            margin-bottom: 10px;
            text-align: left;
            padding: 12px 15px;
            font-size: 16px;
            border: none;
            border-left: 4px solid #007bff;
            transition: all 0.3s;
            background-color: #f8f9fa;
        }
        
        .btn-pravilnik:hover {
            background-color: #e9ecef;
            transform: translateX(5px);
        }
        
        .btn-pravilnik.active {
            background-color: #007bff;
            color: white;
            border-left: 4px solid #0056b3;
        }
        
        .pdf-info-bar {
            background-color: #f8f9fa;
            padding: 10px 15px;
            border-radius: 5px;
            margin-bottom: 15px;
            border-left: 4px solid #007bff;
        }
        
        .section-title {
            color: #343a40;
            border-bottom: 2px solid #007bff;
            padding-bottom: 10px;
            margin-bottom: 20px;
        }
        
        .category-title {
            color: #495057;
            margin-top: 25px;
            margin-bottom: 15px;
            font-weight: 600;
        }
        
        .pdf-controls {
            margin-top: 15px;
            padding: 10px;
            background-color: #f8f9fa;
            border-radius: 5px;
        }
        
        .btn-pravilnik {
            width: 100%;
            margin-bottom: 10px;
            text-align: left;
            padding: 12px 15px;
            font-size: 16px;
            border: none;
            border-left: 4px solid #007bff;
            transition: all 0.3s;
            background-color: #f8f9fa !important;
            color: #212529 !important;
        }

        .btn-pravilnik.active {
            background-color: #007bff !important;
            color: white !important;
            border-left: 4px solid #0056b3 !important;
        }
        
        /* MAIN DISPLAY CONTAINER - GDE SE PRIKAZUJE PDF ILI HTML */
        .main-display-container {
            width: 100%;
            height: 750px;
            margin-bottom: 15px;
            position: relative;
        }
        
        /* Placeholder kada nije izabran dokument */
        .pdf-placeholder {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            width: 100%;
            height: 100%;
            background-color: #f8f9fa;
            border-radius: 5px;
            border: 2px dashed #dee2e6;
        }
        
        /* PDF prikaz - ZAUZIMA CEO MAIN-DISPLAY-CONTAINER */
        .pdf-display {
            width: 100%;
            height: 100%;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            display: none;
        }
        
        /* HTML prikaz - ZAUZIMA CEO MAIN-DISPLAY-CONTAINER */
        .html-display {
            width: 100%;
            height: 100%;
            overflow-y: auto;
            padding: 25px;
            background-color: white;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            display: none;
        }
        
        /* Stilovi za HTML sadržaj */
        .html-display table {
            width: 100%;
            margin-bottom: 20px;
            border-collapse: collapse;
            font-size: 14px;
        }
        
        .html-display th {
            background-color: #f8f9fa;
            padding: 12px 15px;
            text-align: left;
            border: 1px solid #dee2e6;
            font-weight: 600;
        }
        
        .html-display td {
            padding: 10px 15px;
            border: 1px solid #dee2e6;
            vertical-align: top;
        }
        
        .html-display .note {
            background-color: #fff3cd;
            border-left: 4px solid #ffc107;
            padding: 20px;
            margin: 20px 0;
            border-radius: 6px;
        }
        
        .html-display .points-table {
            background-color: #d1ecf1;
            border-left: 4px solid #0dcaf0;
            padding: 20px;
            margin: 25px 0;
            border-radius: 6px;
        }
        
        .html-display h4 {
            color: #0056b3;
            border-bottom: 2px solid #0056b3;
            padding-bottom: 10px;
            margin-top: 0;
            margin-bottom: 20px;
        }
        
        .html-display h5 {
            color: #495057;
            margin-top: 20px;
            margin-bottom: 15px;
            font-weight: 600;
        }
        
        .html-display h6 {
            color: #6c757d;
            margin-top: 15px;
            margin-bottom: 10px;
            font-weight: 600;
        }
        
        .html-display ul {
            padding-left: 25px;
            margin-bottom: 20px;
        }
        
        .html-display ul li {
            margin-bottom: 8px;
            line-height: 1.5;
        }
        
        .html-display p {
            line-height: 1.6;
            margin-bottom: 15px;
        }
        
        .html-display strong {
            color: #343a40;
        }
        
        /* Kontrole za PDF */
        .pdf-controls {
            margin-top: 15px;
            padding: 10px;
            background-color: #f8f9fa;
            border-radius: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pravilnici-container">
        <div class="row">
            <!-- Sidebar sa dugmadima -->
            <div class="col-md-3">
                <div class="sidebar">
                    <h3 class="section-title">Правилници и документи</h3>
                    
                    <h5 class="category-title">Општи правилници</h5>
                    <asp:Button ID="btnPravilnik1" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📄 Правилник Подмладак" OnClick="btnPravilnik_Click"  />
                    
                    <asp:Button ID="btnPravilnik2" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📄 Правилник Јуниори" OnClick="btnPravilnik_Click"  />
                    
                    <asp:Button ID="btnPravilnik3" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📄 Правилник Сениора" OnClick="btnPravilnik_Click" />
                    
                    <h5 class="category-title" style="margin-top: 30px;">Бодовање и критеријуми</h5>
                    <asp:Button ID="btnBodovanje1" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📊 Систем бодовања Подмладак" OnClick="btnPravilnik_Click" />
                    
                    <asp:Button ID="btnBodovanje2" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📊 Систем бодовања Јуниори" OnClick="btnPravilnik_Click" />
                    
                    <asp:Button ID="btnBodovanje3" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📊 Систем бодовања Сениори" OnClick="btnPravilnik_Click" />
                    
                    <h5 class="category-title" style="margin-top: 30px;">Форме и пријаве</h5>
                    <asp:Button ID="btnForma1" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📝 Пријава за такмичење" OnClick="btnPravilnik_Click" 
                        CommandArgument="PrijavaTakmicara.pdf" />
                    
                    <asp:Button ID="btnForma2" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📝 Бодовна листа Пионири" OnClick="btnPravilnik_Click" 
                        CommandArgument="BodovnaPodmladak.pdf" />

                    <asp:Button ID="btnForma3" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📝 Бодовна листа Јуниора" OnClick="btnPravilnik_Click" 
                        CommandArgument="BodovnaJuniora.pdf" />

                    <asp:Button ID="btnForma4" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📝 Бодовна листа Сениори" OnClick="btnPravilnik_Click" 
                        CommandArgument="BodovnaSeniori.pdf" />

                    <asp:Button ID="brnForma5" runat="server" CssClass="btn btn-pravilnik" 
                        Text="📝 Приговор" OnClick="btnPravilnik_Click" 
                        CommandArgument="Prigovor.pdf" />
                </div>
            </div>
            
            <!-- Glavni sadržaj za PDF pregled -->
            <div class="col-md-9">
                <div class="content-area">
                    <h2 class="section-title">Преглед документа</h2>
                    
                    <!-- Informaciona traka -->
                    <div id="pdfInfoBar" runat="server" class="pdf-info-bar" style="display: none;">
                        <div class="row align-items-center">
                            <div class="col-md-8">
                                <h5 id="pdfTitle" runat="server" style="margin: 0;"></h5>
                                <small id="pdfFileInfo" runat="server" class="text-muted"></small>
                            </div>
                            <div class="col-md-4 text-end">
                                <asp:HyperLink ID="lnkDownload" runat="server" CssClass="btn btn-sm btn-outline-primary" 
                                    Target="_blank">
                                    <i class="fas fa-download"></i> Преузми
                                </asp:HyperLink>
                                <asp:HyperLink ID="lnkOpenNew" runat="server" CssClass="btn btn-sm btn-outline-secondary ms-1" 
                                    Target="_blank">
                                    <i class="fas fa-external-link-alt"></i> Отвори у новом прозору
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                    
                    <!-- OVDE IDE GLAVNI PRIKAZ - ZAJEDNICKI KONTEJNER ZA SVE -->
                    <div class="main-display-container">
                        <!-- Placeholder kada nije izabran dokument -->
                        <div id="pdfPlaceholder" runat="server" class="pdf-placeholder">
                            <i class="fas fa-file-pdf fa-5x text-muted mb-3"></i>
                            <h4 class="text-muted">Изаберите документ за преглед</h4>
                            <p class="text-muted">Кликните на један од докумената са леве стране</p>
                        </div>
                        
                        <!-- PDF prikaz -->
                        <iframe id="pdfViewer" runat="server" class="pdf-display" 
                            frameborder="0" scrolling="auto"></iframe>
                        
                        <!-- HTML prikaz -->
                        <div id="htmlBodovanjeContainer" runat="server" class="html-display">
                        </div>
                    </div>
                    
                     <!-- Kontrole za PDF -->
                    <div id="pdfControls" runat="server" class="pdf-controls" style="display: none;">
                        <div class="btn-group btn-group-sm" role="group">
                            <button type="button" class="btn btn-outline-secondary" onclick="zoomIn()">
                                <i class="fas fa-search-plus"></i> Увећај
                            </button>
                            <button type="button" class="btn btn-outline-secondary" onclick="zoomOut()">
                                <i class="fas fa-search-minus"></i> Умањи
                            </button>
                            <button type="button" class="btn btn-outline-secondary" onclick="fitToWidth()">
                                <i class="fas fa-arrows-alt-h"></i> Приступ ширини
                            </button>
                            <button type="button" class="btn btn-outline-secondary" onclick="fitToPage()">
                                <i class="fas fa-expand-alt"></i> Приступ страни
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Font Awesome za ikonice -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
    
    <!-- JavaScript za kontrole PDF-a -->
    <script>
        var currentZoom = 100;

        function zoomIn() {
            currentZoom += 10;
            document.getElementById('<%= pdfViewer.ClientID %>').style.zoom = currentZoom + '%';
        }

        function zoomOut() {
            if (currentZoom > 30) {
                currentZoom -= 10;
                document.getElementById('<%= pdfViewer.ClientID %>').style.zoom = currentZoom + '%';
            }
        }

        function fitToWidth() {
            var iframe = document.getElementById('<%= pdfViewer.ClientID %>');
            iframe.style.width = '100%';
            iframe.style.height = '100%';
            currentZoom = 100;
            iframe.style.zoom = '100%';
        }

        function fitToPage() {
            var iframe = document.getElementById('<%= pdfViewer.ClientID %>');
            iframe.style.width = '100%';
            iframe.style.height = '100%';
            currentZoom = 100;
            iframe.style.zoom = '100%';
        }

        // Automatski resize kada se promeni veličina prozora
        window.addEventListener('resize', function () {
            var iframe = document.getElementById('<%= pdfViewer.ClientID %>');
            if (iframe.style.display !== 'none') {
                iframe.style.height = '100%';
            }
        });

        function resetButtons() {
            var buttons = document.querySelectorAll('.sidebar .btn-pravilnik');
            buttons.forEach(function (btn) {
                btn.classList.remove('active');
                btn.classList.remove('btn-primary');
                btn.classList.add('btn-light');
            });
        }

        function onButtonClick(clickedButton) {
            resetButtons();
            clickedButton.classList.add('active');
            clickedButton.classList.remove('btn-light');
            clickedButton.classList.add('btn-primary');
        }
    </script>
</asp:Content>