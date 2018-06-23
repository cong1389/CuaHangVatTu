<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="MenuGroupList.aspx.vb" Inherits="Administrator_MenuGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
    <div style="background-color:#F1F3F5;padding:8px; height:20px;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width:50%"><b>CẬP NHẬT MENU NHÓM SẢN PHẨM</b></td>
                <td style="width:50%" align="right">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width:150px;padding-right:10px;">Loại</td>
                <td style="width:250px;padding-right:10px;">Nhóm cha</td>
                <td style="width:150px;padding-right:10px;">Mã</td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>

                    <asp:DropDownList ID="CboType" runat="server" CssClass="inputbox" Width="150px">
                        <asp:ListItem Value="">All</asp:ListItem>
                        <asp:ListItem Value="0">Division</asp:ListItem>
                        <asp:ListItem Value="1">Category</asp:ListItem>
                        <asp:ListItem Value="2">Group</asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td>
                     <asp:DropDownList ID="CboMenuGroup" runat="server" CssClass="inputbox" Width="250px">
                                        </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="TxtNo_" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="cmdLoad" runat="server" Text="Load"  CssClass="button"/>
                    <asp:Button ID="cmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
                </td>
            </tr>

        </table>
    </div>
    <div style="margin: 0px auto 0px auto;">
        <asp:GridView ID="grdMenuGroup" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            AutoGenerateColumns="False" Width="100%"  AllowPaging="True" PageSize="20" 
            GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="No_" HeaderText="Mã" SortExpression="No_" />
                <asp:TemplateField HeaderText="Diễn giải">
                    <itemtemplate>
                        <a href="MenuGroupCard.aspx?menu=<%#Eval("No_")%>"><%#Eval("Menu Name")%></a>
                    </itemtemplate>
                    <ControlStyle />
                </asp:TemplateField>     
                <asp:BoundField DataField="Type" HeaderText="Loại" SortExpression="Type" />
                <asp:BoundField DataField="Menu Order" HeaderText="Stt" 
                    SortExpression="Menu Order" />
                <asp:BoundField DataField="Parent Menu" HeaderText="Nhóm menu" 
                    SortExpression="Parent Menu" />
                <asp:BoundField DataField="Link Display" HeaderText="Đường dẫn hiển thị" 
                    SortExpression="Link Display" />
                <asp:CheckBoxField DataField="Published" HeaderText="Hiển thị" />
                <asp:CommandField DeleteText="[Xóa]" ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle ForeColor="Black" BackColor="#EEEEEE" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
    </div>

</asp:Content>

