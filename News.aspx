<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage/MasterSite.master"
    CodeFile="News.aspx.vb" Inherits="News" %>

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

    <!--News-->
    <asp:HiddenField ID="hdCateName" runat="server" />
    <asp:HiddenField ID="hdBrandName" runat="server" />

    <div class="collection">
        <div class="columns-container">
            <div id="columns" class="container">

                <div class="breadcrumb clearfix">
                    <div class="col-md-12 ">
                        <div class="util-clearfix" id="breadView">
                            <div id="aliGlobalCrumb" class="ui-breadcrumb textuppercase">
                                <a href="<%= GetImgUrl() + "Default.aspx" %>" title="Home">Trang chủ </a><span class="divider">&gt;</span> <a href="<%=GetImgUrl() + "tin-tuc/"%>" rel="nofollow" title="Tin tức">Tin tức</a>
                                <asp:Literal ID="ltrRelatedCategory" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="column col-xs-12 col-sm-3" id="left_column">

                        <div class="block left-module">

                            <dl class="category-list  ">
                                <p class="title_block">Danh mục sản phẩm</p>
                                <dd class="block_content">
                                    <dl class="layered layered-category">
                                        <asp:Literal ID="ltrShowRelatedLeft" runat="server"></asp:Literal>
                                    </dl>
                                </dd>
                            </dl>

                        </div>

                        <div class="block left-module hidden-xs">
                            <p class="title_block">Sản phẩm tiêu biểu</p>
                            <div class="block_content">

                                <ul id="p4p-side-content" class="products-block best-sell">
                                    <asp:Repeater ID="rptHotProduct" runat="server" OnItemDataBound="rptHotProduct_ItemDataBound">
                                        <ItemTemplate>
                                            <li>
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
                        <div id="view-product-list" class="ui-slidebox-container">

                            <div id="list-items" class="ui-slidebox-slider sale-off temp-height lazy-load">
                                <asp:Repeater ID="rptProductList" runat="server" OnItemDataBound="rptProductList_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrUL" runat="server"></asp:Literal>
                                        <li class="col-lg-3 col-md-3 col-sm-6 col-xs-6 noPM">
                                            <div class="product-container">

                                                <div class="left-block">

                                                    <asp:Literal ID="ltrpicCore" runat="server"></asp:Literal>

                                                </div>

                                                <div class="right-block bc-nowrap-ellipsis">
                                                    <h5 class="product-name">
                                                        <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                                    </h5>
                                                    <div class="content_price">
                                                        <div class="price product-price" itemprop="price">
                                                            <asp:Literal ID="ltrPrice" runat="server"></asp:Literal>
                                                        </div>
                                                        <asp:Literal ID="ltrOrgPrice" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                        </li>
                                        <asp:Literal ID="ltrEndUL" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div style="color: Red; text-align: center; font-weight: bold">
                                    <asp:Label ID="lblNodata" Visible="false" runat="server" Text="Không có dữ liệu"></asp:Label>
                                </div>
                            </div>

                            <uc1:CtrListPage ID="CtrListPage1" runat="server"></uc1:CtrListPage>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
</asp:Content>
