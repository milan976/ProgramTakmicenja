using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProgramTakmicenja
{
    public class Database
    {
        private string connectionString;

        public Database()
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
                    string checkSql = "SELECT COUNT(*) FROM podmladakRezultati WHERE EkipaID = @EkipaID";

                    using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                    {
                        checkCmd.Parameters.AddWithValue("@EkipaID", ekipaId);
                        int count = (int)checkCmd.ExecuteScalar();

                        string sql;
                        if (count > 0)
                        {
                            // Update postojećeg
                            sql = @"UPDATE podmladakRezultati SET 
                                    VremeBrentaca_SST = @VremeBrentaca_SST,
                                    VremeBrentaca_SMV = @VremeBrentaca_SMV,
                                    VremeBrentaca_GS = @VremeBrentaca_GS,
                                    Greska2_Brentaca_SST = @Greska2_Brentaca_SST,
                                    Greska2_Brentaca_SMV = @Greska2_Brentaca_SMV,
                                    Greska2_Brentaca_GS = @Greska2_Brentaca_GS,
                                    Greska3_Brentaca_SST = @Greska3_Brentaca_SST,
                                    Greska3_Brentaca_SMV = @Greska3_Brentaca_SMV,
                                    Greska3_Brentaca_GS = @Greska3_Brentaca_GS,
                                    Greska4_Brentaca_SST = @Greska4_Brentaca_SST,
                                    Greska4_Brentaca_SMV = @Greska4_Brentaca_SMV,
                                    Greska4_Brentaca_GS = @Greska4_Brentaca_GS,
                                    Greska5_Brentaca_SST = @Greska5_Brentaca_SST,
                                    Greska5_Brentaca_SMV = @Greska5_Brentaca_SMV,
                                    Greska5_Brentaca_GS = @Greska5_Brentaca_GS,
                                    Greska6_Brentaca_SST = @Greska6_Brentaca_SST,
                                    Greska6_Brentaca_SMV = @Greska6_Brentaca_SMV,
                                    Greska6_Brentaca_GS = @Greska6_Brentaca_GS,
                                    Greska7_Brentaca_SST = @Greska7_Brentaca_SST,
                                    Greska7_Brentaca_SMV = @Greska7_Brentaca_SMV,
                                    Greska7_Brentaca_GS = @Greska7_Brentaca_GS,
                                    Greska8_Brentaca_SST = @Greska8_Brentaca_SST,
                                    Greska8_Brentaca_SMV = @Greska8_Brentaca_SMV,
                                    Greska8_Brentaca_GS = @Greska8_Brentaca_GS,
                                    Greska9_Brentaca_SST = @Greska9_Brentaca_SST,
                                    Greska9_Brentaca_SMV = @Greska9_Brentaca_SMV,
                                    Greska9_Brentaca_GS = @Greska9_Brentaca_GS,
                                    Greska1_PV_SST = @Greska1_PV_SST,
                                    Greska1_PV_SMV = @Greska1_PV_SMV,
                                    Greska1_PV_GS = @Greska1_PV_GS,
                                    Greska2_PV_SST = @Greska2_PV_SST,
                                    Greska2_PV_SMV = @Greska2_PV_SMV,
                                    Greska2_PV_GS = @Greska2_PV_GS,
                                    Greska3_PV_SST = @Greska3_PV_SST,
                                    Greska3_PV_SMV = @Greska3_PV_SMV,
                                    Greska3_PV_GS = @Greska3_PV_GS,
                                    Greska4_PV_SST = @Greska4_PV_SST,
                                    Greska4_PV_SMV = @Greska4_PV_SMV,
                                    Greska4_PV_GS = @Greska4_PV_GS,
                                    Greska5_PV_SST = @Greska5_PV_SST,
                                    Greska5_PV_SMV = @Greska5_PV_SMV,
                                    Greska5_PV_GS = @Greska5_PV_GS,
                                    Greska6_PV_SST = @Greska6_PV_SST,
                                    Greska6_PV_SMV = @Greska6_PV_SMV,
                                    Greska6_PV_GS = @Greska6_PV_GS
                                    WHERE EkipaID = @EkipaID";
                        }
                        else
                        {
                            // Insert novog
                            sql = @"INSERT INTO podmladakRezultati 
                                    (EkipaID, VremeBrentaca_SST, VremeBrentaca_SMV, VremeBrentaca_GS,
                                     Greska2_Brentaca_SST, Greska2_Brentaca_SMV, Greska2_Brentaca_GS,
                                     Greska3_Brentaca_SST, Greska3_Brentaca_SMV, Greska3_Brentaca_GS,
                                     Greska4_Brentaca_SST, Greska4_Brentaca_SMV, Greska4_Brentaca_GS,
                                     Greska5_Brentaca_SST, Greska5_Brentaca_SMV, Greska5_Brentaca_GS,
                                     Greska6_Brentaca_SST, Greska6_Brentaca_SMV, Greska6_Brentaca_GS,
                                     Greska7_Brentaca_SST, Greska7_Brentaca_SMV, Greska7_Brentaca_GS,
                                     Greska8_Brentaca_SST, Greska8_Brentaca_SMV, Greska8_Brentaca_GS,
                                     Greska9_Brentaca_SST, Greska9_Brentaca_SMV, Greska9_Brentaca_GS,
                                     Greska1_PV_SST, Greska1_PV_SMV, Greska1_PV_GS,
                                     Greska2_PV_SST, Greska2_PV_SMV, Greska2_PV_GS,
                                     Greska3_PV_SST, Greska3_PV_SMV, Greska3_PV_GS,
                                     Greska4_PV_SST, Greska4_PV_SMV, Greska4_PV_GS,
                                     Greska5_PV_SST, Greska5_PV_SMV, Greska5_PV_GS,
                                     Greska6_PV_SST, Greska6_PV_SMV, Greska6_PV_GS)
                                    VALUES 
                                    (@EkipaID, @VremeBrentaca_SST, @VremeBrentaca_SMV, @VremeBrentaca_GS,
                                     @Greska2_Brentaca_SST, @Greska2_Brentaca_SMV, @Greska2_Brentaca_GS,
                                     @Greska3_Brentaca_SST, @Greska3_Brentaca_SMV, @Greska3_Brentaca_GS,
                                     @Greska4_Brentaca_SST, @Greska4_Brentaca_SMV, @Greska4_Brentaca_GS,
                                     @Greska5_Brentaca_SST, @Greska5_Brentaca_SMV, @Greska5_Brentaca_GS,
                                     @Greska6_Brentaca_SST, @Greska6_Brentaca_SMV, @Greska6_Brentaca_GS,
                                     @Greska7_Brentaca_SST, @Greska7_Brentaca_SMV, @Greska7_Brentaca_GS,
                                     @Greska8_Brentaca_SST, @Greska8_Brentaca_SMV, @Greska8_Brentaca_GS,
                                     @Greska9_Brentaca_SST, @Greska9_Brentaca_SMV, @Greska9_Brentaca_GS,
                                     @Greska1_PV_SST, @Greska1_PV_SMV, @Greska1_PV_GS,
                                     @Greska2_PV_SST, @Greska2_PV_SMV, @Greska2_PV_GS,
                                     @Greska3_PV_SST, @Greska3_PV_SMV, @Greska3_PV_GS,
                                     @Greska4_PV_SST, @Greska4_PV_SMV, @Greska4_PV_GS,
                                     @Greska5_PV_SST, @Greska5_PV_SMV, @Greska5_PV_GS,
                                     @Greska6_PV_SST, @Greska6_PV_SMV, @Greska6_PV_GS)";
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

                    string sql = "SELECT * FROM podmladakRezultati WHERE EkipaID = @EkipaID";

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
                                ISNULL(VremeBrentaca_SST, 0) as VremeSST,
                                ISNULL(VremeBrentaca_SMV, 0) as VremeSMV,
                                ISNULL(VremeBrentaca_GS, 0) as VremeGS,
                                ISNULL(Greska2_Brentaca_SST, 0) as G2SST,
                                ISNULL(Greska2_Brentaca_SMV, 0) as G2SMV,
                                ISNULL(Greska2_Brentaca_GS, 0) as G2GS,
                                ISNULL(Greska3_Brentaca_SST, 0) as G3SST,
                                ISNULL(Greska3_Brentaca_SMV, 0) as G3SMV,
                                ISNULL(Greska3_Brentaca_GS, 0) as G3GS,
                                ISNULL(Greska4_Brentaca_SST, 0) as G4SST,
                                ISNULL(Greska4_Brentaca_SMV, 0) as G4SMV,
                                ISNULL(Greska4_Brentaca_GS, 0) as G4GS,
                                ISNULL(Greska5_Brentaca_SST, 0) as G5SST,
                                ISNULL(Greska5_Brentaca_SMV, 0) as G5SMV,
                                ISNULL(Greska5_Brentaca_GS, 0) as G5GS,
                                ISNULL(Greska6_Brentaca_SST, 0) as G6SST,
                                ISNULL(Greska6_Brentaca_SMV, 0) as G6SMV,
                                ISNULL(Greska6_Brentaca_GS, 0) as G6GS,
                                ISNULL(Greska7_Brentaca_SST, 0) as G7SST,
                                ISNULL(Greska7_Brentaca_SMV, 0) as G7SMV,
                                ISNULL(Greska7_Brentaca_GS, 0) as G7GS,
                                ISNULL(Greska8_Brentaca_SST, 0) as G8SST,
                                ISNULL(Greska8_Brentaca_SMV, 0) as G8SMV,
                                ISNULL(Greska8_Brentaca_GS, 0) as G8GS,
                                ISNULL(Greska9_Brentaca_SST, 0) as G9SST,
                                ISNULL(Greska9_Brentaca_SMV, 0) as G9SMV,
                                ISNULL(Greska9_Brentaca_GS, 0) as G9GS,
                                ISNULL(Greska1_PV_SST, 0) as G1PVSST,
                                ISNULL(Greska1_PV_SMV, 0) as G1PVSMV,
                                ISNULL(Greska1_PV_GS, 0) as G1PVGS,
                                ISNULL(Greska2_PV_SST, 0) as G2PVSST,
                                ISNULL(Greska2_PV_SMV, 0) as G2PVSMV,
                                ISNULL(Greska2_PV_GS, 0) as G2PVGS,
                                ISNULL(Greska3_PV_SST, 0) as G3PVSST,
                                ISNULL(Greska3_PV_SMV, 0) as G3PVSMV,
                                ISNULL(Greska3_PV_GS, 0) as G3PVGS,
                                ISNULL(Greska4_PV_SST, 0) as G4PVSST,
                                ISNULL(Greska4_PV_SMV, 0) as G4PVSMV,
                                ISNULL(Greska4_PV_GS, 0) as G4PVGS,
                                ISNULL(Greska5_PV_SST, 0) as G5PVSST,
                                ISNULL(Greska5_PV_SMV, 0) as G5PVSMV,
                                ISNULL(Greska5_PV_GS, 0) as G5PVGS,
                                ISNULL(Greska6_PV_SST, 0) as G6PVSST,
                                ISNULL(Greska6_PV_SMV, 0) as G6PVSMV,
                                ISNULL(Greska6_PV_GS, 0) as G6PVGS
    
                                FROM podmladakRezultati WHERE EkipaID = @EkipaID";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@EkipaID", ekipaId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Izračunaj srednju vrednost za brentace
                                decimal vremeSST = reader.GetDecimal(0);
                                decimal vremeSMV = reader.GetDecimal(1);
                                decimal vremeGS = reader.GetDecimal(2);
                                decimal srednjaVrednost = (vremeSST + vremeSMV + vremeGS) / 3;

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