<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage/MasterSite.master"
    CodeFile="Specialtem.aspx.vb" Inherits="Specialtem" %>

<%@ Register Src="~/Common/CtrListPage.ascx" TagName="CtrListPage" TagPrefix="uc1" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/jquery-ui.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/Category-list.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/catlist1.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/SlicePrice.css"%>" />
    <%--<link rel="stylesheet" href="<%=GetImgUrl() + "Styles/header.css"%>" />--%>
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="phMainContent">
    <asp:HiddenField ID="hdCateName" runat="server" />
    <asp:HiddenField ID="hdBrandName" runat="server" />
    <!--Specialtem-->
    <div class="collection">
        <div class="columns-container">
            <div id="columns" class="container">
                <div class="breadcrumb clearfix">
                    <div class="col-md-12 ">
                        <div class="util-clearfix" id="Div1">
                            <div id="aliGlobalCrumb" class="ui-breadcrumb textuppercase">
                                <a href="<%= GetImgUrl() + "Default.aspx" %>" title="Home">Trang chủ </a><span class="divider">
                                    &gt;</span> <a href="<%=GetImgUrl()+"all-category/" %>" rel="nofollow" title="All Categories">
                                        Tất cả</a>
                                <asp:Literal ID="ltrRelatedCategory" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="column col-xs-12 col-sm-3" id="left_column">
                        <div class="block left-module">
                            <%--<dl class="category-list  ">--%>
                            <div class="bc-list-title bc-title bc-nowrap-ellipsis">
                                <p class="title_block">
                                    Danh mục sản phẩm</p>
                            </div>
                            <div class="block_content">
                                <div class="layered layered-category">
                                    <asp:Literal ID="ltrShowRelatedLeft" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <%--  </dl>--%>
                        </div>
                    </div>
                    <div class="center_column col-xs-12 col-sm-9 product-col" id="center_column">
                        <div id="view-product-list" class="view-product-list">
                            <div id="gallery-item">
                                <div id="list-items" class="clearfix temp-height lazy-load">
                                    <ul class="row product-list grid filter">
                                        <asp:Repeater ID="rptProductList" runat="server" OnItemDataBound="rptProductList_ItemDataBound">
                                            <ItemTemplate>
                                                 <li class="col-lg-3 col-md-3 col-sm-6 col-xs-6 noPM">
                                        <asp:Literal ID="ltrDiv" runat="server"></asp:Literal>

                                        <div class="product-container">
                                            <div class="left-block">
                                                <asp:HyperLink ID="hpLinkImage" runat="server">
                                                    <asp:Image ID="imgImages" runat="server" CssClass="center-block" />
                                                </asp:HyperLink>
                                            </div>
                                            <div class="right-block bc-nowrap-ellipsis col-sm-9">
                                                <asp:HyperLink ID="hpLink" runat="server"></asp:HyperLink>
                                                <div class='price-info'>
                                                    <asp:Literal ID="ltrPrices" runat="server"></asp:Literal>                                                   
                                                </div>
                                            </div>
                                            <div class="col-sm-3 divAddCard"><span><i class="fa fa-cart-plus fa-2x"></i> </span></div>
                                        </div>

                                        <asp:Literal ID="ltrEndDiv" runat="server"></asp:Literal>

                                    </li>
                                                <%--<asp:Literal ID="ltrUL" runat="server"></asp:Literal>--%>
                                                <%-- <li class="col-lg-3 col-md-3 col-sm-6 col-xs-6 noPM">
                                                    <div class="product-container">
                                                        <div class="left-block">
                                                            <asp:Literal ID="ltrpicCore" runat="server"></asp:Literal>
                                                        </div>
                                                        <div class="right-block bc-nowrap-ellipsis">
                                                            <asp:HyperLink ID="hpLink" runat="server"></asp:HyperLink>
                                                            <div class='price-info'>
                                                                <asp:Literal ID="ltrPrices" runat="server"></asp:Literal>--%>
                                                <%--<span class='old-price'>2500 VND</span><span class='new-price'>1500 VND</span><span class='discount-percent'>30%</span>--%>
                                                <%--  </div>
                                                        </div>--%>
                                                <%-- <div class="right-block bc-nowrap-ellipsis">
                                                        <h5 class="product-name">
                                                            <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                                        </h5>
                                                        <div class="content_price">
                                                            <div class="price product-price" itemprop="price">
                                                                <asp:Literal ID="ltrPrice" runat="server"></asp:Literal>
                                                            </div>
                                                            <asp:Literal ID="ltrOrgPrice" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>--%>
                                                <%--  </li>--%>
                                                <asp:Literal ID="ltrEndUL" runat="server"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
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
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
</asp:Content>
