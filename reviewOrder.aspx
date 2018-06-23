<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterSite.master" AutoEventWireup="false"
    CodeFile="reviewOrder.aspx.vb" Inherits="reviewOrder" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phMainContent" runat="Server">
    <div class="pathway">
    </div>
    <div class="TitleLine">
        Đơn hàng của tôi</div>
    <div style="width: 900px; margin: 20px auto 0px auto;" class="order-list">
        <table>
            <thead>
                <tr>
                    <th>
                        Đơn hàng
                    </th>
                    <th>
                        Ngày mua
                    </th>
                    <th>
                        Gửi đến
                    </th>
                    <th>
                        Trạng thái đơn hàng
                    </th>
                    <th>
                        &nbsp;
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="order-number">
                        <asp:Label ID="lblNuber" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </td>
                    <td class="status">
                        <asp:Label ID="lblstatus" runat="server"></asp:Label>
                    </td>
                    <td class="view-detail">
                        <asp:Literal ID="ltrViewDetail" runat="server"></asp:Literal>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
