<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="UserList.aspx.vb" Inherits="Administrator_Default2" %>

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
                    <asp:DropDownList ID="CboBannerGroup" runat="server"  Width="200px" CssClass="inputbox">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="CboStatus" runat="server" Width="130px" CssClass="inputbox">
                        <asp:ListItem Value="0">Đã active</asp:ListItem>
                        <asp:ListItem Value="1">Chưa active</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
                    <asp:Button ID="cmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
                    <asp:Button ID="cmdAssignRightForGroup" runat="server" Text="Quyền nhóm" CssClass="button" />

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
                <asp:GridView ID="GrUserList" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    GridLines="Vertical" AutoGenerateColumns="False" 
                    Width="100%" AllowPaging="True" 
                    PageSize="20">
                        
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <itemtemplate>
                                <a href="UserCard.aspx?username=<%#Eval("Username")%>"><%#Eval("FullName")%></a>
                            </itemtemplate>
                        </asp:TemplateField>     
                        <asp:BoundField DataField="Username" HeaderText="User Name" SortExpression="Username" />
                        <asp:BoundField DataField="Group Name" HeaderText="Group Name" SortExpression="Group Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Last Logon" HeaderText="Last Logon" SortExpression="Last Logon" />
                        <asp:CheckBoxField DataField="Active" HeaderText="Active" />
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

