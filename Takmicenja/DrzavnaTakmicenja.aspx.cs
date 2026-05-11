using System;
using System.Web.UI;

namespace ProgramTakmicenja
{
    public partial class DrzavnaTakmicenja : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["role"] != null)
                {
                    string userRole = Session["role"].ToString();

                    if (userRole == "admin" || userRole == "adminx")
                    {
                        pnlAdmin.Visible = true; // Proveri da li postoji u .aspx
                    }
                }
            }
        }
    }
}
