<%@ Page Title="" Language="VB" MasterPageFile="~/Administrator/MasterPage.master" AutoEventWireup="false" CodeFile="MsgContentList.aspx.vb" Inherits="Administrator_MsgContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" Runat="Server">
<div style="background:#dddddd; padding:5px;">
    <asp:Button ID="cmdLoad" runat="server" Text="Load" CssClass="button" />
    <asp:Button ID="cmdAddNew" runat="server" Text="Thêm mới" CssClass="button" />
</div>
<div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdLoad" />
            </Triggers>
               
            <ContentTemplate>
                <asp:GridView ID="GrMsgContentList" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    GridLines="Vertical" AutoGenerateColumns="False" 
                    Width="100%" AllowPaging="True" 
                    PageSize="20">
                        
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <itemtemplate>
                                <a href="msgcontentcard.aspx?contentno=<%#Eval("No_")%>"><%#Eval("Name")%></a>
                            </itemtemplate>
                        </asp:TemplateField>     
                        <asp:BoundField DataField="No_" HeaderText="No_" SortExpression="No_" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                       <asp:BoundField DataField="Link URL" HeaderText="Link" SortExpression="Link URL" />
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
</asp:Content>

