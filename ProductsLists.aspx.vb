Imports System.Data
Imports Common_CtrListPage

Partial Class ProductsLists
    Inherits System.Web.UI.Page
    Dim totalProducts As Integer
    Public categoryId As String
    'Public BrandName As String
    Dim totalNewProducts As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim rParamCategory As String = ""
            Dim catName As String = ""
            Dim brandName As String = ""
            rParamCategory = Request("catid")
            Dim arrParam As Array = rParamCategory.Split("/")
            If arrParam.Length > 2 Then
                range__values__from.Text = arrParam(1)
                hdPriceFrom.Value = arrParam(1)
                range__values__to.Text = arrParam(2)
                hdPriceTo.Value = arrParam(2)
            Else
                If arrParam.Length > 1 Then
                    brandName = arrParam(1)
                End If
            End If
            catName = arrParam(0)
            hdCateName.Value = catName
            hdBrandName.Value = brandName
            LoadRelatedCategories(catName)
            LoadDataforProductList(catName, brandName)
            LoadNewProducts()
            LoadHotProducts()
            'CtrListPage1.PageIndexNextEvent += New NextPagerEventHandler(AddressOf CtrListPage1_PageIndexChangedEvent)
            'CtrListPage1.PageIndexNextEvent += new MinhThuweb.Page.News.CtrListPage.NextPagerEventHandler(CtrListPage1_PageIndexChangedEvent);

        End If
        AddHandler CtrListPage1.PageIndexNextEvent, New Common_CtrListPage.NextPagerEventHandler(AddressOf CtrListPage1_PageIndexChangedEvent)

    End Sub

    Private Sub LoadRelatedCategories(ByVal lstCategory As String)
        'Dim arrParam As Array = lstCategory.Split("/")
        Dim html As String = ""
        Dim drw As DataTable = GetcategoryNameByLinkURL(lstCategory)
        Dim menuName1 As String = drw.Rows(0).Item(0)
        Dim menuName2 As String = drw.Rows(0).Item(1)
        Dim menuName3 As String = drw.Rows(0).Item(2)
        html &= " <a href='" & GetUrl() + "sub-category/" & drw.Rows(0).Item("LinkDisplayL0") & "/" & "' title='" & menuName1 & "' rel='category tag'>" & menuName1 & "</a>" & vbCrLf
        html &= " <span class='divider'>&gt;</span>" & vbCrLf
        html &= " <a href='" & GetImgUrl() + "sub-products-list/" & drw.Rows(0).Item("LinkDisplayL1") & "/" & "' title='" & menuName2 & "'rel='category tag'>" & menuName2 & "</a> <span class='divider'>&gt;</span>"
        html &= " <span>" & menuName3 & "(" & drw.Rows(0).Item("total") & ")" & "</span>"
        ltrRelatedCategory.Text = html
        Dim htmlShowMenuName As String = ""


        htmlShowMenuName &= " <dt class='sn-parent-title'><a href='" & GetImgUrl() + "sub-category/" & drw.Rows(0).Item("LinkDisplayL0") & "/" & "' dir='' rel='nofollow'><i class='fa fa-angle-double-right'></i> " & menuName1 & " </a></dt>" & vbCrLf
        htmlShowMenuName &= " <dd class='list-box'><dl class='son-category'>" & vbCrLf
        htmlShowMenuName &= " <dt class='sn-parent-title'><a href='" & GetImgUrl() + "sub-products-list/" & drw.Rows(0).Item("LinkDisplayL1") & "/" & "' rel='nofollow'><i class='fa fa-angle-double-right'></i>" & menuName2 & "</a></dt>"
        htmlShowMenuName &= "  <dd> <dl class='son-category'> <dt class='sn-parent-title'><span class='current-cate'>" & menuName3 & "(" & drw.Rows(0).Item("total") & ")" & "</span> </dt></dl></dd></dl></dd>"
        ltrShowRelatedLeft.Text = htmlShowMenuName

        ' show brand
        LoadBrand(drw.Rows(0).Item(3), lstCategory)
    End Sub
    Sub LoadBrand(ByVal sNo_ As String, ByVal sName As String)
        Dim sSQL As String
        'sSQL = " SELECT * FROM [Brand]"
        Dim arParams() As IDataParameter = New IDataParameter(0) {}
        arParams(0) = DBHelper.createParameter("@categoryid", SqlDbType.VarChar, ParameterDirection.Input, sNo_)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "GetBrandWithProducts", arParams).Tables(0)
        Dim htmlBrand As String = ""
        For nIj = 0 To tbl.Rows.Count - 1
            Dim linkUrl As String = ""
            linkUrl = GetUrl() & "products-list/" & sName & "/" & tbl.Rows(nIj).Item("Name") & "/"
            htmlBrand &= " <dd class='list-box'><ul class='common-select clearfix'>" & vbCrLf
            htmlBrand &= " <li><a rel='nofollow' href='" & linkUrl & "'>" & tbl.Rows(nIj).Item("Name") & "<span>(" & tbl.Rows(nIj).Item("totalBrand") & ")</span>" & "</a> </li></ul></dd> "
        Next
        ltrLstBrand.Text = htmlBrand
    End Sub
    Public Function GetcategoryNameByLinkURL(ByVal linkDispaly As String) As DataTable
        'Dim sSQL As String
        'sSQL = " SELECT * FROM [Good Menu] WHERE [Link Display] = '" & linkDispaly & "'"
        Dim arParams() As IDataParameter = New IDataParameter(0) {}
        arParams(0) = DBHelper.createParameter("@linkURL", SqlDbType.VarChar, ParameterDirection.Input, linkDispaly)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "GetGoogMenuNameByLinkURL", arParams).Tables(0)
        Return tbl
    End Function
    Public Function GetMenuName(ByVal linkDispaly As String) As String
        Dim sSQL As String

        sSQL = " SELECT * FROM [Good Menu] WHERE [Link Display] = '" & linkDispaly & "'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, sSQL).Tables(0)
        If tbl.Rows.Count > 0 Then
            Return tbl.Rows(0).Item("Name")
        Else
            Return ""
        End If
    End Function

    Public Shared Function LoadSaleOffPrice(ByVal itemid As String, ByVal sgroup As String) As DataTable
        Dim sToday As String = Date2Char(getToday())
        Dim sSQL As String
        sSQL = String.Format(" select * from [Sales Price] where [Store Group] like '{3}' and  [Item No_] like '{0}' AND [Starting Date] <= '{1}' AND ([Ending Date] = '' OR [Ending Date] > ='{2}')", itemid, sToday, sToday, sgroup)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Return tbl

    End Function

    Public Sub CtrListPage1_PageIndexChangedEvent(sender As Object, eventArgument As Integer)
        CtrListPage1.CurrentPageIndex = eventArgument
        LoadDataforProductList(hdCateName.Value, hdBrandName.Value)
    End Sub
    Private Sub LoadDataforProductList(ByVal catId As String, ByVal BrandName As String)
        Dim sSQL As String
        Dim totalItem As Integer = 0
        Dim pageCount As Integer = 0
        sSQL = " SELECT * FROM [Item]"
        sSQL = String.Format(" SELECT * FROM [Good Menu] where [Link Display]='{0}'", catId)
        Dim tblCat As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        categoryId = tblCat.Rows(0).Item("No_")
        Dim tbl As DataTable '= DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        tbl = adv.Business.Item.LoadProductssListById(CtrListPage1.CurrentPageIndex + 1, CtrListPage1.PageSize, tblCat.Rows(0).Item("No_"), BrandName, hdPriceFrom.Value, hdPriceTo.Value, totalItem)
        rptProductList.DataSource = tbl
        totalProducts = totalItem
        rptProductList.DataBind()
        'Reflesh list paging
        If totalItem = 0 Then
            pageCount = 1
        Else
            'pageCount = If(totalItem Mod CtrListPage1.PageSize = 0, totalItem / CtrListPage1.PageSize, totalItem / CtrListPage1.PageSize + 1)
            pageCount = totalItem / CtrListPage1.PageSize
        End If

        CtrListPage1.PageCount = pageCount
        CtrListPage1.RefreshPager()

    End Sub
    Dim countRow As Integer = 0
    Dim flash As Boolean = False

    Protected Sub rptProductList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If countRow = 0 Or countRow Mod 4 = 0 Then
                Dim ltrUL As Literal = e.Item.FindControl("ltrUL")
                ltrUL.Text = "<ul class='row product-list grid filter'>"
                flash = True
            End If
            Dim linkUrl As String = ""

            'Dim arrParam As Array = hd.Value.Split("/")

            Dim drv As DataRowView = e.Item.DataItem

            linkUrl = GetUrl() & "product-detail/" & drv.Row("Link URL") & "/"

            'b.CommandArgument = drv.Row["ID_COLUMN_NAME"].ToString();
            Dim ltrPic As Literal = e.Item.FindControl("ltrpicCore")

            ltrPic.Text = String.Format("<a class='' href='{0}'> <img class='img-responsive' src='{1}'></a>", linkUrl, Utils.CheckExitsImages(GetImgUrl() & "Images/Product/" & drv.Row("Images Thum URL")))
            Dim ltrName As Literal = e.Item.FindControl("ltrName")
            ltrName.Text = String.Format("<a class='product' href='{0}' title='{1}'>{2}</a>", linkUrl, drv.Row("Name"), drv.Row("Name"))

            Dim ltrPrice As Literal = e.Item.FindControl("ltrPrice")
            Dim ltrOrgPrice As Literal = e.Item.FindControl("ltrOrgPrice")
            ' Process price include sale off price
            Dim tblSaleoffPrice As DataTable = LoadSaleOffPrice(drv.Row("No_"), "008")
            If tblSaleoffPrice.Rows.Count = 0 Then
                Dim tblPriceWithHCMPlace As DataTable = GetPriceNormal(drv.Row("No_"), "008")
                Dim showPrice As String = ""
                If tblPriceWithHCMPlace.Rows.Count > 0 Then
                    showPrice = tblPriceWithHCMPlace.Rows(0).Item("Origin Price")
                Else
                    showPrice = drv.Row("Sales Price")
                End If

                If showPrice <> "0" Then
                    ltrPrice.Text = Convert.ToDecimal(showPrice).ToString("#,###") & " VND"
                Else
                    ltrPrice.Text = "Call"
                End If
            Else
                Dim originPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Origin Price")
                Dim dealPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Deal Price")
                ltrPrice.Text = dealPrice.ToString("#,###") & " VND"
                ltrOrgPrice.Text = String.Format("<div><span>Giá gốc: </span><span style='font-size:11pt;font-weight:bold; text-decoration: line-through;'> {0} VND </span></div><span>Tiết kiệm: </span> <span style='font-size:11pt;font-weight:bold'>{1}%</span>", originPrice.ToString("#,###"), Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100))
                'ltrSaleOff.Text = originPrice.ToString("#,###")
                'ltrSaleOff.Text = Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100)

            End If
            'If drv.Row("Sales Price") <> "0" Then
            '    ltrPrice.Text = Convert.ToDecimal(drv.Row("Sales Price")).ToString("#,###") & " VND"
            'Else
            '    ltrPrice.Text = "0 VND"
            'End If

            countRow = countRow + 1
            If countRow Mod 4 = 0 Or totalProducts = countRow Then
                If flash = True Then
                    Dim ltrEndUL As Literal = e.Item.FindControl("ltrEndUL")
                    ltrEndUL.Text = "</ul>"
                    flash = False
                End If

            End If

        End If
    End Sub

    Public Shared Function GetPriceNormal(ByVal itemid As String, ByVal sgroup As String) As DataTable
        Dim sToday As String = Date2Char(getToday())
        Dim sSQL As String
        sSQL = String.Format(" select * from [Sales Price] where [Store Group] like '{0}' and  [Item No_] like '{1}'", sgroup, itemid)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Return tbl

    End Function
    Private Sub LoadNewProducts()
        Dim sSQL As String = ""
        'sSQL = " SELECT * FROM [Item]"
        sSQL = String.Format(" SELECT top 10 * FROM [Item] where [Menu Group No_]='{0}' and Published = 1 and [New Product] = 1 ", categoryId)
        Dim tblCat As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        rptNewProduct.DataSource = tblCat
        totalNewProducts = tblCat.Rows().Count
        rptNewProduct.DataBind()
    End Sub
    Private Sub LoadHotProducts()
        Dim sSQL As String = ""
        'sSQL = " SELECT * FROM [Item]"
        sSQL = String.Format(" SELECT top 10 * FROM [Item] where [Menu Group No_]='{0}' and Published = 1 and [Hot Product] = 1 ", categoryId)
        Dim tblCat As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        rptHotProduct.DataSource = tblCat
        'totalNewProducts = tblCat.Rows().Count
        rptHotProduct.DataBind()
    End Sub
    Dim CountNewProduct As Integer = 0
    Dim FlashNewProduct As Boolean = False

    Protected Sub rptNewProduct_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If CountNewProduct = 0 Or CountNewProduct Mod 4 = 0 Then
                'Dim ltrUlNew As Literal = e.Item.FindControl("ltrUlNew")
                'ltrUlNew.Text = "<ul id='p4p-ul-content' class='p4p-bottom-content clearfix'>"
                FlashNewProduct = True
            End If
            Dim drv As DataRowView = e.Item.DataItem
            Dim linkUrl As String = ""
            'Dim arrParam As Array = hdRequestparam.Value.Split("/")
            linkUrl = GetUrl() & "product-detail/" & drv.Row("Link URL") & "/"
            Dim imgProduct As Image = e.Item.FindControl("imgProduct")
            imgProduct.ImageUrl = Utils.CheckExitsImages(GetImgUrl() & "Images/Product/" & drv.Row("Images Thum URL"))
            Dim hpImage As HyperLink = e.Item.FindControl("hpImage")
            hpImage.NavigateUrl = linkUrl
            hpImage.ToolTip = drv.Row("Name")
            Dim hpNameProduct As HyperLink = e.Item.FindControl("hpNameProduct")
            hpNameProduct.NavigateUrl = linkUrl
            hpNameProduct.Text = drv.Row("Name")
            Dim ltrPrice As Literal = e.Item.FindControl("ltrPrice")
            Dim ltrOrgPrice As Literal = e.Item.FindControl("ltrOrgPrice")
            ' Process price include sale off price
            Dim tblSaleoffPrice As DataTable = LoadSaleOffPrice(drv.Row("No_"), "008")
            If tblSaleoffPrice.Rows.Count = 0 Then
                Dim tblPriceWithHCMPlace As DataTable = GetPriceNormal(drv.Row("No_"), "008")
                Dim showPrice As String = ""
                If tblPriceWithHCMPlace.Rows.Count > 0 Then
                    showPrice = tblPriceWithHCMPlace.Rows(0).Item("Origin Price")
                Else
                    showPrice = drv.Row("Sales Price")
                End If

                If showPrice <> "0" Then
                    ltrPrice.Text = Convert.ToDecimal(showPrice).ToString("#,###") & " VND"
                Else
                    ltrPrice.Text = "0" & " VND"
                End If
            Else
                Dim originPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Origin Price")
                Dim dealPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Deal Price")
                ltrPrice.Text = dealPrice.ToString("#,###") & " VND"
                ltrOrgPrice.Text = String.Format("<div><span>Giá gốc: </span><span style='font-weight:bold; text-decoration: line-through;'> {0} VND </span></div><span>Tiết kiệm: </span> <span style='font-weight:bold'>{1}%</span>", originPrice.ToString("#,###"), Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100))
                'ltrSaleOff.Text = originPrice.ToString("#,###")
                'ltrSaleOff.Text = Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100)

            End If
            'If drv.Row("Sales Price") <> "0" Then
            '    ltrPrice.Text = Convert.ToDecimal(drv.Row("Sales Price")).ToString("#,###") & " VND"
            'Else
            '    ltrPrice.Text = "0 VND"
            'End If

            CountNewProduct = CountNewProduct + 1
            If CountNewProduct Mod 4 = 0 Or totalNewProducts = CountNewProduct Then
                If FlashNewProduct = True Then
                    'Dim ltrEndUl As Literal = e.Item.FindControl("ltrEndUl")
                    'ltrEndUl.Text = "</ul>"
                    FlashNewProduct = False
                End If
            End If
        End If
    End Sub

    Protected Sub rptHotProduct_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim drv As DataRowView = e.Item.DataItem
            Dim linkUrl As String = ""
            'Dim arrParam As Array = hdRequestparam.Value.Split("/")
            linkUrl = GetUrl() & "product-detail/" & drv.Row("Link URL") & "/"
            Dim imgProduct As Image = e.Item.FindControl("imgProduct")
            imgProduct.ImageUrl = Utils.CheckExitsImages(GetImgUrl() & "Images/Product/" & drv.Row("Images Thum URL"))
            Dim hpImageProducts As HyperLink = e.Item.FindControl("hpImageProducts")
            hpImageProducts.NavigateUrl = linkUrl
            hpImageProducts.ToolTip = drv.Row("Name")
            Dim hpNameProduct As HyperLink = e.Item.FindControl("hpNameProduct")
            hpNameProduct.NavigateUrl = linkUrl
            hpNameProduct.Text = drv.Row("Name")
            Dim ltrPrice As Literal = e.Item.FindControl("ltrPrice")
            Dim ltrOrgPrice As Literal = e.Item.FindControl("ltrOrgPrice")
            ' Process price include sale off price
            Dim tblSaleoffPrice As DataTable = LoadSaleOffPrice(drv.Row("No_"), "008")
            If tblSaleoffPrice.Rows.Count = 0 Then
                Dim tblPriceWithHCMPlace As DataTable = GetPriceNormal(drv.Row("No_"), "008")
                Dim showPrice As String = ""
                If tblPriceWithHCMPlace.Rows.Count > 0 Then
                    showPrice = tblPriceWithHCMPlace.Rows(0).Item("Origin Price")
                Else
                    showPrice = drv.Row("Sales Price")
                End If

                If showPrice <> "0" Then
                    ltrPrice.Text = Convert.ToDecimal(showPrice).ToString("#,###") & " VND"
                Else
                    ltrPrice.Text = "Call"
                End If
            Else
                Dim originPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Origin Price")
                Dim dealPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Deal Price")
                ltrPrice.Text = dealPrice.ToString("#,###") & " VND"
                ltrOrgPrice.Text = String.Format("<p><span>Giá gốc: </span><span style='font-weight:bold; text-decoration: line-through;'> {0} VND </span></p><span>Tiết kiệm: </span> <span style='font-weight:bold'>{1}%</span>", originPrice.ToString("#,###"), Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100))
                'ltrSaleOff.Text = originPrice.ToString("#,###")
                'ltrSaleOff.Text = Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100)

            End If
            'If drv.Row("Sales Price") <> "0" Then
            '    ltrPrice.Text = Convert.ToDecimal(drv.Row("Sales Price")).ToString("#,###") & " VND"
            'Else
            '    ltrPrice.Text = "0 VND"
            'End If


        End If
    End Sub

    Protected Sub btnSearchPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchPrice.Click
        Dim linkUrl As String = ""
        linkUrl = GetUrl() & "products-list/" & hdCateName.Value & "/" & range__values__from.Text & "/" & range__values__to.Text & "/"
        Response.Redirect(linkUrl)

    End Sub
End Class
