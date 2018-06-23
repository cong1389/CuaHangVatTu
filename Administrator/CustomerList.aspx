<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="CustomerList.aspx.vb" Inherits="Administrator_CustomerList" %>
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
<div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="MyTabStyle"
            Height="550px" Width="100%">
            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                <HeaderTemplate>
                    Danh sách khách hàng
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="GrdCustomerList" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="No_" HeaderText="Mã khách hàng" />
                            <asp:BoundField DataField="Full Name" HeaderText="Tên khách hàng" />
                            <asp:BoundField DataField="Full Address" HeaderText="Địa chỉ" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Telephone" HeaderText="Điện thoại" />
                            <asp:BoundField DataField="Province" HeaderText="Tỉnh / thành phố" />
                            <asp:BoundField DataField="Register Date" HeaderText="Ngày đăng ký" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>

                </ContentTemplate>
            </asp:TabPanel>
             <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>
                    Khách hàng đánh giá
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="normaltext">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrdCustomerReview" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="Line No_" HeaderText="Line no." />
                                        <asp:BoundField DataField="Item No_" HeaderText="Item no." />
                                        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" />
                                        <asp:BoundField DataField="Review Text" HeaderText="Review" />
                                        <asp:BoundField DataField="Customer Rate" HeaderText="Rate" />
                                        <asp:BoundField DataField="Review Date" HeaderText="Date" />
                                        <asp:BoundField DataField="Review Hour" HeaderText="Hour" />
                                        <asp:BoundField DataField="Customer IP" HeaderText="Customer IP" />
                                        <asp:CheckBoxField DataField="Published" HeaderText="Published" />
                                        <asp:CommandField SelectText="Duyệt" ShowSelectButton="True" />
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>                                        
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            
        </asp:TabContainer>
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
                <div class = "popupheader" style="height:22px;"><b>cuahangvattu.com - Chi tiết đơn hàng</b></div>
                <div style="height:510px;overflow-x: hidden; overflow-y: auto;">
                    <asp:Label ID="LblOrderDetail" runat="server" Text=""></asp:Label>
                </div>
                <div style="height:24px;padding-right:10px;">
                    <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
                </div>
                <div style="height:24px;text-align:right;padding-right:10px;">
                    <asp:Button ID="cmdAccept" runat="server" Text="Duyệt" Cssclass="button"/>    
                    <asp:Button ID="cmdCancel" runat="server" Text="Hủy" Cssclass="button"/>    
                    <asp:Button ID="cmdCloseModalPopupSalesOrder" runat="server" Text="Đóng" Cssclass="button"/>    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

