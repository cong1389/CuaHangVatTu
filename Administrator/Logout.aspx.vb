
Partial Class Administrator_Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("adminuser") = ""
        Response.Redirect("../")
    End Sub
End Class
