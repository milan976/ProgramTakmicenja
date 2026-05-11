using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja
{
    public partial class profilsudija : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                UpdateTotalCount();
                ClearForm();
            }
        }

        protected void BindGridView()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT SudijeID, imePrezime, gradMesto FROM TabelaSudija ORDER BY imePrezime";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    gvSudije.DataSource = reader;
                    gvSudije.DataBind();

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ShowToastMessage("Грешка при учитавању судија: " + ex.Message, "danger");
                gvSudije.DataSource = null;
                gvSudije.DataBind();
            }
        }

        protected void UpdateTotalCount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM TabelaSudija";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    lblUkupnoSudija.Text = "Укупно судија " + count;
                }
            }
            catch (Exception ex)
            {
                lblUkupnoSudija.Text = "Укупно судија 0";
                ShowToastMessage("Грешка при бројању судија: " + ex.Message, "danger");
            }
        }

        protected void gvSudije_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvSudije.SelectedIndex >= 0)
                {
                    int sudijaID = Convert.ToInt32(gvSudije.DataKeys[gvSudije.SelectedIndex].Value);
                    LoadJudgeDetails(sudijaID);

                    // Позивање JavaScript функције за уклањање "empty-form" стила
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "removeEmptyClass", "removeEmptyFormClass();", true);

                    // Прикажи поруку
                    ShowToastMessage("Судија је изабран успешно!", "success");
                }
            }
            catch (Exception ex)
            {
                ShowToastMessage("Грешка при одабиру судије: " + ex.Message, "danger");
            }
        }

        protected void gvSudije_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSudije, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
            }
        }

        // ДОДАТО: Дугме за измену судије - ОВАЈ МЕТОД ЈЕ БИО НЕДОСТАЈАО
        protected void btnIzmeniSudiju_Click(object sender, EventArgs e)
        {
            if (gvSudije.SelectedIndex >= 0)
            {
                int sudijaID = Convert.ToInt32(gvSudije.DataKeys[gvSudije.SelectedIndex].Value);
                // Пренеси ID судије на страницу за унос и отвори је у режиму измене
                Response.Redirect($"unossudija.aspx?mode=edit&id={sudijaID}");
            }
            else
            {
                ShowToastMessage("Молимо изаберите судију пре него што кликнете на измену.", "warning");
            }
        }

        private void LoadJudgeDetails(int sudijaID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"SELECT imePrezime, datumRodjenja, brojTelefona, email, gradMesto, 
                                    vatrogasniSavez, dvdDrustvo, sudijaOd, vaznostLicence, vrstaLicence 
                                    FROM TabelaSudija WHERE SudijeID = @SudijeID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@SudijeID", sudijaID);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Попуњавање текстуалних поља
                        txtImePrezime.Text = reader["imePrezime"].ToString();
                        txtDatumRodjenja.Text = FormatDate(reader["datumRodjenja"]);
                        txtBrojTelefona.Text = reader["brojTelefona"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtGradMesto.Text = reader["gradMesto"].ToString();
                        txtVatrogasniSavez.Text = reader["vatrogasniSavez"].ToString();
                        txtDvdDrustvo.Text = reader["dvdDrustvo"].ToString();
                        txtSudijaOd.Text = FormatDate(reader["sudijaOd"]);
                        txtVaznostLicence.Text = FormatDate(reader["vaznostLicence"]);
                        txtVrstaLicence.Text = reader["vrstaLicence"].ToString();

                        // Провера статуса лиценце
                        string statusText = "";
                        string statusClass = "";

                        if (reader["vaznostLicence"] != DBNull.Value)
                        {
                            string rawDate = reader["vaznostLicence"].ToString();
                            DateTime datumVaznosti;
                            bool parseSuccess = false;

                            parseSuccess = DateTime.TryParseExact(rawDate, "MM/dd/yyyy",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out datumVaznosti);

                            if (!parseSuccess)
                            {
                                parseSuccess = DateTime.TryParseExact(rawDate, "dd.MM.yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None, out datumVaznosti);
                            }

                            if (!parseSuccess)
                            {
                                parseSuccess = DateTime.TryParseExact(rawDate, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None, out datumVaznosti);
                            }

                            if (!parseSuccess)
                            {
                                parseSuccess = DateTime.TryParse(rawDate, out datumVaznosti);
                            }

                            if (parseSuccess)
                            {
                                if (datumVaznosti > DateTime.Today)
                                {
                                    statusText = "Активан";
                                    statusClass = "badge rounded-pill badge-success";
                                }
                                else
                                {
                                    statusText = "Не активан";
                                    statusClass = "badge rounded-pill badge-danger";
                                }
                            }
                            else
                            {
                                statusText = "Неисправан датум";
                                statusClass = "badge rounded-pill badge-warning";
                            }
                        }
                        else
                        {
                            statusText = "Нема датума";
                            statusClass = "badge rounded-pill badge-info";
                        }

                        lblStatus.Text = statusText;
                        lblStatus.CssClass = statusClass;
                    }
                    else
                    {
                        ShowToastMessage("Подаци за изабрану судију нису пронађени.", "warning");
                        ClearForm();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ShowToastMessage("Грешка при учитавању детаља судије: " + ex.Message, "danger");
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtImePrezime.Text = "";
            txtDatumRodjenja.Text = "";
            txtBrojTelefona.Text = "";
            txtEmail.Text = "";
            txtGradMesto.Text = "";
            txtVatrogasniSavez.Text = "";
            txtDvdDrustvo.Text = "";
            txtSudijaOd.Text = "";
            txtVaznostLicence.Text = "";
            txtVrstaLicence.Text = "";

            lblStatus.Text = "Није изабрано";
            lblStatus.CssClass = "badge rounded-pill text-bg-info";
        }

        private string FormatDate(object dateValue)
        {
            if (dateValue == null || dateValue == DBNull.Value)
                return "";

            string rawDate = dateValue.ToString().Trim();

            if (string.IsNullOrEmpty(rawDate))
                return "";

            DateTime date;

            if (DateTime.TryParseExact(rawDate, "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out date))
            {
                return date.ToString("dd.MM.yyyy");
            }

            var culture = new System.Globalization.CultureInfo("sr-Latn-RS");
            if (DateTime.TryParse(rawDate, culture, System.Globalization.DateTimeStyles.None, out date))
            {
                return date.ToString("dd.MM.yyyy");
            }

            return rawDate;
        }

        private void ShowToastMessage(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toastMessage",
                $"showToastMessage('{message}', '{type}');", true);
        }
    }
}