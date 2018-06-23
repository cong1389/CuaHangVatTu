<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="MasterPage/MasterSite.master"  CodeFile="YourCart.aspx.vb" Inherits="YourCart" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />    
</asp:Content>
<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">
  
  <div class="pathway">
    
    <asp:Label ID="LblPathWay" runat="server" Text="Label"></asp:Label>
</div>    
<div class="main" style="margin:5px;">
    
    <div class="TitleLine">GIỎ HÀNG CỦA BẠN</div>
    <div class="cart-list">
        <asp:UpdatePanel ID="UpCart" runat="server">
        <ContentTemplate>
            <div><asp:Label ID="LblCart" runat="server" Text=""></asp:Label></div>           
            <div style="padding:5px;">
                <asp:HiddenField ID="TxtLineNo" runat="server" />
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td rowspan="2" valign="bottom">
                        <asp:Button ID="cmdUpdateCart" runat="server" Text="Cập nhật giỏ hàng" Cssclass="ButtonW8Big"></asp:Button>
                                    
                        <td rowspan="2"valign="bottom" align="right">
                        <asp:Button ID="cmdPayment" runat="server" Text="Thanh toán" Cssclass="ButtonW8Big"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding:5px;">
                <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
            </div>
            <div><br /></div>
            <div style="padding-left:5px;">Để thay đổi số lượng, quý khách nhập số lượng cần thay đổi vào ô số lượng sau đó nhấn nút "Cập nhật giỏ hàng".</div>
            <div style="padding-left:5px;">Để bỏ sản phẩm đã chọn, quý khách click vào link "xóa" ở dòng sản phẩm muốn bỏ.</div>
            <div><hr /></div>
            <div><br /><br /></div>
            <div>
                <table>
                    <tr>
                        <td style="width:400px;"><b>Chấp nhận thanh toán</b></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="ImgPayAcept" runat="server" />
                        </td>
                        <td>
                                        
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
</div>
 

</asp:Content>


