Imports System.Data

Partial Class Administrator_linklist
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitCombo()
            If Not Request.Cookies("modulespostion") Is Nothing Then CboPosition.SelectedValue = ReturnIfNull(Request.Cookies("modulespostion").Value, "").ToString
            ShowList()
        End If
    End Sub

    Sub ShowList()
        Dim SQL As String, sWhere As String = ""
        If CboPosition.SelectedValue <> "" Then
            sWhere = " WHERE L.[Position No_] = '" & CboPosition.SelectedValue & "'"
        End If
        SQL = " SELECT L.No_, L.Name, M.Name Postition, L.[URL Link], L.[Class CSS], L.[Order Position], L.[Is Group] " & _
                " FROM [Link Menu] L LEFT JOIN Modules M ON L.[Position No_] = M.No_ AND M.[Is Banner] = 0 " & sWhere & " ORDER BY 1, 3, 6"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdLinkList.DataSource = tbl
        grdLinkList.DataBind()
    End Sub

    Sub InitCombo()
        Dim SQL As String
        SQL = " SELECT No_, Name FROM Modules WHERE [Is Banner] = 0 "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        tbl.Rows.InsertAt(tbl.NewRow, 0)
        tbl.Rows(0).Item(0) = ""
        tbl.Rows(0).Item(1) = ""
        CboPosition.DataSource = tbl
        CboPosition.DataTextField = "Name"
        CboPosition.DataValueField = "No_"
        CboPosition.DataBind()

    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        Response.Cookies("modulespostion").Value = CboPosition.SelectedValue
        Response.Cookies("modulespostion").Expires = Now.AddHours(8)
        ShowList()
    End Sub

    Protected Sub grdLinkList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLinkList.PageIndexChanging
        grdLinkList.PageIndex = e.NewPageIndex
        ShowList()
    End Sub

    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        Response.Redirect("LinkCard.aspx")
    End Sub

    Protected Sub grdLinkList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdLinkList.RowDeleting
        Dim sLinkNo As String
        sLinkNo = grdLinkList.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String
        SQL = "delete from [Link Menu] where No_ = '" & sLinkNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        ShowList()
    End Sub
End Class
