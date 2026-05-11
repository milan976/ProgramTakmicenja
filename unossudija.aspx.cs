using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace ProgramTakmicenja
{
    public partial class unossudija : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        private List<Takmicenje> listaTakmicenja = new List<Takmicenje>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mode = Request.QueryString["mode"];
                string sudijaID = Request.QueryString["id"];

                if (mode == "edit" && !string.IsNullOrEmpty(sudijaID))
                {
                    // Режим измене - учитај податке судије
                    lblNaslov.Text = "Измена података судије";
                    UcitajPodatkeZaIzmenu(Convert.ToInt32(sudijaID));
                }
                else
                {
                    // Режим додавања новог
                    lblNaslov.Text = "Унос судија";
                    ClearForm();
                }

                // Иницијализуј листу такмичења
                Session["Takmicenja"] = new List<Takmicenje>();
                BindTakmicenjaGrid();
            }
            else
            {
                // Обнови листу такмичења из Session-а
                if (Session["Takmicenja"] != null)
                {
                    listaTakmicenja = (List<Takmicenje>)Session["Takmicenja"];
                }
            }
        }

        private void UcitajPodatkeZaIzmenu(int sudijaID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"SELECT imePrezime, datumRodjenja, brojTelefona, email, gradMesto, 
                            vatrogasniSavez, dvdDrustvo, sudijaOd, vaznostLicence, vrstaLicence, brojLegitimacije
                            FROM TabelaSudija WHERE SudijeID = @SudijeID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@SudijeID", sudijaID);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Попуни форму са подацима судије
                        txtImePrezime.Text = reader["imePrezime"].ToString();
                        txtDatumRodjenja.Text = FormatDate(reader["datumRodjenja"]);
                        txtBrojTelefona.Text = reader["brojTelefona"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtGrad.Text = reader["gradMesto"].ToString();
                        txtVatrogasniSavez.Text = reader["vatrogasniSavez"].ToString();
                        txtDvd.Text = reader["dvdDrustvo"].ToString();
                        txtSudijaOd.Text = FormatDate(reader["sudijaOd"]);
                        txtVaznostLicence.Text = FormatDate(reader["vaznostLicence"]);
                        txtBrojLegitimacije.Text = reader["brojLegitimacije"].ToString();

                        // Постави врсту лиценце
                        string vrstaLicence = reader["vrstaLicence"].ToString();
                        ddlVrstaLicence.ClearSelection();
                        ddlVrstaLicence.Items.FindByText(vrstaLicence).Selected = true;

                        // Сачувај ID судије у ViewState за касније ажурирање
                        ViewState["SudijaID"] = sudijaID;

                        // Промени текст дугмета
                        btnSacuvaj.Text = "Ажурирај податке";
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ShowToastMessage("Грешка при учитавању података судије: " + ex.Message, "danger");
            }
        }

        protected void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    // Провери да ли је режим измене или додавања
                    if (ViewState["SudijaID"] != null)
                    {
                        // Режим измене
                        AzurirajSudiju(Convert.ToInt32(ViewState["SudijaID"]));
                        ShowToastMessage("Подаци судије су успешно ажурирани!", "success");
                    }
                    else
                    {
                        // Режим додавања новог
                        DodajNovogSudiju();
                        ShowToastMessage("Судија је успешно додат!", "success");
                    }
                }
                catch (Exception ex)
                {
                    ShowToastMessage("Грешка при чувању података: " + ex.Message, "danger");
                }
            }
        }

        private void DodajNovogSudiju()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TabelaSudija 
                            (imePrezime, datumRodjenja, brojTelefona, email, gradMesto, 
                             vatrogasniSavez, dvdDrustvo, sudijaOd, vaznostLicence, vrstaLicence, brojLegitimacije, datumUnosa)
                            VALUES 
                            (@ImePrezime, @DatumRodjenja, @BrojTelefona, @Email, @GradMesto, 
                             @VatrogasniSavez, @DvdDrustvo, @SudijaOd, @VaznostLicence, @VrstaLicence, @BrojLegitimacije, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, con);
                PopuniParametre(cmd);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void AzurirajSudiju(int sudijaID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE TabelaSudija SET 
                        imePrezime = @ImePrezime,
                        datumRodjenja = @DatumRodjenja,
                        brojTelefona = @BrojTelefona,
                        email = @Email,
                        gradMesto = @GradMesto,
                        vatrogasniSavez = @VatrogasniSavez,
                        dvdDrustvo = @DvdDrustvo,
                        sudijaOd = @SudijaOd,
                        vaznostLicence = @VaznostLicence,
                        vrstaLicence = @VrstaLicence,
                        brojLegitimacije = @BrojLegitimacije
                        WHERE SudijeID = @SudijeID";

                SqlCommand cmd = new SqlCommand(query, con);
                PopuniParametre(cmd);
                cmd.Parameters.AddWithValue("@SudijeID", sudijaID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void PopuniParametre(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@ImePrezime", txtImePrezime.Text.Trim());
            cmd.Parameters.AddWithValue("@DatumRodjenja", KonvertujDatum(txtDatumRodjenja.Text));
            cmd.Parameters.AddWithValue("@BrojTelefona", txtBrojTelefona.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@GradMesto", txtGrad.Text.Trim());
            cmd.Parameters.AddWithValue("@VatrogasniSavez", txtVatrogasniSavez.Text.Trim());
            cmd.Parameters.AddWithValue("@DvdDrustvo", txtDvd.Text.Trim());
            cmd.Parameters.AddWithValue("@SudijaOd", KonvertujDatum(txtSudijaOd.Text));
            cmd.Parameters.AddWithValue("@VaznostLicence", KonvertujDatum(txtVaznostLicence.Text));
            cmd.Parameters.AddWithValue("@VrstaLicence", ddlVrstaLicence.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@BrojLegitimacije", txtBrojLegitimacije.Text.Trim());
        }

        private object KonvertujDatum(string datum)
        {
            if (string.IsNullOrEmpty(datum))
                return DBNull.Value;

            if (DateTime.TryParse(datum, out DateTime result))
                return result;

            return DBNull.Value;
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

        private void ClearForm()
        {
            txtImePrezime.Text = "";
            txtDatumRodjenja.Text = "";
            txtBrojTelefona.Text = "";
            txtEmail.Text = "";
            txtGrad.Text = "";
            txtVatrogasniSavez.Text = "";
            txtDvd.Text = "";
            txtSudijaOd.Text = "";
            txtVaznostLicence.Text = "";
            txtBrojLegitimacije.Text = "";
            ddlVrstaLicence.SelectedIndex = 0;
        }

        // Методе за управљање такмичењима
        protected void gvTakmicenja_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DodajRed")
            {
                DodajNovoTakmicenje();
            }
            else if (e.CommandName == "ObrisiRed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                ObrisiTakmicenje(rowIndex);
            }
        }

        private void DodajNovoTakmicenje()
        {
            GridViewRow footerRow = gvTakmicenja.FooterRow;
            TextBox txtNovoTakmicenje = (TextBox)footerRow.FindControl("txtNovoTakmicenje");
            TextBox txtNovaUloga = (TextBox)footerRow.FindControl("txtNovaUloga");

            if (!string.IsNullOrEmpty(txtNovoTakmicenje.Text) && !string.IsNullOrEmpty(txtNovaUloga.Text))
            {
                Takmicenje novo = new Takmicenje
                {
                    RepublickaTakmicenja = txtNovoTakmicenje.Text,
                    UlogaNaTakmicenju = txtNovaUloga.Text
                };

                listaTakmicenja.Add(novo);
                Session["Takmicenja"] = listaTakmicenja;
                BindTakmicenjaGrid();

                txtNovoTakmicenje.Text = "";
                txtNovaUloga.Text = "";
            }
        }

        private void ObrisiTakmicenje(int index)
        {
            if (index >= 0 && index < listaTakmicenja.Count)
            {
                listaTakmicenja.RemoveAt(index);
                Session["Takmicenja"] = listaTakmicenja;
                BindTakmicenjaGrid();
            }
        }

        private void BindTakmicenjaGrid()
        {
            gvTakmicenja.DataSource = listaTakmicenja;
            gvTakmicenja.DataBind();
        }

        protected void gvPregledTakmicenja_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ObrisiRed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                ObrisiTakmicenje(rowIndex);
            }
        }

        private void ShowToastMessage(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toastMessage",
                $"showToastMessage('{message}', '{type}');", true);
        }
    }

    public class Takmicenje
    {
        public string RepublickaTakmicenja { get; set; }
        public string UlogaNaTakmicenju { get; set; }
    }
}