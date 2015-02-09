<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EVUser.Account.Login" Title="EVSmartPlug" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<style type="text/css">
    .gradient {
       filter: none;
    }    
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
margin-left:20px;
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
margin-left:20px;
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
margin-left:20px;
} 

.wrapperlogin 
{
        margin-left:10px;
        margin-right:10px;
        width: auto;
        background-color:#919192;
        -moz-border-bottom-left-radius: 10px;
        -webkit-border-bottom-right-radius: 10px;
        -moz-border-bottom-left-radius: 10px;
        -webkit-border-bottom-right-radius: 10px;
        border-bottom-left-radius:10px;
        border-bottom-right-radius:10px;        
}

.login{
	font-family:Arial, Helvetica, sans-serif;
	font-size:3.5em;
	background:url(LoginButton.png);
	color:#000;
	margin-right:50px;
	font-weight:bold;
	height:96px;
	width:186px;
	line-height:90px;
	margin-bottom:40px;
	text-decoration:none;
	border-radius:10px;
	-moz-border-radius: 10px;
	-webkit-border-radius: 10px;
	text-align:center;
}

.loginbutton {
	-moz-box-shadow:inset 0px 1px 0px 0px #ffffff;
	-webkit-box-shadow:inset 0px 1px 0px 0px #ffffff;
	box-shadow:inset 0px 1px 0px 0px #ffffff;
	    background-color: #ededed;
        -moz-border-radius: 6px;
        -webkit-border-radius: 6px;
        border-radius: 6px;
        border: 1px solid #dcdcdc;
        display: inline-block;
        color: #777777;
        font-family: arial;
        font-size: 40px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 1px 1px 0px #ffffff;

	    margin-bottom:40px;
	    margin-right:50px;

    }
.loginbutton:hover {
	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #dfdfdf), color-stop(1, #ededed) );
	background:-moz-linear-gradient( center top, #dfdfdf 5%, #ededed 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#dfdfdf', endColorstr='#ededed');
	background-color:#dfdfdf;
}

.loginbutton {
	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #dfdfdf), color-stop(1, #ededed) );
	background:-moz-linear-gradient( center top, #dfdfdf 5%, #ededed 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#dfdfdf', endColorstr='#ededed');
	background-color:#dfdfdf;
}
.loginbutton:active {
	position:relative;
	top:1px;
}

body,td,th {
	font-size: 12px;
	font-family: Arial, Helvetica, sans-serif;
	color: #ffffff;
	margin: 0; 
	padding: 0
}

.wrapper 
{
    top:0px;
	width: auto; 
	margin: 0 auto; 
	background-color: #ffffff; }

.field	
{
    margin-left:20px;
    margin-right:20px;
	display:block;
	width:90%;
	font-size:4em;
	border-radius:10px;
	-moz-border-radius: 10px;
	-webkit-border-radius: 10px;
	font-weight:bold;
}

.rememberme input
{
    width:3.5em;
    height:3.5em;
    vertical-align:middle;
    margin-left:20px;

}
    .style1
    {
        height: 10px;
    }
    .style2
    {
        margin-left:20px;
    }
    
    .page
    {
        width: 960px;
        background-color: #fff;
        margin: 20px auto 0px auto;
    }

            .button_o
            {
                -webkit-appearance: none;
                border:0px solid #5c7a2d; -webkit-border-radius: 75px; -moz-border-radius: 75px;border-radius: 75px;width:auto;font-size:48px;font-family:arial, helvetica, sans-serif; padding: 15px 15px 15px 15px; font-weight:bold; text-align: center; color: #FFFFFF; background-color: #f59630;
                background: #f59630; /* Old browsers */
                	margin-bottom:40px;
                    	    margin-right:50px;
            }
            .button_o:disabled{
                -webkit-appearance: none;
            border:0px solid #5c7a2d; -webkit-border-radius: 75px; -moz-border-radius: 75px;border-radius: 75px;width:auto;font-size:48px;font-family:arial, helvetica, sans-serif; padding: 15px 15px 15px 15px; font-weight:bold; text-align: center; color: #FFFFFF; background-color: #7BA33C;
            background: #e6e6e6; /* Old browsers */
            	margin-bottom:40px;
                	    margin-right:50px;
            }
</style>

<head id="Head1" runat="server">
<%--    <link rel="apple-touch-icon-precomposed" href="apple-touch-icon-157x157.png"/>--%>
    <%--<link rel="apple-touch-icon-precomposed" href="http://wireless3.seas.ucla.edu/evuser/Account/apple-touch-icon-precomposed.png"/>--%>
</head>
<body class="page">
        <form id="Form1" runat="server" class="wrapperlogin">
            <asp:Login ID="Login1" runat="server" BorderColor="#E6E2D8" 
                        BorderPadding="4" BorderStyle="None" BorderWidth="1px" Font-Bold="True" 
                         ForeColor="#333333" 
                        PasswordLabelText="Password" RememberMeText="Remember me next time" 
                        TitleText="Login to SMERC User" UserNameLabelText="Login" Width="100%" OnLoggedIn="Login1_LoggedIn" 
>
                <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                <LayoutTemplate>
                <table style="border-collapse:collapse; font-size: xx-large; font-weight: bolder;" width="100%" align="center">
                    <tr>
<%--                            <td align="center" colspan="2" style="background-color:#7ba33c" >
                                <img id="imgTitle" src="login_top_new.png" alt="EVUser"/>--%>
                            <td align="center" colspan="2" style="background-color:#7ba33c" >
<%--                                <img id="imgTitle" runat="server" src="login_top_moev.png" alt="EVUser"/>--%>
                                <asp:Image ID="imgTitle" runat="server" ImageUrl="~/Account/login_top_new.png" AlternateText="EVUser" />

                            </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" style="background-color:#ffffff" class="style1">
                         &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="style1">
                         &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="style1">
                         &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="style1">
                         &nbsp;
                        </td>
                    </tr>                    
                    <tr>
                        <td colspan="2" align="left" style="font-size: 100%;">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="style2">Username</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="UserName" runat="server" CssClass="field" /> 
                        </td>
                        <td style="font-size: 100%; font-weight: bolder" width="10px">
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="style1">
                         &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="style1">
                         &nbsp;
                        </td>
                    </tr>
                    <tr>
                            <td colspan="2" align="left" style="font-size: 100%;">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="style2">Password</asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td>
                                <asp:TextBox ID="Password" runat="server" 
                                    TextMode="Password" CssClass="field" />
                        </td>
                            <td style="font-size: 100%;" width="10px">
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Password is required." 
                                    ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr>
                        <td colspan="2" align="left" class="style1">
                         &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="style1">
                         &nbsp;
                        </td>
                    </tr>
                        <tr>
                            <td align="left" colspan="2" style="font-size: 100%;">
                                <asp:CheckBox ID="RememberMe" runat="server" Checked="True"
                                     Font-Bold="True" Text="Remember me" Visible="True" 
                                    CssClass="rememberme" Font-Names="Arial" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="font-size: 60%; color: #FF0000;">
                                Do not check if on a public computer or device
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 100%;">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="font-size: 100%;">
                                <asp:HyperLink ID="HyperLink3" runat="server" 
                                    NavigateUrl="~/Account/ForgotUsername.aspx" ForeColor="White" CssClass="style2">Forgot username</asp:HyperLink>
                            </td>
                        </tr>

                        <tr>
                            <td align="left" colspan="2" style="font-size: 100%;">
                                <asp:HyperLink ID="HyperLink1" runat="server" 
                                    NavigateUrl="~/Account/ResetPassword.aspx" ForeColor="White" CssClass="style2">Forgot password</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="font-size: 200%;">
                                <asp:HyperLink ID="HyperLink2" runat="server" 
                                    NavigateUrl="~/Account/GuestMode.aspx" ForeColor="Yellow" 
                                    CssClass="style2">Guest Charging</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            &nbsp;
                            </td>
                        </tr>
<%--                        <tr>
                            <td align="left" colspan="3" style="font-size: 100%;">
                                <asp:HyperLink ID="HyperLink2" runat="server" 
                                    NavigateUrl="~/Account/NewUserCreation.aspx" ForeColor="White" CssClass="style2">Create an account</asp:HyperLink>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2" style="font-size: 100%;">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" 
                                    Text="Login" ValidationGroup="Login1" CssClass="login" />
                            </td>
                            <td align="right">
                            </td>
                        </tr>
               </table>
               </LayoutTemplate>
            </asp:Login>                                                                      
    </form>
</body>
</html>

