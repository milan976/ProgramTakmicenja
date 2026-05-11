using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Text.RegularExpressions;
using Microsoft.IO;
using System.Globalization;

namespace ProgramTakmicenja
{
    public partial class unosEkipaPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Postavite JavaScript potvrdu za dugme
                ButtonUnosEkipe.OnClientClick = "return confirm('Да ли сте сигурни да желите да сачувате екипу?');";
                // Inicijalizacija ViewState-a za Clanove ako nije postavljen

                if (ViewState["Clanovi"] == null)
                {
                    ViewState["Clanovi"] = new List<Clan>();
                }

                // Prikaz trenutnog datuma u TextBox-u
                TextBoxDatumUnosa.Text = DateTime.Now.ToString("dd.MM.yyyy");

                // Inicijalizuj tabelu
                InitializeClanoviTable();
            }
        }

        protected void DropDownListKategorija_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Clanovi.Count > 0)
            {
                AzurirajTabelu();
            }
        }

        private void InitializeClanoviTable()
        {
            // Očistite tabelu prvo
            TableClanovi.Rows.Clear();

            // Dodajemo red sa zaglavljem tablice
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(new TableCell { Text = "Ред. број", CssClass = "fw-bold" });
            headerRow.Cells.Add(new TableCell { Text = "Улога", CssClass = "fw-bold" });
            headerRow.Cells.Add(new TableCell { Text = "Име и презиме", CssClass = "fw-bold" });
            headerRow.Cells.Add(new TableCell { Text = "Датум рођења", CssClass = "fw-bold" });
            headerRow.Cells.Add(new TableCell { Text = "Године", CssClass = "fw-bold" });
            headerRow.Cells.Add(new TableCell { Text = "Акција", CssClass = "fw-bold" });

            TableClanovi.Rows.Add(headerRow);
        }

        private List<Clan> Clanovi
        {
            get
            {
                if (ViewState["Clanovi"] == null)
                {
                    ViewState["Clanovi"] = new List<Clan>();
                }
                return (List<Clan>)ViewState["Clanovi"];
            }
            set
            {
                ViewState["Clanovi"] = value;
            }
        }

        [Serializable]
        public class Clan
        {
            public int RedniBroj { get; set; }
            public string Uloga { get; set; }
            public string ImePrezime { get; set; }
            public DateTime? DatumRodjenja { get; set; }
            public int? Godine { get; set; }
        }

        protected void ButtonDodajClana_Click(object sender, EventArgs e)
        {
            // Proverite validaciju podataka
            if (DropDownListKategorija.SelectedValue == "-1" ||
                DropDownListUloga.SelectedValue == "-1" ||
                string.IsNullOrWhiteSpace(TextBoxImePrezime.Text))
            {
                LabelMessage.Text = "Молим попуните сва поља.";
                LabelMessage.CssClass = "text-danger";
                return;
            }

            // Izvuci ulogu iz DropDownList
            string ulogaText = DropDownListUloga.SelectedItem.Text;
            Clan noviClan = new Clan()
            {
                RedniBroj = Clanovi.Count + 1,
                Uloga = ulogaText,
                ImePrezime = TextBoxImePrezime.Text,
                DatumRodjenja = null,
                Godine = null
            };

            // Obrada datuma rođenja (nije obavezno)
            if (!string.IsNullOrWhiteSpace(TextBoxDatumRodjenja.Text))
            {
                DateTime datumRodjenja;
                if (!DateTime.TryParseExact(TextBoxDatumRodjenja.Text, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out datumRodjenja))
                {
                    LabelMessage.Text = "Унесите датум у формату dd.MM.yyyy или оставите празно.";
                    LabelMessage.CssClass = "text-danger";
                    return;
                }

                // Provera da li je datum rođenja manji od trenutnog datuma
                if (datumRodjenja > DateTime.Now)
                {
                    LabelMessage.Text = "Датум рођења не може бити у будућности.";
                    LabelMessage.CssClass = "text-danger";
                    return;
                }

                // Postavi datum rodjenja
                noviClan.DatumRodjenja = datumRodjenja;

                // Izračun godina
                int godine = DateTime.Now.Year - datumRodjenja.Year;
                if (DateTime.Now.Month < datumRodjenja.Month ||
                    (DateTime.Now.Month == datumRodjenja.Month && DateTime.Now.Day < datumRodjenja.Day))
                {
                    godine--;
                }

                // Postavi godine
                noviClan.Godine = godine;

                // Provera na osnovu kategorije samo ako su godine unete
                string kategorija = DropDownListKategorija.SelectedItem.Text;

                if ((kategorija == "Ватрогасни подмладак - мушка" || kategorija == "Ватрогасни подмладак - жене") && godine > 9)
                {
                    LabelMessage.Text = "Члан категорије Ватрогасни подмлатак не сме бити старији од 9 година.";
                    LabelMessage.CssClass = "text-danger";
                    return;
                }

                if ((kategorija == "Јуниори - мушка" || kategorija == "Јуниори - жене") && godine > 16)
                {
                    LabelMessage.Text = "Члан категорије Јуниори не сме бити старији од 16 година.";
                    LabelMessage.CssClass = "text-danger";
                    return;
                }

                if ((kategorija == "Сениори класа Б - мушка" || kategorija == "Сениори класа Б - жене" ||
                     kategorija == "Професионалци класа Б - мушка" || kategorija == "Професионалци класа Б - жене") && godine < 30)
                {
                    LabelMessage.Text = "Члан категорије Сениори или Професионалци не сме бити млађи од 30 година.";
                    LabelMessage.CssClass = "text-danger";
                    return;
                }
            }

            // Dodajte člana u ViewState
            Clanovi.Add(noviClan);
            AzurirajTabelu();

            // Očistite unosne kontrole
            DropDownListUloga.SelectedValue = "-1";
            TextBoxImePrezime.Text = string.Empty;
            TextBoxDatumRodjenja.Text = string.Empty;

            LabelMessage.Text = "Члан успешно додат.";
            LabelMessage.CssClass = "text-success";
        }

        private void AzurirajTabelu()
        {
            // Očistite tabelu osim zaglavlja
            TableClanovi.Rows.Clear();
            InitializeClanoviTable();

            // Dodajte redove za svakog člana
            foreach (var clan in Clanovi)
            {
                TableRow row = new TableRow();

                row.Cells.Add(new TableCell { Text = clan.RedniBroj.ToString() });
                row.Cells.Add(new TableCell { Text = clan.Uloga });
                row.Cells.Add(new TableCell { Text = clan.ImePrezime });
                row.Cells.Add(new TableCell { Text = clan.DatumRodjenja.HasValue ? clan.DatumRodjenja.Value.ToString("dd.MM.yyyy") : "N/A" });
                row.Cells.Add(new TableCell { Text = clan.Godine.HasValue ? clan.Godine.Value.ToString() : "N/A" });

                TableCell deleteCell = new TableCell();
                Button deleteButton = new Button
                {
                    Text = "Обриши",
                    CommandArgument = clan.RedniBroj.ToString(),
                    CssClass = "btn btn-danger btn-sm"
                };
                deleteButton.Click += DeleteRow_Click;
                deleteCell.Controls.Add(deleteButton);
                row.Cells.Add(deleteCell);

                TableClanovi.Rows.Add(row);
            }

            // DODAJTE OVU PROVERU - OVO JE KLJUČNO
            if (Clanovi.Count > 0)
            {
                int zbirGodina = Clanovi.Where(c => c.Godine.HasValue).Sum(c => c.Godine.Value);
                string kategorija = DropDownListKategorija.SelectedItem.Text;

                var (pocetniBodovi, zadatoVreme) = OdrediPocetneBodoveIVreme(zbirGodina);

                TableRow footerRow = new TableRow();

                // Zbir godina (prikazuje se za sve kategorije)
                footerRow.Cells.Add(new TableCell
                {
                    ColumnSpan = 3,
                    Text = $"<b>Укупан збир година такмичара: {zbirGodina}</b>",
                    CssClass = "fw-bold text-center"
                });

                // Početni bodovi (prikazuje se za sve kategorije)
                footerRow.Cells.Add(new TableCell
                {
                    Text = $"<b>Почетни бодови: {pocetniBodovi}</b>",
                    CssClass = "fw-bold"
                });

                // Zadato vreme (samo za juniore)
                if (kategorija.Contains("Јуниори"))
                {
                    footerRow.Cells.Add(new TableCell
                    {
                        Text = $"<b>Задато време: {zadatoVreme}</b>",
                        CssClass = "fw-bold"
                    });
                }
                else
                {
                    // Dodaj praznu ćeliju za poravnanje ako nije juniorska kategorija
                    footerRow.Cells.Add(new TableCell { Text = "" });
                }

                TableClanovi.Rows.Add(footerRow);
            }
        }
        private (int PocetniBodovi, string ZadatoVreme) OdrediPocetneBodoveIVreme(int zbirGodina)
        {
            string kategorija = DropDownListKategorija.SelectedItem.Text;
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
            // 4. Pravila za Seniore i Profesionalce B
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
                else if (zbirGodina >= 512) pocetniBodovi += 35;
            }

            return (pocetniBodovi, zadatoVreme);
        }

        protected void DeleteRow_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;

            int redniBroj;
            if (int.TryParse(deleteButton.CommandArgument, out redniBroj))
            {
                Clan clanZaBrisanje = Clanovi.FirstOrDefault(c => c.RedniBroj == redniBroj);
                if (clanZaBrisanje != null)
                {
                    Clanovi.Remove(clanZaBrisanje);

                    // Ponovo numeriši redne brojeve
                    for (int i = 0; i < Clanovi.Count; i++)
                    {
                        Clanovi[i].RedniBroj = i + 1;
                    }

                    AzurirajTabelu();

                    LabelMessage.Text = "Члан је успешно обрисан.";
                    LabelMessage.CssClass = "text-success";
                }
                else
                {
                    LabelMessage.Text = "Члан није пронађен.";
                    LabelMessage.CssClass = "text-danger";
                }
            }
            else
            {
                LabelMessage.Text = "Дошло је до грешке при брисању.";
                LabelMessage.CssClass = "text-danger";
            }
        }

        protected void ButtonUnosEkipe_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonUnosEkipe.Enabled = false;

                // Proverite da li ima članova
                if (Clanovi.Count == 0)
                {
                    LabelMessage.Text = "Молимо додајте бар једног члана екипе.";
                    LabelMessage.CssClass = "text-danger";
                    ButtonUnosEkipe.Enabled = true;
                    return;
                }

                // OBAVEZNO OSVEŽI TABELU PRE ČUVANJA DA BI SE TAČNO IZRAČUNALI BODOVI
                AzurirajTabelu();

                // Koristite connection string iz web.config
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // PROVERA JEDINSTVENOSTI NAZIVA EKIPE
                    string proveraQuery = "SELECT COUNT(*) FROM Ekipe WHERE NazivEkipe = @NazivEkipe";
                    using (SqlCommand proveraCmd = new SqlCommand(proveraQuery, conn))
                    {
                        proveraCmd.Parameters.AddWithValue("@NazivEkipe", TextBoxNazivEkipe.Text);
                        int brojPostojecih = (int)proveraCmd.ExecuteScalar();

                        if (brojPostojecih > 0)
                        {
                            LabelMessage.Text = "Екипа са овим називом већ постоји у бази!";
                            LabelMessage.CssClass = "text-danger";
                            ButtonUnosEkipe.Enabled = true;
                            return;
                        }
                    }

                    // Izračunaj početne bodove - PONOVO POZOVI METODU ZA TAČNE BODOVE
                    int zbirGodina = Clanovi.Where(c => c.Godine.HasValue).Sum(c => c.Godine.Value);
                    var (pocetniBodovi, zadatoVreme) = OdrediPocetneBodoveIVreme(zbirGodina);

                    // 1. Prvo unesi ekipu u tabelu "Ekipe" - ISPRAVLJEN UPIT SA DATUMUNOSA
                    string queryEkipa = @"INSERT INTO Ekipe (NazivEkipe, Kategorija, Trener, VodjaEkipe, PocetniBodovi, OVS, DatumUnosa) 
                VALUES (@NazivEkipe, @Kategorija, @Trener, @VodjaEkipe, @PocetniBodovi, @OVS, @DatumUnosa);
                SELECT SCOPE_IDENTITY();";

                    int ekipaID;
                    using (SqlCommand cmdEkipa = new SqlCommand(queryEkipa, conn))
                    {
                        cmdEkipa.Parameters.AddWithValue("@NazivEkipe", TextBoxNazivEkipe.Text);
                        cmdEkipa.Parameters.AddWithValue("@Kategorija", DropDownListKategorija.SelectedItem.Text);
                        cmdEkipa.Parameters.AddWithValue("@Trener", TextBoxTrener.Text);
                        cmdEkipa.Parameters.AddWithValue("@VodjaEkipe", TextBoxVodjaEkipe.Text);
                        cmdEkipa.Parameters.AddWithValue("@PocetniBodovi", pocetniBodovi);
                        cmdEkipa.Parameters.AddWithValue("@OVS", TextBoxOVS.Text);
                        cmdEkipa.Parameters.AddWithValue("@DatumUnosa", DateTime.ParseExact(TextBoxDatumUnosa.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture));

                        ekipaID = Convert.ToInt32(cmdEkipa.ExecuteScalar());
                    }

                    // 2. Zatim unesi sve članove u "Clanovi" sa referencom na EkipaID
                    foreach (var clan in Clanovi)
                    {
                        string queryClan = @"INSERT INTO Clanovi (EkipaID, RedniBroj, Uloga, ImePrezime, DatumRodjenja, Godine) 
                    VALUES (@EkipaID, @RedniBroj, @Uloga, @ImePrezime, @DatumRodjenja, @Godine)";

                        using (SqlCommand cmdClan = new SqlCommand(queryClan, conn))
                        {
                            cmdClan.Parameters.AddWithValue("@EkipaID", ekipaID);
                            cmdClan.Parameters.AddWithValue("@RedniBroj", clan.RedniBroj);
                            cmdClan.Parameters.AddWithValue("@Uloga", clan.Uloga);
                            cmdClan.Parameters.AddWithValue("@ImePrezime", clan.ImePrezime);

                            if (clan.DatumRodjenja.HasValue)
                            {
                                cmdClan.Parameters.AddWithValue("@DatumRodjenja", clan.DatumRodjenja.Value);
                                cmdClan.Parameters.AddWithValue("@Godine", clan.Godine.Value);
                            }
                            else
                            {
                                cmdClan.Parameters.AddWithValue("@DatumRodjenja", DBNull.Value);
                                cmdClan.Parameters.AddWithValue("@Godine", DBNull.Value);
                            }
                            cmdClan.ExecuteNonQuery();
                        }
                    }

                    // Nakon uspešnog čuvanja:
                    LabelMessage.Text = "Екипа успешно сачувана! Нови унос је спреман.";
                    LabelMessage.CssClass = "text-success";

                    // OČIŠĆAVANJE POLJA
                    OcistiFormu();
                }
            }
            catch (Exception ex)
            {
                LabelMessage.Text = "Грешка при чувању: " + ex.Message;
                LabelMessage.CssClass = "text-danger";
            }
            finally
            {
                ButtonUnosEkipe.Enabled = true;
            }
        }
        private void OcistiFormu()
        {
            // 1. Očistite osnovne podatke
            TextBoxNazivEkipe.Text = string.Empty;
            TextBoxOVS.Text = string.Empty;
            TextBoxTrener.Text = string.Empty;
            TextBoxVodjaEkipe.Text = string.Empty;
            DropDownListKategorija.SelectedIndex = 0;
            TextBoxDatumUnosa.Text = DateTime.Now.ToString("dd.MM.yyyy");

            // 2. Očistite listu članova
            Clanovi.Clear();
            ViewState["Clanovi"] = null;

            // 3. Očistite tabelu (samo zaglavlje će ostati)
            TableClanovi.Rows.Clear();
            InitializeClanoviTable();

            // 4. Očistite polja za unos članova
            DropDownListUloga.SelectedIndex = 0;
            TextBoxImePrezime.Text = string.Empty;
            TextBoxDatumRodjenja.Text = string.Empty;
        }
    }
}