<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="ConsultantContentCard.aspx.vb" Inherits="Administrator_PromotionContentCard" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

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
<div style="width:1010px; margin: 0px auto 0px auto;">
    <table cellspacing="1" style="background:#F1F3F5;">
        <tr>
            <td  class="title" style="width:100px;">Mã</td>
            <td ><asp:TextBox ID="TxtNo_" runat="server" Width="120px" ReadOnly="True"></asp:TextBox></td>
            <td class="title">Tên</td>
            <td ><asp:TextBox ID="TxtName" runat="server" Width="300px"></asp:TextBox></td>
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
            <td class="title">Link</td>
            <td ><asp:TextBox ID="TxtLinkContent" runat="server" Width="300px"></asp:TextBox></td>
            <td class="title">Màu nền </td>
            <td>
                <asp:Panel ID="PanelColor" style="width:18px;height:18px;border:1px solid #000;float:left;margin-top:2px;" runat="server" />
                &nbsp;
                <asp:TextBox ID="TxtColor" runat="server" Width="95px"></asp:TextBox>
                    <asp:ColorPickerExtender
                ID="ColorPickerExtender1" runat="server" TargetControlID="TxtColor" SampleControlID="PanelColor">
                    </asp:ColorPickerExtender>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CKPublished" runat="server" Checked="True" Text="Active"/>
            </td>
        </tr>
        <tr>
            <td colspan="4" >
                 <FCKeditorV2:FCKeditor ID="TxtContent" runat="server" BasePath="~/administrator/fckeditor/" Height="500px" AutoDetectLanguage="false" DefaultLanguage="en" Width="1010">
                  </FCKeditorV2:FCKeditor>                     
            </td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
    </table>
</div>
</asp:Content>

