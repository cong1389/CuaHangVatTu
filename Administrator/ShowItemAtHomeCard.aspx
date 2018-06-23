<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="ShowItemAtHomeCard.aspx.vb" Inherits="Administrator_ShowItemAtHomeCard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<div class="toolbox">
    <div style="float:left; padding:6px;"><b>
        <asp:Label ID="LblWarning1" runat="server" Text=""></asp:Label></b></div>
        <div style="float:left;margin-right:5px;">
            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
            <asp:Button ID="cmdUnSave" runat="server" Text="Hủy" CssClass="button" />
        </div>
    <div style="clear:both;">
    </div>
</div>
<div style="width:1000px;margin:0px auto 0px auto;">
    <table>
        <tr>    
            <td style="width:120px;">Tiêu đề</td>
            <td>
                <asp:HiddenField ID="TxtNo_" runat="server" />    
                <asp:TextBox ID="TxtName" runat="server" Width="300"></asp:TextBox>
            </td>
            <td style="width:120px;">Loại hiển thị</td>
            <td>
                <asp:DropDownList ID="CboShowType" runat="server" CssClass ="inputbox" Width="300">
                    <asp:ListItem Value="0" >Chọn </asp:ListItem>
                    <asp:ListItem Value="1" >Sản phẩm khuyến mãi</asp:ListItem>
                    <asp:ListItem Value="2" >Sản phẩm hot</asp:ListItem>
                    <asp:ListItem Value="3" >Sản phẩm mới</asp:ListItem>
                    <asp:ListItem Value="4" >Danh sách theo menu</asp:ListItem>
                    <asp:ListItem Value="5" >Danh sách tùy chọn</asp:ListItem>
                    <asp:ListItem Value="6" >Sản phẩm bán chạy</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td  class="title">Từ ngày</td>
            <td ><asp:TextBox ID="TxtStartingDate" runat="server" Width="120px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                            TargetControlID="TxtStartingDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
            </td>
            <td class="title">Đến ngày</td>
            <td ><asp:TextBox ID="TxtEndingDate" runat="server" Width="120px"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                            TargetControlID="TxtEndingDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="title">Nhóm menu</td>
            <td >
                <asp:DropDownList ID="CboMenuGroup" runat="server"  Width="300px" CssClass="inputbox"></asp:DropDownList>
            </td>
            <td  class="title">Số sp hiển thị</td>
            <td ><asp:TextBox ID="TxtNumItem" runat="server" Width="120px"></asp:TextBox>
                
            </td>
        </tr>
        <tr>    
            <td>URL Link</td>
            <td><asp:TextBox ID="TxtUrlPage" runat="server" Width="300"></asp:TextBox></td>
            <td  class="title">Số thứ tự</td>
            <td ><asp:TextBox ID="TxtOrderPosition" runat="server" Width="120px"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CKShow" runat="server" Checked="True" /> Hiển thị
            </td>

        </tr>
    </table>
</div>
</asp:Content>

