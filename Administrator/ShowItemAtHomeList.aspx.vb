Imports System.Data

Partial Class Administrator_ShowItemAtHomeList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Dim SQL As String
        SQL = " SELECT No_, Title, [Get Type] = CASE [List Type] WHEN 1 THEN N'Sản phẩm khuyến mãi' WHEN 2 THEN N'Sản phẩm hot' WHEN 3 THEN N'Sản phẩm mới' " & _
                " WHEN 4 THEN N'Danh sách theo menu' WHEN 5 THEN N'Danh sách tùy chọn' WHEN 6 THEN N'Sản phẩm bán chạy' END, " & _
                " dbo.Char2Date([Starting Date]) [Starting Date], dbo.Char2Date([Ending Date]) [Ending Date], [Url Page] " & _
                " FROM [Show List Header] WHERE 1 = 1 "
        If CboType.SelectedValue <> "0" Then
            SQL &= " AND [List Type] = " & CboType.SelectedValue
        End If
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdShowList.DataSource = tbl
        grdShowList.DataBind()
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        LoadData()
    End Sub

    Protected Sub CmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddNew.Click
        Response.Redirect("ShowItemAtHomeCard.aspx")
    End Sub

    Protected Sub grdShowList_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdShowList.RowDeleting
        Dim sLinkNo As String
        sLinkNo = grdShowList.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String
        SQL = "delete from [Show List Header] where No_ = '" & sLinkNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)

        SQL = "delete from [Show List Line] WHERE [Document No_] = '" & sLinkNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadData()
    End Sub
End Class
