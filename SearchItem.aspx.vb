Imports System.Data

Partial Class SearchItem
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

    Public Shared Function LoadKeyWord(ByVal keyword As String) As DataTable
        Dim sSQL As String
        sSQL = String.Format(" select distinct Name from item where dbo.fChuyenCoDauThanhKhongDau(Name) like '{0}%'", keyword)
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Return tbl

    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim rParamCategory As String = ""
            Dim catId As String = ""
            Dim keyword As String = ""
            rParamCategory = Request("keyword")
            Dim arrParam As Array = rParamCategory.Split("/")

            If arrParam.Length > 1 Then
                keyword = arrParam(0)
                catId = arrParam(1)
            End If


            hdCateName.Value = catId
            hdBrandName.Value = keyword
            LoadRelatedCategories(catId, keyword)
            LoadDataforProductList(catId, keyword)
            LoadNewProducts(catId, keyword)
            LoadHotProducts(catId, keyword)
            'CtrListPage1.PageIndexNextEvent += New NextPagerEventHandler(AddressOf CtrListPage1_PageIndexChangedEvent)
            'CtrListPage1.PageIndexNextEvent += new MinhThuweb.Page.News.CtrListPage.NextPagerEventHandler(CtrListPage1_PageIndexChangedEvent);

        End If
        AddHandler CtrListPage1.PageIndexNextEvent, New Common_CtrListPage.NextPagerEventHandler(AddressOf CtrListPage1_PageIndexChangedEvent)

    End Sub

    Private Sub LoadRelatedCategories(ByVal catid As String, ByVal keyword As String)
        'Dim arrParam As Array = lstCategory.Split("/")
        Dim html As String = ""
        'Dim menuName3 As String = drw.Rows(0).Item(2)
        'html &= " <a href='" & GetImgUrl() + "InProcess.aspx" & "' title='" & menuName1 & "' rel='category tag'>" & menuName1 & "</a>" & vbCrLf
        'html &= " <span class='divider'>&gt;</span>" & vbCrLf
        'html &= "<span>" & menuName2 & "</span>"
        ''html &= " <span>" & menuName2 & "</span>"
        Dim tblMenu As DataTable
        Dim htmlShowMenuName As String = ""
        If catid = "0" Then
            tblMenu = GettblMenu(keyword)

            For nik = 0 To tblMenu.Rows.Count - 1
                If tblMenu.Rows(nik).Item("total") <> "0" Then
                    htmlShowMenuName &= " <dt class='sn-parent-title'><a href='" & GetImgUrl() + "InProcess.aspx" & "' dir='' rel='nofollow'><i class='fa fa-angle-double-right'></i> " & tblMenu.Rows(nik).Item("Name") & " </a></dt>" & vbCrLf
                    htmlShowMenuName &= " <dd class='list-box'><dl class='son-category'>" & vbCrLf
                    htmlShowMenuName &= "<dd>"
                    Dim tblCate As DataTable = LoadSubCategory(tblMenu.Rows(nik).Item("No_"), keyword)
                    For nIj = 0 To tblCate.Rows.Count - 1
                        'html &= " <a href='" & GetImgUrl() + "products-list/" & tblCate.Rows(nIj).Item("Link Display") & "' title='" & tblCate.Rows(nIj).Item("Name") & "'rel='category tag'>" & tblCate.Rows(nIj).Item("FullName") & "</a> <span class='divider'>&gt;</span>"
                        If tblCate.Rows(nIj).Item("total") <> "0" Then
                            htmlShowMenuName &= "<dl class='son-category'> <dt class='sn-parent-title'><span class='current-cate'> <a href='" & GetImgUrl() & "sub-products-list/" & tblCate.Rows(nIj).Item("Link Display") & "/" & "'>" & tblCate.Rows(nIj).Item("Name") & "</a></span> </dt></dl>"
                        End If

                    Next
                    htmlShowMenuName &= "</dd></dd>"
                End If

            Next
        Else
            Dim strName As String = GetCatNameById(hdCateName.Value).Rows(0).Item("Name")
            htmlShowMenuName &= " <dt class='sn-parent-title'><a href='" & GetImgUrl() + "InProcess.aspx" & "' dir='' rel='nofollow'><i class='fa fa-angle-double-right'></i> " & strName & " </a></dt>" & vbCrLf
            htmlShowMenuName &= " <dd class='list-box'><dl class='son-category'>" & vbCrLf
            htmlShowMenuName &= "<dd>"
            Dim tblCate As DataTable = LoadSubCategory(hdCateName.Value, keyword)
            For nIj = 0 To tblCate.Rows.Count - 1
                'html &= " <a href='" & GetImgUrl() + "products-list/" & tblCate.Rows(nIj).Item("Link Display") & "' title='" & tblCate.Rows(nIj).Item("Name") & "'rel='category tag'>" & tblCate.Rows(nIj).Item("FullName") & "</a> <span class='divider'>&gt;</span>"
                If tblCate.Rows(nIj).Item("total") <> "0" Then
                    htmlShowMenuName &= "<dl class='son-category'> <dt class='sn-parent-title'><span class='current-cate'> <a href='" & GetImgUrl() & "sub-products-list/" & tblCate.Rows(nIj).Item("Link Display") & "/" & "'>" & tblCate.Rows(nIj).Item("Name") & "</a></span> </dt></dl>"
                End If

            Next
            htmlShowMenuName &= "</dd></dd>"
        End If

        'ltrRelatedCategory.Text = html


        ' htmlShowMenuName &= "  <dd> <dl class='son-category'> <dt class='sn-parent-title'><span class='current-cate'>" & menuName3 & "</span> </dt></dl></dd></dl></dd>"
        ltrShowRelatedLeft.Text = htmlShowMenuName

        ' show brand
        'LoadBrand(drw.Rows(0).Item("No_"), catid)
    End Sub


    Private Function LoadSubCategory(ByVal categoryid As String, ByVal keywork As String) As DataTable

        'sSQL = " SELECT * FROM [Brand]"
        Dim arParams() As IDataParameter = New IDataParameter(1) {}
        arParams(0) = DBHelper.createParameter("@categoryid", SqlDbType.VarChar, ParameterDirection.Input, categoryid)
        arParams(1) = DBHelper.createParameter("@keywork", SqlDbType.VarChar, ParameterDirection.Input, keywork)

        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "CalSumItemWithCategory", arParams).Tables(0)
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
        LoadDataforProductList(hdCateName.Value, hdBrandName.Value)
    End Sub

    Private Sub LoadDataforProductList(ByVal catId As String, ByVal keywork As String)
        Dim sSQL As String
        Dim totalItem As Integer = 0
        Dim pageCount As Integer = 0
        'sSQL = " SELECT * FROM [Item]"
        'sSQL = String.Format(" SELECT * FROM [Good Menu] where [Link Display]='{0}'", catId)
        'Dim tblCat As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        'categoryId = tblCat.Rows(0).Item("No_")
        Dim tbl As DataTable '= DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        tbl = adv.Business.Item.SearchItems(CtrListPage1.CurrentPageIndex + 1, CtrListPage1.PageSize, catId, keywork, totalItem)
        rptProductList.DataSource = tbl
        If tbl.Rows.Count = 0 Then
            lblNodata.Visible = True
        End If
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

    Public Function GettblMenu(ByVal keywork As String) As DataTable
        'sSQL = " SELECT * FROM [Brand]"
        Dim arParams() As IDataParameter = New IDataParameter(0) {}
        arParams(0) = DBHelper.createParameter("@keywork", SqlDbType.VarChar, ParameterDirection.Input, keywork)

        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.StoredProcedure, "GetAllCategoryWhithKeyword", arParams).Tables(0)
        Return tbl
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
                ltrUL.Text = "<ul class='util-clearfix son-list first-son-list'>"
                flash = True
            End If
            Dim linkUrl As String = ""

            'Dim arrParam As Array = hd.Value.Split("/")

            Dim drv As DataRowView = e.Item.DataItem

            linkUrl = GetUrl() & "product-detail/" & drv.Row("Link URL") & "/"

            'b.CommandArgument = drv.Row["ID_COLUMN_NAME"].ToString();
            Dim ltrPic As Literal = e.Item.FindControl("ltrpicCore")

            ltrPic.Text = String.Format("<a class='picRind' href='{0}'> <img class='picCore pic-Core-v' src='{1}'></a>", linkUrl, Utils.CheckExitsImages(GetImgUrl() & "Images/Product/" & drv.Row("Images Thum URL")))
            Dim ltrName As Literal = e.Item.FindControl("ltrName")
            ltrName.Text = String.Format("<a class='product' href='{0}' title='{1}'>{2}</a>", linkUrl, drv.Row("Name"), drv.Row("Name"))

            Dim ltrPrice As Literal = e.Item.FindControl("ltrPrice")
            If drv.Row("Sales Price") <> "0" Then
                ltrPrice.Text = Convert.ToDecimal(drv.Row("Sales Price")).ToString("#,###") & " VND"
            Else
                ltrPrice.Text = "Call"
            End If

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
            sSQL = String.Format(" SELECT top 10 * FROM [Item] where [Menu Division No_]='{0}' and Published = 1 and [New Product] = 1 and (Name like N'%{1}%' or Descriptions like N'%{1}%' or No_ like N'%{1}%')", categoryId, keyword)
        Else
            sSQL = String.Format(" SELECT top 10 * FROM [Item] where Published = 1 and [New Product] = 1 and (Name like N'%{0}%' or Descriptions like N'%{0}%' or No_ like N'%{0}%')", keyword)
        End If

        Dim tblCat As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        rptNewProduct.DataSource = tblCat
        totalNewProducts = tblCat.Rows().Count
        rptNewProduct.DataBind()
    End Sub

    Private Sub LoadHotProducts(ByVal categoryId As String, ByVal keyword As String)
        Dim sSQL As String = ""
        'sSQL = " SELECT * FROM [Item]"
        If categoryId <> "0" Then
            sSQL = String.Format(" SELECT top 10 * FROM [Item] where [Menu Division No_]='{0}' and Published = 1 and [Hot Product] = 1 and (Name like N'%{1}%' or Descriptions like N'%{1}%' or No_ like N'%{1}%')", categoryId, keyword)
        Else
            sSQL = String.Format(" SELECT top 10 * FROM [Item] where Published = 1 and [Hot Product] = 1 and (Name like N'%{0}%' or Descriptions like N'%{0}%' or No_ like N'%{0}%')", keyword)
        End If
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
                Dim ltrUl As Literal = e.Item.FindControl("ltrUl")
                ltrUl.Text = "<ul id='p4p-ul-content' class='p4p-bottom-content clearfix'>"
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
            If drv.Row("Sales Price") <> "0" Then
                ltrPrice.Text = Convert.ToDecimal(drv.Row("Sales Price")).ToString("#,###") & " VND"
            Else
                ltrPrice.Text = "Call"
            End If

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
