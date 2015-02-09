<%@ Page Title="EVSmartPlug" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="EVUser._Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<style type="text/css">

a.button{
	font-family:Arial, Helvetica, sans-serif;
	font-size:3em;
	
background: #007947; /* Old browsers */

background: -moz-linear-gradient(top,  #007947 0%, #007947 50%, #07673f 51%, #007947 100%); /* FF3.6+ */

background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#007947), color-stop(50%,#007947), color-stop(51%,#07673f), color-stop(100%,#007947)); /* Chrome,Safari4+ */

background: -webkit-linear-gradient(top,  #007947 0%,#007947 50%,#07673f 51%,#007947 100%); /* Chrome10+,Safari5.1+ */

background: -o-linear-gradient(top,  #007947 0%,#007947 50%,#07673f 51%,#007947 100%); /* Opera 11.10+ */

background: -ms-linear-gradient(top,  #007947 0%,#007947 50%,#07673f 51%,#007947 100%); /* IE10+ */

background: linear-gradient(to bottom,  #007947 0%,#007947 50%,#07673f 51%,#007947 100%); /* W3C */

filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#007947', endColorstr='#007947',GradientType=0 ); /* IE6-9 */


	
	display:block;
	color:#fff;
	font-weight:bold;
	height:96px;
	line-height:90px;
	margin-bottom:10px;
	text-decoration:none;
	border-radius:10px;
}
a.button1{
	font-family:Arial, Helvetica, sans-serif;
	font-size:3em;
	background:url(images/button1_bg.png);
	display:block;
	color:#fff;
	font-weight:bold;
	height:96px;
	width:390px;
	line-height:90px;
	margin-bottom:10px;
	text-decoration:none;
	border-radius:10px;
	text-indent:30px;
}
a:hover.button{
	color:# FF0;
}

/* -------------------- */
/* CLASSES */
/* -------------------- */
/*
.map{
background:url(icon_menu_mapofstations.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
}
.status{
background:url(images/icon_menu_chargingstatus.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
}
.start{
background:url(images/icon_menu_startcharging.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
}
.stop{
background:url(images/icon_menu_stopcharging.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
}
.schedule{
background:url(images/icon_menu_schedulecharging.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
}
.check{
background:url(images/icon_menu_checkschedule.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
}
.cancel{
background:url(images/icon_menu_cancelschedule.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
}
.records{
background:url(images/icon_menu_chargingrecords.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
}
.user{
background:url(images/icon_menu_profile.png) no-repeat 32px 18px;
text-indent:120px;
display:block;
*/
}
</style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/icon_page_home.gif" />
    <asp:Table ID="tblMainMenu" runat="server" Width="100%">
        <asp:TableRow ID="trMap">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="ImageButton4" PostBackUrl="~/StationMap.aspx" 
                    runat="server" ImageUrl="~/Images/nav_moas.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trStartCharging">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="btnStartCharging" PostBackUrl="~/TurnOn.aspx" 
                    runat="server" ImageUrl="~/Images/menu_button03_startcharging.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trChargingStatus">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="btnViewStatus"  onclick="btnViewStatus_Click" 
                    runat="server" ImageUrl="~/Images/menu_button02_chargingstatus.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trStopCharging">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="btnStopCharging" PostBackUrl="~/TurnOff.aspx" 
                    runat="server" ImageUrl="~/Images/menu_button04_stopcharging.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trChargingRecord">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="ImageButton2" PostBackUrl="~/Records.aspx" 
                    runat="server" ImageUrl="~/Images/menu_button08_chargingrecords.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trUserProfile">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="ImageButton3" PostBackUrl="~/Profile.aspx"
                    runat="server" ImageUrl="~/Images/menu_button09_profile.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trFeedBack">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="ImageButton5" PostBackUrl="~/Feedback.aspx"
                    runat="server" ImageUrl="~/Images/nav_feedback.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trSocialMedia">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="ImageButton6" PostBackUrl="~/SocialMedia.aspx"
                    runat="server" ImageUrl="~/Images/nav_soclaimedia.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trChangePassword">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <asp:ImageButton ID="ImageButton7" PostBackUrl="~/ChangePassword.aspx"
                    runat="server" ImageUrl="~/Images/nav_changepassword.png" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trUserManual">
            <asp:TableCell HorizontalAlign="Center"  CssClass="style2">
                <a href="http://wireless3.seas.ucla.edu/evuser/EVSmartPlugUserManual.pdf">
                    <img src="images/nav_download.png" alt="Download User Manual" width="829" height="96" border="0" />
                </a>
            </asp:TableCell>
        </asp:TableRow>

    </asp:Table>
</asp:Content>
