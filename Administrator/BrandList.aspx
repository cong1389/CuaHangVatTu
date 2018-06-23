<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="BrandList.aspx.vb" Inherits="Administrator_BrandList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function ShowCart(sBrandNo) {
        $get("maincontent_BrandNo").value = sBrandNo;
        $get("maincontent_cmdShowBrand").click();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  <div style="padding:10px;">
    <asp:Button ID="cmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
  </div>
  <div style="margin: 0px auto 0px auto;">
        <asp:GridView ID="grdMenuGroup" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            AutoGenerateColumns="False" Width="100%"  AllowPaging="True" PageSize="30" 
            GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="Diễn giải">
                    <itemtemplate>
                        <a href="#" onclick="ShowCart('<%#Eval("No_")%>')"><%# Eval("Name")%></a>
                    </itemtemplate>
                    <ControlStyle />
                </asp:TemplateField>     
                <asp:BoundField DataField="No_" HeaderText="Mã" SortExpression="No_" />
                <asp:CommandField DeleteText="[Xóa]" ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle ForeColor="Black" BackColor="#EEEEEE" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
    </div>
    <div style="display:none;">
        <asp:Button ID="cmdShowModal" runat="server" Text="Thêm mới" CssClass="button" />
        <asp:Button ID="cmdShowBrand" runat="server" Text="Edit" CssClass="button" />
        <asp:HiddenField ID="BrandNo" runat="server" />
    </div>
       <asp:ModalPopupExtender ID="ModalPopupBrand" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="cmdShowModal"   PopupControlID="Panel1" OkControlID= "cmdCloseBrand" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="popup">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <div style="display:none;"><asp:Button ID="cmdGetVariants" runat="server" Text="OK" /></div>
        <div style="width:400px;height:300px;">
            <div class="popupheader" style="height:20px;">
                <b>Thương hiệu</b>
            </div>
            <div style="height:220px;padding-top:10px;padding-left:20px;">
               <div style="margin-top:10px;">Mã thương hiệu</div>
               <div>
                    <asp:TextBox ID="TxtNo_" runat="server" Width="250"></asp:TextBox>
               </div>
               <div style="margin-top:10px;">
                    Tên thương hiệu
               </div>
               <div>
                    <asp:TextBox ID="TxtName" runat="server" Width="250"></asp:TextBox>
               </div>
               <div style="margin-top:10px;">
                    Logo
                   <asp:FileUpload ID="UplBrandLogo" runat="server" />
                   <asp:Button ID="CmdUpload" runat="server" Text="Upload" CssClass="button" />
                   <asp:Button ID="CmdProcessData" runat="server" Text="Upload" CssClass="button" Visible="False" />
                   <asp:HiddenField ID="TxtImgUrl" runat="server" />
               </div>
               <div>
                    <asp:Image ID="ImgBanner" runat="server" style="max-height:120px;"/>
               </div>
               <div style="margin-top:30px;">
                    <asp:Label ID="Label2" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
               </div>
            </div>
            <div style="height:30px;text-align:right;padding-right:10px;">
                <asp:Button ID="cmdSave" runat="server" Text="Lưu" class="button" />
                <asp:Button ID="cmdCloseBrand" runat="server" Text="Đóng" class="button" />
            </div>
        </div>
        </ContentTemplate>
          <Triggers>
                <asp:PostBackTrigger ControlID="CmdUpload"  />
                <asp:AsyncPostBackTrigger ControlID="CmdProcessData" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

