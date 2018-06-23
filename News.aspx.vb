Imports System.Data

Partial Class News
    Inherits System.Web.UI.Page
    Dim totalProducts As Integer
    Public categoryId As String
    'Public BrandName As String
    Dim totalNewProducts As Integer

    <System.Web.Services.WebMethod()>
    Public Shared Function LoadKey(ByVal keyword As String) As String
        Dim tbl As DataTable = LoadKeyWord(keyword)
        Dim rSql As String = "<ul id='country-list'>"
        For nik = 0 To tbl.Rows.Count - 1
            rSql &= String.Format("<li onClick=""selectCountry('{0}')"">{0}</li>", tbl.Rows(nik).Item("Name"))
        Next
        'Return "<li onClick=""selectCountry('Afghanistan')"">Afghanistan</li>"
        rSql &= "</ul>"
        Return rSql
    End Function

    Public Shared Function LoadSaleOffPrice(ByVal itemid As String, ByVal sgroup As String) As DataTable
        Dim sToday As String = Date2Char(getToday())
        Dim sSQL As String
        sSQL = String.Format(" select * from [Sales Price] where [Store Group] like '{3}' and  [Item No_] like '{0}' AND [Starting Date] <= '{1}' AND ([Ending Date] = '' OR [Ending Date] > ='{2}')", itemid, sToday, sToday, sgroup)
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

    Public Shared Function LoadKeyWord(ByVal keyword As String) As DataTable
        Dim sSQL As String
        sSQL = String.Format(" select distinct Name from item where dbo.fChuyenCoDauThanhKhongDau(Name) like '{0}%'", keyword)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Return tbl

    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim rParamCategory As String = ""
            Dim typeId As String = ""
            Dim keyword As String = ""
            rParamCategory = Request("typeid")
            'Dim arrParam As Array = rParamCategory.Split("/")

            'If arrParam.Length > 0 Then
            '    typeId = arrParam(0)
            'End If

            hdCateName.Value = typeId
            'hdBrandName.Value = keyword
            LoadRelatedCategories()
            LoadDataforProductList(rParamCategory)
            'LoadNewProducts(typeId)
            LoadHotProducts(1)
            'CtrListPage1.PageIndexNextEvent += New NextPagerEventHandler(AddressOf CtrListPage1_PageIndexChangedEvent)
            'CtrListPage1.PageIndexNextEvent += new MinhThuweb.Page.News.CtrListPage.NextPagerEventHandler(CtrListPage1_PageIndexChangedEvent);

        End If
        AddHandler CtrListPage1.PageIndexNextEvent, New Common_CtrListPage.NextPagerEventHandler(AddressOf CtrListPage1_PageIndexChangedEvent)

    End Sub

    Private Sub LoadRelatedCategories()

        Dim html As String = ""
        Dim tblMenu As DataTable
        Dim htmlShowMenuName As String = ""

        tblMenu = GettblMenu()

        For nik = 0 To tblMenu.Rows.Count - 1
            If tblMenu.Rows(nik).Item("total") <> "0" Then
                htmlShowMenuName &= " <dt class='sn-parent-title'><a href='" & GetImgUrl() + "tin-tuc/" & tblMenu.Rows(nik).Item("Link Display") & "/" & "' dir='' rel='nofollow'><i class='fa fa-angle-double-right'></i> " & tblMenu.Rows(nik).Item("Name") & " </a></dt>" & vbCrLf
                htmlShowMenuName &= " <dd class='list-box'><dl class='son-category'>" & vbCrLf
                htmlShowMenuName &= "<dd>"
                'Dim tblCate As DataTable = LoadSubCategory(stypeid, tblMenu.Rows(nik).Item("No_"))
                'For nIj = 0 To tblCate.Rows.Count - 1
                '    'html &= " <a href='" & GetImgUrl() + "products-list/" & tblCate.Rows(nIj).Item("Link Display") & "' title='" & tblCate.Rows(nIj).Item("Name") & "'rel='category tag'>" & tblCate.Rows(nIj).Item("FullName") & "</a> <span class='divider'>&gt;</span>"
                '    If tblCate.Rows(nIj).Item("total") <> "0" Then
                '        htmlShowMenuName &= "<dl class='son-category'> <dt class='sn-parent-title'><span class='current-cate'> <a href='" & GetImgUrl() & "sub-products-list/" & tblCate.Rows(nIj).Item("Link Display") & "/" & "'>" & tblCate.Rows(nIj).Item("Name") & "</a></span> </dt></dl>"
                '    End If

                'Next
                htmlShowMenuName &= "</dd></dd>"
            End If

        Next


        ltrRelatedCategory.Text = html


        ' htmlShowMenuName &= "  <dd> <dl class='son-category'> <dt class='sn-parent-title'><span class='current-cate'>" & menuName3 & "</span> </dt></dl></dd></dl></dd>"
        ltrShowRelatedLeft.Text = htmlShowMenuName

        ' show brand
        'LoadBrand(drw.Rows(0).Item("No_"), catid)
    End Sub


    Private Function LoadSubCategory(ByVal stypeId As Integer, ByVal catId As String) As DataTable

        'sSQL = " SELECT * FROM [Brand]"
        Dim arParams() As IDataParameter = New IDataParameter(1) {}
        arParams(0) = DBHelper.createParameter("@styid", SqlDbType.Int, ParameterDirection.Input, stypeId)
        arParams(1) = DBHelper.createParameter("@catId", SqlDbType.VarChar, ParameterDirection.Input, catId)


        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "LoadSpecialProducts", arParams).Tables(0)
        Return tbl
    End Function

    Sub LoadBrand(ByVal sNo_ As String, ByVal sName As String)

        Dim arParams() As IDataParameter = New IDataParameter(0) {}
        arParams(0) = DBHelper.createParameter("@categoryid", SqlDbType.VarChar, ParameterDirection.Input, sNo_)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "GetBrandWithSubCategory", arParams).Tables(0)
        Dim htmlBrand As String = ""
        For nIj = 0 To tbl.Rows.Count - 1
            Dim linkUrl As String = ""
            linkUrl = GetUrl() & "sub-products-list/" & sName & "/" & tbl.Rows(nIj).Item("Name") & "/"
            htmlBrand &= " <dd class='list-box'><ul class='common-select clearfix'>" & vbCrLf
            htmlBrand &= " <li><a rel='nofollow' href='" & linkUrl & "'>" & tbl.Rows(nIj).Item("Name") & "<span>(" & tbl.Rows(nIj).Item("totalBrand") & ")</span>" & "</a> </li></ul></dd> "
        Next
        'ltrLstBrand.Text = htmlBrand
    End Sub

    Public Function GetcategoryNameByLinkURL(ByVal linkDispaly As String) As DataTable
        'Dim sSQL As String
        'sSQL = " SELECT * FROM [Good Menu] WHERE [Link Display] = '" & linkDispaly & "'"
        Dim arParams() As IDataParameter = New IDataParameter(0) {}
        arParams(0) = DBHelper.createParameter("@linkURL", SqlDbType.VarChar, ParameterDirection.Input, linkDispaly)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "GetSubCategoryNameByLinkURL", arParams).Tables(0)
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

    Public Sub CtrListPage1_PageIndexChangedEvent(ByVal sender As Object, ByVal eventArgument As Integer)
        CtrListPage1.CurrentPageIndex = eventArgument
        LoadDataforProductList(hdCateName.Value)
    End Sub

    Private Sub LoadDataforProductList(ByVal rParamCategory As String)

        Dim SQL As String
        Dim sWhere As String = " and ('" & rParamCategory & "'='' OR  G.[Link Menu]='" & rParamCategory & "')"
        Dim totalItem As Integer = 0
        Dim pageCount As Integer = 0
        SQL = " SELECT *" & _
                " FROM Content C INNER JOIN [Content Group] G ON C.[Group No_] = G.[Group No_] " & _
                " WHERE C.[Content Type] > 0 " & sWhere & _
                "  ORDER BY [Date Create] DESC, 1, 2 "

        Dim pData As DataTable
        pData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        rptProductList.DataSource = pData
        rptProductList.DataBind()

        If totalItem = 0 Then
            pageCount = 1
        Else
            'pageCount = If(totalItem Mod CtrListPage1.PageSize = 0, totalItem / CtrListPage1.PageSize, totalItem / CtrListPage1.PageSize + 1)
            pageCount = totalItem / CtrListPage1.PageSize
        End If

        CtrListPage1.PageCount = pageCount
        CtrListPage1.RefreshPager()

    End Sub

    Public Function GettblMenu() As DataTable

        Dim SQL As String
        Dim sWhere As String = ""

        SQL = " SELECT G.[Group Name] as Name, G.[Group No_] as No_, g.[Link Menu] as [Link Display], Count(*) AS total FROM Content C INNER JOIN [Content Group] G ON C.[Group No_] = G.[Group No_]" & _
                " WHERE C.[Content Type] > 0 GROUP BY G.[Group Name], G.[Group No_], g.[Link Menu]"

        Dim pData As DataTable
        pData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        Return pData
    End Function

    Public Function GetCatNameById(ByVal cateId As String) As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        sSQL = " SELECT * FROM [Good Menu] WHERE [No_] = '" & cateId & "'"
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Dim countRow As Integer = 0

    Dim flash As Boolean = False

    Protected Sub rptProductList_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If countRow = 0 Or countRow Mod 4 = 0 Then
                Dim ltrUL As Literal = e.Item.FindControl("ltrUL")
                ltrUL.Text = "<ul class='row product-list grid filter'>"
                flash = True
            End If
            Dim linkUrl As String = ""

            'Dim arrParam As Array = hd.Value.Split("/")

            Dim drv As DataRowView = e.Item.DataItem

            linkUrl = GetUrl() & "tin-tuc-detail/" & drv.Row("Link Menu") & "/"

            'b.CommandArgument = drv.Row["ID_COLUMN_NAME"].ToString();
            Dim ltrPic As Literal = e.Item.FindControl("ltrpicCore")

            ltrPic.Text = String.Format("<a class='picRind' href='{0}'> <img class='center-block' src='{1}'></a>", linkUrl, Utils.CheckExitsImages(GetImgUrl() & "Images/Content/Images/" & drv.Row("Images URL")))
            Dim ltrName As Literal = e.Item.FindControl("ltrName")
            ltrName.Text = String.Format("<a class='product' href='{0}' title='{1}'>{2}</a>", linkUrl, drv.Row("Title"), drv.Row("Title"))

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

    Private Sub LoadNewProducts(ByVal categoryId As String, ByVal keyword As String)
        Dim sSQL As String = ""
        'sSQL = " SELECT * FROM [Item]"
        If categoryId <> "0" Then
            sSQL = String.Format(" SELECT top 10 * FROM [Item] where [Menu Division No_]='{0}' and Published = 1 and [New Product] = 1 and (Name like N'%{1}%' or Descriptions like N'%{1}%')", categoryId, keyword)
        Else
            sSQL = String.Format(" SELECT top 10 * FROM [Item] where Published = 1 and [New Product] = 1 and (Name like N'%{0}%' or Descriptions like N'%{0}%')", keyword)
        End If

        Dim tblCat As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        'rptNewProduct.DataSource = tblCat
        'totalNewProducts = tblCat.Rows().Count
        'rptNewProduct.DataBind()
    End Sub

    Private Sub LoadHotProducts(ByVal styleId As Integer)
        Dim sSQL As String = ""
        'sSQL = " SELECT * FROM [Item]"
        If styleId = 1 Then
            sSQL = " SELECT top 10 * FROM [Item] where Published = 1 and [Hot Product] = 1 order by [Order Position]"
        ElseIf (styleId = 2) Then
            sSQL = "SELECT top 10 * FROM [Item] where Published = 1 and [Best Selling] = 1 order by [Order Position]"
        Else
            sSQL = " SELECT top 10 * FROM [Item] where Published = 1 and [Hot Product] = 1 order by [Order Position]"
        End If
        Dim tblCat As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        rptHotProduct.DataSource = tblCat
        totalNewProducts = tblCat.Rows().Count
        rptHotProduct.DataBind()
    End Sub
    Dim CountNewProduct As Integer = 0
    Dim FlashNewProduct As Boolean = False

    Protected Sub rptNewProduct_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If CountNewProduct = 0 Or CountNewProduct Mod 4 = 0 Then
                Dim ltrUl As Literal = e.Item.FindControl("ltrUl")
                ltrUl.Text = "<ul id='p4p-ul-content' class='p4p-bottom-content clearfix'>"
                FlashNewProduct = True
            End If
            Dim drv As DataRowView = e.Item.DataItem
            Dim linkUrl As String = ""
            'Dim arrParam As Array = hdRequestparam.Value.Split("/")
            linkUrl = GetUrl() & "newsdetail/" & drv.Row("Link URL") & "/"
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



            CountNewProduct = CountNewProduct + 1
            If CountNewProduct Mod 4 = 0 Or totalNewProducts = CountNewProduct Then
                If FlashNewProduct = True Then
                    Dim ltrEndUl As Literal = e.Item.FindControl("ltrEndUl")
                    ltrEndUl.Text = "</ul>"
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
            If drv.Row("Sales Price") <> "0" Then
                ltrPrice.Text = Convert.ToDecimal(drv.Row("Sales Price")).ToString("#,###") & " VND"
            Else
                ltrPrice.Text = "Call"
            End If


        End If
    End Sub


End Class
