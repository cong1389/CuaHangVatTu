<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CtrListPage.ascx.vb"
    Inherits="Common_CtrListPage" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:LinkButton ID="lbtnChangePageSize" runat="server" Style="display: none" OnClick="lbtnChangePageSize_Click">
</asp:LinkButton>
<div id="pagination-bottom" class="ui-pagination ui-pagination-body util-clearfix">
    <div class="ui-pagination-navi util-left">
        <asp:Panel ID="PagerPanel" runat="server">
            <span id="PreviousPagerContainer" runat="server">
                <asp:LinkButton ID="PreviousPager" runat="server" CssClass="ui-pagination-prev" OnClick="PreviousPager_Click"
                    CausesValidation="False"><i class="fa fa-angle-double-left"></i></asp:LinkButton>
            </span>
            <asp:Repeater ID="PageList" runat="server" OnItemCommand="PageList_ItemCommand" OnItemDataBound="PageList_ItemDataBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <span id="Item" runat="server">
                        <asp:LinkButton ID="PageIndexLink" CausesValidation="false" CommandName='<%# Eval("Key") %>'
                            runat="server"><%# Eval("Value") %></asp:LinkButton>
                    </span>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <span id="NextPagerContainer" runat="server">
                <asp:LinkButton ID="NextPager" runat="server" OnClick="NextPager_Click" CssClass="page-next ui-pagination-next"
                    CausesValidation="False"><i class="fa fa-angle-double-right"></i></asp:LinkButton>
            </span>
        </asp:Panel>
    </div>
</div>
