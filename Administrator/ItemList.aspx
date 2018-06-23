<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ItemList.aspx.vb" Inherits="Administrator_ItemList"
    MasterPageFile="MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="maincontent">
    <div id="wrapper">
        <div style="background-color: #F1F3F5;">
            <table width="100%">
                <tr>
                    <td style="width: 152px;">
                        Hãng sản xuất
                    </td>
                    <td style="width: 202px;">
                        Nhóm sản phẩm
                    </td>
                    <td style="width: 152px;">
                        Phân loại
                    </td>
                    <td style="width: 132px;">
                        Tình trạng
                    </td>
                    <td style="width: 172px;">
                        Tìm kiếm
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="CboBrand" runat="server" Width="150px" CssClass="inputbox">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="CboMenuGroup" runat="server" Width="200px" CssClass="inputbox">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="CboType" runat="server" CssClass="inputbox" Width="150px">
                            <asp:ListItem Value="0">Chọn loại</asp:ListItem>
                            <asp:ListItem Value="1">Sản phẩm khuyến mãi</asp:ListItem>
                            <asp:ListItem Value="2">Sản phẩm bán chạy</asp:ListItem>
                            <asp:ListItem Value="3">Sản phẩm hot</asp:ListItem>
                            <asp:ListItem Value="4">Sản phẩm mới</asp:ListItem>
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
                    <td>
                        <asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
                    </td>
                    <td align="right">
                        <asp:Button ID="CmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
                        <asp:Button ID="CmdHide" runat="server" Text="Ẩn" CssClass="button" />
                        <asp:Button ID="CmdShow" runat="server" Text="Hiển thị" CssClass="button" />
                        <asp:Button ID="CmdDel" runat="server" Text="Xóa" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <p>
                        Loading</p>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdLoad" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="TxtPageNo" runat="server" />
                    <asp:GridView ID="GrProduct" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                        Width="100%" AllowPaging="True" PageSize="20">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <a href="ItemImage360.aspx?item=<%#Eval("No_")%>">[Hình 360]</a> &nbsp; <a href="ItemGift.aspx?item=<%#Eval("No_")%>">
                                        [Quà tặng]</a>
                                </ItemTemplate>
                                <ControlStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item name">
                                <ItemTemplate>
                                    <a href="ItemCard.aspx?item=<%#Eval("No_")%>">
                                        <%#Eval("Name")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="No_" HeaderText="Item no." SortExpression="No_" />
                            <asp:BoundField DataField="Sales Price" HeaderText="Sales Price" SortExpression="Sales Price" />
                            <asp:BoundField DataField="Deal Price" HeaderText="Deal Price" SortExpression="Deal Price" />
                            <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                            <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
                            <asp:BoundField DataField="Brand No_" HeaderText="Brand No." SortExpression="Brand No_" />
                            <asp:BoundField DataField="Group" HeaderText="Menu group" SortExpression="Group" />
                            <asp:CheckBoxField DataField="Promotions Product" HeaderText="Promotions" />
                            <asp:CheckBoxField DataField="Best Selling" HeaderText="Best selling" />
                            <asp:CheckBoxField DataField="New Product" HeaderText="New" />
                            <asp:CheckBoxField DataField="Published" HeaderText="Show/Hide" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div style="display: none;">
        <asp:Button ID="cmdShowWarning" runat="server" Text="OK" />
    </div>
    <asp:ModalPopupExtender ID="ModalPopupWarning" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="cmdShowWarning" PopupControlID="Panel1" OkControlID="cmdCloseWarning">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="popup">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="txtItemNo" runat="server" />
                <div style="width: 400px; height: 300px;">
                    <div class="popupheader" style="height: 20px;">
                        <b>Liên hoa</b>
                    </div>
                    <div style="height: 210px; padding: 10px;">
                        <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="height: 30px; text-align: right; padding-right: 4px;">
                        <asp:Button ID="cmdOK" runat="server" Text="Đồng ý" class="button" />
                        <asp:Button ID="cmdCloseWarning" runat="server" Text="Đóng" class="button" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
