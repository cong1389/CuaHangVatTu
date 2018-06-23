Imports System.Data

Partial Class Administrator_PromotionsList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Dim SQL As String
        SQL = "SELECT P.No_, P.Name, dbo.Char2Date(P.[Starting Date]) [Starting Date], dbo.Char2Date(P.[Ending Date]) [Ending Date]," & _
                " D.Name [Division Name], C.Name [Category Name], G.Name [Group Name], P.[Brand No_] Brand, P.[Item No_] [Item No]," & _
                " CONVERT(bit, P.Published) Published " & _
                " FROM Promotions P " & _
                " LEFT JOIN [Division] D ON P.[Division No_] = D.No_  " & _
                " LEFT JOIN [Item Category] C ON P.[Category No_] = C.No_  " & _
                " LEFT JOIN [Product Group] G ON P.[Product Group No_] = G.No_ "
        Dim tblDT As New DataTable
        tblDT = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrPromotionsList.DataSource = tblDT
        GrPromotionsList.DataBind()
    End Sub

    Protected Sub GrPromotionsList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrPromotionsList.PageIndexChanging
        GrPromotionsList.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub CmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddNew.Click
        Response.Redirect("PromotionCard.aspx")
    End Sub

    Protected Sub GrPromotionsList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrPromotionsList.RowDeleting
        Dim sPromotionNo As String
        sPromotionNo = GrPromotionsList.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String
        SQL = " DELETE FROM Promotions WHERE No_ = '" & sPromotionNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadData()
    End Sub
End Class
