<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="BannerList.aspx.vb" Inherits="Administrator_BannerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
 <div id="wrapper">
    <div style="background-color:#F1F3F5;">
        <table width="100%">
            <tr>
                <td style="width:202px;">Nhóm banner</td>
                <td style="width:132px;">Tình trạng</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="CboBannerGroup" runat="server"  Width="200px" CssClass="inputbox"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="CboStatus" runat="server" Width="130px" CssClass="inputbox">
                        <asp:ListItem Value="0">Chọn tình trạng</asp:ListItem>
                        <asp:ListItem Value="1">Đã published</asp:ListItem>
                        <asp:ListItem Value="2">Chưa published</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
                    <asp:Button ID="cmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <p>Loading</p>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdLoad" />
            </Triggers>
               
            <ContentTemplate>
                <asp:GridView ID="GrBannerList" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    GridLines="Vertical" AutoGenerateColumns="False" 
                    Width="100%" AllowPaging="True" 
                    PageSize="20">
                        
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <itemtemplate>
                                <a href="BannerCard.aspx?bannerno=<%#Eval("No_")%>"><%#Eval("Name")%></a>
                            </itemtemplate>
                        </asp:TemplateField>     
                        <asp:BoundField DataField="No_" HeaderText="Banner no." SortExpression="No_" />    
                        <asp:BoundField DataField="Group" HeaderText="Group" SortExpression="Group" />
                        <asp:BoundField DataField="Images Src" HeaderText="Image" SortExpression="Image" />
                        <asp:BoundField DataField="Starting Date" HeaderText="Starting Date" SortExpression="Starting Date" />
                        <asp:BoundField DataField="Ending Date" HeaderText="Ending Date" SortExpression="Ending Date" />
                        <asp:BoundField DataField="Url" HeaderText="Url" SortExpression="Url" />
                        <asp:BoundField DataField="Num Click" HeaderText="Click" SortExpression="Click" />
                        <asp:CheckBoxField DataField="Show" HeaderText="Show" />
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
            </ContentTemplate>
        </asp:UpdatePanel> 
    </div>
</div>
</asp:Content>

