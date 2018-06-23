Imports System.Data

Partial Class Administrator_CategoryList
    Inherits System.Web.UI.Page
    Dim objCat As New adv.Business.ItemCategory

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboDivision, adv.Business.List.Division, "", True, "Chọn ngành hàng")
            BuildCombo(CboFilterDivision, adv.Business.List.Division, "", True, "Lọc theo ngành hàng")
            BuildCombo(CboMenu, adv.Business.List.MenuGroup, "", True, "Chọn menu")
            ShowList()
        End If
    End Sub

    Sub ShowList()
        Dim SQL As String
        SQL = "SELECT C.No_, C.Name, D.Name Division, D.No_ [Division No] FROM [Item Category] C INNER JOIN Division D ON C.[Division No_] = D.No_  WHERE 1 = 1 "
        If ReturnIfNull(CboFilterDivision.SelectedValue, "").ToString.Trim <> "" Then
            SQL &= " AND C.[Division No_] = '" & CboFilterDivision.SelectedValue & "'"
        End If
        If TxtSearch.Text.Trim <> "" Then
            SQL &= " AND ( C.Name Like '%" & TxtSearch.Text & "%' OR C.No_  Like '%" & TxtSearch.Text & "%' )"
        End If
        SQL &= " ORDER BY D.No_ "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdMenuGroup.DataSource = tbl
        grdMenuGroup.DataBind()
    End Sub


    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        TxtOldNo_.Value = ""
        TxtNo_.Focus()
        ModalPopupPanel.Show()
    End Sub

    Sub ShowData(ByVal sNo_ As String)
        objCat.Load(sNo_)
        CboDivision.SelectedValue = objCat.DivisionNo_
        TxtNo_.Text = objCat.No_
        TxtOldNo_.Value = objCat.No_
        TxtName.Text = objCat.Name
        CboMenu.SelectedValue = objCat.MenuGroupNo_
    End Sub

    Protected Sub cmdShowDivision_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowDivision.Click
        ShowData(TxtOldNo_.Value)
        TxtNo_.Focus()
        ModalPopupPanel.Show()
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CboDivision.SelectedValue.Trim = "" Then
            Label2.Text = "Bạn phải chọn ngành hàng"
            Exit Sub
        End If
        If TxtNo_.Text.Trim = "" Then
            Label2.Text = "Bạn phải nhập mã"
            Exit Sub
        End If
        If TxtName.Text.Trim = "" Then
            Label2.Text = "Bạn phải nhập tên"
            Exit Sub
        End If
        If TxtOldNo_.Value = "" Or TxtOldNo_.Value.Trim <> TxtNo_.Text.Trim Then
            If objCat.Load(TxtNo_.Text) Then
                Label2.Text = "Mã đã tồn tại"
                Exit Sub
            End If
        End If
        With objCat
            .DivisionNo_ = CboDivision.SelectedValue
            .No_ = TxtNo_.Text
            .Name = TxtName.Text
            .MenuGroupNo_ = CboMenu.SelectedValue
            .Save(TxtOldNo_.Value)
        End With
        ShowList()
        ModalPopupPanel.Hide()
    End Sub

    Protected Sub grdMenuGroup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMenuGroup.PageIndexChanging
        grdMenuGroup.PageIndex = e.NewPageIndex
        ShowList()
    End Sub

    Protected Sub grdMenuGroup_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMenuGroup.RowDeleting
        Dim sNo As String
        sNo = grdMenuGroup.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String, sValue As String = ""

        SQL = " SELECT TOP 1 No_ FROM [Product Group] WHERE [Category No_] = '" & sNo & "'"
        sValue = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        If sValue.Trim <> "" Then
            Dim Script As String
            Script = "alert('Mã loại hàng này đã được sử dụng. Bạn không thể xóa')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallWarning", Script, True)
            Exit Sub
        End If
        SQL = " DELETE FROM [Item Category] WHERE No_ = '" & sNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        ShowList()
    End Sub

    Protected Sub cmdCloseBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClosePanel.Click
        ModalPopupPanel.Hide()
    End Sub


    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        ShowList()
    End Sub
End Class
