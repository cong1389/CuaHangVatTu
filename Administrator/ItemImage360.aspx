<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="ItemImage360.aspx.vb" Inherits="Administrator_ItemImage360" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function changecheck(obj) {
        if (obj == 0) {
            $get("OptImage").checked = !$get("OptFlash").checked   
        }
    }
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<div class="toolbox">
    <div style="padding:6px;">
        <asp:HiddenField ID="TxtItemNo" runat="server" />
        Sử dụng file flash<asp:RadioButton ID="OptFlash" runat="server" Checked="true" OnCheckedChanged="OptFlash_CheckedChanged" AutoPostBack="true"/>
        Sử dụng file hình<asp:RadioButton ID="OptImage" runat="server" OnCheckedChanged="OptImage_CheckedChanged" AutoPostBack="true" />
        <asp:FileUpload ID="FileUpl" runat="server" Width="300"/> (Dùng cho sản phẩm có sẵn file flash)
        <asp:Button ID="cmdUpload" runat="server" Text="Upload"  CssClass="button"/>
    </div>
</div>
<div>
    <asp:Label ID="LblImg" runat="server" Text=""></asp:Label>
</div>
</asp:Content>

