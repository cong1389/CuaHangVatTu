<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="AssignRightForGroup.aspx.vb" Inherits="Administrator_Assign_Right_For_Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<div style="background-color:#F1F3F5;">
        <table width="100%">
            <tr>
                <td style="width:202px;">Nhóm người dùng</td>
                <td rowspan="2"><asp:Label ID="LblWarning" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="CboUserGroup" runat="server"  Width="200px" CssClass="inputbox" AutoPostBack="true" OnSelectedIndexChanged="CboUserGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="tabpage">
    <div class="advform">
        <table>
            <tr>
                <td>Mã Nghiệp vụ</td>
                <td>Tên nghiệp vụ</td>
                <td style="width:120px;">Quyền xem</td>
                <td style="width:120px;">Quyền tạo mới</td>
                <td style="width:120px;">Quyền sửa</td>
                <td style="width:120px;">Quyền xóa</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TxtMenuNo" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="TxtMenuName" runat="server" Width="250px"></asp:TextBox></td>
                <td>
                    <asp:CheckBox ID="CKView" runat="server" />
                </td>
                <td><asp:CheckBox ID="CKAddNew" runat="server" /></td>
                <td><asp:CheckBox ID="CKEdit" runat="server" /></td>
                <td><asp:CheckBox ID="CKDelete" runat="server" /></td>
                <td>
                    <asp:Button ID="CmdSave" runat="server" Text="Save"  CssClass="button"/>
                </td>
            </tr>
            
        </table>
    </div>
    <div class="advform">
        <asp:GridView ID="grdRightList" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            ForeColor="Black" GridLines="Vertical" Width="100%" 
            AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="No_" HeaderText="Mã" SortExpression="No_" />
                <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                <asp:CheckBoxField DataField="View Right" HeaderText="View" />
                <asp:CheckBoxField DataField="Add Right" HeaderText="Add new" />
                <asp:CheckBoxField DataField="Edit Right" HeaderText="Edit" />
                <asp:CheckBoxField DataField="Del Right" HeaderText="Delete" />
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

