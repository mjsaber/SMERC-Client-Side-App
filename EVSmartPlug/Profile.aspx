<%@ Page Title="Update Profile" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Profile.aspx.cs" Inherits="EVUser.Account.Profile" EnableEventValidation="false"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
.button_green{
border:0px solid #5c7a2d; -webkit-border-radius: 10px; -moz-border-radius: 10px;border-radius: 10px;width:auto;font-size:48px;font-family:arial, helvetica, sans-serif; padding: 15px 15px 15px 15px; font-weight:bold; text-align: center; color: #FFFFFF; background-color: #7BA33C;
background: #7ba33c; /* Old browsers */
background: -moz-linear-gradient(top,  #7ba33c 0%, #7ba33c 50%, #6a8936 51%, #7ba23c 100%); /* FF3.6+ */
background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#7ba33c), color-stop(50%,#7ba33c), color-stop(51%,#6a8936), color-stop(100%,#7ba23c)); /* Chrome,Safari4+ */
background: -webkit-linear-gradient(top,  #7ba33c 0%,#7ba33c 50%,#6a8936 51%,#7ba23c 100%); /* Chrome10+,Safari5.1+ */
background: -o-linear-gradient(top,  #7ba33c 0%,#7ba33c 50%,#6a8936 51%,#7ba23c 100%); /* Opera 11.10+ */
background: -ms-linear-gradient(top,  #7ba33c 0%,#7ba33c 50%,#6a8936 51%,#7ba23c 100%); /* IE10+ */
background: linear-gradient(to bottom,  #7ba33c 0%,#7ba33c 50%,#6a8936 51%,#7ba23c 100%); /* W3C */
filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#7ba33c', endColorstr='#7ba23c',GradientType=0 ); /* IE6-9 */
}

.stationlistbig	{

	font-size:2.5em;
	font-weight:bold;	
	color:#999;
}

.button_green:hover{
border:0px solid #5c7a2d; -webkit-border-radius: 10px; -moz-border-radius: 10px;border-radius: 10px;width:auto;font-size:48px;font-family:arial, helvetica, sans-serif; padding: 15px 15px 15px 15px; font-weight:bold; text-align: center; color: #FFFFFF; background-color: #7BA33C;
background: #7ba23c; /* Old browsers */
background: -moz-linear-gradient(top,  #7ba23c 0%, #6a8936 50%, #7ba33c 51%, #7ba33c 100%); /* FF3.6+ */
background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#7ba23c), color-stop(50%,#6a8936), color-stop(51%,#7ba33c), color-stop(100%,#7ba33c)); /* Chrome,Safari4+ */
background: -webkit-linear-gradient(top,  #7ba23c 0%,#6a8936 50%,#7ba33c 51%,#7ba33c 100%); /* Chrome10+,Safari5.1+ */
background: -o-linear-gradient(top,  #7ba23c 0%,#6a8936 50%,#7ba33c 51%,#7ba33c 100%); /* Opera 11.10+ */
background: -ms-linear-gradient(top,  #7ba23c 0%,#6a8936 50%,#7ba33c 51%,#7ba33c 100%); /* IE10+ */
background: linear-gradient(to bottom,  #7ba23c 0%,#6a8936 50%,#7ba33c 51%,#7ba33c 100%); /* W3C */
filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#7ba23c', endColorstr='#7ba33c',GradientType=0 ); /* IE6-9 */
}


.button_green:disabled{
border:0px solid #5c7a2d; -webkit-border-radius: 10px; -moz-border-radius: 10px;border-radius: 10px;width:auto;font-size:48px;font-family:arial, helvetica, sans-serif; padding: 15px 15px 15px 15px; font-weight:bold; text-align: center; color: #FFFFFF; background-color: #7BA33C;
background: #e6e6e6; /* Old browsers */
background: -moz-linear-gradient(top,  #e6e6e6 0%, #e6e6e6 50%, #c0bfbf 51%, #e4e4e4 100%); /* FF3.6+ */
background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#e6e6e6), color-stop(50%,#e6e6e6), color-stop(51%,#c0bfbf), color-stop(100%,#e4e4e4)); /* Chrome,Safari4+ */
background: -webkit-linear-gradient(top,  #e6e6e6 0%,#e6e6e6 50%,#c0bfbf 51%,#e4e4e4 100%); /* Chrome10+,Safari5.1+ */
background: -o-linear-gradient(top,  #e6e6e6 0%,#e6e6e6 50%,#c0bfbf 51%,#e4e4e4 100%); /* Opera 11.10+ */
background: -ms-linear-gradient(top,  #e6e6e6 0%,#e6e6e6 50%,#c0bfbf 51%,#e4e4e4 100%); /* IE10+ */
background: linear-gradient(to bottom,  #e6e6e6 0%,#e6e6e6 50%,#c0bfbf 51%,#e4e4e4 100%); /* W3C */
filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#e6e6e6', endColorstr='#e4e4e4',GradientType=0 ); /* IE6-9 */
}         
        .auto-style1
        {
            height: 73px;
        }
            .button_o
            {
                -webkit-appearance: none;
                border:0px solid #5c7a2d; -webkit-border-radius: 75px; -moz-border-radius: 75px;border-radius: 75px;width:auto;font-size:48px;font-family:arial, helvetica, sans-serif; padding: 15px 15px 15px 15px; font-weight:bold; text-align: center; color: #FFFFFF; background-color: #f59630;
                background: #f59630; /* Old browsers */
            }
            .button_o:disabled{
                -webkit-appearance: none;
            border:0px solid #5c7a2d; -webkit-border-radius: 75px; -moz-border-radius: 75px;border-radius: 75px;width:auto;font-size:48px;font-family:arial, helvetica, sans-serif; padding: 15px 15px 15px 15px; font-weight:bold; text-align: center; color: #FFFFFF; background-color: #7BA33C;
            background: #e6e6e6; /* Old browsers */
            }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/icon_page_profile.png" />
        <asp:Label ID="lblError" runat="server" Text="" style="color: #FF0000"></asp:Label>
        <asp:Button ID="btnHideError" runat="server" Text="Hide Error" Visible="false" OnClick="btnHideError_Click" />
        
        <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
        
    </h2>
     <table align="center" width="90%">

        <%--Profile Data Table--%>
        <tr align="center">
            <td align="center">
                <fieldset>
                    <legend style="font-family: Arial; font-size: xx-large; font-weight: bold; color: #000000">Account Information</legend>
                        <table style="font-size: xx-large; width: 100%;">
                        <tr>
                            <td style="text-align: left; font-weight: 700;" class="auto-style1">Total Energy Consumed</td>
                            <td style="text-align: right" class="auto-style1">
                                <asp:Label ID="lblEnergyConsumed" runat="server" Text="" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>

                        <tr style="background-color: #F0F0F0;">
                            <td style="text-align: left; font-weight: 700;">
                                Total CO<sub>2</sub> 
                                Reduced</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblCO2Reduced" runat="server" Text="" style="font-weight: 700"></asp:Label>
                                </td>
                        </tr>

<%--                        <tr>
                            <td style="text-align: left; font-weight: 700;">
                                Total Charging Cost</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblChargingCost" runat="server" Text="" style="font-weight: 700"></asp:Label>
                            </td>

                        </tr>--%>

                            <tr>
                            <td style="text-align: left; font-weight: 700;">
                                State of Charge Available</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblAccountInfoSOCAvailable" runat="server" Text="" style="font-weight: 700"></asp:Label>
                            </td>

                        </tr>

                       <tr style="background-color: #F0F0F0;">
                            <td style="text-align: left; font-weight: 700;">
                                Organization-Role</td>
                            <td style="text-align: right">
                                <asp:Label ID="lblAccountInfoRole" runat="server" Text="" style="font-weight: 700"></asp:Label>
                            </td>

                        </tr>

                        

                        </table>  
                 </fieldset>
            </td>
        </tr>


         <%--Start Profile Update Section--%>
        <tr align="center">
            <td align="center">

                <fieldset>
                    <legend style="font-family: Arial; font-size: xx-large; font-weight: bold; color: #000000">Update Profile</legend>
                        <table align="center" width ="100%">

                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">&nbsp;</td>
                                <td style="width: 80%; text-align: left;">     <asp:Label ID="lblSuccessOrFailure" runat="server" Font-Bold="True" Font-Size="xx-large" Text=""></asp:Label>
                                    
                                <br />                                                                                             
                                <td></td>
                            </tr>

                            <%--Desired Username--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                     <asp:Label ID="lblUsername" runat="server" AssociatedControlID="lblUsername" Font-Bold="True" CssClass="style2" Font-Size="xx-large">Username</asp:Label>                           
                                     <asp:TextBox ID="tbUsername" runat="server" CssClass="inputstyle" Width="100%" ReadOnly="True"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">
                                    &nbsp;</td>
                            </tr>                   

                            <%--Password Question--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblPasswordQuestion" runat="server" AssociatedControlID="lblPasswordQuestion" Font-Bold="True" Font-Size="xx-large">Password Question</asp:Label>
                                    <asp:TextBox ID="tbPasswordQuestion" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">       
                                    
                                </td>
                            </tr>

                            <%--Password Answer--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblPasswordAnswer" runat="server" AssociatedControlID="lblPasswordAnswer" Font-Bold="True" Font-Size="xx-large">Password Answer</asp:Label>
                                    &nbsp;<strong style="font-size: x-large">&nbsp; *Hidden for privacy</strong>
                                    <asp:TextBox ID="tbPasswordAnswer" runat="server" CssClass="inputstyle" MaxLength="50"  Width="100%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">    </td>
                            </tr>                       

                            
                            <%--Email--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="lblEmail" Font-Bold="True" Font-Size="xx-large">Email</asp:Label>
                                    <asp:TextBox ID="tbEmail" runat="server" CssClass="inputstyle" Width="100%"></asp:TextBox>      
                                </td>


                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbEmail" runat="server" ControlToValidate="tbEmail" ErrorMessage="Required." Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>                              
                                    <br />
                                    <asp:RegularExpressionValidator ID="reqAuthEmail" RunAt="server" ControlToValidate="tbEmail" ErrorMessage="Format Error." ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$" />                                
                                    &nbsp;<br />
                                </td>
                            </tr>

                            <%--First Name--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="lblFirstName" Font-Bold="True" Font-Size="xx-large">First Name</asp:Label>
                                    <asp:TextBox ID="tbFirstName" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>      
                                </td>
                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbFirstName" runat="server" ControlToValidate="tbFirstName" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                              
                                </td>
                            </tr>
                            
                            <%--Last Name--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblLastName" runat="server" AssociatedControlID="lblLastName" Font-Bold="True" Font-Size="xx-large">Last Name</asp:Label>
                                    <asp:TextBox ID="tbLastName" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbLastName" runat="server" ControlToValidate="tbLastName" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                                                              

                                </td>
                            </tr>
                        
                            <%--Phone Number--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left; font-size: large;">         
                                    <asp:Label ID="lblPhoneNumber" runat="server" AssociatedControlID="lblPhoneNumber" Font-Bold="True" Font-Size="xx-large">Phone Number</asp:Label>
                                    &nbsp;<strong style="font-size: x-large">*Example&nbsp;Format: 310-267-6979</strong><asp:TextBox ID="tbPhoneNumber" runat="server" MaxLength="20"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbPhoneNumber" runat="server" ControlToValidate="tbPhoneNumber" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator> 
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbPhoneNumber" ErrorMessage="Format Error." ValidationExpression="^[2-9]\d{2}-\d{3}-\d{4}$" ForeColor="Red" />         
                                </td>
                            </tr>

                            <%--Address 1--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblAddress1" runat="server" AssociatedControlID="lblAddress1" Font-Bold="True" Font-Size="xx-large">Address 1</asp:Label>
                                    <asp:TextBox ID="tbAddress1" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbAddress1" runat="server" ControlToValidate="tbAddress1" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>          
                                </td>
                            </tr>

                            <%--Address 2 (Not required)--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblAddress2" runat="server" AssociatedControlID="lblAddress2" Font-Bold="True" Font-Size="xx-large">Address 2</asp:Label>
                                    <asp:TextBox ID="tbAddress2" runat="server" CssClass="inputstyle" MaxLength="50"  Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">                                          
                                </td>
                            </tr>

                            <%--Zip Code--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="lblZipCode" Font-Bold="True" Font-Size="xx-large">Zip Code</asp:Label>
                                    <asp:TextBox ID="tbZipCode" runat="server" CssClass="inputstyle" MaxLength="50"  Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">          
                                    <asp:RequiredFieldValidator ID="rfltbZipCode" runat="server" ControlToValidate="tbZipCode" SetFocusOnError="true" ErrorMessage="Required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                          
                                &nbsp;<br />
                                    
                                    <asp:RegularExpressionValidator ID="RegExp1" runat="server"  ErrorMessage="Format Error"  ControlToValidate="tbZipCode" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{5,10}$" ForeColor="Red" />
                                </td>
                            </tr>

                            <%--City--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblCity" runat="server" AssociatedControlID="lblCity" Font-Bold="True" Font-Size="xx-large">City</asp:Label>
                                    <asp:TextBox ID="tbCity" runat="server" CssClass="inputstyle" MaxLength="50"  Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">          
                                    <asp:RequiredFieldValidator ID="rfltbCity" runat="server" ControlToValidate="tbCity" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                                          
                                </td>
                            </tr>

                            <%--State--%>
                            <tr align="center"  style="font-size: 130%" class="style2">
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; font-size: xx-large;  text-align: left;">    
                                    <asp:Label ID="lblState" runat="server" AssociatedControlID="lblState" Font-Bold="True" Font-Size="xx-large">State</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlState" runat="server" Font-Size="130%" Font-Bold="True" Width="100%" CssClass="stationlistbig" ></asp:DropDownList>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">          
                                    <asp:RequiredFieldValidator ID="rflddlState" runat="server" ControlToValidate="ddlState" SetFocusOnError="true" InitialValue="-1" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                                          
                                </td>
                            </tr>

                             <%--EV Model--%>
                            <tr  align="center" style="font-size: 130%" class="style2">
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; font-size: xx-large; text-align: left;">         
                                    <asp:Label ID="lblEVModel" runat="server" AssociatedControlID="lblEVModel" Font-Bold="True" Font-Size="xx-large" Visible="False">EV Model</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlEVModel" runat="server"  Font-Bold="True"  Font-Size="130%"  Width="100%" CssClass="stationlistbig" Enabled="false" Visible="False"></asp:DropDownList>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">          
                                    <asp:RequiredFieldValidator ID="rflddlEVModel" runat="server" ControlToValidate="ddlEVModel" SetFocusOnError="true" InitialValue="-1" ErrorMessage="Required." ForeColor="Red" Visible="False"></asp:RequiredFieldValidator>                                          
                                </td>
                            </tr>

                              

                            <%--Smart Phone OS--%>
                            <tr  align="center" style="font-size: 130%" class="style2">
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; font-size: xx-large; text-align: left;">         
                                    <asp:Label ID="lblSmartPhoneOS" runat="server" AssociatedControlID="lblSmartPhoneOS" Font-Bold="True" Font-Size="xx-large">Smart Phone OS</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlSmartPhoneOS" runat="server" Font-Bold="True" Font-Size="130%" Width="100%" CssClass="stationlistbig">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">   
                                </td>
                            </tr>

                            <%--Phone Service Carrier--%>
                            <tr  align="center" style="font-size: 130%" class="style2">
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; font-size: xx-large; text-align: left;">         
                                    <asp:Label ID="lblPhoneServiceCarrier" runat="server" AssociatedControlID="lblPhoneServiceCarrier" Font-Bold="True" Font-Size="xx-large">Phone Service Carrier</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlPhoneServiceCarrier" runat="server" Font-Bold="True" Font-Size="130%" Width="100%" CssClass="stationlistbig">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">   
                                </td>
                            </tr>

                            <%--Smart Phone Model No.--%>
                            <tr >
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblSmartPhoneModel" runat="server" AssociatedControlID="lblSmartPhoneModel" Font-Bold="True" Font-Size="xx-large">Smart Phone Model</asp:Label>
                                    <asp:TextBox ID="tbSmartPhoneModel" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">         
                                    
                                </td>
                            </tr>

                            <%--Social Media ID--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; font-size: xx-large; text-align: left;">          
                                    <asp:Label ID="lblSocialMediaID" runat="server" AssociatedControlID="lblSocialMediaID" Font-Bold="True" Font-Size="xx-large">Social Media</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlSocialMediaID" runat="server" Font-Bold="True" Font-Size="130%" Width="100%" CssClass="stationlistbig" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">         
                                    
                                </td>
                            </tr>

                            <%-- Social Media Account Name --%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblSocialMediaAccountName" runat="server" AssociatedControlID="lblSocialMediaAccountName" Font-Bold="True" Font-Size="xx-large">Social Media Account Name</asp:Label>
                                    <asp:TextBox ID="tbSocialMediaAccName" runat="server" CssClass="inputstyle" MaxLength="50" Width="100%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">         
                                    
                                </td>
                            </tr>
                            
                            <%--MaximumVehicles--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblMaximumVehicles" runat="server" AssociatedControlID="lblMaximumVehicles" Font-Bold="True" Font-Size="xx-large" Enabled="False">Maximum Vehicles</asp:Label>
                                    <asp:TextBox ID="tbMaximumVehicles" runat="server" CssClass="inputstyle" MaxLength="50" Width="100%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">         
                                    
                                </td>
                            </tr>
                            
                            <%--GvEvList--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                
                                <td style="width: 80%; text-align: left;"> 
                                          
                                    <asp:Label runat="server" Font-Bold="True" Font-Size="xx-large">EV List</asp:Label>
                                    <br/>
                                    <div style ="height:200px; overflow: auto;">  
                                    <asp:GridView ID="GvEvList" runat="server" CssClass="datatable" 
                                    GridLines="Vertical" OnRowCreated="GvEvListRowCreated"
                                    onselectedindexchanged="GvEvListSelectedIndexChanged"
                                    OnRowDataBound="GvEvListDataBound"
                                    CellPadding="0" CellSpacing="0" BorderWidth="1" AutoGenerateColumns="False"
                                    DataKeyNames="ID" Width="55%" Font-Size ="xx-large">
                                    <AlternatingRowStyle Wrap="False" />
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID"/> 
                                        <asp:BoundField DataField="EvModelID" HeaderText="EvModelID"/>
                                        <asp:BoundField DataField="Number" HeaderText="No." />
                                        <asp:BoundField DataField="EvName" HeaderText="EV Name"/> 
                                        <asp:BoundField DataField="Nickname" HeaderText="Nickname"/>
                                    </Columns>
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle Wrap="False" />
                                    <RowStyle CssClass="row" Wrap="False" />
                                    <SelectedRowStyle BackColor="#38CDB9" Font-Bold="True" ForeColor="White" Wrap="False" /> 
                                     </asp:GridView>
                                    </div>
                                    <br/>
                                        <asp:DropDownList ID="ddlGvEvListModel" runat="server" Visible="false" Font-Size="xx-large"
                                              style="margin-bottom: 0px">
                                        </asp:DropDownList>
                                    <br/>
                                        <asp:TextBox ID="tbNickname" runat="server" Visible="false" Font-Size="xx-large" style="margin-bottom: 0px"></asp:TextBox>
                                    <br/>    
                                        <asp:Button ID="btnGvEvListAdd" runat="server" Text="add" onclick="btnGvEvListAddClick" Font-Size="xx-large" />               
                                        <asp:Button ID="btnGvEvListModify" runat="server" Font-Size="xx-large" Text="modify" 
                                            Visible="false" onclick="btnGvEvListModifyClick"/>    
                                        <asp:Button ID="btnGvEvListDelete" runat="server" Font-Size="xx-large" Text="delete" 
                                            Visible="false" onclick="btnGvEvListDeleteClick" CausesValidation="False"/>
                                </td>
                            </tr>
                    </table>
                </fieldset>


                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="button_green" OnClick="btnUpdateProfile_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel" CssClass="button_green" OnClick="btnCancel_Click"/>
                        </td>
                    </tr>
               </table>
                
            </td>
        </tr>
    </table>
</asp:Content>
