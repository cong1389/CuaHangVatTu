<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="BannerCard.aspx.vb" Inherits="Administrator_BannerCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="Server">
    <div class="toolbox">
        <table>
            <tr>
                <td>
                    <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" /><asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="button" /></td>
                <td><b>
                    <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label></b></td>
            </tr>
        </table>
    </div>
    <div style="width: 1000px; margin: 0px auto 0px auto;">
        <table cellspacing="2">
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>

            <tr>
                <td class="title">Hiển thị</td>
                <td>
                    <asp:HiddenField ID="TxtNo_" runat="server" />
                    <asp:CheckBox ID="CKPublished" runat="server" />
                    Mở cửa sổ mới<asp:CheckBox ID="CKNewWindow" runat="server" />
                </td>

                <td class="title">Trang</td>
                <td>
                    <asp:DropDownList ID="CboPage" runat="server" Width="200px" CssClass="inputbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="title">Nhóm banner</td>
                <td>
                    <asp:DropDownList ID="CboBannerGroup" runat="server" Width="200px" CssClass="inputbox"></asp:DropDownList>
                </td>

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
                <td class="title">Số thứ tự</td>
                <td>
                    <asp:TextBox ID="TxtOrderPosition" runat="server" Width="120px"></asp:TextBox></td>
                <td class="title">Link</td>
                <td>
                    <asp:TextBox ID="TxtUrl" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="title">Nhóm menu</td>
                <td>
                    <asp:DropDownList ID="CboMenuGroup" runat="server" Width="200px" CssClass="inputbox"></asp:DropDownList>
                </td>
                <td class="title">Số lần click</td>
                <td>
                    <asp:TextBox ID="TxtNumClick" runat="server" Width="120px"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td class="title">File hình</td>
                <td>
                    <asp:DropDownList ID="CboImageFile" runat="server" Width="200px" CssClass="inputbox" AutoPostBack="true"
                        OnSelectedIndexChanged="CboImageFile_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="title">Chọn hình Upload</td>
                <td>
                    <asp:FileUpload ID="UplImgBanner" runat="server" />
                    <asp:Button ID="CmdUpload" runat="server" Text="Upload" CssClass="button" />
                </td>
            </tr>

            <tr>

                <td class="title">Chạy</td>
                <td>
                    <asp:CheckBox ID="ckRun" runat="server" />

                </td>

                <td></td>
                <td></td>

            </tr>
            <tr>
                <td></td>
                <td colspan="3" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Image ID="ImgBanner" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="CboImageFile" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>

            <tr>
                <td colspan="4">&nbsp;</td>
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
                    $("#<%=TxtUrl.ClientID%>").val(RemoveUnicode(name));
                }

            });

        };
    </script>

</asp:Content>

