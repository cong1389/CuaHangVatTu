<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage/MasterSite.master"
    CodeFile="InProcess.aspx.vb" Inherits="InProcess" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
</asp:Content>
<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">
    <div class="c-warp">
        <div id="aliGlobalCrumb" class="crumb">
        </div>
        <h2 class="con-header" style="padding-left: 300px; padding-bottom: 300px; color: Green">
            Đang xử lý vui lòng quay lại sau.
        </h2>
        <div class="con-main">
        </div>
    </div>
</asp:Content>
