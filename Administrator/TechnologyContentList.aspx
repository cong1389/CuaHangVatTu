<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="TechnologyContentList.aspx.vb" Inherits="Administrator_TechnologyContentList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
 <div id="wrapper">
    <div style="background-color:#F1F3F5;">
        <table width="100%">
            <tr>
                <td style="width:132px;">Loại nội dung</td>
                <td style="width:132px;">Nhóm nội dung</td>
                <td style="width:132px;">Tình trạng</td>
                <td style="width:132px;">Mã sản phẩm</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td >
                    <asp:DropDownList ID="CboContentType" runat="server"  Width="150px" CssClass="inputbox">
                    </asp:DropDownList>
                </td>
                <td >
                    <asp:DropDownList ID="CboContentGroup" runat="server"  Width="150px" CssClass="inputbox">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="CboStatus" runat="server" Width="130px" CssClass="inputbox">
                        <asp:ListItem Value="0">Đã published</asp:ListItem>
                        <asp:ListItem Value="1">Chưa published</asp:ListItem>
                        <asp:ListItem Value="2">Tất cả</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="TxtItemNo" runat="server"></asp:TextBox>
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

                <asp:GridView ID="GrContent" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    GridLines="Vertical" AutoGenerateColumns="False" 
                    Width="100%" AllowPaging="True" 
                    PageSize="20">
                        
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Item name">
                            <itemtemplate>
                                <a href="TechnologyContentCard.aspx?ContentNo=<%#Eval("No_")%>"><%#Eval("Title")%></a>
                            </itemtemplate>
                        </asp:TemplateField>     
                        <asp:BoundField DataField="No_" HeaderText="Code" SortExpression="Code" />    
                        <asp:BoundField DataField="Group Name" HeaderText="Nhóm" 
                            SortExpression="Group Name" />
                        <asp:BoundField DataField="Date Create" HeaderText="Ngày tạo" 
                            SortExpression="Date Create" />
                        <asp:BoundField DataField="Link Menu" HeaderText="Link" 
                            SortExpression="Link Menu" />
                        <asp:CheckBoxField DataField="Published" HeaderText="Show/Hide" />
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
</asp:Content>

