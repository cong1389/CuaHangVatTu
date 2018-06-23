<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="FaqsList.aspx.vb" Inherits="Administrator_FaqsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<div style="background-color:#F1F3F5;">
        <table width="100%">
            <tr>
                <td style="width:202px;">Nhóm banner</td>
                <td style="width:132px;">Tình trạng</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="CboBannerGroup" runat="server"  Width="200px" CssClass="inputbox"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="CboStatus" runat="server" Width="130px" CssClass="inputbox">
                        <asp:ListItem Value="0">Chọn tình trạng</asp:ListItem>
                        <asp:ListItem Value="1">Đã published</asp:ListItem>
                        <asp:ListItem Value="2">Chưa published</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
                    <asp:Button ID="cmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
    <div>
    <asp:GridView ID="grdList" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            AutoGenerateColumns="False" Width="100%"  AllowPaging="True" PageSize="30" 
            GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="Câu hỏi">
                    <itemtemplate>
                        <a href="FaqsCard.aspx?lineno=<%#Eval("Line No_")%>"><%# Eval("Question")%></a>
                    </itemtemplate>
                    <ControlStyle />
                </asp:TemplateField>     
                <asp:BoundField DataField="Line No_" HeaderText="Mã" SortExpression="No_" />
                <asp:BoundField DataField="Link" HeaderText="Link" SortExpression="URL Link" />
                <asp:BoundField DataField="Order Position" HeaderText="Stt" SortExpression="Order Position" />
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

