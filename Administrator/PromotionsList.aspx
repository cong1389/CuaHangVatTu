<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="PromotionsList.aspx.vb" Inherits="Administrator_PromotionsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
 <div id="wrapper">
    <div style="background-color:#F1F3F5;">
        <table width="100%">
            <tr>
                <td style="width:132px;">Tình trạng</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                
                <td>
                    <asp:DropDownList ID="CboStatus" runat="server" Width="130px" CssClass="inputbox">
                        <asp:ListItem Value="0">Đã published</asp:ListItem>
                        <asp:ListItem Value="1">Chưa published</asp:ListItem>
                        <asp:ListItem Value="2">Tất cả</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
                    &nbsp;
                    <asp:Button ID="CmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
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
                <asp:GridView ID="GrPromotionsList" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    GridLines="Vertical" AutoGenerateColumns="False" 
                    Width="100%" AllowPaging="True" 
                    PageSize="20">
                        
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <itemtemplate>
                                <a href="PromotionCard.aspx?promotionno=<%#Eval("No_")%>"><%#Eval("Name")%></a>
                            </itemtemplate>
                        </asp:TemplateField>     
                        <asp:BoundField DataField="No_" HeaderText="No." SortExpression="No_" />    
                        <asp:BoundField DataField="Starting Date" HeaderText="Starting Date" SortExpression="Starting Date" />
                        <asp:BoundField DataField="Ending Date" HeaderText="Ending Date" SortExpression="Ending Date" />
                        <asp:BoundField DataField="Division Name" HeaderText="Division Name" SortExpression="Division Name" />
                        <asp:BoundField DataField="Category Name" HeaderText="Category Name" SortExpression="Category Name" />
                        <asp:BoundField DataField="Group Name" HeaderText="Group Name" SortExpression="Group Name" />
                        <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" />
                        <asp:BoundField DataField="Item No" HeaderText="Item No." SortExpression="Item No" />
                        <asp:CheckBoxField DataField="Published" HeaderText="Active" />
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

