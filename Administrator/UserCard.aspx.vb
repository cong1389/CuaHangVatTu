
Partial Class Administrator_UserCard
    Inherits System.Web.UI.Page
    Dim objUser As New adv.Business.Userdefine
    Dim sUserName As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(DrdUserGroup, adv.Business.List.UserGroup)
            sUserName = ReturnIfNull(Request("username"), "").ToString.Trim
            If sUserName.Trim <> "" Then
                ShowData(sUserName)
            End If
        End If
    End Sub

    Sub ShowData(ByVal sUser As String)
        objUser.Load(sUser)
        With objUser
            TxtUserName.Text = .Username
            TxtFullName.Text = .Fullname
            TxtDivision.Text = .Division
            DrdUserGroup.SelectedValue = .UserGroupNo_
            TxtUserNameOld.Value = .Username
            TxtEmail.Text = .Email
            CKPublished.Checked = IIf(.Active = 1, True, False)
        End With

    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("UserList.aspx")
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If TxtUserNameOld.Value.Trim = "" Then
            If TxtPassowrd.Text.Trim = "" Then
                LblWarning.Text = "Bạn phải nhập mật khẩu."
                Exit Sub
            End If
            If TxtPassowrd.Text.Trim.Length < 6 Then
                LblWarning.Text = "Mật khẩu phải 6 ký tự trở lên."
                Exit Sub
            End If
            If TxtPassowrd.Text.Trim <> TxtConfirmPassword.Text.Trim Then
                LblWarning.Text = "Xác nhận mật khẩu không chính xác."
                Exit Sub
            End If
        Else
            If TxtPassowrd.Text.Trim <> "" Then
                If TxtPassowrd.Text.Trim.Length < 6 Then
                    LblWarning.Text = "Mật khẩu phải 6 ký tự trở lên."
                    Exit Sub
                End If
                If TxtPassowrd.Text.Trim <> TxtConfirmPassword.Text.Trim Then
                    LblWarning.Text = "Xác nhận mật khẩu không chính xác."
                    Exit Sub
                End If
            End If
        End If
        Try
            With objUser
                .Load(ReturnIfNull(TxtUserNameOld.Value, "").ToString.Trim)
                .Username = TxtUserName.Text
                Dim sMD5 As String
                If TxtPassowrd.Text.Trim <> "" Then
                    sMD5 = adv.Business.MD5Encrypt(.Username & TxtPassowrd.Text.Trim)
                    .Password = sMD5
                End If
                .Fullname = TxtFullName.Text
                .UserGroupNo_ = DrdUserGroup.SelectedValue
                .Email = TxtEmail.Text
                .StaffNo_ = ""
                .Active = IIf(CKPublished.Checked, 1, 0)
                .Division = TxtDivision.Text
                If Not .Save(ReturnIfNull(TxtUserNameOld.Value, "").ToString.Trim) Then Exit Sub
                Response.Redirect("UserList.aspx")
            End With
        Catch ex As Exception
            LblWarning.Text = ex.Message
        End Try
    End Sub
End Class
