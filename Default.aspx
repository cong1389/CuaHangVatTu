<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterSite.master"
    CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Src="~/Common/AdvBanber.ascx" TagName="CtrlBaner" TagPrefix="uc1" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
</asp:Content>
<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">
    <div id="home-firstscreen" class="home-firstscreen ">
        <div class="container">
            <div class="row">
                <div class="categories hidden-xs col-lg-3 col-md-3 col-xs-3 noPM">
                    <!--TMS:left menu-->
                    <div class="categories-main">
                        <div class="categories-content-title">
                            <span>Loại Sản phẩm</span>
                            <span class="btn-open-mobile pull-right home-page"><i class="fa fa-bars"></i></span>
                            <a class="hidden" href="<%=GetImgUrl()+"all-category/" %>">Tất cả &gt;</a>
                        </div>
                        <div class="categories-list-box">
                            <!--TMS:1702599-->
                            <asp:Literal ID="ltrMainCategory" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <uc1:CtrlBaner ID="ucBaner" runat="server" />
                <%--banber--%>
            </div>
        </div>
    </div>

    <!--TMS:1811353-->
    <div class="content-page">

        <asp:Literal ID="ltrLoadFirstTier" runat="server"></asp:Literal>

    </div>
    <div class="containerLoader hidden">
        <p id="loader" class="hidden">
            <img src="Images/loading_more.gif" />
        </p>
    </div>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ScrollPaging.js"%>"></script>
    <script type="text/javascript" src="<%=GetImgUrl() + "Scripts/ShowMenuHead.js"%>"></script>
</asp:Content>
<asp:Content ID="phFloorFixedPanel1" runat="server" ContentPlaceHolderID="phFloorFixedPanel">
    <div class="floor-fixed-panel" id="j-floor-fixed-panel" style="position: fixed; left: 34.5px; top: 120px; visibility: visible;">
        <asp:Literal ID="ltrFloorFixedPanel" runat="server"></asp:Literal>
    </div>
</asp:Content>
