<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Header.ascx.vb" Inherits="Common_Header" %>
<!--Common_Header-->

<header class="container-fluid noPM main-header top-header">
    <div class="top-lighthouse hidden-sm hidden-xs" id="headertop">

        <div class="col-lg-12 col-md-12 col-sm-12" id="top-lighthouse">
            <div class="nav-global" id="nav-global">
                <div class="ng-item ng-bp col-lg-3 col-md-3 col-sm-3 col-xs-10">
                    <%-- <div class="pull-left">
                        <asp:Literal runat="server" ID="ltrHotlineHeader"></asp:Literal>:</div>
                    <div style="color: Red; font-weight: bold">--%>
                    <asp:Literal runat="server" ID="ltrHotlineContent"></asp:Literal>
                    <%--</div>--%>
                </div>
                <div class="ng-item ng-help ng-sub col-sm-3 hidden-xs">

                    <span class="ng-sub-title">
                        <%--<img alt="email" src="<%=GetImgUrl() + "Images/email.png"%>">--%>
                        Dịch vụ khách hàng</span>
                    <ul class="ng-sub-list">
                        <li><a href="<%=GetUrl() + "static-detail/cham-soc-khac-hang/" %>" rel="nofollow"
                            class="ng-help-link" data-role="help-center-link">Chăm sóc khách hàng</a></li>
                        <li><a href="<%=GetImgUrl()+"static-detail/chinh-sach-thanh-toan/" %>" rel="nofollow">Chính sách thanh toán</a></li>
                    </ul>
                </div>
                <div class="ng-item ng-bp col-sm-3 hidden-xs">
                    <span class=""></span><a href="#" rel="nofollow" class="ng-help-link" id="check-order" data-role="help-center-link">Kiểm tra đơn hàng</a>
                    <div id="dialog-message" title="Kiểm tra đơn hàng">
                        <div class="modal-body">
                            <div class="line">
                                <label for="">
                                    Mã đơn hàng<span class="requirement">*</span></label>
                                <input type="text" class="form-control" placeholder="Mã đơn hàng" id="gblorderId"
                                    name="txtgblorder" maxlength="20">
                            </div>
                            <div class="line" id="orderPhone">
                                <label for="">
                                    Số điện thoại<span class="requirement">*</span></label>
                                <input type="text" class="form-control" placeholder="Số điện thoại" id="gblMobileId"
                                    maxlength="13" name="txtgblMobile">
                            </div>
                            <div class="line requirement" style="display: none" id="invalidation">
                                Mã đơn hàng hoặc số điện thoại không đúng.
                            </div>
                            <div class="line requirement" style="display: none" id="requireinput">
                                Vui lòng nhập đơn hàng và số điện thoại.
                            </div>
                        </div>
                    </div>
                </div>
                <div class="ng-item ng-bp col-sm-3 hidden-xs">
                    <span class=""></span><a href="<%=GetUrl() + "dang-ky/" %>"
                        rel="nofollow" class="ng-help-link" data-role="help-center-link">Đăng ký thành viên</a>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hdURL" value="<% =GetUrl() %>" />
    <div class="header header-outer-container main-header" id="header">
        <div class="container">
            <div class="col-xs-2 hidden-lg hidden-md hidden-sm noPM ">
                <a class="navbar-minimalize btn btn-oranges navcus " href="#">
                    <i class="fa fa-bars"></i></a>
            </div>
            <div class="col-lg-3 col-sm-3 col-xs-4 noPM">
                <div class="header-categories hidden" id="header-categories">
                    <span class="categories-title">Danh mục sản phẩm</span> <i class="balloon-arrow"></i>
                    <div class="categories-main new-categories-main categories-main-list">
                        <div class="categories-content-title">
                            <span>Danh mục sản phẩm</span><a href="<% =GetUrl() + "all-category/" %>">See All &gt;</a>
                            <span class="btn-open-mobile pull-right home-page"><i class="fa fa-bars"></i></span>
                        </div>
                        <div class="categories-list-box">
                            <asp:Literal ID="ltrMainCategory" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <a href="<%=GetUrl() %>">
                    <img src="<%=GetImgUrl() + "Images/logo.jpg"%>" class="logo" />
                </a>

            </div>
            <div class="col-lg-6 col-sm-5 hidden-xs">
                <div id="form-searchbar" class="searchbar-form" action="<%=GetImgUrl()+"InProcess.aspx" %>"
                    method="get">
                    <div class="searchbar-operate-box">
                        <div id="search-cate" class="search-category hidden-sm">
                            <div class="search-cate-title">
                                <span id="search-category-value" selected-val="0" class="search-category-value">Tất
                                    cả</span>
                            </div>
                            <asp:DropDownList ID="ddlSearchCategory" class="search-cate notranslate" runat="server">
                            </asp:DropDownList>
                        </div>
                        <input type="button" class="search-button" onclick="searchData()" value="" />
                    </div>
                    <div class="search-key-box">
                        <input type="text" placeholder="Nhập từ khóa" maxlength="50" value="" name="SearchText"
                            class="search-key" id="search-key" />
                    </div>
                    <div id="suggesstion-box">
                    </div>
                    <div class="ui-autocomplete" style="z-index: 99; display: none; position: absolute; left: -9999px; top: -9999px;">
                        <div class="hot-word" id="hot-word">
                        </div>
                        <ul class="ui-autocomplete-ctn">
                        </ul>
                    </div>
                </div>
            </div>

            <div class="col-xs-5  col-lg-3 col-sm-4 group-button-header new-login">
                <asp:HyperLink ID="hpLogin" runat="server" Text="Đăng nhập" CssClass="btn-heart"></asp:HyperLink>
                <asp:Literal ID="ltrLogin" runat="server"></asp:Literal>
                <asp:LinkButton ID="lbLogout" runat="server" Visible="false" Text="Đăng xuất" />
                <%--<a title="Đăng nhập" href="/account/login" class="btn-heart">wishlist</a>--%>
                <div class="btn-cart" id="cart-block">
                    <a rel="nofollow" title="My cart" href="<%=GetImgUrl()+"gio-hang/" %>">Cart</a>
                    <span class="notify notify-right" id="nav-cart-num">
                        <asp:Literal ID="ltrNumberCard" Text="0" runat="server"></asp:Literal>
                    </span>
                </div>
            </div>

        </div>
        <script>
            $("#ctrTop1_ddlSearchCategory").change(function () {
                var textSelect = $('#ctrTop1_ddlSearchCategory option:selected').text();
                var valSelect = $('#ctrTop1_ddlSearchCategory option:selected').val();
                $("#search-category-value").text(textSelect);
                $("#search-category-value").attr('selected-val', valSelect);
            });
        </script>
        <script>

            $(window).scroll(function () {
                var scrollTop = $(window).scrollTop();
                if (scrollTop == 0) {
                    $('#header').removeClass("header header-outer-container header-fixed").addClass("header header-outer-container");
                }
                else {
                    $('#header').removeClass("header header-outer-container").addClass("header header-outer-container header-fixed");
                }
            });

            function searchData() {

                var url = $('#hdURL').val();
                url = url + "search-item/" + $("#search-key").val() + "/" + $("#ctrTop1_ddlSearchCategory").val() + "/"
                location.href = url
            }

        </script>
    </div>
</header>
