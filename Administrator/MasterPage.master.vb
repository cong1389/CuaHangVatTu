
Partial Class Administrator_MasterPage
    Inherits System.Web.UI.MasterPage
    Dim objU As New adv.Business.Userdefine

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ReturnIfNull(Session("adminuser"), "").ToString.Trim = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Protected Sub cmdOKChangePwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOKChangePwd.Click
        If TxtOldPwd.Text = "" Then
            LblErr.Text = "Bạn phải nhập mật khẩu cũ."
            ModalPopupExtenderPwd.Show()
            Exit Sub
        End If

        If TxtNewPwd.Text = "" Then
            LblErr.Text = "Bạn phải nhập mật khẩu mới."
            ModalPopupExtenderPwd.Show()
            Exit Sub
        End If

        If TxtNewPwd.Text <> TxtConfirmPwd.Text Then
            LblErr.Text = "Xác nhận mật khẩu không khớp."
            ModalPopupExtenderPwd.Show()
            Exit Sub
        End If

        Dim sOldPwd As String
        With objU
            .Load(Session("adminuser"))
            sOldPwd = adv.Business.MD5Encrypt(.Username & TxtOldPwd.Text.Trim)
            If sOldPwd <> .Password Then
                LblErr.Text = "Mật khẩu cũ không chính xác."
                Exit Sub
            End If
        End With

        Dim sNewPwd As String = adv.Business.MD5Encrypt(objU.Username.Trim & TxtNewPwd.Text.Trim)

        If Not objU.ChangePwd(objU.Username, sNewPwd) Then Exit Sub

        LblMasterMenuDescription.Text = "Đổi mật khẩu thành công."
        ModalPopupExtenderPwd.Hide()
    End Sub

    Protected Sub cmdCancelChangePwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelChangePwd.Click
        ModalPopupExtenderPwd.Hide()
    End Sub
End Class

