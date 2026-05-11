using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgramTakmicenja
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        private const string RoleSessionKey = "role";
        private const string FullNameSessionKey = "punoime";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session[RoleSessionKey] == null || string.IsNullOrEmpty(Session[RoleSessionKey].ToString()))
                {
                    SetVisibilityForUnauthenticatedUser();
                }
                else
                {
                    SetVisibilityForAuthenticatedUser();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                System.Diagnostics.Debug.WriteLine($"Exception in Page_Load: {ex.Message}");
            }

            if (!IsPostBack)
            {
                ShowCompetitionMenuIfNeeded();
            }
        }

        private void SetVisibilityForUnauthenticatedUser()
        {
            Sudije.Visible = false;
            Pozdrav.Visible = false;
            Administrator.Visible = true;
            AdminKorisnika.Visible = false;
            UnosEkipa.Visible = false;
            UnosSudija.Visible = false;
            Pravilnici.Visible = false;
            Takmicenja.Visible = false;
            EditEkipa.Visible = false;

        }

        private void SetVisibilityForAuthenticatedUser()
        {
            Izlaz.Visible = true;
            Sudije.Visible = false;
            Pozdrav.Visible = true;
            Administrator.Visible = false;
            UnosEkipa.Visible = true;
            UnosSudija.Visible = false;
            Pravilnici.Visible = true;
            Takmicenja.Visible = false;
            EditEkipa.Visible = false;


            string role = Session[RoleSessionKey].ToString();
            string fullName = Session[FullNameSessionKey]?.ToString();

            Pozdrav.Text = !string.IsNullOrEmpty(fullName) ? $"Добродошао админ. {fullName}" : "Добродошао админ.";

            Sudije.Visible = role.Equals("adminx") || role.Equals("admin");
            Administrator.Visible = role.Equals("adminx");
            UnosSudija.Visible = role.Equals("adminx") || role.Equals("admin");
            Takmicenja.Visible = role.Equals("adminx") || role.Equals("admin");
            EditEkipa.Visible = role.Equals("adminx") || role.Equals("admin");
        }

        private void ShowCompetitionMenuIfNeeded()
        {
            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (currentPage == "Juniori.aspx" ||
                currentPage == "Podmladak.aspx" ||
                currentPage == "Dobrovoljci.aspx" ||
                currentPage == "Profesionalci.aspx" ||
                currentPage == "RezultatiTakmicenja.aspx" ||
                currentPage == "UnosEkipa.aspx" ||
                currentPage == "UnosSudija.aspx" ||
                currentPage == "Pravilnici.aspx")
            {
                phTakmicenjaMeni.Visible = true;
            }
        }

        protected void Administrator_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/adminlogin.aspx");
        }

        protected void AdminKorisnika_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/adminkorisnika.aspx"));
        }

        protected void UnosSudija_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/unossudija.aspx"));
        }

        protected void Pravilnici_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/StraniceTakmicenja/Pravilnici/PravilniciBodovanje.aspx"));
        }

        protected void UnosEkipa_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/unosEkipa.aspx"));
        }

        protected void Sudije_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/profilsudija.aspx"));
        }

        protected void Pozdrav_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/korprofil.aspx");
        }

        protected void Izlaz_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect(ResolveUrl("~/default.aspx"));
        }
        protected void EditEkipa_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("EditEkipa_Click pozvan");

            Response.Redirect(ResolveUrl("~/EditEkipa.aspx"));
        }
        protected void RezultatiTakmicenja_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/StraniceTakmicenja/RezultatiTakmicenja.aspx"));
        }
    }
}
