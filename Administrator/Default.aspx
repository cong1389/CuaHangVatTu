<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Administrator_Default" MasterPageFile="MasterPage.master"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
    function ajaxclick(sOrderNo) {
        $get("maincontent_txtOrderNo").value = sOrderNo;
        $get("maincontent_ajaxButton").click();
    }

</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="maincontent">
    <div id="wrapper">
        <div>
            <table class="adminform" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:800px;" valign="top">
	   	                <div id="cpanel">
                             <asp:Label ID="LblAdminMenu" runat="server" Text=""></asp:Label>
	                        <div style="clear:both;"> </div>
                        </div>
		            </td>
                    <td  valign="top">
                        
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" class="footer">
	        <table width="99%" border="0">
	            <tr>
		            <td align="center">			
			            <div align="center">Copyright © 2015 Công ty cuahangvattu.com</div>
                        <div align="center">	Design By:&nbsp;<a href="http://www.cuahangvattu.com/" target="_blank"><b>cuahangvattu.com</b></a></div>
                    </td>
			        </tr>
	        </table>
        </div>
    </div>
</asp:Content>