<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdvBanber.ascx.vb" Inherits="Common_AdvBanber" %>


<div class="advertise-main col-lg-9 col-md-9 col-sm-9 noPM">


    <div class="nav-top-menu" id="key-visual-main"
        data-hemisphere="north" data-spm="15" data-widget-cid="widget-26">
        <!--TMS:1717630-->
        <div class="channel-entrance">
            <a href="<%=GetImgUrl() + "special-products/1/"%>">Sản phẩm giảm giá</a> <a href="<%=GetImgUrl() + "special-products/2/"%>">Sản phẩm mới</a> <a href="<%=GetImgUrl() + "special-products/3/"%>">Sản phẩm bán chạy nhất</a><a href="<%=GetImgUrl() + "tin-tuc/"%>">Tin tức</a>
        </div>
        <!--TMS:left slide-->
        <div class="ui-banner-slider-container noPM col-lg-9 col-md-9">
            <ul class="ui-banner-slider-slider" data-role="slider">
                <asp:Literal ID="ltrBanerRun" runat="server"></asp:Literal>
            </ul>
            <a class="ui-banner-slider-prev" data-role="prev" style="visibility: visible;">Lui</a>
            <a class="ui-banner-slider-next" data-role="next" style="visibility: visible;">Tới</a>
            <span class="ui-banner-slider-nav"></span>
        </div>


        <!--TMS:rigt slide-->
        <div class="sub-visual hidden-xs noPM hidden-sm col-md-3 col-lg-3 noPM" data-spm="16">
            <div class="sub-visual-inner">
                <ul class="sub-visual-list">
                    <asp:Literal ID="ltrBannerNotRun" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
    </div>

</div>

