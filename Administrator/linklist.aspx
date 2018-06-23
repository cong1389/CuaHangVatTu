<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="LinkList.aspx.vb" Inherits="Administrator_linklist" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<table>
    <tr>
        <td style="width:150px;">Loại</td>
        <td style="width:150px;">Nhóm</td>
        <td></td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="CboType" runat="server"  Width="200px" CssClass="inputbox"></asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="CboPosition" runat="server"  Width="200px" CssClass="inputbox"></asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
            <asp:Button ID="cmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
            
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div style="margin: 0px auto 0px auto;">
        <asp:GridView ID="grdLinkList" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            AutoGenerateColumns="False" Width="100%"  AllowPaging="True" PageSize="30" 
            GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="Diễn giải">
                    <itemtemplate>
                        <a href="linkcard.aspx?linkmenuno=<%#Eval("No_")%>"><%# Eval("Name")%></a>
                    </itemtemplate>
                    <ControlStyle />
                </asp:TemplateField>     
                <asp:BoundField DataField="No_" HeaderText="Mã" SortExpression="No_" />
                <asp:BoundField DataField="Postition" HeaderText="Vị trí" SortExpression="Postition" />
                <asp:BoundField DataField="URL Link" HeaderText="Link" SortExpression="URL Link" />
                <asp:BoundField DataField="Class CSS" HeaderText="CSS" SortExpression="Class CSS" />
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

    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

