Imports System.Data

Partial Class Administrator_ProductGroupList
    Inherits System.Web.UI.Page
    Dim objG As New adv.Business.ProductGroup

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
        SQL = "SELECT G.No_, G.Name, C.Name Category, D.Name Division, D.No_ [Division No] " & _
            " FROM [Product Group] G LEFT JOIN [Item Category] C ON RTRIM(G.[Category No_]) = RTRIM(C.No_) " & _
            " LEFT JOIN Division D ON RTRIM(G.[Division No_]) = RTRIM(D.No_)  WHERE 1 = 1 "
        If ReturnIfNull(CboFilterDivision.SelectedValue, "").ToString.Trim <> "" Then
            SQL &= " AND RTRIM(G.[Division No_]) = '" & CboFilterDivision.SelectedValue.ToString.Trim & "'"
        End If
        If ReturnIfNull(CbofilterCategory.SelectedValue, "").ToString.Trim <> "" Then
            SQL &= " AND RTRIM(G.[Category No_]) = '" & CbofilterCategory.SelectedValue.ToString.Trim & "'"
        End If
        If TxtSearch.Text.Trim <> "" Then
            SQL &= " AND ( G.Name Like '%" & TxtSearch.Text & "%' OR G.No_  Like '%" & TxtSearch.Text & "%' )"
        End If
        SQL &= " ORDER BY D.No_ "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdMenuGroup.DataSource = tbl
        grdMenuGroup.DataBind()
    End Sub

    Protected Sub CboFilterDivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboFilterDivision.SelectedIndexChanged
        If ReturnIfNull(CboFilterDivision.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim = "" Then Exit Sub
        BuildCombo(CbofilterCategory, adv.Business.List.ItemCategory, " [Division No_] = '" & CboFilterDivision.SelectedValue.Trim & "'", True, "Lọc theo loại hàng")
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        ShowList()
    End Sub

    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        TxtOldNo_.Value = ""
        CboDivision.Focus()
        ModalPopupPanel.Show()
    End Sub

    Protected Sub grdMenuGroup_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMenuGroup.RowDeleting
        Dim sNo As String
        sNo = grdMenuGroup.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String, sValue As String = ""

        SQL = " SELECT TOP 1 No_ FROM Item WHERE [Produc Group No_] = '" & sNo & "'"
        sValue = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        If sValue.Trim <> "" Then
            Dim Script As String
            Script = "alert('Mã nhóm hàng này đã được sử dụng. Bạn không thể xóa')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallWarning", Script, True)
            Exit Sub
        End If
        SQL = " DELETE FROM [Product Group] WHERE No_ = '" & sNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        ShowList()
    End Sub

    Protected Sub CboDivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboDivision.SelectedIndexChanged
        If ReturnIfNull(CboDivision.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim = "" Then Exit Sub
        BuildCombo(CboCategory, adv.Business.List.ItemCategory, " [Division No_] = '" & CboDivision.SelectedValue.Trim & "'", True, "Chọn loại hàng")
        ModalPopupPanel.Show()
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CboDivision.SelectedValue.Trim = "" Then
            Label2.Text = "Bạn phải chọn ngành hàng"
            Exit Sub
        End If
        If CboCategory.SelectedValue.Trim = "" Then
            Label2.Text = "Bạn phải chọn loại hàng"
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
            If objG.Load(TxtNo_.Text) Then
                Label2.Text = "Mã đã tồn tại"
                Exit Sub
            End If
        End If
        With objG
            .DivisionNo_ = CboDivision.SelectedValue
            .CategoryNo_ = CboCategory.SelectedValue
            .No_ = TxtNo_.Text
            .Name = TxtName.Text
            .MenuGroupNo_ = CboMenu.SelectedValue
            If Not .Save(TxtOldNo_.Value) Then Exit Sub
        End With
        ShowList()
        ModalPopupPanel.Hide()
    End Sub


    Sub ShowData(ByVal sNo_ As String)
        objG.Load(sNo_)
        CboDivision.SelectedValue = objG.DivisionNo_
        CboDivision_SelectedIndexChanged(CboDivision, Nothing)
        CboCategory.SelectedValue = objG.CategoryNo_
        TxtNo_.Text = objG.No_
        TxtOldNo_.Value = objG.No_
        TxtName.Text = objG.Name
        CboMenu.SelectedValue = IIf(objG.MenuGroupNo_.Trim <> "", objG.MenuGroupNo_, objG.MenuCategoryNo_)
    End Sub

    Protected Sub cmdShowDivision_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowDivision.Click
        ShowData(TxtOldNo_.Value)
        TxtNo_.Focus()
        ModalPopupPanel.Show()
    End Sub

    Protected Sub cmdClosePanel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClosePanel.Click
        ModalPopupPanel.Hide()
    End Sub
End Class
