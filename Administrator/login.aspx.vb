
Partial Class Administrator_login
    Inherits System.Web.UI.Page
    Dim objUser As New adv.Business.Userdefine

    Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
        If Not objUser.Load(Login1.UserName) Then
            LblErr.Text = "Can not find username:" & Login1.UserName
            Exit Sub
        End If
        Dim sMD5 As String
        sMD5 = adv.Business.MD5Encrypt(Login1.UserName & Login1.Password)

        'If sMD5.Trim <> objUser.Password.Trim Then Exit Sub


        Session("adminuser") = Login1.UserName
        Session("adminname") = objUser.Fullname
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Login1.Focus()
    End Sub
End Class
