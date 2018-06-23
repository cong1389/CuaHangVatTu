
Imports System.Data

Partial Class AllCategory
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadAllCategory()
    End Sub
    Private Sub LoadAllCategory()
        Dim tbl As DataTable = GettblMenu(0)
        rptAllcategory.DataSource = tbl
        rptAllcategory.DataBind()

    End Sub

    Public Function GettblMenu(ByVal sLevel As Integer, Optional ByVal sConditions As String = "") As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        If sConditions.Trim <> "" Then sWhere = " AND " & sConditions
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = " & sLevel & " AND Published = 1 AND [Using For] = 0 " & sWhere & " ORDER BY [Menu Order] "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function


    Protected Sub rptAllcategory_OnItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            Dim hpLinkParent As HyperLink = e.Item.FindControl("hpLinkParent")
            hpLinkParent.Text = drv.Row("Name")
            Dim linkUrl As String = GetUrl() & "sub-category/" & drv.Row("Link Display") & "/"
            hpLinkParent.NavigateUrl = linkUrl
            Dim rptSubcategory As Repeater = e.Item.FindControl("rptSubcategory")
            Dim tblSubcat As DataTable = GettblMenu(1, " [Parent No_] = '" & drv.Row("No_") & "'")
            rptSubcategory.DataSource = tblSubcat
            rptSubcategory.DataBind()
        End If
    End Sub

    Protected Sub rptSubcategory_OnItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            Dim hpLinkSubCat As HyperLink = e.Item.FindControl("hpLinkSubCat")
            hpLinkSubCat.NavigateUrl = GetUrl() & "sub-products-list/" & drv.Row("Link Display") & "/"
            hpLinkSubCat.Text = drv.Row("Name")
        End If
    End Sub
End Class
