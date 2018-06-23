<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="ItemGift.aspx.vb" Inherits="Administrator_ItemGift" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function chooseItem(ItemNo, ItemName) {
        $get('maincontent_TxtItemNo').value = ItemNo;
        $get('maincontent_TxtItemName').value = ItemName;
        var modalPopupExtend = $find('maincontent_ModalPopupPanel');
        modalPopupExtend.hide();
    }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
    <div>
        <asp:Button ID="cmdItemList" runat="server" Text="Danh sách sản phẩm" CssClass="button" />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width:1000px; margin:0 auto 0 auto;">
                <table>
                    <tr>
                        <td>Mã chương trình</td>
                        <td>Sản phẩm chính</td>
                        <td>ĐVT</td>
                        <td>Số lượng</td>
                        <td>
                            <asp:LinkButton ID="FindItem" runat="server" style="cursor:pointer;">Mã quà tặng</asp:LinkButton>
                    
                        </td>
                        <td>Tên quà tặng</td>
                        <td>Số lượng</td>
                        <td>Từ ngày</td>
                        <td>Đến ngày</td>
                    </tr>
                    <tr>
                        <td valign="top"><asp:TextBox ID="TxtOfferNo" runat="server" Width="100px"></asp:TextBox></td>
                        <td valign="top"><asp:TextBox ID="TxtMainItem" runat="server" Width="80px" Enabled="false"></asp:TextBox></td>
                        <td><asp:DropDownList ID="CboUnitOfMeasure" runat="server" CssClass="inputbox" Width="80px" Enabled="false"></asp:DropDownList></td>
                        <td valign="top"><asp:TextBox ID="TxtMainQty" runat="server" Width="60px" Text="1" Enabled="false"></asp:TextBox></td>
                        <td valign="top"><asp:TextBox ID="TxtItemNo" runat="server" Width="80px"></asp:TextBox></td>
                        <td valign="top"><asp:TextBox ID="TxtItemName" runat="server" Width="200px"></asp:TextBox></td>
                        <td valign="top"><asp:TextBox ID="TxtQuantity" runat="server" Width="60px"></asp:TextBox></td>
                        <td valign="top"><asp:TextBox ID="TxtStartingDate" runat="server" Width="80px"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                                    TargetControlID="TxtStartingDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender></td>
                        <td valign="top"><asp:TextBox ID="TxtEndingDate" runat="server" Width="80px"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender4" runat="server" 
                                    TargetControlID="TxtEndingDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender></td>
        
                    </tr>
                    <tr>
                        <td colspan="9">
                            <asp:HiddenField ID="TxtLineNo" runat="server" />
                            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" /> &nbsp;&nbsp;
                            <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>

                        </td>
                    </tr>
                </table>
            </div>
            <div style="width:1000px; margin:0 auto 0 auto;">
                 <asp:GridView ID="GrdGift" runat="server" AutoGenerateColumns="false" 
                     CellPadding="3" Width="100%" BackColor="White" BorderColor="#CCCCCC" 
                     BorderStyle="None" BorderWidth="1px">
                    <RowStyle ForeColor="#000066" />
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Discount No_" HeaderText="Mã chương trình" />
                        <asp:BoundField DataField="Line No_" HeaderText="Line No" />
                        <asp:BoundField DataField="Gift Item No_" HeaderText="Mã quà tặng" />
                        <asp:BoundField DataField="Gift Item Name" HeaderText="Tên quà tặng" />
                        <asp:BoundField DataField="Unit of Measure No_" HeaderText="ĐVT" />
                        <asp:BoundField DataField="Starting Date" HeaderText="Từ ngày" />
                        <asp:BoundField DataField="Ending Date" HeaderText="Đến ngày" />
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                     <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                     <SortedAscendingCellStyle BackColor="#F1F1F1" />
                     <SortedAscendingHeaderStyle BackColor="#007DBB" />
                     <SortedDescendingCellStyle BackColor="#CAC9C9" />
                     <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </div>
          </ContentTemplate>
    </asp:UpdatePanel>
    <div style="display:none;">
        <asp:Button ID="cmdShowPanel" runat="server" Text="Đóng" CssClass="button" />
    </div>
    <asp:ModalPopupExtender ID="ModalPopupPanel" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="cmdShowPanel"   PopupControlID="Panel1" OkControlID= "cmdClosePanel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="popup">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <div style="width:800px;height:600px;">
            <div>
                <table width="100%">
                    <tr>
                        <td style="width:202px;">Nhóm sản phẩm</td>
                        <td style="width:132px;">Tình trạng</td>
                        <td style="width:172px;">Tìm kiếm</td>
                        <td></td>
                    </tr>
                    <tr>
               
                        <td>
                            <asp:DropDownList ID="CboMenuGroup" runat="server"  Width="200px" CssClass="inputbox">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="CboStatus" runat="server" Width="130px" CssClass="inputbox">
                                <asp:ListItem Value="0">Chọn tình trạng</asp:ListItem>
                                <asp:ListItem Value="1">Đã published</asp:ListItem>
                                <asp:ListItem Value="2">Chưa published</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtSearch" runat="server" Width="170"></asp:TextBox>
                        </td>
                        <td><asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" /> &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="cmdClosePanel" runat="server" Text="Đóng" CssClass="button" /></td>
                    </tr>
                </table>
            </div>
            <div style="width:800px; height:520px; overflow:auto;">
                <asp:GridView ID="grdItem" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" Width="100%">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Item No">
                            <itemtemplate>
                                <a href="#" onclick="chooseItem('<%#Eval("No_")%>','<%#Eval("Name")%>')"><%#Eval("No_")%></a>
                            </itemtemplate>
                        </asp:TemplateField>     
                        <asp:BoundField DataField="Name" HeaderText="Item Name" />
                        <asp:BoundField DataField="Model" HeaderText="Model" />
                        <asp:BoundField DataField="Brand No_" HeaderText="Brand" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                    </Columns>
                </asp:GridView>
            </div>    
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
   
     
</asp:Content>

