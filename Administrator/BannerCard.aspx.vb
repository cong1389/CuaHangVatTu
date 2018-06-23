Imports System
Imports System.IO
Imports System.Collections

Partial Class Administrator_BannerCard
    Inherits System.Web.UI.Page
    Dim sBannerNo As String = ""
    Dim objBanner As New adv.Business.Banner

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            sBannerNo = ReturnIfNull(Request("bannerno"), "").ToString.Trim
            InitCboFile()
            BuildCombo(CboBannerGroup, adv.Business.List.Modules, " [Is Banner] = 1 ", True, "--")
            BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, , True, "--")
            BuildCombo(CboPage, adv.Business.List.Page, , True, "--")
            If sBannerNo.Trim <> "" Then

                ShowData(sBannerNo)
            End If
        End If
    End Sub

    Sub InitCboFile()
        Dim sf As String
        Dim sFolderName As String = Server.MapPath("../")
        sFolderName &= "Images\Banners"
        CboImageFile.Items.Clear()
        For Each sf In Directory.GetFiles(sFolderName & "\")
            CboImageFile.Items.Add(Path.GetFileName(sf))
        Next

    End Sub

    Sub ShowData(ByVal sBannerNo As String)
        With objBanner
            objBanner.Load(sBannerNo)
            CKPublished.Checked = IIf(objBanner.Show = 1, True, False)
            TxtNo_.Value = .No_
            TxtName.Text = .Name
            CboBannerGroup.SelectedValue = .BannerGroupNo_
            CboMenuGroup.SelectedValue = .GoodMenuNo_
            TxtStartingDate.Text = Char2Date(.StartingDate)
            TxtEndingDate.Text = Char2Date(.EndingDate)
            TxtOrderPosition.Text = .OrderPosition
            TxtUrl.Text = .Url
            TxtNumClick.Text = .NumClick
            CKNewWindow.Checked = IIf(.NewWindows = 1, True, False)
            ImgBanner.ImageUrl = GetUrl() & "Images/Banners/" & .ImagesSrc
            CboImageFile.Text = .ImagesSrc
            CboPage.SelectedValue = .Page
            ckRun.Checked = IIf(objBanner.IsRun = 1, True, False)
        End With
    End Sub

    Protected Sub CboImageFile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboImageFile.SelectedIndexChanged
        If CboImageFile.Text.Trim = "" Then
            ImgBanner.ImageUrl = ""
        Else
            ImgBanner.ImageUrl = GetUrl() & "Images/Banners/" & CboImageFile.Text
        End If
    End Sub

    
    Protected Sub CmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdUpload.Click
        Dim sFolderName As String = Server.MapPath("../")
        Dim fileExt As String = "", sImgFileName As String = ""
        If UplImgBanner.HasFile Then
            fileExt = System.IO.Path.GetExtension(UplImgBanner.FileName)
            If fileExt = ".gif" Or fileExt = ".jpg" Or fileExt = ".png" Then
                sFolderName &= "Images\Banners\"
                sImgFileName = sFolderName & UplImgBanner.FileName
                UplImgBanner.SaveAs(sImgFileName)
            End If
            InitCboFile()
            CboImageFile.SelectedValue = UplImgBanner.FileName
            CboImageFile_SelectedIndexChanged(CboImageFile, e)
        End If
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CboBannerGroup.SelectedValue.Trim = "" Then
            LblWarning.Text = "You have to select banner group."
            Exit Sub
        End If
        Dim sNo As String = ""
        Try
            Dim objM As New adv.Business.Modules
            If TxtNo_.Value.Trim = "" Then
                Dim sYM As String = ""
                sYM = sLeft(Date2Char(getToday()), 6)
                Dim objSeriNo As New adv.Business.cNoSeri
                objSeriNo.Load("BANNER")
                sNo = objSeriNo.CreateNoSeri(sYM)
            Else
                sNo = TxtNo_.Value
            End If
            With objBanner
                .No_ = sNo
                .Name = TxtName.Text
                .Show = IIf(CKPublished.Checked, 1, 0)
                .BannerGroupNo_ = CboBannerGroup.SelectedValue
                .StartingDate = Date2Char(TxtStartingDate.Text)
                .EndingDate = Date2Char(TxtEndingDate.Text)
                .OrderPosition = CInt(ReturnIfNull(TxtOrderPosition.Text, 0))
                If TxtUrl.Text.Trim <> "" Then
                    .Url = TxtUrl.Text
                Else
                    .Url = ConvertIntoNone(TxtName.Text)
                End If
                .GoodMenuNo_ = CboMenuGroup.SelectedValue
                .NumClick = CInt(ReturnIfNull(TxtNumClick.Text, 0))
                .NewWindows = IIf(CKNewWindow.Checked, 1, 0)
                .ImagesSrc = CboImageFile.Text
                .Page = CboPage.SelectedValue
                objM.Load(CboBannerGroup.SelectedValue)
                .Width = objM.MaxWidth
                .Height = objM.MaxHeight
                .PositionNo_ = objM.PostionNo_
                .IsRun = IIf(ckRun.Checked, 1, 0)

                .Save()

            End With
            Response.Redirect("BannerList.aspx")
        Catch ex As Exception
            LblWarning.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("BannerList.aspx")
    End Sub
End Class
