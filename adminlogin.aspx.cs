using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProgramTakmicenja
{
    public partial class adminlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(strcon))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Грешка: Connection string није пронађен у web.config');", true);
                    return;
                }
            }
        }
        
        private string HashPasswordWithSalt(string password, string salt)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                string saltedPassword = password + salt; // Povežite lozinku i salt
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(bytes);
            }
        }
        
        // Admin login
        protected void AdminUlaz_Click(object sender, EventArgs e)
        {
            Response.Write("Dugme je kliknuto!");

            try
            {
                Response.Write("Pokušaj konekcije...");

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    Response.Write("Uspešno povezano sa bazom!");

                    string query = "SELECT adminUsername, adminPassword, salt, punoime, role FROM adminKorisnici WHERE adminUsername = @AdminUsername";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@AdminUsername", SqlDbType.VarChar).Value = AdminBox.Text.Trim();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                string storedPasswordHash = dr["adminPassword"].ToString();
                                string storedSalt = dr["salt"].ToString();
                                string inputPassword = AdminPass.Text.Trim();
                                string hashedInputPassword = HashPasswordWithSalt(inputPassword, storedSalt);

                                if (inputPassword == storedPasswordHash) // Ako je lozinka običan tekst u bazi
                                {
                                    Session["adminUsername"] = dr["adminUsername"].ToString();
                                    Session["punoime"] = dr["punoime"].ToString();
                                    Session["role"] = dr["role"].ToString();

                                    Response.Redirect("default.aspx");
                                }

                                if (hashedInputPassword == storedPasswordHash)
                                {
                                    // Postavljanje podataka u sesiju
                                    Session["adminUsername"] = dr["adminUsername"].ToString();
                                    Session["punoime"] = dr["punoime"].ToString();
                                    Session["role"] = dr["role"].ToString();

                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Успешно логовање');", true);
                                    Response.Redirect("default.aspx");
                                    Response.Write("Pronađen admin: " + dr["adminUsername"].ToString());
                                    Response.End();
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Неправилна лозинка');", true);
                                    Response.Write("Administrator ne postoji.");
                                    Response.End();
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Администратор не постоји');", true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Greška pri povezivanju: " + ex.Message);

                // Bolja obrada greške
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Грешка: " + ex.Message + "');", true);
                // Možda dodati logovanje greške u fajl ili bazu podataka
            }
        }

    }
}
