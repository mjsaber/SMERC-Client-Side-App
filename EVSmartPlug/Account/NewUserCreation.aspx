<%@ Page Title="New User Creation" Language="C#" AutoEventWireup="true" CodeBehind="NewUserCreation.aspx.cs" Inherits="EVUser.Account.NewUserCreation" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
    .login{
	    font-family:Arial, Helvetica, sans-serif;
	    font-size:3.5em;
	    background:url(LoginButton.png);
	    margin-left:20px;
	    color:#000;
	    font-weight:bold;
	    height:60px;
	    width:186px;
	    line-height:60px;
	    margin-bottom:40px;
	    text-decoration:none;
	    border-radius:10px;
	    -moz-border-radius: 10px;
	    -webkit-border-radius: 10px;
	    text-align:center;
    }

    .wrapperlogin 
    {
            margin-left:10px;
            margin-right:10px;
            width: auto;
            background-color:#919192;
            border-bottom-left-radius:10px;
            border-bottom-right-radius:10px;
            -moz-border-bottom-leftradius: 10px;
            -webkit-borderbottom-right-radius: 10px;
            -moz-border-bottom-left-radius: 10px;
            -webkit-border-bottom-right-radius: 10px;
    }
     .field	
    {
        margin-left:20px;
        margin-right:20px;
	    display:block;
	    font-size:3.5em;
	    border-radius:10px;
	    -moz-border-radius: 10px;
	    -webkit-border-radius: 10px;
	    font-weight:bold;
    }


    .style1
    {
        height: 10px;
        width:auto;
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

    /* DEFAULTS
----------------------------------------------------------*/

body   
{
    background: #b6b7bc;
    font-size: .80em;
    font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    margin: 0px;
    padding: 0px;
    color: #696969;
}

a:link, a:visited
{
    color: #034af3;
}

a:hover
{
    color: #1d60ff;
    text-decoration: none;
}

a:active
{
    color: #034af3;
}

p
{
    margin-bottom: 10px;
    line-height: 1.6em;
}


/* HEADINGS   
----------------------------------------------------------*/

h1, h2, h3, h4, h5, h6
{
    font-size: 1.5em;
    color: #666666;
    font-variant: small-caps;
    text-transform: none;
    font-weight: 200;
    margin-bottom: 0px;
}

h1
{
    font-size: 1.6em;
    padding-bottom: 0px;
    margin-bottom: 0px;
}

h2
{
    font-size: 1.5em;
    font-weight: 600;
}

h3
{
    font-size: 1.2em;
}

h4
{
    font-size: 1.1em;
}

h5, h6
{
    font-size: 1em;
}

/* this rule styles <h1> and <h2> tags that are the 
first child of the left and right table columns */
.rightColumn > h1, .rightColumn > h2, .leftColumn > h1, .leftColumn > h2
{
    margin-top: 0px;
}


/* PRIMARY LAYOUT ELEMENTS   
----------------------------------------------------------*/

.page
{
    width: 960px;
    background-color: #fff;
    margin: 20px auto 0px auto;
    border: 1px solid #496077;
}

.header
{
    position: relative;
    margin: 0px;
    padding: 0px;
    background: #4b6c9e;
    width: 100%;
}

.header h1
{
    font-weight: 700;
    margin: 0px;
    padding: 0px 0px 0px 20px;
    color: #f9f9f9;
    border: none;
    line-height: 2em;
    font-size: 2em;
}

.main
{
    padding: 0px 12px;
    margin: 12px 8px 8px 8px;
    min-height: 420px;
}

.leftCol
{
    padding: 6px 0px;
    margin: 12px 8px 8px 8px;
    width: 200px;
    min-height: 200px;
}

.footer
{
    color: #4e5766;
    padding: 8px 0px 0px 0px;
    margin: 0px auto;
    text-align: center;
    line-height: normal;
}


/* TAB MENU   
----------------------------------------------------------*/

div.hideSkiplink
{
    background-color:#3a4f63;
    width:100%;
}

div.menu
{
    padding: 4px 0px 4px 8px;
}

div.menu ul
{
    list-style: none;
    margin: 0px;
    padding: 0px;
    width: auto;
}

div.menu ul li a, div.menu ul li a:visited
{
    background-color: #465c71;
    border: 1px #4e667d solid;
    color: #dde4ec;
    display: block;
    line-height: 1.35em;
    padding: 4px 20px;
    text-decoration: none;
    white-space: nowrap;
}

div.menu ul li a:hover
{
    background-color: #bfcbd6;
    color: #465c71;
    text-decoration: none;
}

div.menu ul li a:active
{
    background-color: #465c71;
    color: #cfdbe6;
    text-decoration: none;
}

/* FORM ELEMENTS   
----------------------------------------------------------*/

fieldset
{
    margin: 1em 0px;
    padding: 1em;
    border: 1px solid #ccc;
}

fieldset p 
{
    margin: 2px 12px 10px 10px;
}

fieldset.login label, fieldset.register label, fieldset.changePassword label
{
    display: block;
}

fieldset label.inline 
{
    display: inline;
}

legend 
{
    font-size: 1.1em;
    font-weight: 600;
    padding: 2px 4px 8px 4px;
}

input.textEntry 
{
    width: 320px;
    border: 1px solid #ccc;
}

input.passwordEntry 
{
    width: 320px;
    border: 1px solid #ccc;
}

div.accountInfo
{
    width: 99%;
}

/* MISC  
----------------------------------------------------------*/

.clear
{
    clear: both;
}

.title
{
    display: block;
    float: left;
    text-align: left;
    width: auto;
}

.loginDisplay
{
    font-size: 1.1em;
    display: block;
    text-align: right;
    padding: 10px;
    color: White;
}

.loginDisplay a:link
{
    color: white;
}

.loginDisplay a:visited
{
    color: white;
}

.loginDisplay a:hover
{
    color: white;
}

.failureNotification
{
    font-size: 1.2em;
    color: Red;
}

.bold
{
    font-weight: bold;
}

.submitButton
{
    text-align: right;
    padding-right: 10px;
}

.noteofmap
	{font-size:1.5em;
	color:#919192;
}

.stationlistbig	{

	font-size:2.5em;
	font-weight:bold;	
	color:#999;

}
.result2	{
	font-size:1em;
	color:#999;
}

.dropdownliststyle
{
    background: -webkit-gradient(linear, left top, left bottom, color-stop(0, #aaa), color-stop(0.12, #fff));
}

.gapstyle
{
    height:10px;
}

.inputstyle{
    font-size:4em; font-weight:bold; display:block; border-radius:10px; margin-bottom:30px;
}


			

</style>

<head id="Head1" runat="server">
<%--    <link rel="apple-touch-icon-precomposed" href="apple-touch-icon-157x157.png"/>--%>
    <link rel="apple-touch-icon-precomposed" href="http://wireless3.seas.ucla.edu/evuser/Account/apple-touch-icon-157x157.png"/>
    <link rel="apple-touch-icon" href="apple-touch-icon.png"/>
</head>


<body class="page">
<form id="formCreateNewUser" runat="server">
    <table style="border-collapse:collapse; font-size: xx-large; font-weight: bolder;" width="100%" align="center">
        <tr>
            <td align="center" colspan="3" style="background-color:#7ba33c" >
                <img src="login_top_new.png" alt="SMERC" width="100%" height="100%" />
            </td>
        </tr>
    
        <td>
    <h3>
        Create New User:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        <asp:Button ID="btnHideError" runat="server" Text="Hide Error" Visible="false" OnClick="btnHideError_Click" />
        
        <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
        
        <br />
    </h3>
    <p style="font-family: Arial; font-size: xx-large; font-weight: bold; color: #C0C0C0" align="left">
        -Your email address must be validated by an administrator before creating a profile.<br />
        -New passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length
        <br />
    </p>
            </td>
    </tr>
    <table align="center" width="90%">
        <tr align="center">
            <td align="center">

                <fieldset>
                    <legend style="font-family: Arial; font-size: xx-large; font-weight: bold; color: #000000">New User Information</legend>
                        <table align="center" width ="100%">

                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">&nbsp;</td>
                                <td style="width: 80%; text-align: left;">     <asp:Label ID="lblSuccessOrFailure" runat="server" Font-Bold="True" Font-Size="xx-large" Text=""></asp:Label>
                                    
                                    <br />
                                    
                                    <br />
                                    <asp:Label ID="lblEmailError" runat="server" ForeColor="Red" Font-Size="XX-Large"></asp:Label>
                                                                    
                                    <br />
                                    <asp:Label ID="lblDesiredUserNameError" runat="server" Font-Size="XX-Large"></asp:Label>
                                                                                                    
                                <td></td>
                            </tr>

                            <%--Desired Username--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                     <asp:Label ID="lblDesiredUserName" runat="server" AssociatedControlID="lblDesiredUserName" Font-Bold="True" CssClass="style2" Font-Size="xx-large">Desired Username</asp:Label>                           
                                     <asp:TextBox ID="tbDesiredUsername" runat="server" MaxLength="50" CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbDesiredUsername" runat="server" SetFocusOnError="true" ControlToValidate="tbDesiredUsername" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                              
                                </td>
                            </tr>

                            <%--Password--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                     <asp:Label ID="lblPassword" runat="server" AssociatedControlID="lblPassword" Font-Bold="True" Font-Size="xx-large">Password</asp:Label>                           
                                     <asp:TextBox ID="tbPassword" runat="server" MaxLength="50" TextMode="Password" CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbPassword" runat="server" ControlToValidate="tbPassword" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>                              
                                    &nbsp;<asp:RegularExpressionValidator ID="vltbPassword" runat="server" ControlToValidate="tbPassword" ErrorMessage="Minimum password length is 6" Display="Dynamic" ForeColor="Red" ValidationExpression=".{6}.*" />
                                    <asp:Label ID="lblPasswordError" runat="server" SetFocusOnError="true" Text=""></asp:Label>
                                </td>
                            </tr>

                            <%--Repeat Password--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                     <asp:Label ID="lblRepeatPassword" runat="server" AssociatedControlID="lblRepeatPassword" Font-Bold="True" Font-Size="xx-large">Repeat Password</asp:Label>                           
                                     <asp:TextBox ID="tbRepeatPassword" runat="server" MaxLength="50" TextMode="Password" CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbRepeatPassword" runat="server" ControlToValidate="tbRepeatPassword" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                              
                                    <br />
                                    <%--Password Checker--%>
                                    <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="tbPassword" SetFocusOnError="true" ControlToValidate="tbRepeatPassword" Display="Dynamic" ErrorMessage="Passwords must match." ForeColor="Red" />
                                </td>
                            </tr>

                              <%--Password Question--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblPasswordQuestion" runat="server" AssociatedControlID="lblPasswordQuestion" Font-Bold="True" Font-Size="xx-large">Password Question</asp:Label>
                                    <asp:TextBox ID="tbPasswordQuestion" runat="server" MaxLength="50"   CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">       
                                    
                                </td>
                            </tr>

                            <%--Password Answer--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold"></td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblPasswordAnswer" runat="server" AssociatedControlID="lblPasswordAnswer" Font-Bold="True" Font-Size="xx-large">Password Answer</asp:Label>
                                    <asp:TextBox ID="tbPasswordAnswer" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">         
                                    
                                </td>
                            </tr>                       

                            

                            <%--Email--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="lblEmail" Font-Bold="True" Font-Size="xx-large">Email</asp:Label>
                                    <asp:TextBox ID="tbEmail" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>      
                                </td>


                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbEmail" runat="server" ControlToValidate="tbEmail" ErrorMessage="Required." Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>                              
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
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblPhoneNumber" runat="server" AssociatedControlID="lblPhoneNumber" Font-Bold="True" Font-Size="xx-large">Phone Number</asp:Label>
                                   &nbsp;<strong style="font-size: x-large">&nbsp;*Example Format: 310-267-6979</strong><asp:TextBox ID="tbPhoneNumber" runat="server" MaxLength="20"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">
                                    <asp:RequiredFieldValidator ID="rfltbPhoneNumber" runat="server" ControlToValidate="tbPhoneNumber" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>          
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbPhoneNumber" ErrorMessage="Format Erro." ValidationExpression="^[2-9]\d{2}-\d{3}-\d{4}$" ForeColor="Red" />         
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
                                    <asp:TextBox ID="tbAddress2" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">                                          
                                </td>
                            </tr>

                            <%--Zip Code--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="lblZipCode" Font-Bold="True" Font-Size="xx-large">Zip Code</asp:Label>
                                    <asp:TextBox ID="tbZipCode" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">          
                                    <asp:RequiredFieldValidator ID="rfltbZipCode" runat="server" ControlToValidate="tbZipCode" SetFocusOnError="true" ErrorMessage="Required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                          
                                
                                    <asp:RegularExpressionValidator ID="RegExp1" runat="server"  ErrorMessage="Format Error."  ControlToValidate="tbZipCode" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{5,10}$" ForeColor="Red" />
                                </td>
                            </tr>

                            <%--City--%>
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblCity" runat="server" AssociatedControlID="lblCity" Font-Bold="True" Font-Size="xx-large">City</asp:Label>
                                    <asp:TextBox ID="tbCity" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">          
                                    <asp:RequiredFieldValidator ID="rfltbCity" runat="server" ControlToValidate="tbCity" SetFocusOnError="true" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                                          
                                </td>
                            </tr>

                            <%--State--%>
                            <tr align="center"  style="font-size: 130%" class="style2">
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%;  font-size: xx-large; text-align: left;">         
                                    <asp:Label ID="lblState" runat="server" AssociatedControlID="lblState" Font-Bold="True" Font-Size="xx-large">State</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlState" runat="server"  Font-Bold="True" Font-Size="130%" Width="100%" CssClass="stationlistbig" ></asp:DropDownList>
                                </td>

                                <td style="text-align: left; font-size: xx-large;">          
                                    <asp:RequiredFieldValidator ID="rflddlState" runat="server" ControlToValidate="ddlState" SetFocusOnError="true" InitialValue="-1" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                                          
                                </td>
                            </tr>

                             <%--EV Model--%>
                            <tr align="center"  style="font-size: 130%" class="style2">
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%;  font-size: xx-large; text-align: left;">         
                                    <asp:Label ID="lblEVModel" runat="server" AssociatedControlID="lblEVModel" Font-Bold="True" Font-Size="xx-large">EV Model</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlEVModel" runat="server"  Font-Bold="True" Font-Size="130%" Width="100%" CssClass="stationlistbig" ></asp:DropDownList>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">          
                                    <asp:RequiredFieldValidator ID="rflddlEVModel" runat="server" ControlToValidate="ddlEVModel" SetFocusOnError="true" InitialValue="-1" ErrorMessage="Required." ForeColor="Red"></asp:RequiredFieldValidator>                                          
                                </td>
                            </tr>

                              

                            <%--Smart Phone OS--%>
                           <tr align="center"  style="font-size: 130%" class="style2">
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
                            <tr align="center"  style="font-size: 130%" class="style2">
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
                            <tr>
                                <td style="color: #FF0000; font-size: xx-large; font-weight: bold">*</td>
                                <td style="width: 80%; text-align: left;">         
                                    <asp:Label ID="lblSmartPhoneModel" runat="server" AssociatedControlID="lblSmartPhoneModel" Font-Bold="True" Font-Size="xx-large">Smart Phone Model</asp:Label>
                                    <asp:TextBox ID="tbSmartPhoneModel" runat="server" MaxLength="50"  CssClass="inputstyle" Width="100%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; font-size: xx-large;">         
                                    
                                </td>

                            </tr>


                    </table>
                </fieldset>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnCreateNewUser" runat="server" Text="Create New User" CssClass="button_green" OnClick="btnCreateNewUser_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel" CssClass="button_green" OnClick="btnCancel_Click"/>
                        </td>
                    </tr>
               </table>
                
            </td>
        </tr>
    </table>
        </table>

    </form>
</body>
</html>
