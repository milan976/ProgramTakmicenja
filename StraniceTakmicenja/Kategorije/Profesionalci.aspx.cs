using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja.StraniceTakmicenja.Kategorije
{
    public partial class Profesionalci : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEkipePoKategoriji("Професионалци");
            }
        }

        protected void ddlFilterEkipaProf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFilterEkipaProf.SelectedValue == "0")
            {
                lblPocetniBodoviProfesionalci.Text = "Почетни бодови: -";
                OcistiFormu();
                ResetujStatistiku();
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT PocetniBodovi FROM Ekipe WHERE EkipaID = @EkipaID";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EkipaID", ddlFilterEkipaProf.SelectedValue);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    lblPocetniBodoviProfesionalci.Text = $"Почетни бодови: {result.ToString()}";
                }
                else
                {
                    lblPocetniBodoviProfesionalci.Text = "Почетни бодови: 100.00";
                }
            }

            UcitajPodatkeZaEkipuProfesionalci(ddlFilterEkipaProf.SelectedValue);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "initProfesionalci",
                "setTimeout(function() { initProfesionalci(); izracunajSveStatistike(); }, 500);", true);
        }

        private void BindEkipePoKategoriji(string kategorija)
        {
            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = @"SELECT EkipaID, NazivEkipe, Kategorija 
                    FROM Ekipe 
                    WHERE Kategorija LIKE '%' + @Kategorija + '%' 
                    ORDER BY NazivEkipe";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Kategorija", kategorija);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlFilterEkipaProf.Items.Clear();
                    ddlFilterEkipaProf.Items.Add(new ListItem("-- Изаберите екипу --", "0"));

                    while (reader.Read())
                    {
                        string id = reader["EkipaID"].ToString();
                        string naziv = reader["NazivEkipe"].ToString();
                        string kat = reader["Kategorija"].ToString();
                        ddlFilterEkipaProf.Items.Add(new ListItem($"{naziv} ({kat})", id));
                    }
                }
            }
        }

        private void UcitajPodatkeZaEkipuProfesionalci(string ekipaID)
        {
            if (ekipaID == "0")
            {
                OcistiFormu();
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = @"SELECT TOP 1 * FROM profesionalciRezultati 
            WHERE EkipaID = @EkipaID 
            ORDER BY ID DESC";

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EkipaID", ekipaID);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // DEBUG: Proverite da li se podaci čitaju
                            System.Diagnostics.Debug.WriteLine("Pronađeni podaci za ekipu: " + ekipaID);

                            // VREMENA MVP
                            VremeMVP_Sudija1.Text = SafeGetDecimal(reader, "VremeMVP_Sudija1");
                            VremeMVP_Sudija2.Text = SafeGetDecimal(reader, "VremeMVP_Sudija2");
                            VremeMVP_Sudija3.Text = SafeGetDecimal(reader, "VremeMVP_Sudija3");
                            VremeMVP_GlavniSudija.Text = SafeGetDecimal(reader, "VremeMVP_GlavniSudija");

                            // GREŠKE MVP
                            Greska2_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska2_MVP_Sudija1");
                            Greska2_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska2_MVP_Sudija2");
                            Greska2_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska2_MVP_Sudija3");
                            Greska2_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska2_MVP_GlavniSudija");

                            Greska3_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska3_MVP_Sudija1");
                            Greska3_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska3_MVP_Sudija2");
                            Greska3_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska3_MVP_Sudija3");
                            Greska3_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska3_MVP_GlavniSudija");

                            Greska4_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska4_MVP_Sudija1");
                            Greska4_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska4_MVP_Sudija2");
                            Greska4_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska4_MVP_Sudija3");
                            Greska4_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska4_MVP_GlavniSudija");

                            Greska5_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska5_MVP_Sudija1");
                            Greska5_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska5_MVP_Sudija2");
                            Greska5_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska5_MVP_Sudija3");
                            Greska5_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska5_MVP_GlavniSudija");

                            Greska6_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska6_MVP_Sudija1");
                            Greska6_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska6_MVP_Sudija2");
                            Greska6_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska6_MVP_Sudija3");
                            Greska6_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska6_MVP_GlavniSudija");

                            Greska7_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska7_MVP_Sudija1");
                            Greska7_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska7_MVP_Sudija2");
                            Greska7_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska7_MVP_Sudija3");
                            Greska7_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska7_MVP_GlavniSudija");

                            Greska8_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska8_MVP_Sudija1");
                            Greska8_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska8_MVP_Sudija2");
                            Greska8_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska8_MVP_Sudija3");
                            Greska8_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska8_MVP_GlavniSudija");

                            Greska9_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska9_MVP_Sudija1");
                            Greska9_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska9_MVP_Sudija2");
                            Greska9_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska9_MVP_Sudija3");
                            Greska9_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska9_MVP_GlavniSudija");

                            Greska10_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska10_MVP_Sudija1");
                            Greska10_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska10_MVP_Sudija2");
                            Greska10_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska10_MVP_Sudija3");
                            Greska10_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska10_MVP_GlavniSudija");

                            Greska11_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska11_MVP_Sudija1");
                            Greska11_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska11_MVP_Sudija2");
                            Greska11_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska11_MVP_Sudija3");
                            Greska11_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska11_MVP_GlavniSudija");

                            Greska12_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska12_MVP_Sudija1");
                            Greska12_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska12_MVP_Sudija2");
                            Greska12_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska12_MVP_Sudija3");
                            Greska12_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska12_MVP_GlavniSudija");

                            Greska13_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska13_MVP_Sudija1");
                            Greska13_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska13_MVP_Sudija2");
                            Greska13_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska13_MVP_Sudija3");
                            Greska13_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska13_MVP_GlavniSudija");

                            Greska14_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska14_MVP_Sudija1");
                            Greska14_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska14_MVP_Sudija2");
                            Greska14_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska14_MVP_Sudija3");
                            Greska14_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska14_MVP_GlavniSudija");

                            Greska15_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska15_MVP_Sudija1");
                            Greska15_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska15_MVP_Sudija2");
                            Greska15_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska15_MVP_Sudija3");
                            Greska15_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska15_MVP_GlavniSudija");

                            Greska16_MVP_Sudija1.Text = SafeGetDecimal(reader, "Greska16_MVP_Sudija1");
                            Greska16_MVP_Sudija2.Text = SafeGetDecimal(reader, "Greska16_MVP_Sudija2");
                            Greska16_MVP_Sudija3.Text = SafeGetDecimal(reader, "Greska16_MVP_Sudija3");
                            Greska16_MVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska16_MVP_GlavniSudija");

                            // VREMENA ŠTAFETA
                            VremeStafetaMVP_SST.Text = SafeGetDecimal(reader, "VremeStafetaMVP_SST");
                            VremeStafetaMVP_SMV.Text = SafeGetDecimal(reader, "VremeStafetaMVP_SMV");
                            VremeStafetaMVP_ST.Text = SafeGetDecimal(reader, "VremeStafetaMVP_ST");
                            VremeStafetaMVP_GlavniSudija.Text = SafeGetDecimal(reader, "VremeStafetaMVP_GlavniSudija");

                            // GREŠKE ŠTAFETA
                            Greska2_StafetaMVP_SST.Text = SafeGetDecimal(reader, "Greska2_StafetaMVP_SST");
                            Greska2_StafetaMVP_SMV.Text = SafeGetDecimal(reader, "Greska2_StafetaMVP_SMV");
                            Greska2_StafetaMVP_ST.Text = SafeGetDecimal(reader, "Greska2_StafetaMVP_ST");
                            Greska2_StafetaMVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska2_StafetaMVP_GlavniSudija");

                            Greska3_StafetaMVP_SST.Text = SafeGetDecimal(reader, "Greska3_StafetaMVP_SST");
                            Greska3_StafetaMVP_SMV.Text = SafeGetDecimal(reader, "Greska3_StafetaMVP_SMV");
                            Greska3_StafetaMVP_ST.Text = SafeGetDecimal(reader, "Greska3_StafetaMVP_ST");
                            Greska3_StafetaMVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska3_StafetaMVP_GlavniSudija");

                            Greska4_StafetaMVP_SST.Text = SafeGetDecimal(reader, "Greska4_StafetaMVP_SST");
                            Greska4_StafetaMVP_SMV.Text = SafeGetDecimal(reader, "Greska4_StafetaMVP_SMV");
                            Greska4_StafetaMVP_ST.Text = SafeGetDecimal(reader, "Greska4_StafetaMVP_ST");
                            Greska4_StafetaMVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska4_StafetaMVP_GlavniSudija");

                            Greska5_StafetaMVP_SST.Text = SafeGetDecimal(reader, "Greska5_StafetaMVP_SST");
                            Greska5_StafetaMVP_SMV.Text = SafeGetDecimal(reader, "Greska5_StafetaMVP_SMV");
                            Greska5_StafetaMVP_ST.Text = SafeGetDecimal(reader, "Greska5_StafetaMVP_ST");
                            Greska5_StafetaMVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska5_StafetaMVP_GlavniSudija");

                            Greska6_StafetaMVP_SST.Text = SafeGetDecimal(reader, "Greska6_StafetaMVP_SST");
                            Greska6_StafetaMVP_SMV.Text = SafeGetDecimal(reader, "Greska6_StafetaMVP_SMV");
                            Greska6_StafetaMVP_ST.Text = SafeGetDecimal(reader, "Greska6_StafetaMVP_ST");
                            Greska6_StafetaMVP_GlavniSudija.Text = SafeGetDecimal(reader, "Greska6_StafetaMVP_GlavniSudija");

                            // DEBUG poruka
                            System.Diagnostics.Debug.WriteLine("Podaci uspešno učitani za ekipu: " + ekipaID);

                            // Pokreni JavaScript nakon što se forma popuni
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "calculateAfterLoad",
                                "setTimeout(function() { izracunajSveStatistike(); }, 300);", true);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Nema podataka za ekipu: " + ekipaID);
                            OcistiFormu();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Greška pri učitavanju: " + ex.Message);
                lblPoruka.Text = "Грешка при учитавању података: " + ex.Message;
                lblPoruka.CssClass = "text-danger";
            }
        }
        protected void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (ddlFilterEkipaProf.SelectedValue == "0")
            {
                lblPoruka.Text = "Морате изабрати екипу!";
                lblPoruka.CssClass = "text-danger";
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string ekipaID = ddlFilterEkipaProf.SelectedValue;

                System.Diagnostics.Debug.WriteLine("Čuvanje podataka za ekipu: " + ekipaID);

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    bool zapisPostoji = ProveriDaLiZapisPostoji(con, ekipaID);

                    System.Diagnostics.Debug.WriteLine("Zapis postoji: " + zapisPostoji);

                    if (zapisPostoji)
                    {
                        IzvrsiUpdate(con, ekipaID);
                        lblPoruka.Text = "Резултати успешно ажурирани!";
                        System.Diagnostics.Debug.WriteLine("UPDATE izvršen za ekipu: " + ekipaID);
                    }
                    else
                    {
                        IzvrsiInsert(con, ekipaID);
                        lblPoruka.Text = "Резултати успешно сачувани!";
                        System.Diagnostics.Debug.WriteLine("INSERT izvršen za ekipu: " + ekipaID);
                    }

                    lblPoruka.CssClass = "text-success";

                    // Ponovo učitaj podatke nakon čuvanja
                    UcitajPodatkeZaEkipuProfesionalci(ekipaID);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Greška pri čuvanju: " + ex.Message);
                lblPoruka.Text = "Грешка при чувању: " + ex.Message;
                lblPoruka.CssClass = "text-danger";
            }
        }
        private bool ProveriDaLiZapisPostoji(SqlConnection con, string ekipaID)
        {
            string query = "SELECT COUNT(*) FROM profesionalciRezultati WHERE EkipaID = @EkipaID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void IzvrsiInsert(SqlConnection con, string ekipaID)
        {
            string query = @"INSERT INTO profesionalciRezultati 
                (EkipaID, VremeMVP_Sudija1, VremeMVP_Sudija2, VremeMVP_Sudija3, VremeMVP_GlavniSudija, 
                 Greska2_MVP_Sudija1, Greska2_MVP_Sudija2, Greska2_MVP_Sudija3, Greska2_MVP_GlavniSudija,
                 Greska3_MVP_Sudija1, Greska3_MVP_Sudija2, Greska3_MVP_Sudija3, Greska3_MVP_GlavniSudija,
                 Greska4_MVP_Sudija1, Greska4_MVP_Sudija2, Greska4_MVP_Sudija3, Greska4_MVP_GlavniSudija,
                 Greska5_MVP_Sudija1, Greska5_MVP_Sudija2, Greska5_MVP_Sudija3, Greska5_MVP_GlavniSudija,
                 Greska6_MVP_Sudija1, Greska6_MVP_Sudija2, Greska6_MVP_Sudija3, Greska6_MVP_GlavniSudija,
                 Greska7_MVP_Sudija1, Greska7_MVP_Sudija2, Greska7_MVP_Sudija3, Greska7_MVP_GlavniSudija,
                 Greska8_MVP_Sudija1, Greska8_MVP_Sudija2, Greska8_MVP_Sudija3, Greska8_MVP_GlavniSudija,
                 Greska9_MVP_Sudija1, Greska9_MVP_Sudija2, Greska9_MVP_Sudija3, Greska9_MVP_GlavniSudija,
                 Greska10_MVP_Sudija1, Greska10_MVP_Sudija2, Greska10_MVP_Sudija3, Greska10_MVP_GlavniSudija,
                 Greska11_MVP_Sudija1, Greska11_MVP_Sudija2, Greska11_MVP_Sudija3, Greska11_MVP_GlavniSudija,
                 Greska12_MVP_Sudija1, Greska12_MVP_Sudija2, Greska12_MVP_Sudija3, Greska12_MVP_GlavniSudija,
                 Greska13_MVP_Sudija1, Greska13_MVP_Sudija2, Greska13_MVP_Sudija3, Greska13_MVP_GlavniSudija,
                 Greska14_MVP_Sudija1, Greska14_MVP_Sudija2, Greska14_MVP_Sudija3, Greska14_MVP_GlavniSudija,
                 Greska15_MVP_Sudija1, Greska15_MVP_Sudija2, Greska15_MVP_Sudija3, Greska15_MVP_GlavniSudija,
                 Greska16_MVP_Sudija1, Greska16_MVP_Sudija2, Greska16_MVP_Sudija3, Greska16_MVP_GlavniSudija,
                 VremeStafetaMVP_SST, VremeStafetaMVP_SMV, VremeStafetaMVP_ST, VremeStafetaMVP_GlavniSudija,
                 Greska2_StafetaMVP_SST, Greska2_StafetaMVP_SMV, Greska2_StafetaMVP_ST, Greska2_StafetaMVP_GlavniSudija,
                 Greska3_StafetaMVP_SST, Greska3_StafetaMVP_SMV, Greska3_StafetaMVP_ST, Greska3_StafetaMVP_GlavniSudija,
                 Greska4_StafetaMVP_SST, Greska4_StafetaMVP_SMV, Greska4_StafetaMVP_ST, Greska4_StafetaMVP_GlavniSudija,
                 Greska5_StafetaMVP_SST, Greska5_StafetaMVP_SMV, Greska5_StafetaMVP_ST, Greska5_StafetaMVP_GlavniSudija,
                 Greska6_StafetaMVP_SST, Greska6_StafetaMVP_SMV, Greska6_StafetaMVP_ST, Greska6_StafetaMVP_GlavniSudija) 
            VALUES 
                (@EkipaID, @VremeMVP_Sudija1, @VremeMVP_Sudija2, @VremeMVP_Sudija3, @VremeMVP_GlavniSudija,
                 @Greska2_MVP_Sudija1, @Greska2_MVP_Sudija2, @Greska2_MVP_Sudija3, @Greska2_MVP_GlavniSudija,
                 @Greska3_MVP_Sudija1, @Greska3_MVP_Sudija2, @Greska3_MVP_Sudija3, @Greska3_MVP_GlavniSudija,
                 @Greska4_MVP_Sudija1, @Greska4_MVP_Sudija2, @Greska4_MVP_Sudija3, @Greska4_MVP_GlavniSudija,
                 @Greska5_MVP_Sudija1, @Greska5_MVP_Sudija2, @Greska5_MVP_Sudija3, @Greska5_MVP_GlavniSudija,
                 @Greska6_MVP_Sudija1, @Greska6_MVP_Sudija2, @Greska6_MVP_Sudija3, @Greska6_MVP_GlavniSudija,
                 @Greska7_MVP_Sudija1, @Greska7_MVP_Sudija2, @Greska7_MVP_Sudija3, @Greska7_MVP_GlavniSudija,
                 @Greska8_MVP_Sudija1, @Greska8_MVP_Sudija2, @Greska8_MVP_Sudija3, @Greska8_MVP_GlavniSudija,
                 @Greska9_MVP_Sudija1, @Greska9_MVP_Sudija2, @Greska9_MVP_Sudija3, @Greska9_MVP_GlavniSudija,
                 @Greska10_MVP_Sudija1, @Greska10_MVP_Sudija2, @Greska10_MVP_Sudija3, @Greska10_MVP_GlavniSudija,
                 @Greska11_MVP_Sudija1, @Greska11_MVP_Sudija2, @Greska11_MVP_Sudija3, @Greska11_MVP_GlavniSudija,
                 @Greska12_MVP_Sudija1, @Greska12_MVP_Sudija2, @Greska12_MVP_Sudija3, @Greska12_MVP_GlavniSudija,
                 @Greska13_MVP_Sudija1, @Greska13_MVP_Sudija2, @Greska13_MVP_Sudija3, @Greska13_MVP_GlavniSudija,
                 @Greska14_MVP_Sudija1, @Greska14_MVP_Sudija2, @Greska14_MVP_Sudija3, @Greska14_MVP_GlavniSudija,
                 @Greska15_MVP_Sudija1, @Greska15_MVP_Sudija2, @Greska15_MVP_Sudija3, @Greska15_MVP_GlavniSudija,
                 @Greska16_MVP_Sudija1, @Greska16_MVP_Sudija2, @Greska16_MVP_Sudija3, @Greska16_MVP_GlavniSudija,
                 @VremeStafetaMVP_SST, @VremeStafetaMVP_SMV, @VremeStafetaMVP_ST, @VremeStafetaMVP_GlavniSudija,
                 @Greska2_StafetaMVP_SST, @Greska2_StafetaMVP_SMV, @Greska2_StafetaMVP_ST, @Greska2_StafetaMVP_GlavniSudija,
                 @Greska3_StafetaMVP_SST, @Greska3_StafetaMVP_SMV, @Greska3_StafetaMVP_ST, @Greska3_StafetaMVP_GlavniSudija,
                 @Greska4_StafetaMVP_SST, @Greska4_StafetaMVP_SMV, @Greska4_StafetaMVP_ST, @Greska4_StafetaMVP_GlavniSudija,
                 @Greska5_StafetaMVP_SST, @Greska5_StafetaMVP_SMV, @Greska5_StafetaMVP_ST, @Greska5_StafetaMVP_GlavniSudija,
                 @Greska6_StafetaMVP_SST, @Greska6_StafetaMVP_SMV, @Greska6_StafetaMVP_ST, @Greska6_StafetaMVP_GlavniSudija)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                DodajParametre(cmd, ekipaID);
                cmd.ExecuteNonQuery();
            }
        }

        private void IzvrsiUpdate(SqlConnection con, string ekipaID)
        {
            string query = @"UPDATE profesionalciRezultati SET 
                VremeMVP_Sudija1 = @VremeMVP_Sudija1,
                VremeMVP_Sudija2 = @VremeMVP_Sudija2,
                VremeMVP_Sudija3 = @VremeMVP_Sudija3,
                VremeMVP_GlavniSudija = @VremeMVP_GlavniSudija,
                Greska2_MVP_Sudija1 = @Greska2_MVP_Sudija1,
                Greska2_MVP_Sudija2 = @Greska2_MVP_Sudija2,
                Greska2_MVP_Sudija3 = @Greska2_MVP_Sudija3,
                Greska2_MVP_GlavniSudija = @Greska2_MVP_GlavniSudija,
                Greska3_MVP_Sudija1 = @Greska3_MVP_Sudija1,
                Greska3_MVP_Sudija2 = @Greska3_MVP_Sudija2,
                Greska3_MVP_Sudija3 = @Greska3_MVP_Sudija3,
                Greska3_MVP_GlavniSudija = @Greska3_MVP_GlavniSudija,
                Greska4_MVP_Sudija1 = @Greska4_MVP_Sudija1,
                Greska4_MVP_Sudija2 = @Greska4_MVP_Sudija2,
                Greska4_MVP_Sudija3 = @Greska4_MVP_Sudija3,
                Greska4_MVP_GlavniSudija = @Greska4_MVP_GlavniSudija,
                Greska5_MVP_Sudija1 = @Greska5_MVP_Sudija1,
                Greska5_MVP_Sudija2 = @Greska5_MVP_Sudija2,
                Greska5_MVP_Sudija3 = @Greska5_MVP_Sudija3,
                Greska5_MVP_GlavniSudija = @Greska5_MVP_GlavniSudija,
                Greska6_MVP_Sudija1 = @Greska6_MVP_Sudija1,
                Greska6_MVP_Sudija2 = @Greska6_MVP_Sudija2,
                Greska6_MVP_Sudija3 = @Greska6_MVP_Sudija3,
                Greska6_MVP_GlavniSudija = @Greska6_MVP_GlavniSudija,
                Greska7_MVP_Sudija1 = @Greska7_MVP_Sudija1,
                Greska7_MVP_Sudija2 = @Greska7_MVP_Sudija2,
                Greska7_MVP_Sudija3 = @Greska7_MVP_Sudija3,
                Greska7_MVP_GlavniSudija = @Greska7_MVP_GlavniSudija,
                Greska8_MVP_Sudija1 = @Greska8_MVP_Sudija1,
                Greska8_MVP_Sudija2 = @Greska8_MVP_Sudija2,
                Greska8_MVP_Sudija3 = @Greska8_MVP_Sudija3,
                Greska8_MVP_GlavniSudija = @Greska8_MVP_GlavniSudija,
                Greska9_MVP_Sudija1 = @Greska9_MVP_Sudija1,
                Greska9_MVP_Sudija2 = @Greska9_MVP_Sudija2,
                Greska9_MVP_Sudija3 = @Greska9_MVP_Sudija3,
                Greska9_MVP_GlavniSudija = @Greska9_MVP_GlavniSudija,
                Greska10_MVP_Sudija1 = @Greska10_MVP_Sudija1,
                Greska10_MVP_Sudija2 = @Greska10_MVP_Sudija2,
                Greska10_MVP_Sudija3 = @Greska10_MVP_Sudija3,
                Greska10_MVP_GlavniSudija = @Greska10_MVP_GlavniSudija,
                Greska11_MVP_Sudija1 = @Greska11_MVP_Sudija1,
                Greska11_MVP_Sudija2 = @Greska11_MVP_Sudija2,
                Greska11_MVP_Sudija3 = @Greska11_MVP_Sudija3,
                Greska11_MVP_GlavniSudija = @Greska11_MVP_GlavniSudija,
                Greska12_MVP_Sudija1 = @Greska12_MVP_Sudija1,
                Greska12_MVP_Sudija2 = @Greska12_MVP_Sudija2,
                Greska12_MVP_Sudija3 = @Greska12_MVP_Sudija3,
                Greska12_MVP_GlavniSudija = @Greska12_MVP_GlavniSudija,
                Greska13_MVP_Sudija1 = @Greska13_MVP_Sudija1,
                Greska13_MVP_Sudija2 = @Greska13_MVP_Sudija2,
                Greska13_MVP_Sudija3 = @Greska13_MVP_Sudija3,
                Greska13_MVP_GlavniSudija = @Greska13_MVP_GlavniSudija,
                Greska14_MVP_Sudija1 = @Greska14_MVP_Sudija1,
                Greska14_MVP_Sudija2 = @Greska14_MVP_Sudija2,
                Greska14_MVP_Sudija3 = @Greska14_MVP_Sudija3,
                Greska14_MVP_GlavniSudija = @Greska14_MVP_GlavniSudija,
                Greska15_MVP_Sudija1 = @Greska15_MVP_Sudija1,
                Greska15_MVP_Sudija2 = @Greska15_MVP_Sudija2,
                Greska15_MVP_Sudija3 = @Greska15_MVP_Sudija3,
                Greska15_MVP_GlavniSudija = @Greska15_MVP_GlavniSudija,
                Greska16_MVP_Sudija1 = @Greska16_MVP_Sudija1,
                Greska16_MVP_Sudija2 = @Greska16_MVP_Sudija2,
                Greska16_MVP_Sudija3 = @Greska16_MVP_Sudija3,
                Greska16_MVP_GlavniSudija = @Greska16_MVP_GlavniSudija,
                VremeStafetaMVP_SST = @VremeStafetaMVP_SST,
                VremeStafetaMVP_SMV = @VremeStafetaMVP_SMV,
                VremeStafetaMVP_ST = @VremeStafetaMVP_ST,
                VremeStafetaMVP_GlavniSudija = @VremeStafetaMVP_GlavniSudija,
                Greska2_StafetaMVP_SST = @Greska2_StafetaMVP_SST,
                Greska2_StafetaMVP_SMV = @Greska2_StafetaMVP_SMV,
                Greska2_StafetaMVP_ST = @Greska2_StafetaMVP_ST,
                Greska2_StafetaMVP_GlavniSudija = @Greska2_StafetaMVP_GlavniSudija,
                Greska3_StafetaMVP_SST = @Greska3_StafetaMVP_SST,
                Greska3_StafetaMVP_SMV = @Greska3_StafetaMVP_SMV,
                Greska3_StafetaMVP_ST = @Greska3_StafetaMVP_ST,
                Greska3_StafetaMVP_GlavniSudija = @Greska3_StafetaMVP_GlavniSudija,
                Greska4_StafetaMVP_SST = @Greska4_StafetaMVP_SST,
                Greska4_StafetaMVP_SMV = @Greska4_StafetaMVP_SMV,
                Greska4_StafetaMVP_ST = @Greska4_StafetaMVP_ST,
                Greska4_StafetaMVP_GlavniSudija = @Greska4_StafetaMVP_GlavniSudija,
                Greska5_StafetaMVP_SST = @Greska5_StafetaMVP_SST,
                Greska5_StafetaMVP_SMV = @Greska5_StafetaMVP_SMV,
                Greska5_StafetaMVP_ST = @Greska5_StafetaMVP_ST,
                Greska5_StafetaMVP_GlavniSudija = @Greska5_StafetaMVP_GlavniSudija,
                Greska6_StafetaMVP_SST = @Greska6_StafetaMVP_SST,
                Greska6_StafetaMVP_SMV = @Greska6_StafetaMVP_SMV,
                Greska6_StafetaMVP_ST = @Greska6_StafetaMVP_ST,
                Greska6_StafetaMVP_GlavniSudija = @Greska6_StafetaMVP_GlavniSudija
                WHERE EkipaID = @EkipaID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                DodajParametre(cmd, ekipaID);
                cmd.ExecuteNonQuery();
            }
        }

        private void DodajParametre(SqlCommand cmd, string ekipaID)
        {
            cmd.Parameters.AddWithValue("@EkipaID", ekipaID);

            // VREMENA MVP
            cmd.Parameters.AddWithValue("@VremeMVP_Sudija1", SafeParseDecimal(VremeMVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@VremeMVP_Sudija2", SafeParseDecimal(VremeMVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@VremeMVP_Sudija3", SafeParseDecimal(VremeMVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@VremeMVP_GlavniSudija", SafeParseDecimal(VremeMVP_GlavniSudija.Text));

            // GREŠKE MVP
            cmd.Parameters.AddWithValue("@Greska2_MVP_Sudija1", SafeParseDecimal(Greska2_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska2_MVP_Sudija2", SafeParseDecimal(Greska2_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska2_MVP_Sudija3", SafeParseDecimal(Greska2_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska2_MVP_GlavniSudija", SafeParseDecimal(Greska2_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska3_MVP_Sudija1", SafeParseDecimal(Greska3_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska3_MVP_Sudija2", SafeParseDecimal(Greska3_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska3_MVP_Sudija3", SafeParseDecimal(Greska3_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska3_MVP_GlavniSudija", SafeParseDecimal(Greska3_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska4_MVP_Sudija1", SafeParseDecimal(Greska4_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska4_MVP_Sudija2", SafeParseDecimal(Greska4_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska4_MVP_Sudija3", SafeParseDecimal(Greska4_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska4_MVP_GlavniSudija", SafeParseDecimal(Greska4_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska5_MVP_Sudija1", SafeParseDecimal(Greska5_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska5_MVP_Sudija2", SafeParseDecimal(Greska5_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska5_MVP_Sudija3", SafeParseDecimal(Greska5_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska5_MVP_GlavniSudija", SafeParseDecimal(Greska5_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska6_MVP_Sudija1", SafeParseDecimal(Greska6_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska6_MVP_Sudija2", SafeParseDecimal(Greska6_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska6_MVP_Sudija3", SafeParseDecimal(Greska6_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska6_MVP_GlavniSudija", SafeParseDecimal(Greska6_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska7_MVP_Sudija1", SafeParseDecimal(Greska7_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska7_MVP_Sudija2", SafeParseDecimal(Greska7_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska7_MVP_Sudija3", SafeParseDecimal(Greska7_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska7_MVP_GlavniSudija", SafeParseDecimal(Greska7_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska8_MVP_Sudija1", SafeParseDecimal(Greska8_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska8_MVP_Sudija2", SafeParseDecimal(Greska8_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska8_MVP_Sudija3", SafeParseDecimal(Greska8_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska8_MVP_GlavniSudija", SafeParseDecimal(Greska8_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska9_MVP_Sudija1", SafeParseDecimal(Greska9_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska9_MVP_Sudija2", SafeParseDecimal(Greska9_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska9_MVP_Sudija3", SafeParseDecimal(Greska9_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska9_MVP_GlavniSudija", SafeParseDecimal(Greska9_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska10_MVP_Sudija1", SafeParseDecimal(Greska10_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska10_MVP_Sudija2", SafeParseDecimal(Greska10_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska10_MVP_Sudija3", SafeParseDecimal(Greska10_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska10_MVP_GlavniSudija", SafeParseDecimal(Greska10_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska11_MVP_Sudija1", SafeParseDecimal(Greska11_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska11_MVP_Sudija2", SafeParseDecimal(Greska11_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska11_MVP_Sudija3", SafeParseDecimal(Greska11_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska11_MVP_GlavniSudija", SafeParseDecimal(Greska11_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska12_MVP_Sudija1", SafeParseDecimal(Greska12_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska12_MVP_Sudija2", SafeParseDecimal(Greska12_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska12_MVP_Sudija3", SafeParseDecimal(Greska12_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska12_MVP_GlavniSudija", SafeParseDecimal(Greska12_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska13_MVP_Sudija1", SafeParseDecimal(Greska13_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska13_MVP_Sudija2", SafeParseDecimal(Greska13_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska13_MVP_Sudija3", SafeParseDecimal(Greska13_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska13_MVP_GlavniSudija", SafeParseDecimal(Greska13_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska14_MVP_Sudija1", SafeParseDecimal(Greska14_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska14_MVP_Sudija2", SafeParseDecimal(Greska14_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska14_MVP_Sudija3", SafeParseDecimal(Greska14_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska14_MVP_GlavniSudija", SafeParseDecimal(Greska14_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska15_MVP_Sudija1", SafeParseDecimal(Greska15_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska15_MVP_Sudija2", SafeParseDecimal(Greska15_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska15_MVP_Sudija3", SafeParseDecimal(Greska15_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska15_MVP_GlavniSudija", SafeParseDecimal(Greska15_MVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska16_MVP_Sudija1", SafeParseDecimal(Greska16_MVP_Sudija1.Text));
            cmd.Parameters.AddWithValue("@Greska16_MVP_Sudija2", SafeParseDecimal(Greska16_MVP_Sudija2.Text));
            cmd.Parameters.AddWithValue("@Greska16_MVP_Sudija3", SafeParseDecimal(Greska16_MVP_Sudija3.Text));
            cmd.Parameters.AddWithValue("@Greska16_MVP_GlavniSudija", SafeParseDecimal(Greska16_MVP_GlavniSudija.Text));

            // VREMENA ŠTAFETA
            cmd.Parameters.AddWithValue("@VremeStafetaMVP_SST", SafeParseDecimal(VremeStafetaMVP_SST.Text));
            cmd.Parameters.AddWithValue("@VremeStafetaMVP_SMV", SafeParseDecimal(VremeStafetaMVP_SMV.Text));
            cmd.Parameters.AddWithValue("@VremeStafetaMVP_ST", SafeParseDecimal(VremeStafetaMVP_ST.Text));
            cmd.Parameters.AddWithValue("@VremeStafetaMVP_GlavniSudija", SafeParseDecimal(VremeStafetaMVP_GlavniSudija.Text));

            // GREŠKE ŠTAFETA
            cmd.Parameters.AddWithValue("@Greska2_StafetaMVP_SST", SafeParseDecimal(Greska2_StafetaMVP_SST.Text));
            cmd.Parameters.AddWithValue("@Greska2_StafetaMVP_SMV", SafeParseDecimal(Greska2_StafetaMVP_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska2_StafetaMVP_ST", SafeParseDecimal(Greska2_StafetaMVP_ST.Text));
            cmd.Parameters.AddWithValue("@Greska2_StafetaMVP_GlavniSudija", SafeParseDecimal(Greska2_StafetaMVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska3_StafetaMVP_SST", SafeParseDecimal(Greska3_StafetaMVP_SST.Text));
            cmd.Parameters.AddWithValue("@Greska3_StafetaMVP_SMV", SafeParseDecimal(Greska3_StafetaMVP_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska3_StafetaMVP_ST", SafeParseDecimal(Greska3_StafetaMVP_ST.Text));
            cmd.Parameters.AddWithValue("@Greska3_StafetaMVP_GlavniSudija", SafeParseDecimal(Greska3_StafetaMVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska4_StafetaMVP_SST", SafeParseDecimal(Greska4_StafetaMVP_SST.Text));
            cmd.Parameters.AddWithValue("@Greska4_StafetaMVP_SMV", SafeParseDecimal(Greska4_StafetaMVP_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska4_StafetaMVP_ST", SafeParseDecimal(Greska4_StafetaMVP_ST.Text));
            cmd.Parameters.AddWithValue("@Greska4_StafetaMVP_GlavniSudija", SafeParseDecimal(Greska4_StafetaMVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska5_StafetaMVP_SST", SafeParseDecimal(Greska5_StafetaMVP_SST.Text));
            cmd.Parameters.AddWithValue("@Greska5_StafetaMVP_SMV", SafeParseDecimal(Greska5_StafetaMVP_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska5_StafetaMVP_ST", SafeParseDecimal(Greska5_StafetaMVP_ST.Text));
            cmd.Parameters.AddWithValue("@Greska5_StafetaMVP_GlavniSudija", SafeParseDecimal(Greska5_StafetaMVP_GlavniSudija.Text));

            cmd.Parameters.AddWithValue("@Greska6_StafetaMVP_SST", SafeParseDecimal(Greska6_StafetaMVP_SST.Text));
            cmd.Parameters.AddWithValue("@Greska6_StafetaMVP_SMV", SafeParseDecimal(Greska6_StafetaMVP_SMV.Text));
            cmd.Parameters.AddWithValue("@Greska6_StafetaMVP_ST", SafeParseDecimal(Greska6_StafetaMVP_ST.Text));
            cmd.Parameters.AddWithValue("@Greska6_StafetaMVP_GlavniSudija", SafeParseDecimal(Greska6_StafetaMVP_GlavniSudija.Text));
        }

        private decimal SafeParseDecimal(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0m;

            decimal result;
            if (decimal.TryParse(text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            return 0m;
        }

        private string SafeGetDecimal(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                if (!reader.IsDBNull(ordinal))
                {
                    object value = reader[columnName];

                    // Proveri tip podataka
                    if (value is decimal)
                    {
                        return ((decimal)value).ToString("0.00");
                    }
                    else if (value is double)
                    {
                        return ((double)value).ToString("0.00");
                    }
                    else if (value is float)
                    {
                        return ((float)value).ToString("0.00");
                    }
                    else if (value is int)
                    {
                        return ((int)value).ToString("0");
                    }
                    else
                    {
                        // Pokušaj parsiranje stringa
                        decimal decimalValue;
                        if (decimal.TryParse(value.ToString(), out decimalValue))
                        {
                            return decimalValue.ToString("0.00");
                        }
                        return "0";
                    }
                }
                return "0";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Greška u SafeGetDecimal za {columnName}: {ex.Message}");
                return "0";
            }
        }
        private void ResetujStatistiku()
        {
            lblSrednjaVrednostProfesionalciMvp.Text = "Средња време препреке: 0.00";
            lblSrednjaVrednostProfesionalciStafeta.Text = "Средња вредност штафете: 0.00";
            lblZbirGresakaProfesionalciMvp.Text = "Збир грешака препреке: 0";
            lblZbirGresakaProfesionalciStafeta.Text = "Збир грешака штафете: 0";
            lblUkupanPlasmanProfesionalci.Text = "Укупан резултат: 100.00";
        }

        private void OcistiFormu()
        {
            // Očisti sva polja
            VremeMVP_Sudija1.Text = "";
            VremeMVP_Sudija2.Text = "";
            VremeMVP_Sudija3.Text = "";
            VremeMVP_GlavniSudija.Text = "";

            Greska2_MVP_Sudija1.Text = "";
            Greska2_MVP_Sudija2.Text = "";
            Greska2_MVP_Sudija3.Text = "";
            Greska2_MVP_GlavniSudija.Text = "";

            // ... očisti sva ostala polja

            VremeStafetaMVP_SST.Text = "";
            VremeStafetaMVP_SMV.Text = "";
            VremeStafetaMVP_ST.Text = "";
            VremeStafetaMVP_GlavniSudija.Text = "";

            Greska2_StafetaMVP_SST.Text = "";
            Greska2_StafetaMVP_SMV.Text = "";
            Greska2_StafetaMVP_ST.Text = "";
            Greska2_StafetaMVP_GlavniSudija.Text = "";

            // ... očisti sve greške štafete

            ResetujStatistiku();
        }
    }
}