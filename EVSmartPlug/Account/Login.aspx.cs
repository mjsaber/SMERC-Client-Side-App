using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;

using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

namespace EVUser.Account
{    
    public partial class Login : System.Web.UI.Page
    {
        public string strScreenWidth;
        public string strScreenHeight;

        private Control FindControlRecursive(Control rootControl, string controlID)
        {
            if (rootControl.ID == controlID) return rootControl;

            foreach (Control controlToSearch in rootControl.Controls)
            {
                Control controlToReturn =
                    FindControlRecursive(controlToSearch, controlID);
                if (controlToReturn != null) return controlToReturn;
            }
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            string strOrganization = System.Web.Configuration.WebConfigurationManager.AppSettings["intOrganization"].ToString();
            if (strOrganization == "0")
            {
                //HtmlElement imageTitle = webBrowser1.Document.GetElementById("imgTitle");
                //imgTitle = "~/Account/login_top_moev.png";
                //System.Web.UI.HtmlControls.HtmlImage imgTitle = (System.Web.UI.HtmlControls.HtmlImage)Page.Master.FindControl("imgTitle");
                //imgTitle.Attributes["src"] = "~/Account/login_top_moev.png";

                try
                {
                    Control ctlButton = FindControlRecursive((Control)Login1, "LoginButton");
                    ((Button)ctlButton).CssClass = "button_o";

                    Control imgTitle = FindControlRecursive((Control)Login1, "imgTitle");
                    ((Image)imgTitle).ImageUrl = "~/Account/login_top_moev.png";
                    
                }
                catch (Exception ex)
                {
                }
                
            }
            Cache.Remove("zigBeeNode");
        //    //strScreenWidth = this.Request["screenWidth"].ToString();
        //    //strScreenHeight = this.Request["screenHeight"].ToString();
        //    // Insure that the __doPostBack() JavaScript method is created...
        //    this.ClientScript.GetPostBackEventReference(this, string.Empty);


        //    if (this.IsPostBack)
        //    {
        //        string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
        //        string eventArgument = (this.Request["__EVENTARGUMENT"] == null) ? string.Empty : this.Request["__EVENTARGUMENT"];

        //        if (eventTarget == "GetScreenWidthPostBack")
        //        {
        //            string[] screenSizeArray = eventArgument.Split(',');
        //            double screenWidth = Convert.ToDouble(screenSizeArray[0]);
        //            double screenHeight = Convert.ToDouble(screenSizeArray[1]);
        //            double dblPixalRatio = 1.0;
        //            try
        //            {
        //                dblPixalRatio = Convert.ToUInt16(screenSizeArray[2]);
        //            }
        //            catch
        //            {                        
        //            }
        //            screenWidth *= dblPixalRatio;
        //            screenHeight *= dblPixalRatio;
        //            //Label1.Text=screenSizeArray[0]+"x"+screenSizeArray[1]+" pr:"+screenSizeArray[2];

        //            Login1.Width = Convert.ToUInt16(screenWidth);
        //            Login1.Height = Convert.ToUInt16(screenHeight);
        //        }
        //    }
        //    else
        //    {
        //        //string javaScript = "__doPostBack('GetScreenWidthPostBack', screen.width + ',' + screen.height);";
        //        string javaScript = "__doPostBack('GetScreenWidthPostBack', window.screen.width + ',' + window.screen.height+','+window.devicePixelRatio);";

        //        ClientScript.RegisterStartupScript(this.GetType(), "GetScreenWidthScript", javaScript, true);
        //    }

        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {            

            //try
            //{
            //    string strLoweredUserName = Login1.UserName.ToLower();

            //    string strQuery = "SELECT p.EVUserSessionTimeout " +
            //                      "FROM     aspnet_Users AS u INNER JOIN aspnet_Profile AS p ON u.UserId = p.UserId " +
            //                      "WHERE  u.LoweredUserName = @LoweredUserName";
            //    string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString; 
                
            //    SqlConnection cnn = new SqlConnection(strCnn);
            //    cnn.Open();
            //    SqlCommand cmd = new SqlCommand(strQuery, cnn);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.AddWithValue("@LoweredUserName", strLoweredUserName);
            //    int intSessionTimeout = Convert.ToInt32(cmd.ExecuteScalar());
            //    HttpCookie cookie = FormsAuthentication.GetAuthCookie(Login1.UserName, Login1.RememberMeSet);

            //    Session.Timeout = intSessionTimeout;
            //    cookie.Expires = DateTime.Now.AddMinutes(intSessionTimeout);
            //    Response.Cookies.Add(cookie);
            //}
            //catch(Exception ex)
            //{
            //    FormsAuthentication.SignOut(); // Sign out user
            //    Session.Abandon(); // Destroy all objects stored in Session object and release resources. 
            //    Session.Contents.Clear();
            //    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.Cache.SetNoStore();  
            //}
        }
    }
}