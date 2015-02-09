using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using System.Drawing;

namespace EVUser
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strOrganization = System.Web.Configuration.WebConfigurationManager.AppSettings["intOrganization"].ToString();
            if (strOrganization == "0")
            {
                if (DateTime.Now.Year == 2014)
                    lblCopyRight.Text = "© 2014, MOEV";
                else
                    lblCopyRight.Text = "© 2014-" + DateTime.Now.Year + ", MOEV";
                Image1.ImageUrl = "~/Images/moevlogo.png";
                Label1.Text = "ID: ";
                tcID.BackColor = ColorTranslator.FromHtml("#f59630");//245,150,48
                tcHome.BackColor = ColorTranslator.FromHtml("#7BA33C");
                tcLogout.BackColor = ColorTranslator.FromHtml("#c0c0c0");

                ibHome.ImageUrl = "~/Images/MImage/icon_header_home.png";
                ibLogout.ImageUrl = "~/Images/MImage/icon_header_logout.png";
            }
            else
            {
                lblCopyRight.Text = "© 2011-" + DateTime.Now.Year + ", SMERC";

            }            
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }
    }
}
