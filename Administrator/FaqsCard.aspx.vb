
Partial Class Administrator_FaqsCard
    Inherits System.Web.UI.Page
    Dim nLineNo As Integer
    Dim objFAQ As New adv.Business.FAQs

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            nLineNo = ReturnIfNull(Request("lineno"), 0)
            If nLineNo <> 0 Then
                objFAQ.Load(nLineNo)
                ShowData()
            Else
                EmptyData()
            End If
        End If
    End Sub

    Sub ShowData()
        With objFAQ
            TxtLineNo.Value = .LineNo_
            TxtQuestion.Text = .Question
            TxtLinkMenu.Text = .Link
            txtOrderPosition.Text = .OrderPosition
            CKPublished.Checked = IIf(.Published = 1, True, False)
            TxtContent.Value = .Answer
        End With
    End Sub

    Sub EmptyData()
        TxtLineNo.Value = 0
        TxtQuestion.Text = ""
        TxtLinkMenu.Text = ""
        TxtOrderPosition.Text = 0
        CKPublished.Checked = True
        TxtContent.Value = ""
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If TxtQuestion.Text.Trim = "" Then
            LblWarning1.Text = "Bạn phải nhập câu hỏi"
            Exit Sub
        End If
        If TxtContent.Value.Trim = "" Then
            LblWarning1.Text = "Bạn phải nhập câu trả lời"
            Exit Sub
        End If
        With objFAQ
            If TxtLineNo.Value.Trim <> "" And TxtLineNo.Value.Trim <> "0" Then
                .Load(TxtLineNo.Value)
            End If
            .Question = TxtQuestion.Text
            .Link = IIf(TxtLinkMenu.Text.Trim = "", CreateLink(TxtQuestion.Text), TxtLinkMenu.Text)
            .UserID = Session("adminuser")
            If .DateCreate.Trim = "" Then .DateCreate = Date2Char(getToday)
            .OrderPosition = IIf(TxtOrderPosition.Text.Trim <> "", TxtOrderPosition.Text, 0)
            .Published = IIf(CKPublished.Checked, 1, 0)
            .Answer = TxtContent.Value
            If Not .Save Then Exit Sub
            Response.Redirect("FaqsList.aspx")
        End With
    End Sub

    Protected Sub cmdUnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnSave.Click
        Response.Redirect("FaqsList.aspx")
    End Sub
End Class
