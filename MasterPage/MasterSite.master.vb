Imports System.Data

Partial Class MasterPage_MasterSite
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim catListId As String = ""
        Dim tbl As DataTable
        tbl = GettblMenu(0)
        For nIJ = 0 To tbl.Rows.Count - 1
            If nIJ = 0 Then
                catListId &= tbl.Rows(nIJ).Item("No_")
            Else
                catListId &= "," & tbl.Rows(nIJ).Item("No_")
            End If
        Next
        hdCategoryidlist.Value = catListId
        ShowLinkFooter()
        ltrMetadata.Text = String.Format("<meta name='keywords' content='{0}'>", ConfigurationManager.AppSettings("keywork"))

        GetFooter()

    End Sub

    Public Function GettblMenu(ByVal sLevel As Integer, Optional ByVal sConditions As String = "") As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        If sConditions.Trim <> "" Then sWhere = " AND " & sConditions
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = " & sLevel & " AND Published = 1 AND [Using For] = 0 " & sWhere & " ORDER BY [Menu Order] "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Sub ShowLinkFooter()
        'Dim SQL As String = ""
        'Dim sHtml As String = ""
        'SQL = "SELECT * FROM [Link Menu] WHERE [Position No_] = '20' AND [Is Group] = 1 ORDER BY [Order Position] "
        'Dim tbl As DataTable
        'tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        'rptGroupFooter.DataSource = tbl
        'rptGroupFooter.DataBind()

    End Sub

    Protected Sub rptGroupFooter_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            Dim ltrName As Literal = e.Item.FindControl("ltrName")
            ltrName.Text = drv.Item("Name")
            Dim rptSub As Repeater = e.Item.FindControl("rptSubMenu")
            Dim SQL As String = ""
            Dim sHtml As String = ""
            SQL = "SELECT * FROM [Link Menu] WHERE [Position No_] = '20' AND [Is Group] = 0 AND [Parent No_] = '" & drv.Item("No_") & "' ORDER BY [Order Position] "
            Dim tbl As DataTable
            tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
            rptSub.DataSource = tbl
            rptSub.DataBind()
        End If
    End Sub

    Protected Sub rptSubMenu_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            Dim lnkUrl As HyperLink = e.Item.FindControl("lnkUrl")
            Dim link As String = GetUrl() & "static-detail/" & drv.Item("URL Link") & "/"
            lnkUrl.NavigateUrl = link
            lnkUrl.Text = drv.Item("Name")
        End If
    End Sub

    Private Sub GetFooter()
        Dim sNo As String = ""

        Dim sSQL As String = ""
        sSQL = " SELECT * FROM [Content] WHERE [Link Menu] = 'footer'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        If tbl.Rows().Count > 0 Then
            'ltrHotlineContent.Text = tbl.Rows(0).Item("Content")
            'ltrHotlineHeader.Text = tbl.Rows(0).Item("Title")
            'Dim text As String = ""
            'text = text & "<div class='pull-left'>"
            'text = text & tbl.Rows(0).Item("Title") & ":</div> "
            'text = text & "<div style='color: Red; font-weight: bold' > "
            'text = text & tbl.Rows(0).Item("Content") & "</div>"


            ltrFooter.Text = tbl.Rows(0).Item("Content")
        End If

    End Sub

End Class

