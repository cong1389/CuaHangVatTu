<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterSite.master" AutoEventWireup="false"
    CodeFile="SelectDeliveryAddress.aspx.vb" Inherits="SelectDelieveryAddress" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
    <script type="text/javascript">
        function DisplayVAT() {
            cK = document.getElementById("CkVATIssued").checked;
            element1 = document.getElementById("VATInfo")
            if (cK == true) {
                $get("MainContent_TxtVATInvoiceIssued").value = 1;
                element1.style.display = 'block';
            }
            else
                element1.style.display = 'none';
        }
        function ChangeDistrict() {
            $("#cartsumary").html('<div style="padding-top:75px;padding-bottom:75px;text-align:center;"><img src="http://www.costcom.vn/Images/Template/loading.gif"/></div>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phMainContent" runat="Server">
    <div class="container">
        <div class="pathway">
            <asp:Label ID="LblPathWay" runat="server" Text="Label"></asp:Label></div>
        <div class="delivery-info">
            <div class="TitleLine">
                Bước 2: Nhập địa chỉ nhận hàng</div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server">
                            <div class="form-horizontal">
                                <fieldset>
                                    <legend>Chọn địa chỉ giao hàng</legend>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">
                                            Người nhận<span class="requirement">(*)</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="TxtFullName" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">
                                            Điện thoại<span class="requirement">(*)</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="TxtTelephone" runat="server" class="form-control"></asp:TextBox></div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">
                                            Tỉnh/thành phố<span class="requirement">(*)</span></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="CboCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CboCity_SelectedIndexChanged"
                                                class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">
                                            Quận/huyện<span class="requirement">(*)</span></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="CboDistrict" runat="server" class="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="CboDistrict_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">
                                            Số nhà, đường, phường<span class="requirement">(*)</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="TxtHouseNo" runat="server" Width="380" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-9 control-label">
                                            Thông tin xuất hóa đơn tài chính cho công ty
                                        </label>
                                        <div class="col-sm-2">
                                            <input id="CkVATIssued" type="checkbox" onchange="DisplayVAT()" value="1" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">
                                            Lời nhắn giao hàng
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="TxtRequirement" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:HiddenField ID="txtOrderNo" runat="server" />
                                        <asp:HiddenField ID="TxtVATInvoiceIssued" runat="server" />
                                    </div>
                                    <div id="VATInfo" style="margin-left: 15px; margin-top: 10px; display: none;">
                                        <div style="margin-top: 10px;">
                                            Tên công ty</div>
                                        <div>
                                            <asp:TextBox ID="TxtBillToName" runat="server" Width="400" class="form-control"></asp:TextBox></div>
                                        <div style="margin-top: 10px;">
                                            Địa chỉ</div>
                                        <div>
                                            <asp:TextBox ID="TxtBillToAddress" runat="server" Width="400" class="form-control"></asp:TextBox></div>
                                        <div style="margin-top: 10px;">
                                            Mã số thuế</div>
                                        <div>
                                            <asp:TextBox ID="TxtTaxCode" runat="server" class="form-control"></asp:TextBox></div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="LblWaring" runat="server" Text="" ForeColor="#FF0000"></asp:Label></div>
                                </fieldset>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <fieldset>
                    <legend>Đơn hàng</legend>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="LblCartSumary" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="txtDeliveryFee" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="CboDistrict" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div  class="pull-right">
                        <i>Đơn giá đã bao gồm thuế VAT</i>
                    </div>
                </fieldset>
            </div>
            <div style="clear: both;">
            </div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div style="margin-bottom: 15px;">
                        <div style="float: left;  padding-left: 7px;">
                            <asp:Button ID="cmdBack" runat="server" Text="QUAY LẠI" CssClass="buttonprocessback">
                            </asp:Button>
                        </div>
                        <div style="float: right;  text-align: right; padding-right: 12px;">
                            <asp:Label ID="LblErr" runat="server" Text="" CssClass="requirement"></asp:Label>
                            <asp:Button ID="cmdNext" runat="server" Text="TIẾP TỤC" CssClass="buttonprocessnext">
                            </asp:Button>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
