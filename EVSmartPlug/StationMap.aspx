<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="StationMap.aspx.cs" Inherits="EVUser.StationMap" %>
<%@ Register assembly="GoogleMap" namespace="GoogleMap" tagprefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style5
        {
            width: 758px;
        }
        .style6
        {
            width: 600px;
        }
        .style7
        {
            width: 205px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<script type = "text/javascript">
    function clickMarker(id) {
        var marker = MarkersList[id];
        google.maps.event.trigger(marker.marker, "click");
        
    }
</script>


    <p style="font-size: xx-large; font-weight: bolder">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/icon_page_moas.png" />
    </p>
    <table style="font-size: xx-large; font-weight: bolder" width="100%">        
        <tr>
            <td colspan="2" style="font-size: 130%; font-weight: bolder">
                <asp:DropDownList ID="ddlCity" runat="server" Font-Bold="True" 
                    Font-Size="130%" AutoPostBack="True" 
                    onselectedindexchanged="ddlCity_SelectedIndexChanged" Width="100%" 
                    CssClass="stationlistbig" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: 130%; font-weight: bolder">
                Click the balloon in the map or the row in the table to view the direction.
            </td>
        </tr>

        <tr>
            <td align="center" colspan="2" class="style5" style="font-size: 50%; font-weight: bolder">
                <asp:GridView ID="gvParkingLotList" runat="server" 
                    AlternatingRowStyle-BackColor="#CCCCCC" AutoGenerateColumns="False" 
                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="Black" 
                    HeaderStyle-ForeColor="White" Width="100%">
<AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField HeaderText="Parking Lot" DataField="Name" 
                            SortExpression="Status" ItemStyle-CssClass="stationlistbig" >
<ItemStyle CssClass="stationlistbig"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Address" DataField="Address" 
                            SortExpression="Address" HtmlEncode="false" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="result2">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total" DataField="Total" SortExpression="Total"  
                            ItemStyle-CssClass="stationlistbig" >
<ItemStyle CssClass="stationlistbig"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Available*" DataField="Available" 
                            SortExpression="Available"  ItemStyle-CssClass="stationlistbig"  >
<ItemStyle CssClass="stationlistbig"></ItemStyle>
                        </asp:BoundField>
                    </Columns>

<HeaderStyle HorizontalAlign="Left" BackColor="#0E784A" ForeColor="White" Font-Size="XX-Large"></HeaderStyle>
                </asp:GridView>                
            </td>
        </tr>
<%--        <tr>
            <td class="gapstyle">
            &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="style5" style="font-size: 130%; font-weight: bolder">                                                        

                <asp:Button ID="btnStartCharging" runat="server" Text="Start Charging" Font-Size="100%" 
                    PostBackUrl="~/TurnOn.aspx" Font-Bold="True" CssClass="buttonstyle" />                                                
            </td>
        </tr>--%>


        <tr>
            <td align="center" colspan="2" class="style5" style="font-size: 130%; font-weight: bolder">
                <cc1:GoogleMap ID="gmParkLot" runat="server">
                </cc1:GoogleMap>                                                
            </td>
        </tr>
        <tr>
            <td>
                <div id = "divDirection">
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="style5" style="font-size: 130%; font-weight: bolder">
                <asp:Label ID="lblRetrieveTime" runat="server" Text=""></asp:Label>                                                            
            </td>
        </tr>
    </table>
<%--<p class="noteofmap"><strong>Important Note:</strong> After select a city, please allow the communication networks to load available information to your smartphone (the drop down list will show &quot;Select ...&quot;). If you do not allow to load, a&nbsp; message will be shown. Do not close, leave, or refresh this page when it is in waiting status.</p>--%>
</asp:Content>
