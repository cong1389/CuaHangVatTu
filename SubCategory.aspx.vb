Imports System.Data

Partial Class SubCategory
    Inherits System.Web.UI.Page
    Dim totalRow As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request("cat")) Then
            Dim catId As String = Request("cat").Split("/")(0)
            ShowCategory(catId)
        End If
    End Sub

    Sub ShowCategory(ByVal urlDisplay As String)
        Dim sHtmlMainManu As String = ""
        Dim sHtmlFloorFixed As String = ""
        Dim tbl As DataTable
        Dim tbtCat As DataTable = GetMenuNo(urlDisplay)
        Dim catId As String = tbtCat.Rows(0).Item("No_")
        Page.Title = tbtCat.Rows(0).Item("Name")
        tbl = GettblMenu(1, " [Parent No_] = '" & catId & "'")
        ' show for find
        rptSubLeftMenu.DataSource = tbl
        rptSubLeftMenu.DataBind()
        ' show for left menu
        Dim tblItem As DataTable = LoadItemWithMenuDivision(catId)
        rptProductSubCategory.DataSource = tblItem
        rptProductSubCategory.DataBind()
        totalRow = tblItem.Rows.Count
        ' show id cate to call back
        Dim catListId As String = ""
        For nIJ = 0 To tbl.Rows.Count - 1
            If nIJ = 0 Then
                catListId &= tbl.Rows(nIJ).Item("No_")
            Else
                catListId &= "," & tbl.Rows(nIJ).Item("No_")
            End If
        Next
        hdSubCategoryidlist.Value = catListId


    End Sub

    Public Function GettblMenu(ByVal sLevel As Integer, Optional ByVal sConditions As String = "") As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        If sConditions.Trim <> "" Then sWhere = " AND " & sConditions
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = " & sLevel & " AND Published = 1 AND [Using For] = 0 " & sWhere & " ORDER BY [Menu Order] "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Public Function GetMenuNo(ByVal linkDisplay As String) As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        sSQL = " SELECT * FROM [Good Menu] WHERE [Link Display] = '" & linkDisplay & "'"
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Protected Sub rptSubLeftMenu_OnItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            Dim hpLinkSub As HyperLink = e.Item.FindControl("hpLinkSub")
            hpLinkSub.Text = drv.Row("Name")
            Dim urlSub As String = GetImgUrl() + "sub-products-list/" & drv.Row("Link Display") & "/"
            hpLinkSub.NavigateUrl = urlSub
            Dim tbl As DataTable
            tbl = GettblMenu(2, " [Parent No_] = '" & GetMenuNo(drv.Row("Link Display")).Rows(0).Item("No_") & "'")
            Dim rptChildMenu As Repeater = e.Item.FindControl("rptChildMenu")
            rptChildMenu.DataSource = tbl
            rptChildMenu.DataBind()

        End If
    End Sub

    Protected Sub rptChildMenu_OnItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            Dim hpLinkChild As HyperLink = e.Item.FindControl("hpLinkChild")
            hpLinkChild.Text = drv.Row("Name")
            Dim urlSub As String = GetImgUrl() + "products-list/" & drv.Row("Link Display") & "/"
            hpLinkChild.NavigateUrl = urlSub
        End If
    End Sub

    Public Function LoadItemWithMenuDivision(ByVal catId As String) As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        sSQL = " SELECT top 8 * FROM [Item] WHERE [Menu Division No_] like '%" & catId & "' and Published = 1 and [Hot Product] = 1 order by [Order Position]"
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Public Shared Function LoadItemWithMenuCategory(ByVal catId As String) As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        sSQL = " SELECT top 8 * FROM [Item] WHERE [Menu Category No_] like '%" & catId & "' and Published = 1 and [Hot Product] = 1 order by [Order Position]"
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Private countRow As Integer
    Protected Sub rptProductSubCategory_OnItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim drv As DataRowView = e.Item.DataItem
            'Dim hpLinkChild As HyperLink = e.Item.FindControl("hpLinkChild")
            Dim ltrDiv As Literal = e.Item.FindControl("ltrDiv")
            'If countRow = 0 Or countRow Mod 2 = 0 Then
            '    ltrDiv.Text = "<div class='bc-navy-cate-wrap'>"

            'End If
            Dim linkUrl As String = ""
            linkUrl = GetUrl() & "product-detail/" & drv.Row("Link URL") & "/"
            Dim hpLinkImage As HyperLink = e.Item.FindControl("hpLinkImage")
            Dim imgImages As Image = e.Item.FindControl("imgImages")
            Dim ltrPrices As Literal = e.Item.FindControl("ltrPrices")
            ' Process price include sale off price
            Dim tblSaleoffPrice As DataTable = LoadSaleOffPrice(drv.Item("No_"), "008")
            If tblSaleoffPrice.Rows.Count = 0 Then
                Dim tblPriceWithHCMPlace As DataTable = GetPriceNormal(drv.Item("No_"), "008")
                Dim showPrice As String = ""
                If tblPriceWithHCMPlace.Rows.Count > 0 Then
                    showPrice = tblPriceWithHCMPlace.Rows(0).Item("Origin Price")
                Else
                    showPrice = drv.Item("Sales Price")
                End If

                If showPrice <> "0" Then
                    ltrPrices.Text = String.Format("<div class='price-info'><span class='new-price'>{0}</span></div>", Convert.ToDecimal(showPrice).ToString("#,###") & " VND")
                Else
                    ltrPrices.Text = String.Format("<div class='price-info'><span class='new-price'>{0}</span></div>", "Call")
                End If
            Else
                Dim originPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Origin Price")
                Dim dealPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Deal Price")
                'ltrPrice.Text = dealPrice.ToString("#,###") & " VND"
                'ltrOrgPrice.Text = String.Format("<div><span>Giá gốc: </span><span style='font-size:11pt;font-weight:bold; text-decoration: line-through;'> {0} VND </span></div><span>Tiết kiệm: </span> <span style='font-size:11pt;font-weight:bold'>{1}%</span>", originPrice.ToString("#,###"), Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100))
                ltrPrices.Text = String.Format("<div class='price-info'><span class='old-price'>{0} VND</span><span class='new-price'>{1} VND</span><span class='discount-percent'>{2}%</span></div>", originPrice.ToString("#,###"), dealPrice.ToString("#,###"), Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100))
                'ltrSaleOff.Text = originPrice.ToString("#,###")
                'ltrSaleOff.Text = Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100)

            End If
            hpLinkImage.NavigateUrl = linkUrl
            imgImages.ImageUrl = Utils.CheckExitsImages(GetImgUrl() & "Images/Product/" & drv.Row("Images Thum URL"))

            Dim hpLink As HyperLink = e.Item.FindControl("hpLink")
            hpLink.Text = Utils.DropText(drv.Row("Name"), 18)
            hpLink.NavigateUrl = linkUrl

            'Dim ltrEndDiv As Literal = e.Item.FindControl("ltrEndDiv")
            'If countRow = 1 Or countRow Mod 2 = 1 Or totalRow = countRow - 1 Then
            '    ltrEndDiv.Text = " </div>"
            'End If
            countRow = countRow + 1
        End If
    End Sub

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

    <System.Web.Services.WebMethod()>
    Public Shared Function LoadProductBySubCategory(ByVal catId As Integer) As String
        Dim templateHTML As String
        Dim uc As UserControl = New UserControl()
        templateHTML = Utils.RenderControlToHtml(uc.LoadControl("~/Common/LoadSubCategory.ascx"))
        Dim strCatNames As String
        Dim SqlName = String.Format("SELECT * FROM [Good Menu] WHERE No_ like '%{0}'", catId)
        strCatNames = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SqlName).Tables(0).Rows(0).Item("Name")
        Dim sSQLtbl As String
        sSQLtbl = " SELECT * FROM [Good Menu] WHERE [Menu Type] = 2  AND Published = 1 AND [Using For] = 0 and [Parent No_] = '" & catId & "' ORDER BY [Menu Order] "
        Dim tblcatNames As DataTable
        tblcatNames = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQLtbl).Tables(0)
        Dim htmlCatList As String = ""

        For nik = 0 To tblcatNames.Rows.Count - 1
            Dim linkUrl As String = ""
            linkUrl = GetUrl() & "products-list/" & tblcatNames.Rows(nik).Item("Link Display") & "/"
            htmlCatList &= String.Format("<li><a href='{0}' title='{1}' >{1}</a> </li>", linkUrl, tblcatNames.Rows(nik).Item("Name"), tblcatNames.Rows(nik).Item("Name"))
        Next

        Dim tblItemCategorys As DataTable = LoadItemWithMenuCategory(catId.ToString())
        Dim htmlItems As String = ""
        For inh = 0 To tblItemCategorys.Rows.Count - 1
            Dim linkUrl As String = ""
            linkUrl = GetUrl() & "product-detail/" & tblItemCategorys.Rows(inh).Item("Link URL") & "/"
            htmlItems &= "<li class='col-lg-3 col-md-3 col-sm-6 col-xs-6 noPM'>" & vbCrLf
            htmlItems &= " <div class='product-container'>" & vbCrLf
            htmlItems &= " <div class='left-block'>" & vbCrLf
            htmlItems &= "<a href='" & linkUrl & "'>" & vbCrLf
            htmlItems &= "<img class='center-block' src='" & Utils.CheckExitsImages(GetImgUrl() & "Images/Product/" & tblItemCategorys.Rows(inh).Item("Images Thum URL")) & "' alt='" & tblItemCategorys.Rows(inh).Item("Name") & "' /></a></div>" & vbCrLf
            htmlItems &= " <div class='right-block bc-nowrap-ellipsis'>" & vbCrLf
            htmlItems &= " <a href='" & linkUrl & "' title='" & tblItemCategorys.Rows(inh).Item("Name") & "'>" & tblItemCategorys.Rows(inh).Item("Name") & " </a>" & vbCrLf

            Dim strPrices As String = ""
            ' Process price include sale off price
            Dim tblSaleoffPrice As DataTable = LoadSaleOffPrice(tblItemCategorys.Rows(inh).Item("No_"), "008")
            If tblSaleoffPrice.Rows.Count = 0 Then
                Dim tblPriceWithHCMPlace As DataTable = GetPriceNormal(tblItemCategorys.Rows(inh).Item("No_"), "008")
                Dim showPrice As String = ""
                If tblPriceWithHCMPlace.Rows.Count > 0 Then
                    showPrice = tblPriceWithHCMPlace.Rows(0).Item("Origin Price")
                Else
                    showPrice = tblItemCategorys.Rows(inh).Item("Sales Price")
                End If

                If showPrice <> "0" Then
                    strPrices = String.Format("<div class='price-info'><span class='new-price'>{0}</span></div>", Convert.ToDecimal(showPrice).ToString("#,###") & " VND")
                Else
                    strPrices = String.Format("<div class='price-info'><span class='new-price'>{0} VND</span></div>", "Call")
                End If
            Else
                Dim originPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Origin Price")
                Dim dealPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Deal Price")
                strPrices = String.Format("<div class='price-info'><span class='old-price'>{0} VND</span><span class='new-price'>{1} VND</span><span class='discount-percent'>{2}%</span></div>", originPrice.ToString("#,###"), dealPrice.ToString("#,###"), Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100))

            End If

            htmlItems &= String.Format("  <div class='price-info'>{0}</div></div></div></li>", strPrices)
        Next

        templateHTML = String.Format(templateHTML, strCatNames, htmlCatList, htmlItems)
        Return templateHTML
    End Function

End Class
