Imports System.Data

Partial Class Administrator_FaqsList
    Inherits System.Web.UI.Page
    Const PageKey As String = "021"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Dim SQL As String
        SQL = " SELECT [Line No_], Question, Link, [Order Position], CONVERT(bit, Published) FROM FAQs ORDER BY 1"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdList.DataSource = tbl
        grdList.DataBind()
    End Sub

    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        Response.Redirect("FaqsCard.aspx")
    End Sub

    Protected Sub grdList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdList.PageIndexChanging
        grdList.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub grdList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdList.RowDeleting
        Dim nLineNo As Integer
        nLineNo = CInt(grdList.Rows(e.RowIndex).Cells.Item(1).Text())
        Dim SQL As String
        SQL = " DELETE FROM FAQs WHERE [Line No_] = " & nLineNo
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadData()
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        LoadData()
    End Sub
End Class
