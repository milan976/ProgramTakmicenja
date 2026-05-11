using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ProgramTakmicenja.StraniceTakmicenja
{
    public partial class RangListaPodmladakZenski : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRangLista();
            }
        }

        private void BindRangLista()
        {
            try
            {
                DataTable dt = UzmiRangListuZenski();

                if (dt.Rows.Count > 0)
                {
                    GridViewPodmladakZenski.DataSource = dt;
                    GridViewPodmladakZenski.DataBind();
                    ApplyRangColors();
                    ShowStatus($"Приказано {dt.Rows.Count} женских екипа подмладка", "info");
                }
                else
                {
                    ShowStatus("Нема екипа у категорији 'Ватрогасни подмладак - жене'.", "upozorenje");
                    GridViewPodmladakZenski.DataSource = null;
                    GridViewPodmladakZenski.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Грешка: {ex.Message}", "greska");
            }
        }

        private DataTable UzmiRangListuZenski()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY 
                            CASE 
                                WHEN pr.KonacniBodPodmladak IS NOT NULL AND pr.KonacniBodPodmladak > 0 
                                THEN pr.KonacniBodPodmladak 
                                ELSE 0 
                            END DESC) as Rang,
                        e.NazivEkipe,
                        e.Kategorija,
                        ISNULL(pr.PocetniBodovi, e.PocetniBodovi) as PocetniBodovi,
                        
                        CASE 
                            WHEN ISNULL(pr.VremeProsekBrentaca, 0) > 0 
                            THEN 
                                CAST(FLOOR(pr.VremeProsekBrentaca / 60) AS VARCHAR) + ':' + 
                                RIGHT('0' + CAST(ROUND(pr.VremeProsekBrentaca % 60, 2) AS VARCHAR(10)), 5)
                            ELSE '0:00.00'
                        END as VremeVezba,
                        
                        ISNULL(pr.ZbirGresakaBrentaca, 0) as GreskeVezba,
                        '0:00.00' as VremeStafeta,
                        ISNULL(pr.ZbirGresakaStafeta, 0) as GreskeStafeta,
                        ISNULL(pr.KonacniBodPodmladak, 0) as KonacniBodovi,
                        e.EkipaID
                    FROM Ekipe e
                    LEFT JOIN podmladakRezultati pr ON e.EkipaID = pr.EkipaID
                    WHERE e.Kategorija = N'Ватрогасни подмладак - жене'
                    ORDER BY 
                        CASE 
                            WHEN pr.KonacniBodPodmladak IS NOT NULL AND pr.KonacniBodPodmladak > 0 
                            THEN pr.KonacniBodPodmladak 
                            ELSE 0 
                        END DESC";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        private void ApplyRangColors()
        {
            foreach (GridViewRow row in GridViewPodmladakZenski.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.RowIndex == 0) row.CssClass = "rang-1";
                    else if (row.RowIndex == 1) row.CssClass = "rang-2";
                    else if (row.RowIndex == 2) row.CssClass = "rang-3";
                }
            }
        }

        private void ShowStatus(string poruka, string tip)
        {
            pnlStatus.Visible = true;
            lblStatus.Text = poruka;
            pnlStatus.CssClass = "status-poruka " + $"status-{tip}";
        }
    }
}