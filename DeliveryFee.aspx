<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterSite.master" AutoEventWireup="false"
    CodeFile="DeliveryFee.aspx.vb" Inherits="DeliveryFee" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="phHeader" runat="server">
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/MainStyle2.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/MainStyle1.css" %>" />
    <link rel="stylesheet" href="<%=GetImgUrl() +"Styles/content.css"%>" />
    <link rel="stylesheet" href="<%=GetImgUrl() + "Styles/themesele.css"%>" />
</asp:Content>
<asp:Content ID="phcontent1" runat="server" ContentPlaceHolderID="phMainContent">
    <div class="container">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="">
                    <asp:Label ID="LblPathWay" runat="server" Text=""></asp:Label></div>
                <div class="TitleLine policy">
                    Chính sách giao hàng</div>
                <div class="">
                    <div class=" box-header with-border ">
                        cuahangvattu.com giao hàng tới tất cả các khu vực tỉnh thành trên phạm vi toàn lãnh
                        thổ Việt Nam. Chính sách cụ thể như sau:
                    </div>
                    <div class="policy-title">
                        01. Phân vùng giao hàng</div>
                    <div style="margin-top: 10px; margin-left: 10px;">
                        Để phục vụ cho việc giao hàng được liên tục và xuyên suốt, khu vực giao hàng trong
                        cả nước được phân ra thành 11 (mười một) phân vùng dựa theo sự phân chia của tỉnh
                        /thành và quận/ huyện
                    </div>
                    <div style="margin-top: 10px; margin-left: 10px;">
                        Chi phí đóng gói và giao hàng, phí phụ thu cho hàng cồng kềnh tới địa điểm giao
                        hàng sẽ khác nhau tại mỗi phân vùng và bạn có thể kiểm tra theo thông tin bên dưới.
                    </div>
                    <div style="margin-top: 10px; margin-left: 10px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-3 noPM">
                                    <label class="form-group">
                                        Chọn tỉnh / thành phố</label>
                                    <asp:DropDownList ID="CboProvince" runat="server" CssClass="form-control form-group"
                                        AutoPostBack="true" OnSelectedIndexChanged="CboProvince_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="table-responsive" style="margin-top: 15px;">
                                    <asp:Label ID="LblDictricByZone" runat="server" Text=""></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="policy-title">
                        02. Chi phí đóng gói và giao hàng tận nhà</div>
                    <div style="margin-top: 10px; margin-left: 10px;">
                        cuahangvattu.com áp dụng mức chi phí đóng gói và giao hàng mới theo bảng dưới cho
                        tất cả đơn hàng giao hàng tận nhà có giá trị đơn hàng dưới 1,000,000 đồng. Các đơn
                        hàng sẽ có mức phí đóng gói và giao hàng được tính như sau:
                    </div>
                    <div class="selected-province-inner2" style="margin-top: 15px; margin-left: 10px;">
                        <asp:Label ID="LblDeliveryFeeByAmount" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="font-size: 15px; font-weight: bold; margin-top: 15px;">
                        Lưu ý:</div>
                    <div>
                        <ul>
                            <li>Giá trị đơn hàng tối thiểu được áp dụng cho tất cả các danh mục là <strong>100.000
                                VND</strong>.</li>
                            <li>cuahangvattu.com giao hàng miễn phí cho đơn hàng trên 1.000.000 VND <strong>(trừ
                                phân vùng 6 là 1.500.000 VND trở lên)</strong>. Chi phí này <strong>không bao gồm Phí
                                    hàng cồng kềnh</strong> áp dụng cho một số sản phẩm tại cuahangvattu.com quy
                                định tại mục 3 - <strong>Phí phụ thu cho hàng cồng kềnh</strong>.</li>
                        </ul>
                    </div>
                    <div class="policy-title">
                        03. Phí phụ thu cho hàng cồng kềnh</div>
                    <div style="margin-top: 10px; margin-left: 10px;">
                        cuahangvattu.com áp dụng mức phí phụ thu cho hàng cồng kềnh với biểu phí được tính
                        dựa theo kích cỡ của bao bì sản phẩm của mỗi sản phẩm. Chi tiết như sau:
                    </div>
                    <div class="selected-province-inner2" style="margin-top: 15px; margin-left: 10px;">
                        <asp:Label ID="LblDeliveryFeeByVolume" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="margin-top: 10px; margin-left: 10px;">
                        Cách tính kích thước hàng hóa: <strong>Kích thước (vw) = (dài x rộng x cao)/ 6000</strong></div>
                    <div style="margin-top: 10px; margin-left: 10px;">
                        <strong>Lưu ý:</strong> Chỉ những sản phẩm có kích thước (vw) lớn hơn 10 vw mới
                        được hiển thị tem Hàng cồng kềnh, những sản phẩm không có tem vẫn có thể bị tính
                        phí theo bảng chi phí trên.<br />
                        Vì phụ phí cho hàng cồng kềnh được tính dựa theo kích thước bao bì sản phẩm của
                        từng sản phẩm trong đơn hàng nên nếu đơn hàng của quý khách gồm nhiều sản phẩm,
                        phụ phí cho hàng cồng kềnh sẽ được lũy tiến tăng dần cho từng sản phẩm.
                    </div>
                    <div class="policy-title">
                        04. Thời gian giao hàng</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div style="margin-top: 10px; margin-left: 10px;" class="col-sm-3 noPM">
                                <asp:DropDownList ID="CboTimeDelivery" runat="server" CssClass="form-control form-group"
                                    AutoPostBack="true" OnSelectedIndexChanged="CboTimeDelivery_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="selected-province-inner2 LblDi table-responsive" style=" margin-left: 10px;">
                                <asp:Label ID="LblDistrictList" runat="server" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="margin-top: 15px; margin-left: 10px;">
                        <i>* Lưu ý: Thời gian trên là thời gian dự kiến. Thời gian giao hàng chính thức có thể
                            sớm hơn hoặc trễ hơn một vài ngày tùy theo một số điều kiện khách quan mà Sieuthigiare
                            <strong>không thể kiểm soát được</strong> (Ví dụ: Thiên tai, lũ lụt, hỏng hóc phương
                            tiện...)</i></div>
                    <div style="margin-top: 15px; margin-left: 10px;">
                        <i>* Những sản phẩm có <strong>tem giao hàng nhanh</strong> sẽ được ưu tiên giao hàng
                            sớm hơn những sản phẩm khác. Lưu ý: đối với những đơn hàng có <strong>ít nhất 1 sản
                                phẩm không có tem giao hàng nhanh</strong> thì đơn hàng đó sẽ được áp dụng giao
                            hàng theo thời gian như đơn hàng bình thường.</i></div>
                    <div style="margin-top: 15px; margin-left: 10px;">
                        Trong một số trường hợp bất khả kháng,khách hàng sẽ nhận được hàng trong vòng 7-14
                        ngày.</div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
