using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace ProgramTakmicenja
{
    public partial class adminkorisnikaaspx : System.Web.UI.Page
    {
        
        // Konekcija sa bazom
        private string connectionString = ConfigurationManager.ConnectionStrings["con"].ToString();

            private string GenerateSalt()
            {
                byte[] saltBytes = new byte[16]; // 16 bytes for the salt
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(saltBytes);
                }
                return Convert.ToBase64String(saltBytes);
            }

            private string HashPasswordWithSalt(string password, string salt)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    string saltedPassword = password + salt; // Combine password with salt
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    return Convert.ToBase64String(bytes);
                }
            }
            protected void Page_Load(object sender, EventArgs e)
                {
                    if (!IsPostBack)
                    {
                        // Ocisti polja pri prvom učitavanju stranice
                        ClearFields();
                    }
                }
            // Metoda za cistenje polja
            private void ClearForm()
            {
                TextBox3.Text = string.Empty; // Ime i prezime
                TextBox2.Text = string.Empty; // Email
                TextBox8.Text = string.Empty; // KOrisnicko ime
                TextBox1.Text = string.Empty; // Pristup
                TextBox9.Text = string.Empty; // Lozinka
            }

        // Događaj za dugme "Регистрација"
        protected void Button2_Click(object sender, EventArgs e)
            {
                string punoime = TextBox3.Text.Trim();
                string email = TextBox2.Text.Trim();
                string adminusername = TextBox8.Text.Trim();
                string role = TextBox1.Text.Trim();
                string password = TextBox9.Text.Trim();

                if (!string.IsNullOrEmpty(punoime) && !string.IsNullOrEmpty(email) &&
                    !string.IsNullOrEmpty(adminusername) && !string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(password))
                {
                    // Unos podataka u bazu
                    SaveAdminToDatabase(punoime, email, adminusername, role, password);
                }
                else
                {
                    // Prikaz poruke ako nisu popunjena sva polja
                    Response.Write("<script>alert('Молимо вас да попуните сва поља!');</script>");
                }
            }

        // Funkcija za unos administratora u bazu
        private void SaveAdminToDatabase(string punoime, string email, string adminusername, string role, string password)
        {
            // Generate salt and hash the password
            string salt = GenerateSalt();
            string hashedPassword = HashPasswordWithSalt(password, salt);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO adminKorisnici (punoime, email, adminusername, role, password, salt) " +
                                   "VALUES (@punoime, @email, @adminusername, @role, @password, @salt)"; // Include salt in the query
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@punoime", SqlDbType.NVarChar, 100).Value = punoime;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = email;
                    cmd.Parameters.Add("@adminusername", SqlDbType.NVarChar, 100).Value = adminusername;
                    cmd.Parameters.Add("@role", SqlDbType.NVarChar, 50).Value = role;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = hashedPassword;
                    cmd.Parameters.Add("@salt", SqlDbType.NVarChar, 255).Value = salt;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Response.Write("<script>alert('Администратор је успешно додат!');</script>");
                        ClearFields();
                    }
                    else
                    {
                        Response.Write("<script>alert('Дошло је до грешке. Покушајте поново.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Грешка: " + ex.Message + "\n" + ex.StackTrace + "');</script>");

                }
            }
        }


        // Funkcija za brisanje sadržaja polja nakon uspešne registracije
        private void ClearFields()
            {
                TextBox3.Text = "";
                TextBox2.Text = "";
                TextBox8.Text = "";
                TextBox1.Text = "";
                TextBox9.Text = "";
            }
        
    }
}