<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterSite.master" AutoEventWireup="false"
    CodeFile="Payment.aspx.vb" Inherits="Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
    <script type="text/javascript">
        function delpayment(nLineNo) {
            $get("MainContent_txtPaymentLine").value = nLineNo
            $get("MainContent_CmdDelPayment").click();
        }
        function deldiscount() {
            $get("MainContent_CmdDelDiscount").click();
        }
    </script>
</asp:Content>
<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">
    <div class="container">
        <div class="pathway">
            <asp:Label ID="LblPathWay" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="payment-field">
            <div class="TitleLine" style="margin-top: 10px;">
                Bước 3: Thanh toán, chọn hình thức thanh toán</div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table class="table table-bordered">
                            <tr>
                                <td class="cart-detail" >
                                    <div style="margin-top: 10px;">
                                        <asp:Label ID="LblCart" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div style="margin: 5px;">
                                        <table >
                                            <tr >
                                                <td style="width: 110px;">
                                                    Mã giảm giá
                                                </td>
                                                <td style="width: 110px;">
                                                </td>
                                                <td style="width: 110px; padding-left: 10px;">
                                                    Số dư tài khoản
                                                </td>
                                                <td style="width: 110px;">
                                                </td>
                                                <td style="width: 540px;" rowspan="2" valign="top">
                                                    <asp:Label ID="LblCustomerAccount" runat="server" ForeColor="#ff0000" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:TextBox ID="TxtPromotionsCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td valign="top">
                                                    <asp:Button ID="cmdUpdateCart" runat="server" Text="Cập nhật" CssClass="ButtonW8Big">
                                                    </asp:Button>
                                                </td>
                                                <td style="padding-left: 10px;" valign="top">
                                                    <asp:TextBox ID="TxtBalanceAmount" runat="server" CssClass="form-control" Style="text-align: right;"
                                                        ForeColor="Red" Font-Bold="True" Font-Size="14" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td valign="top">
                                                    <asp:Button ID="cmdPayFromAccount" runat="server" Text="Cấn trừ" CssClass="ButtonW8Big" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 20px;">
                                                    <asp:Label ID="LblDiscountWarning" runat="server" ForeColor="#ff0000" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="text-align: right; padding-right: 10px;">
                                        <asp:HiddenField ID="TxtPaymentMethod" runat="server" />
                                        <asp:HiddenField ID="TxtOrderNo" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <fieldset>
                    <legend>Lựa chọn phương thức thanh toán</legend>
                    <div>
                        <input type="radio" name="payment_method_id" id="RdDirect" value="1" checked="checked">Thanh
                        toán trực tiếp cho nhân viên giao hàng sau khi nhận hàng.</div>
                    <div>
                        <br />
                    </div>
                    <div>
                        <input type="radio" name="payment_method_id" id="RdBannkTransfer" value="2">Chuyển
                        khoản qua ngân hàng (chúng tôi chỉ giao hàng sau khi quí khách chuyển khoản)</div>
                    <div>
                        <br />
                    </div>
                    <div>
                        <input type="radio" name="payment_method_id" id="RdPayOnline" value="4">Thanh toán
                        trực tuyến sử dụng thẻ nội địa (ATM đã đăng ký thanh toán qua mạng có mã OTP)</div>
                    <div style="padding-left: 20px;">
                        <br />
                        <asp:Label ID="lbATMLogo" runat="server" Text=""></asp:Label>
                        <br />
                    </div>
                    <div>
                        <br />
                    </div>
                    <div>
                        <input type="radio" name="payment_method_id" id="RdPayOnlineGC" value="5">Thanh
                        toán trực tuyến sử dụng thẻ Quốc Tế(Visa, Master, Amerian Express, ...)</div>
                    <div style="padding-left: 20px;">
                        <br />
                        <asp:Label ID="lbCRCLogo" runat="server" Text=""></asp:Label>
                        <br />
                    </div>
                    <div>
                        <br />
                    </div>
                </fieldset>
            </div>
            <div style="clear: both;">
            </div>
            <div style="padding-right: 10px; text-align: right;">
                <div style="float: left; width: 120px; padding-left: 7px;">
                    <asp:Button ID="cmdBack" runat="server" Text="QUAY LẠI" CssClass="buttonprocessback">
                    </asp:Button>
                </div>
                <div style="float: right; width: 750px; text-align: right; padding-right: 12px;">
                    <asp:Label ID="LblWaring" runat="server" Text=""></asp:Label></div>
                <asp:Button ID="cmdConfirm" runat="server" Text="HOÀN TẤT" CssClass="buttonprocessnext">
                </asp:Button>
            </div>
        </div>
        <div style="display: none;">
            <asp:Button ID="cmdShowPayment" runat="server" Text="payment" />
        </div>
        <asp:ModalPopupExtender ID="ModalPopupPayment" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="cmdShowPayment" PopupControlID="PaymentForm">
        </asp:ModalPopupExtender>
        <div id="PaymentForm" class="popup" style="width: 500px; height: 300px; display: none;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="popupheader" style="height: 20px;">
                        <b>cuahangvattu.com - Xác nhận cấn trừ tiền trong tài khoản cho đơn hàng.</b>
                    </div>
                    <div style="height: 200px; padding-left: 20px;">
                        <div style="margin-top: 10px;">
                            Số dư trong tài khoản</div>
                        <div>
                            <asp:TextBox ID="TxtBalance" runat="server" Width="200" Style="text-align: right;"
                                class="inputbox" ReadOnly="True"></asp:TextBox></div>
                        <div style="margin-top: 10px;">
                            Số tiền đơn hàng</div>
                        <div>
                            <asp:TextBox ID="TxtOrderAmount" runat="server" Width="200" Style="text-align: right;"
                                class="inputbox" ReadOnly="True"></asp:TextBox></div>
                        <div style="margin-top: 10px;">
                            Số tiền cấn trừ</div>
                        <div>
                            <asp:TextBox ID="TxtPaymentAmount" runat="server" Width="200" Style="text-align: right;"
                                class="inputbox" onkeyup="FormatNumber(this,0)"></asp:TextBox></div>
                        <div style="margin-top: 10px;">
                            Số tiền còn phải trả</div>
                        <div>
                            <asp:TextBox ID="TxtRemaining" runat="server" Width="200" Style="text-align: right;"
                                class="inputbox" ReadOnly="True"></asp:TextBox></div>
                    </div>
                    <div style="height: 20px; padding-left: 10px;">
                        <asp:Label ID="LblPayWarning" runat="server" Text="" ForeColor="#FF0000"></asp:Label></div>
                    <div style="height: 30px; text-align: right; padding-right: 10px;">
                        <asp:Button ID="cmdPayment" runat="server" Text="Đồng ý" CssClass="ButtonW8Big" />
                        <asp:Button ID="cmdClosePayment" runat="server" Text="Đóng" CssClass="ButtonW8Big" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="display: none;">
                    <asp:Button ID="CmdDelPayment" runat="server" Text="" />
                    <asp:HiddenField ID="txtPaymentLine" runat="server" />
                    <asp:Button ID="CmdDelDiscount" runat="server" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
