using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;

namespace EVUser
{
    public partial class BasePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            AutoRedirect();
        }

        public void AutoRedirect()
        {
            int int_MilliSecondsTimeOut = (this.Session.Timeout * 60000);
            string strPath = Page.ResolveUrl("~/Account/Login.aspx");
            string str_Script = @"<script type='text/javascript'> intervalset = window.setInterval('Logout()'," + int_MilliSecondsTimeOut.ToString() +
                                @");function Logout(){window.location.href='" + strPath + "'; }</script>";

            ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", str_Script);
        }
    }
}