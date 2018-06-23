
Partial Class Administrator_MsgContentCard
    Inherits System.Web.UI.Page
    Dim sMsgContentNo As String
    Dim objMsgContent As New adv.Business.MsgContent

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            sMsgContentNo = ReturnIfNull(Request("contentno"), "")
            If sMsgContentNo.Trim <> "" Then
                objMsgContent.Load(sMsgContentNo)
                TxtNo.Text = objMsgContent.No_
                TxtName.Text = objMsgContent.Name
                TxtLinkMenu.Text = objMsgContent.LinkURL
                TxtContent.Value = objMsgContent.Content
            End If
        End If
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If TxtNo.Text = "" Then
            LblWarning1.Text = "Bạn phải nhập mã."
        End If
        With objMsgContent
            .No_ = TxtNo.Text
            .Name = TxtName.Text
            .LinkURL = TxtLinkMenu.Text
            .Content = TxtContent.Value
            .Save()

        End With
        Response.Redirect("MsgContentList.aspx")
    End Sub

    Protected Sub cmdUnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnSave.Click
        Response.Redirect("MsgContentList.aspx")
    End Sub
End Class
