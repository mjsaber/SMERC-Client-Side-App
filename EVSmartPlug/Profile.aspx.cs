using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;

namespace EVUser.Account
{
    public partial class Profile : System.Web.UI.Page
    {
        // connectionString is the string to connect to the SQL database.
        string connectionString = WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        readonly string[] EvListColumnsToHide = { "EvModelID", "ID" };

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string strOrganization = System.Web.Configuration.WebConfigurationManager.AppSettings["intOrganization"].ToString();
                if (strOrganization == "0")
                {
                    //Image1.Attributes["src"] = "~/images/MImage/icon_page_changepassword.png";
                    Image1.ImageUrl = "~/images/MImage/icon_page_profile.png";
                    btnUpdateProfile.CssClass = "button_o";
                    btnCancel.CssClass = "button_o";
                }
                PopulateAll();
            }
        }


        
        #region Misc Helper Functions.  ObtainUserCityFromGuid, ReturnUniqueCombo

        protected List<string> ReturnRoleNameAppendString(string userGUID)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            List<string> returnlist = new List<string>();
            try
            {
                cnn.Open();
                List<string> RoleIDs = new List<string>();
                strQuery = "SELECT [RoleID] FROM [aspnet_UsersInRoles] WHERE [UserID] ='" + userGUID + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                int count = dt.Rows.Count;

                da.Dispose();
                cmd.Dispose();

                try
                {
                    string ReturnString = string.Empty;

                    for (int i = 0; i < count; i++)
                    {
                        RoleIDs.Add(dt.Rows[i][0].ToString());
                    }

                    for (int i = 0; i < count; i++)
                    {
                        strQuery = "SELECT [RoleName] FROM [aspnet_Roles] WHERE [RoleID] ='" + RoleIDs[i] + "'";
                        cmd = new SqlCommand(strQuery, cnn);
                        cmd.CommandType = CommandType.Text;
                        da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        dt = new DataTable();
                        da.Fill(dt);
                        
                        returnlist.Add(dt.Rows[0][0].ToString());
                    }


                    da.Dispose();
                    cmd.Dispose();

                    cnn.Close();
                    return returnlist;
                }
                catch
                {
                    cnn.Close();
                    return null;
                }

            }
            catch (Exception ex)
            {
                cnn.Close();
                return null;
            }
        }

        protected string ObtainUserCityFromGUID(string CityGUID)
        {
            if (CityGUID == "-1")
            {
                return string.Empty;
            }
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            try
            {
                cnn.Open();
                strQuery = "SELECT Name FROM [City] WHERE [ID] = '" + CityGUID + "'";
                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                string UserCityName = dt.Rows[0][0].ToString();
                cnn.Close();
                return UserCityName;

            }
            catch
            {
                List<ComboCityAndGuidClass> ComboCityAndGuid = ReturnUniqueCombinedGUID();
                for (int i = 0; i < ComboCityAndGuid.Count; i++)
                {
                    if (ComboCityAndGuid[i].Guid == CityGUID)
                    {
                        cnn.Close();
                        return ComboCityAndGuid[i].ComboCityString;
                    }
                }
                cnn.Close();
                return null;
            }
        }

        protected List<ComboCityAndGuidClass> ReturnUniqueCombinedGUID()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;
            DataTable dt2 = null;
            DataTable dt3 = null;

            try
            {
                cnn.Open(); // Open the Connection

                /// The code below stores the CityID and the city Name into a table.  This table will be accessed 
                /// later when connecting the relationship between the IDs and names in the ComboCitiesList
                /// 
                strQuery = "SELECT [ID], [Name] FROM [City]";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt2 = new DataTable();
                da.Fill(dt2);
                da.Dispose();
                cmd.Dispose();

                // This ditionary, CityIdNameRelation, stores the ID and name of each city in the City Database
                Dictionary<string, string> CityIdNameRelation = new Dictionary<string, string>();

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    CityIdNameRelation.Add(dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString());
                }

                strQuery = "SELECT * From CombinatedCity";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt); // Fill a datagrid with Data we can retrive them later
                int iterator = 0; // iterator is used to keep track of which index of the list differs from the previous index


                List<string> uniqueGUIDs = new List<string>(); // Create a list of string that the unique GUIDs will go into
                List<string> ActivatedCheck = new List<string>(); // Create a list of string that we can check if the city is activated

                if ((dt.Rows.Count == 0)) // Make sure that the Table is populated
                {
                    return null;
                }
                uniqueGUIDs.Add(dt.Rows[0][0].ToString()); // Add first GUID. Each from here will be the same or unique
                ActivatedCheck.Add(dt.Rows[0][3].ToString());
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (0 != String.Compare(uniqueGUIDs[iterator].ToString(), dt.Rows[i][0].ToString()))
                    {
                        uniqueGUIDs.Add(dt.Rows[i][0].ToString());
                        ActivatedCheck.Add(dt.Rows[i][3].ToString());
                        iterator++; // increment the iterator only if two adjacent index match.
                    }
                }

                List<int> LengthsOfMainCities = new List<int>(); // Create list to store the lengths of each combo city

                // List<ComboCityAndGuidClass> ComboCityWithGuid = new List<ComboCityAndGuidClass>();

                int adder = 0;

                for (int i = 0; i < uniqueGUIDs.Count; i++)
                {
                    strQuery = "SELECT COUNT(*) FROM [CombinatedCity] WHERE ID='" + dt.Rows[adder][0] + "'";

                    cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt3 = new DataTable();
                    da.Fill(dt3);
                    LengthsOfMainCities.Add(Convert.ToInt32(dt3.Rows[0][0].ToString()));
                    adder += LengthsOfMainCities[i];
                }
                da.Dispose();
                cmd.Dispose();

                List<string> ComboCitiesList = new List<string>();   // This list will contain all of the Main City + combo cities.  
                //                                                   //  The main city will always be the 0 index. i.e. [0]

                iterator = 0;
                bool MainCityRead = false;

                for (int j = 0; j < dt.Rows.Count; j++) // This for loop creates the ComboCitiesList variable which stores all of the combocity/main city IDs, we will need to retrieve the actual names later
                {
                    if (0 == String.Compare(dt.Rows[j][0].ToString(), uniqueGUIDs[iterator]) && !MainCityRead)
                    {
                        MainCityRead = true;
                        ComboCitiesList.Add(dt.Rows[j][2].ToString());
                    }
                    if (0 == String.Compare(dt.Rows[j][0].ToString(), uniqueGUIDs[iterator]))
                    {
                        ComboCitiesList.Add(dt.Rows[j][1].ToString());
                        if (j < dt.Rows.Count - 1 && 0 != (String.Compare(dt.Rows[j + 1][0].ToString(), uniqueGUIDs[iterator])))
                        {
                            iterator++;
                            MainCityRead = false;
                        }
                    }
                }
                List<ComboCityAndGuidClass> ComboCityWithGuid = new List<ComboCityAndGuidClass>();
                List<string> ComboCitiesWithCityNames = new List<string>(); // Create a list of all the Actual City names

                for (int i = 0; i < ComboCitiesList.Count; i++)
                {
                    ComboCitiesWithCityNames.Add(CityIdNameRelation[ComboCitiesList[i]]);
                }

                List<List<string>> CityList = new List<List<string>>(); // 2D nest Store all City values
                List<string> ReturnCityList = new List<string>(); // The second 2D nest to return.
                iterator = 0;
                for (int i = 0; i < LengthsOfMainCities.Count; i++)
                {
                    // Populate the sublist with the main city and sub city combos
                    List<string> sublist = new List<string>(); // Create a sublist to enter in the main-subcity data

                    for (int v = iterator; v <= iterator + LengthsOfMainCities[i]; v++)
                    {
                        sublist.Add(ComboCitiesWithCityNames[v]);
                    }
                    //
                    // Add the sublist to the top-level List reference.
                    //
                    CityList.Add(sublist);
                    iterator += LengthsOfMainCities[i] + 1;
                }

                string subcities;

                for (int i = 0; i < LengthsOfMainCities.Count; i++)
                {
                    subcities = string.Empty;
                    for (int v = 0; v <= LengthsOfMainCities[i]; v++)
                    {
                        subcities += CityList[i][v];
                        if (v != LengthsOfMainCities[i])
                            subcities += " - ";
                    }
                    //ReturnCityList.Add(subcities); // Add the maincity - subcity combo to this list<string>
                    ComboCityWithGuid.Add(new ComboCityAndGuidClass(subcities, uniqueGUIDs[i]));
                }

                da.Dispose();
                cmd.Dispose();
                cnn.Close();
                return ComboCityWithGuid;

            }
            catch
            {
                cnn.Close();
                return null;
            }
        }

        public class ComboCityAndGuidClass // Class used to obtain and maintain the 
        {
            public ComboCityAndGuidClass(string ComboCityString, string Guid)
            {
                _ComboCityString = ComboCityString;
                _Guid = Guid;
            }

            private string _Guid;

            public string Guid
            {
                get { return _Guid; }
                set { _Guid = value; }
            }

            private string _ComboCityString;

            public string ComboCityString
            {
                get { return _ComboCityString; }
                set { _ComboCityString = value; }
            }
        }

        #endregion
        #region Populate Functions.  (ddlState, EVModel, etc)

        // Populate all ddls 
        protected void PopulateAll()
        {
            Populate_ddlPhoneServiceCarrier();
            Populate_ddlSmartPhoneOS();
            PopulateEVDDL();
            PopulateddlState();
            UpdateProfileWithData(strReturnUserGUIDfromUsername(User.Identity.Name));
            // Check web.config to see current settings on whether or not to show the social media info
            showOrHide(bool.Parse(WebConfigurationManager.AppSettings["blnShowSocialMediaOnProfile"]));
            
            PopulateUserInfo();
            PopulateddlGvEvListModel();
            PopulateEvList(strReturnUserGUIDfromUsername(User.Identity.Name));
        }

        protected void showOrHide(bool blnShow)
        {
            lblSocialMediaID.Visible = blnShow;
            lblSocialMediaAccountName.Visible = blnShow;
            if (blnShow)
            {
                Populate_ddlSocialMediaID();
            }
        }

        // Populate All Textboxes
        protected void PopulateUserInfo()
        {
            // If authenticated, At this point, the user should be authenticated,
            // otherwise there is a large error in user validation
            if (User.Identity.IsAuthenticated)
            {
                // Get user information
                MembershipUser memUser = Membership.GetUser();

                // Fill in all information
                fillInTb(memUser);
            }            
        }

        // Fill out user information given the user info
        protected void fillInTb(MembershipUser memUser)
        {

            string strUserName = memUser.UserName;
            
            // Obtain UserId
            string strUserID = strReturnUserGUIDfromUsername(strUserName);

            tbUsername.Text = strUserName;
            tbPasswordQuestion.Text = memUser.PasswordQuestion;
            tbEmail.Text = memUser.Email;

            // Retrieve Profile Information
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            try
            {
                cnn.Open();
                strQuery = " SELECT [FirstName], [LastName], [Address1], [Address2], [City], [State], [ZipCode], " +
                           " [PhoneNo], [EV ID], [SmartPhoneOS], [PhoneServiceCarrier], [SmartPhoneModelNo], " +
                           " [SocialMediaID], [SocialMediaAccountName], [SocialMediaAccountPassword], [MaximumVehicles] " +
                           " FROM [aspnet_Profile] WHERE [UserId] ='" + strUserID + "'";

                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    tbFirstName.Text = reader["FirstName"].ToString();
                    tbLastName.Text = reader["LastName"].ToString();
                    tbPhoneNumber.Text = reader["PhoneNo"].ToString();
                    tbAddress1.Text = reader["Address1"].ToString();
                    tbAddress2.Text = reader["Address2"].ToString();
                    tbZipCode.Text = reader["ZipCode"].ToString();
                    tbCity.Text = reader["City"].ToString();
                    ddlState.SelectedValue = reader["State"].ToString();
               

                    // Get EV Name from the "EV ID"
                    ddlEVModel.SelectedValue = ObtainEVnameFromGUID(reader["EV ID"].ToString());

                    ddlSmartPhoneOS.SelectedIndex = int.Parse(reader["SmartPhoneOS"].ToString());
                    ddlPhoneServiceCarrier.SelectedIndex = int.Parse(reader["PhoneServiceCarrier"].ToString());
                    tbSmartPhoneModel.Text = reader["SmartPhoneModelNo"].ToString();
                    ddlSocialMediaID.SelectedValue = reader["SocialMediaID"].ToString();
                    tbSocialMediaAccName.Text = reader["SocialMediaAccountName"].ToString();
                    tbMaximumVehicles.Text = reader["MaximumVehicles"].ToString();

                    try
                    {
                        string strPassword = reader["SocialMediaAccountPassword"].ToString();

                        if (!string.IsNullOrEmpty(strPassword))
                        {
                            using (RijndaelManaged myR = new RijndaelManaged())
                            {
                                byte[] byteRijKey = Convert.FromBase64String(WebConfigurationManager.AppSettings["RijKey"]);
                                byte[] byteRijIV = Convert.FromBase64String(WebConfigurationManager.AppSettings["RijIV"]);
                                byte[] bytePassword = Convert.FromBase64String(strPassword);
                                //byte[] byteEncryptText = EncryptStringToBytes(strPasswordText, byteRijKey, byteRijIV);

                                // strEncodedPassword = byteEncryptText.ToString();

                                string strEncodedPassword = DecryptStringFromBytes(bytePassword, byteRijKey, byteRijIV);

                                // strEncodedPassword is the actual password!  it will be hidden.
                                // If the password needs to be accessed, then simply use the string,
                                // strEncodedPassword to accesss the users password.
                            }
                        }
                        else
                        {
                           // tbSocialMediaAccPassword.Text = string.Empty;
                        }
                    }
                    catch
                    {
                    }
                    reader.Close();
                    cmd.Dispose();

                }
            }
            catch (Exception ex)
            {
                showMessage("Error at fillInTb: " + ex.Message);
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }                        
        }

        // Return EV Name from EV GUID
        protected string ObtainEVnameFromGUID(string EVGUID)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            try
            {
                cnn.Open();
                strQuery = "SELECT Manufacturer, Model FROM [EV Model] WHERE [Id] = '" + EVGUID + "'";
                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                string combinatedEVname = dt.Rows[0][0].ToString() + " " + dt.Rows[0][1].ToString();
                da.Dispose();
                cmd.Dispose();
                cnn.Close();
                return combinatedEVname;
            }
            catch (Exception ex)
            {
                cnn.Close();
                return null;
            }
        }


        // Return the User's GUID from the Username
        protected string strReturnUserGUIDfromUsername(string strUsername)
        {
            string strUserGUID = string.Empty;

            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;

            try
            {
                cnn.Open();
                strQuery = "SELECT [UserID] FROM [aspnet_Users] WHERE [UserName] ='" + strUsername + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    strUserGUID = reader["UserID"].ToString();
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                lblError.Text += "<br> Error at strReturnUserGUIDfromUsername: " + ex.Message;

            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
            return strUserGUID;
        }

        // Populate ddlSocialMediaID
        protected void Populate_ddlSocialMediaID()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            try
            {
                cnn.Open();
                strQuery = "SELECT [SocialMediaID], [SocialMediaName] FROM [SocialMedia]";

                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                ddlSocialMediaID.DataSource = dt;
                ddlSocialMediaID.DataValueField = "SocialMediaID";
                ddlSocialMediaID.DataTextField = "SocialMediaName";
                ddlSocialMediaID.DataBind();

                ListItem li = new ListItem("Select...", "-1");
                ddlSocialMediaID.Items.Insert(0, li);

                da.Dispose();
                cmd.Dispose();
                dt.Dispose();

            }
            catch (Exception ex)
            {
                showMessage("Error at Populate_ddlSocialMediaID: " + ex.Message);                
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }

            // Temporary, default is Twitter
            ddlSocialMediaID.SelectedIndex = 1;

        }

        // Populate ddlPhoneService
        protected void Populate_ddlPhoneServiceCarrier() 
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            try
            {
                cnn.Open();
                strQuery = "SELECT [ProviderName] FROM [WirelessServiceCarrier] ORDER BY ProviderName";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                ddlPhoneServiceCarrier.DataSource = dt;
                ddlPhoneServiceCarrier.DataValueField = "ProviderName";
                ddlPhoneServiceCarrier.DataTextField = "ProviderName";
                ddlPhoneServiceCarrier.DataBind();

                da.Dispose();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                lblError.Text += "<br>Error at ddlPhoneServiceCarrier: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
        }

        // Populate the ddlSmartPhoneOS 
        protected void Populate_ddlSmartPhoneOS()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            try
            {
                cnn.Open();
                strQuery = "SELECT [OS Name] FROM [SmartPhoneOS] ORDER BY [OS Name]";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                ddlSmartPhoneOS.DataSource = dt;
                ddlSmartPhoneOS.DataValueField = "OS Name";
                ddlSmartPhoneOS.DataTextField = "OS Name";
                ddlSmartPhoneOS.DataBind();

                cmd.Dispose();
                da.Dispose();
            }
            catch (Exception ex)
            {
                lblError.Text += "<br>Error at ddlSmartPhoneOS: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
        }

        // Populate the EVModel ddl.
        protected void PopulateEVDDL()
        {
            List<string> EVLIST = ListreturnListofEvCars();
            ddlEVModel.DataSource = EVLIST;
            ddlEVModel.DataBind();
            ListItem li = new ListItem("Select...", "-1");
            ddlEVModel.Items.Insert(0, li);
        }

        // Update Profile Page of User With Charging Data
        protected void UpdateProfileWithData(string strUserID)
        {
            float fltEnergyPrice = float.Parse(WebConfigurationManager.AppSettings["EnergyPrice"].ToString());
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;

            float fltCO2 = 0;
            float fltEnergyConsumed = 0;
            float fltChargingCost = 0;

            float fltNewEnergyConsumed = GetLatestEnergyConsumed(strUserID);
            
            // Get latest profile data and set it to the variables above
            getCurrentProfileData(strUserID, out fltCO2, out fltEnergyConsumed, out fltChargingCost);

            float fltNewTotalEnergyConsumed = fltEnergyConsumed + fltNewEnergyConsumed;
            float fltNewCO2 = fltCO2 + fltNewEnergyConsumed * 1.9f;
            float fltNewChargingCost = fltChargingCost + fltNewEnergyConsumed * fltEnergyPrice/100;

            

            try
            {
                strQuery = " UPDATE [aspnet_Profile] SET [CO2Accumulation] = '" + fltNewCO2 + "', [TotalEnergyConsumed] = '" + fltNewTotalEnergyConsumed + "'" +
                           ", [TotalChargingCost] = '" + fltNewChargingCost + "'" +
                           " WHERE [UserID] ='" + strUserID + "'";
                cnn.Open();

                cmd = new SqlCommand(strQuery, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                lblTest.Text += "<br>UpdateCalculateCO2to1 Error: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();
                UpdateCalculateCO2to1(strUserID);
            }

            // Round
            fltNewChargingCost = (float)Math.Round(fltNewChargingCost, 2);
            fltNewCO2 = (float)Math.Round(fltNewCO2, 4);
            fltNewTotalEnergyConsumed = (float)Math.Round(fltNewTotalEnergyConsumed, 4);

            // Place variables to corresponding labels
            lblCO2Reduced.Text = fltNewCO2.ToString() + " lbs";
            lblEnergyConsumed.Text = fltNewTotalEnergyConsumed.ToString() + " kWh";
            
            
            //lblChargingCost.Text = "$" + fltNewChargingCost.ToString();

            lblAccountInfoSOCAvailable.Text = blnUserHasStateOfCharge(strUserID) ? "Yes" : "No";

            List<string> RoleNameList = ReturnRoleNameAppendString(strUserID);
            int count = RoleNameList.Count;

            for (int i = 0; i < count; i++)
            {
                lblAccountInfoRole.Text += RoleNameList[i];
                if (i < count - 1)
                {
                    lblAccountInfoRole.Text += "<br>";
                }
            }        
        }

        // Find User's role area.

        //Check if User has state of charge available
        protected bool blnUserHasStateOfCharge(string strUserID)
        {
            bool blnHasSOC = false;

            string strSMERCID = string.Empty;

            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;

            try
            {
                cnn.Open();
                strQuery = " SELECT [SMERCID] " +
                           " FROM  [aspnet_Profile] " +
                           " WHERE [UserID] = '" + strUserID + "'";

                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    strSMERCID = reader["SMERCID"].ToString().Trim();
                }

                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                lblTest.Text += "<br>Error at blnUserHasStateOfCharge: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();
            }

            // Check if SmercID is the same as the username, and if not, check if it is a HEX
            if (strSMERCID != User.Identity.Name)
            {
                blnHasSOC = System.Text.RegularExpressions.Regex.IsMatch(strSMERCID, @"\A\b[0-9a-fA-F]+\b\Z");
            }
            return blnHasSOC;
        }

        // Get Role Area of User
        protected string returnRoleArea(string userGUID)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            try
            {
                cnn.Open();
                strQuery = "SELECT [RoleCityID] FROM [aspnet_Profile] WHERE [UserID] ='" + userGUID + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                string CityGUID = dt.Rows[0][0].ToString();
                da.Dispose();
                cmd.Dispose();

                cnn.Close();
                string CityName = ObtainUserCityFromGUID(CityGUID);
                return CityName;
            }
            catch //(Exception ex)
            {
                //  ErrorMessage.Text += "Return user City Error: " + ex.Message;
                cnn.Close();
                return null;
            }

        }

        // Update CalculateCO2 to 1
        protected void UpdateCalculateCO2to1(string strUserID)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;

            try
            {
                strQuery = "UPDATE [ChargingRecords] SET [CalculateCO2] = '1' WHERE [UserID] ='" + strUserID + "'";
                cnn.Open();

                cmd = new SqlCommand(strQuery, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch ( Exception ex)
            {
                lblTest.Text += "<br>UpdateCalculateCO2to1 Error: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();
            }
        }

        // Get Latest profile data on CO2, Energy Consumed, and ChargingCost
        protected void getCurrentProfileData(string strUserID, out float fltCO2, out float fltEnergyConsumed, out float fltChargingCost)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            fltCO2 = 0;
            fltEnergyConsumed = 0;
            fltChargingCost = 0;
            try
            {
                cnn.Open();
                strQuery = " SELECT [CO2Accumulation], [TotalEnergyConsumed], [TotalChargingCost] " +
                           " FROM  [aspnet_Profile] " +
                           " WHERE [UserID] = '" + strUserID + "'";

                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    fltCO2 = float.Parse(reader["CO2Accumulation"].ToString().Trim());
                    fltEnergyConsumed = float.Parse(reader["TotalEnergyConsumed"].ToString().Trim());
                    fltChargingCost = float.Parse(reader["TotalChargingCost"].ToString().Trim());
                }

                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                lblTest.Text += "<br>Error at getCurrentProfileData: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();
            }
        }

        // Return total energy consumed where CalculateCO2 = 0;
        protected float GetLatestEnergyConsumed(string strUserID)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            float fltEnergyConsumed = 0;

            try
            {
                cnn.Open();
                strQuery = " SELECT [EndMainPower]-[StartMainPower] as EnergyConsumedinkWH " +
                           " FROM [ChargingRecords] " +
                           " WHERE [UserID] = '" + strUserID + "' and [IsEnd] = '1' and [CalculateCO2] ='0'";

                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                string strValue = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strValue = dt.Rows[i][0].ToString();
                    if(!string.IsNullOrWhiteSpace(strValue))
                        fltEnergyConsumed += (float.Parse(strValue));
                }

                da.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                lblError.Text += "<br> GetLatestEnergyConsumed Error: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();

                //UpdateCalculateCO2to1(strUserID);

            }
            if (fltEnergyConsumed < 0)
            {
                fltEnergyConsumed = 0;
            }
            return fltEnergyConsumed;
        }

        // Return a list of EV Models.
        protected List<string> ListreturnListofEvCars()
        {
            List<string> EVList = new List<string>();

            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;
            try
            {
                cnn.Open();
                strQuery = "SELECT [Manufacturer] + ' ' + [Model] AS [Name] FROM [EV Model] ORDER BY Name";
                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EVList.Add(dt.Rows[i][0].ToString());
                }

                da.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                lblError.Text += "<br> EV Error: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                    cnn.Close();
            }
            return EVList;
        }

        // Populate the ddlState 
        protected void PopulateddlState()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            try
            {
                cnn.Open();
                strQuery = "SELECT State FROM [State]";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                ddlState.DataSource = dt;
                ddlState.DataValueField = "State";
                ddlState.DataTextField = "State";
                ddlState.DataBind();

                cmd.Dispose();
                da.Dispose();

            }

            catch (Exception ex)
            {
                lblError.Text += "<br> Error when Populating User State DDL" + ex.Message;

            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }

            // Default chosen to "CA" - California 
            ddlState.SelectedIndex = 4;
        }

        #endregion
        #region button clicks and helper functions

        protected void showMessage(string strMessage)
        {
            btnHideError.Visible = true;
            lblError.Text += strMessage;
        }

        // Update profile with new values
        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            // Change Password
            MembershipUser membUser = Membership.GetUser(User.Identity.Name);           


            // Obtain UserID
            string userGUID = strReturnUserGUIDfromUsername(Membership.GetUser().UserName);

            // Prelim setup (evcar, etc)
            
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            //Prelim setup
            List<string> models = new List<string>();
            string EVGUID = string.Empty;
            string SelectedModel = string.Empty;
            string EVCARname = ddlEVModel.SelectedValue; // Ev car name is two words, manufacturer and model thus need to separate the terms
            
            strQuery = "SELECT [Model] FROM [EV Model]";
            cmd = new SqlCommand(strQuery, cnn);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                models.Add(dt.Rows[i][0].ToString());
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (EVCARname.IndexOf(models[i]) != -1)
                    SelectedModel = models[i];
            }


            strQuery = "SELECT ID FROM [EV Model] WHERE MODEL='" + SelectedModel + "'";
            cmd = new SqlCommand(strQuery, cnn);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            EVGUID = dt.Rows[0][0].ToString();




            // Begin Transcation

            SqlCommand command = cnn.CreateCommand();
            SqlTransaction transaction; // Required for rollback features 

            cnn.Open();
            transaction = cnn.BeginTransaction("UpdateFunction");
            command.Connection = cnn;
            command.Transaction = transaction;

            bool blnPasses = true;  // Have a marker.  If the transaction fails, we need to also delete the user/membership that we created earlier for the new user.       


            // Get and Encrypt the social media password
            //string strPasswordText = tbSocialMediaAccPassword.Text;
            string strEncodedPassword = string.Empty;

            try
            {
                using (RijndaelManaged myR = new RijndaelManaged())
                {
                    byte[] byteRijKey = Convert.FromBase64String(WebConfigurationManager.AppSettings["RijKey"]);
                    byte[] byteRijIV = Convert.FromBase64String(WebConfigurationManager.AppSettings["RijIV"]);
                   // byte[] byteEncryptText = EncryptStringToBytes(strPasswordText, byteRijKey, byteRijIV);
                  //  strEncodedPassword = Convert.ToBase64String(byteEncryptText);
                }
            }
            catch
            {
            }

            bool blnSocialMedia = ddlSocialMediaID.SelectedIndex > 0;

            try
            {
                command.CommandText = "UPDATE [aspnet_Profile] SET [LastUpdatedDate]= @DateTime,  [EV ID]=@EVGUID ,"
                            + "[FirstName]= @FirstName, [LastName] = @LastName, [Address1] = @Address1 , [Address2] = @Address2, [City] = @City, [State] = @State, [PhoneNo] = @PhoneNo, [ZipCode] = @ZipCode, [SmartPhoneOS] = @SmartPhoneOS, [PhoneServiceCarrier] = @PhoneServiceCarrier, [SmartPhoneModelNo] = @SmartPhoneModelNo, ";

                if (blnSocialMedia)
                {
                    command.CommandText += " [SocialMediaID] = @SocialMediaID, ";
                }

                command.CommandText += " [SocialMediaAccountName] = @SocialMediaAccountName, [SocialMediaAccountPassword] = @SocialMediaAccountPassword, [MaximumVehicles] = @MaximumVehicles "
                                    + " WHERE [UserId] = @userGUID";

                command.Parameters.AddWithValue("@userGUID", userGUID);
                command.Parameters.AddWithValue("@DateTime", DateTime.Now);
                command.Parameters.AddWithValue("@FirstName", tbFirstName.Text);
                command.Parameters.AddWithValue("@LastName", tbLastName.Text);
                command.Parameters.AddWithValue("@Address1", tbAddress1.Text);
                command.Parameters.AddWithValue("@Address2", tbAddress2.Text);
                command.Parameters.AddWithValue("@City", tbCity.Text);
                command.Parameters.AddWithValue("@State", ddlState.SelectedValue);
                command.Parameters.AddWithValue("@ZipCode", tbZipCode.Text);
                command.Parameters.AddWithValue("@PhoneNo", tbPhoneNumber.Text);                
                command.Parameters.AddWithValue("@EVGUID", EVGUID);                
                command.Parameters.AddWithValue("@SmartPhoneOS", ddlSmartPhoneOS.SelectedIndex);
                command.Parameters.AddWithValue("@PhoneServiceCarrier", ddlPhoneServiceCarrier.SelectedIndex);
                command.Parameters.AddWithValue("@SmartPhoneModelNo", tbSmartPhoneModel.Text);
                

                if (blnSocialMedia)
                {
                    command.Parameters.AddWithValue("@SocialMediaID", ddlSocialMediaID.SelectedValue);                    
                }

                command.Parameters.AddWithValue("@SocialMediaAccountName", tbSocialMediaAccName.Text);
                command.Parameters.AddWithValue("@SocialMediaAccountPassword", strEncodedPassword);
                command.Parameters.AddWithValue("@MaximumVehicles", tbMaximumVehicles.Text);
                command.ExecuteNonQuery();

                // UPDATE MEMBERSHIP

                command.CommandText = "UPDATE [aspnet_Membership] SET [Email]= @TB_Email , [LoweredEmail] = @TB_EmailLowered, [PasswordQuestion] = @TB_PassQuestion, [PasswordAnswer] = @TB_PassAnswer WHERE [UserId] =@userGUID";

                command.Parameters.AddWithValue("@TB_Email", tbEmail.Text);
                command.Parameters.AddWithValue("@TB_EmailLowered", tbEmail.Text.ToLower());
                command.Parameters.AddWithValue("@TB_PassQuestion", tbPasswordQuestion.Text);
                command.Parameters.AddWithValue("@TB_PassAnswer", tbPasswordAnswer.Text);

                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                showMessage("Error Updating.  All transactions reversed: " + ex.Message);
                blnPasses = false;
                try
                {
                    transaction.Rollback();                   
                }
                catch (Exception ex2)
                {
                    showMessage("Transaction Rollback error: " + ex2.Message);
                }
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }

            
            if (blnPasses)
            {
                lblSuccessOrFailure.Text = "<span style='color: blue;'> Updated.</span>";
                
            }
            else
            {
                lblSuccessOrFailure.Text = "<span style='color: red;'> Error. </span>";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        #endregion
        #region clear Funcs

        // Clear all tbs and ddl
        protected void ClearAllTbs()
        {
            tbUsername.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbAddress1.Text = string.Empty;
            tbAddress2.Text = string.Empty;
            tbCity.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbFirstName.Text = string.Empty;
            tbLastName.Text = string.Empty;
            tbPasswordAnswer.Text = string.Empty;
            tbPasswordQuestion.Text = string.Empty;
            tbPhoneNumber.Text = string.Empty;
            tbSmartPhoneModel.Text = string.Empty;
            tbZipCode.Text = string.Empty;
            ddlEVModel.SelectedIndex = 0;
            ddlPhoneServiceCarrier.SelectedIndex = 0;
            ddlSmartPhoneOS.SelectedIndex = 0;
            ddlState.SelectedIndex = 4;
        }
        protected void HideError()
        {
            lblError.Text = string.Empty;
            btnHideError.Visible = false;
        }
        protected void btnHideError_Click(object sender, EventArgs e)
        {
            HideError();
        }
        #endregion
        #region Encryption
        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
        #endregion

        protected int findGVcolumn(string Name, GridView gv)
        {
            for (int j = 0; j < gv.Columns.Count; j++) // Cycle through all Columns of gridview
            {
                if (gv.Columns[j].HeaderText == Name)
                    return j;
            }
            return -1;
        }

        protected void GvEvListRowCreated(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < EvListColumnsToHide.Count(); i++)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[findGVcolumn(EvListColumnsToHide[i], GvEvList)].Visible = false;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[findGVcolumn(EvListColumnsToHide[i], GvEvList)].Visible = false;
                }
            }
        }

        protected void GvEvListSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = GvEvList.Rows[GvEvList.SelectedIndex];
            btnGvEvListModify.Visible = true;
            btnGvEvListDelete.Visible = true;
            ddlGvEvListModel.SelectedValue = gvRow.Cells[findGVcolumn("EvModelID", GvEvList)].Text;
        }

        protected void GvEvListDataBound(object sender, GridViewRowEventArgs e)
        {
            var i = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvEvList, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.ToolTip = cell.Text;
                }
            }
        }

        protected void PopulateEvList(string userId)
        {
            var DT = new DataTable();
            using (var conn = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "SELECT list.ID, list.EvModelID, Manufacturer+' '+Model AS [EVName], list.Nickname " +
                    "FROM [EVDemo].[dbo].[EV Model] as info, [EVDemo].[dbo].[UsersEVList] as list " +
                    "WHERE info.ID = list.EVModelID " + "AND " + "list.UserID = '" + userId + "' " +
                    "ORDER BY ID ";
                using (var cmd = new SqlCommand(sqlQuery, conn))
                {
                    using (var AD = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            AD.Fill(DT);
                        }
                        catch (Exception ex)
                        {
                            showMessage("Error at PopulateEvList: " + ex.Message);
                            return;
                        }
                        if (DT.Rows.Count == 0)
                        {
                            showMessage("Please add at least one EV");
                        }
                    }
                }
                var newDT = new DataTable();
                newDT.Columns.Add(new DataColumn("ID", typeof(string)));
                newDT.Columns.Add(new DataColumn("EvModelID", typeof(string)));
                newDT.Columns.Add(new DataColumn("Number", typeof(string)));
                newDT.Columns.Add(new DataColumn("EvName", typeof(string)));
                newDT.Columns.Add(new DataColumn("Nickname", typeof(string)));
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    newDT.Rows.Add(DT.Rows[i][0].ToString(), DT.Rows[i][1].ToString(), (i+1).ToString(), DT.Rows[i][2].ToString(), DT.Rows[i][3].ToString());
                }
                Session["data"] = newDT;
                GvEvList.DataSource = Session["data"];
                GvEvList.DataBind();
                GvEvList.Visible = true;
                GvEvList.SelectedIndex = -1;
                tbNickname.Text = String.Empty;
            }
        }

        protected void PopulateddlGvEvListModel()
        {
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    ddlGvEvListModel.Visible = true;
                    tbNickname.Visible = true;
                    string strQuery = "SELECT ID, Manufacturer+' '+Model AS [EV Info] FROM [EV Model] ORDER BY [EV Info]";
                    string strListItemInsert = "Select...";
                    SqlCommand cmd = new SqlCommand(strQuery, cnn);
                    DataTable DT = new DataTable();
                    SqlDataAdapter DA = new SqlDataAdapter();
                    DA.SelectCommand = cmd;
                    DA.Fill(DT);
                    ddlGvEvListModel.DataSource = DT;
                    ddlGvEvListModel.DataValueField = "ID";
                    ddlGvEvListModel.DataTextField = "EV Info";
                    ddlGvEvListModel.DataBind();
                    ListItem l1 = new ListItem(strListItemInsert, "-1");
                    ddlGvEvListModel.Items.Insert(0, l1);
                    DA.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    showMessage("Error at populateEvInfo: " + ex.Message);
                }
            }
        }

        protected Boolean canAddNewEV(string userid, bool allowEqual)
        {
            string maximumVehicles = string.Empty;
            string currentVehicles = string.Empty;
            using (var cnn = new SqlConnection(connectionString))
            {

                try
                {
                    string strQuery = "SELECT [MaximumVehicles] FROM [aspnet_Profile] WHERE UserId = '"
                        + userid + "' ";
                    var cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    var da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    var dt = new DataTable();
                    da.Fill(dt);
                    maximumVehicles = dt.Rows[0][0].ToString();

                    da.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    showMessage("Error at get maximum vehicles number: " + ex.Message);
                }

                try
                {
                    string strQuery = "SELECT COUNT(EVModelID) FROM [UsersEVList] WHERE UserId = '"
                        + userid + "' ";
                    var cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    var da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    var dt = new DataTable();
                    da.Fill(dt);
                    currentVehicles = dt.Rows[0][0].ToString();

                    da.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    showMessage("Error at get current vehicles number: " + ex.Message);
                }
            }
            if (!allowEqual)
            {
                if (Int32.Parse(maximumVehicles) <= Int32.Parse(currentVehicles))
                {
                    showMessage("You can only add up to " + Int32.Parse(maximumVehicles) + " EVs");
                    return false;
                }
                return true;
            }
            else
            {
                if (Int32.Parse(maximumVehicles) < Int32.Parse(currentVehicles))
                {
                    showMessage("You can only add up uo " + Int32.Parse(maximumVehicles) + " EVs");
                    return false;
                }
                return true;
            }

        }

        protected Boolean canDeleteExistEV(string userid)
        {
            string currentVehicles = string.Empty;
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    string strQuery = "SELECT COUNT(EVModelID) FROM [UsersEVList] WHERE UserId = '"
                        + userid + "' ";
                    var cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    var da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    var dt = new DataTable();
                    da.Fill(dt);
                    currentVehicles = dt.Rows[0][0].ToString();

                    da.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    showMessage("Error at get current vehicles number: " + ex.Message);
                }
            }
            if (Int32.Parse(currentVehicles) == 1)
            {
                showMessage("You have to keep at least 1 EV");
                return false;
            }
            return true;
        }

        protected void btnGvEvListAddClick(object sender, EventArgs e)
        {
            HideError();
            string userid = strReturnUserGUIDfromUsername(User.Identity.Name);
            if (!canAddNewEV(userid, false))
                return;
            if (ddlGvEvListModel.SelectedIndex == 0)
            {
                showMessage("Must select an EV");
                return;
            }
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    string strQuery = "INSERT INTO [UsersEVList] (UserID, EVModelID, Nickname) VALUES(@UserID, @EVModelID, @Nickname) ";
                    var cmd = new SqlCommand(strQuery, cnn);
                    SqlDataReader readerProfile = null;
                    cnn.Open();
                    cmd.Parameters.Add(new SqlParameter("@UserID", userid));
                    cmd.Parameters.Add(new SqlParameter("@EVModelID", ddlGvEvListModel.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@Nickname", tbNickname.Text));
                    readerProfile = cmd.ExecuteReader();
                    readerProfile.Close();
                }
                catch (Exception ex)
                {
                    showMessage("Error at add button click: " + ex.Message);
                    return;
                }


            }
            GvEvList.SelectedIndex = -1;
            PopulateEvList(userid);
            btnGvEvListModify.Visible = false;
            btnGvEvListDelete.Visible = false;
            showMessage("Information Added");
        }

        protected void btnGvEvListModifyClick(object sender, EventArgs e)
        {
            HideError();
            string userid = strReturnUserGUIDfromUsername(User.Identity.Name);
            GridViewRow gvEvRow = GvEvList.Rows[GvEvList.SelectedIndex];
            string id = gvEvRow.Cells[findGVcolumn("ID", GvEvList)].Text;
            if (!canAddNewEV(userid, true))
                return;
            if (ddlGvEvListModel.SelectedIndex == 0)
            {
                showMessage("Must select an EV");
                return;
            }
            if (GvEvList.SelectedIndex == 0)
            {
                using (var cnn = new SqlConnection(connectionString))
                {
                    try
                    {
                        string strQuery = "UPDATE [aspnet_Profile] SET [EV ID] = @EVModelID WHERE UserId = @UserId ";
                        var cmd = new SqlCommand(strQuery, cnn);
                        SqlDataReader readerProfile = null;
                        cnn.Open();
                        cmd.Parameters.Add(new SqlParameter("@UserId", userid));
                        cmd.Parameters.Add(new SqlParameter("@EVModelID", ddlGvEvListModel.SelectedValue));

                        readerProfile = cmd.ExecuteReader();
                        readerProfile.Close();
                    }
                    catch (Exception ex)
                    {
                        showMessage("Error at modify button click: " + ex.Message);
                        return;
                    }
                }
            }
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    string strQuery = "UPDATE [UsersEVList] SET EVModelID = @EVModelID, Nickname = @Nickname WHERE ID = @ID ";
                    var cmd = new SqlCommand(strQuery, cnn);
                    SqlDataReader readerProfile = null;
                    cnn.Open();
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Parameters.Add(new SqlParameter("@EVModelID", ddlGvEvListModel.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@Nickname", tbNickname.Text));
                    readerProfile = cmd.ExecuteReader();
                    readerProfile.Close();
                }
                catch (Exception ex)
                {
                    showMessage("Error at modify button click: " + ex.Message);
                    return;
                }
            }
            GvEvList.SelectedIndex = -1;
            PopulateEvList(userid);
            btnGvEvListModify.Visible = false;
            btnGvEvListDelete.Visible = false;
            showMessage("Information Modified");
        }

        protected void btnGvEvListDeleteClick(object sender, EventArgs e)
        {
            HideError();
            string userid = strReturnUserGUIDfromUsername(User.Identity.Name);
            if (!canDeleteExistEV(userid))
                return;
            GridViewRow gvEvRow = GvEvList.Rows[GvEvList.SelectedIndex];
            string id = gvEvRow.Cells[findGVcolumn("ID", GvEvList)].Text;
            using (var cnn = new SqlConnection(connectionString))
            {
                try
                {
                    string strQuery = "DELETE FROM [UsersEVList] WHERE ID = @ID ";
                    var cmd = new SqlCommand(strQuery, cnn);
                    SqlDataReader readerProfile = null;
                    cnn.Open();
                    cmd.Parameters.Add(new SqlParameter("@ID", id));

                    readerProfile = cmd.ExecuteReader();
                    readerProfile.Close();
                }
                catch (Exception ex)
                {
                    showMessage("Error at delete button click: " + ex.Message);
                    return;
                }
            }
            GvEvList.SelectedIndex = -1;
            PopulateEvList(userid);
            btnGvEvListModify.Visible = false;
            btnGvEvListDelete.Visible = false;
            showMessage("Information Deleted");
        }
    }
}

