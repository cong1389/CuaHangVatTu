Imports System.Data

Partial Class Administrator_Feature
    Inherits System.Web.UI.Page

    Dim objFeature As New adv.Business.FeatureGroup

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillData()
        End If
    End Sub

    Sub FillData()
        Dim SQL As String
        SQL = " select No_ , Name ,[Type] = Case [Type] when 0 then	N'Nhóm' when 1 then N'Thuộc tính' end, Descriptions" & _
                " from [Feature Group] order by 1, 2 "
        Dim dtbl As DataTable
        dtbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdFeatureList.DataSource = dtbl
        grdFeatureList.DataBind()
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objFeature.SetBlank()
        objFeature.No_ = TxtNo.Text
        objFeature.Name = TxtName.Text
        objFeature.Type = CInt(CboType.SelectedValue)
        objFeature.Descriptions = TxtDescriptions.Text
        If TxtNo.Text.Trim <> TxtOldNo.Value.Trim Then
            If Not objFeature.CheckExistsNo(TxtNo.Text.Trim) Then
                LblWarning.Text = "The code has already exists."
                Exit Sub
            End If
        End If
        If Not objFeature.Update(TxtOldNo.Value) Then Exit Sub
        FillData()
        grdFeatureList.SelectedIndex = -1
        TxtNo.Text = ""
        TxtName.Text = ""
        TxtDescriptions.Text = ""
        TxtOldNo.Value = ""
        LblWarning.Text = ""
    End Sub

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        grdFeatureList.SelectedIndex = -1
        TxtNo.Text = ""
        TxtName.Text = ""
        CboType.SelectedIndex = 0
        TxtDescriptions.Text = ""
        TxtOldNo.Value = ""
        LblWarning.Text = ""
    End Sub

    Protected Sub grdFeatureList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdFeatureList.RowDeleting
        Dim sFNo As String
        sFNo = grdFeatureList.Rows(e.RowIndex).Cells.Item(0).Text()
        Dim SQL As String
        SQL = "delete from [Feature Group] where No_ = '" & sFNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        FillData()
    End Sub

    Protected Sub grdFeatureList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFeatureList.SelectedIndexChanged
        Dim row As GridViewRow = grdFeatureList.SelectedRow
        TxtNo.Text = row.Cells.Item(0).Text.Trim
        objFeature.Load(TxtNo.Text)
        TxtName.Text = objFeature.Name
        CboType.SelectedValue = objFeature.Type
        TxtDescriptions.Text = objFeature.Descriptions
        TxtOldNo.Value = TxtNo.Text
    End Sub
End Class
