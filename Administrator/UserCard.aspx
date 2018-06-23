<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="UserCard.aspx.vb" Inherits="Administrator_UserCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">

<div class="toolbox">
    <table>
        <tr>
            <td><asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
            &nbsp;
            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="button" /></td>
            <td><b><asp:Label ID="LblWarning" runat="server" Text=""></asp:Label></b></td>
        </tr>
    </table>
</div>
<br />
<br />
<div style="width:1000px; margin: 0px auto 0px auto;">
    <table cellspacing="1" style="background:#F1F3F5" width="100%">
        <tr>
            <td colspan="4"><br />
                <asp:HiddenField ID="TxtUserNameOld" runat="server" />
            </td>
        </tr>
        <tr>
            <td  class="title" style="width:100px;">User name</td>
            <td ><asp:TextBox ID="TxtUserName" runat="server" Width="120px"></asp:TextBox></td>
            <td class="title">Full Name</td>
            <td ><asp:TextBox ID="TxtFullName" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td class="title">Mật khẩu</td>
            <td >
                <asp:TextBox ID="TxtPassowrd" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
            </td>
            <td class="title">Xác nhận lại mật khẩu</td>
            <td >
                <asp:TextBox ID="TxtConfirmPassword" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="title">Nhóm người dùng</td>
            <td>
                <asp:DropDownList ID="DrdUserGroup" runat="server" CssClass="inputbox" Width="200px">
                </asp:DropDownList>
            </td>
            <td class="title">Ngành hàng</td>
            <td >
                <asp:TextBox ID="TxtDivision" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>

         <tr>
            <td  class="title" style="width:100px;">Email</td>
            <td ><asp:TextBox ID="TxtEmail" runat="server" Width="300px"></asp:TextBox></td>
            <td class="title">Active</td>
            <td ><asp:CheckBox ID="CKPublished" runat="server" /></td>
        </tr>
        
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
    </table>
</div>

</asp:Content>

