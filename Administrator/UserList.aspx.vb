
Partial Class Administrator_Default2
    Inherits System.Web.UI.Page
    Dim objUser As New adv.Business.Userdefine



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
        End If
    End Sub

    Sub LoadData()
        GrUserList.DataSource = objUser.GetList
        GrUserList.DataBind()
    End Sub

    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        Response.Redirect("UserCard.aspx")
    End Sub

    Protected Sub cmdAssignRightForGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAssignRightForGroup.Click
        Response.Redirect("AssignRightForGroup.aspx")
    End Sub
End Class
