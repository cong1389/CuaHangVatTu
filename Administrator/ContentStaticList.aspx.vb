Imports System.Data

Partial Class Administrator_ContentStaticList
    Inherits System.Web.UI.Page

    Protected Sub CmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddNew.Click
        Response.Redirect("StaticContentCard.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboContentGroup, adv.Business.List.GroupContent, " [Content Type] = 0 ", True, "Chọn nhóm")
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Dim SQL As String
        Dim sGroup As String
        Dim sWhere As String = ""
        sGroup = ReturnIfNull(CboContentGroup.SelectedValue, "").ToString.Trim

        If sGroup.Trim <> "" Then
            sWhere = " AND C.[Group No_] = '" & sGroup & "'"
        End If

        SQL = " SELECT C.Title, C.No_ Code, G.[Group Name], dbo.Char2Date([Date Create])[Date Create], C.[Link Menu], CONVERT(Bit, C.Published) Published" & _
                " FROM Content C INNER JOIN [Content Group] G ON C.[Group No_] = G.[Group No_] " & _
                " WHERE C.Type = 0 " & sWhere & " ORDER BY 1, 2 "
        Dim pData As DataTable
        pData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrContent.DataSource = pData
        GrContent.DataBind()
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        LoadData()
    End Sub

    Protected Sub GrContent_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrContent.RowDeleting
        Dim sNo As String
        sNo = GrContent.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String
        SQL = " DELETE FROM Content WHERE No_ = '" & sNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadData()
    End Sub
End Class