using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja.StraniceTakmicenja.Kategorije
{
    public partial class Juniori : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEkipePoKategoriji("Јуниори");
                lblPocetniBodoviJuniori.Text = "Почетни бодови: ";
                lblSrednjaVrednostJunioriPrepreke.Text = "Средња вредност препреке: ";
                lblSrednjaVrednostJunioriStafeta.Text = "Средња вредност штафете: ";
                lblZbirGresakaJunioriPrepreke.Text = "Збир грешака препреке: 0";
                lblZbirGresakaStafetaJuniori.Text = "Збир грешака штафете: ";
                lblUkupanPlasmanJuniori.Text = "Укупан резултат: ";
            }
        }

        protected void ddlFilterEkipaJun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFilterEkipaJun.SelectedValue == "0")
            {
                lblPocetniBodoviJuniori.Text = "Почетни бодови: 100.00";
                OcistiFormu();
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT PocetniBodovi FROM Ekipe WHERE EkipaID = @EkipaID";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EkipaID", ddlFilterEkipaJun.SelectedValue);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    lblPocetniBodoviJuniori.Text = $"Почетни бодови: {result.ToString()}";
                }
                else
                {
                    lblPocetniBodoviJuniori.Text = "Почетни бодови: 100.00";
                }
            }

            UcitajPodatkeZaEkipuJun(ddlFilterEkipaJun.SelectedValue);

            // Pokreni JavaScript da ažurira ukupan rezultat
            ScriptManager.RegisterStartupScript(this, this.GetType(), "initJuniori", "initJuniori();", true);
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
                    ddlFilterEkipaJun.Items.Clear();
                    ddlFilterEkipaJun.Items.Add(new ListItem("-- Изаберите екипу --", "0"));

                    while (reader.Read())
                    {
                        string id = reader["EkipaID"].ToString();
                        string naziv = reader["NazivEkipe"].ToString();
                        string kat = reader["Kategorija"].ToString();

                        ddlFilterEkipaJun.Items.Add(new ListItem($"{naziv} ({kat})", id));
                    }
                }
            }
        }

        private void UcitajPodatkeZaEkipuJun(string ekipaID)
        {
            if (ekipaID == "0")
            {
                OcistiFormu();
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT * FROM junioriRezultati WHERE EkipaID = @EkipaID";

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Vremena za prepreke
                        VremePrepreke_Sudija1.Text = SafeGetString(reader, "VremePrepreke_Sudija1");
                        VremePrepreke_Sudija2.Text = SafeGetString(reader, "VremePrepreke_Sudija2");
                        VremePrepreke_Sudija3.Text = SafeGetString(reader, "VremePrepreke_Sudija3");
                        VremePrepreke_Sudija4.Text = SafeGetString(reader, "VremePrepreke_Sudija4");
                        VremePrepreke_Sudija5.Text = SafeGetString(reader, "VremePrepreke_Sudija5");
                        VremePrepreke_GlavniSudija.Text = SafeGetString(reader, "VremePrepreke_GlavniSudija");

                        // Greške za prepreke
                        Greska2_Prep_Sudija1.Text = SafeGetString(reader, "Greska2_Prep_Sudija1");
                        Greska2_Prep_Sudija2.Text = SafeGetString(reader, "Greska2_Prep_Sudija2");
                        Greska2_Prep_Sudija3.Text = SafeGetString(reader, "Greska2_Prep_Sudija3");
                        Greska2_Prep_Sudija4.Text = SafeGetString(reader, "Greska2_Prep_Sudija4");
                        Greska2_Prep_Sudija5.Text = SafeGetString(reader, "Greska2_Prep_Sudija5");
                        Greska2_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska2_Prep_GlavniSudija");

                        Greska3_Prep_Sudija1.Text = SafeGetString(reader, "Greska3_Prep_Sudija1");
                        Greska3_Prep_Sudija2.Text = SafeGetString(reader, "Greska3_Prep_Sudija2");
                        Greska3_Prep_Sudija3.Text = SafeGetString(reader, "Greska3_Prep_Sudija3");
                        Greska3_Prep_Sudija4.Text = SafeGetString(reader, "Greska3_Prep_Sudija4");
                        Greska3_Prep_Sudija5.Text = SafeGetString(reader, "Greska3_Prep_Sudija5");
                        Greska3_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska3_Prep_GlavniSudija");

                        Greska4_Prep_Sudija1.Text = SafeGetString(reader, "Greska4_Prep_Sudija1");
                        Greska4_Prep_Sudija2.Text = SafeGetString(reader, "Greska4_Prep_Sudija2");
                        Greska4_Prep_Sudija3.Text = SafeGetString(reader, "Greska4_Prep_Sudija3");
                        Greska4_Prep_Sudija4.Text = SafeGetString(reader, "Greska4_Prep_Sudija4");
                        Greska4_Prep_Sudija5.Text = SafeGetString(reader, "Greska4_Prep_Sudija5");
                        Greska4_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska4_Prep_GlavniSudija");

                        Greska5_Prep_Sudija1.Text = SafeGetString(reader, "Greska5_Prep_Sudija1");
                        Greska5_Prep_Sudija2.Text = SafeGetString(reader, "Greska5_Prep_Sudija2");
                        Greska5_Prep_Sudija3.Text = SafeGetString(reader, "Greska5_Prep_Sudija3");
                        Greska5_Prep_Sudija4.Text = SafeGetString(reader, "Greska5_Prep_Sudija4");
                        Greska5_Prep_Sudija5.Text = SafeGetString(reader, "Greska5_Prep_Sudija5");
                        Greska5_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska5_Prep_GlavniSudija");

                        Greska6_Prep_Sudija1.Text = SafeGetString(reader, "Greska6_Prep_Sudija1");
                        Greska6_Prep_Sudija2.Text = SafeGetString(reader, "Greska6_Prep_Sudija2");
                        Greska6_Prep_Sudija3.Text = SafeGetString(reader, "Greska6_Prep_Sudija3");
                        Greska6_Prep_Sudija4.Text = SafeGetString(reader, "Greska6_Prep_Sudija4");
                        Greska6_Prep_Sudija5.Text = SafeGetString(reader, "Greska6_Prep_Sudija5");
                        Greska6_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska6_Prep_GlavniSudija");

                        Greska7_Prep_Sudija1.Text = SafeGetString(reader, "Greska7_Prep_Sudija1");
                        Greska7_Prep_Sudija2.Text = SafeGetString(reader, "Greska7_Prep_Sudija2");
                        Greska7_Prep_Sudija3.Text = SafeGetString(reader, "Greska7_Prep_Sudija3");
                        Greska7_Prep_Sudija4.Text = SafeGetString(reader, "Greska7_Prep_Sudija4");
                        Greska7_Prep_Sudija5.Text = SafeGetString(reader, "Greska7_Prep_Sudija5");
                        Greska7_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska7_Prep_GlavniSudija");

                        Greska8_Prep_Sudija1.Text = SafeGetString(reader, "Greska8_Prep_Sudija1");
                        Greska8_Prep_Sudija2.Text = SafeGetString(reader, "Greska8_Prep_Sudija2");
                        Greska8_Prep_Sudija3.Text = SafeGetString(reader, "Greska8_Prep_Sudija3");
                        Greska8_Prep_Sudija4.Text = SafeGetString(reader, "Greska8_Prep_Sudija4");
                        Greska8_Prep_Sudija5.Text = SafeGetString(reader, "Greska8_Prep_Sudija5");
                        Greska8_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska8_Prep_GlavniSudija");

                        Greska9_Prep_Sudija1.Text = SafeGetString(reader, "Greska9_Prep_Sudija1");
                        Greska9_Prep_Sudija2.Text = SafeGetString(reader, "Greska9_Prep_Sudija2");
                        Greska9_Prep_Sudija3.Text = SafeGetString(reader, "Greska9_Prep_Sudija3");
                        Greska9_Prep_Sudija4.Text = SafeGetString(reader, "Greska9_Prep_Sudija4");
                        Greska9_Prep_Sudija5.Text = SafeGetString(reader, "Greska9_Prep_Sudija5");
                        Greska9_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska9_Prep_GlavniSudija");

                        Greska10_Prep_Sudija1.Text = SafeGetString(reader, "Greska10_Prep_Sudija1");
                        Greska10_Prep_Sudija2.Text = SafeGetString(reader, "Greska10_Prep_Sudija2");
                        Greska10_Prep_Sudija3.Text = SafeGetString(reader, "Greska10_Prep_Sudija3");
                        Greska10_Prep_Sudija4.Text = SafeGetString(reader, "Greska10_Prep_Sudija4");
                        Greska10_Prep_Sudija5.Text = SafeGetString(reader, "Greska10_Prep_Sudija5");
                        Greska10_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska10_Prep_GlavniSudija");

                        Greska11_Prep_Sudija1.Text = SafeGetString(reader, "Greska11_Prep_Sudija1");
                        Greska11_Prep_Sudija2.Text = SafeGetString(reader, "Greska11_Prep_Sudija2");
                        Greska11_Prep_Sudija3.Text = SafeGetString(reader, "Greska11_Prep_Sudija3");
                        Greska11_Prep_Sudija4.Text = SafeGetString(reader, "Greska11_Prep_Sudija4");
                        Greska11_Prep_Sudija5.Text = SafeGetString(reader, "Greska11_Prep_Sudija5");
                        Greska11_Prep_GlavniSudija.Text = SafeGetString(reader, "Greska11_Prep_GlavniSudija");

                        // Vremena za štafetu
                        VremeStafete_Starter.Text = SafeGetString(reader, "VremeStafete_Starter");
                        VremeStafete_Merilac1.Text = SafeGetString(reader, "VremeStafete_Merilac1");
                        VremeStafete_Merilac2.Text = SafeGetString(reader, "VremeStafete_Merilac2");
                        VremeStafete_StazniSudija.Text = SafeGetString(reader, "VremeStafete_StazniSudija");
                        VremeStafete_GlavniSudija.Text = SafeGetString(reader, "VremeStafete_GlavniSudija");

                        // Greške za štafetu
                        Greska2_Stafeta_Starter.Text = SafeGetString(reader, "Greska2_Stafeta_Starter");
                        Greska2_Stafeta_Merilac1.Text = SafeGetString(reader, "Greska2_Stafeta_Merilac1");
                        Greska2_Stafeta_Merilac2.Text = SafeGetString(reader, "Greska2_Stafeta_Merilac2");
                        Greska2_Stafeta_StazniSudija.Text = SafeGetString(reader, "Greska2_Stafeta_StazniSudija");
                        Greska2_Stafeta_GlavniSudija.Text = SafeGetString(reader, "Greska2_Stafeta_GlavniSudija");

                        Greska3_Stafeta_Starter.Text = SafeGetString(reader, "Greska3_Stafeta_Starter");
                        Greska3_Stafeta_Merilac1.Text = SafeGetString(reader, "Greska3_Stafeta_Merilac1");
                        Greska3_Stafeta_Merilac2.Text = SafeGetString(reader, "Greska3_Stafeta_Merilac2");
                        Greska3_Stafeta_StazniSudija.Text = SafeGetString(reader, "Greska3_Stafeta_StazniSudija");
                        Greska3_Stafeta_GlavniSudija.Text = SafeGetString(reader, "Greska3_Stafeta_GlavniSudija");

                        Greska4_Stafeta_Starter.Text = SafeGetString(reader, "Greska4_Stafeta_Starter");
                        Greska4_Stafeta_Merilac1.Text = SafeGetString(reader, "Greska4_Stafeta_Merilac1");
                        Greska4_Stafeta_Merilac2.Text = SafeGetString(reader, "Greska4_Stafeta_Merilac2");
                        Greska4_Stafeta_StazniSudija.Text = SafeGetString(reader, "Greska4_Stafeta_StazniSudija");
                        Greska4_Stafeta_GlavniSudija.Text = SafeGetString(reader, "Greska4_Stafeta_GlavniSudija");
                    }
                    else
                    {
                        OcistiFormu();
                    }
                }
            }

            // Ažuriraj JavaScript izračunavanja
            ScriptManager.RegisterStartupScript(this, this.GetType(), "azurirajIzracunavanja",
                "izracunajSrednjuVrednostJunioriPrepreke(); izracunajSrednjuVrednostJunioriStafeta(); izracunajZbirGresakaJunioriPrepreke(); izracunajZbirGresakaStafetaJuniori(); izracunajUkupanPlasmanJuniori();", true);
        }

        private string SafeGetString(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return !reader.IsDBNull(ordinal) ? reader[columnName].ToString() : "";
            }
            catch
            {
                return "";
            }
        }

        private void OcistiFormu()
        {
            // Očisti polja za prepreke
            VremePrepreke_Sudija1.Text = "";
            VremePrepreke_Sudija2.Text = "";
            VremePrepreke_Sudija3.Text = "";
            VremePrepreke_Sudija4.Text = "";
            VremePrepreke_Sudija5.Text = "";
            VremePrepreke_GlavniSudija.Text = "";

            // Očisti greške za prepreke
            Greska2_Prep_Sudija1.Text = "";
            Greska2_Prep_Sudija2.Text = "";
            Greska2_Prep_Sudija3.Text = "";
            Greska2_Prep_Sudija4.Text = "";
            Greska2_Prep_Sudija5.Text = "";
            Greska2_Prep_GlavniSudija.Text = "";

            Greska3_Prep_Sudija1.Text = "";
            Greska3_Prep_Sudija2.Text = "";
            Greska3_Prep_Sudija3.Text = "";
            Greska3_Prep_Sudija4.Text = "";
            Greska3_Prep_Sudija5.Text = "";
            Greska3_Prep_GlavniSudija.Text = "";

            Greska4_Prep_Sudija1.Text = "";
            Greska4_Prep_Sudija2.Text = "";
            Greska4_Prep_Sudija3.Text = "";
            Greska4_Prep_Sudija4.Text = "";
            Greska4_Prep_Sudija5.Text = "";
            Greska4_Prep_GlavniSudija.Text = "";

            Greska5_Prep_Sudija1.Text = "";
            Greska5_Prep_Sudija2.Text = "";
            Greska5_Prep_Sudija3.Text = "";
            Greska5_Prep_Sudija4.Text = "";
            Greska5_Prep_Sudija5.Text = "";
            Greska5_Prep_GlavniSudija.Text = "";

            Greska6_Prep_Sudija1.Text = "";
            Greska6_Prep_Sudija2.Text = "";
            Greska6_Prep_Sudija3.Text = "";
            Greska6_Prep_Sudija4.Text = "";
            Greska6_Prep_Sudija5.Text = "";
            Greska6_Prep_GlavniSudija.Text = "";

            Greska7_Prep_Sudija1.Text = "";
            Greska7_Prep_Sudija2.Text = "";
            Greska7_Prep_Sudija3.Text = "";
            Greska7_Prep_Sudija4.Text = "";
            Greska7_Prep_Sudija5.Text = "";
            Greska7_Prep_GlavniSudija.Text = "";

            Greska8_Prep_Sudija1.Text = "";
            Greska8_Prep_Sudija2.Text = "";
            Greska8_Prep_Sudija3.Text = "";
            Greska8_Prep_Sudija4.Text = "";
            Greska8_Prep_Sudija5.Text = "";
            Greska8_Prep_GlavniSudija.Text = "";

            Greska9_Prep_Sudija1.Text = "";
            Greska9_Prep_Sudija2.Text = "";
            Greska9_Prep_Sudija3.Text = "";
            Greska9_Prep_Sudija4.Text = "";
            Greska9_Prep_Sudija5.Text = "";
            Greska9_Prep_GlavniSudija.Text = "";

            Greska10_Prep_Sudija1.Text = "";
            Greska10_Prep_Sudija2.Text = "";
            Greska10_Prep_Sudija3.Text = "";
            Greska10_Prep_Sudija4.Text = "";
            Greska10_Prep_Sudija5.Text = "";
            Greska10_Prep_GlavniSudija.Text = "";

            Greska11_Prep_Sudija1.Text = "";
            Greska11_Prep_Sudija2.Text = "";
            Greska11_Prep_Sudija3.Text = "";
            Greska11_Prep_Sudija4.Text = "";
            Greska11_Prep_Sudija5.Text = "";
            Greska11_Prep_GlavniSudija.Text = "";

            // Očisti polja za štafetu
            VremeStafete_Starter.Text = "";
            VremeStafete_Merilac1.Text = "";
            VremeStafete_Merilac2.Text = "";
            VremeStafete_StazniSudija.Text = "";
            VremeStafete_GlavniSudija.Text = "";

            // Očisti greške za štafetu
            Greska2_Stafeta_Starter.Text = "";
            Greska2_Stafeta_Merilac1.Text = "";
            Greska2_Stafeta_Merilac2.Text = "";
            Greska2_Stafeta_StazniSudija.Text = "";
            Greska2_Stafeta_GlavniSudija.Text = "";

            Greska3_Stafeta_Starter.Text = "";
            Greska3_Stafeta_Merilac1.Text = "";
            Greska3_Stafeta_Merilac2.Text = "";
            Greska3_Stafeta_StazniSudija.Text = "";
            Greska3_Stafeta_GlavniSudija.Text = "";

            Greska4_Stafeta_Starter.Text = "";
            Greska4_Stafeta_Merilac1.Text = "";
            Greska4_Stafeta_Merilac2.Text = "";
            Greska4_Stafeta_StazniSudija.Text = "";
            Greska4_Stafeta_GlavniSudija.Text = "";

            // Resetuj labele
            lblSrednjaVrednostJunioriPrepreke.Text = "Средња вредност препреке: 0.00";
            lblSrednjaVrednostJunioriStafeta.Text = "Средња вредност штафете: 0.00";
            lblZbirGresakaJunioriPrepreke.Text = "Збир грешака препреке: 0.00";
            lblZbirGresakaStafetaJuniori.Text = "Збир грешака штафете: 0.00";
            lblUkupanPlasmanJuniori.Text = "Укупан резултат: 100.00";
        }

        protected void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (ddlFilterEkipaJun.SelectedValue == "0")
            {
                lblPoruka.Text = "Морате изабрати екипу пре чувања!";
                lblPoruka.CssClass = "text-danger";
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    // Proveri da li zapis već postoji
                    string checkQuery = "SELECT COUNT(*) FROM junioriRezultati WHERE EkipaID = @EkipaID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@EkipaID", ddlFilterEkipaJun.SelectedValue);

                    con.Open();
                    int exists = (int)checkCmd.ExecuteScalar();

                    string query;
                    if (exists > 0)
                    {
                        // UPDATE query sa svim poljima
                        query = @"UPDATE junioriRezultati SET 
                                VremePrepreke_Sudija1 = @VremePreprekeS1, VremePrepreke_Sudija2 = @VremePreprekeS2, 
                                VremePrepreke_Sudija3 = @VremePreprekeS3, VremePrepreke_Sudija4 = @VremePreprekeS4, 
                                VremePrepreke_Sudija5 = @VremePreprekeS5, VremePrepreke_GlavniSudija = @VremePreprekeGS,
                                Greska2_Prep_Sudija1 = @Greska2S1, Greska2_Prep_Sudija2 = @Greska2S2, Greska2_Prep_Sudija3 = @Greska2S3,
                                Greska2_Prep_Sudija4 = @Greska2S4, Greska2_Prep_Sudija5 = @Greska2S5, Greska2_Prep_GlavniSudija = @Greska2GS,
                                Greska3_Prep_Sudija1 = @Greska3S1, Greska3_Prep_Sudija2 = @Greska3S2, Greska3_Prep_Sudija3 = @Greska3S3,
                                Greska3_Prep_Sudija4 = @Greska3S4, Greska3_Prep_Sudija5 = @Greska3S5, Greska3_Prep_GlavniSudija = @Greska3GS,
                                Greska4_Prep_Sudija1 = @Greska4S1, Greska4_Prep_Sudija2 = @Greska4S2, Greska4_Prep_Sudija3 = @Greska4S3,
                                Greska4_Prep_Sudija4 = @Greska4S4, Greska4_Prep_Sudija5 = @Greska4S5, Greska4_Prep_GlavniSudija = @Greska4GS,
                                Greska5_Prep_Sudija1 = @Greska5S1, Greska5_Prep_Sudija2 = @Greska5S2, Greska5_Prep_Sudija3 = @Greska5S3,
                                Greska5_Prep_Sudija4 = @Greska5S4, Greska5_Prep_Sudija5 = @Greska5S5, Greska5_Prep_GlavniSudija = @Greska5GS,
                                Greska6_Prep_Sudija1 = @Greska6S1, Greska6_Prep_Sudija2 = @Greska6S2, Greska6_Prep_Sudija3 = @Greska6S3,
                                Greska6_Prep_Sudija4 = @Greska6S4, Greska6_Prep_Sudija5 = @Greska6S5, Greska6_Prep_GlavniSudija = @Greska6GS,
                                Greska7_Prep_Sudija1 = @Greska7S1, Greska7_Prep_Sudija2 = @Greska7S2, Greska7_Prep_Sudija3 = @Greska7S3,
                                Greska7_Prep_Sudija4 = @Greska7S4, Greska7_Prep_Sudija5 = @Greska7S5, Greska7_Prep_GlavniSudija = @Greska7GS,
                                Greska8_Prep_Sudija1 = @Greska8S1, Greska8_Prep_Sudija2 = @Greska8S2, Greska8_Prep_Sudija3 = @Greska8S3,
                                Greska8_Prep_Sudija4 = @Greska8S4, Greska8_Prep_Sudija5 = @Greska8S5, Greska8_Prep_GlavniSudija = @Greska8GS,
                                Greska9_Prep_Sudija1 = @Greska9S1, Greska9_Prep_Sudija2 = @Greska9S2, Greska9_Prep_Sudija3 = @Greska9S3,
                                Greska9_Prep_Sudija4 = @Greska9S4, Greska9_Prep_Sudija5 = @Greska9S5, Greska9_Prep_GlavniSudija = @Greska9GS,
                                Greska10_Prep_Sudija1 = @Greska10S1, Greska10_Prep_Sudija2 = @Greska10S2, Greska10_Prep_Sudija3 = @Greska10S3,
                                Greska10_Prep_Sudija4 = @Greska10S4, Greska10_Prep_Sudija5 = @Greska10S5, Greska10_Prep_GlavniSudija = @Greska10GS,
                                Greska11_Prep_Sudija1 = @Greska11S1, Greska11_Prep_Sudija2 = @Greska11S2, Greska11_Prep_Sudija3 = @Greska11S3,
                                Greska11_Prep_Sudija4 = @Greska11S4, Greska11_Prep_Sudija5 = @Greska11S5, Greska11_Prep_GlavniSudija = @Greska11GS,
                                VremeStafete_Starter = @VremeStafeteStarter, VremeStafete_Merilac1 = @VremeStafeteM1,
                                VremeStafete_Merilac2 = @VremeStafeteM2, VremeStafete_StazniSudija = @VremeStafeteStazni,
                                VremeStafete_GlavniSudija = @VremeStafeteGS,
                                Greska2_Stafeta_Starter = @Greska2StafetaStarter, Greska2_Stafeta_Merilac1 = @Greska2StafetaM1,
                                Greska2_Stafeta_Merilac2 = @Greska2StafetaM2, Greska2_Stafeta_StazniSudija = @Greska2StafetaStazni,
                                Greska2_Stafeta_GlavniSudija = @Greska2StafetaGS,
                                Greska3_Stafeta_Starter = @Greska3StafetaStarter, Greska3_Stafeta_Merilac1 = @Greska3StafetaM1,
                                Greska3_Stafeta_Merilac2 = @Greska3StafetaM2, Greska3_Stafeta_StazniSudija = @Greska3StafetaStazni,
                                Greska3_Stafeta_GlavniSudija = @Greska3StafetaGS,
                                Greska4_Stafeta_Starter = @Greska4StafetaStarter, Greska4_Stafeta_Merilac1 = @Greska4StafetaM1,
                                Greska4_Stafeta_Merilac2 = @Greska4StafetaM2, Greska4_Stafeta_StazniSudija = @Greska4StafetaStazni,
                                Greska4_Stafeta_GlavniSudija = @Greska4StafetaGS
                                WHERE EkipaID = @EkipaID";
                    }
                    else
                    {
                        // INSERT query sa svim poljima
                        query = @"INSERT INTO junioriRezultati (EkipaID, VremePrepreke_Sudija1, VremePrepreke_Sudija2, 
                                VremePrepreke_Sudija3, VremePrepreke_Sudija4, VremePrepreke_Sudija5, VremePrepreke_GlavniSudija,
                                Greska2_Prep_Sudija1, Greska2_Prep_Sudija2, Greska2_Prep_Sudija3, Greska2_Prep_Sudija4, Greska2_Prep_Sudija5, Greska2_Prep_GlavniSudija,
                                Greska3_Prep_Sudija1, Greska3_Prep_Sudija2, Greska3_Prep_Sudija3, Greska3_Prep_Sudija4, Greska3_Prep_Sudija5, Greska3_Prep_GlavniSudija,
                                Greska4_Prep_Sudija1, Greska4_Prep_Sudija2, Greska4_Prep_Sudija3, Greska4_Prep_Sudija4, Greska4_Prep_Sudija5, Greska4_Prep_GlavniSudija,
                                Greska5_Prep_Sudija1, Greska5_Prep_Sudija2, Greska5_Prep_Sudija3, Greska5_Prep_Sudija4, Greska5_Prep_Sudija5, Greska5_Prep_GlavniSudija,
                                Greska6_Prep_Sudija1, Greska6_Prep_Sudija2, Greska6_Prep_Sudija3, Greska6_Prep_Sudija4, Greska6_Prep_Sudija5, Greska6_Prep_GlavniSudija,
                                Greska7_Prep_Sudija1, Greska7_Prep_Sudija2, Greska7_Prep_Sudija3, Greska7_Prep_Sudija4, Greska7_Prep_Sudija5, Greska7_Prep_GlavniSudija,
                                Greska8_Prep_Sudija1, Greska8_Prep_Sudija2, Greska8_Prep_Sudija3, Greska8_Prep_Sudija4, Greska8_Prep_Sudija5, Greska8_Prep_GlavniSudija,
                                Greska9_Prep_Sudija1, Greska9_Prep_Sudija2, Greska9_Prep_Sudija3, Greska9_Prep_Sudija4, Greska9_Prep_Sudija5, Greska9_Prep_GlavniSudija,
                                Greska10_Prep_Sudija1, Greska10_Prep_Sudija2, Greska10_Prep_Sudija3, Greska10_Prep_Sudija4, Greska10_Prep_Sudija5, Greska10_Prep_GlavniSudija,
                                Greska11_Prep_Sudija1, Greska11_Prep_Sudija2, Greska11_Prep_Sudija3, Greska11_Prep_Sudija4, Greska11_Prep_Sudija5, Greska11_Prep_GlavniSudija,
                                VremeStafete_Starter, VremeStafete_Merilac1, VremeStafete_Merilac2, VremeStafete_StazniSudija, VremeStafete_GlavniSudija,
                                Greska2_Stafeta_Starter, Greska2_Stafeta_Merilac1, Greska2_Stafeta_Merilac2, Greska2_Stafeta_StazniSudija, Greska2_Stafeta_GlavniSudija,
                                Greska3_Stafeta_Starter, Greska3_Stafeta_Merilac1, Greska3_Stafeta_Merilac2, Greska3_Stafeta_StazniSudija, Greska3_Stafeta_GlavniSudija,
                                Greska4_Stafeta_Starter, Greska4_Stafeta_Merilac1, Greska4_Stafeta_Merilac2, Greska4_Stafeta_StazniSudija, Greska4_Stafeta_GlavniSudija
                                ) VALUES (@EkipaID, @VremePreprekeS1, @VremePreprekeS2, @VremePreprekeS3, @VremePreprekeS4, 
                                @VremePreprekeS5, @VremePreprekeGS, @Greska2S1, @Greska2S2, @Greska2S3, @Greska2S4, @Greska2S5, @Greska2GS,
                                @Greska3S1, @Greska3S2, @Greska3S3, @Greska3S4, @Greska3S5, @Greska3GS, @Greska4S1, @Greska4S2, @Greska4S3, @Greska4S4, @Greska4S5, @Greska4GS,
                                @Greska5S1, @Greska5S2, @Greska5S3, @Greska5S4, @Greska5S5, @Greska5GS, @Greska6S1, @Greska6S2, @Greska6S3, @Greska6S4, @Greska6S5, @Greska6GS,
                                @Greska7S1, @Greska7S2, @Greska7S3, @Greska7S4, @Greska7S5, @Greska7GS, @Greska8S1, @Greska8S2, @Greska8S3, @Greska8S4, @Greska8S5, @Greska8GS,
                                @Greska9S1, @Greska9S2, @Greska9S3, @Greska9S4, @Greska9S5, @Greska9GS, @Greska10S1, @Greska10S2, @Greska10S3, @Greska10S4, @Greska10S5, @Greska10GS,
                                @Greska11S1, @Greska11S2, @Greska11S3, @Greska11S4, @Greska11S5, @Greska11GS, @VremeStafeteStarter, @VremeStafeteM1, @VremeStafeteM2, @VremeStafeteStazni, @VremeStafeteGS,
                                @Greska2StafetaStarter, @Greska2StafetaM1, @Greska2StafetaM2, @Greska2StafetaStazni, @Greska2StafetaGS,
                                @Greska3StafetaStarter, @Greska3StafetaM1, @Greska3StafetaM2, @Greska3StafetaStazni, @Greska3StafetaGS,
                                @Greska4StafetaStarter, @Greska4StafetaM1, @Greska4StafetaM2, @Greska4StafetaStazni, @Greska4StafetaGS)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EkipaID", ddlFilterEkipaJun.SelectedValue);

                        // Dodaj parametre za vremena prepreka
                        cmd.Parameters.AddWithValue("@VremePreprekeS1", SafeDecimalParse(VremePrepreke_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@VremePreprekeS2", SafeDecimalParse(VremePrepreke_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@VremePreprekeS3", SafeDecimalParse(VremePrepreke_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@VremePreprekeS4", SafeDecimalParse(VremePrepreke_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@VremePreprekeS5", SafeDecimalParse(VremePrepreke_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@VremePreprekeGS", SafeDecimalParse(VremePrepreke_GlavniSudija.Text));

                        // Dodaj parametre za greške prepreka
                        cmd.Parameters.AddWithValue("@Greska2S1", SafeDecimalParse(Greska2_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska2S2", SafeDecimalParse(Greska2_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska2S3", SafeDecimalParse(Greska2_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska2S4", SafeDecimalParse(Greska2_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska2S5", SafeDecimalParse(Greska2_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska2GS", SafeDecimalParse(Greska2_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska3S1", SafeDecimalParse(Greska3_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska3S2", SafeDecimalParse(Greska3_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska3S3", SafeDecimalParse(Greska3_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska3S4", SafeDecimalParse(Greska3_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska3S5", SafeDecimalParse(Greska3_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska3GS", SafeDecimalParse(Greska3_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska4S1", SafeDecimalParse(Greska4_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska4S2", SafeDecimalParse(Greska4_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska4S3", SafeDecimalParse(Greska4_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska4S4", SafeDecimalParse(Greska4_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska4S5", SafeDecimalParse(Greska4_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska4GS", SafeDecimalParse(Greska4_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska5S1", SafeDecimalParse(Greska5_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska5S2", SafeDecimalParse(Greska5_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska5S3", SafeDecimalParse(Greska5_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska5S4", SafeDecimalParse(Greska5_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska5S5", SafeDecimalParse(Greska5_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska5GS", SafeDecimalParse(Greska5_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska6S1", SafeDecimalParse(Greska6_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska6S2", SafeDecimalParse(Greska6_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska6S3", SafeDecimalParse(Greska6_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska6S4", SafeDecimalParse(Greska6_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska6S5", SafeDecimalParse(Greska6_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska6GS", SafeDecimalParse(Greska6_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska7S1", SafeDecimalParse(Greska7_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska7S2", SafeDecimalParse(Greska7_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska7S3", SafeDecimalParse(Greska7_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska7S4", SafeDecimalParse(Greska7_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska7S5", SafeDecimalParse(Greska7_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska7GS", SafeDecimalParse(Greska7_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska8S1", SafeDecimalParse(Greska8_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska8S2", SafeDecimalParse(Greska8_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska8S3", SafeDecimalParse(Greska8_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska8S4", SafeDecimalParse(Greska8_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska8S5", SafeDecimalParse(Greska8_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska8GS", SafeDecimalParse(Greska8_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska9S1", SafeDecimalParse(Greska9_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska9S2", SafeDecimalParse(Greska9_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska9S3", SafeDecimalParse(Greska9_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska9S4", SafeDecimalParse(Greska9_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska9S5", SafeDecimalParse(Greska9_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska9GS", SafeDecimalParse(Greska9_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska10S1", SafeDecimalParse(Greska10_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska10S2", SafeDecimalParse(Greska10_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska10S3", SafeDecimalParse(Greska10_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska10S4", SafeDecimalParse(Greska10_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska10S5", SafeDecimalParse(Greska10_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska10GS", SafeDecimalParse(Greska10_Prep_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska11S1", SafeDecimalParse(Greska11_Prep_Sudija1.Text));
                        cmd.Parameters.AddWithValue("@Greska11S2", SafeDecimalParse(Greska11_Prep_Sudija2.Text));
                        cmd.Parameters.AddWithValue("@Greska11S3", SafeDecimalParse(Greska11_Prep_Sudija3.Text));
                        cmd.Parameters.AddWithValue("@Greska11S4", SafeDecimalParse(Greska11_Prep_Sudija4.Text));
                        cmd.Parameters.AddWithValue("@Greska11S5", SafeDecimalParse(Greska11_Prep_Sudija5.Text));
                        cmd.Parameters.AddWithValue("@Greska11GS", SafeDecimalParse(Greska11_Prep_GlavniSudija.Text));

                        // Dodaj parametre za štafetu
                        cmd.Parameters.AddWithValue("@VremeStafeteStarter", SafeDecimalParse(VremeStafete_Starter.Text));
                        cmd.Parameters.AddWithValue("@VremeStafeteM1", SafeDecimalParse(VremeStafete_Merilac1.Text));
                        cmd.Parameters.AddWithValue("@VremeStafeteM2", SafeDecimalParse(VremeStafete_Merilac2.Text));
                        cmd.Parameters.AddWithValue("@VremeStafeteStazni", SafeDecimalParse(VremeStafete_StazniSudija.Text));
                        cmd.Parameters.AddWithValue("@VremeStafeteGS", SafeDecimalParse(VremeStafete_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska2StafetaStarter", SafeDecimalParse(Greska2_Stafeta_Starter.Text));
                        cmd.Parameters.AddWithValue("@Greska2StafetaM1", SafeDecimalParse(Greska2_Stafeta_Merilac1.Text));
                        cmd.Parameters.AddWithValue("@Greska2StafetaM2", SafeDecimalParse(Greska2_Stafeta_Merilac2.Text));
                        cmd.Parameters.AddWithValue("@Greska2StafetaStazni", SafeDecimalParse(Greska2_Stafeta_StazniSudija.Text));
                        cmd.Parameters.AddWithValue("@Greska2StafetaGS", SafeDecimalParse(Greska2_Stafeta_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska3StafetaStarter", SafeDecimalParse(Greska3_Stafeta_Starter.Text));
                        cmd.Parameters.AddWithValue("@Greska3StafetaM1", SafeDecimalParse(Greska3_Stafeta_Merilac1.Text));
                        cmd.Parameters.AddWithValue("@Greska3StafetaM2", SafeDecimalParse(Greska3_Stafeta_Merilac2.Text));
                        cmd.Parameters.AddWithValue("@Greska3StafetaStazni", SafeDecimalParse(Greska3_Stafeta_StazniSudija.Text));
                        cmd.Parameters.AddWithValue("@Greska3StafetaGS", SafeDecimalParse(Greska3_Stafeta_GlavniSudija.Text));

                        cmd.Parameters.AddWithValue("@Greska4StafetaStarter", SafeDecimalParse(Greska4_Stafeta_Starter.Text));
                        cmd.Parameters.AddWithValue("@Greska4StafetaM1", SafeDecimalParse(Greska4_Stafeta_Merilac1.Text));
                        cmd.Parameters.AddWithValue("@Greska4StafetaM2", SafeDecimalParse(Greska4_Stafeta_Merilac2.Text));
                        cmd.Parameters.AddWithValue("@Greska4StafetaStazni", SafeDecimalParse(Greska4_Stafeta_StazniSudija.Text));
                        cmd.Parameters.AddWithValue("@Greska4StafetaGS", SafeDecimalParse(Greska4_Stafeta_GlavniSudija.Text));

                        cmd.ExecuteNonQuery();
                        lblPoruka.Text = "Резултати успешно сачувани!";
                        lblPoruka.CssClass = "text-success";
                    }
                }
            }
            catch (Exception ex)
            {
                lblPoruka.Text = "Грешка при чувању: " + ex.Message;
                lblPoruka.CssClass = "text-danger";
            }
        }

        protected void btnOcisti_Click(object sender, EventArgs e)
        {
            OcistiFormu();
            lblPoruka.Text = "Поља су успешно очишћена!";
            lblPoruka.CssClass = "text-info";

            // Ažuriraj JavaScript izračunavanja
            ScriptManager.RegisterStartupScript(this, this.GetType(), "azurirajIzracunavanja",
                "izracunajSrednjuVrednostJunioriPrepreke(); izracunajSrednjuVrednostJunioriStafeta(); izracunajZbirGresakaJunioriPrepreke(); izracunajZbirGresakaStafetaJuniori(); izracunajUkupanPlasmanJuniori();", true);
        }

        private decimal SafeDecimalParse(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            try
            {
                // Prvo pokušaj sa double pa konvertuj u decimal
                double doubleValue = double.Parse(text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                return (decimal)doubleValue;
            }
            catch
            {
                return 0;
            }
        }
    }
}