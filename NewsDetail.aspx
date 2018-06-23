<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage/MasterSite.master"
    CodeFile="NewsDetail.aspx.vb" Inherits="NewsDetail" %>

<%@ Register Src="~/Common/CtrListPage.ascx" TagName="CtrListPage" TagPrefix="uc1" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/jquery-ui.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/Category-list.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/catlist1.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/SlicePrice.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/header.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/subCategory.css"%>" />
</asp:Content>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="phMainContent">

    <asp:HiddenField ID="hdCateName" runat="server" />
    <asp:HiddenField ID="hdBrandName" runat="server" />

    <div id="product" class="base">
        <div class="container bc-not-standard no-standard-more-eight bc-category-floor-title">

            <div class="breadcrumb clearfix">
                <div class="col-md-12 ">
                    <div class="util-clearfix" id="Div1">
                        <div id="Div2" class="ui-breadcrumb textuppercase">
                            <a href="<%= GetImgUrl() + "Default.aspx" %>" title="Home">Trang chủ </a><span class="divider">&gt;</span> <a href="<%=GetImgUrl() + "tin-tuc/"%>" rel="nofollow" title="Tin tức">Tin tức</a>
                            <asp:Literal ID="ltrRelatedCategory" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">

                <div class="column col-xs-12 col-sm-3" id="left_column">
                    <div class="block left-module">
                        <div class="bc-list-title bc-title bc-nowrap-ellipsis" title="Categories">Loại sản phẩm</div>
                        <div id="refine-category-list" class="block_content">
                            <div class="layered layered-category">
                                <div class="list-box">
                                    <dl class="son-category">
                                        <asp:Literal ID="ltrShowRelatedLeft" runat="server"></asp:Literal>
                                    </dl>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="block left-module">
                        <p class="title_block">Sản phẩm tiêu biểu</p>

                        <div class="block_content">
                            <ul id="p4p-side-content" class="products-block best-sell">
                                <asp:Repeater ID="rptHotProduct" runat="server" OnItemDataBound="rptHotProduct_ItemDataBound">
                                    <ItemTemplate>
                                        <li class="p4p-list-item">
                                            <div class="products-block-left">
                                                <asp:HyperLink ID="hpImageProducts" runat="server">
                                                    <asp:Image ID="imgProduct" runat="server" />
                                                </asp:HyperLink>
                                            </div>
                                            <div class="products-block-right">
                                                <p class="product-name">
                                                    <asp:HyperLink ID="hpNameProduct" runat="server"></asp:HyperLink>
                                                </p>
                                                <p class="product-price">
                                                    <asp:Literal ID="ltrPrice" runat="server"></asp:Literal>
                                                </p>
                                                <p class="product-price">
                                                    <asp:Literal ID="ltrOrgPrice" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>

                </div>

                <div class="center_column col-xs-12 col-sm-9 product-col" id="center_column">
                    <div id="view-product-list" class="view-product-list">
                        <div id="gallery-item">
                            <div id="aliGlobalCrumb" class="ui-breadcrumb">
                                <%--  <a href="<%= GetImgUrl() + "Default.aspx" %>" title="Home">Trang chủ </a><span class="divider">&gt;</span> <a href="<%=GetImgUrl() + "tin-tuc/"%>" rel="nofollow" title="Tin tức">Tin tức</a>--%>
                                <h1>
                                    <%--  <asp:Literal ID="ltrRelatedCategory" runat="server"></asp:Literal>--%>
                                </h1>
                            </div>
                        </div>
                        <div id="gallery-item">
                            <div id="list-items" class="clearfix temp-height lazy-load">
                                <asp:Literal runat="server" ID="ltrContent"></asp:Literal>
                            </div>
                        </div>
                        <uc1:CtrListPage ID="CtrListPage1" runat="server"></uc1:CtrListPage>
                        <div id="secure-info" class="secure-info">
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
</asp:Content>
