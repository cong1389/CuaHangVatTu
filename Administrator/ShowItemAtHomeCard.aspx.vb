
Partial Class Administrator_ShowItemAtHomeCard
    Inherits System.Web.UI.Page
    Dim objSLH As New adv.Business.ShowListHeader
    Dim objSLL As New adv.Business.ShowListLine
    Dim sDocumentNo As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, "", True, "")
            sDocumentNo = ReturnIfNull(Request("no_"), "").ToString.Trim
            If sDocumentNo.Trim <> "" Then
                ShowData(sDocumentNo)
            End If
        End If
    End Sub

    Sub ShowData(ByVal sNo As String)
        objSLH.Load(sNo)
        With objSLH
            TxtNo_.Value = .No_
            TxtName.Text = .Title
            CboMenuGroup.SelectedValue = .MenuNo_
            TxtStartingDate.Text = Char2Date(.StartingDate)
            TxtEndingDate.Text = Char2Date(.EndingDate)
            CboShowType.SelectedValue = .ListType
            TxtNumItem.Text = .NumItemAtHome
            TxtUrlPage.text = .UrlPage
            TxtOrderPosition.Text = .PositionOrder
            CKShow.Checked = IIf(.Published = 1, True, False)
        End With
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sNo As String
        If TxtNo_.Value.Trim = "" Then
            Dim sYM As String = ""
            sYM = sLeft(Date2Char(getToday()), 6)
            Dim objSeriNo As New adv.Business.cNoSeri
            objSeriNo.Load("BANNER")
            sNo = objSeriNo.CreateNoSeri(sYM)
        Else
            sNo = TxtNo_.Value
        End If
        With objSLH
            .No_ = sNo
            .Title = TxtName.Text
            .UrlPage = TxtUrlPage.Text
            .ListType = CboShowType.SelectedValue
            .StartingDate = Date2Char(TxtStartingDate.Text)
            .EndingDate = Date2Char(TxtEndingDate.Text)
            .NumItemAtHome = Val(TxtNumItem.Text)
            .Published = IIf(CKShow.Checked, 1, 0)
            .PositionOrder = Val(TxtOrderPosition.Text)
            .MenuNo_ = CboMenuGroup.SelectedValue
            .Save()
            Response.Redirect("ShowItemAtHomeList.aspx")
        End With
    End Sub
End Class
