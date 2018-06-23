Imports System.Data

Partial Class Administrator_PromotionsContentList
    Inherits System.Web.UI.Page
    Dim sContentNo As String

    Protected Sub CmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddNew.Click
        Response.Redirect("PromotionContentCard.aspx")
    End Sub

    Sub LoadData()
        Dim SQL As String = ""
        SQL = " SELECT No_, dbo.Char2Date([Starting Date]) [Starting Date], dbo.Char2Date([Ending Date]) [Ending Date]," & _
                " Title, [Link Content], CONVERT(bit, Published) Published " & _
                " FROM [Promotions Content]"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrContentList.DataSource = tbl
        GrContentList.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
        End If
    End Sub

    Protected Sub GrContentList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrContentList.RowDeleting
        Dim sContentNo As String
        sContentNo = GrContentList.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String
        SQL = " DELETE FROM [Promotions Content] WHERE No_ = '" & sContentNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadData()
    End Sub
End Class
