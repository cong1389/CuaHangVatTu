<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="MasterPage/MasterSite.master"
    CodeFile="AllCategory.aspx.vb" Inherits="AllCategory" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
</asp:Content>
<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">
    <style>
        
    </style>
    <div class="c-warp col-lg-push-3 col-lg-7">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3>
                    Tất cả</h3>
            </div>
            <div class="box-body">
                <div class="con-main">
                    <asp:Literal runat="server" ID="ltrContent"></asp:Literal>
                </div>
                <div class="cg-main" data-spm="1" data-spm-max-idx="347">
                    <asp:Repeater ID="rptAllcategory" runat="server" OnItemDataBound="rptAllcategory_OnItemDataBound">
                        <ItemTemplate>
                            <div class="item util-clearfix">
                                <h3 class="big-title anchor1 anchor-agricuture">
                                    <span id="anchor1" class="anchor-subsitution"></span><i class="cg-icon1"></i>
                                    <%--<a href="#">Women's Clothing &amp; Accessories</a>--%>
                                    <asp:HyperLink ID="hpLinkParent" runat="server"></asp:HyperLink>
                                </h3>
                                <div class="sub-item-wrapper util-clearfix">
                                    <div class="sub-item-cont-wrapper">
                                        <ul class="sub-item-cont util-clearfix">
                                            <%--<li><a href="#">Dresses</a> </li>--%>
                                            <asp:Repeater ID="rptSubcategory" runat="server" OnItemDataBound="rptSubcategory_OnItemDataBound">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:HyperLink ID="hpLinkSubCat" runat="server"></asp:HyperLink></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
