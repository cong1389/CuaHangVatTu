<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage/MasterSite.master"
    CodeFile="SearchItem.aspx.vb" Inherits="SearchItem" %>

<%@ Register Src="~/Common/CtrListPage.ascx" TagName="CtrListPage" TagPrefix="uc1" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/jquery-ui.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/Category-list.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/catlist1.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/SlicePrice.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/subCategory.css"%>" />
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="phMainContent">
    <asp:HiddenField ID="hdCateName" runat="server" />
    <asp:HiddenField ID="hdBrandName" runat="server" />
    <div class="container bc-not-standard no-standard-more-eight bc-category-floor-title">
        <div class="row">
            <div class="column col-xs-12 col-sm-3 " id="left_column">

                <div class="block left-module">
                    <div class="bc-list-title bc-title bc-nowrap-ellipsis" title="Categories">
                        Loại sản phẩm</div>
                    <div id="refine-category-list">
                        <dl class="  ">
                            <dd class="list-box">
                                <dl class="son-category">
                                    <asp:Literal ID="ltrShowRelatedLeft" runat="server"></asp:Literal>
                                </dl>
                            </dd>
                        </dl>
                    </div>
                </div>

                <div class="" id="p4p-right-side-wrap ">
                    <div class="autosize-list-p4p p4pExpress p4p-side" id="p4pHotProductsSide">
                        <h4>
                            Sản phẩm tiêu biểu</h4>
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
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="center_column col-xs-12 col-sm-9 product-col" data-spm="4">
                <div class="ui-slidebox-container">
                    <div class="box-header with-border util-clearfix" id="breadView">
                        <div id="aliGlobalCrumb" class="ui-breadcrumb">
                            <a href="<%= GetImgUrl() + "Default.aspx" %>" title="Home">Trang chủ </a><span class="divider">
                                &gt;</span> <a href="<%=GetImgUrl()+"all-category/" %>" rel="nofollow" title="All Categories">
                                    Tất cả</a>
                            <h1>
                                <asp:Literal ID="ltrRelatedCategory" runat="server"></asp:Literal>
                            </h1>
                        </div>
                    </div>
                    <div class="ui-slidebox-slider sale-off" data-role="slider">
                        <div id="list-items" class="product-list temp-height lazy-load">
                            <asp:Repeater ID="rptProductList" runat="server" OnItemDataBound="rptProductList_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrUL" runat="server"></asp:Literal>
                                    <li class=" list-item-first col-lg-3 col-md-3 col-sm-6 col-xs-6 noPM">
                                        <div class="product-container">
                                            <div class="left-block">
                                                <div>
                                                    <asp:Literal ID="ltrpicCore" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="right-block bc-nowrap-ellipsis">
                                                <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                                <div class="price-info">
                                                    <div class="new-price">
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
            <div id="p4pHotProducts-old" class="p4pExpress hidden">
                <h4>
                    Sản phẩm mới</h4>
                <div id="relatied-product-slide" class="relatied-product-slide">
                    <div class="relatied-products-container">
                        <asp:Repeater ID="rptNewProduct" runat="server" OnItemDataBound="rptNewProduct_ItemDataBound">
                            <ItemTemplate>
                                <%--<ul id="p4p-ul-content" class="p4p-bottom-content clearfix">--%>
                                <asp:Literal ID="ltrUl" runat="server"></asp:Literal>
                                <li class="p4p-list-item">
                                    <div class="img-box-wrap">
                                        <div class="img-box">
                                            <%-- <a rel="nofollow" target="_blank" title=""
                                                href="#" <img class="auto-set-img0" alt="" src="./ListCategory_files/HTB1uK7CHpXXXXbeXXXXq6xXFXXXI.jpg_200x200.jpg"></a>--%>
                                            <asp:HyperLink ID="hpImage" runat="server">
                                                <asp:Image ID="imgProduct" CssClass="auto-set-img0" runat="server" />
                                            </asp:HyperLink>
                                        </div>
                                        <div class="discount-rate">
                                        </div>
                                    </div>
                                    <p class="p4p-product-title">
                                        <asp:HyperLink ID="hpNameProduct" runat="server"></asp:HyperLink>
                                    </p>
                                    <p class="p4p-price-list">
                                        <asp:Literal ID="ltrPrice" runat="server"></asp:Literal>
                                    </p>
                                </li>
                                <asp:Literal ID="ltrEndUl" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <a class="lnk-prev" id="lnk-prev" title="previous page"></a><a class="lnk-next" id="lnk-next"
                        title="next page"></a>
                </div>
                <div class="p4pClearfix">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
</asp:Content>
