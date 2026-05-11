using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProgramTakmicenja
{
    public class DatabaseJuniori
    {
        private string connectionString;

        public DatabaseJuniori()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool SacuvajPodmladakRezultate(int ekipaId, Dictionary<string, object> vrednosti)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Proveri da li postoji rezultat
                    string checkSql = "SELECT COUNT(*) FROM junioriRezultati WHERE EkipaID = @EkipaID";

                    using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                    {
                        checkCmd.Parameters.AddWithValue("@EkipaID", ekipaId);
                        int count = (int)checkCmd.ExecuteScalar();

                        string sql;
                        if (count > 0)
                        {
                            // Update postojećeg
                            sql = @"UPDATE junioriRezultati SET 
                                    VremePrepreke_Sudija1 = @VremePrepreke_Sudija1,
                                    VremePrepreke_Sudija2 = @VremePrepreke_Sudija2,
                                    VremePrepreke_Sudija3 = @VremePrepreke_Sudija3,
                                    Greska2_Prep_Sudija1 = @Greska2_Prep_Sudija1,
                                    Greska2_Prep_Sudija2 = @Greska2_Prep_Sudija2,
                                    Greska2_Prep_Sudija3 = @Greska2_Prep_Sudija3,
                                    Greska2_Prep_Sudija4 = @Greska2_Prep_Sudija4,
                                    Greska2_Prep_Sudija5 = @Greska2_Prep_Sudija5,
                                    Greska2_Prep_GlavniSudija =@Greska2_Prep_GlavniSudija,
                                    Greska3_Prep_Sudija1 = @Greska3_Prep_Sudija1,
                                    Greska3_Prep_Sudija2 = @Greska3_Prep_Sudija2,
                                    Greska3_Prep_Sudija3 = @Greska3_Prep_Sudija3,
                                    Greska3_Prep_Sudija4 = @Greska3_Prep_Sudija4,
                                    Greska3_Prep_Sudija5 = @Greska3_Prep_Sudija5,
                                    Greska3_Prep_GlavniSudija = @Greska3_Prep_GlavniSudija,
                                    Greska4_Prep_Sudija1 = @Greska4_Prep_Sudija1,
                                    Greska4_Prep_Sudija2 = @Greska4_Prep_Sudija2,
                                    Greska4_Prep_Sudija3 = @Greska4_Prep_Sudija3,
                                    Greska4_Prep_Sudija4 = @Greska4_Prep_Sudija4,
                                    Greska4_Prep_Sudija5 = @Greska4_Prep_Sudija5,
                                    Greska4_Prep_GlavniSudija = @Greska4_Prep_GlavniSudija,
                                    greska5_Prep_Sudija1 = @Greska5_Prep_Sudija1,
                                    greska5_Prep_Sudija2 = @Greska5_Prep_Sudija2,
                                    greska5_Prep_Sudija3 = @Greska5_Prep_Sudija3,
                                    greska5_Prep_Sudija4 = @Greska5_Prep_Sudija4,
                                    greska5_Prep_Sudija5 = @Greska5_Prep_Sudija5,
                                    greska5_Prep_GlavniSudija = @Greska5_Prep_GlavniSudija,
                                    Greska6_Prep_Sudija1 = @Greska6_Prep_Sudija1,
                                    Greska6_Prep_Sudija2 = @Greska6_Prep_Sudija2,
                                    Greska6_Prep_Sudija3 = @Greska6_Prep_Sudija3,
                                    Greska6_Prep_Sudija4 = @Greska6_Prep_Sudija4,
                                    Greska6_Prep_Sudija5 = @Greska6_Prep_Sudija5,
                                    Greska6_Prep_GlavniSudija = @Greska6_Prep_GlavniSudija,
                                    Greska7_Prep_Sudija1 = @Greska7_Prep_Sudija1,
                                    Greska7_Prep_Sudija2 = @Greska7_Prep_Sudija2,
                                    Greska7_Prep_Sudija3 = @Greska7_Prep_Sudija3,
                                    Greska7_Prep_Sudija4 = @Greska7_Prep_Sudija4,
                                    Greska7_Prep_Sudija5 = @Greska7_Prep_Sudija5,
                                    Greska7_Prep_GlavniSudija = @Greska7_Prep_GlavniSudija,
                                    Greska8_Prep_Sudija1 = @Greska8_Prep_Sudija1,
                                    Greska8_Prep_Sudija2 = @Greska8_Prep_Sudija2,
                                    Greska8_Prep_Sudija3 = @Greska8_Prep_Sudija3,
                                    Greska8_Prep_Sudija4 = @Greska8_Prep_Sudija4,
                                    Greska8_Prep_Sudija5 = @Greska8_Prep_Sudija5,
                                    Greska8_Prep_GlavniSudija = @Greska8_Prep_GlavniSudija,
                                    Greska9_Prep_Sudija1 = @Greska9_Prep_Sudija1,
                                    Greska9_Prep_Sudija2 = @Greska9_Prep_Sudija2,
                                    Greska9_Prep_Sudija3 = @Greska9_Prep_Sudija3,
                                    Greska9_Prep_Sudija4 = @Greska9_Prep_Sudija4,
                                    Greska9_Prep_Sudija5 = @Greska9_Prep_Sudija5,
                                    Greska9_Prep_GlavniSudija = @Greska9_Prep_GlavniSudija,
                                    Greska10_Prep_Sudija1 = @Greska10_Prep_Sudija1,
                                    Greska10_Prep_Sudija2 = @Greska10_Prep_Sudija2,
                                    Greska10_Prep_Sudija3 = @Greska10_Prep_Sudija3,
                                    Greska10_Prep_Sudija4 = @Greska10_Prep_Sudija4,
                                    Greska10_Prep_Sudija5 = @Greska10_Prep_Sudija5,
                                    Greska10_Prep_GlavniSudija = @Greska10_Prep_GlavniSudija,
                                    Greska11_Prep_Sudija1 = @Greska11_Prep_Sudija1,
                                    Greska11_Prep_Sudija2 = @Greska11_Prep_Sudija2,
                                    Greska11_Prep_Sudija3 = @Greska11_Prep_Sudija3,
                                    Greska11_Prep_Sudija4 = @Greska11_Prep_Sudija4,
                                    Greska11_Prep_Sudija5 = @Greska11_Prep_Sudija5,
                                    Greska11_Prep_GlavniSudija = @Greska11_Prep_GlavniSudija,
                                    WHERE EkipaID = @EkipaID";
                        }
                        else
                        {
                            // Insert novog
                            sql = @"INSERT INTO junioriRezultati 
                                    (EkipaID, VremePrepreke_Sudija1, VremePrepreke_Sudija2, VremePrepreke_GlavniSudija, Greska2_Prep_Sudija1, Greska2_Prep_Sudija2, Greska2_Prep_Sudija3, Greska2_Prep_Sudija4, Greska2_Prep_Sudija5, Greska2_Prep_GlavniSudija,
                                     Greska3_Prep_Sudija1, Greska3_Prep_Sudija2, Greska3_Prep_Sudija3, Greska3_Prep_Sudija4, Greska3_Prep_Sudija5, Greska3_Prep_GlavniSudija,
                                     Greska4_Prep_Sudija1, Greska4_Prep_Sudija2, Greska4_Prep_Sudija3, Greska4_Prep_Sudija4, Greska4_Prep_Sudija5, Greska4_Prep_GlavniSudija,
                                     greska5_Prep_Sudija1, greska5_Prep_Sudija2, greska5_Prep_Sudija3, greska5_Prep_Sudija4, greska5_Prep_Sudija5, greska5_Prep_GlavniSudija,
                                     Greska6_Prep_Sudija1, Greska6_Prep_Sudija2, Greska6_Prep_Sudija3, Greska6_Prep_Sudija4, Greska6_Prep_Sudija5, Greska6_Prep_GlavniSudija,
                                     Greska7_Prep_Sudija1, Greska7_Prep_Sudija2, Greska7_Prep_Sudija3, Greska7_Prep_Sudija4, Greska7_Prep_Sudija5, Greska7_Prep_GlavniSudija,
                                     Greska8_Prep_Sudija1, Greska8_Prep_Sudija2, Greska8_Prep_Sudija3, Greska8_Prep_Sudija4, Greska8_Prep_Sudija5, Greska8_Prep_GlavniSudija,
                                     Greska9_Prep_Sudija1, Greska9_Prep_Sudija2, Greska9_Prep_Sudija3, Greska9_Prep_Sudija4, Greska9_Prep_Sudija5, Greska9_Prep_GlavniSudija, Greska10_Prep_Sudija1, Greska10_Prep_Sudija2, Greska10_Prep_Sudija3, Greska10_Prep_Sudija4, Greska10_Prep_Sudija5, Greska10_Prep_GlavniSudija, Greska11_Prep_Sudija1, Greska11_Prep_Sudija2, Greska11_Prep_Sudija3, Greska11_Prep_Sudija4, Greska11_Prep_Sudija5, Greska11_Prep_GlavniSudija, VremeStafeta_Merilac1, VremeStafeta_Merilac2, VremeStafeta_GlavniSudija, Greska2_Stafeta_Starter, Greska2_Stafeta_Merilac1, Greska2_Stafeta_Merilac2, Greska2_Stafeta_StazniSudija, Greska2_Stafeta_GlavniSudija, Greska3_Stafeta_Starter, Greska3_Stafeta_Merilac1, Greska3_Stafeta_Merilac2, Greska3_Stafeta_StazniSudija, Greska3_Stafeta_GlavniSudija, Greska4_Stafeta_Starter, Greska4_Stafeta_Merilac1, Greska4_Stafeta_Merilac2, Greska4_Stafeta_StazniSudija, Greska4_Stafeta_GlavniSudija) 
                                    VALUES 
                                    (@EkipaID, @VremePrepreke_Sudija1, @VremePrepreke_Sudija2, @VremePrepreke_GlavniSudija, @Greska2_Prep_Sudija1, @Greska2_Prep_Sudija2, @Greska2_Prep_Sudija3, @Greska2_Prep_Sudija4, @Greska2_Prep_Sudija5, @Greska2_Prep_GlavniSudija,
                                     @Greska3_Prep_Sudija1, @Greska3_Prep_Sudija2, @Greska3_Prep_Sudija3, @Greska3_Prep_Sudija4, @Greska3_Prep_Sudija5, @Greska3_Prep_GlavniSudija,
                                     @Greska4_Prep_Sudija1, @Greska4_Prep_Sudija2, @Greska4_Prep_Sudija3, @Greska4_Prep_Sudija4, @Greska4_Prep_Sudija5, @Greska4_Prep_GlavniSudija,
                                     @greska5_Prep_Sudija1, @greska5_Prep_Sudija2, @greska5_Prep_Sudija3, @greska5_Prep_Sudija4, @greska5_Prep_Sudija5, @greska5_Prep_GlavniSudija,
                                     @Greska6_Prep_Sudija1, @Greska6_Prep_Sudija2, @Greska6_Prep_Sudija3, @Greska6_Prep_Sudija4, @Greska6_Prep_Sudija5, @Greska6_Prep_GlavniSudija,
                                     @Greska7_Prep_Sudija1, @Greska7_Prep_Sudija2, @Greska7_Prep_Sudija3, @Greska7_Prep_Sudija4, @Greska7_Prep_Sudija5, @Greska7_Prep_GlavniSudija,
                                     @Greska8_Prep_Sudija1, @Greska8_Prep_Sudija2, @Greska8_Prep_Sudija3, @Greska8_Prep_Sudija4, @Greska8_Prep_Sudija5, @Greska8_Prep_GlavniSudija, @Greska9_Prep_Sudija1, @Greska9_Prep_Sudija2, @Greska9_Prep_Sudija3, @Greska9_Prep_Sudija4, @Greska9_Prep_Sudija5, @Greska9_Prep_GlavniSudija, @Greska10_Prep_Sudija1, @Greska10_Prep_Sudija2, @Greska10_Prep_Sudija3, @Greska10_Prep_Sudija4, @Greska10_Prep_Sudija5, @Greska10_Prep_GlavniSudija, @Greska11_Prep_Sudija1, @Greska11_Prep_Sudija2, @Greska11_Prep_Sudija3, @Greska11_Prep_Sudija4, @Greska11_Prep_Sudija5, @Greska11_Prep_GlavniSudija, @VremeStafeta_Merilac1, @VremeStafeta_Merilac2, @VremeStafeta_GlavniSudija, @greska2_Stafeta_Starter, @Greska2_Stafeta_Merilac1, @Greska2_Stafeta_Merilac2, @Greska2_Stafeta_StazniSudija, @Greska2_Stafeta_GlavniSudija, @Greska3_Stafeta_Starter, @Greska3_Stafeta_Merilac1, @Greska3_Stafeta_Merilac2, @Greska3_Stafeta_StazniSudija, @Greska3_Stafeta_GlavniSudija, @Greska4_Stafeta_Starter, @Greska4_Stafeta_Merilac1, @Greska4_Stafeta_Merilac2, @Greska4_Stafeta_StazniSudija, @Greska4_Stafeta_GlavniSudija)";
                        }

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@EkipaID", ekipaId);

                            // Dodaj sve parametre
                            foreach (var item in vrednosti)
                            {
                                cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Greška pri čuvanju: " + ex.Message);
                return false;
            }
        }

        public Dictionary<string, object> UcitajPodmladakRezultate(int ekipaId)
        {
            var rezultati = new Dictionary<string, object>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM junioriRezultati WHERE EkipaID = @EkipaID";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@EkipaID", ekipaId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string fieldName = reader.GetName(i);
                                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                    rezultati[fieldName] = value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Greška pri učitavanju: " + ex.Message);
            }

            return rezultati;
        }

        // Metoda za izračunavanje rezultata
        public Dictionary<string, decimal> IzracunajRezultate(int ekipaId)
        {
            var rezultati = new Dictionary<string, decimal>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"SELECT 
                                ISNULL(VremePrepreke_Sudija1, 0) as VremePrepreke_Sudija1,
                                ISNULL(VremePrepreke_Sudija2, 0) as VremePrepreke_Sudija2,
                                ISNULL(VremePrepreke_GlavniSudija, 0) as VremePrepreke_GlavniSudija,
                                ISNULL(Greska2_Prep_Sudija1, 0) as Greska2_Prep_Sudija1,
                                ISNULL(Greska2_Prep_Sudija2, 0) as Greska2_Prep_Sudija2,
                                ISNULL(Greska2_Prep_Sudija3, 0) as Greska2_Prep_Sudija3,
                                ISNULL(Greska2_Prep_Sudija4, 0) as Greska2_Prep_Sudija4,
                                ISNULL(Greska2_Prep_Sudija5, 0) as Greska2_Prep_Sudija5,
                                ISNULL(Greska2_Prep_GlavniSudija, 0) as Greska2_Prep_GlavniSudija,
                                ISNULL(Greska3_Prep_Sudija1, 0) as Greska3_Prep_Sudija1,
                                ISNULL(Greska3_Prep_Sudija2, 0) as Greska3_Prep_Sudija2,
                                ISNULL(Greska3_Prep_Sudija3, 0) as Greska3_Prep_Sudija3,
                                ISNULL(Greska3_Prep_Sudija4, 0) as Greska3_Prep_Sudija4,
                                ISNULL(Greska3_Prep_Sudija5, 0) as Greska3_Prep_Sudija5,
                                ISNULL(Greska3_Prep_GlavniSudija, 0) as Greska3_Prep_GlavniSudija,
                                ISNULL(Greska4_Prep_Sudija1, 0) as Greska4_Prep_Sudija1,
                                ISNULL(Greska4_Prep_Sudija2, 0) as Greska4_Prep_Sudija2,
                                iSNULL(Greska4_Prep_Sudija3, 0) as Greska4_Prep_Sudija3,
                                ISNULL(Greska4_Prep_Sudija4, 0) as Greska4_Prep_Sudija4,
                                ISNULL(Greska4_Prep_Sudija5, 0) as Greska4_Prep_Sudija5,
                                ISNULL(Greska4_Prep_GlavniSudija, 0) as Greska4_Prep_GlavniSudija,
                                ISNULL(greska5_Prep_Sudija1, 0) as greska5_Prep_Sudija1,
                                ISNULL(greska5_Prep_Sudija2, 0) as greska5_Prep_Sudija2,
                                ISNULL(greska5_Prep_Sudija3, 0) as greska5_Prep_Sudija3,
                                ISNULL(greska5_Prep_Sudija4, 0) as greska5_Prep_Sudija4,
                                ISNULL(greska5_Prep_Sudija5, 0) as greska5_Prep_Sudija5,
                                ISNULL(greska5_Prep_GlavniSudija, 0) as greska5_Prep_GlavniSudija,
                                ISNULL(Greska6_Prep_Sudija1, 0) as Greska6_Prep_Sudija1,
                                ISNULL(Greska6_Prep_Sudija2, 0) as Greska6_Prep_Sudija2,
                                ISNULL(Greska6_Prep_Sudija3, 0) as Greska6_Prep_Sudija3,
                                ISNULL(Greska6_Prep_Sudija4, 0) as Greska6_Prep_Sudija4,
                                ISNULL(Greska6_Prep_Sudija5, 0) as Greska6_Prep_Sudija5,
                                ISNULL(Greska6_Prep_GlavniSudija, 0) as Greska6_Prep_GlavniSudija,
                                ISNULL(Greska7_Prep_Sudija1, 0) as Greska7_Prep_Sudija1,
                                ISNULL(Greska7_Prep_Sudija2, 0) as Greska7_Prep_Sudija2,
                                ISNULL(Greska7_Prep_Sudija3, 0) as Greska7_Prep_Sudija3,
                                ISNULL(Greska7_Prep_Sudija4, 0) as Greska7_Prep_Sudija4,
                                ISNULL(Greska7_Prep_Sudija5, 0) as Greska7_Prep_Sudija5,
                                ISNULL(Greska7_Prep_GlavniSudija, 0) as Greska7_Prep_GlavniSudija,
                                ISNULL(Greska8_Prep_Sudija1, 0) as Greska8_Prep_Sudija1,
                                ISNULL(Greska8_Prep_Sudija2, 0) as Greska8_Prep_Sudija2,
                                ISNULL(Greska8_Prep_Sudija3, 0) as Greska8_Prep_Sudija3,
                                ISNULL(Greska8_Prep_Sudija4, 0) as Greska8_Prep_Sudija4,
                                ISNULL(Greska8_Prep_Sudija5, 0) as Greska8_Prep_Sudija5,
                                ISNULL(Greska8_Prep_GlavniSudija, 0) as Greska8_Prep_GlavniSudija,
                                ISNULL(Greska9_Prep_Sudija1, 0) as Greska9_Prep_Sudija1,
                                ISNULL(Greska9_Prep_Sudija2, 0) as Greska9_Prep_Sudija2,
                                ISNULL(Greska9_Prep_Sudija3, 0) as Greska9_Prep_Sudija3,
                                ISNULL(Greska9_Prep_Sudija4, 0) as Greska9_Prep_Sudija4,
                                ISNULL(Greska9_Prep_Sudija5, 0) as Greska9_Prep_Sudija5,
                                ISNULL(Greska9_Prep_GlavniSudija, 0) as Greska9_Prep_GlavniSudija,
                                ISNULL(Greska10_Prep_Sudija1, 0) as Greska10_Prep_Sudija1,
                                ISNULL(Greska10_Prep_Sudija2, 0) as Greska10_Prep_Sudija2,
                                ISNULL(Greska10_Prep_Sudija3, 0) as Greska10_Prep_Sudija3,
                                ISNULL(Greska10_Prep_Sudija4, 0) as Greska10_Prep_Sudija4,
                                ISNULL(Greska10_Prep_Sudija5, 0) as Greska10_Prep_Sudija5,
                                ISNULL(Greska10_Prep_GlavniSudija, 0) as Greska10_Prep_GlavniSudija,
                                ISNULL(Greska11_Prep_Sudija1, 0) as Greska11_Prep_Sudija1,
                                ISNULL(Greska11_Prep_Sudija2, 0) as Greska11_Prep_Sudija2,
                                ISNULL(Greska11_Prep_Sudija3, 0) as Greska11_Prep_Sudija3,
                                ISNULL(Greska11_Prep_Sudija4, 0) as Greska11_Prep_Sudija4,
                                ISNULL(Greska11_Prep_Sudija5, 0) as Greska11_Prep_Sudija5,
                                ISNULL(Greska11_Prep_GlavniSudija, 0) as Greska11_Prep_GlavniSudija,
                                ISNULL(VremeStafeta_Merilac1, 0) as VremeStafeta_Merilac1,
                                ISNULL(VremeStafeta_Merilac2, 0) as VremeStafeta_Merilac2,
                                ISNULL(VremeStafeta_GlavniSudija, 0) as VremeStafeta_GlavniSudija,
                                ISNULL(Greska2_Stafeta_Starter, 0) as Greska2_Stafeta_Starter,
                                ISNULL(Greska2_Stafeta_Merilac1, 0) as Greska2_Stafeta_Merilac1,
                                ISNULL(Greska2_Stafeta_Merilac2, 0) as Greska2_Stafeta_Merilac2,
                                ISNULL(Greska2_Stafeta_StazniSudija, 0) as Greska2_Stafeta_StazniSudija,
                                ISNULL(Greska2_Stafeta_GlavniSudija, 0) as Greska2_Stafeta_GlavniSudija,
                                ISNULL(Greska3_Stafeta_Starter, 0) as Greska3_Stafeta_Starter,
                                ISNULL(Greska3_Stafeta_Merilac1, 0) as Greska3_Stafeta_Merilac1,
                                ISNULL(Greska3_Stafeta_Merilac2, 0) as Greska3_Stafeta_Merilac2,
                                ISNULL(Greska3_Stafeta_StazniSudija, 0) as Greska3_Stafeta_StazniSudija,
                                ISNULL(Greska3_Stafeta_GlavniSudija, 0) as Greska3_Stafeta_GlavniSudija,
                                ISNULL(Greska4_Stafeta_Starter, 0) as Greska4_Stafeta_Starter,
                                ISNULL(Greska4_Stafeta_Merilac1, 0) as Greska4_Stafeta_Merilac1,
                                ISNULL(Greska4_Stafeta_Merilac2, 0) as Greska4_Stafeta_Merilac2,
                                ISNULL(Greska4_Stafeta_StazniSudija, 0) as Greska4_Stafeta_StazniSudija,
                                ISNULL(Greska4_Stafeta_GlavniSudija, 0) as Greska4_Stafeta_GlavniSudija

                                FROM junioriRezultati WHERE EkipaID = @EkipaID";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@EkipaID", ekipaId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Izračunaj srednju vrednost za brentace
                                decimal VremePrepreke_Sudija1 = reader.GetDecimal(0);
                                decimal VremePrepreke_Sudija2 = reader.GetDecimal(1);
                                decimal VremePrepreke_GlavniSudija = reader.GetDecimal(2);
                                decimal srednjaVrednost = (VremePrepreke_Sudija1 + VremePrepreke_Sudija2 + VremePrepreke_GlavniSudija) / 3;

                                rezultati["SrednjaVrednost"] = srednjaVrednost;

                                // Izračunaj zbir grešaka
                                decimal zbirGresaka = 0;
                                for (int i = 3; i < reader.FieldCount; i++)
                                {
                                    zbirGresaka += reader.GetDecimal(i);
                                }
                                rezultati["ZbirGresaka"] = zbirGresaka;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Greška pri izračunavanju: " + ex.Message);
            }

            return rezultati;
        }
    }
}