Imports System.Data

Partial Class Administrator_DivisionList
    Inherits System.Web.UI.Page

    Dim objD As New adv.Business.Division

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowList()
        End If
    End Sub

    Sub ShowList()
        Dim SQL As String
        SQL = "SELECT No_, Name FROM Division "
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
        objD.Load(sNo_)
        TxtNo_.Text = objD.No_
        TxtOldNo_.Value = objD.No_
        TxtName.Text = objD.Name

       

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
            If objD.Load(TxtNo_.Text) Then
                Label2.Text = "Mã đã tồn tại"
                Exit Sub
            End If
        End If
        With objD
            .No_ = TxtNo_.Text
            .Name = TxtName.Text
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

        SQL = " SELECT TOP 1 No_ FROM [Item Category] WHERE [Division No_] = '" & sNo & "'"
        sValue = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        If sValue.Trim <> "" Then
            Dim Script As String
            Script = "alert('Mã ngành hàng này đã được sử dụng. Bạn không thể xóa')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallWarning", Script, True)
            Exit Sub
        End If
        SQL = " DELETE FROM Division WHERE No_ = '" & sNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        ShowList()
    End Sub

    Protected Sub cmdCloseBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClosePanel.Click
        ModalPopupPanel.Hide()
    End Sub


End Class
