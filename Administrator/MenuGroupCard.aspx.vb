Imports System.Data
Imports System.Drawing
Imports System.Drawing.Image

Partial Class Administrator_MenuGroupCard
    Inherits System.Web.UI.Page
    Dim sMenuNo As String
    Dim objMN As New adv.Business.MenuItem
    Dim objCF As New adv.Business.CategoryFeature
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            sMenuNo = ReturnIfNull(Request.QueryString("menu"), "")
            Dim Lbl As Label = CType(Master.FindControl("LblMasterMenu"), Label)
            Lbl.Text = "<a href=""MenuGroupList.aspx"">Danh sách </a>"
            InitCombo()
            If sMenuNo.Trim <> "" Then
                ShowData(sMenuNo)
            End If
        End If
    End Sub

    Sub LoadSearchByPrice(ByVal MenuNo As String)
        Dim SQL As String = ""
        SQL = " select * from [Search Product By Price] where [Category No_] = '" & MenuNo & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GridView1.DataSource = tbl
        GridView1.DataBind()
    End Sub

    Sub InitCombo()
        BuildCombo(CboFeatureGroup, adv.Business.List.Feature, " [Type] = 0 ", False)
        BuildCombo(CboPage, adv.Business.List.Page, , True, "--")
    End Sub

    Sub ShowData(ByVal sMenuNo_ As String)
        objMN.Load(sMenuNo_)
        With objMN
            TxtNo_.Text = .No_
            TxtName.Text = .Name
            CboType.SelectedValue = .MenuType.ToString
            CboType_SelectedIndexChanged(CboType, Nothing)
            CKPublished.Checked = IIf(.Published = 1, True, False)
            TxtMenuOrder.Text = .MenuOrder
            CboMenuGroup.SelectedValue = .ParentNo_
            CboPage.SelectedValue = .PageNo_
            TxtLinkDisplay.Text = .LinkDisplay
            txtMetaKeywords.Text = .MetaKeywords
            imgFull.ImageUrl = IIf(.ImagesURL.Trim <> "", GetUrl() & "Images/ProductGroup/" & .ImagesURL, GetUrl() & "Images/Template/NoImage.jpg")
            imgThum.ImageUrl = IIf(.ImagesThumURL.Trim <> "", GetUrl() & "Images/ProductGroup/" & .ImagesThumURL, GetUrl() & "Images/Template/NoImage.jpg")
            If .MenuType = 2 Or .MenuType = 0 Then
                TabPanel2.Enabled = False
            End If
        End With
        FillFeature(sMenuNo_)
        LoadSearchByPrice(sMenuNo_)

    End Sub

    Protected Sub cmdLoadFeature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadFeature.Click
        Dim SQL As String
        SQL = "SELECT No_, Name FROM [Feature Group] WHERE [Type] = 1 ORDER BY Name "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdFeature.DataSource = tbl
        grdFeature.DataBind()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sNo As String = ""
        If TxtName.Text.Trim = "" Then
            LblWarning.Text = "Bạn phải nhập tên"
            Exit Sub
        End If

        If CboType.SelectedIndex > 0 Then
            If CboMenuGroup.SelectedValue = "" Then
                LblWarning.Text = "Bạn phải chọn nhóm"
                Exit Sub
            End If
        End If
        If TxtLinkDisplay.Text.Trim = "" Then
            LblWarning.Text = "Bạn phải nhập link hiển thị"
            Exit Sub
        End If
        With objMN
            If Not .Load(TxtNo_.Text) Then
                .SetBlank()
            Else
                If .MenuType <> ReturnIfNull(CboType.SelectedValue, "") Then
                    TxtNo_.Text = ""
                    Dim SQL As String
                    SQL = " DELETE FROM "
                End If
            End If
            If TxtNo_.Text.Trim = "" Then
                sNo = CreateNo(CboType.SelectedValue, CboMenuGroup.SelectedValue)
            Else
                sNo = TxtNo_.Text.Trim
            End If
            .No_ = sNo
            .Name = TxtName.Text
            .MenuType = ReturnIfNull(CboType.SelectedValue, "")
            .ParentLink = IIf(.MenuType = 0, "sp", "")
            .MenuOrder = CInt(ReturnIfNull(TxtMenuOrder.Text, 0))
            .ParentNo_ = ReturnIfNull(CboMenuGroup.SelectedValue, "")
            .LinkDisplay = TxtLinkDisplay.Text
            .Published = IIf(CKPublished.Checked, 1, 0)
            .PageNo_ = CboPage.SelectedValue
            .MetaKeywords = txtMetaKeywords.Text
        End With

        Dim fileExt As String
        Dim sImgFileName As String = ""
        Dim sImgThumbFileName As String
        Dim sFolderName As String = Server.MapPath("../")
        sFolderName &= "Images\ProductGroup\"
        If UplImagesURL.HasFile Then
            fileExt = System.IO.Path.GetExtension(UplImagesURL.FileName)
            If fileExt.ToUpper.Trim = ".GIF" Or fileExt.ToUpper.Trim = ".JPG" Or fileExt.ToUpper.Trim = ".PNG" Then
                sImgFileName = sFolderName & TxtNo_.Text & fileExt
                UplImagesURL.SaveAs(sImgFileName)
                objMN.ImagesURL = TxtNo_.Text & fileExt
            End If
        End If

        If UplImagesThumURL.HasFile Then
            fileExt = System.IO.Path.GetExtension(UplImagesThumURL.FileName)
            If fileExt.ToUpper.Trim = ".GIF" Or fileExt.ToUpper.Trim = ".JPG" Or fileExt.ToUpper.Trim = ".PNG" Then
                sImgThumbFileName = sFolderName & TxtNo_.Text & "_T" & fileExt
                UplImagesThumURL.SaveAs(sImgThumbFileName)
                objMN.ImagesThumURL = TxtNo_.Text & "_T" & fileExt
            End If
        End If
        Try
            objMN.Save()
            imgFull.ImageUrl = IIf(objMN.ImagesURL.Trim <> "", GetUrl() & "Images/ProductGroup/" & objMN.ImagesURL, GetUrl() & "Images/Template/NoImage.jpg")
            imgThum.ImageUrl = IIf(objMN.ImagesThumURL.Trim <> "", GetUrl() & "Images/ProductGroup/" & objMN.ImagesThumURL, GetUrl() & "Images/Template/NoImage.jpg")
            Label2.Text = "Updated sucessful."
            ShowData(objMN.No_)
        Catch ex As Exception
            Label2.Text = ex.Message
        End Try

    End Sub


    Function CreateNo(ByVal nType As Integer, ByVal sParentNo As String) As String
        Dim SQL As String = ""
        Dim nMaxNo As Integer, sMaxNo As String, sNo As String = ""
        Select Case nType
            Case 0
                SQL = " SELECT ISNULL(MAX(No_),'00') FROM [Good Menu] WHERE [Menu Type] = 0 "
                sMaxNo = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
                nMaxNo = CInt(sMaxNo) + 1
                sNo = sRight("00" & nMaxNo, 2)
            Case 1
                SQL = " SELECT ISNULL(MAX(No_),'0000') FROM [Good Menu] WHERE [Menu Type] = 1 AND [Parent No_] = '" & sParentNo & "'"
                sMaxNo = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
                nMaxNo = CInt(sMaxNo) + 1
                sNo = sParentNo & sRight("00" & nMaxNo, 2)
            Case 2
                SQL = " SELECT ISNULL(MAX(No_),'000000') FROM [Good Menu] WHERE [Menu Type] = 2 AND [Parent No_] = '" & sParentNo & "'"
                sMaxNo = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
                nMaxNo = CInt(sMaxNo) + 1
                sNo = sParentNo & sRight("0000" & nMaxNo, 4)
        End Select
        Return sNo
    End Function

    Sub EmptyCategoryFeatureGroupData()
        TxtFeatureNo.Text = ""
        TxtFeatureCode.Value = ""
        TxtFeatureName.Text = ""
        TxtOrderPosition.Text = "0"
        TxtOptionString.Text = ""
        TxtDescription.Text = ""
        CKShowInList.Checked = False
        CKFilter.Checked = False
        TxtOldValues.Value = ""
    End Sub

    Protected Sub cmdSaveFeatureCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveFeatureCategory.Click
        Dim sCategoryNoOld As String, sFeatureGroupNoOld As String, sFeatureNoOld As String, sValueOld As String, sArr As Object
        If ReturnIfNull(CboFeatureGroup.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", "alert('Bạn phải chọn nhóm thuộc tính');", True)
            Exit Sub
        End If
        If TxtFeatureCode.Value.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", "alert('Bạn phải chọn mã thuộc tính');", True)
            Exit Sub
        End If

        sValueOld = TxtOldValues.Value.Trim
        If sValueOld.Trim = "" Then sValueOld = ",,"
        sArr = sValueOld.Split(",")
        sCategoryNoOld = sArr(0)
        sFeatureGroupNoOld = sArr(1)
        sFeatureNoOld = sArr(2)
        With objCF
            .CategoryNo_ = TxtNo_.Text
            .FeatureGroupNo_ = CboFeatureGroup.SelectedValue
            .FeatureNo_ = TxtFeatureCode.Value.Trim
            .Name = TxtFeatureName.Text.Trim
            .Description = TxtDescription.Text.Trim
            .UnitOrMeasure = TxtUnitOfMeasure.Text.Trim
            .ShowInList = IIf(CKShowInList.Checked, 1, 0)
            .Filter = IIf(CKFilter.Checked, 1, 0)
            If TxtOrderPosition.Text.Trim = "" Then
                .PositionOrder = 0
            Else
                .PositionOrder = CInt(TxtOrderPosition.Text.Trim)
            End If
            .LinkTable = 0
            .OptionString = TxtOptionString.Text
            .LastDateModify = Date2Char(getToday())
            .UserID = Session("adminuser")
            If Not .Save(sCategoryNoOld, sFeatureGroupNoOld, sFeatureNoOld) Then Exit Sub
        End With
        FillFeature(TxtNo_.Text)
        EmptyCategoryFeatureGroupData()
        LblWarning.Text = "Save successfully!"
    End Sub

    Sub FillFeature(ByVal sMenuCategoryNo As String)
        Dim SQL As String
        SQL = " SELECT C.[Feature Group No_], F.Name [Feature Group], C.[Feature No_], C.Name, C.[Description], C.[Position Order], C.[Option String], CONVERT(bit,C.[Show In List]) [Show In List], CONVERT(bit,C.[Filter]) Filter" & _
                " FROM [Categoy Feature] C LEFT JOIN [Feature Group] F ON C.[Feature Group No_] = F.No_ " & _
                " WHERE C.[Category No_] = '" & sMenuCategoryNo & "' ORDER BY C.[Position Order] "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdCategoryFeatureList.DataSource = tbl
        grdCategoryFeatureList.DataBind()
    End Sub

    Protected Sub grdCategoryFeatureList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCategoryFeatureList.SelectedIndexChanged
        Dim row As GridViewRow = grdCategoryFeatureList.SelectedRow
        Dim sCategoryNoOld As String, sFeatureGroupNoOld As String, sFeatureNoOld As String
        sCategoryNoOld = TxtNo_.Text
        sFeatureGroupNoOld = row.Cells.Item(0).Text.Trim
        sFeatureNoOld = row.Cells.Item(2).Text.Trim
        TxtOldValues.Value = sCategoryNoOld & "," & sFeatureGroupNoOld & "," & sFeatureNoOld
        objCF.Load(sCategoryNoOld, sFeatureGroupNoOld, sFeatureNoOld)
        CboFeatureGroup.SelectedValue = objCF.FeatureGroupNo_
        TxtFeatureNo.Text = objCF.FeatureNo_
        TxtFeatureName.Text = objCF.Name
        TxtUnitOfMeasure.Text = objCF.UnitOrMeasure
        TxtDescription.Text = objCF.Description
        TxtOrderPosition.Text = objCF.PositionOrder
        TxtOptionString.Text = objCF.OptionString
        CKShowInList.Checked = IIf(objCF.ShowInList = 1, True, False)
        CKFilter.Checked = IIf(objCF.Filter = 1, True, False)
    End Sub

    Protected Sub CboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboType.SelectedIndexChanged
        Select Case CboType.SelectedValue
            Case "0"
                CboMenuGroup.DataSource = Nothing
            Case "1"
                InitComboMenu(0)
            Case "2"
                InitComboMenu(1)
        End Select

    End Sub

    Sub InitComboMenu(ByVal nMenuType As Integer)
        Dim SQL As String, sWHERE As String = ""
        Dim tbl As DataTable
        If nMenuType = 0 Then
            SQL = " SELECT No_ [DivNo], No_, Name, [Page No_], [Menu Order] [DivOrder], 0 [CatOrder], 0 [GrOrder] FROM [Good Menu] WHERE [Menu Type] = 0 " & _
                    " ORDER BY 4 DESC,5,6,7"
        Else
            SQL = " SELECT No_ [DivNo], No_, Name, [Page No_], [Menu Order] [DivOrder], 0 [CatOrder], 0 [GrOrder] FROM [Good Menu] WHERE [Menu Type] = 0 " & _
                " UNION ALL " & _
                " SELECT C.[Parent No_] [DivNo] , C.No_,'___' + C.Name, C.[Page No_], D.[Menu Order] [DivOrder], C.[Menu Order] [CatOrder], 0 [GrOrder] " & _
                " FROM [Good Menu] C LEFT JOIN [Good Menu] D ON C.[Parent No_] = D.No_ WHERE C.[Menu Type] = 1 " & _
                " ORDER BY 4 DESC,5,6,7"
        End If

        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        CboMenuGroup.DataSource = tbl
        CboMenuGroup.DataTextField = "Name"
        CboMenuGroup.DataValueField = "No_"
        CboMenuGroup.DataBind()

    End Sub

    Protected Sub CmdSaveSearchPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSaveSearchPrice.Click
        Dim objSBP As New adv.Business.SearchByPrice
        With objSBP
            If TxtLineNo.Value = "" Then
                .LineNo_ = 0
            Else
                .LineNo_ = CInt(TxtLineNo.Value)
            End If
            .CategoryNo_ = TxtNo_.Text
            .Descriptions = TxtPriceDescription.Text
            .FromAmount = Val(TxtFromAmount.Text)
            .ToAmount = Val(TxtToAmount.Text)
            If TxtOrder.Text = "" Then
                .OrderPosition = 0
            Else
                .OrderPosition = CInt(TxtOrder.Text)
            End If
            If Not .Save Then Exit Sub
        End With
        EmptySearchPrice()
        LoadSearchByPrice(TxtNo_.Text)
    End Sub

    Sub EmptySearchPrice()
        TxtLineNo.Value = ""
        TxtPriceDescription.Text = ""
        TxtFromAmount.Text = ""
        TxtToAmount.Text = ""
        TxtOrder.Text = ""
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim sLineNo As String
        sLineNo = GridView1.Rows(e.RowIndex).Cells.Item(0).Text()
        Dim SQL As String = " DELETE FROM [Search Product By Price] WHERE [Category No_] = '" & TxtNo_.Text & "' AND [Line No_] = " & sLineNo
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadSearchByPrice(TxtNo_.Text.Trim)
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow
        TxtLineNo.Value = row.Cells.Item(0).Text.Trim
        TxtPriceDescription.Text = row.Cells.Item(1).Text.Trim
        TxtFromAmount.Text = row.Cells.Item(2).Text.Trim
        TxtToAmount.Text = row.Cells.Item(3).Text.Trim
        TxtOrder.Text = row.Cells.Item(4).Text.Trim

    End Sub

    Protected Sub grdCategoryFeatureList_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCategoryFeatureList.RowDeleting
        Dim sFeatureNo As String
        sFeatureNo = grdCategoryFeatureList.Rows(e.RowIndex).Cells.Item(2).Text()
        Dim SQL As String
        SQL = " DELETE FROM [Categoy Feature] WHERE [Category No_] = '" & TxtNo_.Text.Trim & "' AND [Feature No_] = '" & sFeatureNo.Trim & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        SQL = " DELETE FROM [Item Features] WHERE [Feature No_] = '" & sFeatureNo.Trim & "' AND [Item No_] IN (SELECT No_ FROM Item WHERE [Menu Category No_] = '" & TxtNo_.Text.Trim & "' ) "
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)

        FillFeature(TxtNo_.Text.Trim)
    End Sub

    Protected Sub cmdList_Click(sender As Object, e As EventArgs) Handles cmdList.Click
        Response.Redirect("MenuGroupList.aspx")
    End Sub
End Class
