<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterSite.master" AutoEventWireup="false"
    CodeFile="Cart.aspx.vb" Inherits="Cart" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
    <script type="text/javascript">
        function del(LineNo) {
            $get("phMainContent_TxtLineNo").value = LineNo;
            $get("phMainContent_cmdUpdateCart").click();
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="phMainContent" runat="Server">
    <div class="collection">
        <div class="columns-container">
            <div id="columns" class="container">

                <div class="util-clearfix" id="breadView">
                    <div id="aliGlobalCrumb" class="ui-breadcrumb textuppercase">
                        
                        <asp:Label ID="LblPathWay" runat="server" Text="Label" CssClass="hidden"></asp:Label>
                    </div>
                </div>

                <div class="pathway">
                </div>

                <!--Cart-->

                <div class="main">
                    <div class="TitleLine">
                        GIỎ HÀNG CỦA BẠN
                    </div>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpCart" runat="server" >
                            <ContentTemplate>
                                <div>
                                    <asp:Label ID="LblCart" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="cart-buttons table-responsive">
                                    <asp:HiddenField ID="TxtLineNo" runat="server" />
                                    <table class="table">
                                        <tr>
                                            <td rowspan="2" valign="bottom">
                                                <asp:Button ID="cmdUpdateCart" runat="server" Text="Cập nhật giỏ hàng" CssClass="ButtonW8Big"></asp:Button>
                                                <span style="color: Red;">
                                                    <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
                                                </span>
                                                <td rowspan="2" valign="bottom" align="right">
                                                    <asp:Button ID="cmdPayment" runat="server" Text="Thanh toán" CssClass="ButtonW8Big"></asp:Button>
                                                </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="hidden">
                                    <div style="padding: 5px;">
                                        <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                    <div style="padding-left: 5px;">
                                        Để thay đổi số lượng, quý khách nhập số lượng cần thay đổi vào ô số lượng sau đó
                                nhấn nút "Cập nhật giỏ hàng".
                                    </div>
                                    <div style="padding-left: 5px;">
                                        Để bỏ sản phẩm đã chọn, quý khách click vào link "xóa" ở dòng sản phẩm muốn bỏ.
                                    </div>
                                    <div>
                                        <hr />
                                    </div>
                                    <div>
                                        <br />
                                        <br />
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table payment-options">
                                            <tr>
                                                <td style="width: 400px;">
                                                    <b>Chấp nhận thanh toán</b>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 10px;">
                                                    <asp:Image ID="ImgPayAcept" runat="server" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
