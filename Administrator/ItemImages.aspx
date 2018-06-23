<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="ItemImages.aspx.vb" Inherits="Administrator_ItemImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
 <div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
    <div class="toolbox">
        <asp:Button ID="cmdUploadImg" runat="server" Text="Save" CssClass="button"/>
        <asp:Button ID="cmdDelImg" runat="server" Text="Delete" CssClass="button"/>
        <asp:Label ID="LblWarningUploadImg" runat="server" Text=""></asp:Label>
    </div>
                            
    <div style="padding-left:10px;">
        <asp:HiddenField ID="TxtNo_" runat="server" />
        <asp:FileUpload ID="UplImagesDetail" runat="server" Width="300px" CssClass="inputbox" Height="20px" />
        <asp:FileUpload ID="FileUpload1" runat="server" />

    </div>
    <div>
        <asp:Label ID="LblImg" runat="server" Text=""></asp:Label>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>                
</div>    
</asp:Content>

