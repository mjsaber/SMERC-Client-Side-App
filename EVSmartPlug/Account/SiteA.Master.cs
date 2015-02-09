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
    public partial class SiteA : System.Web.UI.MasterPage
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
                Image1.ImageUrl = "moevlogo.png";
            }
            else
            {
                lblCopyRight.Text = "© 2011-" + DateTime.Now.Year + ", SMERC";

            }            
        }
    }
}
