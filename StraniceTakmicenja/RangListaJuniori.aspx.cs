using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja.StraniceTakmicenja
{
    public partial class RangListaJuniori : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;
        private string izabranaKategorija = "muski"; // Podrazumevana vrednost

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Postavi aktivan tab i podrazumevanu kategoriju
                SetActiveTab("muski");
                izabranaKategorija = "muski";
                BindRangLista();
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

        // Event handler za Muski tab
        protected void lnkMuski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "muski";
            SetActiveTab("muski");
            BindRangLista();
        }

        // Event handler za Zenski tab
        protected void lnkZenski_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "zenski";
            SetActiveTab("zenski");
            BindRangLista();
        }

        // Metoda za postavljanje aktivnog taba
        private void SetActiveTab(string tab)
        {
            // Resetuj sve tab-ove
            lnkMuski.CssClass = "tab-button";
            lnkZenski.CssClass = "tab-button";

            // Postavi aktivni tab
            if (tab == "muski")
            {
                lnkMuski.CssClass = "tab-button active";
                Page.Title = "Ранг листа - Јуниори мушка";
            }
            else if (tab == "zenski")
            {
                lnkZenski.CssClass = "tab-button active";
                Page.Title = "Ранг листа - Јуниори жене";
            }
        }

        private void BindRangLista()
        {
            DataTable dt = GetRangListaJuniori(izabranaKategorija);
            GridViewJuniori.DataSource = dt;
            GridViewJuniori.DataBind();
            ApplyRangColors();
        }

        private DataTable GetRangListaJuniori(string kategorija)
        {
            DataTable dt = new DataTable();

            string whereClause = "";
            if (kategorija == "muski")
                whereClause = "e.Kategorija = N'Јуниори - мушка'";
            else
                whereClause = "e.Kategorija = N'Јуниори - жене'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $@"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY 
                            CASE 
                                WHEN (
                                    ISNULL(e.PocetniBodovi, 100) - 
                                    (ISNULL(r.Greska2_Prep_Sudija1, 0) + ISNULL(r.Greska2_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska2_Prep_Sudija3, 0) + ISNULL(r.Greska2_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska2_Prep_Sudija5, 0) + ISNULL(r.Greska2_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska3_Prep_Sudija1, 0) + ISNULL(r.Greska3_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska3_Prep_Sudija3, 0) + ISNULL(r.Greska3_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska3_Prep_Sudija5, 0) + ISNULL(r.Greska3_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska4_Prep_Sudija1, 0) + ISNULL(r.Greska4_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska4_Prep_Sudija3, 0) + ISNULL(r.Greska4_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska4_Prep_Sudija5, 0) + ISNULL(r.Greska4_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska5_Prep_Sudija1, 0) + ISNULL(r.Greska5_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska5_Prep_Sudija3, 0) + ISNULL(r.Greska5_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska5_Prep_Sudija5, 0) + ISNULL(r.Greska5_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska6_Prep_Sudija1, 0) + ISNULL(r.Greska6_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska6_Prep_Sudija3, 0) + ISNULL(r.Greska6_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska6_Prep_Sudija5, 0) + ISNULL(r.Greska6_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska7_Prep_Sudija1, 0) + ISNULL(r.Greska7_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska7_Prep_Sudija3, 0) + ISNULL(r.Greska7_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska7_Prep_Sudija5, 0) + ISNULL(r.Greska7_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska8_Prep_Sudija1, 0) + ISNULL(r.Greska8_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska8_Prep_Sudija3, 0) + ISNULL(r.Greska8_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska8_Prep_Sudija5, 0) + ISNULL(r.Greska8_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska9_Prep_Sudija1, 0) + ISNULL(r.Greska9_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska9_Prep_Sudija3, 0) + ISNULL(r.Greska9_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska9_Prep_Sudija5, 0) + ISNULL(r.Greska9_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska10_Prep_Sudija1, 0) + ISNULL(r.Greska10_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska10_Prep_Sudija3, 0) + ISNULL(r.Greska10_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska10_Prep_Sudija5, 0) + ISNULL(r.Greska10_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska11_Prep_Sudija1, 0) + ISNULL(r.Greska11_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska11_Prep_Sudija3, 0) + ISNULL(r.Greska11_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska11_Prep_Sudija5, 0) + ISNULL(r.Greska11_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska2_Stafeta_Starter, 0) + ISNULL(r.Greska2_Stafeta_Merilac1, 0) + 
                                     ISNULL(r.Greska2_Stafeta_Merilac2, 0) + ISNULL(r.Greska2_Stafeta_StazniSudija, 0) + 
                                     ISNULL(r.Greska2_Stafeta_GlavniSudija, 0) +
                                     ISNULL(r.Greska3_Stafeta_Starter, 0) + ISNULL(r.Greska3_Stafeta_Merilac1, 0) + 
                                     ISNULL(r.Greska3_Stafeta_Merilac2, 0) + ISNULL(r.Greska3_Stafeta_StazniSudija, 0) + 
                                     ISNULL(r.Greska3_Stafeta_GlavniSudija, 0) +
                                     ISNULL(r.Greska4_Stafeta_Starter, 0) + ISNULL(r.Greska4_Stafeta_Merilac1, 0) + 
                                     ISNULL(r.Greska4_Stafeta_Merilac2, 0) + ISNULL(r.Greska4_Stafeta_StazniSudija, 0) + 
                                     ISNULL(r.Greska4_Stafeta_GlavniSudija, 0))
                                ) IS NOT NULL 
                                THEN (
                                    ISNULL(e.PocetniBodovi, 100) - 
                                    (ISNULL(r.Greska2_Prep_Sudija1, 0) + ISNULL(r.Greska2_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska2_Prep_Sudija3, 0) + ISNULL(r.Greska2_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska2_Prep_Sudija5, 0) + ISNULL(r.Greska2_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska3_Prep_Sudija1, 0) + ISNULL(r.Greska3_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska3_Prep_Sudija3, 0) + ISNULL(r.Greska3_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska3_Prep_Sudija5, 0) + ISNULL(r.Greska3_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska4_Prep_Sudija1, 0) + ISNULL(r.Greska4_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska4_Prep_Sudija3, 0) + ISNULL(r.Greska4_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska4_Prep_Sudija5, 0) + ISNULL(r.Greska4_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska5_Prep_Sudija1, 0) + ISNULL(r.Greska5_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska5_Prep_Sudija3, 0) + ISNULL(r.Greska5_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska5_Prep_Sudija5, 0) + ISNULL(r.Greska5_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska6_Prep_Sudija1, 0) + ISNULL(r.Greska6_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska6_Prep_Sudija3, 0) + ISNULL(r.Greska6_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska6_Prep_Sudija5, 0) + ISNULL(r.Greska6_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska7_Prep_Sudija1, 0) + ISNULL(r.Greska7_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska7_Prep_Sudija3, 0) + ISNULL(r.Greska7_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska7_Prep_Sudija5, 0) + ISNULL(r.Greska7_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska8_Prep_Sudija1, 0) + ISNULL(r.Greska8_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska8_Prep_Sudija3, 0) + ISNULL(r.Greska8_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska8_Prep_Sudija5, 0) + ISNULL(r.Greska8_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska9_Prep_Sudija1, 0) + ISNULL(r.Greska9_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska9_Prep_Sudija3, 0) + ISNULL(r.Greska9_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska9_Prep_Sudija5, 0) + ISNULL(r.Greska9_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska10_Prep_Sudija1, 0) + ISNULL(r.Greska10_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska10_Prep_Sudija3, 0) + ISNULL(r.Greska10_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska10_Prep_Sudija5, 0) + ISNULL(r.Greska10_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska11_Prep_Sudija1, 0) + ISNULL(r.Greska11_Prep_Sudija2, 0) + 
                                     ISNULL(r.Greska11_Prep_Sudija3, 0) + ISNULL(r.Greska11_Prep_Sudija4, 0) + 
                                     ISNULL(r.Greska11_Prep_Sudija5, 0) + ISNULL(r.Greska11_Prep_GlavniSudija, 0) +
                                     ISNULL(r.Greska2_Stafeta_Starter, 0) + ISNULL(r.Greska2_Stafeta_Merilac1, 0) + 
                                     ISNULL(r.Greska2_Stafeta_Merilac2, 0) + ISNULL(r.Greska2_Stafeta_StazniSudija, 0) + 
                                     ISNULL(r.Greska2_Stafeta_GlavniSudija, 0) +
                                     ISNULL(r.Greska3_Stafeta_Starter, 0) + ISNULL(r.Greska3_Stafeta_Merilac1, 0) + 
                                     ISNULL(r.Greska3_Stafeta_Merilac2, 0) + ISNULL(r.Greska3_Stafeta_StazniSudija, 0) + 
                                     ISNULL(r.Greska3_Stafeta_GlavniSudija, 0) +
                                     ISNULL(r.Greska4_Stafeta_Starter, 0) + ISNULL(r.Greska4_Stafeta_Merilac1, 0) + 
                                     ISNULL(r.Greska4_Stafeta_Merilac2, 0) + ISNULL(r.Greska4_Stafeta_StazniSudija, 0) + 
                                     ISNULL(r.Greska4_Stafeta_GlavniSudija, 0))
                                )
                                ELSE 0 
                            END DESC) as Rang,
                        e.NazivEkipe,
                        e.Kategorija,
                        e.PocetniBodovi,
                        
                        -- Srednja vrednost prepreke (najbolje vreme od svih sudija)
                        CASE 
                            WHEN ISNULL((
                                SELECT MIN(vreme) 
                                FROM (VALUES 
                                    (r.VremePrepreke_Sudija1),
                                    (r.VremePrepreke_Sudija2),
                                    (r.VremePrepreke_Sudija3),
                                    (r.VremePrepreke_Sudija4),
                                    (r.VremePrepreke_Sudija5),
                                    (r.VremePrepreke_GlavniSudija)
                                ) AS vremena(vreme)
                                WHERE vreme > 0
                            ), 0) > 0 
                            THEN 
                                -- Konvertuj sekunde u mm:ss.ff
                                CAST(FLOOR(ISNULL((
                                    SELECT MIN(vreme) 
                                    FROM (VALUES 
                                        (r.VremePrepreke_Sudija1),
                                        (r.VremePrepreke_Sudija2),
                                        (r.VremePrepreke_Sudija3),
                                        (r.VremePrepreke_Sudija4),
                                        (r.VremePrepreke_Sudija5),
                                        (r.VremePrepreke_GlavniSudija)
                                    ) AS vremena(vreme)
                                    WHERE vreme > 0
                                ), 0) / 60) AS VARCHAR) + ':' + 
                                RIGHT('0' + CAST(ROUND(ISNULL((
                                    SELECT MIN(vreme) 
                                    FROM (VALUES 
                                        (r.VremePrepreke_Sudija1),
                                        (r.VremePrepreke_Sudija2),
                                        (r.VremePrepreke_Sudija3),
                                        (r.VremePrepreke_Sudija4),
                                        (r.VremePrepreke_Sudija5),
                                        (r.VremePrepreke_GlavniSudija)
                                    ) AS vremena(vreme)
                                    WHERE vreme > 0
                                ), 0) % 60, 2) AS VARCHAR(10)), 5)
                            ELSE '0:00.00'
                        END as VremeVezba,
                        
                        -- Zbir grešaka za prepreke
                        ISNULL(r.Greska2_Prep_Sudija1, 0) + ISNULL(r.Greska2_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska2_Prep_Sudija3, 0) + ISNULL(r.Greska2_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska2_Prep_Sudija5, 0) + ISNULL(r.Greska2_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska3_Prep_Sudija1, 0) + ISNULL(r.Greska3_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska3_Prep_Sudija3, 0) + ISNULL(r.Greska3_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska3_Prep_Sudija5, 0) + ISNULL(r.Greska3_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska4_Prep_Sudija1, 0) + ISNULL(r.Greska4_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska4_Prep_Sudija3, 0) + ISNULL(r.Greska4_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska4_Prep_Sudija5, 0) + ISNULL(r.Greska4_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska5_Prep_Sudija1, 0) + ISNULL(r.Greska5_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska5_Prep_Sudija3, 0) + ISNULL(r.Greska5_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska5_Prep_Sudija5, 0) + ISNULL(r.Greska5_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska6_Prep_Sudija1, 0) + ISNULL(r.Greska6_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska6_Prep_Sudija3, 0) + ISNULL(r.Greska6_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska6_Prep_Sudija5, 0) + ISNULL(r.Greska6_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska7_Prep_Sudija1, 0) + ISNULL(r.Greska7_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska7_Prep_Sudija3, 0) + ISNULL(r.Greska7_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska7_Prep_Sudija5, 0) + ISNULL(r.Greska7_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska8_Prep_Sudija1, 0) + ISNULL(r.Greska8_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska8_Prep_Sudija3, 0) + ISNULL(r.Greska8_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska8_Prep_Sudija5, 0) + ISNULL(r.Greska8_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska9_Prep_Sudija1, 0) + ISNULL(r.Greska9_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska9_Prep_Sudija3, 0) + ISNULL(r.Greska9_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska9_Prep_Sudija5, 0) + ISNULL(r.Greska9_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska10_Prep_Sudija1, 0) + ISNULL(r.Greska10_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska10_Prep_Sudija3, 0) + ISNULL(r.Greska10_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska10_Prep_Sudija5, 0) + ISNULL(r.Greska10_Prep_GlavniSudija, 0) +
                        ISNULL(r.Greska11_Prep_Sudija1, 0) + ISNULL(r.Greska11_Prep_Sudija2, 0) + 
                        ISNULL(r.Greska11_Prep_Sudija3, 0) + ISNULL(r.Greska11_Prep_Sudija4, 0) + 
                        ISNULL(r.Greska11_Prep_Sudija5, 0) + ISNULL(r.Greska11_Prep_GlavniSudija, 0) as GreskeVezba,
                        
                        -- Srednja vrednost štafete (najbolje vreme od svih merilaca/sudija)
                        CASE 
                            WHEN ISNULL((
                                SELECT MIN(vreme) 
                                FROM (VALUES 
                                    (r.VremeStafete_Starter),
                                    (r.VremeStafete_Merilac1),
                                    (r.VremeStafete_Merilac2),
                                    (r.VremeStafete_StazniSudija),
                                    (r.VremeStafete_GlavniSudija)
                                ) AS vremena(vreme)
                                WHERE vreme > 0
                            ), 0) > 0 
                            THEN 
                                -- Konvertuj sekunde u mm:ss.ff
                                CAST(FLOOR(ISNULL((
                                    SELECT MIN(vreme) 
                                    FROM (VALUES 
                                        (r.VremeStafete_Starter),
                                        (r.VremeStafete_Merilac1),
                                        (r.VremeStafete_Merilac2),
                                        (r.VremeStafete_StazniSudija),
                                        (r.VremeStafete_GlavniSudija)
                                    ) AS vremena(vreme)
                                    WHERE vreme > 0
                                ), 0) / 60) AS VARCHAR) + ':' + 
                                RIGHT('0' + CAST(ROUND(ISNULL((
                                    SELECT MIN(vreme) 
                                    FROM (VALUES 
                                        (r.VremeStafete_Starter),
                                        (r.VremeStafete_Merilac1),
                                        (r.VremeStafete_Merilac2),
                                        (r.VremeStafete_StazniSudija),
                                        (r.VremeStafete_GlavniSudija)
                                    ) AS vremena(vreme)
                                    WHERE vreme > 0
                                ), 0) % 60, 2) AS VARCHAR(10)), 5)
                            ELSE '0:00.00'
                        END as VremeStafeta,
                        
                        -- Zbir grešaka za štafetu
                        ISNULL(r.Greska2_Stafeta_Starter, 0) + ISNULL(r.Greska2_Stafeta_Merilac1, 0) + 
                        ISNULL(r.Greska2_Stafeta_Merilac2, 0) + ISNULL(r.Greska2_Stafeta_StazniSudija, 0) + 
                        ISNULL(r.Greska2_Stafeta_GlavniSudija, 0) +
                        ISNULL(r.Greska3_Stafeta_Starter, 0) + ISNULL(r.Greska3_Stafeta_Merilac1, 0) + 
                        ISNULL(r.Greska3_Stafeta_Merilac2, 0) + ISNULL(r.Greska3_Stafeta_StazniSudija, 0) + 
                        ISNULL(r.Greska3_Stafeta_GlavniSudija, 0) +
                        ISNULL(r.Greska4_Stafeta_Starter, 0) + ISNULL(r.Greska4_Stafeta_Merilac1, 0) + 
                        ISNULL(r.Greska4_Stafeta_Merilac2, 0) + ISNULL(r.Greska4_Stafeta_StazniSudija, 0) + 
                        ISNULL(r.Greska4_Stafeta_GlavniSudija, 0) as GreskeStafeta,
                        
                        -- Konačni bodovi (početni bodovi minus greške)
                        ISNULL(e.PocetniBodovi, 100) - 
                        (ISNULL(r.Greska2_Prep_Sudija1, 0) + ISNULL(r.Greska2_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska2_Prep_Sudija3, 0) + ISNULL(r.Greska2_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska2_Prep_Sudija5, 0) + ISNULL(r.Greska2_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska3_Prep_Sudija1, 0) + ISNULL(r.Greska3_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska3_Prep_Sudija3, 0) + ISNULL(r.Greska3_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska3_Prep_Sudija5, 0) + ISNULL(r.Greska3_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska4_Prep_Sudija1, 0) + ISNULL(r.Greska4_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska4_Prep_Sudija3, 0) + ISNULL(r.Greska4_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska4_Prep_Sudija5, 0) + ISNULL(r.Greska4_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska5_Prep_Sudija1, 0) + ISNULL(r.Greska5_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska5_Prep_Sudija3, 0) + ISNULL(r.Greska5_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska5_Prep_Sudija5, 0) + ISNULL(r.Greska5_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska6_Prep_Sudija1, 0) + ISNULL(r.Greska6_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska6_Prep_Sudija3, 0) + ISNULL(r.Greska6_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska6_Prep_Sudija5, 0) + ISNULL(r.Greska6_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska7_Prep_Sudija1, 0) + ISNULL(r.Greska7_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska7_Prep_Sudija3, 0) + ISNULL(r.Greska7_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska7_Prep_Sudija5, 0) + ISNULL(r.Greska7_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska8_Prep_Sudija1, 0) + ISNULL(r.Greska8_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska8_Prep_Sudija3, 0) + ISNULL(r.Greska8_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska8_Prep_Sudija5, 0) + ISNULL(r.Greska8_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska9_Prep_Sudija1, 0) + ISNULL(r.Greska9_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska9_Prep_Sudija3, 0) + ISNULL(r.Greska9_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska9_Prep_Sudija5, 0) + ISNULL(r.Greska9_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska10_Prep_Sudija1, 0) + ISNULL(r.Greska10_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska10_Prep_Sudija3, 0) + ISNULL(r.Greska10_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska10_Prep_Sudija5, 0) + ISNULL(r.Greska10_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska11_Prep_Sudija1, 0) + ISNULL(r.Greska11_Prep_Sudija2, 0) + 
                         ISNULL(r.Greska11_Prep_Sudija3, 0) + ISNULL(r.Greska11_Prep_Sudija4, 0) + 
                         ISNULL(r.Greska11_Prep_Sudija5, 0) + ISNULL(r.Greska11_Prep_GlavniSudija, 0) +
                         ISNULL(r.Greska2_Stafeta_Starter, 0) + ISNULL(r.Greska2_Stafeta_Merilac1, 0) + 
                         ISNULL(r.Greska2_Stafeta_Merilac2, 0) + ISNULL(r.Greska2_Stafeta_StazniSudija, 0) + 
                         ISNULL(r.Greska2_Stafeta_GlavniSudija, 0) +
                         ISNULL(r.Greska3_Stafeta_Starter, 0) + ISNULL(r.Greska3_Stafeta_Merilac1, 0) + 
                         ISNULL(r.Greska3_Stafeta_Merilac2, 0) + ISNULL(r.Greska3_Stafeta_StazniSudija, 0) + 
                         ISNULL(r.Greska3_Stafeta_GlavniSudija, 0) +
                         ISNULL(r.Greska4_Stafeta_Starter, 0) + ISNULL(r.Greska4_Stafeta_Merilac1, 0) + 
                         ISNULL(r.Greska4_Stafeta_Merilac2, 0) + ISNULL(r.Greska4_Stafeta_StazniSudija, 0) + 
                         ISNULL(r.Greska4_Stafeta_GlavniSudija, 0)) as KonacniBodovi

                    FROM Ekipe e
                    LEFT JOIN junioriRezultati r ON e.EkipaID = r.EkipaID
                    WHERE {whereClause}
                    ORDER BY KonacniBodovi DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private void ApplyRangColors()
        {
            foreach (GridViewRow row in GridViewJuniori.Rows)
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