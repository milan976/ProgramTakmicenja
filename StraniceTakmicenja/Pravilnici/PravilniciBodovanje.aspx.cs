using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja
{
    public partial class PravilniciBodovanje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicijalno sakrij sve
                pdfViewer.Style["display"] = "none";
                pdfInfoBar.Style["display"] = "none";
                pdfControls.Style["display"] = "none";
                htmlBodovanjeContainer.Style["display"] = "none";
                pdfPlaceholder.Style["display"] = "flex";

                // Prikaži placeholder
                lnkDownload.Visible = true;
                lnkOpenNew.Visible = true;
            }
        }
        protected void btnPravilnik_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            // Sakrij sve kontejnere na početku
            pdfPlaceholder.Style["display"] = "none";
            pdfViewer.Style["display"] = "none";
            pdfInfoBar.Style["display"] = "none";
            pdfControls.Style["display"] = "none";
            htmlBodovanjeContainer.Style["display"] = "none";

            // Prikaži informacionu traku
            pdfInfoBar.Style["display"] = "block";
            pdfTitle.InnerText = btn.Text.Replace("📄", "").Replace("📊", "").Replace("📝", "").Trim();

            string buttonText = btn.Text.ToLower();

            // AKO JE DUCME ZA SISTEM BODOVANJA
            if (buttonText.Contains("систем бодовања"))
            {
                ShowHTMLContent(btn, "Bodovanje");
            }
            // AKO JE DUCME ZA PRAVILNIK
            else if (buttonText.Contains("правилник"))
            {
                ShowHTMLContent(btn, "Pravilnik");
            }
            // AKO JE DUCME ZA FORME (i dalje PDF)
            else if (buttonText.Contains("пријава") || buttonText.Contains("листа") || buttonText.Contains("приговор"))
            {
                ShowPDFContent(btn);
            }

            // Obeleži aktivno dugme
            SetActiveButton(btn);
        }

        private void ShowHTMLContent(Button btn, string type)
        {
            // Sakrij PDF komponente
            pdfViewer.Style["display"] = "none";
            pdfControls.Style["display"] = "none";

            // Generiši odgovarajući HTML
            string htmlContent = "";
            string buttonText = btn.Text;

            if (type == "Bodovanje")
            {
                htmlContent = GenerateBodovanjeHTML(buttonText);
            }
            else if (type == "Pravilnik")
            {
                htmlContent = GeneratePravilnikHTML(buttonText);
            }

            htmlBodovanjeContainer.InnerHtml = htmlContent;
            htmlBodovanjeContainer.Style["display"] = "block";

            // Postavi informacije za HTML
            pdfFileInfo.InnerText = $"HTML документ | {btn.Text.Replace("📄", "").Replace("📊", "").Replace("📝", "").Trim()}";

            // Sakrij linkove za download
            lnkDownload.Visible = false;
            lnkOpenNew.Visible = false;
        }

        private void ShowPDFContent(Button btn)
        {
            string pdfFileName = btn.CommandArgument;

            // Prikaži linkove za download
            lnkDownload.Visible = true;
            lnkOpenNew.Visible = true;

            // Putanja do PDF fajla
            string relativePath = "~/PDFs/" + pdfFileName;
            string physicalPath = Server.MapPath(relativePath);

            if (File.Exists(physicalPath))
            {
                // Sakrij HTML kontejner
                htmlBodovanjeContainer.Style["display"] = "none";

                // Prikaži PDF viewer
                pdfViewer.Src = ResolveUrl(relativePath);
                pdfViewer.Style["display"] = "block";
                pdfControls.Style["display"] = "block";

                // Prikaži informacije o fajlu
                FileInfo fileInfo = new FileInfo(physicalPath);
                pdfFileInfo.InnerText = string.Format("{0} | {1} KB | {2}",
                    Path.GetFileName(pdfFileName),
                    (fileInfo.Length / 1024).ToString(),
                    fileInfo.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));

                // Postavi linkove za download i otvaranje
                lnkDownload.NavigateUrl = ResolveUrl(relativePath);
                lnkDownload.Target = "_blank";
                lnkOpenNew.NavigateUrl = ResolveUrl(relativePath);
                lnkOpenNew.Target = "_blank";
            }
            else
            {
                // Ako fajl ne postoji
                string errorScript = string.Format(
                    "alert('Документ {0} није пронађен.\\nПутања: {1}\\n\\nМолимо проверите да ли је PDF фајл постављен у PDFs фолдер.');",
                    pdfFileName, physicalPath);
                ClientScript.RegisterStartupScript(this.GetType(), "PDFError", errorScript, true);

                // Vrati placeholder
                pdfPlaceholder.Style["display"] = "flex";
                pdfInfoBar.Style["display"] = "none";
            }
        }
        private string LoadHTMLFromFile(string fileName)
        {
            string filePath = Server.MapPath("~/HTMLContent/" + fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    return File.ReadAllText(filePath, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    return $"<div class='alert alert-danger'>Грешка при учитавању документа: {ex.Message}</div>";
                }
            }
            else
            {
                return $"<div class='alert alert-warning'>Документ {fileName} није пронађен.</div>";
            }
        }

        private string GenerateBodovanjeHTML(string buttonText)
        {
            // Odredi kategoriju na osnovu teksta dugmeta
            if (buttonText.Contains("Подмладак"))
            {
                return LoadHTMLFromFile("BodovanjePodmladak.html");
            }
            else if (buttonText.Contains("Јуниори"))
            {
                return LoadHTMLFromFile("BodovanjeJuniori.html");
            }
            else if (buttonText.Contains("Сениори"))
            {
                return LoadHTMLFromFile("BodovanjeSeniori.html");
            }

            return "<div class='alert alert-warning'>Текст за ову категорију није дефинисан.</div>";
        }

        private string GeneratePravilnikHTML(string buttonText)
        {
            // Odredi kategoriju na osnovu teksta dugmeta
            if (buttonText.Contains("Подмладак"))
            {
                return LoadHTMLFromFile("PravilnikPodmladak.html");
            }
            else if (buttonText.Contains("Јуниори"))
            {
                return LoadHTMLFromFile("PravilnikJuniori.html");
            }
            else if (buttonText.Contains("Сениора"))
            {
                return LoadHTMLFromFile("PravilnikSeniori.html");
            }

            return "<div class='alert alert-warning'>Текст за ову категорију није дефинисан.</div>";
        }
        // Možete obrisati ove metode jer sve čitate iz fajlova:
        // private string GeneratePodmladakHTML()
        // private string GenerateJunioriHTML()  
        // private string GenerateSenioriHTML()

        private void SetActiveButton(Button activeButton)
        {
            // Resetuj SVA dugmad eksplicitno
            ResetAllButtons();

            // Postavi aktivno dugme
            activeButton.CssClass = "btn btn-pravilnik active btn-primary";
        }

        private void ResetAllButtons()
        {
            // Lista SVIH dugmadi u sidebar-u
            Button[] allButtons = {
                btnPravilnik1, btnPravilnik2, btnPravilnik3,
                btnBodovanje1, btnBodovanje2, btnBodovanje3,
                btnForma1, btnForma2, btnForma3, btnForma4, brnForma5
            };

            // Resetuj svako dugme
            foreach (Button button in allButtons)
            {
                if (button != null)
                {
                    // Postavi na osnovnu klasu
                    button.CssClass = "btn btn-pravilnik";
                }
            }
        }
    }
}