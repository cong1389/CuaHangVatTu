Imports System.Data

Partial Class Administrator_BannerList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboBannerGroup, adv.Business.List.Modules, , True, "Chọn nhóm")
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Dim SQL As String

        SQL = "SELECT B.No_, B.Name, [Position No_], M.Name [Group], [Images Src], dbo.Char2Date(B.[Starting Date]) [Starting Date], dbo.Char2Date(B.[Ending Date])[Ending Date], " & _
                " B.Url, B.[Num Click], CONVERT(bit,Show) Show " & _
                " FROM Banner B INNER JOIN Modules M ON B.[Banner Group No_] = M.No_ WHERE 1 = 1 "
        If ReturnIfNull(CboBannerGroup.SelectedValue, "").ToString.Trim <> "" Then
            SQL &= " AND B.[Banner Group No_] = '" & ReturnIfNull(CboBannerGroup.SelectedValue, "").ToString.Trim & "'"
        End If
        Dim tblDT As New DataTable
        tblDT = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrBannerList.DataSource = tblDT
        GrBannerList.DataBind()
    End Sub

    Protected Sub GrBannerList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrBannerList.PageIndexChanging
        GrBannerList.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        LoadData()
    End Sub

    Protected Sub GrBannerList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrBannerList.RowDeleting
        Dim sBannerNo As String
        sBannerNo = GrBannerList.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String
        SQL = " DELETE FROM Banner WHERE No_ = '" & sBannerNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadData()
    End Sub

    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        Response.Redirect("BannerCard.aspx")
    End Sub
End Class
