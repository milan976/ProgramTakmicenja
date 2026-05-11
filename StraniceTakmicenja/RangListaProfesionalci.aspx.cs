using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace ProgramTakmicenja.StraniceTakmicenja
{
    public partial class RangListaProfesionalci : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;
        private string izabranaKategorija = "profesionalci_b_muski"; // Podrazumevana vrednost

        // Enum za kategorije radi bolje tipizacije
        private enum KategorijaProfesionalci
        {
            ProfesionalciBMuski,
            ProfesionalciBZenski,
            ProfesionalciAMuski,
            ProfesionalciAZenski
        }

        // Lista dozvoljenih kategorija za validaciju
        private readonly List<string> dozvoljeneKategorije = new List<string>
        {
            "profesionalci_b_muski",
            "profesionalci_b_zenski",
            "profesionalci_a_muski",
            "profesionalci_a_zenski"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Postavi aktivan tab i podrazumevanu kategoriju
                SetActiveTab("profesionalci_b_muski");
                izabranaKategorija = "profesionalci_b_muski";
                BindRangLista();
                if (lblTrenutnaKategorija != null)
                {
                    lblTrenutnaKategorija.Text = "Професионалци класа Б - Мушки";
                }
            }
            else
            {
                // Čuvanje stanja izabrane kategorije
                // ViewState je sada dostupan jer se Page_Load izvršava posle LoadViewState
                if (ViewState["IzabranaKategorija"] != null)
                {
                    string kategorijaIzViewState = ViewState["IzabranaKategorija"].ToString();

                    // Validacija kategorije iz ViewState
                    if (dozvoljeneKategorije.Contains(kategorijaIzViewState))
                    {
                        izabranaKategorija = kategorijaIzViewState;
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            // Sačuvaj stanje u ViewState
            ViewState["IzabranaKategorija"] = izabranaKategorija;
        }

        // Event handleri za sve 4 taba
        protected void lnkPBMuski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "profesionalci_b_muski";
            SetActiveTab("profesionalci_b_muski");
            if (lblTrenutnaKategorija != null)
            {
                lblTrenutnaKategorija.Text = "Професионалци класа Б - Мушки";
            }
            BindRangLista();
        }

        protected void lnkPBZenski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "profesionalci_b_zenski";
            SetActiveTab("profesionalci_b_zenski");
            if (lblTrenutnaKategorija != null)
            {
                lblTrenutnaKategorija.Text = "Професионалци класа Б - Жене";
            }
            BindRangLista();
        }

        protected void lnkPAMuski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "profesionalci_a_muski";
            SetActiveTab("profesionalci_a_muski");
            if (lblTrenutnaKategorija != null)
            {
                lblTrenutnaKategorija.Text = "Професионалци класа А - Мушки";
            }
            BindRangLista();
        }

        protected void lnkPAZenski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "profesionalci_a_zenski";
            SetActiveTab("profesionalci_a_zenski");
            if (lblTrenutnaKategorija != null)
            {
                lblTrenutnaKategorija.Text = "Професионалци класа А - Жене";
            }
            BindRangLista();
        }

        // Metoda za postavljanje aktivnog taba
        private void SetActiveTab(string tab)
        {
            // Resetuj sve tab-ove (sa proverom null)
            if (lnkPBMuski != null) lnkPBMuski.CssClass = "tab-button";
            if (lnkPBZenski != null) lnkPBZenski.CssClass = "tab-button";
            if (lnkPAMuski != null) lnkPAMuski.CssClass = "tab-button";
            if (lnkPAZenski != null) lnkPAZenski.CssClass = "tab-button";

            // Postavi aktivni tab
            switch (tab)
            {
                case "profesionalci_b_muski":
                    if (lnkPBMuski != null) lnkPBMuski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - Професионалци класа Б - Мушки";
                    break;
                case "profesionalci_b_zenski":
                    if (lnkPBZenski != null) lnkPBZenski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - Професионалци класа Б - Жене";
                    break;
                case "profesionalci_a_muski":
                    if (lnkPAMuski != null) lnkPAMuski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - Професионалци класа А - Мушки";
                    break;
                case "profesionalci_a_zenski":
                    if (lnkPAZenski != null) lnkPAZenski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - Професионалци класа А - Жене";
                    break;
                default:
                    // Ako je nepoznat tab, postavi podrazumevani
                    if (lnkPBMuski != null) lnkPBMuski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - Професионалци класа Б - Мушки";
                    break;
            }
        }

        private void BindRangLista()
        {
            DataTable dt = GetRangListaProfesionalci(izabranaKategorija);

            if (GridViewProfesionalci != null)
            {
                GridViewProfesionalci.DataSource = dt;
                GridViewProfesionalci.DataBind();
                ApplyRangColors();
            }
        }

        private DataTable GetRangListaProfesionalci(string kategorija)
        {
            DataTable dt = new DataTable();

            // Validacija ulaznog parametra
            if (!dozvoljeneKategorije.Contains(kategorija))
            {
                kategorija = "profesionalci_b_muski"; // Podrazumevana vrednost
            }

            string kategorijaVrednost = GetKategorijaValue(kategorija);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Parametrizovani upit za bezbednost
                string query = @"
                    SELECT 
                        e.NazivEkipe,
                        ISNULL(e.PocetniBodovi, 100) as PocetniBodovi,
                        
                        -- Srednja vrednost MVP (glavni sudija ili srednja vrednost)
                        ISNULL((CASE 
                            WHEN d.VremeMVP_GlavniSudija > 0 THEN d.VremeMVP_GlavniSudija
                            WHEN d.VremeMVP_Sudija1 > 0 OR d.VremeMVP_Sudija2 > 0 OR d.VremeMVP_Sudija3 > 0 THEN
                                (ISNULL(d.VremeMVP_Sudija1, 0) + ISNULL(d.VremeMVP_Sudija2, 0) + ISNULL(d.VremeMVP_Sudija3, 0)) / 
                                NULLIF((CASE WHEN ISNULL(d.VremeMVP_Sudija1, 0) > 0 THEN 1 ELSE 0 END +
                                        CASE WHEN ISNULL(d.VremeMVP_Sudija2, 0) > 0 THEN 1 ELSE 0 END +
                                        CASE WHEN ISNULL(d.VremeMVP_Sudija3, 0) > 0 THEN 1 ELSE 0 END), 0)
                            ELSE 0
                        END), 0) as SrednjaVrednostMVP,
                        
                        -- Zbir grešaka za MVP
                        ISNULL(d.Greska2_MVP_Sudija1, 0) + ISNULL(d.Greska2_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska2_MVP_Sudija3, 0) + ISNULL(d.Greska2_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska3_MVP_Sudija1, 0) + ISNULL(d.Greska3_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska3_MVP_Sudija3, 0) + ISNULL(d.Greska3_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska4_MVP_Sudija1, 0) + ISNULL(d.Greska4_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska4_MVP_Sudija3, 0) + ISNULL(d.Greska4_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska5_MVP_Sudija1, 0) + ISNULL(d.Greska5_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska5_MVP_Sudija3, 0) + ISNULL(d.Greska5_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska6_MVP_Sudija1, 0) + ISNULL(d.Greska6_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska6_MVP_Sudija3, 0) + ISNULL(d.Greska6_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska7_MVP_Sudija1, 0) + ISNULL(d.Greska7_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska7_MVP_Sudija3, 0) + ISNULL(d.Greska7_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska8_MVP_Sudija1, 0) + ISNULL(d.Greska8_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska8_MVP_Sudija3, 0) + ISNULL(d.Greska8_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska9_MVP_Sudija1, 0) + ISNULL(d.Greska9_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska9_MVP_Sudija3, 0) + ISNULL(d.Greska9_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska10_MVP_Sudija1, 0) + ISNULL(d.Greska10_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska10_MVP_Sudija3, 0) + ISNULL(d.Greska10_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska11_MVP_Sudija1, 0) + ISNULL(d.Greska11_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska11_MVP_Sudija3, 0) + ISNULL(d.Greska11_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska12_MVP_Sudija1, 0) + ISNULL(d.Greska12_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska12_MVP_Sudija3, 0) + ISNULL(d.Greska12_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska13_MVP_Sudija1, 0) + ISNULL(d.Greska13_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska13_MVP_Sudija3, 0) + ISNULL(d.Greska13_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska14_MVP_Sudija1, 0) + ISNULL(d.Greska14_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska14_MVP_Sudija3, 0) + ISNULL(d.Greska14_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska15_MVP_Sudija1, 0) + ISNULL(d.Greska15_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska15_MVP_Sudija3, 0) + ISNULL(d.Greska15_MVP_GlavniSudija, 0) +
                        ISNULL(d.Greska16_MVP_Sudija1, 0) + ISNULL(d.Greska16_MVP_Sudija2, 0) + 
                        ISNULL(d.Greska16_MVP_Sudija3, 0) + ISNULL(d.Greska16_MVP_GlavniSudija, 0) as ZbirGresakaMVP,
                        
                        -- Srednja vrednost štafete (glavni sudija ili srednja vrednost)
                        ISNULL((CASE 
                            WHEN d.VremeStafetaMVP_GlavniSudija > 0 THEN d.VremeStafetaMVP_GlavniSudija
                            WHEN d.VremeStafetaMVP_SST > 0 OR d.VremeStafetaMVP_SMV > 0 OR d.VremeStafetaMVP_ST > 0 THEN
                                (ISNULL(d.VremeStafetaMVP_SST, 0) + ISNULL(d.VremeStafetaMVP_SMV, 0) + ISNULL(d.VremeStafetaMVP_ST, 0)) / 
                                NULLIF((CASE WHEN ISNULL(d.VremeStafetaMVP_SST, 0) > 0 THEN 1 ELSE 0 END +
                                        CASE WHEN ISNULL(d.VremeStafetaMVP_SMV, 0) > 0 THEN 1 ELSE 0 END +
                                        CASE WHEN ISNULL(d.VremeStafetaMVP_ST, 0) > 0 THEN 1 ELSE 0 END), 0)
                            ELSE 0
                        END), 0) as SrednjaVrednostStafeta,
                        
                        -- Zbir grešaka za štafetu
                        ISNULL(d.Greska2_StafetaMVP_SST, 0) + ISNULL(d.Greska2_StafetaMVP_SMV, 0) + 
                        ISNULL(d.Greska2_StafetaMVP_ST, 0) + ISNULL(d.Greska2_StafetaMVP_GlavniSudija, 0) +
                        ISNULL(d.Greska3_StafetaMVP_SST, 0) + ISNULL(d.Greska3_StafetaMVP_SMV, 0) + 
                        ISNULL(d.Greska3_StafetaMVP_ST, 0) + ISNULL(d.Greska3_StafetaMVP_GlavniSudija, 0) +
                        ISNULL(d.Greska4_StafetaMVP_SST, 0) + ISNULL(d.Greska4_StafetaMVP_SMV, 0) + 
                        ISNULL(d.Greska4_StafetaMVP_ST, 0) + ISNULL(d.Greska4_StafetaMVP_GlavniSudija, 0) +
                        ISNULL(d.Greska5_StafetaMVP_SST, 0) + ISNULL(d.Greska5_StafetaMVP_SMV, 0) + 
                        ISNULL(d.Greska5_StafetaMVP_ST, 0) + ISNULL(d.Greska5_StafetaMVP_GlavniSudija, 0) +
                        ISNULL(d.Greska6_StafetaMVP_SST, 0) + ISNULL(d.Greska6_StafetaMVP_SMV, 0) + 
                        ISNULL(d.Greska6_StafetaMVP_ST, 0) + ISNULL(d.Greska6_StafetaMVP_GlavniSudija, 0) as ZbirGresakaStafeta,
                        
                        -- Ukupan rezultat (početni bodovi minus sve greške)
                        ISNULL(e.PocetniBodovi, 100) - 
                        (ISNULL(d.Greska2_MVP_Sudija1, 0) + ISNULL(d.Greska2_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska2_MVP_Sudija3, 0) + ISNULL(d.Greska2_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska3_MVP_Sudija1, 0) + ISNULL(d.Greska3_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska3_MVP_Sudija3, 0) + ISNULL(d.Greska3_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska4_MVP_Sudija1, 0) + ISNULL(d.Greska4_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska4_MVP_Sudija3, 0) + ISNULL(d.Greska4_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska5_MVP_Sudija1, 0) + ISNULL(d.Greska5_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska5_MVP_Sudija3, 0) + ISNULL(d.Greska5_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska6_MVP_Sudija1, 0) + ISNULL(d.Greska6_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska6_MVP_Sudija3, 0) + ISNULL(d.Greska6_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska7_MVP_Sudija1, 0) + ISNULL(d.Greska7_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska7_MVP_Sudija3, 0) + ISNULL(d.Greska7_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska8_MVP_Sudija1, 0) + ISNULL(d.Greska8_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska8_MVP_Sudija3, 0) + ISNULL(d.Greska8_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska9_MVP_Sudija1, 0) + ISNULL(d.Greska9_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska9_MVP_Sudija3, 0) + ISNULL(d.Greska9_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska10_MVP_Sudija1, 0) + ISNULL(d.Greska10_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska10_MVP_Sudija3, 0) + ISNULL(d.Greska10_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska11_MVP_Sudija1, 0) + ISNULL(d.Greska11_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska11_MVP_Sudija3, 0) + ISNULL(d.Greska11_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska12_MVP_Sudija1, 0) + ISNULL(d.Greska12_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska12_MVP_Sudija3, 0) + ISNULL(d.Greska12_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska13_MVP_Sudija1, 0) + ISNULL(d.Greska13_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska13_MVP_Sudija3, 0) + ISNULL(d.Greska13_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska14_MVP_Sudija1, 0) + ISNULL(d.Greska14_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska14_MVP_Sudija3, 0) + ISNULL(d.Greska14_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska15_MVP_Sudija1, 0) + ISNULL(d.Greska15_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska15_MVP_Sudija3, 0) + ISNULL(d.Greska15_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska16_MVP_Sudija1, 0) + ISNULL(d.Greska16_MVP_Sudija2, 0) + 
                         ISNULL(d.Greska16_MVP_Sudija3, 0) + ISNULL(d.Greska16_MVP_GlavniSudija, 0) +
                         ISNULL(d.Greska2_StafetaMVP_SST, 0) + ISNULL(d.Greska2_StafetaMVP_SMV, 0) + 
                         ISNULL(d.Greska2_StafetaMVP_ST, 0) + ISNULL(d.Greska2_StafetaMVP_GlavniSudija, 0) +
                         ISNULL(d.Greska3_StafetaMVP_SST, 0) + ISNULL(d.Greska3_StafetaMVP_SMV, 0) + 
                         ISNULL(d.Greska3_StafetaMVP_ST, 0) + ISNULL(d.Greska3_StafetaMVP_GlavniSudija, 0) +
                         ISNULL(d.Greska4_StafetaMVP_SST, 0) + ISNULL(d.Greska4_StafetaMVP_SMV, 0) + 
                         ISNULL(d.Greska4_StafetaMVP_ST, 0) + ISNULL(d.Greska4_StafetaMVP_GlavniSudija, 0) +
                         ISNULL(d.Greska5_StafetaMVP_SST, 0) + ISNULL(d.Greska5_StafetaMVP_SMV, 0) + 
                         ISNULL(d.Greska5_StafetaMVP_ST, 0) + ISNULL(d.Greska5_StafetaMVP_GlavniSudija, 0) +
                         ISNULL(d.Greska6_StafetaMVP_SST, 0) + ISNULL(d.Greska6_StafetaMVP_SMV, 0) + 
                         ISNULL(d.Greska6_StafetaMVP_ST, 0) + ISNULL(d.Greska6_StafetaMVP_GlavniSudija, 0)) as UkupanRezultat

                    FROM Ekipe e
                    LEFT JOIN profesionalciRezultati d ON e.EkipaID = d.EkipaID
                    WHERE e.Kategorija = @Kategorija
                    ORDER BY UkupanRezultat DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Kategorija", kategorijaVrednost);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private string GetKategorijaValue(string kategorija)
        {
            switch (kategorija)
            {
                case "profesionalci_b_muski":
                    return "Професионалци класа Б - мушка";
                case "profesionalci_b_zenski":
                    return "Професионалци класа Б - жене";
                case "profesionalci_a_muski":
                    return "Професионалци класа А - мушка";
                case "profesionalci_a_zenski":
                    return "Професионалци класа А - жене";
                default:
                    return "Професионалци класа Б - мушка";
            }
        }

        private void ApplyRangColors()
        {
            if (GridViewProfesionalci != null)
            {
                foreach (GridViewRow row in GridViewProfesionalci.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // Ukloni sve postojeće rang klase
                        string cssClass = row.CssClass ?? "";

                        // Koristimo Regex za sigurno uklanjanje rang klasa
                        System.Text.RegularExpressions.Regex regex =
                            new System.Text.RegularExpressions.Regex(@"\brang-\d+\b");
                        cssClass = regex.Replace(cssClass, "").Trim();

                        // Dodaj odgovarajuću klasu
                        if (row.RowIndex == 0)
                            cssClass += " rang-1";
                        else if (row.RowIndex == 1)
                            cssClass += " rang-2";
                        else if (row.RowIndex == 2)
                            cssClass += " rang-3";

                        row.CssClass = cssClass.Trim();
                    }
                }
            }
        }
    }
}