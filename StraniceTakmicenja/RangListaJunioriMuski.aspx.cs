using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja.StraniceTakmicenja
{
    public partial class RangListaJunioriMuski : System.Web.UI.Page
    {
        private readonly string connectionString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRangLista();
            }
        }

        private void BindRangLista()
        {
            DataTable dt = GetRangListaJuniori();
            GridViewJuniori.DataSource = dt;
            GridViewJuniori.DataBind();
            ApplyRangColors();
        }

        private DataTable GetRangListaJuniori()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        e.NazivEkipe,
                        e.PocetniBodovi,
                        
                        -- Srednja vrednost prepreke (najbolje vreme od svih sudija)
                        ISNULL((
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
                        ), 0) as VremeVezba,
                        
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
                        ISNULL((
                            SELECT MIN(vreme) 
                            FROM (VALUES 
                                (r.VremeStafete_Starter),
                                (r.VremeStafete_Merilac1),
                                (r.VremeStafete_Merilac2),
                                (r.VremeStafete_StazniSudija),
                                (r.VremeStafete_GlavniSudija)
                            ) AS vremena(vreme)
                            WHERE vreme > 0
                        ), 0) as VremeStafeta,
                        
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
                    WHERE e.Kategorija LIKE N'%Јуниори%'
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
                if (row.RowIndex == 0)
                    row.CssClass = "rang-1";
                else if (row.RowIndex == 1)
                    row.CssClass = "rang-2";
                else if (row.RowIndex == 2)
                    row.CssClass = "rang-3";
            }
        }

        protected void btnOsvezi_Click(object sender, EventArgs e)
        {
            BindRangLista();
        }
    }
}