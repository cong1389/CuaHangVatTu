<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="MainternanceAddressCard.aspx.vb" Inherits="Administrator_ManternanceAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
    <div class="toolbox">
    <table>
        <tr>
            <td><asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="button" />
            &nbsp;
            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="button" /></td>
            <td><b><asp:Label ID="LblWarning" runat="server" Text=""></asp:Label></b></td>
        </tr>
    </table>
</div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
            Width="100%" CssClass="MyTabStyle">
            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                <HeaderTemplate>
                    Nội dung
                </HeaderTemplate>
                <ContentTemplate>
                <div class="tab_body" style="padding:20px;">
                    <table cellspacing="1" style="background:#F1F3F5;">
                        <tr>
                            <td class="title" style="height:40px;">Diễn giải</td>
                            <td >
                                <asp:HiddenField ID="TxtNo" runat="server" />
                                <asp:TextBox ID="TxtDescription" runat="server" Width="300px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" >
                                 <FCKeditorV2:FCKeditor ID="TxtContent" runat="server" BasePath="~/administrator/fckeditor/" Height="500px" AutoDetectLanguage="false" DefaultLanguage="en" Width="1010">
                                  </FCKeditorV2:FCKeditor>                     
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;</td>
                        </tr>
                    </table>
                </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>
                    Nhóm sản phẩm
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                        <div class="tab_body">
                            <div style="padding:10px;">
                                Loại sản phẩm
                                <asp:DropDownList ID="CboMenuGroup" runat="server"  Width="200px" CssClass="inputbox">
                                        </asp:DropDownList>
                                <asp:Button ID="cmdSaveMainternanceAddByMenu" runat="server" Text="Save" CssClass="button" />
                                <asp:Label ID="LblWarningAddByMenu" runat="server" Text="" ForeColor="#FF3300"></asp:Label>
                            </div>
                            <asp:GridView ID="GrList" runat="server" BackColor="White" 
                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                GridLines="Vertical" AutoGenerateColumns="False" 
                                Width="100%" AllowPaging="True" 
                                PageSize="20">
                        
                                <AlternatingRowStyle BackColor="Gainsboro" />
                                <Columns>
                                    <asp:BoundField DataField="No_" HeaderText="No" SortExpression="MenuGroup" />
                                    <asp:BoundField DataField="MenuGroup" HeaderText="Menu" SortExpression="MenuGroup" />
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
           
        </asp:TabContainer>
    <div style="width:1010px; margin: 0px auto 0px auto;">
    
    </div>
</asp:Content>

