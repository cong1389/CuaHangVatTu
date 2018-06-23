<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="MenuGroupCard.aspx.vb" Inherits="Administrator_MenuGroupCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="Server">
    <script type="text/javascript">
        function choosefeature(FeatureCode, FeatureName) {
            $get('maincontent_TabContainer1_TabPanel2_TxtFeatureNo').value = FeatureCode;
            $get('maincontent_TabContainer1_TabPanel2_TxtFeatureCode').value = FeatureCode;
            $get('maincontent_TabContainer1_TabPanel2_TxtFeatureName').value = FeatureName;
            var modalPopupExtend = $find('maincontent_TabContainer1_TabPanel2_ModalPopupExtender1');
            modalPopupExtend.hide();
        }
    </script>

    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
        Width="100%" Height="600">
        <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
            <HeaderTemplate>
                Thông tin nhóm menu
            </HeaderTemplate>
            <ContentTemplate>
                <div class="tabpage">
                    <div style="padding: 5px; height: 22px;">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                    <div style="width: 1000px; margin: 0px auto 0px auto;">
                        <div style="margin-bottom: 10px;">
                            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="cmdList" runat="server" Text="Cancel" CssClass="button" />
                        </div>
                        <div>
                            <table class="advform">
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 120px;">Mã</td>
                                    <td>
                                        <asp:TextBox ID="TxtNo_" runat="server" Enabled="False"></asp:TextBox></td>
                                    <td>Trang</td>
                                    <td>
                                        <asp:DropDownList ID="CboPage" runat="server" Width="200px" CssClass="inputbox">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tên</td>
                                    <td>
                                        <asp:TextBox ID="TxtName" runat="server" Width="250px"></asp:TextBox></td>
                                    <td>Thuộc nhóm</td>
                                    <td>
                                        <asp:DropDownList ID="CboMenuGroup" runat="server" CssClass="inputbox" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Loại</td>
                                    <td>
                                        <asp:DropDownList ID="CboType" runat="server" CssClass="inputbox" AutoPostBack="true" OnSelectedIndexChanged="CboType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Division</asp:ListItem>
                                            <asp:ListItem Value="1">Category</asp:ListItem>
                                            <asp:ListItem Value="2">Product Group</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>Link hiển thị</td>
                                    <td>
                                        <asp:TextBox ID="TxtLinkDisplay" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Hiển thị</td>
                                    <td>
                                        <asp:CheckBox ID="CKPublished" runat="server" /></td>

                                    <td style="width: 120px;">Số thứ tự</td>
                                    <td>
                                        <asp:TextBox ID="TxtMenuOrder" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Meta Keywords
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtMetaKeywords" runat="server" Width="500px" MaxLength="150"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>Hình đại diện </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:FileUpload ID="UplImagesURL" runat="server" />
                                        </div>
                                    </td>
                                    <td>Hình nhỏ</td>
                                    <td>
                                        <asp:FileUpload ID="UplImagesThumURL" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Image ID="imgFull" runat="server" Width="120px" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Image ID="imgThum" runat="server" Width="120px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="Label2" runat="server"></asp:Label>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2">
            <HeaderTemplate>
                Thiết lập thuộc tính
            </HeaderTemplate>
            <ContentTemplate>
                <div class="tabpage">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="background-color: #f5f5f5;">
                                <table>
                                    <tr>
                                        <td>Nhóm</td>
                                        <td>
                                            <asp:LinkButton ID="OpenPopupSearchFeature" runat="server" Text="Mã thuộc tính" />
                                            <asp:Panel ID="PanelSearchFeature" runat="server" Style="display: none; width: 700px; height: 500px;" CssClass="modalPopup">
                                                <div>
                                                    <asp:Panel ID="PanelMove" runat="server" Style="cursor: move; background-color: #DDDDDD; border: solid 1px Gray; color: Black; margin: 0px; padding: 7px;">
                                                        <div>Chọn thuộc tính:</div>
                                                    </asp:Panel>
                                                </div>
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanelListFeature" runat="server">
                                                        <ContentTemplate>
                                                            <div style="text-align: left; padding-top: 5px;">
                                                                Tìm theo tên
                                            <asp:TextBox ID="FindFeatureName" runat="server" Width="300px"></asp:TextBox>
                                                                <asp:Button ID="cmdLoadFeature" runat="server" Text="Find" CssClass="button" />
                                                                <asp:Button ID="OkButton" runat="server" Text="OK" CssClass="button" />
                                                                <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="button" />
                                                            </div>
                                                            <div>
                                                                <hr />
                                                            </div>
                                                            <div style="width: 700px; height: 520px; overflow: auto;">
                                                                <asp:GridView ID="grdFeature" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                                                    ForeColor="#333333" GridLines="None" Width="100%">
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Agency No">
                                                                            <ItemTemplate>
                                                                                <a href="#" onclick="choosefeature('<%#Eval("No_")%>','<%#Eval("Name")%>')"><%#Eval("No_")%></a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Name" HeaderText="Agency Name" />


                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </asp:Panel>
                                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                                                TargetControlID="OpenPopupSearchFeature" PopupControlID="PanelSearchFeature"
                                                CancelControlID="cmdCancel" PopupDragHandleControlID="PanelMove"
                                                OkControlID="OkButton" OnOkScript="onOk()" DynamicServicePath=""
                                                Enabled="True">
                                            </asp:ModalPopupExtender>
                                        </td>
                                        <td>Tên thuộc tính</td>
                                        <td>ĐVT</td>
                                        <td>Mô tả</td>
                                        <td>Số thứ tự</td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="CboFeatureGroup" runat="server" Width="170px" CssClass="inputbox">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFeatureNo" runat="server" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="TxtFeatureCode" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFeatureName" runat="server"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="TxtUnitOfMeasure" runat="server"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="TxtDescription" runat="server" Width="250px"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="TxtOrderPosition" runat="server" Width="40px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Dữ liệu mẫu</td>
                                        <td>Hiển thị danh sách</td>
                                        <td>Tìm kiếm</td>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="TxtOptionString" runat="server" Width="250px"></asp:TextBox></td>
                                        <td>
                                            <asp:CheckBox ID="CKShowInList" runat="server" /></td>
                                        <td>
                                            <asp:CheckBox ID="CKFilter" runat="server" /></td>
                                        <td colspan="2">
                                            <asp:HiddenField ID="TxtOldValues" runat="server" />
                                            <asp:Button ID="cmdNew" runat="server" Text="New" CssClass="button" />
                                            <asp:Button ID="cmdSaveFeatureCategory" runat="server" Text="Save" CssClass="button" />
                                            <asp:Label ID="LblWarning" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <div>
                                <asp:GridView ID="grdCategoryFeatureList" runat="server" BackColor="White"
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                    ForeColor="Black" GridLines="Vertical" Width="100%"
                                    AutoGenerateColumns="False">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="Feature Group No_" HeaderText="Mã nhóm" SortExpression="Feature Group No_" />
                                        <asp:BoundField DataField="Feature Group" HeaderText="Tên nhóm" SortExpression="Feature Group" />
                                        <asp:BoundField DataField="Feature No_" HeaderText="Mã thuộc tính" SortExpression="Feature No_" />
                                        <asp:BoundField DataField="Name" HeaderText="Tên thuộc tính" SortExpression="Name" />
                                        <asp:BoundField DataField="Description" HeaderText="Mô tả" SortExpression="Description" />
                                        <asp:BoundField DataField="Position Order" HeaderText="Số thứ tự" SortExpression="Position Order" />
                                        <asp:BoundField DataField="Option String" HeaderText="Định nghĩa dữ liệu" SortExpression="Option String" />
                                        <asp:CheckBoxField DataField="Show In List" HeaderText="Danh sách SP" />
                                        <asp:CheckBoxField DataField="Filter" HeaderText="Tìm kiếm" />
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel3">
            <HeaderTemplate>
                Lọc sản phẩm theo giá
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <div class="tabpage">
                            <div style="width: 1000px; margin: auto;">
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td>Diễn giải</td>
                                            <td>Giá từ</td>
                                            <td>Giá tới</td>
                                            <td>Thứ tự</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="TxtLineNo" runat="server" />
                                                <asp:TextBox ID="TxtPriceDescription" runat="server" Width="300"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtFromAmount" runat="server" Width="150"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtToAmount" runat="server" Width="150"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtOrder" runat="server" Width="150"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="CmdSaveSearchPrice" runat="server" Text="Lưu" CssClass="button" /></td>
                                        </tr>
                                    </table>

                                </div>
                                <div>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="3" GridLines="Vertical" Width="1000px">

                                        <AlternatingRowStyle BackColor="#DCDCDC" />
                                        <Columns>
                                            <asp:BoundField DataField="Line No_" HeaderText="Line" />
                                            <asp:BoundField DataField="Descriptions" HeaderText="Diễn giải" />
                                            <asp:BoundField DataField="From Amount" HeaderText="Giá từ" />
                                            <asp:BoundField DataField="To Amount" HeaderText="Giá tới" />
                                            <asp:BoundField DataField="Order Position" HeaderText="Số thứ tự" />
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:CommandField ShowDeleteButton="True" />
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#000065" />

                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <script>
                            $(document).ready(function () {
                                CopyValue();
                            });

                            function CopyValue() {

                                //VN
                                $("#<%=TxtName.ClientID%>").change(function () {
                                    var name = $("#<%=TxtName.ClientID%>").val();
                                    if (name != "") {
                                        $("#<%=TxtLinkDisplay.ClientID%>").val(RemoveUnicode(name));
                                    }

                                });

                            };
                        </script>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>


</asp:Content>

