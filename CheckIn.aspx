<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterSite.master" AutoEventWireup="false"
    CodeFile="CheckIn.aspx.vb" Inherits="CheckIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
</asp:Content>
<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">
    <div class="container" id="CheckIn">
        <div class="pathway box-header  ">
            <asp:Label ID="LblPathWay" runat="server" Text="Label"></asp:Label></div>
        <div class="col-lg-4 ">
            <div class="box box-warning">
                <div class="box-header with-border ">
                    <h4 class="box-title">
                        Nếu bạn đã tạo tài khoản tại cuahangvattu.com rồi thì đăng nhập ở đây</h4>
                </div>
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="col-lg-12 form-group">
                            Sử dụng thông tin đã đăng ký để đăng nhập</div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">
                                Email<span class="requirement">(*)</span></label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">
                                Password<span class="requirement">(*)</span></label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="col-lg-push-3 col-lg-12 col-sm-9 form-group">
                            <asp:LinkButton ID="cmdGetPwd" runat="server">Quên mật khẩu</asp:LinkButton>
                        </div>
                        <asp:ModalPopupExtender ID="ModalPopupExtenderPwd" runat="server" BackgroundCssClass="modalBackground"
                            TargetControlID="cmdGetPwd" PopupControlID="PanelgetPwd" OkControlID="cmdCancelGetPwd">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="PanelgetPwd" runat="server" Style="display: none; height: 200px; width: 400px;
                            background: #c8c8c8;" CssClass="modal">
                            <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                                <ProgressTemplate>
                                    <div style="width: 400px; height: 200px; position: fixed; text-align: center; background: #c8c8c8;">
                                        <div style="margin: 70px;">
                                            <img src="../Images/Template/loading.gif" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePnGetPwd" runat="server">
                                <ContentTemplate>
                                    <div style="padding: 5px; background-color: #e5e5e5;">
                                        Nhập vào địa chỉ email của bạn. Chúng tôi sẽ gửi mật khẩu qua email của bạn.
                                    </div>
                                    <div style="padding-top: 30px; padding-left: 20px;">
                                        Email:
                                    </div>
                                    <div style="padding-left: 20px;">
                                        <asp:TextBox ID="TxtEmailFogot" runat="server" Width="300px"></asp:TextBox>
                                    </div>
                                    <div style="padding-left: 20px; height: 40px;">
                                        <asp:Label ID="LblErrEmail" runat="server" Text="" class="requirement"></asp:Label>
                                    </div>
                                    <div>
                                        <center>
                                                <asp:Button ID="cmdOKGetPwd" runat="server" Text="Gửi mail cho tôi" Cssclass="ButtonW8Big"></asp:Button>
                                                <asp:Button ID="cmdCancelGetPwd" runat="server" OnClientClick="ClosePoupAdd()" Text="Đóng" Cssclass="ButtonW8Big"></asp:Button>
                                                </center>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <div class="col-lg-6">
                            Đăng nhập với:&nbsp; <a href="<%="https://www.facebook.com/dialog/oauth?client_id=628479280520933&redirect_uri=" + GetURL() + "facebookCallback.aspx" %>">
                                <img src="<%=GetURL()+"Images/Icon/facebook_icon_tiny.jpg" %>" /></a>
                        </div>
                        <div class="col-lg-5">
                            <asp:Button ID="cmdLogin" runat="server" Text="Đăng nhập" CssClass="ButtonW8Big">
                            </asp:Button>
                        </div>
                        <asp:Label ID="LblErrCheckin" runat="server" Text="" class="requirement"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4 ">
            <div class="box box-warning">
                <div class="box-header with-border">
                    <h4 class="box-title">
                        Nếu bạn chưa có tài khoản tại cuahangvattu.com thì tạo tài khoản mới ở đây</h4>
                </div>
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="col-lg-12 form-group">
                            Tạo tài khoản mang lại lợi ích:
                            <ul class="regis">
                                <li><i class="fa fa-angle-double-right"></i>Lần sau mua hàng thuận tiện hơn.</li>
                                <li><i class="fa fa-angle-double-right"></i>Tích lũy điểm thưởng.</li>
                                <li><i class="fa fa-angle-double-right"></i>Được cấp mã số để giới thiệu bạn bè.</li>
                                <li><i class="fa fa-angle-double-right"></i>Tạo tài khoản mới thanh toán được bằng thẻ.</li>
                            </ul>
                        </div>
                    </div>
                    <div class="box-footer">
                        </br></br>
                        <asp:Button ID="cmdRegister" runat="server" Text="Tạo tài khoản" CssClass="ButtonW8Big">
                        </asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4 ">
            <div class="box box-warning">
                <div class="box-header with-border">
                    <h4 class="box-title">
                        Mua nhanh không cần đăng ký</h4>
                </div>
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="col-lg-12 form-group">
                            Nếu không đăng ký:
                            <ul class="regis">
                                <li><i class="fa fa-angle-double-right"></i>Mỗi lần mua bạn lại phải khai báo thông
                                    tin.</li>
                                <li><i class="fa fa-angle-double-right"></i>Không được tích lũy điểm thưởng.</li>
                                <li><i class="fa fa-angle-double-right"></i>Không có mã số để giới thiệu bạn bè.</li>
                            </ul>
                        </div>
                    </div>
                    <div class="box-footer">
                        </br></br>
                        <asp:Button ID="cmdQuickOrder" runat="server" Text="Mua không cần tài khoản" CssClass="ButtonW8Big">
                        </asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <ul>
                <li>Nếu bạn quên mật khẩu click link [quên mật khẩu] để lấy lại mật khẩu. </li>
                <li><span class="requirement">Lưu ý: những ô có dấu (*) là ô bắt buộc phải điền thông
                    tin.</span> </li>
            </ul>
            <div class="control-button">
                <asp:Button ID="cmdBack" runat="server" Text="QUAY LẠI" CssClass="buttonprocessback">
                </asp:Button>
            </div>
        </div>
    </div>
    <script>
        function ClosePoupAdd() {
            location.href = location.href;
        }
    </script>
</asp:Content>
