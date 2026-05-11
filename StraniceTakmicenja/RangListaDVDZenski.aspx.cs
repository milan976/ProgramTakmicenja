using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja.StraniceTakmicenja
{
    public partial class RangListaDVDZenski : System.Web.UI.Page
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
            DataTable dt = GetRangListaDVD();

            if (dt.Rows.Count > 0)
            {
                GridViewDVD.DataSource = dt;
                GridViewDVD.DataBind();
                ApplyRangColors();
                lblPoruka.Visible = false;
            }
            else
            {
                lblPoruka.Text = "Trenutno nema unetih rezultata za DVD.";
                lblPoruka.Visible = true;
                GridViewDVD.DataSource = null;
                GridViewDVD.DataBind();
            }
        }

        private DataTable GetRangListaDVD()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
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
                    LEFT JOIN dobrovoljciRezultati d ON e.EkipaID = d.EkipaID
                    WHERE e.Kategorija LIKE '%ДВД%'
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

        private void ApplyRangColors()
        {
            foreach (GridViewRow row in GridViewDVD.Rows)
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