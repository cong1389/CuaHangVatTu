<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="SpecialEventsCard.aspx.vb" Inherits="Administrator_ContentConsultantCard" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">

 <div class="toolbox">
    <div style="float:left; padding:6px;"><b>
        <asp:Label ID="LblWarning1" runat="server" Text=""></asp:Label></b></div>
        <div style="float:right;margin-right:5px;">
            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
            <asp:Button ID="cmdUnSave" runat="server" Text="Hủy" CssClass="button" />
        </div>
    <div style="clear:both;">
    </div>
</div>
<table width="1000">
    <tr>
        <td>Tiêu đề</td>
        <td>
            <asp:HiddenField ID="TxtContentNo" runat="server" />
            <asp:TextBox ID="TxtTitle" runat="server" Width="350"></asp:TextBox></td>
        <td>Nhóm nội dung</td>
        <td><asp:DropDownList ID="CboGroupContent" runat="server"  Width="200px" 
                                        CssClass="inputbox"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Hiển thị</td>
        <td ><asp:CheckBox ID="CKPublished" runat="server" /></td>
        <td class="title">Nhóm menu</td>
        <td >
            <asp:DropDownList ID="CboMenuGroup" runat="server"  Width="200px" CssClass="inputbox"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Link</td>
        <td>
            <asp:TextBox ID="TxtLinkMenu" runat="server" Width="350"></asp:TextBox></td>
        <td>Số thứ tự</td>
        <td>
            <asp:TextBox ID="TxtOrderPosition" runat="server" Width="100"></asp:TextBox></td>
    </tr>
</table>
<div>
    <FCKeditorV2:FCKeditor ID="TxtContent" runat="server" BasePath="~/administrator/fckeditor/" Height="500px" AutoDetectLanguage="false" DefaultLanguage="en" Width="1000">
    </FCKeditorV2:FCKeditor>      
</div>

</asp:Content>

