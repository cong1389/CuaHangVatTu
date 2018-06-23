<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="ShowItemAtHomeList.aspx.vb" Inherits="Administrator_ShowItemAtHomeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<div style="background-color:#F1F3F5;">
        <table width="100%">
            <tr>
              <td style="width:152px;">Phân loại</td>
              <td></td>
              <td></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="CboType" runat="server" CssClass="inputbox" Width="150px">
                        <asp:ListItem Value="0">Chọn loại</asp:ListItem>
                        <asp:ListItem Value="1">Sản phẩm khuyến mãi</asp:ListItem>
                        <asp:ListItem Value="2">Sản hot</asp:ListItem>
                        <asp:ListItem Value="3">Sản mới</asp:ListItem>
                        <asp:ListItem Value="4">Hiển thị theo menu</asp:ListItem>
                        <asp:ListItem Value="5">Danh sách tùy chọn</asp:ListItem>
                        <asp:ListItem Value="6">Sản phẩm bán chạy</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
                <td><asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
                </td>
                <td align="right">
                    <asp:Button ID="CmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
                </td>
            </tr>
        </table>
    </div>
    <div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div style="margin: 0px auto 0px auto;">
        <asp:GridView ID="grdShowList" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            AutoGenerateColumns="False" Width="100%"  AllowPaging="True" PageSize="30" 
            GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="Diễn giải">
                    <itemtemplate>
                        <a href="ShowItemAtHomeCard.aspx?no_=<%#Eval("No_")%>"><%# Eval("Title")%></a>
                    </itemtemplate>
                    <ControlStyle />
                </asp:TemplateField>     
                <asp:BoundField DataField="No_" HeaderText="Mã" SortExpression="No_" />
                <asp:BoundField DataField="Title" HeaderText="Tên" SortExpression="Title" />
                <asp:BoundField DataField="Get Type" HeaderText="Loại" SortExpression="Get Type" />
                <asp:BoundField DataField="Starting Date" HeaderText="Ngày bắt đầu" SortExpression="Starting Date" />
                <asp:BoundField DataField="Ending Date" HeaderText="Ngày kết thúc" SortExpression="Ending Date" />
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

    </ContentTemplate>
</asp:UpdatePanel>
    </div>
</asp:Content>

