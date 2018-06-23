<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterSite.master" AutoEventWireup="false"
    CodeFile="Register.aspx.vb" Inherits="Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
</asp:Content>

<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">

    <!---Register-->

    <div class="container" id="columns">

        <div class="breadcrumb clearfix">
            <div class="col-md-12 ">
                <asp:Label ID="LblPathWay" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="box box-primary col-lg-12 col-md-12 col-sm-12 col-xs-12" id="layout-page">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <div class="register-page">
                                <fieldset class="col-sm-6">
                                    <h1 class="text-center">Thông tin tài khoản</h1>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Email<span class="requirement">(*)</span></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="TxtEmail" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Mật khẩu<span class="requirement">(*)</span></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Xác nhận mật khẩu<span class="requirement">(*)</span></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="TxtConfirmPwd" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset class="col-sm-6">
                                    <h1 class="text-center">Thông tin khách hàng</h1>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Họ và tên<span class="requirement">(*)</span></label>
                                            <div class="col-sm-9">
                                                <span class="col-sm-3 noPM">
                                                    <asp:DropDownList ID="CboTitle" runat="server" class="form-control">
                                                        <asp:ListItem Value="0">Anh</asp:ListItem>
                                                        <asp:ListItem Value="1">Chị</asp:ListItem>
                                                    </asp:DropDownList>
                                                </span><span class="col-sm-9 noPR">
                                                    <asp:TextBox ID="TxtFullName" runat="server" class="form-control"></asp:TextBox>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Điện thoại<span class="requirement">(*)</span></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="TxtTelephone" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Số nhà, đường, phường<span class="requirement"></span></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="TxtAddress" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Tỉnh/thành phố<span class="requirement"></span></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="CboCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CboCity_SelectedIndexChanged"
                                                    class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Quận/huyện<span class="requirement"></span></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="CboDistrict" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Nhận thông tin KM</label>
                                            <div class="col-sm-9">
                                                <asp:CheckBox ID="CKReceivingPromotion" runat="server" Checked="true" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Ngày sinh<span class="requirement">(*)</span></label>
                                            <div class="col-sm-9">
                                                <span class="col-sm-4 noPM">
                                                    <asp:DropDownList ID="CboDay" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </span><span class="col-sm-4 noPR">
                                                    <asp:DropDownList ID="CboMonth" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </span><span class="col-sm-4 noPR">
                                                    <asp:DropDownList ID="CboYear" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">
                                                Số chứng minh</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="TxtPersonalID" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="text-center action_bottom">
                                <asp:Button ID="cmdRegister" runat="server" Text="Đăng ký" CssClass="ButtonW8Big"></asp:Button>
                                <asp:Label ID="LblWarning" runat="server" Text="" CssClass="requirement"></asp:Label>
                            </div>
                            <div>
                                <br />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
