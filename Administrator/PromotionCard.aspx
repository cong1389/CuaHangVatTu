<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="PromotionCard.aspx.vb" Inherits="Administrator_PromotionCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="Server">
    <div class="toolbox">
        <table>
            <tr>
                <td>
                    <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
                    &nbsp;
            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="button" /></td>
                <td><b>
                    <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label></b></td>
            </tr>
        </table>
    </div>
    <div style="width: 1000px; margin: 0px auto 0px auto;">
        <table cellspacing="1" style="background: #F1F3F5">
            <tr>
                <td class="title" style="width: 100px;">Mã</td>
                <td>
                    <asp:TextBox ID="TxtNo_" runat="server" Width="120px"></asp:TextBox></td>
                <td class="title">Tên</td>
                <td>
                    <asp:TextBox ID="TxtName" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="title">Từ ngày</td>
                <td>
                    <asp:TextBox ID="TxtStartingDate" runat="server" Width="120px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="TxtStartingDate" Format="dd/MM/yyyy" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td class="title">Đến ngày</td>
                <td>
                    <asp:TextBox ID="TxtEndingDate" runat="server" Width="120px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                        TargetControlID="TxtEndingDate" Format="dd/MM/yyyy" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="title">Loại KM</td>
                <td>
                    <asp:DropDownList ID="CboDiscountType" runat="server" CssClass="inputbox" Width="200px">
                        <asp:ListItem Value="0">Áp dụng cho tất cả</asp:ListItem>
                        <asp:ListItem Value="1">Áp dụng cho loại hàng</asp:ListItem>
                        <asp:ListItem Value="2">Áp dụng cho một sản phẩm</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="title">Nhóm hàng</td>
                <td>
                    <asp:DropDownList ID="CboMenuGroup" runat="server" Width="200px" CssClass="inputbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="title">Thương hiệu</td>
                <td>
                    <asp:DropDownList ID="DrdBrand" runat="server" CssClass="inputbox" Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="title">Sản phẩm</td>
                <td>
                    <asp:TextBox ID="TxtItemNo" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="title">Active</td>
                <td colspan="3">
                    <asp:CheckBox ID="CKPublished" runat="server" /></td>
            </tr>
            <tr>
                <td class="title" valign="top">Mô tả</td>
                <td colspan="3">
                    <FCKeditorV2:FCKeditor ID="TxtContent" runat="server" BasePath="~/administrator/fckeditor/" Height="400px" AutoDetectLanguage="false" DefaultLanguage="en" Width="850">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
        </table>
    </div>
    

</asp:Content>

