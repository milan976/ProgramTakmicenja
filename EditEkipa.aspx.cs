using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja
{
    public partial class EditEkipaPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Popuni padajuće menije za filtriranje
                UcitajKategorije();
                UcitajEkipe();

                if (Request.QueryString["ekipaID"] != null)
                {
                    int ekipaID = Convert.ToInt32(Request.QueryString["ekipaID"]);
                    hfEkipaID.Value = ekipaID.ToString();
                    UcitajPodatkeEkipe(ekipaID);
                    UcitajClanoveEkipe(ekipaID);

                    // Selektuj odgovarajuće vrednosti u filterima
                    SelektujFiltereZaEkipu(ekipaID);
                }
                else
                {
                    // Ako nema ekipaID u URL-u, možda koristimo filtere
                    if (ddlFilterEkipa.SelectedValue != "")
                    {
                        FiltrirajEkipe();
                    }
                }
            }
        }
        
        // Add these event handlers to your EditEkipaPage class
        protected void ddlFilterKategorija_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFilterKategorija.SelectedItem != null)
            {
                UcitajEkipe();
                ddlFilterEkipa.SelectedIndex = 0;
                ClearForm();
            }
        }

        protected void ddlFilterEkipa_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrirajEkipe();
        }
        private void UcitajKategorije()
        {
            try
            {
                string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string query = "SELECT DISTINCT Kategorija FROM Ekipe ORDER BY Kategorija"; // Add this line
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlFilterKategorija.DataSource = reader;
                    ddlFilterKategorija.DataTextField = "Kategorija";
                    ddlFilterKategorija.DataValueField = "Kategorija";
                    ddlFilterKategorija.DataBind();
                    ddlFilterKategorija.Items.Insert(0, new ListItem("-- Изабери категорију --", ""));
                }
            }
            catch (Exception ex)
            {
                lblPoruka.Text = "Грешка при учитавању категорија: " + ex.Message;
                lblPoruka.CssClass = "text-danger";
            }
        }

        private void UcitajEkipe()
        {
            string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

            string kategorija = ddlFilterKategorija.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT EkipaID, NazivEkipe FROM Ekipe";

                if (!string.IsNullOrEmpty(kategorija))
                {
                    query += " WHERE Kategorija = @Kategorija";
                }

                query += " ORDER BY NazivEkipe";

                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(kategorija))
                {
                    cmd.Parameters.AddWithValue("@Kategorija", kategorija);
                }

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlFilterEkipa.DataSource = reader;
                ddlFilterEkipa.DataTextField = "NazivEkipe";
                ddlFilterEkipa.DataValueField = "EkipaID";
                ddlFilterEkipa.DataBind();

                ddlFilterEkipa.Items.Insert(0, new ListItem("-- Изабери екипу --", ""));
            }
        }

        private void SelektujFiltereZaEkipu(int ekipaID)
        {
            string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Kategorija, NazivEkipe FROM Ekipe WHERE EkipaID = @EkipaID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string kategorija = reader["Kategorija"].ToString();
                    string nazivEkipe = reader["NazivEkipe"].ToString();

                    // Selektuj kategoriju
                    if (ddlFilterKategorija.Items.FindByValue(kategorija) != null)
                    {
                        ddlFilterKategorija.SelectedValue = kategorija;
                    }

                    // Osveži padajući meni za ekipe
                    UcitajEkipe();

                    // Selektuj ekipu
                    if (ddlFilterEkipa.Items.FindByValue(ekipaID.ToString()) != null)
                    {
                        ddlFilterEkipa.SelectedValue = ekipaID.ToString();
                    }
                }
                reader.Close();
            }
        }

        private void FiltrirajEkipe()
        {
            if (ddlFilterEkipa.SelectedValue != "")
            {
                int ekipaID = Convert.ToInt32(ddlFilterEkipa.SelectedValue);
                hfEkipaID.Value = ekipaID.ToString();

                // Load all team data including date, OVS, coach and leader
                UcitajPodatkeEkipe(ekipaID);

                // Load team members
                UcitajClanoveEkipe(ekipaID);
            }
            else
            {
                // Clear form if no team is selected
                ClearForm();
            }
        }

        protected void UcitajPodatkeEkipe(int ekipaID)
        {
            string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT * FROM Ekipe WHERE EkipaID = @EkipaID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Basic info
                    hfEkipaID.Value = ekipaID.ToString();
                    txtNazivEkipe.Text = reader["NazivEkipe"].ToString();

                    // Additional info
                    txtOVS.Text = reader["OVS"] != DBNull.Value ? reader["OVS"].ToString() : "";
                    txtDatumUnosa.Text = reader["DatumUnosa"] != DBNull.Value ?
                        Convert.ToDateTime(reader["DatumUnosa"]).ToString("dd.MM.yyyy") : "";
                    txtTrener.Text = reader["Trener"] != DBNull.Value ? reader["Trener"].ToString() : "";
                    txtVodjaEkipe.Text = reader["VodjaEkipe"] != DBNull.Value ? reader["VodjaEkipe"].ToString() : "";
                }
                reader.Close();
            }
        }

        private void UcitajClanoveEkipe(int ekipaID)
        {
            string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT ClanID, RedniBroj, Uloga, ImePrezime, 
                CONVERT(varchar, DatumRodjenja, 104) AS DatumRodjenja,
                DATEDIFF(YEAR, DatumRodjenja, GETDATE()) - 
                CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, DatumRodjenja, GETDATE()), DatumRodjenja) > GETDATE() 
                THEN 1 ELSE 0 END AS Godine
                FROM Clanovi 
                WHERE EkipaID = @EkipaID
                ORDER BY RedniBroj";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvClanovi.DataSource = dt;
                gvClanovi.DataBind();

                // Add calculations to footer
                if (dt.Rows.Count > 0)
                {
                    int zbirGodina = dt.AsEnumerable()
                        .Where(row => !row.IsNull("Godine"))
                        .Sum(row => Convert.ToInt32(row["Godine"]));

                    string kategorija = ddlFilterKategorija.SelectedItem.Text;
                    var (pocetniBodovi, zadatoVreme) = OdrediPocetneBodoveIVreme(zbirGodina);

                    // Create footer row
                    GridViewRow footerRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal);
                    footerRow.CssClass = "table-footer";

                    // Total years (span 3 columns)
                    TableCell zbirCell = new TableCell();
                    zbirCell.ColumnSpan = 3;
                    zbirCell.Text = $"<b>Укупан збир година такмичара: {zbirGodina}</b>";
                    zbirCell.CssClass = "text-center";
                    footerRow.Cells.Add(zbirCell);

                    // Starting points
                    TableCell bodoviCell = new TableCell();
                    bodoviCell.Text = $"<b>Почетни бодови: {pocetniBodovi}</b>";
                    footerRow.Cells.Add(bodoviCell);

                    // Assigned time (only for juniors)
                    if (kategorija.Contains("Јуниори"))
                    {
                        TableCell vremeCell = new TableCell();
                        vremeCell.Text = $"<b>Задато време: {zadatoVreme}</b>";
                        footerRow.Cells.Add(vremeCell);
                    }

                    // Add footer to GridView
                    gvClanovi.Controls[0].Controls.Add(footerRow);
                }
            }
        }
        private (int PocetniBodovi, string ZadatoVreme) OdrediPocetneBodoveIVreme(int zbirGodina)
        {
            string kategorija = ddlFilterKategorija.SelectedItem.Text;
            int pocetniBodovi = 0;
            string zadatoVreme = "";

            // 1. Pravila za Podmladak
            if (kategorija.Contains("Ватрогасни подмладак"))
            {
                pocetniBodovi = 400;

                // Penalizacija po godinama
                if (zbirGodina >= 63 && zbirGodina <= 71) pocetniBodovi -= 4;
                else if (zbirGodina >= 72 && zbirGodina <= 80) pocetniBodovi -= 8;
                else if (zbirGodina >= 81 && zbirGodina <= 89) pocetniBodovi -= 12;
                else if (zbirGodina >= 90 && zbirGodina <= 98) pocetniBodovi -= 16;
                else if (zbirGodina >= 99) pocetniBodovi -= 20;
            }
            // 2. Pravila za Juniore
            else if (kategorija.Contains("Јуниори"))
            {
                if (zbirGodina <= 103)
                {
                    pocetniBodovi = 1000;
                    zadatoVreme = "83";
                }
                else if (zbirGodina <= 112)
                {
                    pocetniBodovi = 997;
                    zadatoVreme = "80";
                }
                else if (zbirGodina <= 121)
                {
                    pocetniBodovi = 994;
                    zadatoVreme = "77";
                }
                else if (zbirGodina <= 130)
                {
                    pocetniBodovi = 991;
                    zadatoVreme = "74";
                }
                else if (zbirGodina <= 139)
                {
                    pocetniBodovi = 988;
                    zadatoVreme = "71";
                }
                else
                {
                    pocetniBodovi = 985;
                    zadatoVreme = "68";
                }
            }
            // 3. Pravila za Seniore i Profesionalce A
            else if (kategorija.Contains("ДВД класа А") || kategorija.Contains("Професионалци класа А"))
            {
                pocetniBodovi = 500;

            }

            // 4. Pravila za Seniore i Profesionalce
            else if (kategorija.Contains("ДВД класа Б") || kategorija.Contains("Професионалци класа Б"))
            {
                pocetniBodovi = 500;

                // Dodatni bodovi po rasponima
                if (zbirGodina >= 240 && zbirGodina <= 247) pocetniBodovi += 1;
                else if (zbirGodina >= 248 && zbirGodina <= 255) pocetniBodovi += 2;
                else if (zbirGodina >= 256 && zbirGodina <= 263) pocetniBodovi += 3;
                else if (zbirGodina >= 264 && zbirGodina <= 271) pocetniBodovi += 4;
                else if (zbirGodina >= 272 && zbirGodina <= 279) pocetniBodovi += 5;
                else if (zbirGodina >= 280 && zbirGodina <= 287) pocetniBodovi += 6;
                else if (zbirGodina >= 288 && zbirGodina <= 295) pocetniBodovi += 7;
                else if (zbirGodina >= 296 && zbirGodina <= 303) pocetniBodovi += 8;
                else if (zbirGodina >= 304 && zbirGodina <= 311) pocetniBodovi += 9;
                else if (zbirGodina >= 312 && zbirGodina <= 319) pocetniBodovi += 10;
                else if (zbirGodina >= 320 && zbirGodina <= 327) pocetniBodovi += 11;
                else if (zbirGodina >= 328 && zbirGodina <= 335) pocetniBodovi += 12;
                else if (zbirGodina >= 336 && zbirGodina <= 343) pocetniBodovi += 13;
                else if (zbirGodina >= 344 && zbirGodina <= 351) pocetniBodovi += 14;
                else if (zbirGodina >= 352 && zbirGodina <= 359) pocetniBodovi += 15;
                else if (zbirGodina >= 360 && zbirGodina <= 367) pocetniBodovi += 16;
                else if (zbirGodina >= 368 && zbirGodina <= 375) pocetniBodovi += 17;
                else if (zbirGodina >= 376 && zbirGodina <= 383) pocetniBodovi += 18;
                else if (zbirGodina >= 384 && zbirGodina <= 391) pocetniBodovi += 19;
                else if (zbirGodina >= 392 && zbirGodina <= 399) pocetniBodovi += 20;
                else if (zbirGodina >= 400 && zbirGodina <= 407) pocetniBodovi += 21;
                else if (zbirGodina >= 408 && zbirGodina <= 415) pocetniBodovi += 22;
                else if (zbirGodina >= 416 && zbirGodina <= 423) pocetniBodovi += 23;
                else if (zbirGodina >= 424 && zbirGodina <= 431) pocetniBodovi += 24;
                else if (zbirGodina >= 432 && zbirGodina <= 439) pocetniBodovi += 25;
                else if (zbirGodina >= 440 && zbirGodina <= 447) pocetniBodovi += 26;
                else if (zbirGodina >= 448 && zbirGodina <= 455) pocetniBodovi += 27;
                else if (zbirGodina >= 456 && zbirGodina <= 463) pocetniBodovi += 28;
                else if (zbirGodina >= 464 && zbirGodina <= 471) pocetniBodovi += 29;
                else if (zbirGodina >= 472 && zbirGodina <= 479) pocetniBodovi += 30;
                else if (zbirGodina >= 480 && zbirGodina <= 487) pocetniBodovi += 31;
                else if (zbirGodina >= 488 && zbirGodina <= 495) pocetniBodovi += 32;
                else if (zbirGodina >= 496 && zbirGodina <= 503) pocetniBodovi += 33;
                else if (zbirGodina >= 504 && zbirGodina <= 511) pocetniBodovi += 34;
                else if (zbirGodina >= 512) pocetniBodovi += 35; // Maksimalni dodatni bodovi
            }

            return (pocetniBodovi, zadatoVreme);
        }
        protected void AzurirajOsnovnePodatkeEkipe()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Ekipe 
                        SET NazivEkipe = @NazivEkipe, 
                            OVS = @OVS,
                            DatumUnosa = @DatumUnosa,
                            Trener = @Trener,
                            VodjaEkipe = @VodjaEkipe
                        WHERE EkipaID = @EkipaID";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@EkipaID", hfEkipaID.Value);
                cmd.Parameters.AddWithValue("@NazivEkipe", txtNazivEkipe.Text);
                cmd.Parameters.AddWithValue("@OVS", string.IsNullOrEmpty(txtOVS.Text) ? (object)DBNull.Value : txtOVS.Text);
                cmd.Parameters.AddWithValue("@DatumUnosa", string.IsNullOrEmpty(txtDatumUnosa.Text) ? (object)DBNull.Value : DateTime.Parse(txtDatumUnosa.Text));
                cmd.Parameters.AddWithValue("@Trener", string.IsNullOrEmpty(txtTrener.Text) ? (object)DBNull.Value : txtTrener.Text);
                cmd.Parameters.AddWithValue("@VodjaEkipe", string.IsNullOrEmpty(txtVodjaEkipe.Text) ? (object)DBNull.Value : txtVodjaEkipe.Text);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        protected void btnSacuvajIzmene_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfEkipaID.Value))
            {
                lblPoruka.Text = "Грешка: Није пронађен ID екипе.";
                lblPoruka.CssClass = "text-danger";
                return;
            }

            try
            {
                int ekipaID = Convert.ToInt32(hfEkipaID.Value);
                string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = @"UPDATE Ekipe SET 
                    NazivEkipe = @NazivEkipe,
                    OVS = @OVS,
                    DatumUnosa = @DatumUnosa,
                    Trener = @Trener,
                    VodjaEkipe = @VodjaEkipe
                    WHERE EkipaID = @EkipaID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NazivEkipe", txtNazivEkipe.Text);
                    cmd.Parameters.AddWithValue("@OVS", string.IsNullOrEmpty(txtOVS.Text) ? (object)DBNull.Value : txtOVS.Text);
                    cmd.Parameters.AddWithValue("@DatumUnosa", string.IsNullOrEmpty(txtDatumUnosa.Text) ? (object)DBNull.Value : DateTime.Parse(txtDatumUnosa.Text));
                    cmd.Parameters.AddWithValue("@Trener", string.IsNullOrEmpty(txtTrener.Text) ? (object)DBNull.Value : txtTrener.Text);
                    cmd.Parameters.AddWithValue("@VodjaEkipe", string.IsNullOrEmpty(txtVodjaEkipe.Text) ? (object)DBNull.Value : txtVodjaEkipe.Text);
                    cmd.Parameters.AddWithValue("@EkipaID", ekipaID);

                    conn.Open();
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        lblPoruka.Text = "Подаци су успешно сачувани!";
                        lblPoruka.CssClass = "text-success";
                        OcistiPolja();
                    }
                    else
                    {
                        lblPoruka.Text = "Грешка: Није пронађена екипа за ажурирање.";
                        lblPoruka.CssClass = "text-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                lblPoruka.Text = "Дошло је до грешке при чувању података: " + ex.Message;
                lblPoruka.CssClass = "text-danger";
            }
        }
        protected void OcistiPolja()
        {
            // Osnovni podaci ekipe
            txtNazivEkipe.Text = string.Empty;
            txtOVS.Text = string.Empty;
            txtDatumUnosa.Text = string.Empty;

            // Odgovorna lica
            txtTrener.Text = string.Empty;
            txtVodjaEkipe.Text = string.Empty;

            // Očisti GridView sa članovima
            gvClanovi.DataSource = null;
            gvClanovi.DataBind();

            // Očisti hidden polje sa ID-om ekipe
            hfEkipaID.Value = string.Empty;

            // Resetujte dropdown liste
            ddlFilterKategorija.SelectedIndex = 0;
            ddlFilterEkipa.SelectedIndex = 0;
            ddlFilterEkipa.Items.Clear();
            ddlFilterEkipa.Items.Insert(0, new ListItem("-- Изабери екипу --", ""));

            // Fokusiraj se na prvo polje
            txtNazivEkipe.Focus();
        }
        protected void gvClanovi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvClanovi.EditIndex = e.NewEditIndex;
            UcitajClanoveEkipe(Convert.ToInt32(hfEkipaID.Value));
        }
        protected string GetSelectedUlogaValue(object dataItem)
        {
            DataRowView rowView = (DataRowView)dataItem;
            string ulogaValue = rowView["Uloga"].ToString();

            // Validate the value exists in the dropdown items
            if (string.IsNullOrEmpty(ulogaValue) ||
                !new[] { "Играч", "Капитен", "Тренер" }.Contains(ulogaValue))
            {   
                return ""; // Returns empty to select the default "Изаберите улогу"
            }

            return ulogaValue;
        }
        protected string GetUlogaText(object ulogaID)
        {
            if (ulogaID == null) return string.Empty;

            switch (ulogaID.ToString())
            {
                case "1": return "Командир";
                case "2": return "Курир";
                case "3": return "Моториста";
                case "4": return "Навални 1";
                case "5": return "Навални 2";
                case "6": return "Цевни 1";
                case "7": return "Цевни 2";
                case "8": return "Водни 1";
                case "9": return "Водни 2";
                case "10": return "Резерва";
                default: return "Непозната улога";
            }
        }
        protected void gvClanovi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvClanovi.Rows[e.RowIndex];
            int clanID = Convert.ToInt32(gvClanovi.DataKeys[e.RowIndex].Value);

            // Get values from controls
            string redniBroj = ((TextBox)row.FindControl("txtEditRedniBroj")).Text;
            string uloga = ((DropDownList)row.FindControl("ddlEditUloga")).SelectedValue;
            string imePrezime = ((TextBox)row.FindControl("txtEditImePrezime")).Text;
            string datumRodjenjaText = ((TextBox)row.FindControl("txtEditDatumRodjenja")).Text;

            // Parse the date with validation
            if (!DateTime.TryParse(datumRodjenjaText, out DateTime datumRodjenja))
            {
                lblPoruka.Text = "Неисправан формат датума! Користите ДД.ММ.ГГГГ";
                lblPoruka.CssClass = "text-danger";
                return;
            }

            string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                // UPDATE query - include all needed columns
                string query = @"UPDATE Clanovi SET 
                        RedniBroj = @RedniBroj,
                        Uloga = @Uloga,
                        ImePrezime = @ImePrezime,
                        DatumRodjenja = @DatumRodjenja
                        WHERE ClanID = @ClanID";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters - each one only once
                    cmd.Parameters.Add("@RedniBroj", SqlDbType.Int).Value = Convert.ToInt32(redniBroj);
                    cmd.Parameters.Add("@Uloga", SqlDbType.NVarChar, 50).Value = uloga;
                    cmd.Parameters.Add("@ImePrezime", SqlDbType.NVarChar, 100).Value = imePrezime;
                    cmd.Parameters.Add("@DatumRodjenja", SqlDbType.DateTime).Value = datumRodjenja;
                    cmd.Parameters.Add("@ClanID", SqlDbType.Int).Value = clanID;

                    try
                    {
                        conn.Open();
                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            gvClanovi.EditIndex = -1;
                            UcitajClanoveEkipe(Convert.ToInt32(hfEkipaID.Value));
                            lblPoruka.Text = "Подаци о члану су успешно ажурирани.";
                            lblPoruka.CssClass = "text-success";
                        }
                    }
                    catch (SqlException ex)
                    {
                        lblPoruka.Text = "Грешка при ажурирању: " + ex.Message;
                        lblPoruka.CssClass = "text-danger";
                    }
                }
            }
        }

        protected void gvClanovi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvClanovi.EditIndex = -1;
            UcitajClanoveEkipe(Convert.ToInt32(hfEkipaID.Value));
        }

        
        private int GetNextRedniBroj(int ekipaID)
        {
            string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT ISNULL(MAX(RedniBroj), 0) + 1 FROM Clanovi WHERE EkipaID = @EkipaID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        protected void gvClanovi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
                (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                DropDownList ddlUloga = (DropDownList)e.Row.FindControl("ddlEditUloga");
                string currentUloga = DataBinder.Eval(e.Row.DataItem, "Uloga")?.ToString();

                if (ddlUloga != null && !string.IsNullOrEmpty(currentUloga))
                {
                    ddlUloga.SelectedValue = currentUloga;
                }
            }
        }
        protected void btnObrisiEkipu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfEkipaID.Value))
            {
                lblPoruka.Text = "Грешка: Није пронађен ID екипе.";
                lblPoruka.CssClass = "text-danger";
                return;
            }

            int ekipaID = Convert.ToInt32(hfEkipaID.Value);
            string connString = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Прво обриши све везе у podmladakRezultati
                            string deleteRezultatiQuery = "DELETE FROM podmladakRezultati WHERE EkipaID = @EkipaID";
                            using (SqlCommand cmd = new SqlCommand(deleteRezultatiQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);
                                cmd.ExecuteNonQuery();
                            }

                            // 2. Затим обриши екипу
                            string deleteEkipaQuery = "DELETE FROM Ekipe WHERE EkipaID = @EkipaID";
                            using (SqlCommand cmd = new SqlCommand(deleteEkipaQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@EkipaID", ekipaID);
                                int affectedRows = cmd.ExecuteNonQuery();

                                if (affectedRows > 0)
                                {
                                    transaction.Commit();
                                    lblPoruka.Text = "Екипа је успешно обрисана.";
                                    lblPoruka.CssClass = "text-success";
                                    ClearForm();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    lblPoruka.Text = "Грешка: Екипа није пронађена.";
                                    lblPoruka.CssClass = "text-danger";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblPoruka.Text = "Грешка при брисању екипе: " + ex.Message;
                lblPoruka.CssClass = "text-danger";
            }
        }
        private void ClearForm()
        {
            txtDatumUnosa.Text = "";
            txtNazivEkipe.Text = "";
            txtOVS.Text = "";
            txtTrener.Text = "";
            txtVodjaEkipe.Text = "";
            gvClanovi.DataSource = null;
            gvClanovi.DataBind();
            hfEkipaID.Value = "";
        }
        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            // Redirect back to the previous page or team list
            Response.Redirect("~/EditEkipa.aspx");
        }
    }
}
    
