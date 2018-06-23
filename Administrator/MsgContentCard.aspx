﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="MsgContentCard.aspx.vb" Inherits="Administrator_MsgContentCard" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="Server">
    <div class="toolbox">
        <div style="float: left; padding: 6px;">
            <b>
                <asp:Label ID="LblWarning1" runat="server" Text=""></asp:Label></b>
        </div>
        <div style="float: right; margin-right: 5px;">
            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
            <asp:Button ID="cmdUnSave" runat="server" Text="Hủy" CssClass="button" />
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div style="width: 1000px; background-color: #fff; margin: 0px auto 0px auto;">
        <table width="1000">
            <tr>

                <td>Mã</td>
                <td>
                    <asp:TextBox ID="TxtNo" runat="server" Width="350"></asp:TextBox>
                </td>
                <td>Tiêu đề</td>
                <td>
                    <asp:TextBox ID="TxtName" runat="server" Width="350"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Link</td>
                <td>
                    <asp:TextBox ID="TxtLinkMenu" runat="server" Width="350"></asp:TextBox></td>
                <td></td>
                <td></td>
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
            $("#<%=TxtName.ClientID%>").change(function () {
                var name = $("#<%=TxtName.ClientID%>").val();
                if (name != "") {
                    $("#<%=TxtLinkMenu.ClientID%>").val(RemoveUnicode(name));
                }

            });

        };
    </script>

</asp:Content>
