using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja.StraniceTakmicenja
{
    public partial class RangListaDVD : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;
        private string izabranaKategorija = "dvd_b_muski"; // Podrazumevana vrednost

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Postavi aktivan tab i podrazumevanu kategoriju
                SetActiveTab("dvd_b_muski");
                izabranaKategorija = "dvd_b_muski";
                BindRangLista();
                lblTrenutnaKategorija.Text = "ДВД Б - Мушки";
            }
            else
            {
                // Čuvanje stanja izabrane kategorije
                if (ViewState["IzabranaKategorija"] != null)
                {
                    izabranaKategorija = ViewState["IzabranaKategorija"].ToString();
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
        protected void lnkBMuski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "dvd_b_muski";
            SetActiveTab("dvd_b_muski");
            lblTrenutnaKategorija.Text = "ДВД Б - Мушки";
            BindRangLista();
        }

        protected void lnkBZenski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "dvd_b_zenski";
            SetActiveTab("dvd_b_zenski");
            lblTrenutnaKategorija.Text = "ДВД Б - Жене";
            BindRangLista();
        }

        protected void lnkAMuski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "dvd_a_muski";
            SetActiveTab("dvd_a_muski");
            lblTrenutnaKategorija.Text = "ДВД А - Мушки";
            BindRangLista();
        }

        protected void lnkAZenski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "dvd_a_zenski";
            SetActiveTab("dvd_a_zenski");
            lblTrenutnaKategorija.Text = "ДВД А - Жене";
            BindRangLista();
        }

        // Metoda za postavljanje aktivnog taba
        private void SetActiveTab(string tab)
        {
            // Resetuj sve tab-ove
            lnkBMuski.CssClass = "tab-button";
            lnkBZenski.CssClass = "tab-button";
            lnkAMuski.CssClass = "tab-button";
            lnkAZenski.CssClass = "tab-button";

            // Postavi aktivni tab
            switch (tab)
            {
                case "dvd_b_muski":
                    lnkBMuski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - ДВД Б - Мушки";
                    break;
                case "dvd_b_zenski":
                    lnkBZenski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - ДВД Б - Жене";
                    break;
                case "dvd_a_muski":
                    lnkAMuski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - ДВД А - Мушки";
                    break;
                case "dvd_a_zenski":
                    lnkAZenski.CssClass = "tab-button active";
                    Page.Title = "Ранг листа - ДВД А - Жене";
                    break;
            }
        }

        private void BindRangLista()
        {
            DataTable dt = GetRangListaDVD(izabranaKategorija);
            GridViewDVD.DataSource = dt;
            GridViewDVD.DataBind();
            ApplyRangColors();
        }

        private DataTable GetRangListaDVD(string kategorija)
        {
            DataTable dt = new DataTable();

            string whereClause = GetWhereClauseForKategorija(kategorija);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $@"
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
                    LEFT JOIN dobrovoljciRezultati d ON e.EkipaID = d.EkipaID
                    WHERE {whereClause}
                    ORDER BY UkupanRezultat DESC, SrednjaVrednostMVP ASC, SrednjaVrednostStafeta ASC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private string GetWhereClauseForKategorija(string kategorija)
        {
            switch (kategorija)
            {
                case "dvd_b_muski":
                    return "e.Kategorija = N'ДВД класа Б - мушка'";
                case "dvd_b_zenski":
                    return "e.Kategorija = N'ДВД класа Б - жене'";
                case "dvd_a_muski":
                    return "e.Kategorija = N'ДВД класа А - мушка'";
                case "dvd_a_zenski":
                    return "e.Kategorija = N'ДВД класа А - жене'";
                default:
                    return "e.Kategorija = N'ДВД класа Б - мушка'";
            }
        }

        private void ApplyRangColors()
        {
            foreach (GridViewRow row in GridViewDVD.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Ukloni sve postojeće rang klase
                    string cssClass = row.CssClass;
                    cssClass = cssClass.Replace("rang-1", "");
                    cssClass = cssClass.Replace("rang-2", "");
                    cssClass = cssClass.Replace("rang-3", "");
                    cssClass = cssClass.Trim();

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