
Partial Class Administrator_PromotionContentCard
    Inherits System.Web.UI.Page
    Dim objPromotionContent As New adv.Business.PromotionContent

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim sNo_ As String = ""
            sNo_ = ReturnIfNull(Request.QueryString("ContentNo"), "").ToString.Trim
            If sNo_.Trim <> "" Then
                ShowData(sNo_)
            End If

        End If
    End Sub

    Sub ShowData(ByVal sContentNo As String)
        With objPromotionContent
            .Load(sContentNo)
            TxtNo_.Text = sContentNo
            TxtName.Text = .Title
            TxtStartingDate.Text = Char2Date(.StartingDate)
            TxtEndingDate.Text = Char2Date(.EndingDate)
            TxtLinkContent.Text = .LinkContent
            CKPublished.Checked = IIf(.Published = 1, True, False)
            TxtColor.Text = .PageBackground
            TxtContent.Value = .Content
        End With
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim sNo As String
            If TxtNo_.Text.Trim = "" Then
                Dim sYM As String = ""
                sYM = sLeft(Date2Char(getToday()), 6)
                Dim objSeriNo As New adv.Business.cNoSeri
                objSeriNo.Load("PROCONTNO ")
                sNo = objSeriNo.CreateNoSeri(sYM)
            Else
                sNo = TxtNo_.Text
            End If
            With objPromotionContent
                .Load(sNo)
                .No_ = sNo
                .Title = TxtName.Text
                .StartingDate = Date2Char(TxtStartingDate.Text)
                .EndingDate = Date2Char(TxtEndingDate.Text)
                .LinkContent = TxtLinkContent.Text
                .Published = IIf(CKPublished.Checked, 1, 0)
                .PageBackground = TxtColor.Text
                .Content = TxtContent.Value
                .Save()
            End With
            Response.Redirect("PromotionsContentList.aspx")
        Catch ex As Exception
            LblWarning.Text = ex.Message
        End Try
    End Sub

End Class
