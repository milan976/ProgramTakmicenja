using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja.StraniceTakmicenja.Kategorije
{
    public partial class Podmladak : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEkipePoKategoriji("подмладак");
            }
        }

        protected void ddlFilterEkipa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFilterEkipa.SelectedValue == "0")
            {
                lblPocetniBodovi.Text = "Почетни бодови: -";
                OcistiFormu();
                return;
            }

            try
            {
                UcitajPocetneBodove();
                UcitajPodatkeZaEkipu(ddlFilterEkipa.SelectedValue);

                // Pokreni JavaScript da ažurira ukupan rezultat
                ScriptManager.RegisterStartupScript(this, this.GetType(), "initPodmladak", "initPodmladak();", true);
            }
            catch (Exception ex)
            {
                lblPoruka.Text = $"Грешка при учитавању података: {ex.Message}";
                lblPoruka.CssClass = "text-danger";
            }
        }
        protected void IzracunajSrednjuVrednost(object sender, EventArgs e)
        {
            try
            {
                decimal suma = 0;
                int brojValidnih = 0;

                // Provera i parsiranje SST
                if (decimal.TryParse(VremeBrentaca_SST.Text, out decimal vremeSST))
                {
                    suma += vremeSST;
                    brojValidnih++;
                }

                // Provera i parsiranje SMV
                if (decimal.TryParse(VremeBrentaca_SMV.Text, out decimal vremeSMV))
                {
                    suma += vremeSMV;
                    brojValidnih++;
                }

                // Provera i parsiranje GS
                if (decimal.TryParse(VremeBrentaca_GS.Text, out decimal vremeGS))
                {
                    suma += vremeGS;
                    brojValidnih++;
                }

                if (brojValidnih > 0)
                {
                    decimal srednjaVrednost = suma / brojValidnih;
                    lblSrednjaVrednost.Text = $"Средња вредност времена брентача: {srednjaVrednost:F2}";
                    lblSrednjaVrednost.CssClass = "text-success";
                }
                else
                {
                    lblSrednjaVrednost.Text = "Средња вредност: Унесите бар једно време";
                    lblSrednjaVrednost.CssClass = "text-warning";
                }
            }
            catch (Exception)
            {
                lblSrednjaVrednost.Text = "Грешка при израчунавању средње вредности";
                lblSrednjaVrednost.CssClass = "text-danger";
            }
        }
        private void UcitajPocetneBodove()
        {
            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT PocetniBodovi FROM Ekipe WHERE EkipaID = @EkipaID";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EkipaID", ddlFilterEkipa.SelectedValue);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    lblPocetniBodovi.Text = $"Почетни бодови: {result.ToString()}";
                }
                else
                {
                    lblPocetniBodovi.Text = "Почетни бодови: -";
                }
            }
        }
        private void BindEkipePoKategoriji(string kategorija)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string query = @"SELECT EkipaID, NazivEkipe, Kategorija, 
                        ISNULL(PocetniBodovi, 0) AS PocetniBodovi 
                        FROM Ekipe 
                        WHERE Kategorija LIKE '%' + @kategorija + '%' 
                        ORDER BY NazivEkipe";

                using (SqlConnection con = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@kategorija", kategorija);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlFilterEkipa.Items.Clear();
                        ddlFilterEkipa.Items.Add(new ListItem("-- Изаберите екипу --", "0"));

                        while (reader.Read())
                        {
                            string id = reader["EkipaID"].ToString();
                            string naziv = reader["NazivEkipe"].ToString();
                            string bodovi = "0";

                            try
                            {
                                bodovi = reader["PocetniBodovi"] != DBNull.Value ?
                                        reader["PocetniBodovi"].ToString() : "0";
                            }
                            catch (IndexOutOfRangeException)
                            {
                                // Колона не постоји, користимо подразумевану вредност
                            }

                            string kategorijaEkipe = reader["Kategorija"].ToString();
                            ddlFilterEkipa.Items.Add(new ListItem($"{naziv} ({kategorijaEkipe})", id));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblPoruka.Text = $"Грешка при учитавању екипа: {ex.Message}";
                lblPoruka.CssClass = "text-danger";
            }
        }
        private void UcitajPodatkeZaEkipu(string ekipaID)
        {
            if (ekipaID == "0")
            {
                OcistiFormu();
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string query = "SELECT * FROM podmladakRezultati WHERE EkipaID = @EkipaID";

                using (SqlConnection con = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EkipaID", ekipaID);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Popunjavanje polja sa podacima iz baze
                            PostaviVrednostPolja(reader, "VremeBrentaca_SST", VremeBrentaca_SST);
                            PostaviVrednostPolja(reader, "VremeBrentaca_SMV", VremeBrentaca_SMV);
                            PostaviVrednostPolja(reader, "VremeBrentaca_GS", VremeBrentaca_GS);

                            // Brentac SST greške
                            PostaviVrednostPolja(reader, "Greska2_Brentaca_SST", Greska2_Brentaca_SST);
                            PostaviVrednostPolja(reader, "Greska3_Brentaca_SST", Greska3_Brentaca_SST);
                            PostaviVrednostPolja(reader, "Greska4_Brentaca_SST", Greska4_Brentaca_SST);
                            PostaviVrednostPolja(reader, "Greska5_Brentaca_SST", Greska5_Brentaca_SST);
                            PostaviVrednostPolja(reader, "Greska6_Brentaca_SST", Greska6_Brentaca_SST);
                            PostaviVrednostPolja(reader, "Greska7_Brentaca_SST", Greska7_Brentaca_SST);
                            PostaviVrednostPolja(reader, "Greska8_Brentaca_SST", Greska8_Brentaca_SST);
                            PostaviVrednostPolja(reader, "Greska9_Brentaca_SST", Greska9_Brentaca_SST);

                            // Brentac SMV greške
                            PostaviVrednostPolja(reader, "Greska2_Brentaca_SMV", Greska2_Brentaca_SMV);
                            PostaviVrednostPolja(reader, "Greska3_Brentaca_SMV", Greska3_Brentaca_SMV);
                            PostaviVrednostPolja(reader, "Greska4_Brentaca_SMV", Greska4_Brentaca_SMV);
                            PostaviVrednostPolja(reader, "Greska5_Brentaca_SMV", Greska5_Brentaca_SMV);
                            PostaviVrednostPolja(reader, "Greska6_Brentaca_SMV", Greska6_Brentaca_SMV);
                            PostaviVrednostPolja(reader, "Greska7_Brentaca_SMV", Greska7_Brentaca_SMV);
                            PostaviVrednostPolja(reader, "Greska8_Brentaca_SMV", Greska8_Brentaca_SMV);
                            PostaviVrednostPolja(reader, "Greska9_Brentaca_SMV", Greska9_Brentaca_SMV);

                            // Brentac GS greške
                            PostaviVrednostPolja(reader, "Greska2_Brentaca_GS", Greska2_Brentaca_GS);
                            PostaviVrednostPolja(reader, "Greska3_Brentaca_GS", Greska3_Brentaca_GS);
                            PostaviVrednostPolja(reader, "Greska4_Brentaca_GS", Greska4_Brentaca_GS);
                            PostaviVrednostPolja(reader, "Greska5_Brentaca_GS", Greska5_Brentaca_GS);
                            PostaviVrednostPolja(reader, "Greska6_Brentaca_GS", Greska6_Brentaca_GS);
                            PostaviVrednostPolja(reader, "Greska7_Brentaca_GS", Greska7_Brentaca_GS);
                            PostaviVrednostPolja(reader, "Greska8_Brentaca_GS", Greska8_Brentaca_GS);
                            PostaviVrednostPolja(reader, "Greska9_Brentaca_GS", Greska9_Brentaca_GS);

                            // PV SST greške
                            PostaviVrednostPolja(reader, "Greska1_PV_SST", Greska1_PV_SST);
                            PostaviVrednostPolja(reader, "Greska2_PV_SST", Greska2_PV_SST);
                            PostaviVrednostPolja(reader, "Greska3_PV_SST", Greska3_PV_SST);
                            PostaviVrednostPolja(reader, "Greska4_PV_SST", Greska4_PV_SST);
                            PostaviVrednostPolja(reader, "Greska5_PV_SST", Greska5_PV_SST);
                            PostaviVrednostPolja(reader, "Greska6_PV_SST", Greska6_PV_SST);

                            // PV SMV greške
                            PostaviVrednostPolja(reader, "Greska1_PV_SMV", Greska1_PV_SMV);
                            PostaviVrednostPolja(reader, "Greska2_PV_SMV", Greska2_PV_SMV);
                            PostaviVrednostPolja(reader, "Greska3_PV_SMV", Greska3_PV_SMV);
                            PostaviVrednostPolja(reader, "Greska4_PV_SMV", Greska4_PV_SMV);
                            PostaviVrednostPolja(reader, "Greska5_PV_SMV", Greska5_PV_SMV);
                            PostaviVrednostPolja(reader, "Greska6_PV_SMV", Greska6_PV_SMV);

                            // PV GS greške
                            PostaviVrednostPolja(reader, "Greska1_PV_GS", Greska1_PV_GS);
                            PostaviVrednostPolja(reader, "Greska2_PV_GS", Greska2_PV_GS);
                            PostaviVrednostPolja(reader, "Greska3_PV_GS", Greska3_PV_GS);
                            PostaviVrednostPolja(reader, "Greska4_PV_GS", Greska4_PV_GS);
                            PostaviVrednostPolja(reader, "Greska5_PV_GS", Greska5_PV_GS);
                            PostaviVrednostPolja(reader, "Greska6_PV_GS", Greska6_PV_GS);
                        }
                        else
                        {
                            OcistiFormu();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblPoruka.Text = $"Грешка при учитавању података екипе: {ex.Message}";
                lblPoruka.CssClass = "text-danger";
                OcistiFormu();
            }
        }

        private void PostaviVrednostPolja(SqlDataReader reader, string fieldName, TextBox textBox)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                {
                    object value = reader[fieldName];
                    textBox.Text = value.ToString();
                }
                else
                {
                    textBox.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                textBox.Text = string.Empty;
            }
        }

        
        private void OcistiFormu()
        {
            // Brentac vremena
            VremeBrentaca_SST.Text = string.Empty;
            VremeBrentaca_SMV.Text = string.Empty;
            VremeBrentaca_GS.Text = string.Empty;

            // Brentac SST greške
            Greska2_Brentaca_SST.Text = string.Empty;
            Greska3_Brentaca_SST.Text = string.Empty;
            Greska4_Brentaca_SST.Text = string.Empty;
            Greska5_Brentaca_SST.Text = string.Empty;
            Greska6_Brentaca_SST.Text = string.Empty;
            Greska7_Brentaca_SST.Text = string.Empty;
            Greska8_Brentaca_SST.Text = string.Empty;
            Greska9_Brentaca_SST.Text = string.Empty;

            // Brentac SMV greške
            Greska2_Brentaca_SMV.Text = string.Empty;
            Greska3_Brentaca_SMV.Text = string.Empty;
            Greska4_Brentaca_SMV.Text = string.Empty;
            Greska5_Brentaca_SMV.Text = string.Empty;
            Greska6_Brentaca_SMV.Text = string.Empty;
            Greska7_Brentaca_SMV.Text = string.Empty;
            Greska8_Brentaca_SMV.Text = string.Empty;
            Greska9_Brentaca_SMV.Text = string.Empty;

            // Brentac GS greške
            Greska2_Brentaca_GS.Text = string.Empty;
            Greska3_Brentaca_GS.Text = string.Empty;
            Greska4_Brentaca_GS.Text = string.Empty;
            Greska5_Brentaca_GS.Text = string.Empty;
            Greska6_Brentaca_GS.Text = string.Empty;
            Greska7_Brentaca_GS.Text = string.Empty;
            Greska8_Brentaca_GS.Text = string.Empty;
            Greska9_Brentaca_GS.Text = string.Empty;

            // PV SST greške
            Greska1_PV_SST.Text = string.Empty;
            Greska2_PV_SST.Text = string.Empty;
            Greska3_PV_SST.Text = string.Empty;
            Greska4_PV_SST.Text = string.Empty;
            Greska5_PV_SST.Text = string.Empty;
            Greska6_PV_SST.Text = string.Empty;

            // PV SMV greške
            Greska1_PV_SMV.Text = string.Empty;
            Greska2_PV_SMV.Text = string.Empty;
            Greska3_PV_SMV.Text = string.Empty;
            Greska4_PV_SMV.Text = string.Empty;
            Greska5_PV_SMV.Text = string.Empty;
            Greska6_PV_SMV.Text = string.Empty;

            // PV GS greške
            Greska1_PV_GS.Text = string.Empty;
            Greska2_PV_GS.Text = string.Empty;
            Greska3_PV_GS.Text = string.Empty;
            Greska4_PV_GS.Text = string.Empty;
            Greska5_PV_GS.Text = string.Empty;
            Greska6_PV_GS.Text = string.Empty;

            lblSrednjaVrednost.Text = string.Empty;
        }

        protected void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (ddlFilterEkipa.SelectedValue == "0")
            {
                lblPoruka.Text = "Морате изабрати екипу!";
                lblPoruka.CssClass = "text-danger";
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();

                    // Provera da li zapis već postoji
                    string checkQuery = "SELECT COUNT(*) FROM podmladakRezultati WHERE EkipaID = @EkipaID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@EkipaID", ddlFilterEkipa.SelectedValue);
                        int exists = (int)checkCmd.ExecuteScalar();

                        // Izračunaj sve vrednosti pre čuvanja
                        decimal vremeSST = GetDecimalValue(VremeBrentaca_SST.Text);
                        decimal vremeSMV = GetDecimalValue(VremeBrentaca_SMV.Text);
                        decimal vremeGS = GetDecimalValue(VremeBrentaca_GS.Text);

                        // Izračunaj prosečno vreme
                        decimal prosekVremena = 0;
                        int brojVremena = 0;

                        if (vremeSST > 0) { prosekVremena += vremeSST; brojVremena++; }
                        if (vremeSMV > 0) { prosekVremena += vremeSMV; brojVremena++; }
                        if (vremeGS > 0) { prosekVremena += vremeGS; brojVremena++; }

                        if (brojVremena > 0)
                            prosekVremena /= brojVremena;

                        // Izračunaj greške
                        decimal zbirGresakaBrentaca = IzracunajZbirGresakaBrentaca();
                        decimal zbirGresakaStafeta = IzracunajZbirGresakaStafeta();

                        // Uzmi početne bodove iz Ekipe tabele
                        int pocetniBodovi = UzmiPocetneBodoveIzEkipe(ddlFilterEkipa.SelectedValue);

                        // Izračunaj konačne bodove
                        int konacniBodovi = pocetniBodovi - (int)zbirGresakaBrentaca - (int)zbirGresakaStafeta;
                        if (konacniBodovi < 0) konacniBodovi = 0;

                        // UPDATE ili INSERT upit
                        string query;
                        if (exists > 0)
                        {
                            query = @"UPDATE podmladakRezultati SET
                        VremeProsekBrentaca = @VremeProsekBrentaca,
                        NegBrentaca = @NegBrentaca,
                        VremeBrentaca_SST = @VremeBrentaca_SST,
                        VremeBrentaca_SMV = @VremeBrentaca_SMV,
                        VremeBrentaca_GS = @VremeBrentaca_GS,
                        Greska2_Brentaca_SST = @Greska2_Brentaca_SST,
                        Greska3_Brentaca_SST = @Greska3_Brentaca_SST,
                        Greska4_Brentaca_SST = @Greska4_Brentaca_SST,
                        Greska5_Brentaca_SST = @Greska5_Brentaca_SST,
                        Greska6_Brentaca_SST = @Greska6_Brentaca_SST,
                        Greska7_Brentaca_SST = @Greska7_Brentaca_SST,
                        Greska8_Brentaca_SST = @Greska8_Brentaca_SST,
                        Greska9_Brentaca_SST = @Greska9_Brentaca_SST,
                        Greska2_Brentaca_SMV = @Greska2_Brentaca_SMV,
                        Greska3_Brentaca_SMV = @Greska3_Brentaca_SMV,
                        Greska4_Brentaca_SMV = @Greska4_Brentaca_SMV,
                        Greska5_Brentaca_SMV = @Greska5_Brentaca_SMV,
                        Greska6_Brentaca_SMV = @Greska6_Brentaca_SMV,
                        Greska7_Brentaca_SMV = @Greska7_Brentaca_SMV,
                        Greska8_Brentaca_SMV = @Greska8_Brentaca_SMV,
                        Greska9_Brentaca_SMV = @Greska9_Brentaca_SMV,
                        Greska2_Brentaca_GS = @Greska2_Brentaca_GS,
                        Greska3_Brentaca_GS = @Greska3_Brentaca_GS,
                        Greska4_Brentaca_GS = @Greska4_Brentaca_GS,
                        Greska5_Brentaca_GS = @Greska5_Brentaca_GS,
                        Greska6_Brentaca_GS = @Greska6_Brentaca_GS,
                        Greska7_Brentaca_GS = @Greska7_Brentaca_GS,
                        Greska8_Brentaca_GS = @Greska8_Brentaca_GS,
                        Greska9_Brentaca_GS = @Greska9_Brentaca_GS,
                        Greska1_PV_SST = @Greska1_PV_SST,
                        Greska2_PV_SST = @Greska2_PV_SST,
                        Greska3_PV_SST = @Greska3_PV_SST,
                        Greska4_PV_SST = @Greska4_PV_SST,
                        Greska5_PV_SST = @Greska5_PV_SST,
                        Greska6_PV_SST = @Greska6_PV_SST,
                        Greska1_PV_SMV = @Greska1_PV_SMV,
                        Greska2_PV_SMV = @Greska2_PV_SMV,
                        Greska3_PV_SMV = @Greska3_PV_SMV,
                        Greska4_PV_SMV = @Greska4_PV_SMV,
                        Greska5_PV_SMV = @Greska5_PV_SMV,
                        Greska6_PV_SMV = @Greska6_PV_SMV,
                        Greska1_PV_GS = @Greska1_PV_GS,
                        Greska2_PV_GS = @Greska2_PV_GS,
                        Greska3_PV_GS = @Greska3_PV_GS,
                        Greska4_PV_GS = @Greska4_PV_GS,
                        Greska5_PV_GS = @Greska5_PV_GS,
                        Greska6_PV_GS = @Greska6_PV_GS,
                        ZbirGresakaBrentaca = @ZbirGresakaBrentaca,
                        ZbirGresakaStafeta = @ZbirGresakaStafeta,
                        PocetniBodovi = @PocetniBodovi,
                        KonacniBodPodmladak = @KonacniBodPodmladak
                        WHERE EkipaID = @EkipaID";
                        }
                        else
                        {
                            query = @"INSERT INTO podmladakRezultati
                        (EkipaID, VremeProsekBrentaca, NegBrentaca, VremeBrentaca_SST, VremeBrentaca_SMV, VremeBrentaca_GS, 
                        Greska2_Brentaca_SST, Greska3_Brentaca_SST, Greska4_Brentaca_SST, Greska5_Brentaca_SST, 
                        Greska6_Brentaca_SST, Greska7_Brentaca_SST, Greska8_Brentaca_SST, Greska9_Brentaca_SST,
                        Greska2_Brentaca_SMV, Greska3_Brentaca_SMV, Greska4_Brentaca_SMV, Greska5_Brentaca_SMV,
                        Greska6_Brentaca_SMV, Greska7_Brentaca_SMV, Greska8_Brentaca_SMV, Greska9_Brentaca_SMV,
                        Greska2_Brentaca_GS, Greska3_Brentaca_GS, Greska4_Brentaca_GS, Greska5_Brentaca_GS,
                        Greska6_Brentaca_GS, Greska7_Brentaca_GS, Greska8_Brentaca_GS, Greska9_Brentaca_GS,
                        Greska1_PV_SST, Greska2_PV_SST, Greska3_PV_SST, Greska4_PV_SST, Greska5_PV_SST, Greska6_PV_SST,
                        Greska1_PV_SMV, Greska2_PV_SMV, Greska3_PV_SMV, Greska4_PV_SMV, Greska5_PV_SMV, Greska6_PV_SMV,
                        Greska1_PV_GS, Greska2_PV_GS, Greska3_PV_GS, Greska4_PV_GS, Greska5_PV_GS, Greska6_PV_GS,
                        ZbirGresakaBrentaca, ZbirGresakaStafeta, PocetniBodovi, KonacniBodPodmladak)
                        VALUES 
                        (@EkipaID, @VremeProsekBrentaca, @NegBrentaca, @VremeBrentaca_SST, @VremeBrentaca_SMV, @VremeBrentaca_GS, 
                        @Greska2_Brentaca_SST, @Greska3_Brentaca_SST, @Greska4_Brentaca_SST, @Greska5_Brentaca_SST, 
                        @Greska6_Brentaca_SST, @Greska7_Brentaca_SST, @Greska8_Brentaca_SST, @Greska9_Brentaca_SST,
                        @Greska2_Brentaca_SMV, @Greska3_Brentaca_SMV, @Greska4_Brentaca_SMV, @Greska5_Brentaca_SMV,
                        @Greska6_Brentaca_SMV, @Greska7_Brentaca_SMV, @Greska8_Brentaca_SMV, @Greska9_Brentaca_SMV,
                        @Greska2_Brentaca_GS, @Greska3_Brentaca_GS, @Greska4_Brentaca_GS, @Greska5_Brentaca_GS,
                        @Greska6_Brentaca_GS, @Greska7_Brentaca_GS, @Greska8_Brentaca_GS, @Greska9_Brentaca_GS,
                        @Greska1_PV_SST, @Greska2_PV_SST, @Greska3_PV_SST, @Greska4_PV_SST, @Greska5_PV_SST, @Greska6_PV_SST,
                        @Greska1_PV_SMV, @Greska2_PV_SMV, @Greska3_PV_SMV, @Greska4_PV_SMV, @Greska5_PV_SMV, @Greska6_PV_SMV,
                        @Greska1_PV_GS, @Greska2_PV_GS, @Greska3_PV_GS, @Greska4_PV_GS, @Greska5_PV_GS, @Greska6_PV_GS,
                        @ZbirGresakaBrentaca, @ZbirGresakaStafeta, @PocetniBodovi, @KonacniBodPodmladak)";
                        }

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Postavi osnovne parametre
                            cmd.Parameters.AddWithValue("@EkipaID", ddlFilterEkipa.SelectedValue);
                            cmd.Parameters.AddWithValue("@VremeProsekBrentaca", prosekVremena);
                            cmd.Parameters.AddWithValue("@NegBrentaca", zbirGresakaBrentaca);

                            // Dodaj nove parametre za izračunate vrednosti
                            cmd.Parameters.AddWithValue("@ZbirGresakaBrentaca", zbirGresakaBrentaca);
                            cmd.Parameters.AddWithValue("@ZbirGresakaStafeta", zbirGresakaStafeta);
                            cmd.Parameters.AddWithValue("@PocetniBodovi", pocetniBodovi);
                            cmd.Parameters.AddWithValue("@KonacniBodPodmladak", konacniBodovi);

                            // Dodaj ostale parametre
                            DodajParametreKomande(cmd);

                            cmd.ExecuteNonQuery();

                            // Ažuriraj statistiku na stranici
                            AzurirajStatistiku(zbirGresakaBrentaca, zbirGresakaStafeta, pocetniBodovi, konacniBodovi);

                            lblPoruka.Text = "Резултати успешно сачувани!";
                            lblPoruka.CssClass = "text-success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblPoruka.Text = "Грешка при чувању: " + ex.Message;
                lblPoruka.CssClass = "text-danger";
            }
        }
        private decimal IzracunajZbirGresakaStafeta()
        {
            decimal zbir = 0;

            // SST greške za stafetu
            zbir += GetDecimalValue(Greska1_PV_SST.Text);
            zbir += GetDecimalValue(Greska2_PV_SST.Text) * 2;
            zbir += GetDecimalValue(Greska3_PV_SST.Text) * 2;
            zbir += GetDecimalValue(Greska4_PV_SST.Text) * 5;
            zbir += GetDecimalValue(Greska5_PV_SST.Text) * 10;
            zbir += GetDecimalValue(Greska6_PV_SST.Text) * 2;

            // SMV greške za stafetu
            zbir += GetDecimalValue(Greska1_PV_SMV.Text);
            zbir += GetDecimalValue(Greska2_PV_SMV.Text) * 2;
            zbir += GetDecimalValue(Greska3_PV_SMV.Text) * 2;
            zbir += GetDecimalValue(Greska4_PV_SMV.Text) * 5;
            zbir += GetDecimalValue(Greska5_PV_SMV.Text) * 10;
            zbir += GetDecimalValue(Greska6_PV_SMV.Text) * 2;

            // GS greške za stafetu
            zbir += GetDecimalValue(Greska1_PV_GS.Text);
            zbir += GetDecimalValue(Greska2_PV_GS.Text) * 2;
            zbir += GetDecimalValue(Greska3_PV_GS.Text) * 2;
            zbir += GetDecimalValue(Greska4_PV_GS.Text) * 5;
            zbir += GetDecimalValue(Greska5_PV_GS.Text) * 10;
            zbir += GetDecimalValue(Greska6_PV_GS.Text) * 2;

            return zbir;
        }

        private int UzmiPocetneBodoveIzEkipe(string ekipaID)
        {
            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT ISNULL(PocetniBodovi, 0) FROM Ekipe WHERE EkipaID = @EkipaID";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);
                con.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private void AzurirajStatistiku(decimal zbirGresakaBrentaca, decimal zbirGresakaStafeta, int pocetniBodovi, int konacniBodovi)
        {
            lblZbirGresaka.Text = $"Збир грешака брентача: {zbirGresakaBrentaca}";
            lblZbirGresakaStafeta.Text = $"Збир грешака стафета: {zbirGresakaStafeta}";
            lblPocetniBodovi.Text = $"Почетни бодови: {pocetniBodovi}";
            lblUkupanPlasman.Text = $"Укупан резултат: {konacniBodovi}";
        }
        private decimal GetDecimalValue(string text)
        {
            return decimal.TryParse(text, out decimal result) ? result : 0;
        }

        private decimal IzracunajZbirGresakaBrentaca()
        {
            decimal zbir = 0;

            // SST greške
            zbir += GetDecimalValue(Greska2_Brentaca_SST.Text) * 5;
            zbir += GetDecimalValue(Greska3_Brentaca_SST.Text) * 10;
            zbir += GetDecimalValue(Greska4_Brentaca_SST.Text) * 5;
            zbir += GetDecimalValue(Greska5_Brentaca_SST.Text) * 2;
            zbir += GetDecimalValue(Greska6_Brentaca_SST.Text) * 5;
            zbir += GetDecimalValue(Greska7_Brentaca_SST.Text) * 5;
            zbir += GetDecimalValue(Greska8_Brentaca_SST.Text) * 5;
            zbir += GetDecimalValue(Greska9_Brentaca_SST.Text) * 5;

            // SMV greške
            zbir += GetDecimalValue(Greska2_Brentaca_SMV.Text) * 5;
            zbir += GetDecimalValue(Greska3_Brentaca_SMV.Text) * 10;
            zbir += GetDecimalValue(Greska4_Brentaca_SMV.Text) * 5;
            zbir += GetDecimalValue(Greska5_Brentaca_SMV.Text) * 2;
            zbir += GetDecimalValue(Greska6_Brentaca_SMV.Text) * 5;
            zbir += GetDecimalValue(Greska7_Brentaca_SMV.Text) * 5;
            zbir += GetDecimalValue(Greska8_Brentaca_SMV.Text) * 5;
            zbir += GetDecimalValue(Greska9_Brentaca_SMV.Text) * 5;

            // GS greške
            zbir += GetDecimalValue(Greska2_Brentaca_GS.Text) * 5;
            zbir += GetDecimalValue(Greska3_Brentaca_GS.Text) * 10;
            zbir += GetDecimalValue(Greska4_Brentaca_GS.Text) * 5;
            zbir += GetDecimalValue(Greska5_Brentaca_GS.Text) * 2;
            zbir += GetDecimalValue(Greska6_Brentaca_GS.Text) * 5;
            zbir += GetDecimalValue(Greska7_Brentaca_GS.Text) * 5;
            zbir += GetDecimalValue(Greska8_Brentaca_GS.Text) * 5;
            zbir += GetDecimalValue(Greska9_Brentaca_GS.Text) * 5;

            return zbir;
        }

        private void DodajParametreKomande(SqlCommand cmd)
        {
            // Brentac vremena
            cmd.Parameters.AddWithValue("@VremeBrentaca_SST", GetDecimalValue(VremeBrentaca_SST.Text));
            cmd.Parameters.AddWithValue("@VremeBrentaca_SMV", GetDecimalValue(VremeBrentaca_SMV.Text));
            cmd.Parameters.AddWithValue("@VremeBrentaca_GS", GetDecimalValue(VremeBrentaca_GS.Text));

            // Brentac SST greške
            cmd.Parameters.AddWithValue("@Greska2_Brentaca_SST", GetDecimalValue(Greska2_Brentaca_SST.Text));
            cmd.Parameters.AddWithValue("@Greska3_Brentaca_SST", GetDecimalValue(Greska3_Brentaca_SST.Text));
            cmd.Parameters.AddWithValue("@Greska4_Brentaca_SST", GetDecimalValue(Greska4_Brentaca_SST.Text));
            cmd.Parameters.AddWithValue("@Greska5_Brentaca_SST", GetDecimalValue(Greska5_Brentaca_SST.Text));
            cmd.Parameters.AddWithValue("@Greska6_Brentaca_SST", GetDecimalValue(Greska6_Brentaca_SST.Text));
            cmd.Parameters.AddWithValue("@Greska7_Brentaca_SST", GetDecimalValue(Greska7_Brentaca_SST.Text));
            cmd.Parameters.AddWithValue("@Greska8_Brentaca_SST", GetDecimalValue(Greska8_Brentaca_SST.Text));
            cmd.Parameters.AddWithValue("@Greska9_Brentaca_SST", GetDecimalValue(Greska9_Brentaca_SST.Text));

            // Brentac SMV greške
            cmd.Parameters.AddWithValue("@Greska2_Brentaca_SMV", GetDecimalValue(Greska2_Brentaca_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska3_Brentaca_SMV", GetDecimalValue(Greska3_Brentaca_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska4_Brentaca_SMV", GetDecimalValue(Greska4_Brentaca_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska5_Brentaca_SMV", GetDecimalValue(Greska5_Brentaca_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska6_Brentaca_SMV", GetDecimalValue(Greska6_Brentaca_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska7_Brentaca_SMV", GetDecimalValue(Greska7_Brentaca_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska8_Brentaca_SMV", GetDecimalValue(Greska8_Brentaca_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska9_Brentaca_SMV", GetDecimalValue(Greska9_Brentaca_SMV.Text));

            // Brentac GS greške
            cmd.Parameters.AddWithValue("@Greska2_Brentaca_GS", GetDecimalValue(Greska2_Brentaca_GS.Text));
            cmd.Parameters.AddWithValue("@Greska3_Brentaca_GS", GetDecimalValue(Greska3_Brentaca_GS.Text));
            cmd.Parameters.AddWithValue("@Greska4_Brentaca_GS", GetDecimalValue(Greska4_Brentaca_GS.Text));
            cmd.Parameters.AddWithValue("@Greska5_Brentaca_GS", GetDecimalValue(Greska5_Brentaca_GS.Text));
            cmd.Parameters.AddWithValue("@Greska6_Brentaca_GS", GetDecimalValue(Greska6_Brentaca_GS.Text));
            cmd.Parameters.AddWithValue("@Greska7_Brentaca_GS", GetDecimalValue(Greska7_Brentaca_GS.Text));
            cmd.Parameters.AddWithValue("@Greska8_Brentaca_GS", GetDecimalValue(Greska8_Brentaca_GS.Text));
            cmd.Parameters.AddWithValue("@Greska9_Brentaca_GS", GetDecimalValue(Greska9_Brentaca_GS.Text));

            // PV SST greške
            cmd.Parameters.AddWithValue("@Greska1_PV_SST", GetDecimalValue(Greska1_PV_SST.Text));
            cmd.Parameters.AddWithValue("@Greska2_PV_SST", GetDecimalValue(Greska2_PV_SST.Text));
            cmd.Parameters.AddWithValue("@Greska3_PV_SST", GetDecimalValue(Greska3_PV_SST.Text));
            cmd.Parameters.AddWithValue("@Greska4_PV_SST", GetDecimalValue(Greska4_PV_SST.Text));
            cmd.Parameters.AddWithValue("@Greska5_PV_SST", GetDecimalValue(Greska5_PV_SST.Text));
            cmd.Parameters.AddWithValue("@Greska6_PV_SST", GetDecimalValue(Greska6_PV_SST.Text));

            // PV SMV greške
            cmd.Parameters.AddWithValue("@Greska1_PV_SMV", GetDecimalValue(Greska1_PV_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska2_PV_SMV", GetDecimalValue(Greska2_PV_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska3_PV_SMV", GetDecimalValue(Greska3_PV_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska4_PV_SMV", GetDecimalValue(Greska4_PV_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska5_PV_SMV", GetDecimalValue(Greska5_PV_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska6_PV_SMV", GetDecimalValue(Greska6_PV_SMV.Text));

            // PV GS greške
            cmd.Parameters.AddWithValue("@Greska1_PV_GS", GetDecimalValue(Greska1_PV_GS.Text));
            cmd.Parameters.AddWithValue("@Greska2_PV_GS", GetDecimalValue(Greska2_PV_GS.Text));
            cmd.Parameters.AddWithValue("@Greska3_PV_GS", GetDecimalValue(Greska3_PV_GS.Text));
            cmd.Parameters.AddWithValue("@Greska4_PV_GS", GetDecimalValue(Greska4_PV_GS.Text));
            cmd.Parameters.AddWithValue("@Greska5_PV_GS", GetDecimalValue(Greska5_PV_GS.Text));
            cmd.Parameters.AddWithValue("@Greska6_PV_GS", GetDecimalValue(Greska6_PV_GS.Text));
        }
    }
}