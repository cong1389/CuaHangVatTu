
Imports System.Data

Partial Class ProductDetail
    Inherits System.Web.UI.Page
    Dim objItem As New adv.Business.Item
    Dim strParam As String = ""
    Dim sHomeoneKey As String = ""
    Dim objNoSeri As New adv.Business.cNoSeri
    Dim objC As New adv.Business.Customer
    Dim objCustomerReview As New adv.Business.CustomerReview

    Private Sub DeliveryPolicy()
        Dim sNo As String = ""

        Dim sSQL As String = ""
        sSQL = " SELECT * FROM [Content] WHERE [Link Menu] = 'chinh-sach-giao-hang'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        If tbl.Rows().Count > 0 Then
            ltrHeaderDeliveryPolicy.Text = tbl.Rows(0).Item("Title")
            ltrDeliveryPolicy.Text = tbl.Rows(0).Item("Content")
        End If

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim paramCategory As String = ""
            paramCategory = Request("catdetailid")
            strParam = paramCategory
            LoadDetailProduct(paramCategory)
            LoadSelectedProducts()
            BuildCombo(ddlOtherPrice, adv.Business.List.Province, , False)
            ddlOtherPrice.Items.Insert(0, New ListItem("Vui lòng chọn", "0"))
            ddlOtherPrice.SelectedIndex = 0

            If ReturnIfNull(Session("CustomerNo"), "").ToString.Trim <> "" Then
                cmdSaveReviewLog.Visible = False
                objC.Load(Session("CustomerNo"))
                TxtEmail.Text = objC.Email
                TxtFullName.Text = objC.FullName
            End If

            DeliveryPolicy()
        End If
    End Sub

    Public Function GetMenuName(ByVal linkDispaly As String) As String
        Dim sSQL As String

        sSQL = " SELECT * FROM [Good Menu] WHERE [Link Display] = '" & linkDispaly & "'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        If tbl.Rows.Count > 0 Then
            Return tbl.Rows(0).Item("Name")
        Else
            Return ""
        End If
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function LoadOtherPrice(ByVal proid As String, ByVal itemid As String) As String

        If proid.Length = 1 Then
            proid = "00" & proid
        ElseIf proid.Length = 2 Then
            proid = "0" & proid

        End If
        Dim sSQL As String
        sSQL = String.Format(" select [Origin Price] from [Sales Price] where [Store Group] like '{0}' and  [Item No_] like '{1}'", proid, itemid)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        If tbl.Rows.Count > 0 Then
            Return Convert.ToDecimal(tbl.Rows(0).Item(0)).ToString("#,###")
        Else
            Dim sSQL1 = String.Format(" select top 3 i.Name,i.[Images Thum URL],i.[Link URL] from Item i inner join [Sales Price] s on i.No_ = s.[Item No_] where s.[Store Group] like '{0}' order by i.[Order Position]", proid)
            Dim tbl1 As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL1).Tables(0)
            Dim sHTML As String = ""
            For nik = 0 To tbl1.Rows.Count - 1
                Dim sUrl As String = GetUrl()
                sHTML &= String.Format("<li><img src='{0}'  width='60px' height='60px'/><p class='replaceProducts'><a href='{1}'>{2}</a></p></li>", Utils.CheckExitsImages(sUrl & "Images/Product/" & tbl1.Rows(nik).Item("Images Thum URL")), sUrl & "product-detail/" & tbl1.Rows(nik).Item("Link URL") & "/", Utils.DropText(tbl1.Rows(nik).Item("Name"), 30))
            Next
            Return sHTML
        End If

    End Function

    Public Shared Function LoadSaleOffPrice(ByVal itemid As String, ByVal sgroup As String) As DataTable
        Dim sToday As String = Date2Char(getToday())
        Dim sSQL As String
        sSQL = String.Format(" select * from [Sales Price] where [Item No_] like '{0}' AND [Starting Date] <= '{1}' AND ([Ending Date] = '' OR [Ending Date] > ='{2}')", itemid, sToday, sToday, sgroup)

        'sSQL = String.Format(" select * from [Sales Price] where [Store Group] like '{3}' and  [Item No_] like '{0}' AND [Starting Date] <= '{1}' AND ([Ending Date] = '' OR [Ending Date] > ='{2}')", itemid, sToday, sToday, sgroup)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Return tbl

    End Function
    Public Shared Function GetPriceNormal(ByVal itemid As String, ByVal sgroup As String) As DataTable
        Dim sToday As String = Date2Char(getToday())
        Dim sSQL As String
        sSQL = String.Format(" select * from [Sales Price] where [Store Group] like '{0}' and  [Item No_] like '{1}'", sgroup, itemid)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Return tbl

    End Function
    Public Function GetcategoryNameByLinkURL(ByVal linkDispaly As String) As DataTable
        'Dim sSQL As String
        'sSQL = " SELECT * FROM [Good Menu] WHERE [Link Display] = '" & linkDispaly & "'"
        Dim arParams() As IDataParameter = New IDataParameter(0) {}
        arParams(0) = DBHelper.createParameter("@linkURL", SqlDbType.VarChar, ParameterDirection.Input, linkDispaly)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "GetcategoryNameByLinkURL", arParams).Tables(0)
        Return tbl
    End Function

    Public Function GetProductDetail(ByVal displayUrl As String) As DataTable
        Dim sSQL As String
        sSQL = " SELECT i.*,b.Name as BrandName FROM [Item] i left join Brand b on i.[Brand No_] = b.No_ WHERE i.[Link URL] = '" & displayUrl & "'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Return tbl
    End Function

    Public Function GetContentProduct(ByVal No_ As String) As DataTable
        Dim sSQL As String
        sSQL = " SELECT * from [Item Descriptions] Where [Item No_] = '" & No_ & "'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Return tbl
    End Function

    Private Sub LoadDetailProduct(ByVal paramCategory As String)
        'Dim arrParam As Array = paramCategory.Split("/")
        Dim drw As DataTable = GetcategoryNameByLinkURL(paramCategory)
        ltrMenuName.Text = String.Format("<a href='{0}' title='{1}' rel='category tag'>{1}</a>", GetImgUrl() & "sub-category/" & drw.Rows(0).Item(3) & "/", drw.Rows(0).Item(0), drw.Rows(0).Item(0))
        ltrSubMenu.Text = String.Format("<a href='{0}' title='{1}'>{2}</a>", GetImgUrl() & "sub-products-list/" & drw.Rows(0).Item(4) & "/", drw.Rows(0).Item(1), drw.Rows(0).Item(1))
        ' Name and Price
        Dim objDataDetail As DataTable = GetProductDetail(paramCategory)

        objItem.LoadByLink(paramCategory)
        Page.Title = objItem.Name
        Dim objContentItem As DataTable = GetContentProduct(objDataDetail.Rows(0).Item("No_"))
        ltrProductName.Text = objDataDetail.Rows(0).Item("Name") '& " - " & objDataDetail.Rows(0).Item("No_")
        If objDataDetail.Rows(0).Item("Descriptions").ToString().Length > 0 Then
            ltrDescription.Text = String.Format("  <div class='info-orther'><div id='Description'> " & _
                                               "<i class='pull-left fa fa-map-marker fa-fw fa-2x'></i> {0}</div></div> ", objDataDetail.Rows(0).Item("Descriptions"))
        End If
        ltrNo.Text = objDataDetail.Rows(0).Item("No_")
        ' Process price include sale off price
        Dim tblSaleoffPrice As DataTable = LoadSaleOffPrice(objDataDetail.Rows(0).Item("No_"), "008")
        If tblSaleoffPrice.Rows.Count = 0 Then
            Dim tblPriceWithHCMPlace As DataTable = GetPriceNormal(objDataDetail.Rows(0).Item("No_"), "008")
            Dim showPrice As String = ""
            If tblPriceWithHCMPlace.Rows.Count > 0 Then
                showPrice = tblPriceWithHCMPlace.Rows(0).Item("Origin Price")
            Else
                showPrice = objDataDetail.Rows(0).Item("Sales Price")
            End If

            If showPrice <> "0" Then
                ltrPrice.Text = Convert.ToDecimal(showPrice).ToString("#,###") + " VNĐ"
            Else
                ltrPrice.Text = "Call"
            End If
        Else
            Dim originPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Origin Price")
            Dim dealPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Deal Price")
            ltrPrice.Text = dealPrice.ToString("#,###")
            ltrPriceSaleOff.Text = String.Format("<span style='font-size: 11px;'>Giá gốc: </span><span style='font-size: 11px; text-decoration: line-through;'> {0} VND </span><span style='font-size: 14px;'>Tiết kiệm: </span> <span style='font-size:14px'>{1}%</span>", originPrice.ToString("#,###"), Convert.ToInt64(((originPrice - dealPrice) / originPrice) * 100))
        End If

        'branch
        Dim linkUrl As String = ""
        linkUrl = GetUrl() & "sub-products-list/" & drw.Rows(0).Item(5) & "/" & objDataDetail.Rows(0).Item("BrandName") & "/"
        If Not (objDataDetail.Rows(0).Item("BrandName") Is Nothing) Then
            ltrBranch.Text = String.Format("<span>{0} </span>- <a style='color:#007dc6!important'  href='{1}'>Xem sản phẩm khác cùng thương hiệu</a>", objDataDetail.Rows(0).Item("BrandName"), linkUrl)
        End If


        hdProductId.Value = objDataDetail.Rows(0).Item("No_")

        ' show the same products
        GetSameGroupItem(objDataDetail.Rows(0).Item("No_"))
        ShowColor(objDataDetail.Rows(0).Item("No_"))
        ltrRemainNumber.Text = objDataDetail.Rows(0).Item("Stock")
        ' show specific furture
        'ltrSpecifics.Text = ShowFeature(hdProductId.Value)
        ' get selected productd
        If Request.Cookies("SelectedItem") Is Nothing Then
            Response.Cookies("SelectedItem").Value &= objDataDetail.Rows(0).Item("No_")
        Else
            Response.Cookies("SelectedItem").Value = Request.Cookies("SelectedItem").Value & "-" & objDataDetail.Rows(0).Item("No_")
        End If
        If objContentItem.Rows().Count > 0 Then
            ltrContent.Text = objContentItem.Rows(0).Item("Content")
        End If
        Dim sItemNo As String = hdProductId.Value
        Dim SQL As String = "", nIJ As Integer = 0
        Dim sUrl As String = GetUrl()
        Dim sBigImg As String = "", sThumbImg As String = "", sMainImages As String = ""
        Dim tDt As DataTable
        SQL = " SELECT [Images URL] Name, 'Images/Product/' + [Images URL] ImgUrl,'Images/Product/' + [Images Thum URL] ImgUrlThumb  FROM Item WHERE No_ = '" & sItemNo & "'" & _
                " UNION ALL " & _
                " SELECT [Image Natural] Name, 'Images/ProductImages/' + [Image Natural] ImgUrl, 'Images/ProductImages/' + [Image Thumbs] ImgUrlThumb FROM [Item Images] WHERE [Item No_] = '" & sItemNo & "'"
        tDt = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        For nIJ = 0 To tDt.Rows.Count - 1
            If nIJ = 0 Then
                sMainImages &= "<img src=""" & Utils.CheckExitsImages(sUrl & tDt.Rows(nIJ).Item("ImgUrl")) & """ alt="""" style='width: 285px; height: 350px;object-fit: contain;' data-imagezoom='true'/>" & vbCrLf
            End If

            If nIJ = 0 Then
                sThumbImg &= "<li class='image-nav-item active' rel='" & nIJ + 1 & "'><span><img src=""" & Utils.CheckExitsImages(sUrl & tDt.Rows(nIJ).Item("ImgUrlThumb")) & """ alt="""" /></span></li>" & vbCrLf
            Else
                sThumbImg &= "<li class='image-nav-item' rel='" & nIJ + 1 & "'><span><img src=""" & Utils.CheckExitsImages(sUrl & tDt.Rows(nIJ).Item("ImgUrlThumb")) & """ alt="""" /></span></li>" & vbCrLf

            End If

        Next

        ltrMainImages.Text = sMainImages
        ltrThumImages.Text = sThumbImg
    End Sub

    Function ShowImageSlide() As String
        Dim sItemNo As String = hdProductId.Value
        Dim sHtml As String = "", sClosedTag As String = ""
        Dim SQL As String = "", nIJ As Integer = 0
        Dim sUrl As String = GetUrl()
        Dim sBigImg As String = "", sThumbImg As String = "", sMainImages As String = ""
        Dim tDt As DataTable
        SQL = " SELECT [Images URL] Name, 'Images/Product/' + [Images URL] ImgUrl,'Images/Product/' + [Images Thum URL] ImgUrlThumb  FROM Item WHERE No_ = '" & sItemNo & "'" & _
                " UNION ALL " & _
                " SELECT [Image Natural] Name, 'Images/ProductImages/' + [Image Natural] ImgUrl, 'Images/ProductImages/' + [Image Thumbs] ImgUrlThumb FROM [Item Images] WHERE [Item No_] = '" & sItemNo & "'"
        tDt = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        'sBigImg &= "<div id=""bigPic"">" & vbCrLf
        'sThumbImg &= "<div style=""margin-left:10px;"">" & vbCrLf
        'sThumbImg &= "<ul id=""thumbs"">" & vbCrLf
        For nIJ = 0 To tDt.Rows.Count - 1
            If nIJ = 0 Then
                sMainImages &= "<img src=""" & Utils.CheckExitsImages(sUrl & tDt.Rows(nIJ).Item("ImgUrl")) & """ alt="""" style='width: 285px; height: 350px;' data-imagezoom='true'/>" & vbCrLf
            End If

            If nIJ = 0 Then
                sBigImg &= "'" & Utils.CheckExitsImages(sUrl & tDt.Rows(nIJ).Item("ImgUrl")) & "'" & vbCrLf
                sThumbImg &= "<li class='image-nav-item active' rel='" & nIJ + 1 & "'><span><img src=""" & Utils.CheckExitsImages(sUrl & tDt.Rows(nIJ).Item("ImgUrlThumb")) & """ alt="""" /></span></li>" & vbCrLf
            Else
                sThumbImg &= "<li class='image-nav-item' rel='" & nIJ + 1 & "'><span><img src=""" & Utils.CheckExitsImages(sUrl & tDt.Rows(nIJ).Item("ImgUrlThumb")) & """ alt="""" /></span></li>" & vbCrLf
                sBigImg &= ",'" & Utils.CheckExitsImages(sUrl & tDt.Rows(nIJ).Item("ImgUrl")) & "'" & vbCrLf
            End If

        Next

        ltrMainImages.Text = sMainImages
        ltrThumImages.Text = sThumbImg
        Return sBigImg
    End Function

    Sub GetSameGroupItem(Optional ByVal sItemNo As String = "")
        Dim SQL As String = ""
        Dim nIJ As Integer = 0
        Dim tbl As DataTable
        Dim sHtml As String = ""
        Dim nNum As Integer = 0
        objItem.Load(sItemNo)
        txtUnitOfMeasure.Value = objItem.UnitOfMeasure
        If objItem.MenuGroupNo_.Trim <> "" Then
            SQL = " SELECT TOP 5 I.No_ [Item No_], 1 Num,I.Name, I.[Link URL],I.[Sales Price],I.[Images Thum URL] FROM Item I " & _
                    " WHERE I.No_ <> '" & sItemNo & "' AND I.[Menu Group No_] = '" & _
                    objItem.MenuGroupNo_ & "' AND I.Published = 1 AND I.[Brand No_] = '" & objItem.BrandNo_ & "'" & _
                    " UNION ALL " & _
                    " SELECT TOP 5 I.No_ [Item No_], 2 Num,I.Name, I.[Link URL],I.[Sales Price],I.[Images Thum URL] FROM Item I " & _
                    " WHERE I.No_ <> '" & sItemNo & "' AND I.[Menu Group No_] = '" & _
                    objItem.MenuGroupNo_ & "' AND I.Published = 1 AND I.[Brand No_] <> '" & objItem.BrandNo_ & "'" & _
                    " ORDER BY Num "
        Else
            SQL = " SELECT TOP 5 I.No_ [Item No_], 1 Num,I.Name, I.[Link URL],I.[Sales Price],I.[Images Thum URL] FROM Item I " & _
                " WHERE I.No_ <> '" & sItemNo & "' AND I.[Menu Category No_] = '" & _
                  objItem.MenuCategoryNo_ & "' AND I.Published = 1 AND I.[Brand No_] = '" & objItem.BrandNo_ & "'" & _
                  " UNION ALL " & _
                  " SELECT TOP 5 I.No_ [Item No_], 2 Num,I.Name, I.[Link URL],I.[Sales Price],I.[Images Thum URL] FROM Item I " & _
                  " WHERE No_ <> '" & sItemNo & "' AND I.[Menu Category No_] = '" & _
                  objItem.MenuCategoryNo_ & "' AND I.Published = 1 AND I.[Brand No_] <> '" & objItem.BrandNo_ & "'" & _
                  " ORDER BY Num "
        End If

        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        rptRelatedProducts.DataSource = tbl
        rptRelatedProducts.DataBind()
    End Sub

    Protected Sub rptRelatedProducts_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            Dim linkUrl As String
            Dim arrParam As Array = strParam.Split("/")
            linkUrl = GetUrl() & "product-detail/" & drv.Row("Link URL") & "/"
            Dim strImage As String = ""
            strImage = String.Format("<a href='{0}' rel='nofollow'> <img alt='' src='{1}'></a>", linkUrl, Utils.CheckExitsImages(GetImgUrl() & "Images/Product/" & drv.Row("Images Thum URL")))
            Dim ltrImages As Literal = e.Item.FindControl("ltrImages")
            ltrImages.Text = strImage
            Dim hpLink As HyperLink = e.Item.FindControl("hpLink")
            hpLink.NavigateUrl = linkUrl
            hpLink.Text = drv.Row("Name")
            Dim ltrPrice As Literal = e.Item.FindControl("ltrPrice")
            If drv.Row("Sales Price") <> "0" Then
                ltrPrice.Text = Convert.ToDecimal(drv.Row("Sales Price")).ToString("#,###") & " VND"
            Else
                ltrPrice.Text = "Call"
            End If

        Else

        End If
    End Sub

    Function ShowFeature(ByVal sItemNo As String) As String
        Dim sHtml As String = ""
        Dim SQL As String = "", nIJ As Integer = 0
        Dim sFeatureValue As String = "", sItemOrigin As String = ""
        SQL = " SELECT [Item Origin No_] FROM [Item Color] WHERE [Item No_] = '" & sItemNo & "'"
        sItemOrigin = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        'If sItemOrigin.Trim <> "" Then sItemNo = sItemOrigin
        Dim tbl As DataTable
        SQL = " SELECT F.[Feature Name], F.[Feature Value], C.[Unit Of Measure] " & _
               " FROM [Item Features] F " & _
               " LEFT JOIN [Categoy Feature] C ON F.[Feature Group No_] = C.[Feature Group No_] AND F.[Feature No_] = C.[Feature No_] AND C.[Category No_] = '" & objItem.MenuCategoryNo_ & "' " & _
               " WHERE F.[Item No_]  = '" & sItemNo & "' AND RTRIM(F.[Feature Value]) <> '' ORDER BY C.[Position Order] "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tbl.Rows.Count = 0 Then Return ""
        sHtml &= "<div class='ui-box-body'>"
        'If tbl.Rows.Count > 0 Then
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<dl class='ui-attr-list util-clearfix'>" & vbCrLf
            sHtml &= "<dt>" & tbl.Rows(nIJ).Item("Feature Name") & vbCrLf
            sHtml &= "</dt>" & vbCrLf
            sFeatureValue = tbl.Rows(nIJ).Item("Feature Value") & " " & tbl.Rows(nIJ).Item("Unit Of Measure")
            sHtml &= "<dd title=''>" & sFeatureValue.Replace(vbLf, "<br />").Replace(vbCrLf, "<br />") & vbCrLf
            sHtml &= "</dd>" & vbCrLf
            sHtml &= "</dl>" & vbCrLf
        Next
        'Else
        'Html &= "<tr><td style=""padding-left:25px;"">" & vbCrLf
        'sHtml &= objItem.Descriptions.Replace(vbCrLf, "<br />").Replace(vbLf, "<br />")
        'sHtml &= "</>"
        'End If
        sHtml &= "</div>"
        Return sHtml
    End Function

    Sub LoadSelectedProducts()
        Dim SQL As String = ""
        Dim nIJ As Integer = 0
        Dim tbl As DataTable
        Dim sHtml As String = ""
        Dim nNum As Integer = 0
        Dim selectedValue As String = Request.Cookies("SelectedItem").Value
        Dim arrData As Array = selectedValue.Split("-")
        Dim sItemNo As String = ""
        For nIJ = 0 To arrData.Length - 1
            If nIJ = 0 Then
                sItemNo = String.Format("'{0}'", arrData(nIJ))
            Else
                sItemNo &= String.Format(",'{0}'", arrData(nIJ))
            End If
        Next
        'objItem.Load(sItemNo)

        SQL = " SELECT I.No_ [Item No_], 1 Num,I.Name, I.[Link URL],I.[Sales Price],I.[Images Thum URL] FROM Item I " & _
                " WHERE I.No_ in (" & sItemNo & ")"

        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        rptSelected.DataSource = tbl
        rptSelected.DataBind()
    End Sub

    Protected Sub rptSelected_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            Dim linkUrl As String
            Dim arrParam As Array = strParam.Split("/")
            linkUrl = GetUrl() & "product-detail/" & drv.Row("Link URL") & "/"
            Dim strImage As String = ""
            strImage = String.Format("<a href='{0}' rel='nofollow'> <img alt='' src='{1}'></a>", linkUrl, Utils.CheckExitsImages(GetImgUrl() & "Images/Product/" & drv.Row("Images Thum URL")))
            Dim ltrImages As Literal = e.Item.FindControl("ltrImages")
            ltrImages.Text = strImage
            Dim hpLink As HyperLink = e.Item.FindControl("hpLink")
            hpLink.NavigateUrl = linkUrl
            hpLink.Text = drv.Row("Name")
            Dim ltrPrice As Literal = e.Item.FindControl("ltrPrice")
            If drv.Row("Sales Price") <> "0" Then
                ltrPrice.Text = Convert.ToDecimal(drv.Row("Sales Price")).ToString("#,###") & " VND"
            Else
                ltrPrice.Text = "Call"
            End If

        End If
    End Sub

    Sub ShowColor(ByVal No_ As String)
        Dim sSQL As String
        sSQL = " SELECT * from [Item Color] Where [Item Origin No_] = '" & No_ & "'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Dim rHtml As String = ""
        For nIk = 0 To tbl.Rows.Count - 1
            rHtml &= String.Format("<li><div style='background-color: #{0};width: 10; padding: 10px; border: 2px solid {1};margin: 5px;'></div></li>", tbl.Rows(nIk).Item("Item Color"), tbl.Rows(nIk).Item("Item Color"))
        Next
        ltrColor.Text = rHtml
    End Sub

    Protected Sub cmdWishList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdWishList.Click
        txtAction.Value = "W"
        If ReturnIfNull(Session("CustomerNo"), "").ToString.Trim = "" Then
            Login_ModalPopupExtender.Show()
            Exit Sub
        End If
        Dim sWishListNo As String = ""
        Dim Script As String
        sWishListNo = GetWishList(Session("CustomerNo"))
        If sWishListNo.Trim = "" Then
            sWishListNo = CreateAutoWishList(Session("CustomerNo"), Request.Cookies("homeoneguest").Value, "apl")
        End If
        If sWishListNo.Trim = "" Then Exit Sub
        Dim objWL As New adv.Business.WishListLine
        If objWL.ExistsItemInWishList(sWishListNo, objItem.No_) Then
            Script = "$(""#MsgContent"").html('Sản phẩm này đã tồn tại trong danh sách thường mua của bạn.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", Script, True)
            HomeoneMsgPopup.Show()
            Exit Sub
        End If
        objItem.Load(hdProductId.Value)
        With objWL
            .CustomerNo_ = Session("CustomerNo")
            .HomeoneGuestNo_ = Request.Cookies("homeoneguest").Value
            .DocumentNo_ = sWishListNo
            .ItemNo_ = objItem.No_
            .PageNo_ = "apl"
            .LineNo_ = 0
            .Variants = ""
            .Save()
        End With
        txtAction.Value = ""
        Dim sMsg As String = ""
        sMsg = "Đã thêm vào danh sách thường mua của bạn."
        sMsg &= "<div style=""margin-top:10px;""><a href=""" & GetUrl() & "InProcess.aspx"">Xem danh sách</a></div>"
        sMsg &= "<div style=""margin-top:10px;""><a href=""" & GetUrl() & "InProcess.aspx"">Mua hàng nhanh</a></div>"
        Script = "$(""#MsgContent"").html('" & sMsg & "')"
        'If ltrMessageDialog.Text = "" Then
        'ltrMessageDialog.Text = sMsg
        If Session("CustomerNo") Is Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallWarning", Script, True)
        End If

        'Else
        '    ltrMessageDialog.Text = sMsg
        'End If

        HomeoneMsgPopup.Show()
    End Sub

    Protected Sub cmdCloseMsg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseMsg.Click
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        Response.Redirect(url)

    End Sub

    Protected Sub cmdLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim sEmail As String = TxtEmailLog.Text
        Dim sPwd As String = TxtPasswordLog.Text
        If Not objC.LoadByEmail(sEmail) Then
            LblErrCheckin.Text = "Email không tồn tại."
            Login_ModalPopupExtender.Show()
            Exit Sub
        End If

        If adv.Business.MD5Encrypt(objC.Email & sPwd.Trim) <> objC.Password Then
            LblErrCheckin.Text = "Mật khẩu không đúng."
            Login_ModalPopupExtender.Show()
            Exit Sub
        End If
        Session("CustomerNo") = objC.No_
        TxtFullName.Text = objC.FullName
        TxtEmail.Text = objC.Email
        cmdSaveReviewLog.Visible = False
        Login_ModalPopupExtender.Hide()
        Dim script As String = ""
        script = "ShowTopLink()"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
        If txtAction.Value = "W" Then cmdWishList_Click(cmdWishList, e)
    End Sub

    Protected Sub cmdSaveRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveRegister.Click
        If TxtEmailRegister.Text.Trim = "" Then
            LblWarningRegister.Text = "Bạn phải nhập email."
            Login_ModalPopupExtender.Show()
            TxtPasswordRegister.Attributes("value") = TxtPasswordRegister.Text
            TxtConfirmPwdRegister.Attributes("value") = TxtConfirmPwdRegister.Text
            Exit Sub
        End If
        Dim sTemp As String = ""
        If Not IsEmail(TxtEmailRegister.Text) Then
            LblWarningRegister.Text = "Địa chỉ email không hợp lệ."
            Login_ModalPopupExtender.Show()
            TxtPasswordRegister.Attributes("value") = TxtPasswordRegister.Text
            TxtConfirmPwdRegister.Attributes("value") = TxtConfirmPwdRegister.Text
            Exit Sub
        End If
        If objC.LoadByEmail(TxtEmailRegister.Text) Then
            LblWarningRegister.Text = "Địa chỉ email đã tồn tại."
            Login_ModalPopupExtender.Show()
            TxtPasswordRegister.Attributes("value") = TxtPasswordRegister.Text
            TxtConfirmPwdRegister.Attributes("value") = TxtConfirmPwdRegister.Text
            Exit Sub
        End If
        If TxtPasswordRegister.Text.Trim = "" Then
            LblWarningRegister.Text = "Bạn phải nhập mật khẩu."
            Login_ModalPopupExtender.Show()
            Exit Sub
        End If
        If TxtPasswordRegister.Text.Trim <> TxtConfirmPwdRegister.Text.Trim Then
            LblWarningRegister.Text = "Mật khẩu bạn nhập không chính xác."
            Login_ModalPopupExtender.Show()
            Exit Sub
        End If
        If TxtFullNameRegister.Text.Trim = "" Then
            TxtPasswordRegister.Attributes("value") = TxtPasswordRegister.Text
            TxtConfirmPwdRegister.Attributes("value") = TxtConfirmPwdRegister.Text
            LblWarningRegister.Text = "Bạn phải nhập họ tên."
            Login_ModalPopupExtender.Show()
            Exit Sub
        End If

        With objC
            Dim sYM As String = ""
            objNoSeri.Load("CUSTOMERNO")
            sYM = sLeft(Date2Char(getToday()), 6)
            .No_ = objNoSeri.CreateNoSeri(sYM)
            .FullName = TxtFullNameRegister.Text.Trim
            .Sex = CboTitle.SelectedValue
            .Telephone = ""
            .Email = TxtEmailRegister.Text
            .Address = ""
            .ProvinceNo_ = ""
            .DistrictNo_ = ""
            .Birthday = ""
            .CustomerType = ""
            .CustomerPriceGroup = ""
            .LoyaltyCardNo_ = ""
            .RegisterDate = Date2Char(getToday)
            .LastVisited = Date2Char(getToday)
            .Password = adv.Business.MD5Encrypt(.Email.Trim & TxtPasswordRegister.Text.Trim)
            .ReceivingEmail = IIf(CKReceivingPromotion.Checked, 1, 0)
            If Not .Save() Then Exit Sub

            Session("CustomerNo") = .No_
        End With
        TxtFullName.Text = objC.FullName
        TxtEmail.Text = objC.Email
        cmdSaveReviewLog.Visible = False
        Login_ModalPopupExtender.Hide()
        Dim script As String = ""
        script = "ShowTopLink()"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
        If txtAction.Value = "W" Then cmdWishList_Click(cmdWishList, e)
    End Sub

    Protected Sub CmdAddtoCart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddtoCart.Click
        Dim nQty As Integer
        If TxtQuantity.Text = "" Then nQty = 1
        If Val(TxtQuantity.Text) = 0 Then
            nQty = 1
        Else
            nQty = Val(TxtQuantity.Text)
        End If
        Dim sCartNo As String = ""
        objItem.Load(hdProductId.Value)
        If Not Context.Request.Cookies("CartNo") Is Nothing Then
            sCartNo = ReturnIfNull(Context.Request.Cookies("CartNo").Value, "").ToString.Trim
        End If

        If sCartNo.Trim = "" Then
            Dim objNoSeri As New adv.Business.cNoSeri
            Dim sYM As String
            objNoSeri.Load("CARTNO")
            sYM = sLeft(Date2Char(getToday()), 6)
            sCartNo = objNoSeri.CreateNoSeri(sYM)
            HttpContext.Current.Response.Cookies("CartNo").Value = sCartNo
            HttpContext.Current.Response.Cookies("CartNo").Expires = Now.AddMonths(12)
        End If
        Dim sVariants As String = ""
        sVariants = ReturnIfNull(Request("size_" & objItem.No_), "").ToString.Trim
        If Not Request.Cookies("homeoneguest") Is Nothing Then sHomeoneKey = ReturnIfNull(Request.Cookies("homeoneguest").Value, "").ToString

        AddtoCart(objItem.No_, nQty, sVariants, sCartNo, txtUnitOfMeasure.Value)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", "ShowCart();", True)
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        Response.Redirect(url)
    End Sub

    Public Sub AddtoCart(ByVal sItemNo As String, ByVal nQty As Integer, ByVal sVariant As String, ByVal sCartNo As String, ByVal sUnitOrMeasure As String)
        Dim objItem As New adv.Business.Item
        Dim objCartLine As New adv.Business.CartLine
        Dim nUnitPrice As Double = 0, nDealPrice As Double = 0, sGift As String = "", sPromotionNo As String = ""
        If sUnitOrMeasure.Trim = "" Then
            objItem.Load(sItemNo)
            sUnitOrMeasure = objItem.UnitOfMeasure
        End If
        objItem.GetSalesPrice(sItemNo, nUnitPrice, nDealPrice, sGift, sPromotionNo, sUnitOrMeasure)
        If nDealPrice = 0 And nUnitPrice <> 0 Then nDealPrice = nUnitPrice
        With objCartLine
            .CartNo_ = sCartNo
            .LineNo_ = 0
            .ItemNo_ = sItemNo
            .Descriptions = sGift
            .Quantity = nQty
            .UnitPrice = IIf(nDealPrice < nUnitPrice And nDealPrice <> 0, nDealPrice, nUnitPrice)
            .AmountIncVAT = .Quantity * .UnitPrice
            .OrderDate = Date2Char(getToday)
            .CustomerIP = HttpContext.Current.Request.UserHostAddress
            .Variants = sVariant
            .UnitOrMeasure = sUnitOrMeasure
            .PromotionNo = sPromotionNo
            .Save()
        End With

        If sGift.Trim <> "" And sPromotionNo.Trim <> "" Then
            Dim tbl As DataTable
            tbl = objItem.GetGift(sPromotionNo)
            Dim nIJ As Integer
            For nIJ = 0 To tbl.Rows.Count - 1
                With objCartLine
                    .CartNo_ = sCartNo
                    .LineNo_ = 0
                    .ItemNo_ = tbl.Rows(nIJ).Item("Item No_")
                    .Descriptions = ""
                    .Quantity = tbl.Rows(nIJ).Item("Quantity") * nQty
                    If tbl.Rows(nIJ).Item("Disc_ Type") = 0 Then
                        nDealPrice = tbl.Rows(nIJ).Item("Deal Price")
                    Else
                        nDealPrice = Val(tbl.Rows(nIJ).Item("Origin Price")) * Val(tbl.Rows(nIJ).Item("Deal Price")) / 100
                    End If
                    .UnitPrice = nDealPrice
                    .AmountIncVAT = .Quantity * .UnitPrice
                    .OrderDate = Date2Char(getToday)
                    .CustomerIP = HttpContext.Current.Request.UserHostAddress
                    .Variants = ""
                    .LineType = 1
                    .PromotionNo = sPromotionNo
                    objItem.Load(.ItemNo_)
                    .UnitOrMeasure = IIf(ReturnIfNull(tbl.Rows(nIJ).Item("Unit Of Measure"), "").ToString.Trim <> "", tbl.Rows(nIJ).Item("Unit Of Measure"), objItem.UnitOfMeasure)
                    .Save()
                End With
            Next
        End If

    End Sub

    Protected Sub cmdSaveReview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveReview.Click
        If TxtFullName.Text.Trim = "" Then
            LblWarning.Text = "Bạn phải nhập họ tên."
            Exit Sub
        End If
        Dim sTemp As String = ""
        If TxtEmail.Text.Trim <> "" Then
            If Not IsEmail(TxtEmail.Text) Then
                LblWarning.Text = "Địa chỉ email không hợp lệ."
                Exit Sub
            End If
        End If
        If ProductRating.CurrentRating = 0 Then
            LblWarning.Text = "Bạn click vào các ngôi sao để chấm điểm sản phẩm, cám ơn bạn."
            Exit Sub
        End If
        Dim sGuestID As String = ""
        If Not Request.Cookies("homeoneguest") Is Nothing Then sGuestID = ReturnIfNull(Request.Cookies("homeoneguest").Value, "").ToString
        If sGuestID.Trim = "" Then
            Dim sYM As String
            objNoSeri.Load("GUEST")
            sYM = sLeft(Date2Char(getToday()), 6)
            sGuestID = adv.Business.MD5Encrypt(objNoSeri.CreateNoSeri(sYM))
            Response.Cookies("homeoneguest").Value = sGuestID
            Response.Cookies("homeoneguest").Expires = DateTime.Now.AddYears(1)
        Else
            If Not objCustomerReview.CheckHaveRated(objC.No_, sGuestID) Then
                LblWarning.Text = "Bạn đã đánh giá sản phẩm này rồi."
                Exit Sub
            End If
        End If
        Try
            With objCustomerReview
                Dim sColorNo As String = ""
                .LineNo_ = 0
                .ItemNo_ = objItem.No_
                sColorNo = objItem.GetMainColorItemNo(.ItemNo_)
                If sColorNo.Trim <> "" Then
                    .ItemNo_ = sColorNo
                End If
                .CustomerNo_ = ReturnIfNull(Session("customerno"), "").ToString.Trim
                .CustomerName = TxtFullName.Text
                .Email = TxtEmail.Text
                .ReviewTitle = ""
                .ReviewText = TxtCommand.Text
                .ReviewDate = Date2Char(getToday)
                .CustomerIP = HttpContext.Current.Request.UserHostAddress
                .CustomerRate = ProductRating.CurrentRating
                .Published = 0
                .ReviewHour = Now.Hour & ":" & Now.Minute & ":" & Now.Second
                .GuestID = sGuestID
                Dim sValue As String = ""
                sValue = .Save()
                If sValue.Trim <> "" Then
                    LblWarning.Text = sValue
                    Exit Sub
                End If
                ShowHideControl(False)
            End With
            LblWarning.Text = "Bạn đã gửi nhận xét đánh giả sản phẩm thành công. Cám ơn bạn"
        Catch ex As Exception
            LblWarning.Text = ex.ToString
        End Try
    End Sub
    Sub UpdateView()
        Dim sCustomerViewKey As String = ""
        Dim sYM As String
        If Not Request.Cookies("homeoneguest") Is Nothing Then sCustomerViewKey = ReturnIfNull(Request.Cookies("homeoneguest").Value, "").ToString
        If sCustomerViewKey.Trim = "" Then
            objNoSeri.Load("GUEST")
            sYM = sLeft(Date2Char(getToday()), 6)
            sCustomerViewKey = adv.Business.MD5Encrypt(objNoSeri.CreateNoSeri(sYM))
            Response.Cookies("homeoneguest").Value = sCustomerViewKey
            Response.Cookies("homeoneguest").Expires = DateTime.Now.AddYears(1)
        End If
        Dim objItemView As New adv.Business.CustomerViewItem
        With objItemView
            .CookieKey = sCustomerViewKey
            .ItemNo_ = objItem.No_
            .DateView = Date2Char(getToday())
            .IPNo_ = HttpContext.Current.Request.UserHostAddress
            .CustomerNo_ = ReturnIfNull(Session("customerno"), "").ToString.Trim
            .Save()
        End With

        If Not objCustomerReview.CheckHaveRated(objC.No_, sCustomerViewKey) Then
            cmdSaveReview.Enabled = False
            cmdSaveReviewLog.Enabled = False
            cmdSaveReview.CssClass = "ButtonW8BigDisabled"
            cmdSaveReviewLog.CssClass = "ButtonW8BigDisabled"
        End If

    End Sub

    Sub ShowHideControl(ByVal bShow As Boolean)
        TxtFullName.Enabled = bShow
        TxtEmail.Enabled = bShow
        ProductRating.ReadOnly = Not bShow
        TxtCommand.Enabled = bShow
    End Sub

    Protected Sub cmdSaveReviewLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveReviewLog.Click
        Login_ModalPopupExtender.Show()
    End Sub
End Class
