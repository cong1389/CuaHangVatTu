<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterSite.master"
    CodeFile="SubCategory.aspx.vb" Inherits="SubCategory" %>

<%@ Register Src="~/Common/AdvBanber.ascx" TagName="CtrlBaner" TagPrefix="uc1" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css"%>" />
    <%--<link rel="stylesheet" href="Styles/MainStyle1.css" />--%>
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/subCategory.css"%>" />
</asp:Content>

<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">

    <asp:HiddenField ID="hdSubCategoryidlist" runat="server" Value="0" />
    <asp:HiddenField ID="hdSubCateValueScrol" Value="0" runat="server" />

    <!--SubCategory-->
    <div class="container bc-not-standard no-standard-more-eight bc-category-floor-title">
        <div class="row">

            <div class="column col-xs-12 col-sm-3 " id="left_column">
                <div class="block left-module">
                    <div class="bc-list-title bc-title bc-nowrap-ellipsis" title="Categories">Loại sản phẩm</div>
                    <div class="block_content">
                        <div class="layered layered-category">
                            <ul class="tree-menu">
                                <asp:Repeater ID="rptSubLeftMenu" runat="server" OnItemDataBound="rptSubLeftMenu_OnItemDataBound">
                                    <ItemTemplate>
                                        <li>
                                            <span></span>
                                            <asp:HyperLink ID="hpLinkSub" runat="server"></asp:HyperLink>
                                            <div class="bc-sub-cate">
                                                <ul class="bc-sub-cate-list">
                                                    <asp:Repeater ID="rptChildMenu" runat="server" OnItemDataBound="rptChildMenu_OnItemDataBound">
                                                        <ItemTemplate>
                                                            <li class="bc-nowrap-ellipsis">
                                                                <asp:HyperLink ID="hpLinkChild" runat="server"></asp:HyperLink>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
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
                    <div class="ui-slidebox-slider sale-off" data-role="slider">
                        <ul class="row product-list grid filter">
                            <asp:Repeater ID="rptProductSubCategory" runat="server" OnItemDataBound="rptProductSubCategory_OnItemDataBound">
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
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="bc-floors-wrap" id="div-callback-category">
    </div>
    <div class="containerLoader hidden">
        <p id="loader" style="display: none">
            <img src="<%=GetImgUrl() + "Images/loading_more.gif"%>" />
        </p>
    </div>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ScrollPagingSubCategory.js"%>"></script>
</asp:Content>
