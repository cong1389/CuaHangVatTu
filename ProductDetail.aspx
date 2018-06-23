<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage/MasterSite.master"
    CodeFile="ProductDetail.aspx.vb" Inherits="ProductDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/jquery.bxslider.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/product-detail2.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/product-detail3.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/default.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/component.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/header.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/ModalPopup.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="phMainContent">
    <div class="container" id="columns">
        <script>
            var PAGE_TIMING = {
                pageType: 'detail2013'
            };
            PAGE_TIMING.startRenderImage = new Image();
            PAGE_TIMING.startRenderImage.onload = function () {
                PAGE_TIMING.startRender = new Date().getTime();
            };
            PAGE_TIMING.startRenderImage.src = 'http://i02.i.aliimg.com/wimg/monitor/start-render.png';
        </script>
        <asp:HiddenField runat="server" ID="hdProductId" />
        <!--ProductDetail-->
        <div class="detail-page ">
            <div class="breadcrumb breadcrumb-arrow hidden-sm hidden-xs">
                <a rel="nofollow" href="<%=GetImgUrl()+"Default.aspx" %>">Trang chủ</a> <span class="divider">
                    &gt;</span> <a href="<%=GetImgUrl()+"all-category/" %>">Tất cả</a> <span class="divider">
                        &gt;</span>
                <asp:Literal ID="ltrMenuName" runat="server"></asp:Literal>
                <span class="divider">&gt;</span>
                <asp:Literal runat="server" ID="ltrSubMenu"></asp:Literal>
            </div>
            <div class="clearfix">
            </div>
            <div id="product" class="base">
                <!--coupon1-->
                <div class="col-lg-3 noPM">
                    <div id="img" class="" data-widget-cid="widget-23">
                        <div class="ui-image-viewer" id="magnifier" itemprop="image" data-widget-cid="widget-22">
                            <div class="ui-image-viewer-thumb-wrap" data-role="thumbWrap">
                                <a class="ui-image-viewer-thumb-frame" data-role="thumbFrame" href="#">
                                    <asp:Literal runat="server" ID="ltrMainImages"></asp:Literal></a>
                            </div>
                        </div>
                        <div class="img-zoom-in">
                            <span id="zoom-in-tips">Click để xem lớn</span>
                        </div>
                        <ul class="image-nav util-clearfix">
                            <asp:Literal runat="server" ID="ltrThumImages"></asp:Literal>
                        </ul>
                    </div>
                    <div class="sns-tool">
                    </div>
                    <script type="text/javascript">
                        if (!window.runParams) {
                            window.runParams = {};
                        }
                        window.runParams.imageServer = "<% = GetImgUrl()%>";
                        window.runParams.imageDetailPageURL = "<% = GetImgUrl()%>";
                        window.runParams.imageBigViewURL = [
                    <%=ShowImageSlide()%>
                        ];
                        window.runParams.mainBigPic = "#";

                    </script>
                </div>
                <div class="col-lg-6 pb-right-column ">
                    <div class="">
                        <h1 class="product-name" itemprop="name">
                            <asp:Literal runat="server" ID="ltrProductName" Text=""></asp:Literal></h1>
                        <div class="">
                            <div class="product-info">
                                <form action="#" class="buy-now-form" id="buy-now-form" name="buyNowForm">
                                <!-- esi -->
                                <div>
                                    <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
                                </div>
                                <div class="product-info-operation-color product-desc">
                                    <dl class="product-info-color">
                                        <dt class="pp-dt-ln sku-color-title">Mã hàng:</dt>
                                        <dd>
                                            <div id="Div2" class="clearfix">
                                                <asp:Literal ID="ltrNo" runat="server"></asp:Literal>
                                            </div>
                                        </dd>
                                    </dl>
                                </div>
                                <div class="product-info-operation-color info-orther">
                                    <dl class="product-info-color">
                                        <dt class="pp-dt-ln sku-color-title">Thương hiệu:</dt>
                                        <dd>
                                            <div id="Ul1" class="clearfix">
                                                <asp:Literal ID="ltrBranch" runat="server"></asp:Literal>
                                            </div>
                                        </dd>
                                    </dl>
                                </div>
                                <div id="product-info-price-pnl " class="product-info-operation-color info-orther">
                                    <dl class="product-info-current">
                                        <dt>Giá bán:</dt>
                                        <dd>
                                            <div class="product-price-group">
                                                <div id="product-price" class="ui-cost" itemprop="offers" itemscope="" itemtype="http://schema.org/Offer">
                                                    <b><span itemprop="price"></span><span id="sku-price" class="sku-price"><span itemprop="lowPrice">
                                                        <asp:Literal runat="server" ID="ltrPrice"></asp:Literal></span> <span itemprop="highPrice"
                                                            class="hidden">VND</span> </span></b>
                                                    <asp:Literal ID="ltrPriceSaleOff" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </dd>
                                    </dl>
                                </div>
                                <div class="product-info-operation-color info-orther">
                                    <div id="product-info-sku1" data-widget-cid="widget-11">
                                        <dl class="product-info-color">
                                            <dt class="pp-dt-ln sku-color-title">Màu: </dt>
                                            <dd>
                                                <ul id="sku-color" class="sku-attr sku-color clearfix">
                                                    <asp:Literal ID="ltrColor" runat="server"></asp:Literal>
                                                </ul>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                                <div class="product-info-operation-color product-desc">
                                    <label>
                                        Số lượng</label>
                                    <asp:TextBox ID="TxtQuantity" Text="1" CssClass="ui-textfield ui-textfield-system"
                                        runat="server"></asp:TextBox>
                                    Còn:
                                    <asp:Literal ID="ltrRemainNumber" runat="server"></asp:Literal>
                                    sản phẩm
                                    <asp:HiddenField ID="txtUnitOfMeasure" runat="server" />
                                </div>
                                <div class="product-info-operation-color info-orther">
                                    <label>
                                    </label>
                                    <asp:Button ID="CmdAddtoCart" runat="server" Text="Mua ngay" CssClass="btn-cart add-cart "
                                        UseSubmitBehavior="false" />
                                    <asp:Button ID="cmdWishList" runat="server" Visible="false" Text="Đưa vào MyList"
                                        CssClass="add-to-cart-btn" UseSubmitBehavior="false" />
                                    <asp:HiddenField ID="txtAction" runat="server" />
                                    <input id="skuAttr" name="skuAttr" type="hidden" value="" />
                                </div>
                                <div class="product-info-operation-color info-orther hidden">
                                    <label>
                                        Giá tại khu vực khác, vui lòng chọn để xem</label>
                                    <asp:DropDownList ID="ddlOtherPrice" runat="server" AutoPostBack="False" CssClass="ui-textfield-system">
                                    </asp:DropDownList>
                                </div>
                                <div class="product-info-operation-color info-orther">
                                    <label>
                                        <span id="spOtherPrice"></span>
                                    </label>
                                </div>
                                </form>
                                <!-- store promotion coupon and fixeddisscount -->
                                <div id="seller-promise-list" style="display: block;">
                                </div>
                            </div>
                            <!-- wish list args -->
                            <div class="suggestion-product" style="display: none">
                                <ul>
                                    <asp:Literal ID="ltrReplace" runat="server"></asp:Literal>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!--Chinh sach giao hang-->
                <div class="column col-xs-12 col-sm-3 col-lg-3 noPM" id="left_column">
                    <div class="block left-module">
                        <div class="title_block">
                            <asp:Literal runat="server" ID="ltrHeaderDeliveryPolicy"></asp:Literal>
                        </div>
                        <div class="block_content" id="delivery_policies_list">
                            <asp:Literal runat="server" ID="ltrDeliveryPolicy"></asp:Literal>
                        </div>
                    </div>
                </div>
                <!--/Chinh sach giao hang-->
            </div>
            <!--extend-->
            <div class="clearfix">
            </div>
            <div id="extend" class="col-lg-12 noPM">
                <div class="row">
                    <div class="column col-xs-12 col-sm-3 product-tab" id="left_column">
                        <div class="block left-module">
                            <p class="title_block">
                                Sản phẩm liên quan</p>
                            <div class="block_content">
                                <ul class="products-block best-sell">
                                    <asp:Repeater ID="rptRelatedProducts" runat="server" OnItemDataBound="rptRelatedProducts_ItemDataBound">
                                        <ItemTemplate>
                                            <li>
                                                <div class="products-block-left">
                                                    <a href="/products/dam-body-duoi-ca-d0248">
                                                        <asp:Literal ID="ltrImages" runat="server"></asp:Literal></a>
                                                </div>
                                                <div class="products-block-right">
                                                    <p class="product-name">
                                                        <asp:HyperLink ID="hpLink" runat="server" CssClass="ui-product-listitem-title"></asp:HyperLink>
                                                    </p>
                                                    <p class="product-price">
                                                        <span class="price old-price">
                                                            <asp:Literal ID="ltrPrice" runat="server"></asp:Literal></span>
                                                    </p>
                                                    <p class="product-star">
                                                        <i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i
                                                            class="fa fa-star"></i><i class="fa fa-star-half-o"></i>
                                                    </p>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-8 col-xs-12 noPM">
                        <div class="similer-bar hidden" style="position: absolute;">
                        </div>
                        <!----->
                        <div class="product-tab">
                            <ul class="nav-tab">
                                <li class="active"><a aria-expanded="true" data-toggle="tab" href="#product-detail">
                                    Chi tiết sản phẩm</a> </li>
                                <li class=""><a aria-expanded="false" data-toggle="tab" href="#information">Bình luận</a>
                                </li>
                            </ul>
                            <div class="tab-container">
                                <div id="product-detail" class="tab-panel active">
                                    <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                                </div>
                                <div id="information" class="tab-panel">
                                    <div id="binhluan">
                                        <div class="container-fluid">
                                            <div class="tab-pane" id="Div3">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">
                                                            Đánh giá</label>
                                                        <div class="col-sm-2">
                                                            <label for="inputEmail3" class="control-label">
                                                                <asp:Rating ID="ProductRating" runat="server" BehaviorID="RatingBehavior1" CurrentRating="0"
                                                                    MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                                                                    FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" Style="float: left;">
                                                                </asp:Rating>
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            Nhận xét của bạn: (Vui lòng gõ tiếng Việt có dấu, không quá 1000 chữ)
                                                            <asp:TextBox ID="TxtCommand" runat="server" MaxLength="1000" TextMode="MultiLine"
                                                                CssClass="form-control" Rows="5"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">
                                                            Họ tên</label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="TxtFullName" runat="server" Width CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">
                                                            Email</label>
                                                        <div class="col-sm-10">
                                                            <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">
                                                        </label>
                                                        <div class="col-sm-10">
                                                            <asp:Button ID="cmdSaveReview" runat="server" Text="Gửi ý kiến" CssClass="ButtonW8Big">
                                                            </asp:Button>
                                                            <asp:Button ID="cmdSaveReviewLog" runat="server" Text="Đăng nhập để tích lũy điểm"
                                                                CssClass="ButtonW8Big"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-2 col-sm-10">
                                                            <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-2 col-sm-10">
                                                            <asp:Label ID="LblReview" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!------>
                        <div class="page-product-box hidden">
                            <h3 class="heading">
                                Sản phẩm đã xem</h3>
                            <div class="seller-info-wrap reviewed-product">
                                <div class="trending-products-related">
                                    <div class="ui-box ui-box-primary">
                                        <div class="ui-box-body">
                                            <div class="trending-products-list more">
                                                <asp:Repeater ID="rptSelected" runat="server" OnItemDataBound="rptSelected_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="ui-product-listitem-vertical util-clearfix">
                                                            <div class="ui-product-listitem-thumb">
                                                                <asp:Literal ID="ltrImages" runat="server"></asp:Literal>
                                                            </div>
                                                            <div class="ui-product-listitem-info">
                                                                <asp:HyperLink ID="hpLink" runat="server" CssClass="ui-product-listitem-title"></asp:HyperLink>
                                                                <div class="ui-product-listitem-row ui-cost">
                                                                    <b class="notranslate">
                                                                        <asp:Literal ID="ltrPrice" runat="server"></asp:Literal></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="related-searches">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--/extend-->
        </div>
        <div style="display: none;">
            <asp:Button ID="cmdShowMsg" runat="server" Text="OK" />
        </div>
        <asp:ModalPopupExtender ID="HomeoneMsgPopup" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="cmdShowMsg" PopupControlID="MsgPopupPanel" OkControlID="cmdCloseMsg">
        </asp:ModalPopupExtender>
        <asp:Panel ID="MsgPopupPanel" runat="server" Style="display: none;" CssClass="popup">
            <div style="width: 400px; height: 220px;">
                <div class="popupheader" style="height: 20px;">
                    <b>cuahangvattu.com</b>
                </div>
                <div id="MsgContent" style="display: table-cell; vertical-align: middle; width: 400px;
                    height: 150px; padding-left: 10px;">
                    Đã thêm vào danh sách thường mua của bạn.
                    <div style="margin-top: 10px;">
                        <a href="<%= GetUrl() + "gio-hang/" %>">Xem danh sách mua hàng</a>
                    </div>
                </div>
                <div style="height: 30px; text-align: right; padding-right: 4px;">
                    <asp:Button ID="cmdCloseMsg" runat="server" OnClientClick="ClosePoupAdd()" Text="Đóng"
                        class="ButtonW8Big" />
                </div>
            </div>
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div style="display: none">
                    <asp:Button ID="cmdShowLoginPopup" runat="server" Text="OK" />
                </div>
                <asp:ModalPopupExtender ID="Login_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="cmdShowLoginPopup" PopupControlID="PanelLogin" OkControlID="cmdCancelLogin">
                </asp:ModalPopupExtender>
                <asp:Panel ID="PanelLogin" runat="server" Style="display: none; width: 850px; height: auto;"
                    CssClass="popup">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="popupheader" style="height: 30px;">
                                Vui lòng đăng nhập vào tài khoản (nếu bạn đã đăng ký) hoặc đăng ký tài khoản mới
                                để được hưởng nhiều ưu đãi.
                            </div>
                            <div class="add-to-list" style="margin: 10px 8px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 420px;">
                                            <div style="width: 400px; background-color: #f0f0f0; padding: 5px; margin-top: 5px;
                                                margin-right: 2px;">
                                                <div style="background: #fff; padding: 5px; height: 340px;">
                                                    <div>
                                                        <b>Bạn đã đăng ký tài khoản?</b>
                                                    </div>
                                                    <div class="requirement-field">
                                                        Sử dụng thông tin đã đăng ký để đăng nhập
                                                    </div>
                                                    <div>
                                                        <fieldset style="height: 204px;">
                                                            <legend>Thông tin đăng nhập</legend>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        Email
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtEmailLog" runat="server" Width="200px"></asp:TextBox><span class="requirement">(*)</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Password
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtPasswordLog" runat="server" Width="200px" TextMode="Password"></asp:TextBox><span
                                                                            class="requirement">(*)</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 28px;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Width="249" class="inputbox" Style="display: none;"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                        <div class="submit-validate">
                                                            <asp:Button ID="cmdLogin" runat="server" Text="Đăng nhập" CssClass="ButtonW8Big">
                                                            </asp:Button>
                                                            <asp:Label ID="LblErrCheckin" runat="server" Text="" class="requirement"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="width: 420px; padding-left: 2px;">
                                            <div style="width: 400px; background-color: #f0f0f0; padding: 5px; margin-top: 5px;">
                                                <div class="create-new-account-fields">
                                                    <div>
                                                        <b>Bạn là khách hàng mới?</b>
                                                    </div>
                                                    <div class="requirement-field">
                                                        Hãy tạo một tài khoản mới.
                                                    </div>
                                                    <div style="min-height: 320px; padding-left: 3px; padding-right: 3px;">
                                                        <fieldset>
                                                            <legend>Thông tin tài khoản</legend>
                                                            <table width="100%" cellpadding="3" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 90px; padding-left: 0px;">
                                                                        Email<span class="requirement">(*)</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtEmailRegister" runat="server" Width="249" class="inputbox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90px; padding-left: 0px;">
                                                                        Mật khẩu<span class="requirement">(*)</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtPasswordRegister" runat="server" Width="249" TextMode="Password"
                                                                            class="inputbox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90px; padding-left: 0px;">
                                                                        Xác nhận lại<span class="requirement">(*)</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtConfirmPwdRegister" runat="server" Width="249" TextMode="Password"
                                                                            class="inputbox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90px; padding-left: 0px;">
                                                                        Họ và tên<span class="requirement">(*)</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="CboTitle" runat="server" class="inputdropdown" Width="53">
                                                                            <asp:ListItem Value="0">Anh</asp:ListItem>
                                                                            <asp:ListItem Value="1">Chị</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="TxtFullNameRegister" runat="server" Width="190" class="inputbox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90px; padding-left: 0px;">
                                                                        Mã giới thiệu
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtReferenceCode" runat="server" Width="150" class="inputbox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 90px; padding-left: 0px;" valign="top">
                                                                        Nhận email
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="CKReceivingPromotion" runat="server" Checked="true" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                        <div class="submit-validate">
                                                            <asp:Button ID="cmdSaveRegister" runat="server" Text="Đăng ký" CssClass="ButtonW8Big">
                                                            </asp:Button>
                                                            <asp:Label ID="LblWarningRegister" runat="server" Text="" class="requirement"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                        </td>
                                    </tr>
                                </table>
                                <div style="margin-top: 5px; text-align: right; height: 40px;">
                                    <asp:Button ID="cmdCancelLogin" runat="server" Text="Đóng" CssClass="ButtonW8Big" /></asp:Button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="display: none;">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="Variants" runat="server" />
                    <asp:Button ID="CmdAddtoCart1" runat="server" Text="Add" />
                    <asp:HiddenField ID="Qty" runat="server" />
                    <asp:HiddenField ID="UnitOfMeasure" runat="server" />
                    <asp:Button ID="CmdAddtoCartFromSameItem" runat="server" Text="Add" />
                    <asp:HiddenField ID="ItemNo" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/products-detail.js"%>"></script>
    <input type="hidden" id="hdURL" value="<%= GetImgUrl()%>" />
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/imagezoom.js"%>"></script>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/global.js"%>"></script>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/jquery.bxslider.min.js"%>"></script>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/jquery-ui-1.10.4.custom.min.js"%>"></script>
    <!---->
</asp:Content>
