using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace ProgramTakmicenja
{
    public partial class korprofil : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["adminusername"] == null)
                {
                    Response.Redirect("adminlogin.aspx");
                    return;
                }

                string adminusername = Session["adminusername"].ToString();
                LoadUserData(adminusername);
            }
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
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
                string saltedPassword = password + salt;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(bytes);
            }
        }

        private void LoadUserData(string adminusername)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // ISPRAVKA: Koristite adminPassword umesto password
                    string query = "SELECT punoime, email, adminusername, adminPassword, salt FROM adminkorisnici WHERE adminusername = @adminusername";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@adminusername", adminusername);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TextBox3.Text = reader["punoime"].ToString();
                                TextBox2.Text = reader["email"].ToString();
                                TextBox8.Text = reader["adminusername"].ToString();
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "alert('Корисничко име не постоји.');", true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('Грешка: {ex.Message}');", true);
                }
            }
        }

        private bool IsSameAsOldPassword(string newPassword)
        {
            string currentPasswordHash = string.Empty;
            string currentSalt = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT adminPassword, salt FROM adminkorisnici WHERE adminusername = @adminusername";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@adminusername", Session["adminusername"].ToString());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentPasswordHash = reader["adminPassword"].ToString();
                                currentSalt = reader["salt"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(currentSalt))
                return false;

            string newPasswordHash = HashPasswordWithSalt(newPassword, currentSalt);
            return currentPasswordHash == newPasswordHash;
        }

        private bool ValidatePassword(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(password))
            {
                errorMessage = "Лозинка не сме бити празна.";
                return false;
            }
            if (password.Length < 8)
            {
                errorMessage = "Лозинка мора имати најмање 8 карактера.";
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                errorMessage = "Лозинка мора садржати бар један број.";
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                errorMessage = "Лозинка мора садржати бар једно велико слово.";
                return false;
            }
            if (!password.Any(char.IsLower))
            {
                errorMessage = "Лозинка мора садржати бар једно мало слово.";
                return false;
            }

            return true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string newPassword = TextBox9.Text.Trim();
            string confirmPassword = TextBox1.Text.Trim();

            // Validacija
            if (newPassword != confirmPassword)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Нова лозинка и потврда лозинке се не поклапају.');", true);
                return;
            }

            if (!ValidatePassword(newPassword, out string errorMessage))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('{errorMessage}');", true);
                return;
            }

            if (IsSameAsOldPassword(newPassword))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Нова лозинка не може бити иста као стара лозинка.');", true);
                return;
            }

            UpdatePassword(newPassword);
        }

        private void UpdatePassword(string newPassword)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string salt = GenerateSalt();
                    string hashedPassword = HashPasswordWithSalt(newPassword, salt);

                    // ISPRAVKA: Koristite adminPassword umesto password
                    string query = "UPDATE adminkorisnici SET adminPassword = @password, salt = @salt WHERE adminusername = @adminusername";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@adminusername", Session["adminusername"].ToString());

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('Лозинка је успешно промењена!');", true);

                            // Očisti polja
                            TextBox9.Text = string.Empty;
                            TextBox1.Text = string.Empty;
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('Није било промена у бази података.');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('Дошло је до грешке: {ex.Message}');", true);
                }
            }
        }

        private void LogError(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

            // Možete dodati logovanje u fajl ili bazu
            // System.IO.File.AppendAllText(@"C:\logs\error.log", $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n\n");
        }
    }
}