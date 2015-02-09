using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Collections;

namespace EVUser
{
    public partial class StationMap : BasePage//System.Web.UI.Page
    {

        private SqlDataReader dataReader;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strOrganization = System.Web.Configuration.WebConfigurationManager.AppSettings["intOrganization"].ToString();
            if (strOrganization == "0")
            {
                //Image1.Attributes["src"] = "~/images/MImage/icon_page_changepassword.png";
                Image1.ImageUrl = "~/images/MImage/icon_page_moas.png";
                //btnGetRecords.CssClass = "button";
                //btnGetRecordsAndEmail.CssClass = "button";
                gvParkingLotList.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f59630");
            }
            /******************************************************* Begin of EVUser Account Type ***********************************************/
            if (User != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    string strUserID = Membership.GetUser().ProviderUserKey.ToString();
                    string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    SqlConnection cnn = new SqlConnection(strCnn);
                    int intEVUserAccountTypeID;
                    int intEVUserExpirationWindow;
                    DateTime dtEVUserAccountStartDate;

                    try
                    {
                        //updated on 3/14/2014 for different kinds of EVUserAccountType
                        cnn.Open();
                        string strQuery = @"SELECT p.EVUserAccountTypeID, p.EVUserAccountStartDate, w.ExpirationWindow " +
                                           "FROM aspnet_Profile AS p INNER JOIN EVUserAccountExpirationWindow AS w ON p.EVUserAccountExpirationWindowID = w.ID " +
                                           "WHERE p.UserId = @UserID";
                        SqlCommand cmd = new SqlCommand(strQuery, cnn);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier));
                        cmd.Parameters["@UserID"].Value = new Guid(Membership.GetUser().ProviderUserKey.ToString());
                        dataReader = cmd.ExecuteReader();
                        if (dataReader.Read())
                        {
                            intEVUserAccountTypeID = Convert.ToInt32(dataReader["EVUserAccountTypeID"]);
                            intEVUserExpirationWindow = Convert.ToInt32(dataReader["ExpirationWindow"]);
                            dtEVUserAccountStartDate = Convert.ToDateTime(dataReader["EVUserAccountStartDate"]);
                            switch (intEVUserAccountTypeID)
                            {
                                case 0: //Basic Account
                                case 1: //Plus Status
                                    //case 2: //Plus Map
                                    //case 3: //Plus Charging Record
                                    //case 4: //Plus Social Media
                                    Response.Redirect("~/Error.aspx?ErrMsg=Privilege", false);
                                    break;
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Error.aspx?ErrMsg=Privilege", false);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            /******************************************************* End of EVUser Account Type ***********************************************/

            ////Add on 4/26/2012 for LADWP demo
            //if (User != null)
            //{
            //    if (User.Identity.IsAuthenticated)
            //    {
            //        //RolePrincipal rp = (RolePrincipal)User;
            //        string[] roles = Roles.GetRolesForUser();
            //        if (roles[0] == "UCLA Operator")
            //            Response.Redirect("~/Error.aspx?ErrMsg=Privilege", false);
            //    }
            //}


            // find default city
            if (!this.IsPostBack)
            {
                //added on 12/16/2013 to limit LADWP account
                //if (User != null)
                //{
                //    if (User.Identity.IsAuthenticated)
                //    {
                //        //RolePrincipal rp = (RolePrincipal)User;
                //        string[] roles = Roles.GetRolesForUser();
                //        if (roles[0] == "UCLA - Los Angeles Maintainer" || roles[0] == "Pasadena Operator")
                //            Response.Redirect("~/Error.aspx?ErrMsg=Privilege", false);

                //        for (int i = 0; i < roles.Length; i++)
                //        {
                //            if (roles[i] == "LADWP - Los Angeles User")
                //            {
                //                Response.Redirect("~/Error.aspx?ErrMsg=Privilege", false);
                //            }
                //        }
                //    }
                //}

                RetrieveCity();
                string cityId = retrieveDefaultCity();
                if (cityId != null)
                {
                    for (int i = 0; i < ddlCity.Items.Count; i++)
                    {
                        if (ddlCity.Items[i].Value == cityId)
                        {
                            ddlCity.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            RetrieveParkingLot();
        }

        private void RetrieveCity()
        {
            string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnection cnn = new SqlConnection(strCnn);
            string strQuery;
            SqlCommand cmd;
            SqlDataReader sdr = null;
            DataTable dt = null;
            SqlDataAdapter da;
            Object obj;

            try
            {
                cnn.Open();

                if (!this.IsPostBack)
                {
                    //Begin of retrieve user's role info for the city
                    MembershipUser myObject = Membership.GetUser();
                    string strUserID = myObject.ProviderUserKey.ToString();
                    RolePrincipal rp = (RolePrincipal)User;
                    string[] roles = rp.GetRoles();
                    char[] chrDelimiters = { ' ' };
                    ArrayList al = new ArrayList();
                    int i = 0;
                    foreach (string strSub in roles[0].Split(chrDelimiters))
                    {
                        al.Add(strSub);
                        i++;
                    }

                    strQuery = "";

                    if (al[0].ToString() == "General")
                    {
                        // A general role
                        strQuery = "SELECT ID, Name+', '+State AS CityState FROM City WHERE Activate = 1 ORDER BY CityState";
                    }
                    else
                    {
                        //Not general role
                        strQuery = "SELECT r.RoleName, r.RoleId " +
                                   "FROM  aspnet_Roles AS r INNER JOIN aspnet_UsersInRoles AS ur ON r.RoleId = ur.RoleId " +
                                   "WHERE ur.UserId = @UserID";
                        cnn = new SqlConnection(strCnn);
                        cnn.Open();

                        cmd = new SqlCommand(strQuery, cnn);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserID", strUserID);
                        sdr = cmd.ExecuteReader();
                        strQuery = null;
                        while (sdr.Read())
                        {
                            i = 0;
                            al.Clear();
                            foreach (string strSub in sdr["RoleName"].ToString().Split(chrDelimiters))
                            {
                                al.Add(strSub);
                                i++;
                            }
                            switch (al[al.Count - 1].ToString())
                            {
                                case "Administrator":
                                    if (strQuery == null)
                                        strQuery = "[Administrator Role ID]='" + sdr["RoleID"] + "'";
                                    else
                                        strQuery += " OR [Administrator Role ID]='" + sdr["RoleID"] + "'";
                                    break;
                                case "Maintainer":
                                    if (strQuery == null)
                                        strQuery = "[Maintainer Role ID]='" + sdr["RoleID"] + "'";
                                    else
                                        strQuery += " OR [Maintainer Role ID]='" + sdr["RoleID"] + "'";
                                    break;
                                case "Operator":
                                    if (strQuery == null)
                                        strQuery = "[Operator Role ID]='" + sdr["RoleID"] + "'";
                                    else
                                        strQuery += " OR [Operator Role ID]='" + sdr["RoleID"] + "'";
                                    break;
                                case "User":
                                    if (strQuery == null)
                                        strQuery = "[User Role ID]='" + sdr["RoleID"] + "'";
                                    else
                                        strQuery += " OR [User Role ID]='" + sdr["RoleID"] + "'";
                                    break;

                            }
                        }
                        if (strQuery != null)
                            strQuery = "SELECT ID, Name+', '+State AS CityState FROM City WHERE (" + strQuery + ") and Activate = 1 ORDER BY CityState";

                        if (sdr != null)
                            sdr.Close();
                    }
                    //End of retrieve user's role info for the city

/*******************************************************************************************************/
                    //strQuery = @"SELECT ID, Name+','+State AS CityState FROM City ORDER BY CityState";
                    cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt = new DataTable();
                    da.Fill(dt);
                    ddlCity.DataSource = dt;

                    ddlCity.DataTextField = "CityState";
                    ddlCity.DataValueField = "ID";
                    ddlCity.DataBind();
                    ddlCity.SelectedValue = ddlCity.Items[0].Value;
                    cmd.Dispose();
                    da.Dispose();
                }
            }
            catch(Exception ex)
            {
                if (sdr != null)
                    sdr.Close();
                cnn.Close();
            }
        }

        private void RetrieveParkingLot()
        {
            string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnection cnn = new SqlConnection(strCnn);
            string strQuery;
            SqlCommand cmd;
            SqlDataReader sdr = null;
            DataTable dt = null;
            SqlDataAdapter da;
            Object obj;

            try
            {
                cnn.Open();

                strQuery = @"SELECT pl.ID, pl.Name, pl.Address+'<br />'+ c.Name+', '+ c.State+ pl.[Zip Code] AS Address, CAST (0 AS SMALLINT) AS Total, CAST (0 AS SMALLINT) AS Available, pl.Latitude, pl.Longitude, pl.ChargingBoxLocationDirection "+
                            "FROM [Parking Lot] AS pl INNER JOIN City AS c ON pl.[City ID] = c.ID "+
                            "WHERE c.ID =@CityID "+
                            "ORDER BY pl.Name";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CityID", ddlCity.SelectedValue);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    cmd.Dispose();
                    strQuery = "SELECT COUNT(Station.ID) AS StationNo " +
                               "FROM   [Parking Lot] AS pl INNER JOIN "+
                                  "Gateway ON pl.ID = Gateway.[Parking Lot ID] INNER JOIN "+
                                  "Station ON Gateway.ID = Station.[Gateway ID] "+
                               "WHERE  pl.ID = @ParkingLotID and Station.Enable=1 and Gateway.Enable=1";
                    cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ParkingLotID", dr["ID"]);
                    obj = cmd.ExecuteScalar();
                    dr["Total"] = obj;

                    cmd.Dispose();
                    strQuery = "SELECT COUNT(Station.ID) AS UsedCount " +
                               "FROM   [Parking Lot] AS pl INNER JOIN " +
                                  "Gateway ON pl.ID = Gateway.[Parking Lot ID] INNER JOIN " +
                                  "Station ON Gateway.ID = Station.[Gateway ID] INNER JOIN " +
                                  "ChargingRecords AS cr ON Station.ID = cr.StationID " +
                               "WHERE pl.ID = @ParkingLotID AND cr.IsEnd = 0 and Gateway.Enable=1";
                    cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ParkingLotID", dr["ID"]);
                    obj = cmd.ExecuteScalar();
                    dr["Available"] =Convert.ToInt16(dr["Total"])- Convert.ToInt16( obj);
                    cmd.Dispose();
                }
                gvParkingLotList.DataSource = dt;
                gvParkingLotList.DataBind();

                int dt_i = 0;
                foreach (GridViewRow row in gvParkingLotList.Rows)
                {
                    string id = dt.Rows[dt_i]["ID"].ToString();
                    row.Attributes.Add("onclick", "clickMarker(\"" + id + "\")");
                    dt_i++;
                }

                //gmParkLot.Markers.Clear();
                //gmParkLot.MapInfo.Zoom = 15;
                //gmParkLot.MapInfo.MapType = MapTypes.ROADMAP;
                //gmParkLot.Width = 920;
                //gmParkLot.Height = 920;
                if (dt.Rows.Count == 0)
                {
                    lblRetrieveTime.Text = "No available station found at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    cmd.Dispose();
                    strQuery = "SELECT Latitude,Longitude FROM City WHERE ID=@CityID";
                    cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CityID", ddlCity.SelectedValue);
                    sdr = cmd.ExecuteReader();
                    //if (sdr.Read())
                    //{

                    //    gmParkLot.MapInfo.Latitude = double.Parse(sdr["Latitude"].ToString());//34.068241;
                    //    gmParkLot.MapInfo.Longtitude = double.Parse(sdr[1].ToString()); //-118.44450;
                    //}
                }
                else
                {
                    lblRetrieveTime.Text = "*Retrieve time: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    //Google map info
                    //gmParkLot.MapInfo.Latitude = double.Parse(dt.Rows[0]["Latitude"].ToString());//34.068241;
                    //gmParkLot.MapInfo.Longtitude = double.Parse(dt.Rows[0]["Longitude"].ToString()); //-118.44450;

                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    ////Add google map mark
                    //    Marker marker = new Marker(dr["ID"].ToString(), double.Parse(dr["Latitude"].ToString()), double.Parse(dr["Longitude"].ToString()));
                    //    marker.Draggable = false;
                    //    marker.InfoWindowOnClick = true;
                    //    string strDirection = "No direction";
                    //    if (!dr.IsNull("ChargingBoxLocationDirection"))
                    //        strDirection = dr["ChargingBoxLocationDirection"].ToString();
                    //    //    strDirection = dr["ChargingBoxLocationDirection"].ToString().Replace("\r\n", "<br />");
                    //    marker.InfoWindowContentHtml = "Directions to " + dr["Name"] + "<br/>" +
                    //        "<image id=\"" + dr["ID"].ToString() + "\" src=\"ImgHandler.ashx?plid=" + dr["ID"].ToString() + "\" width = \"100%\" /> <br /> " +
                    //        "<div id = \"" + dr["ID"].ToString() + "infow\" align = \"left\" style = \"position:relative; font-size:20px; overflow:hidden; \">" + strDirection + "</div>";
                    //        //"</div>";
                    //    marker.Tooltip = dr["Name"].ToString();
                    //    marker.ImgSrc = "marker_64x96.png";
                    //    //marker.ImgH = 50;
                    //    //marker.ImgW = 50;
                    //    gmParkLot.Markers.Add(marker);
                    //}
                }//End of if..else

            }
            catch(Exception ex)
            {
                if (sdr != null)
                    sdr.Close();
                cnn.Close();
            }
        }

        protected void gvParkingLotList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[2].Text);
                e.Row.Cells[2].Text = decodedText;
                //e.Row.Attributes.Add("onclick", "javasctript:clickMarker(")
            }
        }

        protected void storeDefaultCity()
        {
            string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(strCnn))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "update aspnet_Profile " +
                                        "set LastestMapInStationCityID = \'" + ddlCity.SelectedValue + "\' " +
                                        "where UserId = \'" + ((Guid)Membership.GetUser().ProviderUserKey).ToString()+ "\' ";
                    cmd.Connection.Open();
                    int rs = cmd.ExecuteNonQuery(); 
                }
            }
        }

        protected string retrieveDefaultCity()
        {
            string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            DataTable dt = new DataTable();
            string strQuery = "Select LastestMapInStationCityID FROM aspnet_Profile WHERE UserId = \'" + 
                                ((Guid)Membership.GetUser().ProviderUserKey).ToString()  + "\'";
            SqlDataAdapter sdr = new SqlDataAdapter(strQuery, strCnn);
            sdr.Fill(dt);
            if (dt.Rows.Count == 0)
                return null;
            if (dt.Rows[0].IsNull("LastestMapInStationCityID"))
                return null;
            return ((Guid)dt.Rows[0][0]).ToString();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RetrieveParkingLot();
            storeDefaultCity();
            //ddlCity.SelectedValue
        }
    }
}