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

namespace EVUser.Account
{
    public partial class NewUserCreation : System.Web.UI.Page
    {
        // connectionString is the string to connect to the SQL database.
        string connectionString = WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAll();
            }            
        }     

        #region ButtonClicks, btn Helper Functions

        // Cancel button, redirect to Login Page
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect user to Login page
            Response.Redirect("~/Account/Login.aspx");
        }
        // Attempt to create a new user
        protected void btnCreateNewUser_Click(object sender, EventArgs e)
        {
            ClearAllLblError();            
            // Validation checks: 1) Username Check, 2) Check for Validation Email

            bool blnPass = true;
            // 1) Check Username.  true = no username, false = username used.
            if (!blnIsDuplicateUserName(tbDesiredUsername.Text))
            {
                lblDesiredUserNameError.Text = "<span style ='color: red;'>-Username already exists.</span>";
                blnPass = false;
            }

            // 2) Check for Validation Email
            if (!blnIsEmailValidated(tbEmail.Text))
            {
                lblEmailError.Text = "-Specified email is not valid.";
                blnPass = false;
            }

            // If errors, return.
            if (!blnPass)
            {
                lblSuccessOrFailure.Text = "<span style='color: red;'> View errors below and re-enter your password. </span>";
                return;
            }

            // If no validation problems, unvalidate this email.
            if (!blnUnValidateEmail(tbEmail.Text))
            {            
                
                return;
            }

            // Get Organization ID associated with the Email
            string strOrganizationGUID = strReturnEmailOrgID(tbEmail.Text);

            // Create a list to hold all of the organizations associated with the email
            List<string> listOfOrganizations = listReturnOrganizations(strOrganizationGUID);

            // Create a list to hold the IDs of the organization + "User" roleIDs.
            List<string> listofRoleIDs = listRoleIDs(listOfOrganizations);

            // Create the Membership User
            // If the transactions after these CreateUser functions fail, everything will be rolled back.
            if (string.IsNullOrWhiteSpace(tbPasswordQuestion.Text))
            {
                Membership.CreateUser(tbDesiredUsername.Text, tbPassword.Text, tbEmail.Text);
            }
            else
            {
                // Generate a GUID for the User  (Why is this only pertain to the Password Question createuser?)
                Guid guidUser = System.Guid.NewGuid();

                // Create user
                MembershipCreateStatus status;
                Membership.CreateUser(tbDesiredUsername.Text, tbPassword.Text, tbEmail.Text, tbPasswordQuestion.Text, tbPasswordAnswer.Text, true, out status);
                
            }
            // Obtain the user's userGUID
            string strUserGUID = strReturnUserGUIDfromUsername(tbDesiredUsername.Text);
            

            // Setup SQL Data connection
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            DataTable dt = null;
            SqlDataAdapter da;

            SqlCommand command = cnn.CreateCommand();
            SqlTransaction transaction; // Required for rollback features 

            //Create string to hold Account info
            string EVUserAccountType = string.Empty;
            string EVUserAccountExpirationWindow = string.Empty;
            string EVUserMaximumVehicles = string.Empty;
            try
            {

                cnn.Open();
                // Obtain all Models
                strQuery = "SELECT [EVUserAccountTypeID] " +
                           "FROM [NewUserAuthorization] " +
                           "WHERE [EmailAddress] = '" + tbEmail.Text + "'";
                //, [EVUserAccountExpirationWindow
                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                EVUserAccountType = dt.Rows[0][0].ToString();

                strQuery = "SELECT [EVUserAccountExpirationWindowID] " +
                           "FROM [NewUserAuthorization] " +
                           "WHERE [EmailAddress] = '" + tbEmail.Text + "'";
                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                EVUserAccountExpirationWindow = dt.Rows[0][0].ToString();

                strQuery = "SELECT [MaximumVehicles] " +
                           "FROM [NewUserAuthorization] " +
                           "WHERE [EmailAddress] = '" + tbEmail.Text + "'";
                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                EVUserMaximumVehicles = dt.Rows[0][0].ToString();

                cmd.Dispose();

            }
            catch (Exception ex)
            {
                lblError.Text += "<br> Error at CreateNewUser, Select EVUserAccountType. " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
            // Create a string to hold the EV GUID
            string strEVGUID = string.Empty;   

            ////////////////////////////////////////////////////////////////////////////////////
            // Begin SQL Database access 
            try
            {
                
                cnn.Open();

                /// Insert EV Information.

                // Get EV Name
                string strEVname = ddlEVModel.SelectedValue;

                // Create a list to store the EV full names.
                List<string> models = new List<string>();

                // Create string to hold the EV Model
                string strSelectedModel = string.Empty;

                 

                // Obtain all Models
                strQuery = "SELECT [Model] FROM [EV Model]";
                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                // Insert all EV Models                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    models.Add(dt.Rows[i][0].ToString());
                }

                // Check for the associated EV with this account.
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strEVname.IndexOf(models[i]) != -1)
                        strSelectedModel = models[i];
                }

                // Obtain the associated EV GUID.
                strQuery = "SELECT ID FROM [EV Model] WHERE MODEL='" + strSelectedModel + "'";
                cmd = new SqlCommand(strQuery, cnn);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);

                strEVGUID = dt.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }

            cnn.Open();
            transaction = cnn.BeginTransaction("CreateNewUser");
            command.Connection = cnn;
            command.Transaction = transaction;

            bool blnPasses = true ; 

            // Begin Transaction and Rollback Protection

            try
            {
                // First Delete any pre existing roles
                command.CommandText = "Delete from [aspnet_UsersInRoles] WHERE [UserId] = @userGUID";
                command.Parameters.AddWithValue("@userGUID", strUserGUID);
                command.ExecuteNonQuery();

                // Insert Role data
                string strRole = string.Empty;
                command.Parameters.AddWithValue("@ListOfRoleGuid", strRole);

                foreach (string strRoleID in listofRoleIDs)
                {
                    strRole = strRoleID;
                    command.CommandText = "INSERT INTO [aspnet_UsersInRoles](UserId, RoleId) VALUES(@userGUID, @ListOfRoleGuid)";
                    command.Parameters["@ListOfRoleGuid"].Value = strRole;
                    command.ExecuteNonQuery();
                }

                // Insert Profile Data
                command.CommandText = "INSERT INTO [aspnet_Profile](UserId, LastUpdatedDate, FirstName, LastName, Address1, Address2, ZipCode, City, State, RoleCityID, PhoneNo, [EV ID], SMERCID, SmartPhoneOS, PhoneServiceCarrier, SmartPhoneModelNo, EVUserAccountTypeID, EVUserAccountExpirationWindowID, MaximumVehicles ) " +
                            "VALUES(@userGUID ,@DateTime,@FirstName,@LastName,@Address1 ,@Address2, @ZipCode, @City, @State,@RoleCityGUID,@PhoneNo,@EVGUID, @SmercID, @SmartPhoneOS, @PhoneServiceCarrier, @SmartPhoneModelNo, @EVUserAccountTypeID, @EVUserAccountExpirationWindowID, @MaximumVehicles)";

                command.Parameters.AddWithValue("@DateTime", DateTime.Now);
                command.Parameters.AddWithValue("@FirstName", tbFirstName.Text);
                command.Parameters.AddWithValue("@LastName", tbLastName.Text);
                command.Parameters.AddWithValue("@Address1", tbAddress1.Text);
                command.Parameters.AddWithValue("@Address2", tbAddress2.Text);
                command.Parameters.AddWithValue("@City", tbCity.Text);
                command.Parameters.AddWithValue("@State", ddlState.SelectedValue);
                command.Parameters.AddWithValue("@ZipCode", tbZipCode.Text);
                command.Parameters.AddWithValue("@RoleCityGUID", strOrganizationGUID);
                command.Parameters.AddWithValue("@EVGUID", strEVGUID);
                command.Parameters.AddWithValue("@SmercID", tbDesiredUsername.Text);
                command.Parameters.AddWithValue("@PhoneNo", tbPhoneNumber.Text);                
                command.Parameters.AddWithValue("@SmartPhoneOS", ddlSmartPhoneOS.SelectedIndex);
                command.Parameters.AddWithValue("@PhoneServiceCarrier", ddlPhoneServiceCarrier.SelectedIndex);
                command.Parameters.AddWithValue("@SmartPhoneModelNo", tbSmartPhoneModel.Text);
                command.Parameters.AddWithValue("@EVUserAccountTypeID", EVUserAccountType);
                command.Parameters.AddWithValue("@EVUserAccountExpirationWindowID", EVUserAccountExpirationWindow);
                command.Parameters.AddWithValue("@MaximumVehicles", EVUserMaximumVehicles);

                command.ExecuteNonQuery();

                // Update Membership

                command.CommandText = "UPDATE [aspnet_Membership] SET [Email]= @TB_Email , [LoweredEmail] = @TB_EmailLowered, [PasswordQuestion] = @TB_PassQuestion, [PasswordAnswer] = @TB_PassAnswer WHERE [UserId] =@userGUID";

                command.Parameters.AddWithValue("@TB_Email", tbEmail.Text);
                command.Parameters.AddWithValue("@TB_EmailLowered", tbEmail.Text.ToLower());
                command.Parameters.AddWithValue("@TB_PassQuestion", tbPasswordQuestion.Text);
                command.Parameters.AddWithValue("@TB_PassAnswer", tbPasswordAnswer.Text);

                command.ExecuteNonQuery();

                // Insert to [UsersEVList]
                command.CommandText = "INSERT INTO [UsersEVList](UserID, EVModelID ) " + "VALUES(@userGUID, @EVGUID)";
                command.ExecuteNonQuery();
                // Commit all transactions
                transaction.Commit();
            }
            catch (Exception ex)
            {
                lblError.Text += "<br> Error at CreateNewUser, All transacations rolled back. " + ex.Message;
                blnPasses = false;
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    showMessage("<br>Error at CreateNewUser: " + ex2.Message);
                }
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }

            // If error, delete user.
            if (!blnPasses)
            {
                // Delete the user.
                Membership.DeleteUser(tbDesiredUsername.Text);
                // REvalidate the email.
                ValidateEmail(tbEmail.Text);
                lblSuccessOrFailure.Text = "<span style='color: red;'> Failed.  Please try again or contact the administrator </span>";
            }
            // Success!
            else
            {
                ClearAllTbs();
                ClearAllLblError();
                HideError();
                lblSuccessOrFailure.Text = "<span style='color: blue;'> User created! </span>";                
            }            
        }


        protected void showMessage(string strMessage)
        {
            btnHideError.Visible = true;
            lblError.Text += strMessage;
        }


        // Return a list of the ROLE ids associated with the organizations
        protected List<string> listRoleIDs(List<string> listOrgNames)
        {
            List<string> listRoleIDs = new List<string>();

            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            
            foreach (string strOrgName in listOrgNames)
            {
                cnn.Open();
                
                strQuery = "SELECT RoleId FROM [aspnet_Roles] WHERE RoleName = '" + strOrgName + " User" + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    listRoleIDs.Add(reader["RoleId"].ToString());
                }

                if (cnn != null)
                {
                    cnn.Close();
                }
            }            
            return listRoleIDs;
        }


        // Return a list of the name of organizations from the organization ID
        protected List<string> listReturnOrganizations(string strOrganizationGUID)
        {
            List<string> listOrganizationNames = new List<string>();
            if (strOrganizationGUID != "-1")
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                string strQuery;
                SqlCommand cmd;
                DataTable dt = null;
                SqlDataAdapter da;

                try
                {
                    strQuery = " SELECT [Name] FROM City " +
                               " WHERE ID ='" + strOrganizationGUID + "'";
                    //" Where ID = 'b033893c-a86d-4c87-a134-f2a155b4e2aa'";

                    cnn.Open();
                    cmd = new SqlCommand(strQuery, cnn);
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        listOrganizationNames.Add(dt.Rows[0][0].ToString());
                    }
                    else // If dt.rows.count == 0, then the GUID represented a combinated city, thus search the combinated table.
                    {
                        strQuery = " SELECT [CombinatedCityID], [MainCityID] " +
                                   " FROM [CombinatedCity] " +
                                   " WHERE [ID] = '" + strOrganizationGUID + "'";

                        cmd = new SqlCommand(strQuery, cnn);
                        cmd.CommandType = CommandType.Text;
                        da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        dt = new DataTable();
                        da.Fill(dt);

                        string strMainCityGUID = dt.Rows[0][1].ToString();

                        listOrganizationNames.Add(ObtainUserCityFromGUID(strMainCityGUID));

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            listOrganizationNames.Add(ObtainUserCityFromGUID(dt.Rows[i][0].ToString()));
                        }
                    }

                    cmd.Dispose();
                    da.Dispose();
                }
                catch (Exception ex)
                {
                    lblError.Text +="<br> Error at listReturnOrganizations: " + ex.Message;
                }
                finally
                {
                    if (cnn != null)
                        cnn.Close();
                }
            }
            return listOrganizationNames;
        }

        // Obtain Email's Organization ID
        protected string strReturnEmailOrgID(string strEmail)
        {
            string strOrgID = string.Empty;

            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            try
            {
                cnn.Open();
                // Retrieve organization ID
                strQuery = " SELECT [Organization] " +
                           " FROM [NewUserAuthorization] " +
                           " WHERE [EmailAddress] = '" + strEmail + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    strOrgID = reader["Organization"].ToString();
                }

                // Dispose and close
                reader.Close();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                lblError.Text += "<br> Error at strReturnEmailOrgID: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
            return strOrgID;
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

                if(reader.Read())
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

        protected void ValidateEmail(string strEmail)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;

            try
            {
                cnn.Open();
                strQuery = " UPDATE [NewUserAuthorization] " +
                           " SET [Valid] = '1' " +
                           " WHERE [EmailAddress] = '" + strEmail + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();  
            }
            catch (Exception ex)
            {
                showMessage("<br>Error at ValidateEmail: " + ex.Message);
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
        }

        // Un-validate the Email Adress i.e. Set valid to 0
        protected bool blnUnValidateEmail(string strEmail)
        {
            bool blnValidated = false;

            // Connect with database and check
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;

            try
            {
                cnn.Open();
                strQuery = " UPDATE [NewUserAuthorization] " +
                           " SET [Valid] = '0' " +
                           " WHERE [EmailAddress] = '" + strEmail + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                blnValidated = true;

            }
            catch
            {

            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
            return blnValidated;


        }

        // Validate Email address
        protected bool blnIsEmailValidated(string strEmail)
        {
            bool blnValid = false;

            // Connect with database and check
            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;

            try
            {
                cnn.Open();

                // Validate the given email
                strQuery = " SELECT [Valid] " +
                           " FROM [NewUserAuthorization] " +
                           " WHERE [EmailAddress] = '" + strEmail + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    // Set the return bool to the result of the validation
                    blnValid = bool.Parse(reader["Valid"].ToString());     
             
                    //// Close the cnn
                    //if (cnn != null)
                    //{
                    //    cnn.Close();
                    //}

                    //// if valid, then devalidate the email address.
                    //if (blnValid)
                    //{
                    //    cnn.Open();
                    //    strQuery = " UPDATE [NewUserAuthorization] " +
                    //               " SET [Valid] = '0' " +
                    //               " WHERE [EmailAddress] = '" + strEmail + "'";
                    //    cmd = new SqlCommand(strQuery, cnn);
                    //    cmd.ExecuteNonQuery();
                    //    cmd.Dispose();                        
                    //}
                }
                
                // Close and dispose cmd and reader
                reader.Close();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                lblError.Text += "Error at blnIsEmailValidated: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
            return blnValid;
        }

        // Check for duplicate usernames.  True = no username, False = username exists
        protected bool blnIsDuplicateUserName(string strUsername)
        {
            
            bool blnPasses = true;

            SqlConnection cnn = new SqlConnection(connectionString);
            string strQuery;
            SqlCommand cmd;
            try
            {
                cnn.Open();
                // Check for duplicate usernames
                strQuery = "Select [Activate] FROM [aspnet_Users] WHERE [LoweredUserName] = '" + tbDesiredUsername.Text.ToLower().Replace("'", "") + "'";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // The username exists.                    
                    blnPasses = false;  
                }
                else
                {
                    // The username does not exist
                    blnPasses = true;
                }
                reader.Close();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                lblError.Text += "<br> Error at blnCheckForDuplicateUserName: " + ex.Message;
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
            return blnPasses;
        }

        #endregion

        #region Misc Helper Functions.  ObtainUserCityFromGuid, ReturnUniqueCombo

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
        }

        // Populate ddlPhoneService
        protected void Populate_ddlPhoneServiceCarrier() // Check the EV MODEL table.  IF there is no records, then add the Nissan Leaf and Chevorlet Volt so the program can run
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
        #region clear Funcs
        protected void ClearAllLblError()
        {
            lblEmailError.Text = string.Empty;
            lblDesiredUserNameError.Text = string.Empty;            
        }

        // Clear all tbs and ddl
        protected void ClearAllTbs()
        {
            tbDesiredUsername.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbAddress1.Text = string.Empty;
            tbAddress2.Text = string.Empty;
            tbCity.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbFirstName.Text = string.Empty;
            tbLastName.Text = string.Empty;
            tbPassword.Text = string.Empty;
            tbPasswordAnswer.Text = string.Empty;
            tbPasswordQuestion.Text = string.Empty;
            tbPhoneNumber.Text = string.Empty;
            tbRepeatPassword.Text = string.Empty;
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

        
    }
}