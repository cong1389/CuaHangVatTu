
Partial Class Administrator_ContentCard
    Inherits System.Web.UI.Page
    Dim objContent As New adv.Business.Content

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, , True, "--")
            BuildCombo(CboContentType, adv.Business.List.TypeOfContent, " No_ > 0 ", True, "--")
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
        CboMenuGroup.SelectedValue = objContent.MenuGroupNo_
        TxtOrderPosition.Text = objContent.OrderPosition
        TxtContent.Value = objContent.Content
        TxtLinkMenu.Text = objContent.LinkMenu
        TxtSumary.Text = objContent.Summary
        TxtMetaKeywords.Text = objContent.MetaKeywords
        CboContentType.SelectedValue = objContent.ContentType
        CboContentType_SelectedIndexChanged(CboContentType, Nothing)
        CboGroupContent.SelectedValue = objContent.GroupNo_
        CKHotContent.Checked = IIf(objContent.HotContent = 1, True, False)
        If objContent.ImagesURL.Trim <> "" Then
            ImgContentRepresentative.ImageUrl = GetUrl() & "Images/Content/Images/" & objContent.ImagesURL
        End If
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sNo As String
        If CboContentType.SelectedValue.Trim = "" Then
            LblWarning1.Text = "Bạn phải chọn loại tin tức"
            Exit Sub
        End If
        If CboGroupContent.SelectedValue.Trim = "" Then
            LblWarning1.Text = "Bạn phải chọn nhóm tin tức"
            Exit Sub
        End If
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
            .CategoryNo_ = CboContentType.SelectedValue
            .ContentType = CInt(CboContentType.SelectedValue)
            .GroupNo_ = CboGroupContent.SelectedValue
            .Title = TxtTitle.Text
            If .DateCreate.Trim = "" Then .DateCreate = Date2Char(getToday())
            .Published = IIf(CKPublished.Checked, 1, 0)
            .Content = TxtContent.Value
            .MenuDivisionNo_ = ""
            .MenuCategoryNo_ = ""
            .MenuGroupNo_ = CboMenuGroup.SelectedValue
            .ItemNo_ = ""
            .ContentType = CboContentType.SelectedValue
            .Type = .ContentType
            .ShowDefault = 0
            .UserCreate = Session("adminuser")
            .LinkMenu = TxtLinkMenu.Text
            If .LinkMenu.Trim = "" Then
                .LinkMenu = ConvertIntoNone(.Title)
            End If
            .OrderPosition = ReturnIfNull(TxtOrderPosition.Text, 1)
            .MetaKeywords = TxtMetaKeywords.Text
            If FileUploadImg.HasFile Then
                Dim sFolderName As String = Server.MapPath("../")
                Dim fileExt As String = "", sImgFileName As String = ""
                fileExt = System.IO.Path.GetExtension(FileUploadImg.FileName)
                If fileExt = ".gif" Or fileExt = ".jpg" Or fileExt = ".png" Then
                    sFolderName &= "Images\Content\Images\"
                    sImgFileName = sFolderName & FileUploadImg.FileName
                    FileUploadImg.SaveAs(sImgFileName)
                    .ImagesURL = FileUploadImg.FileName

                End If
            End If
            .HotContent = IIf(CKHotContent.Checked, 1, 0)
            .Summary = TxtSumary.Text
            .Save()
            Response.Redirect("TechnologyContentList.aspx")
        End With
    End Sub

    Protected Sub cmdUnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnSave.Click
        Response.Redirect("TechnologyContentList.aspx")
    End Sub

    Protected Sub CboContentType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboContentType.SelectedIndexChanged
        BuildCombo(CboGroupContent, adv.Business.List.GroupContent, " [Content Type] = " & CboContentType.SelectedValue & " ", False, "")
    End Sub
End Class
