Imports System.Data

Partial Class CheckIn
    Inherits System.Web.UI.Page

    Dim objC As New adv.Business.Customer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ReturnIfNull(Session("customerno"), "").ToString.Trim <> "" Then
            Response.Redirect("thanh-toan/")
        End If

        If Not IsPostBack Then
            LblPathWay.Text = ShowPathWay()

            TxtEmail.Focus()
        End If
    End Sub

    Function ShowMenuHelp() As String
        Dim sHtml As String = ""
        sHtml &= "<div class=""containerleft"">" & vbCrLf
        sHtml &= "<div class=""leftheader"">Hỗ trợ khách hàng </div>" & vbCrLf
        sHtml &= "<div style=""min-height:90px;padding:5px;"">" & vbCrLf
        sHtml &= ShowLinkMenu("10", "categorylink") & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        Return sHtml

    End Function

    Function ShowPathWay() As String
        Dim sHtml As String = ""
        sHtml = "<a href=""" & GetUrl() & """ class=""linkheader"">Trang chủ</a> "
        sHtml &= "> Ghi nhận thông tin khách hàng"
        Return sHtml
    End Function


    Protected Sub cmdRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        Response.Redirect(GetUrl() & "dang-ky/")
    End Sub

    Protected Sub cmdLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim sEmail As String = TxtEmail.Text
        Dim sPwd As String = TxtPassword.Text
        If Not objC.LoadByEmail(sEmail) Then
            LblErrCheckin.Text = "Email không tồn tại."
            Exit Sub
        End If
        If adv.Business.MD5Encrypt(objC.Email.Trim & sPwd.Trim) <> objC.Password Then
            LblErrCheckin.Text = "Mật khẩu không đúng."
            Exit Sub
        End If

        Session("customerno") = objC.No_
        Response.Redirect(GetUrl() & "static-detail/chinh-sach-thanh-toan/")
    End Sub

    Protected Sub cmdOKGetPwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOKGetPwd.Click
        TxtEmailFogot.Enabled = False
        ModalPopupExtenderPwd.Hide()
        If TxtEmailFogot.Text.Trim = "" Then
            LblErrEmail.Text = "Bạn phải nhập địa chỉ email."
            ModalPopupExtenderPwd.Show()
            Exit Sub
        End If

        If Not IsEmail(TxtEmailFogot.Text.Trim) Then
            LblErrEmail.Text = "Địa chỉ email không đúng."
            ModalPopupExtenderPwd.Show()
            Exit Sub
        End If

        If Not objC.LoadByEmail(TxtEmailFogot.Text.Trim) Then
            LblErrEmail.Text = "Email không tồn tại."
            ModalPopupExtenderPwd.Show()
            Exit Sub
        End If

        SendEmail()
        cmdOKGetPwd.Visible = False
        ModalPopupExtenderPwd.Show()
    End Sub

    Sub SendEmail()
        Dim objWM As New adv.Business.WebEmail
        objWM.ToAddress = TxtEmailFogot.Text
        objWM.Subject = "Mật khẩu đăng nhập tài khoản HOMEONE"
        objWM.FromAddress = GetEmailAdd()
        objWM.IsEmailBodyHtml = True
        objC.LoadByEmail(TxtEmailFogot.Text)
        Dim sEmailTmp As String = ""
        sEmailTmp &= "Bạn " & objC.FullName & " thân mến <br /><br />"
        sEmailTmp &= "Mật khẩu đăng nhập tài khoản của bạn là:" & objC.ResetPassword(TxtEmailFogot.Text) & "<br />"
        sEmailTmp &= "Bạn đăng nhập và đổi lại mật khẩu mới!<br />"
        sEmailTmp &= "<p>Cám ơn bạn!</p>"
        sEmailTmp &= "<p>Quản trị HOMEONE</p>"

        objWM.EmailBody = sEmailTmp
        Dim sReturn As String
        sReturn = objWM.SendMail()
        If sReturn = "OK" Then
            LblErrEmail.Text = "Mật khẩu đăng nhập của bạn đã được gửi đến email " & TxtEmailFogot.Text & ""
        Else
            LblErrEmail.Text = sReturn
        End If
    End Sub

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect(GetUrl() & "gio-hang/")
    End Sub

    Protected Sub cmdQuickOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdQuickOrder.Click
        Session("CustomerNo") = GetCustomerDefault()
        Response.Redirect(GetUrl() & "static-detail/chinh-sach-thanh-toan/")
    End Sub
End Class
