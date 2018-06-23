Imports System.Data

Partial Class Administrator_Assign_Right_For_Group
    Inherits System.Web.UI.Page
    Dim objRFUG As New adv.Business.RightForGroupUser

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboUserGroup, adv.Business.List.UserGroup, , True, "Chọn nhóm người dùng")
        End If
    End Sub

    Function GetData(ByVal sGroupNo As String) As DataTable
        Dim SQL As String
        SQL = " SELECT M.No_, M.Name, CONVERT(bit, ISNULL(R.[View Right],0)) [View Right], CONVERT(bit, ISNULL(R.[Add Right],0)) [Add Right], " & _
            " CONVERT(bit,ISNULL([Edit Right],0)) [Edit Right], CONVERT(bit,ISNULL([Del Right],0)) [Del Right] " & _
            " FROM [Admin Menu] M LEFT JOIN [User Group Right] R on M.No_ = R.[Menu No_] AND R.[User Group No_] = '" & sGroupNo & "'"
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

    End Function

    Protected Sub CboUserGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboUserGroup.SelectedIndexChanged
        Dim tTbl As DataTable
        tTbl = GetData(CboUserGroup.SelectedValue)
        grdRightList.DataSource = tTbl
        grdRightList.DataBind()
    End Sub


    Protected Sub grdRightList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRightList.SelectedIndexChanged
        Dim row As GridViewRow = grdRightList.SelectedRow
        TxtMenuNo.Text = row.Cells.Item(0).Text.Trim
        TxtMenuName.Text = row.Cells.Item(1).Text.Trim
        objRFUG.Load(CboUserGroup.SelectedValue, row.Cells.Item(0).Text.Trim)
        CKView.Checked = IIf(objRFUG.ViewRight = 1, True, False)
        CKAddNew.Checked = IIf(objRFUG.AddRight = 1, True, False)
        CKEdit.Checked = IIf(objRFUG.EditRight = 1, True, False)
        CKDelete.Checked = IIf(objRFUG.DelRight = 1, True, False)
    End Sub

    Function UpdateRightForGroup() As Boolean
        Try
            With objRFUG
                .UserGroupNo_ = CboUserGroup.SelectedValue
                .MenuNo_ = TxtMenuNo.Text
                .ViewRight = IIf(CKView.Checked, 1, 0)
                .AddRight = IIf(CKAddNew.Checked, 1, 0)
                .EditRight = IIf(CKEdit.Checked, 1, 0)
                .DelRight = IIf(CKDelete.Checked, 1, 0)
                .Save()
            End With
            Return True
        Catch ex As Exception
            LblWarning.Text = ex.Message
            Return False
        End Try
    End Function

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        UpdateRightForGroup()
        Dim tTbl As DataTable
        tTbl = GetData(CboUserGroup.SelectedValue)
        grdRightList.DataSource = tTbl
        grdRightList.DataBind()
    End Sub
End Class
