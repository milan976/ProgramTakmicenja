using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ProgramTakmicenja.StraniceTakmicenja
{
    public partial class RangListaPodmladak : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        private string izabranaKategorija = "muski"; // Podrazumevana vrednost

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Postavi aktivan tab i podrazumevanu kategoriju
                SetActiveTab("muski");
                izabranaKategorija = "muski";

                // Inicijalno učitavanje podataka
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

        // Event handler za Sve tab
        protected void lnkSve_Click(object sender, EventArgs e)
        {
            izabranaKategorija = "sve";
            SetActiveTab("sve");
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
            }
            else if (tab == "zenski")
            {
                lnkZenski.CssClass = "tab-button active";
            }
            
            // Ažuriraj naslov stranice
            string naslov = "";
            if (tab == "muski")
                naslov = "Мушки подмладак";
            else if (tab == "zenski")
                naslov = "Женски подмладак";
            else
                naslov = "Подмладак";

            Page.Title = "Ранг листа - " + naslov;
        }

        private void BindRangLista()
        {
            try
            {
                // Proveri stanje za izabranu kategoriju
                var stanje = ProveriDetaljnoStanje(izabranaKategorija);

                System.Diagnostics.Debug.WriteLine($"=== REZULTAT PROVERE ===");
                System.Diagnostics.Debug.WriteLine($"Kategorija: {izabranaKategorija}");
                System.Diagnostics.Debug.WriteLine($"ImaEkipe: {stanje.ImaEkipe}");
                System.Diagnostics.Debug.WriteLine($"ImaRezultata: {stanje.ImaRezultata}");

                if (!stanje.ImaEkipe)
                {
                    
                    GridViewPodmladak.DataSource = null;
                    GridViewPodmladak.DataBind();
                    return;
                }

                if (!stanje.ImaRezultata)
                {
                    
                    GridViewPodmladak.DataSource = null;
                    GridViewPodmladak.DataBind();
                    return;
                }

                DataTable dt = UzmiRangListuIzBaze(izabranaKategorija);

                if (dt != null && dt.Rows.Count > 0)
                {
                    GridViewPodmladak.DataSource = dt;
                    GridViewPodmladak.DataBind();
                    ApplyRangColors();
                }
                else
                {
                    GridViewPodmladak.DataSource = null;
                    GridViewPodmladak.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Greška: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private (bool ImaEkipe, bool ImaRezultata, int BrojEkipa, int BrojRezultata)
            ProveriDetaljnoStanje(string kategorija)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string whereClause = "";
                    if (kategorija == "muski")
                        whereClause = "e.Kategorija = N'Ватрогасни подмладак - мушка'";
                    else if (kategorija == "zenski")
                        whereClause = "e.Kategorija = N'Ватрогасни подмладак - жене'";
                    else
                        whereClause = "(e.Kategorija = N'Ватрогасни подмладак - мушка' OR e.Kategorija = N'Ватрогасни подмладак - жене')";

                    string query = $@"
                        SELECT 
                            COUNT(DISTINCT e.EkipaID) as BrojEkipa,
                            COUNT(DISTINCT pr.EkipaID) as BrojRezultata
                        FROM Ekipe e
                        LEFT JOIN podmladakRezultati pr ON e.EkipaID = pr.EkipaID
                        WHERE {whereClause}";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int brojEkipa = reader["BrojEkipa"] != DBNull.Value ?
                                    Convert.ToInt32(reader["BrojEkipa"]) : 0;

                                int brojRezultata = reader["BrojRezultata"] != DBNull.Value ?
                                    Convert.ToInt32(reader["BrojRezultata"]) : 0;

                                return (brojEkipa > 0, brojRezultata > 0, brojEkipa, brojRezultata);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Greška u ProveriDetaljnoStanje: {ex.Message}");
            }

            return (false, false, 0, 0);
        }

        private DataTable UzmiRangListuIzBaze(string kategorija)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string whereClause = "";
                    if (kategorija == "muski")
                        whereClause = "e.Kategorija = N'Ватрогасни подмладак - мушка'";
                    else if (kategorija == "zenski")
                        whereClause = "e.Kategorija = N'Ватрогасни подмладак - жене'";
                    else
                        whereClause = "(e.Kategorija = N'Ватрогасни подмладак - мушка' OR e.Kategorija = N'Ватрогасни подмладак - жене')";

                    string query = $@"
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
                                    -- Formatiranje vremena: mm:ss.ff
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
                        WHERE {whereClause}
                        ORDER BY 
                            CASE 
                                WHEN pr.KonacniBodPodmladak IS NOT NULL AND pr.KonacniBodPodmladak > 0 
                                THEN pr.KonacniBodPodmladak 
                                ELSE 0 
                            END DESC";

                    System.Diagnostics.Debug.WriteLine($"=== UPIT ZA KATEGORIJU: {kategorija} ===");
                    System.Diagnostics.Debug.WriteLine(query);

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    System.Diagnostics.Debug.WriteLine($"Broj redova u DataTable: {dt.Rows.Count}");

                    if (dt.Rows.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Prvih 5 redova:");
                        for (int i = 0; i < Math.Min(dt.Rows.Count, 5); i++)
                        {
                            DataRow row = dt.Rows[i];
                            System.Diagnostics.Debug.WriteLine($"Red {i}: {row["NazivEkipe"]} - {row["KonacniBodovi"]} bodova");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Greška u UzmiRangListuIzBaze: {ex.Message}");
            }

            return dt;
        }

        private void ApplyRangColors()
        {
            foreach (GridViewRow row in GridViewPodmladak.Rows)
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

        // Event handler za RowDataBound - možete dodati dodatnu logiku
        protected void GridViewPodmladak_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Dodajte dodatnu logiku po potrebi
                // Na primer, formatiranje vremena ili bodova
            }
        }

        
    }
}