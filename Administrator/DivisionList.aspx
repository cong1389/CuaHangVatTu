<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="DivisionList.aspx.vb" Inherits="Administrator_DivisionList" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function ShowCard(sNo) {
        $get("maincontent_TxtOldNo_").value = sNo;
        $get("maincontent_cmdShowDivision").click();
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
                        <a href="#" onclick="ShowCard('<%#Eval("No_")%>')"><%# Eval("Name")%></a>
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
    </div>
       <asp:ModalPopupExtender ID="ModalPopupPanel" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="cmdShowModal"   PopupControlID="Panel1" OkControlID= "cmdClosePanel" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="popup">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <div style="display:none;">
            <asp:Button ID="cmdShowDivision" runat="server" Text="Edit" CssClass="button" />
            <asp:HiddenField ID="TxtOldNo_" runat="server" />
        </div>
        <div style="width:400px;height:300px;">
            <div class="popupheader" style="height:20px;">
                <b>Thương hiệu</b>
            </div>
            <div style="height:210px;padding-top:10px;padding-left:20px;">
               <div style="margin-top:30px;">Mã ngành hàng</div>
               <div>
                   
                    <asp:TextBox ID="TxtNo_" runat="server" Width="250"></asp:TextBox>
               </div>
               <div style="margin-top:30px;">
                    Tên ngành hàng
               </div>
               <div>
                    <asp:TextBox ID="TxtName" runat="server" Width="250"></asp:TextBox>
               </div>
               <div style="margin-top:30px;">
                    <asp:Label ID="Label2" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
               </div>
            </div>
            <div style="height:30px;text-align:right;padding-right:10px;">
                <asp:Button ID="cmdSave" runat="server" Text="Lưu" class="button" />
                <asp:Button ID="cmdClosePanel" runat="server" Text="Đóng" class="button" />
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

