<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ItemCard.aspx.vb" Inherits="Administrator_ItemCard" MasterPageFile="MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="maincontent">
    <div id="wrapper" style="background-color: #CFDDEE;">

        <asp:TabContainer ID="TabContainer1" runat="server" Width="100%"
            ActiveTabIndex="0" CssClass="MyTabStyle">
            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Thông tin sản phẩm">
                <ContentTemplate>

                    <div class="tabpage">
                        <div class="toolbox">
                            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
                            <asp:Button ID="cmdCopy" runat="server" Text="Copy" CssClass="button" />
                            <asp:Button ID="cmdDel" runat="server" Text="Xóa" CssClass="button" />
                            <asp:Button ID="cmdAddNewItem" runat="server" Text="Thêm mới" CssClass="button" />
                            <asp:Button ID="cmdItemList" runat="server" Text="Danh sách" CssClass="button" />
                            <asp:Label ID="LblWarning1" runat="server" Text=""></asp:Label></b>
                            <asp:HiddenField ID="TxtPageNo" runat="server" />
                            <div style="clear: both;"></div>
                        </div>
                        <div style="width: 1000px; margin: 0px auto 0px auto;">
                            <table class="advform" cellspacing="2">
                                <tr>
                                    <td class="title">Hiển thị</td>
                                    <td>
                                        <asp:CheckBox ID="CKPublished" runat="server" /></td>
                                    <td class="title">Nhóm menu</td>
                                    <td>
                                        <asp:DropDownList ID="CboMenuGroup" runat="server" Width="200px" CssClass="inputbox"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title">Mã sản phẩm</td>
                                    <td>
                                        <asp:TextBox ID="TxtNo_" runat="server" Width="200px"></asp:TextBox>
                                        <asp:HiddenField ID="TxtItemNo" runat="server" />
                                    </td>
                                    <td class="title">Hãng sản xuất</td>
                                    <td>
                                        <asp:DropDownList ID="CboBrand" runat="server" Width="200px" CssClass="inputbox"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="title">Tên sản phẩm</td>
                                    <td>
                                        <asp:TextBox ID="TxtName" runat="server" Width="300px"></asp:TextBox></td>
                                    <td class="title">Xuất xứ</td>
                                    <td>
                                        <asp:DropDownList ID="CboOriginCountry" runat="server" Width="200px"
                                            CssClass="inputbox">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="title">Model</td>
                                    <td>
                                        <asp:TextBox ID="TxtModel" runat="server" Width="200px"></asp:TextBox></td>
                                    <td class="title">Sản phẩm mới</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 40px;" align="left">
                                                    <asp:CheckBox ID="CKNewProduct" runat="server" /></td>
                                                <td style="width: 100px;">Sản phẩm bán chạy</td>
                                                <td>
                                                    <asp:CheckBox ID="CKBestSelling" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title">Ngành hàng</td>
                                    <td>
                                        <asp:DropDownList ID="CboDivision" runat="server" Width="200px" CssClass="inputbox" AutoPostBack="True"
                                            OnSelectedIndexChanged="CboDivision_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    <td class="title">Sản phẩm khuyến mãi</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 40px;">
                                                    <asp:CheckBox ID="CKPromotions" runat="server" /></td>
                                                <td style="width: 100px;">Sản phẩm hot</td>
                                                <td>
                                                    <asp:CheckBox ID="CKHot" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="title">Loại hàng</td>
                                    <td>
                                        <asp:DropDownList ID="CboCategory" runat="server" Width="200px"
                                            AutoPostBack="True" OnSelectedIndexChanged="CboCategory_SelectedIndexChanged"
                                            CssClass="inputbox">
                                        </asp:DropDownList></td>
                                    <td class="title">Ẩn thuộc tính</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 40px;">
                                                    <asp:CheckBox ID="CKHideFeature" runat="server" /></td>
                                                <td style="width: 100px;">Đã hết hàng</td>
                                                <td>
                                                    <asp:CheckBox ID="CKNotInStock" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title">Nhóm hàng</td>
                                    <td>
                                        <asp:DropDownList ID="CboProductGroup" runat="server" Width="200px"
                                            CssClass="inputbox">
                                        </asp:DropDownList></td>
                                    <td class="title">Hàng sắp về</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 40px;">
                                                    <asp:CheckBox ID="CKGoodGoingOn" runat="server" /></td>
                                                <td style="width: 100px;">Thể tích</td>
                                                <td>&nbsp;<asp:TextBox ID="TxtVolume" runat="server" Width="100px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title">Số thứ tự</td>
                                    <td>
                                        <asp:TextBox ID="TxtOrderPosition" runat="server" Width="96px"></asp:TextBox>
                                        &nbsp;&nbsp; Từ ngày&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TxtFromDate" runat="server" Width="96px">
                                        </asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                            TargetControlID="TxtFromDate" Format="dd/MM/yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="title">Số lượng tồn</td>
                                    <td>
                                        <asp:TextBox ID="TxtStock" runat="server" Width="100px"></asp:TextBox>
                                        &nbsp;&nbsp; Giá bán&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtSalesPrice" runat="server" Width="100px"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                            TargetControlID="TxtFromDate" Format="dd/MM/yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title">Tỷ lệ thuế</td>
                                    <td>
                                        <asp:DropDownList ID="CboTaxPercent" runat="server" Width="100px"
                                            CssClass="inputbox">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;Đơn vị tính
                                        <asp:DropDownList ID="CboUnitOfMeasure" runat="server" Width="100px"
                                            CssClass="inputbox">
                                        </asp:DropDownList></td>
                                    <td class="title">Tiêu đề</td>
                                    <td>
                                        <asp:TextBox ID="TxtPateTitle" runat="server" Width="300px" ClientIDMode="Static"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="title" valign="top"></td>
                                    <td valign="top"></td>
                                    <td class="title"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="title" valign="top">Từ khóa</td>
                                    <td>
                                        <asp:TextBox ID="TxtMetaKeyword" runat="server" Width="300px" TextMode="MultiLine" Height="30px"></asp:TextBox></td>
                                    <td class="title" valign="top">Link</td>
                                    <td>
                                        <asp:TextBox ID="TxtLink" runat="server" Width="300px" TextMode="MultiLine" Height="30px" ClientIDMode="Static"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="title" valign="top">Chạy</td>
                                    <td>
                                        <asp:CheckBox ID="ckIsRun" runat="server" /></td>
                                    <td class="title" valign="top"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="title" valign="top">Tính năng nổi bật </td>
                                    <td colspan="3">
                                        <%-- <asp:TextBox ID="txtDescriptions" runat="server" Height="150px" 
                                            TextMode="MultiLine" Width="807px"></asp:TextBox>--%>
                                        <FCKeditorV2:FCKeditor ID="txtDescriptions" runat="server" BasePath="~/administrator/fckeditor/" Height="500px" AutoDetectLanguage="false" DefaultLanguage="en" Width="807px">
                                        </FCKeditorV2:FCKeditor>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title" valign="top">Ảnh sản phẩm</td>
                                    <td>
                                        <div style="width: 183px">
                                            <asp:FileUpload ID="FileUploadFullImg" runat="server"
                                                Width="180px" CssClass="inputbox" Height="20px" />
                                        </div>
                                        <div>
                                            <asp:CheckBox ID="CKCreateThumbAuto" runat="server" Checked="True" />Tự động tạo thumbnail
                                        </div>

                                        <div style="margin-top: 5px;">
                                            <asp:Image ID="imgFull" runat="server" Width="120px" />
                                        </div>
                                    </td>
                                    <td class="title" valign="top">Ảnh nhỏ</td>
                                    <td valign="top">
                                        <div>
                                            <asp:FileUpload ID="FileUploadSmallImg" runat="server" />
                                        </div>
                                        <div style="margin-top: 5px;">
                                            <asp:Image ID="imgSmall" runat="server" Width="120px" />
                                        </div>
                                        <asp:HiddenField ID="FileNameImgFull" runat="server" />
                                        <asp:HiddenField ID="FileNameImgSmall" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Giá bán">
                <ContentTemplate>
                    <div class="tabpage">
                        <div class="toolbox">
                            <div style="padding: 6px;">
                                <b>
                                    <asp:Label ID="LblWarning2" runat="server"></asp:Label></b>
                            </div>
                        </div>
                        <div class="advform">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <p>Loading</p>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <asp:GridView ID="grdSalesPrice" runat="server" AutoGenerateColumns="False"
                                            Width="100%" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3">
                                            <Columns>
                                                <asp:BoundField DataField="Line No_" HeaderText="Line No." />
                                                <asp:BoundField DataField="ProName" HeaderText="Khu vực" />
                                                <asp:BoundField DataField="Sales Price No_" HeaderText="Loại giá" />
                                                <asp:BoundField DataField="Unit Of Measure No_" HeaderText="Đơn vị tính" />
                                                <asp:BoundField DataField="Starting Date" HeaderText="Ngày bắt đầu" />
                                                <asp:BoundField DataField="Ending Date" HeaderText="Ngày kết thúc" />
                                                <asp:BoundField DataField="Unit Price"
                                                    HeaderText="Giá bán" DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="Deal Price"
                                                    HeaderText="Giá Khuyến mãi" DataFormatString="{0:n}" />
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:CommandField ShowSelectButton="True" />
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                Font-Size="11px" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        </asp:GridView>
                                    </div>
                                    <div style="margin: 15px;">
                                        <table>
                                            <tr>
                                                <td>Khu vực</td>
                                                <td>Loại giá</td>
                                                <td>Đơn vị tính</td>
                                                <td>Ngày bắt đầu</td>
                                                <td>Ngày kết thúc</td>
                                                <td>Nguyên giá</td>
                                                <td>Giá khuyến mãi</td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:DropDownList ID="CboStoreGroup" runat="server" CssClass="inputbox" Width="120px"></asp:DropDownList></td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="CboSalesPriceNo" runat="server" CssClass="inputbox" Width="80px"></asp:DropDownList></td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="CboSalesUnitOfMeasure" runat="server" CssClass="inputbox" Width="80px"></asp:DropDownList></td>
                                                <td valign="top">
                                                    <asp:TextBox ID="TxtStartingDate" runat="server" Width="100px"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        TargetControlID="TxtStartingDate" Format="dd/MM/yyyy" Enabled="True">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="TxtEndingDate" runat="server" Width="100px"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender4" runat="server"
                                                        TargetControlID="TxtEndingDate" Format="dd/MM/yyyy" Enabled="True">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="TxtUnitPrice" runat="server" Width="100px"></asp:TextBox></td>
                                                <td valign="top">
                                                    <asp:TextBox ID="TxtDealPrice" runat="server" Width="100px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:HiddenField ID="TxtAction" runat="server" />
                                                    <asp:HiddenField ID="TxtLineNo" runat="server" />
                                                    <asp:Button ID="cmdNew" runat="server" Text="New" CssClass="button" />
                                                    <asp:Button ID="cmdSavePrice" runat="server" Text="Save" CssClass="button" />
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="padding-left: 10px;">
                                        <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Khuyến mãi" Visible="false">
                <HeaderTemplate>Quà tặng</HeaderTemplate>
                <ContentTemplate>
                    <div class="tabpage">
                        <div class="toolbox">
                        </div>
                    </div>
                    <div class="advform">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <p>Loading</p>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div>
                                    <asp:GridView ID="grdMMSalesPrice" runat="server" AutoGenerateColumns="False"
                                        Width="100%" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None"
                                        BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField="Discount No_" HeaderText="Chương trình" />
                                            <asp:BoundField DataField="Line No_" HeaderText="Line No." />
                                            <asp:BoundField DataField="Item No_" HeaderText="Mã hàng" />
                                            <asp:BoundField DataField="Item Name" HeaderText="Tên sản phẩm" />
                                            <asp:BoundField DataField="Starting Date" HeaderText="Ngày bắt đầu" />
                                            <asp:BoundField DataField="Ending Date" HeaderText="Ngày kết thúc" />
                                            <asp:BoundField DataField="Origin Price" HeaderText="Giá niêm yết" DataFormatString="{0:n}" />
                                            <asp:BoundField DataField="Deal Price" HeaderText="Giá bán" DataFormatString="{0:n}" />
                                            <asp:BoundField DataField="Discount Type" HeaderText="Cách tính" />
                                            <asp:CheckBoxField DataField="Is Trigger" HeaderText="Is Trigger" />
                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                                <div style="margin: 5px;">
                                    <table>
                                        <tr>
                                            <td>Loại</td>
                                            <td>Mã chương trình</td>
                                            <td>Mã sản phẩm</td>
                                            <td>Khu vực</td>
                                            <td>Loại giá</td>
                                            <td>Ngày bắt đầu</td>
                                            <td>Ngày kết thúc</td>
                                            <td>Nguyên giá</td>
                                            <td>Giá bán</td>
                                            <td>Số lượng</td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="CboLineType" runat="server">
                                                    <asp:ListItem Value="0">Sản phẩm chính</asp:ListItem>
                                                    <asp:ListItem Value="1">Hàng tặng</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtMMPeriodicNo" runat="server" Width="100px"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="TxtItemNoMM" runat="server" Width="100px"></asp:TextBox></td>
                                            <td>
                                                <asp:DropDownList ID="CboMMStoreGroup" runat="server" CssClass="inputbox" Width="100px"></asp:DropDownList></td>
                                            <td>
                                                <asp:DropDownList ID="CboMMSalesPriceGroup" runat="server" CssClass="inputbox" Width="80px"></asp:DropDownList></td>
                                            <td>
                                                <asp:TextBox ID="TxtMMStartingDate" runat="server" Width="80px">
                                                </asp:TextBox><asp:CalendarExtender ID="CalendarExtender5" runat="server"
                                                    TargetControlID="TxtMMStartingDate" Format="dd/MM/yyyy" Enabled="True">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="TxtMMEndingDate" runat="server" Width="80px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender6" runat="server"
                                                    TargetControlID="TxtMMEndingDate" Format="dd/MM/yyyy" Enabled="True">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="TxtMMOriginPrice" runat="server" Width="90px"></asp:TextBox></td>
                                            <td>
                                                <asp:DropDownList ID="CboDiscountType" runat="server">
                                                    <asp:ListItem Value="0">Deal Price</asp:ListItem>
                                                    <asp:ListItem Value="1">Discount %</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="TxtMMSalesPrice" runat="server" Width="90px"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:HiddenField ID="TxtMMAction" runat="server" />
                                                <asp:HiddenField ID="TxtMMLineNo" runat="server" />
                                                <asp:Button ID="cmdNewMM" runat="server" Text="New" CssClass="button" />
                                                <asp:Button ID="cmdSaveMM" runat="server" Text="Save" CssClass="button" />
                                        </tr>
                                    </table>
                                </div>
                                <div style="padding-left: 10px;">
                                    <asp:Label ID="LblMMWarning" runat="server" Text=""></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Thuộc tính">
                <HeaderTemplate>Thuộc tính</HeaderTemplate>
                <ContentTemplate>
                    <div class="tabpage">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="toolbox">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Button ID="cmdSaveFeature" runat="server" Text="Save" CssClass="button" /></td>
                                            <td><b>Cập nhật thuộc tính sản phẩm:<%=sItemName%></b></td>
                                            <td>
                                                <asp:Label ID="LblWarningFeature" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <asp:Label ID="LblFeature" runat="server" Text=""></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Hình ảnh">
                <HeaderTemplate>Hình ảnh</HeaderTemplate>
                <ContentTemplate>
                    <div class="tabpage">
                        <div class="toolbox">
                            <asp:Button ID="cmdUploadImg" runat="server" Text="Save" CssClass="button" />
                            <asp:Button ID="cmdDelImg" runat="server" Text="Delete" CssClass="button" />
                            <asp:Label ID="LblWarningUploadImg" runat="server" Text=""></asp:Label>
                        </div>

                        <div style="padding-left: 10px;">
                            <asp:FileUpload ID="UplImagesDetail" runat="server" Width="300px" CssClass="inputbox" Height="20px" />
                        </div>
                        <div>
                            <asp:Label ID="LblImg" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="Bài viết">
                <HeaderTemplate>Bài viết</HeaderTemplate>
                <ContentTemplate>
                    <div class="tabpage">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="toolbox">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Button ID="cmdSaveContent" runat="server" Text="Save" CssClass="button" /></td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <asp:HiddenField ID="TxtContentNo" runat="server" />
                                    <FCKeditorV2:FCKeditor ID="TxtContent" runat="server" BasePath="~/administrator/fckeditor/" Height="500px" AutoDetectLanguage="false" DefaultLanguage="en" Width="1000">
                                    </FCKeditorV2:FCKeditor>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="Hình ảnh">
                <HeaderTemplate>Màu sắc sản phẩm</HeaderTemplate>
                <ContentTemplate>
                    <div class="tabpage">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="toolbox">
                                    <b>
                                        <asp:Label ID="LblWarningItemColor" runat="server" Text=""></asp:Label></b>
                                </div>
                                <div style="width: 1000px; margin: 0px auto 0px auto; padding: 7px;">
                                    <table cellspacing="2" style="background: #F1F3F5" width="100%">
                                        <tr>
                                            <td>Sản phẩm gốc</td>
                                            <td>Mã sản phẩm</td>
                                            <td>Tên sản phẩm</td>
                                            <td>Màu sắc</td>
                                            <td>Diễn giải</td>
                                            <td>Active</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtOriginItemNo" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtItemColorNo" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtItemColorName" runat="server" Width="220px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Panel ID="PanelColor" Style="width: 18px; height: 18px; border: 1px solid #000; float: left; margin-top: 2px;" runat="server" />
                                                &nbsp;
                                            <asp:TextBox ID="TxtColor" runat="server" Width="100px"></asp:TextBox>

                                                <asp:ColorPickerExtender
                                                    ID="ColorPickerExtender1" runat="server" TargetControlID="TxtColor" SampleControlID="PanelColor">
                                                </asp:ColorPickerExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtItemColorDescription" runat="server" Width="320px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CKPublishedItemColor" runat="server" />
                                                <asp:HiddenField ID="TxtItemColorLineNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Button ID="CmdSaveItemColor" runat="server" Text="Save" CssClass="button" />
                                                &nbsp;
                                            <asp:Button ID="CmdAutoLoad" Enabled="false" runat="server" Text="Auto Load" CssClass="button" />
                                                &nbsp;
                                            <asp:Button ID="cmdNewItemColor" runat="server" Text="Cancel" CssClass="button" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="width: 1000px; margin: 0px auto 0px auto; padding: 7px;">
                                    <asp:GridView ID="GrdItemColor" runat="server" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#CC9966" BorderWidth="1px"
                                        CellPadding="4" Width="100%" BorderStyle="None">
                                        <Columns>
                                            <asp:BoundField DataField="Item Origin No_" HeaderText="Sản phẩm nguồn" />
                                            <asp:BoundField DataField="Item No_" HeaderText="Mã sản phẩm" />
                                            <asp:BoundField DataField="Item Name" HeaderText="Tên sản phẩm" />
                                            <asp:BoundField DataField="Item Color" HeaderText="Màu sắc" />
                                            <asp:BoundField DataField="Description" HeaderText="Diễn giải" />
                                            <asp:CheckBoxField DataField="Published" HeaderText="Active" />
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:CommandField ShowDeleteButton="True" />
                                        </Columns>
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099"
                                            HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#330099" />
                                        <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" Font-Bold="True" />
                                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>

    <div style="display: none;">
        <asp:Button ID="cmdShowCopy" runat="server" Text="Show" CssClass="button"></asp:Button>
    </div>
    <asp:ModalPopupExtender ID="ModalPopupCopy" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="cmdShowCopy" PopupControlID="PanelCopy" OkControlID="cmdCancelCopy">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelCopy" runat="server" Style="display: none; height: 260px; width: 400px; background: #fff;" CssClass="popup">
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div class="popupheader" style="height: 20px;">
                    <b>Copy sản phẩm</b>
                </div>
                <div style="margin-top: 50px; margin-left: 10px;">Nhập mã sản phẩm</div>
                <div style="margin-top: 5px; margin-left: 10px;">
                    <asp:TextBox ID="TxtNewItemNo" runat="server" Width="250px"></asp:TextBox>
                </div>
                <div style="height: 30px; text-align: right; padding-right: 10px; margin-top: 100px;">
                    <asp:Button ID="cmdOKCopy" runat="server" Text="OK" CssClass="button"></asp:Button>
                    <asp:Button ID="cmdCancelCopy" runat="server" Text="Đóng" CssClass="button"></asp:Button>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <script>
        $(document).ready(function () {
            CopyValue();
        });

        function CopyValue() {

            //VN
            $("#<%=TxtPateTitle.ClientID%>").change(function () {
                var name = $("#<%=TxtPateTitle.ClientID%>").val();
                if (name != "") {
                    $("#<%=TxtLink.ClientID%>").val(RemoveUnicode(name));
                }

            });

        };
    </script>

</asp:Content>


