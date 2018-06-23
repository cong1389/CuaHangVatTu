<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="TechnologyContentCard.aspx.vb" Inherits="Administrator_ContentCard" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="Server">
    <div class="toolbox">
        <div style="float: left; padding: 6px;">
            <b>
                <asp:Label ID="LblWarning1" runat="server" Text=""></asp:Label></b>
        </div>
        <div style="float: left; margin-left: 5px;">
            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
            <asp:Button ID="cmdUnSave" runat="server" Text="Hủy" CssClass="button" />
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div style="width: 1000px; background-color: #fff; margin: 0px auto 0px auto;">
        <table width="1000">
            <tr>
                <td>Hiển thị</td>
                <td>
                    <asp:CheckBox ID="CKPublished" runat="server" />
                </td>
                <td>Nhóm nội dung</td>
                <td>
                    <asp:DropDownList ID="CboContentType" runat="server" Width="200px" AutoPostBack="true"
                        OnSelectedIndexChanged="CboContentType_SelectedIndexChanged"
                        CssClass="inputbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Tiêu đề</td>
                <td>
                    <asp:HiddenField ID="TxtContentNo" runat="server" />
                    <asp:TextBox ID="TxtTitle" runat="server" Width="350"></asp:TextBox></td>
                <td>Nhóm nội dung</td>
                <td>
                    <asp:DropDownList ID="CboGroupContent" runat="server" Width="200px"
                        CssClass="inputbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Tin nóng</td>
                <td>
                    <asp:CheckBox ID="CKHotContent" runat="server" />
                </td>
                <td class="title">Nhóm menu</td>
                <td>
                    <asp:DropDownList ID="CboMenuGroup" runat="server" Width="200px" CssClass="inputbox"></asp:DropDownList>
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
            <tr>
                <td valign="top">Nội dung tóm tắt</td>
                <td colspan="3">
                    <asp:TextBox ID="TxtSumary" runat="server" Rows="4" MaxLength="1000" TextMode="MultiLine" Columns="110" Width="800px" />
                </td>
            </tr>
            <tr>
                <td valign="top">Meta keywords</td>
                <td colspan="3">
                    <asp:TextBox ID="TxtMetaKeywords" runat="server" Rows="2" MaxLength="1000" TextMode="MultiLine" Columns="110" Width="800px" />
                </td>
            </tr>
            <tr>
                <td>Ảnh đại diện</td>
                <td>
                    <asp:Image ID="ImgContentRepresentative" runat="server" Width="120px" />
                </td>
                <td colspan="2">
                    <asp:FileUpload ID="FileUploadImg" runat="server"
                        Width="180px" CssClass="inputbox" Height="20px" /></td>
            </tr>
        </table>
        <div>
            <FCKeditorV2:FCKeditor ID="TxtContent" runat="server" BasePath="~/administrator/fckeditor/" Height="500px" AutoDetectLanguage="false" DefaultLanguage="en" Width="1000">
            </FCKeditorV2:FCKeditor>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            CopyValue();
        });

        function CopyValue() {

            //VN
            $("#<%=TxtTitle.ClientID%>").change(function () {
                var name = $("#<%=TxtTitle.ClientID%>").val();
                if (name != "") {
                    $("#<%=TxtLinkMenu.ClientID%>").val(RemoveUnicode(name));
                }

            });

        };
    </script>

</asp:Content>

