Imports System.Data

Partial Class Administrator_ProvinceList
    Inherits System.Web.UI.Page
    Dim objP As New adv.Business.Province

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowList()
        End If
    End Sub

    Sub ShowList()
        Dim SQL As String
        SQL = "SELECT No_, Name FROM Province WHERE [Type] = 0 "
        If TxtSearch.Text.Trim <> "" Then
            SQL &= " AND ( Name Like '%" & TxtSearch.Text & "%' OR No_  Like '%" & TxtSearch.Text & "%' )"
        End If
        SQL &= " ORDER BY 2 "
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
        objP.Load(sNo_)
        TxtNo_.Text = objP.No_
        TxtOldNo_.Value = objP.No_
        TxtName.Text = objP.Name
        TxtNumOfDayMax.Text = objP.NumofDayTo
        TxtNumOfDayMin.Text = objP.NumofDayFrom
        CKBigcity.Checked = IIf(objP.BigCity = 1, True, False)
    End Sub

    Sub EmptyData()
        TxtNo_.Text = ""
        TxtOldNo_.Value = ""
        TxtName.Text = ""
        TxtNumOfDayMax.Text = "0"
        TxtNumOfDayMin.Text = "0"
        CKBigcity.Checked = False
    End Sub

    Protected Sub cmdShowDivision_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowDivision.Click
        ShowData(TxtOldNo_.Value)
        TxtNo_.Focus()
        ModalPopupPanel.Show()
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
            If objP.Load(TxtNo_.Text) Then
                Label2.Text = "Mã đã tồn tại"
                Exit Sub
            End If
        End If
        With objP
            .No_ = TxtNo_.Text
            .Name = TxtName.Text
            .Type = 0
            .BigCity = IIf(CKBigcity.Checked, 1, 0)
            .NumofDayFrom = Val(TxtNumOfDayMin.Text)
            .NumofDayTo = Val(TxtNumOfDayMax.Text)
            .Save(TxtOldNo_.Value)
            EmptyData()
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

        SQL = " SELECT TOP 1 No_ FROM Province WHERE [Parent No_] = '" & sNo & "'"
        sValue = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        If sValue.Trim <> "" Then
            Dim Script As String
            Script = "alert('Mã loại hàng này đã được sử dụng. Bạn không thể xóa')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallWarning", Script, True)
            Exit Sub
        End If
        SQL = " DELETE FROM Province WHERE No_ = '" & sNo & "'"
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
