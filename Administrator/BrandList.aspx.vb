Imports System.Data

Partial Class Administrator_BrandList
    Inherits System.Web.UI.Page
    Dim objBrand As New adv.Business.Brand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowList()
        End If
    End Sub

    Sub ShowList()
        Dim SQL As String
        SQL = "SELECT No_, Name FROM [Brand]"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdMenuGroup.DataSource = tbl
        grdMenuGroup.DataBind()
    End Sub


    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        BrandNo.Value = ""
        TxtNo_.Focus()
        ModalPopupBrand.Show()
    End Sub

    Sub ShowData(ByVal sNo_ As String)
        objBrand.Load(sNo_)
        TxtNo_.Text = objBrand.No_
        TxtName.Text = objBrand.Name
        If objBrand.ImageURL.Trim <> "" Then
            ImgBanner.ImageUrl = GetImgUrl() & "Images/BrandLogo/" & objBrand.ImageURL
            TxtImgUrl.Value = objBrand.ImageURL
        Else
            ImgBanner.ImageUrl = ""
            TxtImgUrl.Value = ""
        End If
    End Sub

    Protected Sub cmdShowBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowBrand.Click
        ShowData(BrandNo.Value)
        TxtNo_.Focus()
        ModalPopupBrand.Show()
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If TxtNo_.Text.Trim = "" Then
            Label2.Text = "Bạn phải nhập mã"
            Exit Sub
        End If
        If TxtName.Text.Trim = "" Then
            Label2.Text = "Bạn phải nhập tên"
            Exit Sub
        End If
        If BrandNo.Value = "" Or BrandNo.Value.Trim <> TxtNo_.Text.Trim Then
            If objBrand.Load(TxtNo_.Text) Then
                Label2.Text = "Mã đã tồn tại"
                Exit Sub
            End If
        End If
        With objBrand
            .No_ = TxtNo_.Text
            .Name = TxtName.Text
            .ImageURL = TxtImgUrl.Value
            .Save()
        End With
        ShowList()
        ModalPopupBrand.Hide()
    End Sub

    Protected Sub grdMenuGroup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMenuGroup.PageIndexChanging
        grdMenuGroup.PageIndex = e.NewPageIndex
        ShowList()
    End Sub

    Protected Sub grdMenuGroup_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMenuGroup.RowDeleting
        Dim sNo As String
        sNo = grdMenuGroup.Rows(e.RowIndex).Cells.Item(1).Text()
        Dim SQL As String
        SQL = " DELETE FROM Brand WHERE No_ = '" & sNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        ShowList()
    End Sub

    Protected Sub cmdCloseBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseBrand.Click
        TxtImgUrl.Value = ""
        TxtNo_.Text = ""
        TxtName.Text = ""
        ImgBanner.ImageUrl = ""
        ModalPopupBrand.Hide()
    End Sub


    Protected Sub CmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdUpload.Click
        Dim sFolderName As String = Server.MapPath("../")
        Dim fileExt As String = "", sImgFileName As String = ""
        If UplBrandLogo.HasFile Then
            fileExt = System.IO.Path.GetExtension(UplBrandLogo.FileName).ToUpper
            If fileExt = ".GIF" Or fileExt = ".JPG" Or fileExt = ".PNG" Then
                sFolderName &= "Images\BrandLogo\"
                sImgFileName = sFolderName & UplBrandLogo.FileName
                UplBrandLogo.SaveAs(sImgFileName)
                ImgBanner.ImageUrl = GetUrl() & "Images/BrandLogo/" & UplBrandLogo.FileName
                TxtImgUrl.Value = UplBrandLogo.FileName
            End If
        Else
            TxtImgUrl.Value = ""
        End If
        ModalPopupBrand.Show()
    End Sub
End Class
