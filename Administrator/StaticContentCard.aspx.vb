
Partial Class Administrator_StaticContentCard
    Inherits System.Web.UI.Page
    Dim objContent As New adv.Business.Content

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, , True, "--")
            BuildCombo(CboGroupContent, adv.Business.List.GroupContent, " [Content Type] = 0 ", False, "")
            Dim sNo_ As String = ""
            sNo_ = ReturnIfNull(Request.QueryString("ContentNo"), "").ToString.Trim
            If sNo_.Trim <> "" Then ShowData(sNo_)
        End If
    End Sub

    Sub ShowData(ByVal sContentNo_ As String)
        objContent.Load(sContentNo_)
        TxtContentNo.Value = objContent.No_
        TxtTitle.Text = objContent.Title
        CKPublished.Checked = IIf(objContent.Published = 1, True, False)
        CboGroupContent.SelectedValue = objContent.GroupNo_
        CboMenuGroup.SelectedValue = objContent.MenuGroupNo_
        TxtOrderPosition.Text = objContent.OrderPosition
        TxtContent.Value = objContent.Content
        TxtLinkMenu.Text = objContent.LinkMenu
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sNo As String
        If TxtContentNo.Value.Trim = "" Then
            Dim sYM As String = ""
            sYM = sLeft(Date2Char(getToday()), 6)
            Dim objSeriNo As New adv.Business.cNoSeri
            objSeriNo.Load("CONTENTNO")
            sNo = objSeriNo.CreateNoSeri(sYM)
        Else
            sNo = TxtContentNo.Value
        End If
        With objContent
            .Load(sNo)
            TxtContentNo.Value = sNo
            .No_ = sNo
            .CategoryNo_ = ""
            .GroupNo_ = CboGroupContent.SelectedValue
            .Title = TxtTitle.Text
            If .DateCreate.Trim = "" Then .DateCreate = Date2Char(getToday())
            .Published = IIf(CKPublished.Checked, 1, 0)
            .Content = TxtContent.Value
            .MenuDivisionNo_ = ""
            .MenuCategoryNo_ = ""
            .MenuGroupNo_ = CboMenuGroup.SelectedValue
            .ItemNo_ = ""
            .ContentType = 0
            .Type = 0
            .ShowDefault = 0
            .UserCreate = Session("adminuser")

            .LinkMenu = TxtLinkMenu.Text
            If .LinkMenu.Trim = "" Then
                .LinkMenu = ConvertIntoNone(.Title)
            End If
            .OrderPosition = ReturnIfNull(TxtOrderPosition.Text, 1)
            .Save()
            Response.Redirect("ContentStaticList.aspx")
        End With
    End Sub

    Protected Sub cmdUnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnSave.Click
        Response.Redirect("ContentStaticList.aspx")
    End Sub
End Class
