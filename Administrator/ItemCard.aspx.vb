Imports System.Data
Imports System.Drawing
Imports System.Drawing.Image

Partial Class Administrator_ItemCard
    Inherits System.Web.UI.Page
    Dim objGMn As New adv.Business.MenuItem
    Dim objItem As New adv.Business.Item
    Dim objSalesPrice As New adv.Business.SalesPrice
    Dim objIC As New adv.Business.ItemColor
    Dim sItemNo As String = ""
    Public sItemName As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ReturnIfNull(Session("adminuser"), "").ToString.Trim = "" Then
            Response.Redirect("Login.aspx")
        End If
        If Not IsPostBack Then
            Dim Lbl As Label = CType(Master.FindControl("LblMasterMenu"), Label)
            Lbl.Text = "<a href=""ItemList.aspx"">Danh sách </a>"
            TxtPageNo.Value = ReturnIfNull(Request("pageno"), "").ToString.Trim
            sItemNo = ReturnIfNull(Request.QueryString("item"), "")
            InitCombo()
            If sItemNo.Trim <> "" Then
                ShowData(sItemNo)
            End If

            Dim LblW As Label = CType(Master.FindControl("LblMasterMenuDescription"), Label)
            LblW.Text = TxtName.Text
        End If
    End Sub

    Sub ShowData(ByVal sItemNo As String)
        objItem.Load(sItemNo)
        With objItem
            CKPublished.Checked = IIf(.Published = 1, True, False)
            TxtNo_.Text = .No_
            TxtItemNo.Value = .No_
            TxtName.Text = .Name
            sItemName = .Name
            TxtModel.Text = .Model
            CboDivision.SelectedValue = .DivisionNo_
            CboDivision_SelectedIndexChanged(CboDivision, Nothing)
            CboCategory.SelectedValue = .CategoryNo_
            CboCategory_SelectedIndexChanged(CboCategory, Nothing)
            CboProductGroup.SelectedValue = .ProducGroupNo_
            TxtOrderPosition.Text = .OrderPosition
            txtDescriptions.Value = .Descriptions
            CboTaxPercent.SelectedValue = .VATGroup
            TxtPateTitle.Text = .PageTitle
            TxtFromDate.Text = Char2Date(.FromDate)
            TxtMetaKeyword.Text = .MetaKeywords
            CboMenuGroup.SelectedValue = IIf(.MenuGroupNo_.Trim <> "", .MenuGroupNo_, .MenuCategoryNo_)
            CboBrand.SelectedValue = .BrandNo_
            CboOriginCountry.SelectedValue = .OriginCountry
            CboUnitOfMeasure.SelectedValue = .UnitOfMeasure
            CKNewProduct.Checked = IIf(.NewProduct = 1, True, False)
            CKPromotions.Checked = IIf(.PromotionsProduct = 1, True, False)
            CKBestSelling.Checked = IIf(.BestSelling = 1, True, False)
            CKHot.Checked = IIf(.HotProduct = 1, True, False)
            CKGoodGoingOn.Checked = IIf(.GoodGoingOn = 1, True, False)
            CKHideFeature.Checked = IIf(.HideFeature = 1, True, False)
            CKNotInStock.Checked = IIf(.NotInStock = 1, True, False)
            TxtStock.Text = .Stock
            TxtLink.Text = .LinkUrl
            TxtVolume.Text = .Volume
            txtSalesPrice.Text = FormatCurrency(.SalesPrice, 0)
            imgFull.ImageUrl = GetUrl() & "Images/Product/" & .ImagesURL
            imgSmall.ImageUrl = GetUrl() & "Images/Product/" & .ImagesThumURL
            FileNameImgFull.Value = .ImagesURL
            FileNameImgSmall.Value = .ImagesThumURL
            ckIsRun.Checked = IIf(.IsRun = 1, True, False)
            FillPrice()
            FillMM()
            LoadFeature(sItemNo)
            LoadContent(sItemNo)
            LoadImgDetail(sItemNo)
            LoadItemColor(sItemNo)
        End With
    End Sub

    Sub InitCombo()
        BuildCombo(CboDivision, adv.Business.List.Division, , True, "--")
        BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, , True, "--")
        BuildCombo(CboBrand, adv.Business.List.Brand)
        BuildCombo(CboTaxPercent, adv.Business.List.VATGroup, , True)
        BuildCombo(CboStoreGroup, adv.Business.List.Province, , False)
        BuildCombo(CboSalesPriceNo, adv.Business.List.SalesPriceType, , False)
        Dim sConditions As String = ""
        If TxtNo_.Text.Trim <> "" Then
            sConditions = " No_ IN (SELECT [Unit Of Measure] FROM Item WHERE No_ = '" & TxtNo_.Text & "')"
        End If
        BuildCombo(CboUnitOfMeasure, adv.Business.List.UnitOfMeasure, , False)
        BuildCombo(CboSalesUnitOfMeasure, adv.Business.List.UnitOfMeasure, sConditions, False)
        BuildCombo(CboMMStoreGroup, adv.Business.List.StoreGroup, , False)
        BuildCombo(CboMMSalesPriceGroup, adv.Business.List.SalesPriceType, , False)

    End Sub

    Protected Sub CboDivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboDivision.SelectedIndexChanged
        If ReturnIfNull(CboDivision.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim = "" Then Exit Sub
        BuildCombo(CboCategory, adv.Business.List.ItemCategory, " [Division No_] = '" & CboDivision.SelectedValue.Trim & "'", True)
    End Sub

    Protected Sub CboCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCategory.SelectedIndexChanged
        If ReturnIfNull(CboCategory.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim = "" Then Exit Sub
        BuildCombo(CboProductGroup, adv.Business.List.ProductGroup, " [Category No_] = '" & CboCategory.SelectedValue.Trim & "'", True)
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim Script As String = ""
        If TxtItemNo.Value.Trim <> TxtNo_.Text.Trim Then
            If objItem.Load(TxtNo_.Text.Trim) Then
                LblWarning1.Text = "alert('Mã sản phẩm đã tồn tại. Bạn nhập mã khác')"
                Exit Sub
            End If
        End If

        If CboMenuGroup.SelectedValue.Trim = "" Then
            LblWarning1.Text = "alert('Bạn phải chọn nhóm menu.')"
            Exit Sub
        End If

        With objItem
            .Load(TxtNo_.Text.Trim)
            .No_ = TxtNo_.Text
            .Name = TxtName.Text
            .Model = TxtModel.Text
            .BrandNo_ = CboBrand.SelectedValue
            .DivisionNo_ = CboDivision.SelectedValue
            .CategoryNo_ = CboCategory.SelectedValue
            .ProducGroupNo_ = CboProductGroup.SelectedValue
            objGMn.Load(CboMenuGroup.SelectedValue)
            If objGMn.MenuType = 1 Then
                .MenuDivisionNo_ = objGMn.ParentNo_
                .MenuCategoryNo_ = objGMn.No_
                .MenuGroupNo_ = ""
            End If
            If objGMn.MenuType = 2 Then
                .MenuCategoryNo_ = objGMn.ParentNo_
                .MenuGroupNo_ = objGMn.No_
                objGMn.Load(.MenuCategoryNo_)
                .MenuDivisionNo_ = objGMn.ParentNo_
            End If
            If objGMn.MenuType = 0 Then
                .MenuDivisionNo_ = objGMn.No_
                .MenuCategoryNo_ = ""
                .MenuGroupNo_ = ""
            End If
            .PromotionsProduct = IIf(CKPromotions.Checked, 1, 0)
            .UnitOfMeasure = CboUnitOfMeasure.SelectedValue
            .BestSelling = IIf(CKBestSelling.Checked, 1, 0)
            .NewProduct = IIf(CKNewProduct.Checked, 1, 0)
            .HideFeature = IIf(CKHideFeature.Checked, 1, 0)
            .NotInStock = IIf(CKNotInStock.Checked, 1, 0)
            .GoodGoingOn = IIf(CKGoodGoingOn.Checked, 1, 0)
            .VATPercent = GetVATPercent(CboTaxPercent.SelectedValue)
            .Descriptions = txtDescriptions.Value
            .Published = IIf(CKPublished.Checked, 1, 0)
            .SalesPrice = Val(txtSalesPrice.Text)
            .PriceIclVAT = 1
            .PageTitle = TxtPateTitle.Text
            .MetaKeywords = TxtMetaKeyword.Text
            .LastDateModify = getToday()
            .IsRun = IIf(ckIsRun.Checked, 1, 0)
            .UserID = Session("adminuser")
            .Stock = Val(TxtStock.Text)
            .OriginCountry = CboOriginCountry.SelectedValue
            .OrderPosition = Val(TxtOrderPosition.Text)
            .HotProduct = IIf(CKHot.Checked, 1, 0)
            .VATGroup = CboTaxPercent.SelectedValue
            .FromDate = Date2Char(TxtFromDate.Text)
            .ItemSameModel = ""
            .Volume = Val(TxtVolume.Text.Replace(",", ""))
            .LinkUrl = IIf(TxtLink.Text.Trim = "", ConvertIntoNone(TxtName.Text), TxtLink.Text.Trim)
            Try
                Dim fileExt As String
                Dim sImgFileName As String = ""
                Dim sImgThumbFileName As String
                fileExt = System.IO.Path.GetExtension(FileUploadFullImg.FileName)
                Dim sFolderName As String = Server.MapPath("../")
                sFolderName &= "Images\Product\"
                If FileUploadFullImg.HasFile Then
                    If fileExt.ToUpper.Trim = ".GIF" Or fileExt.ToUpper.Trim = ".JPG" Or fileExt.ToUpper.Trim = ".PNG" Then
                        sImgFileName = sFolderName & TxtNo_.Text & fileExt
                        FileUploadFullImg.SaveAs(sImgFileName)
                        .ImagesURL = TxtNo_.Text & fileExt
                        If CKCreateThumbAuto.Checked And Not FileUploadSmallImg.HasFile Then
                            sImgThumbFileName = sFolderName & TxtNo_.Text & "_T" & fileExt
                            generateThumbnail(sImgFileName, sImgThumbFileName)
                            .ImagesThumURL = TxtNo_.Text & "_T" & fileExt
                        End If
                    End If
                End If

                If FileUploadSmallImg.HasFile Then
                    fileExt = System.IO.Path.GetExtension(FileUploadSmallImg.FileName)
                    sImgThumbFileName = sFolderName & TxtNo_.Text & "_T" & fileExt
                    If fileExt.ToUpper.Trim = ".GIF" Or fileExt.ToUpper.Trim = ".JPG" Or fileExt.ToUpper.Trim = ".PNG" Then
                        FileUploadSmallImg.SaveAs(sImgThumbFileName)
                    End If
                    .ImagesThumURL = TxtNo_.Text & "_T" & fileExt
                End If
                .Save()
                imgFull.ImageUrl = GetUrl() & "Images/Product/" & .ImagesURL
                imgSmall.ImageUrl = GetUrl() & "Images/Product/" & .ImagesThumURL
                LblWarning1.Text = "Cập nhật thành công!"
                'AddFeatureItem(TxtNo_.Text)
                LoadFeature(TxtNo_.Text)
            Catch ex As Exception
                LblWarning1.Text = ex.Message
            End Try
        End With
    End Sub

    Function generateThumbnail(ByVal sFileName As String, ByVal sNewFileName As String) As Boolean
        'Create a new Bitmap Image loading from location of origional file
        Dim bm As Bitmap = System.Drawing.Image.FromFile(sFileName)


        'Declare Thumbnails Height and Width
        Dim newWidth As Integer = 160
        Dim newHeight As Integer = (newWidth / bm.Width) * bm.Height


        'Create the new image as a blank bitmap
        Dim resized As Bitmap = New Bitmap(newWidth, newHeight)


        'Create a new graphics object with the contents of the origional image
        Dim g As Graphics = Graphics.FromImage(resized)


        'Resize graphics object to fit onto the resized image
        g.DrawImage(bm, New Rectangle(0, 0, resized.Width, resized.Height), 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel)


        'Get rid of the evidence
        g.Dispose()
        bm.Dispose()

        'Save the new image to the same folder as the origional
        resized.Save(sNewFileName)
        Return True

    End Function

    Function GetVATPercent(ByVal sVATCode As String) As Integer
        Dim SQL As String
        SQL = "SELECT [VAT Percent] FROM [VAT Group] WHERE No_ = '" & sVATCode & "' "
        Return CInt(ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, Data.CommandType.Text, SQL), 0))
    End Function

    Sub FillPrice()
        Dim SQL As String
        SQL = "SELECT sp.[Line No_], sp.[Store Group],sp.[Sales Price No_],sp.[Unit Of Measure No_], dbo.Char2Date(sp.[Starting Date]) [Starting Date], " & _
                " dbo.Char2Date(sp.[Ending Date]) [Ending Date],sp.[Unit Price],sp.[Deal Price], pr.Name ProName FROM [Sales Price] sp inner join Province pr on sp.[Store Group] = pr.No_ " & _
                " WHERE [Line Type] = 0 AND [Item No_] = '" & TxtNo_.Text.Trim & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdSalesPrice.DataSource = tbl
        grdSalesPrice.DataBind()
        'CboSalesUnitOfMeasure.SelectedValue = CboUnitOfMeasure.SelectedValue
        'CboSalesUnitOfMeasure.Enabled = False
    End Sub

    Sub FillMM()
        Dim SQL As String
        SQL = "SELECT D.[Discount No_], D.[Line No_], D.[Item No_], I.Name [Item Name], D.[Store Group], D.[Price Group], dbo.Char2Date(D.[Starting Date]) [Starting Date]," & _
                " dbo.Char2Date(D.[Ending Date]) [Ending Date], D.[Origin Price], D.[Deal Price], [Discount Type] = CASE D.[Disc_ Type] WHEN 0 THEN 'Deal Price' WHEN 1 THEN 'Discount %' END, " & _
                " CONVERT(bit, [Trigger Discount]) [Is Trigger] " & _
                " FROM [Discount Line] D LEFT JOIN Item I ON D.[Item No_] = I.No_ " & _
                " WHERE D.[Discount No_] IN (SELECT [Discount No_] FROM [Discount Line] WHERE [Item No_] = '" & TxtNo_.Text.Trim & "')" & _
                " ORDER BY 1, 2"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdMMSalesPrice.DataSource = tbl
        grdMMSalesPrice.DataBind()
        TxtItemNoMM.Text = TxtNo_.Text
    End Sub

    Protected Sub grdSalesPrice_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdSalesPrice.RowDeleting
        Dim SQL As String
        Try
            SQL = " DELETE FROM [Sales Price] WHERE [Item No_] = '" & TxtNo_.Text.Trim & "'" & _
                    " AND [Line No_] = " & grdSalesPrice.Rows(e.RowIndex).Cells.Item(0).Text & " "

            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            FillPrice()
            LblWarning.Text = "DELETE Sucessfuly"
            EmptySalesPrice()
        Catch ex As Exception
            LblWarning.Text = ex.Message
        End Try
    End Sub

    Sub EmptySalesPrice()
        TxtStartingDate.Text = ""
        TxtEndingDate.Text = ""
        TxtUnitPrice.Text = ""
        TxtAction.Value = "E"
        TxtLineNo.Value = 0
        TxtDealPrice.Text = 0
    End Sub


    Protected Sub grdSalesPrice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSalesPrice.SelectedIndexChanged
        Try
            Dim row As GridViewRow = grdSalesPrice.SelectedRow
            objSalesPrice.Load(TxtNo_.Text, row.Cells.Item(0).Text.Trim)
            CboStoreGroup.SelectedValue = objSalesPrice.StoreGroup
            TxtStartingDate.Text = Char2Date(objSalesPrice.StartingDate)
            TxtEndingDate.Text = Char2Date(objSalesPrice.EndingDate)
            TxtUnitPrice.Text = objSalesPrice.UnitPrice
            TxtLineNo.Value = row.Cells.Item(0).Text.Trim
            TxtDealPrice.Text = objSalesPrice.DealPrice

        Catch ex As Exception
            LblWarning.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmdSavePrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSavePrice.Click
        With objSalesPrice
            .LineType = 0
            .ItemNo_ = TxtNo_.Text
            .StoreGroup = CboStoreGroup.SelectedValue
            .SalesPriceNo_ = CboSalesPriceNo.SelectedValue
            .UnitOfMeasureNo_ = CboSalesUnitOfMeasure.SelectedValue
            .StartingDate = Date2Char(TxtStartingDate.Text)
            .EndingDate = Date2Char(TxtEndingDate.Text)
            .PriceIncVAT = 1
            .UnitPrice = Val(TxtUnitPrice.Text.Replace(",", ""))
            .OriginPrice = .UnitPrice
            If TxtDealPrice.Text.Trim = "" Then
                .DealPrice = .UnitPrice
            Else
                .DealPrice = Val(TxtDealPrice.Text.Replace(",", ""))
            End If
            .GiftDescription = ""
            .LineNo = CInt(IIf(TxtLineNo.Value.Trim = "", 0, TxtLineNo.Value))
            .Save()
        End With
        FillPrice()
        EmptySalesPrice()

    End Sub

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        grdSalesPrice.SelectedIndex = -1
        TxtStartingDate.Text = ""
        TxtEndingDate.Text = ""
        TxtUnitPrice.Text = ""
        LblWarning.Text = ""
    End Sub

    Function CheckIvalidMM() As Boolean
        If CboLineType.SelectedValue = 1 Then
            If TxtMMPeriodicNo.Text.Trim = "" Then
                LblMMWarning.Text = "Bạn phải nhập mã chương trình"
                Return False
            End If
            If TxtItemNoMM.Text.Trim = "" Then
                LblMMWarning.Text = "Bạn phải nhập mã sản phẩm"
                Return False
            End If
        End If
        Return True
    End Function

    Protected Sub cmdSaveMM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveMM.Click
        Try
            Dim objDiscountLine As New adv.Business.DiscountLine
            Dim sNo As String
            If Not CheckIvalidMM() Then Exit Sub
            If TxtMMPeriodicNo.Text.Trim = "" Then
                Dim sYM As String = ""
                sYM = sLeft(Date2Char(getToday()), 6)
                Dim objSeriNo As New adv.Business.cNoSeri
                objSeriNo.Load("PROCONTNO ")
                sNo = objSeriNo.CreateNoSeri(sYM)
            Else
                sNo = TxtMMPeriodicNo.Text.Trim
            End If


            With objDiscountLine
                .DiscountNo_ = sNo
                .LineNo_ = CInt(IIf(TxtMMLineNo.Value.Trim = "", 0, TxtMMLineNo.Value))
                .ItemNo_ = TxtItemNoMM.Text
                .StoreGroup = CboMMStoreGroup.SelectedValue
                .PriceGroup = CboMMSalesPriceGroup.SelectedValue
                .StartingDate = Date2Char(TxtMMStartingDate.Text)
                .EndingDate = Date2Char(TxtMMEndingDate.Text)
                .Disc_Type = CInt(CboDiscountType.SelectedValue)
                .Description = ""
                .TriggerDiscount = IIf(CboLineType.SelectedValue = "0", 1, 0)
                .Disc_Type = CboDiscountType.SelectedValue
                .OriginPrice = Val(TxtMMOriginPrice.Text.Replace(",", ""))
                .DealPriceValue = Val(TxtMMSalesPrice.Text.Replace(",", ""))
                .Quantity = 1
                .Save()
            End With
            EmptyMM()
            FillMM()
        Catch ex As Exception
            LblMMWarning.Text = ex.Message
        End Try
    End Sub

    Sub LoadFeature(ByVal sItemNo As String)
        Dim SQL As String
        objItem.Load(sItemNo)
        SQL = " SELECT F.[Feature Group No_], F.[Feature No_], F.[Feature Name], F.[Feature Value], F.[Search Value], C.[Option String], C.[Position Order], " & _
                " C.[Unit Of Measure] " & _
                " FROM [Item Features] F " & _
                " LEFT JOIN [Categoy Feature] C ON F.[Feature Group No_] = C.[Feature Group No_] AND F.[Feature No_] = C.[Feature No_] AND C.[Category No_] = '" & objItem.MenuCategoryNo_ & "' " & _
                " WHERE [Item No_]  = '" & sItemNo & "' " & _
                " UNION ALL " & _
                " SELECT [Feature Group No_], [Feature No_], Name, '', '', [Option String], [Position Order], [Unit Of Measure]  FROM [Categoy Feature] " & _
                " WHERE [Category No_] = '" & objItem.MenuCategoryNo_ & "' AND [Feature Group No_] + [Feature No_] NOT IN " & _
                " (SELECT [Feature Group No_] + [Feature No_] FROM [Item Features] WHERE [Item No_]  = '" & sItemNo & "')" & _
                " ORDER BY C.[Position Order]"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        ' put Future when not exits

        Dim nIJ As Integer
        Dim sHtml As String = ""
        sHtml = "<table width=""100%"" class=""adminlist"">" & vbCrLf
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr class=""row" & nIJ Mod 2 & """>" & vbCrLf
            sHtml &= "<td>" & vbCrLf
            sHtml &= tbl.Rows(nIJ).Item("Feature Name")
            If tbl.Rows(nIJ).Item("Unit Of Measure").ToString.Trim <> "" Then
                sHtml &= "&nbsp; <b>(" & tbl.Rows(nIJ).Item("Unit Of Measure").ToString.Trim & ")</b>" & vbCrLf
            Else
                sHtml &= vbCrLf
            End If
            sHtml &= "</td>" & vbCrLf
            sHtml &= "<td>" & vbCrLf
            If tbl.Rows(nIJ).Item("Option String").ToString.Trim <> "" Then
                sHtml &= CreateCboFromOptionString(tbl.Rows(nIJ).Item("Feature Group No_"), tbl.Rows(nIJ).Item("Feature No_"), tbl.Rows(nIJ).Item("Option String"), tbl.Rows(nIJ).Item("Feature Value"))
            Else
                sHtml &= "<textarea name=""V" & tbl.Rows(nIJ).Item("Feature Group No_") & "-" & tbl.Rows(nIJ).Item("Feature No_") & """ rows=""3"" cols=""40"" class=""text_area"">" & vbCrLf
                sHtml &= tbl.Rows(nIJ).Item("Feature Value") & vbCrLf
                sHtml &= "</textarea>"
            End If
            sHtml &= "</td>" & vbCrLf
            sHtml &= "<td>" & vbCrLf
            If tbl.Rows(nIJ).Item("Option String").ToString.Trim <> "" Then

            Else
                sHtml &= "<textarea name=""S" & tbl.Rows(nIJ).Item("Feature Group No_") & "-" & tbl.Rows(nIJ).Item("Feature No_") & """ rows=""3"" cols=""40"" class=""text_area"">" & vbCrLf
                sHtml &= tbl.Rows(nIJ).Item("Search Value") & vbCrLf
                sHtml &= "</textarea>"
            End If
            sHtml &= "</td>" & vbCrLf
            sHtml &= "</ tr>" & vbCrLf
        Next
        sHtml &= "</table>" & vbCrLf
        LblFeature.Text = sHtml
    End Sub

    Protected Sub cmdSaveFeature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveFeature.Click

        Dim SQL As String
        Dim sWarning As String = ""
        Dim sItemNo As String = TxtNo_.Text.Trim
        Dim sFeatureGroupNo As String = ""
        Dim sFeatureNo As String = ""
        Dim sFeatureValue As String = ""
        Dim sSearchValue As String = ""
        objItem.Load(sItemNo)
        SQL = " SELECT F.[Feature Group No_], F.[Feature No_], F.[Feature Name], F.[Feature Value], F.[Search Value], C.[Option String], C.[Position Order] " & _
                " FROM [Item Features] F " & _
                " LEFT JOIN [Categoy Feature] C ON F.[Feature Group No_] = C.[Feature Group No_] AND F.[Feature No_] = C.[Feature No_] AND C.[Category No_] = '" & objItem.MenuCategoryNo_ & "' " & _
                " WHERE [Item No_]  = '" & sItemNo & "' " & _
                " UNION ALL " & _
                " SELECT [Feature Group No_], [Feature No_], Name, '', '', [Option String], [Position Order]  FROM [Categoy Feature] " & _
                " WHERE [Category No_] = '" & objItem.MenuCategoryNo_ & "' AND [Feature Group No_] + [Feature No_] NOT IN " & _
                " (SELECT [Feature Group No_] + [Feature No_] FROM [Item Features] WHERE [Item No_]  = '" & sItemNo & "')" & _
                " ORDER BY C.[Position Order]"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        sItemName = objItem.Name
        For nIJ = 0 To tbl.Rows.Count - 1
            sFeatureGroupNo = tbl.Rows(nIJ).Item("Feature Group No_")
            sFeatureNo = tbl.Rows(nIJ).Item("Feature No_")
            sFeatureValue = Request("V" & tbl.Rows(nIJ).Item("Feature Group No_").ToString.Trim & "-" & tbl.Rows(nIJ).Item("Feature No_").ToString.Trim)
            sSearchValue = ReturnIfNull(Request("S" & tbl.Rows(nIJ).Item("Feature Group No_").ToString.Trim & "-" & tbl.Rows(nIJ).Item("Feature No_").ToString.Trim), "")
            If sSearchValue.Trim = "" Then
                sSearchValue = sFeatureValue
            End If
            If Not UpdateFeatureItem(sItemNo, sFeatureGroupNo, sFeatureNo, sFeatureValue.Trim, sSearchValue.Trim) Then Exit Sub
        Next
        LoadFeature(sItemNo)
        LblWarningFeature.Text = "Update feature is successfully."
    End Sub

    Function UpdateFeatureItem(ByVal sItemNo As String, ByVal sFeatureGroupNo As String, ByVal sFeatureNo As String, ByVal sValue As String, ByVal sSearchValue As String) As Boolean
        Try
            Dim SQL As String, tbl As DataTable, sFeatureName As String = "", sDescriptions As String = ""
            objItem.Load(sItemNo)
            SQL = " SELECT Name, [Description] FROM [Categoy Feature] WHERE [Category No_] = '" & objItem.MenuCategoryNo_ & "' AND [Feature Group No_] = '" & sFeatureGroupNo & "' AND [Feature No_] = '" & sFeatureNo & "'"
            tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
            sFeatureName = tbl.Rows(0).Item("Name")
            sDescriptions = tbl.Rows(0).Item("Description")
            SQL = " DELETE FROM [Item Features] WHERE [Item No_] = '" & sItemNo & "' AND [Feature Group No_] = '" & sFeatureGroupNo & "' AND [Feature No_] = '" & sFeatureNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            SQL = "  INSERT INTO [Item Features] ([Item No_],[Feature Group No_],[Feature No_],[Feature Name],[Descriptions],[Feature Value],[Language No_],[Search Value])" & _
                    " VALUES ('" & sItemNo & "','" & sFeatureGroupNo & "','" & sFeatureNo & "',N'" & sFeatureName & "',N'" & sDescriptions & "',N'" & sValue & "','VIET',N'" & sSearchValue & "')"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            Return True
        Catch ex As Exception
            LblWarningFeature.Text = ex.Message
            Return False
        End Try
    End Function
    Function AddFeatureItem(ByVal sItemNo As String) As Boolean
        Try
            Dim SQL As String, tbl As DataTable, sFeatureName As String = "", sDescriptions As String = ""
            objItem.Load(sItemNo)

            Dim tblItemFeatures As DataTable
            SQL = " select * from [Item Features] WHERE [Item No_] = '" & objItem.No_ & "'"
            tblItemFeatures = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
            If tblItemFeatures.Rows.Count = 0 Then
                SQL = " SELECT Name, [Description] FROM [Categoy Feature] WHERE [Category No_] = '" & objItem.MenuCategoryNo_ & "'"
                tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

                sFeatureName = tbl.Rows(0).Item("Name")
                sDescriptions = tbl.Rows(0).Item("Description")
                'SQL = " DELETE FROM [Item Features] WHERE [Item No_] = '" & sItemNo & "' AND [Feature Group No_] = '" & sFeatureGroupNo & "' AND [Feature No_] = '" & sFeatureNo & "'"
                'DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
                For nij = 0 To tbl.Rows.Count - 1
                    SQL &= "  INSERT INTO [Item Features] ([Item No_],[Feature Group No_],[Feature No_],[Feature Name],[Descriptions],[Feature Value],[Language No_],[Search Value])" & _
                        " VALUES ('" & sItemNo & "','" & tbl.Rows(nij).Item("Feature Group No_") & "','" & tbl.Rows(nij).Item("Feature No_") & "',N'" & tbl.Rows(nij).Item("Name") & "',N'" & tbl.Rows(nij).Item("Description") & "',N'" & String.Empty & "','VIET',N'" & String.Empty & "')"
                Next

                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            End If
            Return True
        Catch ex As Exception
            LblWarningFeature.Text = ex.Message
            Return False
        End Try
    End Function

    Function CreateCboFromOptionString(ByVal sFeatureGroupNo As String, ByVal sFeatureNo As String, ByVal sOptionString As String, ByVal sValueSelect As String) As String
        Dim sHtml As String = ""
        Dim sArr As Object
        Dim sA As String() = New String(4) {}

        sArr = sOptionString.Split(",")
        Dim nIJ As Integer
        sHtml = "<select name=""V" & sFeatureGroupNo & "-" & sFeatureNo & """ style=""width: 300px;"" class=""inputbox"">" & vbCrLf
        For nIJ = 0 To sArr.Length - 1
            sHtml &= "<option value=""" & sArr(nIJ) & """ "
            If sValueSelect.ToUpper.Trim = sArr(nIJ).ToString.Trim.ToUpper Then
                sHtml &= " selected=""selected"" "
            End If
            sHtml &= ">"
            sHtml &= sArr(nIJ) & "</option>" & vbCrLf
        Next
        sHtml &= "</select>" & vbCrLf
        Return sHtml
    End Function

    Protected Sub grdMMSalesPrice_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMMSalesPrice.RowDeleting
        Dim SQL As String
        Dim row As GridViewRow = grdMMSalesPrice.Rows(e.RowIndex)
        Dim objDiscountLine As New adv.Business.DiscountLine
        objDiscountLine.Load(row.Cells.Item(0).Text, row.Cells.Item(1).Text)
        If objDiscountLine.TriggerDiscount = 1 Then
            SQL = " DELETE FROM [Discount Line] WHERE [Discount No_] = '" & row.Cells.Item(0).Text & "'" & _
                       " AND [Trigger Discount] = 0 "
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            SQL = " DELETE FROM [Discount Line] WHERE [Discount No_] = '" & row.Cells.Item(0).Text & "'" & _
                       " AND [Line No_] = " & row.Cells.Item(1).Text & ""
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        Else
            SQL = " DELETE FROM [Discount Line] WHERE [Discount No_] = '" & row.Cells.Item(0).Text & "'" & _
                       " AND [Line No_] = " & row.Cells.Item(1).Text & ""
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        End If


        LblMMWarning.Text = "Deleting is sucessful."
        'LblMMWarning.Text = SQL
        FillMM()
    End Sub

    Protected Sub grdMMSalesPrice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMMSalesPrice.SelectedIndexChanged
        Dim row As GridViewRow = grdMMSalesPrice.SelectedRow
        Dim iLineNo As Integer
        iLineNo = row.Cells.Item(0).Text
        objSalesPrice.Load(TxtNo_.Text, iLineNo)
        With objSalesPrice
            TxtMMPeriodicNo.Text = .PeriodicDiscountNo_
            CboMMStoreGroup.SelectedValue = .StoreGroup
            CboMMSalesPriceGroup.SelectedValue = .SalesPriceNo_
            TxtMMStartingDate.Text = Char2Date(.StartingDate)
            TxtMMEndingDate.Text = Char2Date(.EndingDate)
            TxtMMOriginPrice.Text = .OriginPrice
            TxtMMSalesPrice.Text = .DealPrice

        End With
        TxtMMLineNo.Value = iLineNo
    End Sub

    Sub EmptyMM()
        grdMMSalesPrice.SelectedIndex = -1
        TxtMMPeriodicNo.Text = ""
        TxtMMStartingDate.Text = ""
        TxtMMEndingDate.Text = ""
        TxtMMOriginPrice.Text = "0"
        TxtMMSalesPrice.Text = "0"
        TxtMMLineNo.Value = 0
    End Sub

    Protected Sub cmdNewMM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNewMM.Click
        EmptyMM()
        LblMMWarning.Text = ""
    End Sub

    Protected Sub cmdSaveContent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveContent.Click
        Dim objContent As New adv.Business.ItemDescriptions
        With objContent
            .Content = TxtContent.Value
            .ItemNo_ = TxtNo_.Text.Trim
            .DateModify = Date2Char(getToday)
            .UserID = Session("adminuser")
            .Save()
        End With
    End Sub

    Sub LoadContent(ByVal sItemNo As String)
        Dim objContent As New adv.Business.ItemDescriptions
        If objContent.Load(sItemNo) Then
            TxtContent.Value = objContent.Content
        End If
    End Sub

    Sub LoadImgDetail(ByVal sItemNo As String)
        Dim SQL As String
        Dim sHtml As String = "", sUrl As String = GetUrl()
        Dim tDt As DataTable
        SQL = " SELECT [Line No_], [Image Natural] Name, 'Images/ProductImages/' + [Image Natural] ImgUrl, 'Images/ProductImages/' + [Image Thumbs] ImgUrlThumb FROM [Item Images] WHERE [Item No_] = '" & sItemNo & "'"
        tDt = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        For nIJ = 0 To tDt.Rows.Count - 1
            sHtml &= "<div class=""ImgDetail"">" & vbCrLf
            sHtml &= "<div>" & vbCrLf
            sHtml &= "<img src=""" & sUrl & tDt.Rows(nIJ).Item("ImgUrlThumb") & """ alt="""" width=""120""/>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "<div>" & vbCrLf
            sHtml &= "<input id=""ckImg"" name=""ImgDetail"" value=""" & tDt.Rows(nIJ).Item("Line No_") & """ type=""checkbox"" />" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
        Next
        LblImg.Text = sHtml
    End Sub

    Protected Sub cmdDelImg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelImg.Click
        Dim sListLineNo As String = ""
        sListLineNo = Request("ImgDetail")
        Dim SQL As String
        SQL = " SELECT [Image Thumbs], [Image Natural] FROM [Item Images] WHERE [Item No_] = '" & TxtNo_.Text & "' AND [Line No_] IN (" & sListLineNo & ")"
        Dim Tbl As DataTable
        Tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim sFolderName As String = Server.MapPath("../")
        sFolderName &= "Images\ProductImages\"

        For nIJ = 0 To Tbl.Rows.Count - 1
            DeleteImgDetail(sFolderName & Tbl.Rows(nIJ).Item("Image Thumbs"))
            DeleteImgDetail(sFolderName & Tbl.Rows(nIJ).Item("Image Natural"))
        Next
        SQL = " DELETE FROM [Item Images] WHERE [Item No_] = '" & TxtNo_.Text & "' AND [Line No_] IN (" & sListLineNo & ")"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadImgDetail(TxtNo_.Text)
    End Sub

    Function DeleteImgDetail(ByVal sFileName As String) As Boolean
        Try
            If System.IO.File.Exists(sFileName) = True Then
                System.IO.File.Delete(sFileName)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub cmdUploadImg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadImg.Click
        Dim objImgDetail As New adv.Business.ItemImages
        Try
            If UplImagesDetail.HasFile Then
                Dim fileExt As String
                Dim sImgFileName As String = ""
                Dim sImgThumbFileName As String
                fileExt = System.IO.Path.GetExtension(UplImagesDetail.FileName)
                If fileExt = ".gif" Or fileExt = ".jpg" Or fileExt = ".png" Then
                    objImgDetail.CreateNew(TxtNo_.Text)
                    Dim sFolderName As String = Server.MapPath("../")
                    sFolderName &= "Images\ProductImages\"
                    objImgDetail.ImageNatural &= fileExt
                    objImgDetail.ImageThumbs &= fileExt
                    sImgFileName = sFolderName & objImgDetail.ImageNatural
                    sImgThumbFileName = sFolderName & objImgDetail.ImageThumbs
                    LblWarningUploadImg.Text &= sImgFileName
                    UplImagesDetail.SaveAs(sImgFileName)
                    generateThumbnail(sImgFileName, sImgThumbFileName)
                    objImgDetail.Save()
                    LoadImgDetail(TxtNo_.Text)
                End If
                LblWarningUploadImg.Text = "Cập nhật thành công!"
            Else
                LblWarningUploadImg.Text = "Không có file"
            End If
        Catch ex As Exception
            LblWarningUploadImg.Text = ex.Message
        End Try
    End Sub

    Sub LoadItemColor(ByVal sItemOrigin As String)
        Dim SQL As String
        SQL = "SELECT C.[Item Origin No_], C.[Item No_], I.Name [Item Name], C.Description, C.[Item Color], CONVERT(bit, C.Published) Published " & _
                " FROM [Item Color] C INNER JOIN Item I ON C.[Item No_] = I.No_ " & _
                " WHERE C.[Item Origin No_] = '" & sItemOrigin & "' ORDER BY C.[Line No_]"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        TxtOriginItemNo.Text = TxtNo_.Text
        If tbl.Rows.Count <> 0 Then
            GrdItemColor.DataSource = tbl
            GrdItemColor.DataBind()
        End If
    End Sub

    Protected Sub CmdAutoLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAutoLoad.Click

        objIC.AutoLoad(TxtNo_.Text)
        LoadItemColor(TxtNo_.Text)
    End Sub

    Protected Sub GrdItemColor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdItemColor.SelectedIndexChanged
        Dim row As GridViewRow = GrdItemColor.SelectedRow
        TxtOriginItemNo.Text = row.Cells.Item(0).Text
        TxtItemColorNo.Text = row.Cells.Item(1).Text
        TxtItemColorName.Text = row.Cells.Item(2).Text
        objIC.Load(row.Cells.Item(1).Text)
        TxtColor.Text = objIC.ItemColor.Trim
        ColorPickerExtender1.SelectedColor = "#" & objIC.ItemColor.Trim
        TxtItemColorDescription.Text = objIC.Description
        TxtItemColorLineNo.Value = objIC.LineNo_
        CKPublishedItemColor.Checked = IIf(objIC.Published = 1, True, False)
    End Sub

    Protected Sub CmdSaveItemColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSaveItemColor.Click
        Try
            With objIC
                .ItemOriginNo_ = TxtOriginItemNo.Text
                .ItemNo_ = TxtItemColorNo.Text
                .ItemColor = TxtColor.Text
                .Description = TxtItemColorDescription.Text
                .Published = IIf(CKPublishedItemColor.Checked, 1, 0)
                If TxtItemColorLineNo.Value = "" Then
                    .LineNo_ = 1
                Else
                    .LineNo_ = CInt(TxtItemColorLineNo.Value)
                End If
                .Save()
                LoadItemColor(.ItemOriginNo_)
            End With
            LblWarningItemColor.Text = "Update sucessfuly"
        Catch ex As Exception
            LblWarningItemColor.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmdNewItemColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNewItemColor.Click
        TxtItemColorNo.Text = ""
        TxtItemColorName.Text = ""
        TxtColor.Text = ""
        TxtItemColorDescription.Text = ""
        CKPublishedItemColor.Checked = False
    End Sub

    Protected Sub cmdAddNewItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNewItem.Click
        Response.Redirect("ItemCard.aspx")
    End Sub

    Protected Sub cmdItemList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdItemList.Click
        Response.Redirect("ItemList.aspx?pageno=" & TxtPageNo.Value)
    End Sub

    Protected Sub cmdCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCopy.Click
        If TxtNo_.Text = "" Then Exit Sub
        ModalPopupCopy.Show()
    End Sub

    Protected Sub cmdOKCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOKCopy.Click
        Dim Script As String = ""
        If TxtNewItemNo.Text = "" Then
            Script = "alert('Bạn phải nhập mã sản phẩm.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", Script, True)
            Exit Sub
        End If
        If objItem.Load(TxtNewItemNo.Text.Trim) Then
            Script = "alert('Mã sản phẩm đã tồn tại, bạn phải nhậ mã khác.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", Script, True)
            Exit Sub
        End If

        objItem.CopyNewItem(TxtNo_.Text, TxtNewItemNo.Text)
        Response.Redirect("ItemCard.aspx?item=" & TxtNewItemNo.Text)

    End Sub

    Protected Sub cmdDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        Dim Script As String = ""
        If TxtNo_.Text.Trim = "" Then Exit Sub
        If objItem.ExistInOrder(TxtNo_.Text.Trim) Then
            Script = "alert('Sản phẩm đã tồn tại trong đơn hàng. Bạn không thể xóa.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", Script, True)
            Exit Sub
        End If
        If Not objItem.DeleteItem(TxtNo_.Text) Then Exit Sub
        Response.Redirect("ItemCard.aspx")
    End Sub
End Class
