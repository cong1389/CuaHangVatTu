<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage/MasterSite.master"
    CodeFile="StaticContent.aspx.vb" Inherits="StaticContent" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
</asp:Content>
<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">
    <style>
        .con-main
        {
            border: 1px #d1d1d1 solid;
            border-top: 2px #999 solid;
            padding: 15px;
            padding-bottom: 0px;
        }
        .c-warp
        {
            width: 990px;
            margin: 0 auto;
            font: 12px/1 Arial,Helvetica,sans-serif;
            padding-top: 40px;
        }
        .con-main h3
        {
            color: #333;
            line-height: 25px;
            margin: 0;
        }
        .con-main p
        {
            line-height: 20px;
            color: #666;
            margin: 5px 0;
        }
    </style>
    <div class="c-warp">
        <div id="aliGlobalCrumb" class="crumb">
        </div>
        <h2 class="con-header">
            <asp:Literal runat="server" ID="ltrHeader"></asp:Literal></h2>
        <div class="con-main">
            <asp:Literal runat="server" ID="ltrContent"></asp:Literal>
        </div>
    </div>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
</asp:Content>
