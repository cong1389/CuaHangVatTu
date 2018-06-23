<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="FaqsCard.aspx.vb" Inherits="Administrator_FaqsCard" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="toolbox">
                <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
                <asp:Button ID="cmdUnSave" runat="server" Text="Hủy" CssClass="button" />
                <asp:Label ID="LblWarning1" runat="server" Text=""></asp:Label>
            </div>
            <div style=" width: 1000px;background-color: #fff;margin: 0px auto 0px auto;padding:15px 5px 5px 5px;">
            <table width="1000">
                <tr>
                    <td valign="top" style="width:150px;">Câu hỏi</td>
                    <td colspan="5">
                        <asp:TextBox ID="TxtQuestion" runat="server" Width="900" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        <asp:HiddenField ID="TxtLineNo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Link</td>
                    <td>
                        <asp:TextBox ID="TxtLinkMenu" runat="server" Width="350"></asp:TextBox></td>
                    <td>Số thứ tự</td>
                    <td><asp:TextBox ID="TxtOrderPosition" runat="server" Width="150"></asp:TextBox></td>
                    <td>Hiển thị</td>
                    <td><asp:CheckBox ID="CKPublished" runat="server" /></td>
                </tr>
            </table>
            <div style="margin-top:10px;">Trả lời</div>
            <div>
                <FCKeditorV2:FCKeditor ID="TxtContent" runat="server" BasePath="~/administrator/fckeditor/" Height="500px" AutoDetectLanguage="false" DefaultLanguage="en" Width="1000">
                </FCKeditorV2:FCKeditor>      
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

