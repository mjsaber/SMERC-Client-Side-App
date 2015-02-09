using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Web.Security;

namespace EVUser
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (User != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    bool blnIsLADWPUser = false;
                    //RolePrincipal rp = (RolePrincipal)User;
                    string[] roles = Roles.GetRolesForUser();
                    if (roles[0] == "UCLA - Los Angeles Maintainer" || roles[0] == "Pasadena Operator")
                        Response.Redirect("~/Error.aspx?ErrMsg=Privilege", false);

                    for (int i = 0; i < roles.Length; i++)
                    {
                        if (roles[i] == "LADWP - Los Angeles User")
                        {
                            blnIsLADWPUser = true;
                        }
                    }

                    string strUserID = Membership.GetUser().ProviderUserKey.ToString();
                    string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    SqlConnection cnn = new SqlConnection(strCnn);

                    string strQuery = @"SELECT Priority FROM aspnet_Profile WHERE UserID=@UserID";
                    SqlCommand cmd = new SqlCommand(strQuery, cnn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@UserID"].Value = new Guid(Membership.GetUser().ProviderUserKey.ToString());
                    try
                    {
                        cnn.Open();
                        object obj = cmd.ExecuteScalar();
                        if (obj == null)
                        {
                            Response.Redirect("~/Error.aspx?ErrMsg=Privilege", false);
                        }
                        cmd.Dispose();

                        int intPriority = Convert.ToInt16(obj);
                        if (blnIsLADWPUser == true)
                        {
                            trMap.Visible = false;
                            trChargingRecord.Visible = false;
                            trSocialMedia.Visible = false;
                            trUserManual.Visible = false;
                        }
                        else if (intPriority == 0)
                        {
                            trMap.Visible = true;
                            trChargingRecord.Visible = false;
                            trSocialMedia.Visible = false;
                            trUserManual.Visible = true;
                        }
                        else if (intPriority > 0)
                        {
                            trMap.Visible = true;
                            trChargingRecord.Visible = true;
                            trSocialMedia.Visible = true;
                            trUserManual.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        //protected void btnViewStatus_Click(object sender, EventArgs e)
        //{
        //    string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        //    SqlConnection cnn = new SqlConnection(strCnn);

        //    string strQuery = @"SELECT UserId FROM aspnet_Users WHERE LoweredUserName=@Username";
        //    SqlCommand cmd = new SqlCommand(strQuery, cnn);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 256));
        //    cmd.Parameters["@Username"].Value = User.Identity.Name;
        //    SqlDataReader sdr = null;
        //    try
        //    {
        //        cnn.Open();
        //        object obj = cmd.ExecuteScalar();
        //        if (obj == null)
        //        {
        //            //throw new System.Exception("Fail to read user id");
        //        }
        //        cmd.Dispose();

        //        strQuery = "Select StationID FROM ChargingRecords " +
        //                   "WHERE UserID=@UserID and StartTime= " +
        //                   "(SELECT MAX(cr.StartTime) AS lst" +
        //                   " FROM ChargingRecords as cr" +
        //                   " WHERE IsEnd=0 AND UserID=@UserID)";
        //        cmd = new SqlCommand(strQuery, cnn);
        //        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier));
        //        cmd.Parameters["@UserID"].Value = obj;
        //        obj = cmd.ExecuteScalar();
        //        if (obj == null)
        //        {
        //            Response.Redirect("~/Error.aspx?ErrMsg=NotInCharging");
        //        }
        //        else
        //        {
        //            Response.Redirect("~/Status.aspx?LoadStatus=1");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        if (sdr != null)
        //            sdr.Close();
        //        cnn.Close();
        //    }
        //}

        protected void btnViewStatus_Click(object sender, ImageClickEventArgs e)
        {
            string strCnn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnection cnn = new SqlConnection(strCnn);

            string strQuery = @"SELECT UserId FROM aspnet_Users WHERE LoweredUserName=@Username";
            SqlCommand cmd = new SqlCommand(strQuery, cnn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 256));
            cmd.Parameters["@Username"].Value = User.Identity.Name;
            SqlDataReader sdr = null;
            try
            {
                cnn.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == null)
                {
                    //throw new System.Exception("Fail to read user id");
                }
                cmd.Dispose();

                strQuery = "Select StationID FROM ChargingRecords " +
                           "WHERE UserID=@UserID and StartTime= " +
                           "(SELECT MAX(cr.StartTime) AS lst" +
                           " FROM ChargingRecords as cr" +
                           " WHERE IsEnd=0 AND UserID=@UserID)";
                cmd = new SqlCommand(strQuery, cnn);
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier));
                cmd.Parameters["@UserID"].Value = obj;
                obj = cmd.ExecuteScalar();
                if (obj == null)
                {
                    Response.Redirect("~/Error.aspx?ErrMsg=NotInCharging", false);
                }
                else
                {
                    Response.Redirect("~/Status.aspx?LoadStatus=1", false);
                }

            }
            catch (Exception ex)
            {
                string strError = ex.Message;
            }
            finally
            {
                if (sdr != null)
                    sdr.Close();
                cnn.Close();
            }

        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {

            string fileName = "EVSmartPlugUserManual.pdf";// System.Web.Configuration.WebConfigurationManager.AppSettings["UserManual"].ToString(); //"EVSmartPlug User Manual.pdf";
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.TransmitFile(fileName);
            Response.End();
        }
    }

    internal class ZigBeeNode
    {
        //added on 1/16/2012
        public string strGatewayIP;
        public string strNodeID;
        public string strStationName;
        public string strPowerInfo;
        public float fltVoltage;
        public float fltCurrent;
        public float fltFrequency;
        public float fltPowerFactory;
        public double dblActivePower;
        public double dblApparentPower;
        public double dblMainEnergy;
        public int intTryTimes;
        public bool blnIsSuccessful;
        public string strMeterStatus;

        public string strDutyCycle;
        public string strRelayOnOff;
        public string strPlugIn;
        public string strErrorCode;
        public string strChannelNo;
        public string strStateOfCharging;

        private string strResponse;
        private int intIndex;

        public int intTimeout = 5000;
        public int intRetrievelInterval = 300000;
        public int intRepeatTimes = 2;
        public bool blnAutoSwitchOff = true;
        public float fltCurrentValve = 0.001f;

        public bool blnSuccessfulInfo;
        public bool blnSuccessfulControl;
        public bool blnSuccessfulStatus;
        //end of added on 1/16/2012

        //Old variables

        public string strStationInfo;
        public string strUsername;
        public string strUserID;

        public int TIME_OUT = 10000; //10 seconds
        public int RETRY_TIMES = 3;

        //Added on 2/20/2013
        private const int DUTY_CYCLE_RETURN_LENGTH = 32;
        private int intDutyCycleReturnGroupNo=0;

        public void ControlNode(bool blnRelayOnOff)
        {
            try
            {
                string strResponse;
                //Sync solution
                if (blnRelayOnOff)
                {
                    //Turn on
                    strResponse = "http://" + strGatewayIP + "/bscp.asp?BSCP=$01,CD," + strNodeID + ",0104,0006,01,01,FFFF,01,01,00,FFFF";
                }
                else
                {
                    //Turn off
                    strResponse = "http://" + strGatewayIP + "/bscp.asp?BSCP=$01,CD," + strNodeID + ",0104,0006,01,01,FFFF,00,01,00,FFFF";
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = TIME_OUT; //7000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public int RetrieveNodeStatus()
        {
            int intIsOn = -1;
            try
            {
                strResponse = "http://" + strGatewayIP + "/bscp.asp?BSCP=$01,CD," + strNodeID + ",0104,0006,01,01,FFFF,00,00,00,FFFF,0000";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = TIME_OUT; //7000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    intIndex = strResponse.IndexOf(",FFFF,000000") + 6;
                    strResponse = strResponse.Substring(intIndex, 10);

                    if (strResponse == "0000001001")
                        intIsOn = 1;    //on
                    else if (strResponse == "0000001000")
                        intIsOn = 0;    //off
                }
            }
            catch
            {
            }

            return intIsOn;
        }

        //public void RetrieveNodePowerInfo()
        //{
        //    blnIsSuccessful = true;
        //    strResponse = "http://" + strGatewayIP + "/bscp.asp?BSCP=$01,CD," + strNodeID + ",0104,0702,01,01,FFFF,00,00,00,FFFF,0080";
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

        //        //request.Method = "POST";
        //        request.Timeout = TIME_OUT; //7000 (7 seconds) for cellphone, it can be more longer

        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        if (response == null || response.StatusCode != HttpStatusCode.OK)
        //        {
        //            blnIsSuccessful = false;
        //            fltVoltage = 0.00f;
        //            fltCurrent = 0.00f;
        //            fltFrequency = 0.00f;
        //            fltPowerFactory = 0.00f;
        //            dblActivePower = 0.00;
        //            dblApparentPower = 0.00;
        //            dblMainEnergy = 0.00;
        //        }
        //        else
        //        {
        //            //successful
        //            Stream dataStream = response.GetResponseStream();


        //            StreamReader reader = new StreamReader(dataStream);
        //            strResponse = reader.ReadToEnd();
        //            reader.Close();
        //            dataStream.Close();
        //            response.Close();

        //            intIndex = strResponse.IndexOf("FFFF,00800041");
        //            strResponse = strResponse.Substring(intIndex + 15);
        //            intIndex = strResponse.IndexOf(",");
        //            strResponse = strResponse.Substring(0, intIndex);

        //            if (strResponse.Length == 42)
        //            {
        //                try
        //                {
        //                    fltVoltage = ((float)int.Parse(strResponse.Substring(0, 4), System.Globalization.NumberStyles.HexNumber)) / 100;
        //                }
        //                catch
        //                {
        //                    fltVoltage = 0.00f;
        //                    blnIsSuccessful = false;
        //                }
        //                try
        //                {
        //                    fltCurrent = ((float)int.Parse(strResponse.Substring(4, 4), System.Globalization.NumberStyles.HexNumber)) / 100;
        //                }
        //                catch
        //                {
        //                    fltCurrent = 0.00f;
        //                    blnIsSuccessful = false;
        //                }
        //                try
        //                {
        //                    fltFrequency = ((float)int.Parse(strResponse.Substring(8, 4), System.Globalization.NumberStyles.HexNumber)) / 100;
        //                }
        //                catch
        //                {
        //                    fltFrequency = 0.00f;
        //                    blnIsSuccessful = false;
        //                }
        //                try
        //                {
        //                    fltPowerFactory = ((float)int.Parse(strResponse.Substring(12, 2), System.Globalization.NumberStyles.HexNumber)) / 100;
        //                }
        //                catch
        //                {
        //                    fltPowerFactory = 0.00f;
        //                    blnIsSuccessful = false;
        //                }
        //                try
        //                {
        //                    dblActivePower = ((double)int.Parse(strResponse.Substring(14, 8), System.Globalization.NumberStyles.HexNumber)) / 100;
        //                }
        //                catch
        //                {
        //                    dblActivePower = 0.00;
        //                    blnIsSuccessful = false;
        //                }
        //                try
        //                {
        //                    dblApparentPower = ((double)int.Parse(strResponse.Substring(22, 8), System.Globalization.NumberStyles.HexNumber)) / 100;
        //                }
        //                catch
        //                {
        //                    dblApparentPower = 0.00;
        //                    blnIsSuccessful = false;
        //                }
        //                try
        //                {
        //                    dblMainEnergy = ((double)int.Parse(strResponse.Substring(30), System.Globalization.NumberStyles.HexNumber)) / 1000;
        //                }
        //                catch
        //                {
        //                    dblMainEnergy = 0.000;
        //                    blnIsSuccessful = false;
        //                }
        //            }
        //            else
        //            {
        //                blnIsSuccessful = false;
        //                fltVoltage = 0.00f;
        //                fltCurrent = 0.00f;
        //                fltFrequency = 0.00f;
        //                fltPowerFactory = 0.00f;
        //                dblActivePower = 0.00;
        //                dblApparentPower = 0.00;
        //                dblMainEnergy = 0.00;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //eventLog1.WriteEntry(ex.Message);
        //        blnIsSuccessful = false;
        //        fltVoltage = 0.00f;
        //        fltCurrent = 0.00f;
        //        fltFrequency = 0.00f;
        //        fltPowerFactory = 0.00f;
        //        dblActivePower = 0.00;
        //        dblApparentPower = 0.00;
        //        dblMainEnergy = 0.00;
        //    }
        //}

        public void RetrieveNodePowerInfo()
        {
            strResponse = "http://" + strGatewayIP + "/bscp.asp?BSCP=$01,CD," + strNodeID + ",0104,0702,01,01,FFFF,00,00,00,FFFF,0080";
            try
            {
                blnIsSuccessful = false;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = intTimeout; //5000 (5 seconds) for 3G/4G, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    blnSuccessfulInfo = false;
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    strPowerInfo = strResponse;
                    intIndex = strResponse.IndexOf("FFFF,00800041");
                    strResponse = strResponse.Substring(intIndex + 15);
                    intIndex = strResponse.IndexOf(",");
                    strResponse = strResponse.Substring(0, intIndex);

                    if (strResponse.Length == 42)
                    {
                        blnIsSuccessful = true;
                        try
                        {
                            fltVoltage = ((float)int.Parse(strResponse.Substring(0, 4), System.Globalization.NumberStyles.HexNumber)) / 100;
                            //Add on 5/8/2012
                            if (fltVoltage < 10.0f)
                                blnIsSuccessful = false;
                        }
                        catch
                        {
                            fltVoltage = 0.00f;
                            blnIsSuccessful = false;
                        }
                        try
                        {
                            fltCurrent = ((float)int.Parse(strResponse.Substring(4, 4), System.Globalization.NumberStyles.HexNumber)) / 100;
                        }
                        catch
                        {
                            fltCurrent = 0.00f;
                            blnIsSuccessful = false;
                        }
                        try
                        {
                            fltFrequency = ((float)int.Parse(strResponse.Substring(8, 4), System.Globalization.NumberStyles.HexNumber)) / 100;
                        }
                        catch
                        {
                            fltFrequency = 0.00f;
                            blnIsSuccessful = false;
                        }
                        try
                        {
                            fltPowerFactory = ((float)int.Parse(strResponse.Substring(12, 2), System.Globalization.NumberStyles.HexNumber)) / 100;
                        }
                        catch
                        {
                            fltPowerFactory = 0.00f;
                            blnIsSuccessful = false;
                        }
                        try
                        {
                            dblActivePower = ((double)int.Parse(strResponse.Substring(14, 8), System.Globalization.NumberStyles.HexNumber)) / 100;
                        }
                        catch
                        {
                            dblActivePower = 0.00;
                            blnIsSuccessful = false;
                        }
                        try
                        {
                            dblApparentPower = ((double)int.Parse(strResponse.Substring(22, 8), System.Globalization.NumberStyles.HexNumber)) / 100;
                        }
                        catch
                        {
                            dblApparentPower = 0.00;
                            blnIsSuccessful = false;
                        }
                        try
                        {
                            dblMainEnergy = ((double)int.Parse(strResponse.Substring(30), System.Globalization.NumberStyles.HexNumber)) / 1000;
                            //Add on 5/8/2012
                            if (dblMainEnergy <= 0.1f)
                                blnIsSuccessful = false;
                        }
                        catch
                        {
                            dblMainEnergy = 0.000;
                            blnIsSuccessful = false;
                        }
                        blnSuccessfulInfo = blnIsSuccessful;
                    }
                    else
                    {
                        blnSuccessfulInfo = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnSuccessfulInfo = false;
            }
        }

        //
        public void ControlRelay(bool blnRelayOnOff, int intNodeIndex)
        {
            try
            {
                blnIsSuccessful = false;
                string strRelayChannel = String.Format("{0:00}", intNodeIndex); //intNodeIndex.ToString("{0:00}");
                string strResponse;
                //Sync solution
                if (blnRelayOnOff)
                {
                    //Turn on
                    //http://164.67.192.249/ttyUSB0.asp?cmd=comdrely0301
                    strResponse = "http://" + strGatewayIP + "/ttyUSB0.asp?cmd=comdrely" + strRelayChannel + "01";
                }
                else
                {
                    //Turn off
                    strResponse = "http://" + strGatewayIP + "/ttyUSB0.asp?cmd=comdrely" + strRelayChannel + "00";
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = intTimeout; //5000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    blnSuccessfulControl = false;
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    //added on 1/23/2014
                    if (strResponse.Substring(0, 4) == "comd")
                        strResponse = strResponse.Substring(4);
                    if (strResponse.Length >= 32)
                    {
                        strDutyCycle = strResponse.Substring(6, 2);
                        strRelayOnOff = strResponse.Substring(14, 2);
                        strPlugIn = strResponse.Substring(22, 2);
                        strErrorCode = strResponse.Substring(30, 2);
                        strChannelNo = strResponse.Substring(12, 2);
                        if ((blnRelayOnOff && strRelayOnOff == "01") || (!blnRelayOnOff && strRelayOnOff == "00"))
                        {
                            blnIsSuccessful = true;
                            blnSuccessfulControl = true;
                        }
                        else
                        {
                            blnIsSuccessful = false;
                            blnSuccessfulControl = false;
                        }
                    }
                    else
                    {
                        blnIsSuccessful = false;
                        blnSuccessfulControl = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnSuccessfulControl = false;
            }
        }//End of ControlRelay

        //intDutyCycle is for percentage, range: 10~96
        //10~85: 6A~51A current=DutyCycle*0.6
        //86~96: 55A~80A current=(DutyCycle-64)*2.5
        public void ChangeDutyCycle(int intNodeIndex, int intDutyCycle)
        {
            try
            {
                blnIsSuccessful = false;
                string strRelayChannel = String.Format("{0:00}", intNodeIndex);
                string strDutyCyclePercentage = String.Format("{0:00}", intDutyCycle);
                string strResponse;

                strResponse = "http://" + strGatewayIP + "/ttyUSB0.asp?cmd=comdduty" + strRelayChannel + strDutyCyclePercentage;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = intTimeout; //5000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    blnSuccessfulControl = false;
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    //added on 1/23/2014
                    if (strResponse.Substring(0, 4) == "comd")
                        strResponse = strResponse.Substring(4);
                    if (strResponse.Length >= 32)
                    {
                        intDutyCycleReturnGroupNo = strResponse.Length / DUTY_CYCLE_RETURN_LENGTH - 1;
                        strDutyCycle = strResponse.Substring(6 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strRelayOnOff = strResponse.Substring(14 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strPlugIn = strResponse.Substring(22 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strErrorCode = strResponse.Substring(30 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strChannelNo = strResponse.Substring(12 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        if (strDutyCycle == strDutyCyclePercentage)
                        {
                            blnIsSuccessful = true;
                            blnSuccessfulControl = true;
                        }
                        else
                        {
                            blnIsSuccessful = false;
                            blnSuccessfulControl = false;
                        }
                    }
                    else
                    {
                        blnIsSuccessful = false;
                        blnSuccessfulControl = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnSuccessfulControl = false;
            }
        }//End of ChangeDutyCycle

        //intDutyCycle is for percentage, range: 10~96
        //10~85: 6A~51A current=DutyCycle*0.6
        //86~96: 55A~80A current=(DutyCycle-64)*2.5
        public void StartCharging(int intNodeIndex)
        {
            try
            {
                blnIsSuccessful = false;
                string strRelayChannel = String.Format("{0:00}", intNodeIndex);
                //string strDutyCyclePercentage = String.Format("{0:00}", intDutyCycle);
                string strResponse;

                strResponse = "http://" + strGatewayIP + "/ttyUSB0.asp?cmd=comdenab" + strRelayChannel + "00";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = intTimeout; //5000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    blnSuccessfulControl = false;
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    //added on 1/23/2014
                    if (strResponse.Substring(0, 4) == "comd")
                        strResponse = strResponse.Substring(4);
                    if (strResponse.Length >= 32)
                    {
                        //strDutyCycle = strResponse.Substring(6, 2);
                        //strRelayOnOff = strResponse.Substring(14, 2);
                        //strPlugIn = strResponse.Substring(22, 2);
                        //strErrorCode = strResponse.Substring(30, 2);
                        //strChannelNo = strResponse.Substring(12, 2);
                        intDutyCycleReturnGroupNo = strResponse.Length / DUTY_CYCLE_RETURN_LENGTH - 1;
                        strDutyCycle = strResponse.Substring(6 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strRelayOnOff = strResponse.Substring(14 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strPlugIn = strResponse.Substring(22 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strErrorCode = strResponse.Substring(30 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strChannelNo = strResponse.Substring(12 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);

                        blnIsSuccessful = true;
                        blnSuccessfulControl = true;

                        //if (strDutyCycle == strDutyCyclePercentage)
                        //{
                        //    blnIsSuccessful = true;
                        //    blnSuccessfulControl = true;
                        //}
                        //else
                        //{
                        //    blnIsSuccessful = false;
                        //    blnSuccessfulControl = false;
                        //}
                    }
                    else
                    {
                        blnIsSuccessful = false;
                        blnSuccessfulControl = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnSuccessfulControl = false;
            }
        }//End of StartCharging

        //intDutyCycle is for percentage, range: 10~96
        //10~85: 6A~51A current=DutyCycle*0.6
        //86~96: 55A~80A current=(DutyCycle-64)*2.5
        public void StopCharging(int intNodeIndex)
        {
            try
            {
                blnIsSuccessful = false;
                string strRelayChannel = String.Format("{0:00}", intNodeIndex);
                //string strDutyCyclePercentage = String.Format("{0:00}", intDutyCycle);
                string strResponse;

                strResponse = "http://" + strGatewayIP + "/ttyUSB0.asp?cmd=comdrest" + strRelayChannel + "00";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = intTimeout; //5000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    blnSuccessfulControl = false;
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    //added on 1/23/2014
                    if (strResponse.Substring(0, 4) == "comd")
                        strResponse = strResponse.Substring(4);
                    if (strResponse.Length >= 32)
                    {
                        //strDutyCycle = strResponse.Substring(6, 2);
                        //strRelayOnOff = strResponse.Substring(14, 2);
                        //strPlugIn = strResponse.Substring(22, 2);
                        //strErrorCode = strResponse.Substring(30, 2);
                        //strChannelNo = strResponse.Substring(12, 2);
                        //blnIsSuccessful = true;
                        //blnSuccessfulControl = true;
                        intDutyCycleReturnGroupNo = strResponse.Length / DUTY_CYCLE_RETURN_LENGTH - 1;
                        strDutyCycle = strResponse.Substring(6 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strRelayOnOff = strResponse.Substring(14 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strPlugIn = strResponse.Substring(22 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strErrorCode = strResponse.Substring(30 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strChannelNo = strResponse.Substring(12 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);


                        if (strDutyCycle == "00")
                        {
                            blnIsSuccessful = true;
                            blnSuccessfulControl = true;
                        }
                        else
                        {
                            blnIsSuccessful = false;
                            blnSuccessfulControl = false;
                        }
                    }
                    else
                    {
                        blnIsSuccessful = false;
                        blnSuccessfulControl = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnSuccessfulControl = false;
            }
        }//End of StopCharging



        public void ResetDutyCycle(int intNodeIndex)
        {
            try
            {
                blnIsSuccessful = false;
                string strRelayChannel = String.Format("{0:00}", intNodeIndex); //intNodeIndex.ToString("{0:00}");
                string strResponse;

                strResponse = "http://" + strGatewayIP + "/ttyUSB0.asp?cmd=comdrest" + strRelayChannel + "00";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = intTimeout; //5000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    blnSuccessfulControl = false;
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    //added on 1/23/2014
                    if (strResponse.Substring(0, 4) == "comd")
                        strResponse = strResponse.Substring(4);
                    if (strResponse.Length >= 32)
                    {
                        //strDutyCycle = strResponse.Substring(6, 2);
                        //strRelayOnOff = strResponse.Substring(14, 2);
                        //strPlugIn = strResponse.Substring(22, 2);
                        //strErrorCode = strResponse.Substring(30, 2);
                        //strChannelNo = strResponse.Substring(12, 2);
                        intDutyCycleReturnGroupNo = strResponse.Length / DUTY_CYCLE_RETURN_LENGTH - 1;
                        strDutyCycle = strResponse.Substring(6 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strRelayOnOff = strResponse.Substring(14 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strPlugIn = strResponse.Substring(22 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strErrorCode = strResponse.Substring(30 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strChannelNo = strResponse.Substring(12 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);

                        if (strDutyCycle == "25")
                        {
                            blnIsSuccessful = true;
                            blnSuccessfulControl = true;
                        }
                        else
                        {
                            blnIsSuccessful = false;
                            blnSuccessfulControl = false;
                        }
                    }
                    else
                    {
                        blnIsSuccessful = false;
                        blnSuccessfulControl = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnSuccessfulControl = false;
            }
        }//End of Reset

        public void RelayStatus(int intNodeIndex)
        {
            try
            {
                blnIsSuccessful = false;
                string strRelayChannel = String.Format("{0:00}", intNodeIndex); //intNodeIndex.ToString("{0:00}");
                string strResponse;

                strResponse = "http://" + strGatewayIP + "/ttyUSB0.asp?cmd=comdstat" + strRelayChannel + "00";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = intTimeout; //7000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    blnSuccessfulControl = false;
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    //added on 1/23/2014
                    if (strResponse.Substring(0, 4) == "comd")
                        strResponse = strResponse.Substring(4);
                    if (strResponse.Length >= 32)
                    {
                        //strDutyCycle = strResponse.Substring(6, 2);
                        //strRelayOnOff = strResponse.Substring(14, 2);
                        //strPlugIn = strResponse.Substring(22, 2);
                        //strErrorCode = strResponse.Substring(30, 2);
                        //strChannelNo = strResponse.Substring(12, 2);
                        intDutyCycleReturnGroupNo = strResponse.Length / DUTY_CYCLE_RETURN_LENGTH - 1;
                        strDutyCycle = strResponse.Substring(6 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strRelayOnOff = strResponse.Substring(14 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strPlugIn = strResponse.Substring(22 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strErrorCode = strResponse.Substring(30 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);
                        strChannelNo = strResponse.Substring(12 + intDutyCycleReturnGroupNo * DUTY_CYCLE_RETURN_LENGTH, 2);

                        //blnIsSuccessful = true;
                        //blnSuccessfulControl = true;

                        if (strChannelNo == strRelayChannel)
                        {
                            blnIsSuccessful = true;
                            blnSuccessfulControl = true;
                        }
                        else
                        {
                            blnIsSuccessful = false;
                            blnSuccessfulControl = false;
                        }
                    }
                    else
                    {
                        blnIsSuccessful = false;
                        blnSuccessfulControl = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnSuccessfulControl = false;
            }
        }//End of RelayStatus

        //Added on 1/23/2013
        public void StateOfCharging(string strMacAddress)
        {
            try
            {
                blnIsSuccessful = false;
                string strResponse;

                strResponse = "http://" + strGatewayIP + "/ttyUSB0.asp?cmd=comdsoch" + strMacAddress + "00";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strResponse);

                //request.Method = "POST";
                request.Timeout = intTimeout; //7000 (7 seconds) for cellphone, it can be more longer

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    blnSuccessfulControl = false;
                }
                else
                {
                    //successful
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    strResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    if (strResponse.Length >= 22)
                    {
                        string strReturnMacAddress = strResponse.Substring(4, 16);
                        strErrorCode = strResponse.Substring(20, 2);
                        if (strErrorCode != "EE"&&strReturnMacAddress==strMacAddress)
                        {
                            strStateOfCharging = (int.Parse(strErrorCode)).ToString() + "%";
                            blnIsSuccessful = true;
                            blnSuccessfulControl = true;
                        }
                        else
                        {
                            strStateOfCharging = "Unavailable";
                            blnIsSuccessful = false;
                            blnSuccessfulControl = false;
                        }
                    }
                    else
                    {
                        strStateOfCharging = "Fail to retrieve";
                        blnIsSuccessful = false;
                        blnSuccessfulControl = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnSuccessfulControl = false;
            }
        }//End of StateOfCharging

    }//End of ZigBeeNode
}
