<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="LinkCard.aspx.vb" Inherits="Administrator_LinkCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="Server">
    <div style="padding: 5px;">
        <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="button" />
        <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
    </div>
    <div style="width: 1000px; margin: 20px auto 0px auto;">
        <asp:HiddenField ID="LinkMenuNo" runat="server" />
        <table>
            <tr>
                <td style="width: 100px;">Mã</td>
                <td style="width: 250px;">
                    <asp:TextBox ID="TxtNo_" runat="server" Width="200px"></asp:TextBox></td>
                <td style="width: 100px;">Diễn giải</td>
                <td>
                    <asp:TextBox ID="TxtName" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Loại</td>
                <td>
                    <asp:DropDownList ID="CboType" runat="server" Width="205px" CssClass="inputbox">
                        <asp:ListItem Value="0">Link</asp:ListItem>
                        <asp:ListItem Value="1">Link Group</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Thuộc nhóm</td>
                <td>
                    <asp:DropDownList ID="CboLinkGroup" runat="server" Width="305px" CssClass="inputbox"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Vị trí</td>
                <td>
                    <asp:DropDownList ID="CboPosition" runat="server" Width="205px" CssClass="inputbox"></asp:DropDownList>
                </td>
                <td>Link URL</td>
                <td>
                    <asp:TextBox ID="TxtLink" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Css class</td>
                <td>
                    <asp:TextBox ID="TxtCss" runat="server" Width="200px"></asp:TextBox></td>
                <td>Float</td>
                <td>
                    <asp:DropDownList ID="CboFloat" runat="server" Width="205px" CssClass="inputbox">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="0">Thẻ div</asp:ListItem>
                        <asp:ListItem Value="1">Left</asp:ListItem>
                        <asp:ListItem Value="2">Right</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Mở cửa sổ mới</td>
                <td>
                    <asp:CheckBox ID="CKNewWindow" runat="server" />
                </td>
                <td>Thứ tự</td>
                <td>
                    <asp:TextBox ID="TxtPosition" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Menu hàng hóa</td>
                <td>
                    <asp:DropDownList ID="CboMenuGroup" runat="server" Width="205px" CssClass="inputbox"></asp:DropDownList></td>
                <td>Tình trạng login</td>
                <td>
                    <asp:DropDownList ID="CboLoginStatus" runat="server" Width="205px" CssClass="inputbox">
                        <asp:ListItem Value="0">Luôn hiển thị</asp:ListItem>
                        <asp:ListItem Value="1">Chưa login</asp:ListItem>
                        <asp:ListItem Value="2">Đã login</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <script>
        $(document).ready(function () {
            CopyValue();
        });

        function CopyValue() {

            //VN
            $("#<%=TxtName.ClientID%>").change(function () {
                var name = $("#<%=TxtName.ClientID%>").val();
                if (name != "") {
                    $("#<%=TxtLink.ClientID%>").val(RemoveUnicode(name));
                }

            });

        };
    </script>

</asp:Content>

