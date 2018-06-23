<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterSite.master" AutoEventWireup="false"
    CodeFile="Complete.aspx.vb" Inherits="Complete" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phMainContent" runat="Server">
    <div class="container">
        <div class="pathway">
            <asp:Label ID="LblPathWay" runat="server" Text="Label"></asp:Label>
        </div>
        <div>
            <div class="TitleLine">
                Hoàn tất đơn hàng</div>
            <div class="col-lg-9 col-lg-offset-2">
                <asp:Label ID="LblDescription" runat="server" Text=""></asp:Label></div>
        </div>
    </div>
</asp:Content>
