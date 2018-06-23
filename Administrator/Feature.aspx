<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="Feature.aspx.vb" Inherits="Administrator_Feature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<div class="tabpage">
    <div class="advform">
        <table>
            <tr>
                <td>Mã</td>
                <td>Tên</td>
                <td>Loại</td>
                <td>Mô tả</td>
                <td></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TxtNo" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="TxtName" runat="server" Width="250px"></asp:TextBox></td>
                <td>
                    <asp:DropDownList ID="CboType" runat="server" CssClass="inputbox" Width="150px">
                        <asp:ListItem Value="0">Nhóm thuộc tính</asp:ListItem>
                        <asp:ListItem Value="1">Thuộc tính</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><asp:TextBox ID="TxtDescriptions" runat="server" Width="250px"></asp:TextBox></td>
                <td>
                    <asp:HiddenField ID="TxtOldNo" runat="server" />
                     <asp:Button ID="cmdNew" runat="server" Text="New"  CssClass="button"/>
                    <asp:Button ID="cmdSave" runat="server" Text="Save"  CssClass="button"/>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="advform">
        <asp:GridView ID="grdFeatureList" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            ForeColor="Black" GridLines="Vertical" Width="100%" 
            AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="No_" HeaderText="Mã" SortExpression="No_" />
                <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                <asp:BoundField DataField="Type" HeaderText="Loại" SortExpression="Type" />
                <asp:BoundField DataField="Descriptions" HeaderText="Mô tả" 
                    SortExpression="Descriptions" />
                <asp:CommandField ShowSelectButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
</div>


</asp:Content>

