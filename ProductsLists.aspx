<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterSite.master"
    CodeFile="ProductsLists.aspx.vb" Inherits="ProductsLists" %>

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

    <div class="collection">
        <div class="columns-container">
            <div id="columns" class="container">

                <div class="breadcrumb clearfix">
                    <div class="col-md-12 ">
                        <div class="util-clearfix" id="Div1">
                            <div id="Div2" class="ui-breadcrumb textuppercase">
                                <a href="<%= GetImgUrl() + "Default.aspx" %>" title="Home">Trang chủ </a><span class="divider">&gt;</span> <a href="<%=GetUrl()+"all-category/" %>" rel="nofollow" title="All Categories">Tất cả</a> <span class="divider">&gt;</span>
                                <asp:Literal ID="ltrRelatedCategory" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="column col-xs-12 col-sm-3" id="left_column">
                        <div class="block left-module">
                            <div class="bc-list-title bc-title bc-nowrap-ellipsis" title="Categories">Loại sản phẩm</div>
                            <div class="block_content">
                                <dd class="list-box">
                                    <dl class="son-category">
                                        <asp:Literal ID="ltrShowRelatedLeft" runat="server"></asp:Literal>
                                    </dl>
                                </dd>
                            </div>
                        </div>

                        <div class="block left-module">
                            <p class="title_block">Bộ lọc sản phẩm</p>
                            <div class="block_content">

                                <div class="layered layered-filter-price">

                                    <div class="layered_subtitle">Thương hiệu</div>
                                    <div class="layered-content filter-brand">
                                        <asp:Literal ID="ltrLstBrand" runat="server"></asp:Literal>
                                    </div>

                                    <div class="layered_subtitle">Giá</div>
                                    <div class="layered-content filter-brand">
                                        <div class="range__content">
                                            <div id="slider-range">
                                            </div>
                                            <div class="range__values">
                                                <asp:TextBox ID="range__values__from" CssClass="range__values__from" Text="0" runat="server"></asp:TextBox>
                                                <asp:HiddenField ID="hdPriceFrom" Value="0" runat="server" />
                                                <asp:HiddenField ID="hdPriceTo" Value="0" runat="server" />
                                                <asp:TextBox ID="range__values__to" Text="100000" CssClass="range__values__to" runat="server"></asp:TextBox>
                                                <asp:Button ID="btnSearchPrice" CssClass="range__button" runat="server" Text="Xem"></asp:Button>
                                            </div>
                                        </div>
                                        <div>
                                            Đơn vị: Ngàn (VNĐ)
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>

                        <div class="block left-module hidden-xs">
                            <p class="title_block">Sản phẩm tiêu biểu</p>

                            <div class="block_content">
                                <ul id="Ul1" class="products-block best-sell">
                                    <asp:Repeater ID="rptNewProduct" runat="server" OnItemDataBound="rptNewProduct_ItemDataBound">
                                        <ItemTemplate>
                                            <li>
                                                <div class="products-block-left">
                                                    <asp:HyperLink ID="hpImage" runat="server">
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
                                <div id="list-items" class="clearfix temp-height lazy-load">
                                    <asp:Repeater ID="rptProductList" runat="server" OnItemDataBound="rptProductList_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrUL" runat="server"></asp:Literal>
                                            <li class="col-lg-3 col-md-3 col-sm-6 col-xs-6 noPM ">
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
                            </div>
                            <uc1:CtrListPage ID="CtrListPage1" runat="server"></uc1:CtrListPage>
                            <div id="secure-info" class="secure-info">
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-12 col-sm-12 col-xs-12 hidden" id="p4p-right-side-wrap">
                        <div class="autosize-list-p4p p4pExpress p4p-side" id="p4pHotProductsSide">
                            <div>
                                <h4>Sản phẩm tiêu biểu</h4>
                            </div>
                            <div class="p4p-side-container">
                                <ul id="p4p-side-content" class="p4p-side-content clearfix">
                                    <asp:Repeater ID="rptHotProduct" runat="server" OnItemDataBound="rptHotProduct_ItemDataBound">
                                        <ItemTemplate>
                                            <li class="p4p-list-item">
                                                <div class="img-box-wrap">
                                                    <div class="img-box">
                                                        <asp:HyperLink ID="hpImageProducts" runat="server">
                                                            <asp:Image ID="imgProduct" CssClass="auto-set-img0" runat="server" />
                                                        </asp:HyperLink>
                                                    </div>
                                                </div>
                                                <p class="p4p-product-title">
                                                    <asp:HyperLink ID="hpNameProduct" runat="server"></asp:HyperLink>
                                                </p>
                                                <p class="p4p-price-list">
                                                    <asp:Literal ID="ltrPrice" runat="server"></asp:Literal>
                                                </p>
                                                <asp:Literal ID="ltrOrgPrice" runat="server"></asp:Literal>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
</asp:Content>
