Imports System.Data

Partial Class Administrator_Unit
    Inherits System.Web.UI.Page
    Dim objUnit As New adv.Business.UnitOfMeasure

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowList()
        End If
    End Sub

    Sub ShowList()
        Dim SQL As String
        SQL = "SELECT No_, Name FROM [Unit Of Measure]"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdMenuGroup.DataSource = tbl
        grdMenuGroup.DataBind()
    End Sub


    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        TxtOldNo_.Value = ""
        TxtNo_.Focus()
        ModalPopupBrand.Show()
    End Sub

    Sub ShowData(ByVal sNo_ As String)
        objUnit.Load(sNo_)
        TxtNo_.Text = objUnit.No_
        TxtName.Text = objUnit.Name
    End Sub

    Protected Sub cmdShowBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowBrand.Click
        ShowData(TxtOldNo_.Value)
        TxtNo_.Focus()
        ModalPopupBrand.Show()
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If TxtNo_.Text.Trim = "" Then
            Label2.Text = "Bạn phải nhập mã"
            Exit Sub
        End If
        If TxtName.Text.Trim = "" Then
            Label2.Text = "Bạn phải nhập tên"
            Exit Sub
        End If
        If TxtOldNo_.Value = "" Or TxtOldNo_.Value.Trim <> TxtNo_.Text.Trim Then
            If objUnit.Load(TxtNo_.Text) Then
                Label2.Text = "Mã đã tồn tại"
                Exit Sub
            End If
        End If
        With objUnit
            .No_ = TxtNo_.Text
            .Name = TxtName.Text
            .Save(TxtOldNo_.Value)
        End With
        ShowList()
        ModalPopupBrand.Hide()
    End Sub

    Protected Sub grdMenuGroup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMenuGroup.PageIndexChanging
        grdMenuGroup.PageIndex = e.NewPageIndex
        ShowList()
    End Sub

    Protected Sub grdMenuGroup_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMenuGroup.RowDeleting
        Dim sNo As String
        sNo = grdMenuGroup.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String
        SQL = " DELETE FROM [Unit Of Measure] WHERE No_ = '" & sNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        ShowList()
    End Sub

    Protected Sub cmdCloseBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseBrand.Click
        ModalPopupBrand.Hide()
    End Sub
End Class
