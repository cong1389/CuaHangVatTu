<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="SalesManagement.aspx.vb" Inherits="Administrator_Sales_Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">
    function ajaxclick(sOrderNo) {
        $get("maincontent_txtOrderNo").value = sOrderNo;
        $get("maincontent_ajaxButton").click();
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<div style="background-color:#F1F3F5;">
        <table width="100%">
            <tr>
                <td style="width:100px;">Từ ngày</td>
                <td style="width:100px;">Đến ngày</td>
                <td style="width:200px;">Tình trạng</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TxtStartingDate" runat="server" Width="100px" ></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                            TargetControlID="TxtStartingDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>

                </td>
                <td>
                    <asp:TextBox ID="TxtEndingDate" runat="server" Width="100px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                            TargetControlID="TxtEndingDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                </td>
                <td>
                    <asp:DropDownList ID="CboStatus" runat="server" Width="130px" CssClass="inputbox">
                        <asp:ListItem Value="0">Chọn tình trạng</asp:ListItem>
                        <asp:ListItem Value="1">Mới đặt hàng</asp:ListItem>
                        <asp:ListItem Value="2">Đã duyệt</asp:ListItem>
                        <asp:ListItem Value="3">Đang giao hàng</asp:ListItem>
                        <asp:ListItem Value="4">Đã hoàn thành</asp:ListItem>
                        <asp:ListItem Value="5">Hủy</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
                </td>
                <td align="right">
                    <asp:Label ID="LblWarningList" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="normaltext">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:gridview ID="grdSaleOrderList" runat="server" AutoGenerateColumns="False" 
                CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="100%" 
                BackColor="White" BorderColor="#dddddd" BorderStyle="Solid" BorderWidth="1px" AllowPaging="True" PageSize="25">
                <AlternatingRowStyle BackColor="#F5F5F5" />
                <Columns>
                    <asp:TemplateField HeaderText="Thao tác">
                        <itemtemplate>
                            <a href="#" onclick="ajaxclick('<%#Eval("No_")%>','E');return false;"><%# Eval("No_")%></a> &nbsp;
                        </itemtemplate>
                        <ControlStyle />
                    </asp:TemplateField>     
                    <asp:BoundField DataField="Order Date" HeaderText="Ngày đặt hàng" />
                    <asp:BoundField DataField="Customer Name" HeaderText="Khách hàng" />
                    <asp:BoundField DataField="Ship to Address" HeaderText="Địa chỉ giao hàng" />
                    <asp:BoundField DataField="Amount Inc VAT" HeaderText="Tổng số tiền" DataFormatString="{0:n}"/>
                    <asp:BoundField DataField="Order Status" HeaderText="Tình trạng đơn hàng" />
                    
                </Columns>
                <FooterStyle BackColor="#F5F5F5" />
                <HeaderStyle BackColor="#FFE061" Font-Bold="True" ForeColor="Black" Height="26px"/>
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle Height="24px" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:gridview>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


       
    <div style="display:none;">
        <asp:HiddenField ID="txtOrderNo" runat="server" />    
        <asp:Button ID="ajaxButton" runat="server" Text="Show" />
        <asp:Button ID="cmdShowModalPopup" runat="server" Text="Show" />
    </div>
    <asp:ModalPopupExtender ID="ModalPopupSalesOrder" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="cmdShowModalPopup" PopupControlID="PopupSalesOrder" OkControlID="cmdCloseModalPopupSalesOrder">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PopupSalesOrder" runat="server" CssClass="popup" style="Display:none; width:900px;height:600px;background-color:#fff;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
            <div style="position:fixed; width:900px;height:600px;background:#fff;">
                <table width="100%">
                    <tr>
                        <td style="width:900px;height:600px;" valign="middle" align="center">
                            Loading...
                        </td>
                    </tr>
                </table>
            </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class = "popupheader" style="height:22px;"><b>Chi tiết đơn hàng</b></div>
                <div style="height:510px;overflow-x: hidden; overflow-y: auto;">
                    <asp:Label ID="LblOrderDetail" runat="server" Text=""></asp:Label>
                </div>
                <div style="height:24px;padding-right:10px;">
                    <asp:CheckBox ID="CKEmail" runat="server" Text="Gửi email thông báo cho khách hàng"/>
                </div>
                <div style="height:24px;text-align:right;padding-right:10px;">
                    <asp:Button ID="cmdAccept" runat="server" Text="Duyệt" Cssclass="button"/>    
                    <asp:Button ID="cmdCancel" runat="server" Text="Hủy" Cssclass="button"/>    
                    <asp:Button ID="cmdDelivery" runat="server" Text="Giao hàng" Cssclass="button"/>    
                    <asp:Button ID="cmdFinished" runat="server" Text="Hoàn tất" Cssclass="button"/>    
                    <asp:Button ID="cmdCloseModalPopupSalesOrder" runat="server" Text="Đóng" Cssclass="button"/>    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

