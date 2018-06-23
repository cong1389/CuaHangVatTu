Imports System.Data
Imports System.IO

Partial Class _Default
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request("categoryid")) Then
            ShowCategory()
        End If

    End Sub
    Sub ShowCategory()
        Dim sHtmlMainManu As String = ""
        Dim sHtmlFloorFixed As String = ""
        Dim tbl As DataTable
        tbl = GettblMenu(0)

        ' show for find

        ' show for left menu
        Dim nIJ As Integer

        ltrLoadFirstTier.Text = LoadProductByCategoryId(tbl.Rows(0).Item("No_"))
        For nIJ = 0 To tbl.Rows.Count - 1
            'If nIJ = 0 Then
            '    catListId &= tbl.Rows(nIJ).Item("No_")
            'Else
            '    catListId &= "," & tbl.Rows(nIJ).Item("No_")
            'End If

            sHtmlMainManu &= "          <dl data-role='first-menu' class='cl-item cl-item-sports'>" & vbCrLf
            sHtmlMainManu &= " <dt class='cate-name'> " &
 " <a href='" & GetImgUrl() + "sub-category/" & tbl.Rows(nIJ).Item("Link Display") & "/" & "'>" &
        "    <img src=" & Utils.CheckExitsImages(GetUrl() & "Images/ProductGroup/" & tbl.Rows(nIJ).Item("Images Thum URL")) &
        " alt=''>" & tbl.Rows(nIJ).Item("Name")
            'sHtmlMainManu &= " <dt class='cate-name'><span><a href='" & GetImgUrl() + "sub-category/" & tbl.Rows(nIJ).Item("Link Display") & "/" & "'>" & tbl.Rows(nIJ).Item("Name") & vbCrLf
            sHtmlMainManu &= "  </a> </dt>" & vbCrLf

            sHtmlMainManu &= " <dd data-role='first-menu-main' class='sub-cate'>" & vbCrLf
            sHtmlMainManu &= " <div class='sub-cate-main'>" & vbCrLf

            Dim tblLev1 As DataTable
            tblLev1 = GettblMenu(1, " [Parent No_] = '" & tbl.Rows(nIJ).Item("No_") & "'")
            Dim nIJ1 As Integer
            Dim countRows As Integer
            countRows = 0
            For nIJ1 = 0 To tblLev1.Rows.Count - 1
                If nIJ1 Mod 2 = 0 Then
                    sHtmlMainManu &= " <div class='sub-cate-row'>" & vbCrLf
                End If
                countRows = countRows + 1
                sHtmlMainManu &= "<dl data-role='two-menu' class='sub-cate-items'>" & vbCrLf
                sHtmlMainManu &= "<dt><a href='" & GetImgUrl() + "sub-products-list/" & tblLev1.Rows(nIJ1).Item("Link Display") & "/" & "'>" & tblLev1.Rows(nIJ1).Item("Name") & "</a></dt>" & vbCrLf

                sHtmlMainManu &= "<dd>" & vbCrLf
                Dim tblLev2 As DataTable
                tblLev2 = GettblMenu(2, " [Parent No_] = '" & tblLev1.Rows(nIJ1).Item("No_") & "'")
                Dim nIJ2 As Integer
                For nIJ2 = 0 To tblLev2.Rows.Count - 1
                    Dim linkUrl As String = ""
                    linkUrl = GetUrl() & "products-list/" & tblLev2.Rows(nIJ2).Item("Link Display") & "/"
                    sHtmlMainManu &= "<a href='" & linkUrl & "'>" & tblLev2.Rows(nIJ2).Item("Name") & "</a>" & vbCrLf
                Next

                sHtmlMainManu &= "</dd>" & vbCrLf

                sHtmlMainManu &= "</dl>" & vbCrLf
                If countRows = 2 Then
                    sHtmlMainManu &= "</div>" & vbCrLf
                    countRows = 0
                End If
            Next



            sHtmlMainManu &= "</div></dd>" & vbCrLf

            sHtmlMainManu &= " </dl>" & vbCrLf
        Next

        ltrMainCategory.Text = sHtmlMainManu
        ' show flor
        ' <ul class="floor-nav-list">
        '    <li class="nav-women"><a href="#"><span>Women's Clothing</span></a></li>            
        '</ul>
        sHtmlFloorFixed &= "          <ul class='floor-nav-list'>" & vbCrLf
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtmlFloorFixed &= " <li class='nav-sports'><a href='" & GetUrl() & "#j-covi-" & tbl.Rows(nIJ).Item("No_") & "'><i></i><span>" & tbl.Rows(nIJ).Item("Name") & vbCrLf
            sHtmlFloorFixed &= "  </span></a></li>" & vbCrLf
        Next
        sHtmlFloorFixed &= "          </ul>" & vbCrLf
        'ltrFloorFixedPanel.Text = sHtmlFloorFixed

    End Sub

    Public Function GettblMenu(ByVal sLevel As Integer, Optional ByVal sConditions As String = "") As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        If sConditions.Trim <> "" Then sWhere = " AND " & sConditions
        sSQL = " SELECT TOP 7 * FROM [Good Menu] WHERE [Menu Type] = " & sLevel & " AND Published = 1 AND [Using For] = 0 " & sWhere & " ORDER BY [Menu Order] "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function



    <System.Web.Services.WebMethod()>
    Public Shared Function LoadProductByCategoryId(ByVal catId As Integer) As String
        Dim templateHTML As String
        Dim uc As UserControl = New UserControl()
        templateHTML = Utils.RenderControlToHtml(uc.LoadControl("~/Common/LoadHomeCategory.ascx"))
        Dim sSQLCate As String
        ' Get category name by category id
        Dim objCate As DataTable
        sSQLCate = " SELECT * FROM [Good Menu] WHERE [No_] =" & catId
        objCate = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQLCate).Tables(0)
        'Repace category Name
        Dim htmlCategoryName As String = ""
        htmlCategoryName = String.Format("<a href='" & GetImgUrl() + "sub-category/" & objCate.Rows(0).Item("Link Display") & "/" & "'>{0}</a>", objCate.Rows(0).Item("Name"))
        'templateHTML = String.Format(templateHTML, objCate.Rows(0).Item("Name"))
        ' Get category list by ctegory Id
        Dim SubCateLev As DataTable
        Dim sWhereCate As String = ""
        Dim sSQL As String
        sWhereCate = " [Parent No_] = " & catId
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = 1 AND Published = 1 AND [Using For] = 0 AND" & sWhereCate & " ORDER BY [Menu Order] "
        SubCateLev = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)

        Dim htmlLstSubCategory As String = ""
        For nIJSubCate = 0 To SubCateLev.Rows.Count - 1
            htmlLstSubCategory &= "  <div class='cate-item'>         <a href='" & GetUrl() + "sub-products-list/" & SubCateLev.Rows(nIJSubCate).Item("Link Display") & "/" & "' class='highlight'>" & SubCateLev.Rows(nIJSubCate).Item("Name") & " | </a></div>" & vbCrLf
        Next

        ' Get List cate id
        Dim splitDataCategory As String = ""

        For nIJ = 0 To SubCateLev.Rows.Count - 1
            If nIJ = 0 Then
                splitDataCategory &= SubCateLev.Rows(nIJ).Item("No_")
            Else
                splitDataCategory &= "," & SubCateLev.Rows(nIJ).Item("No_")
            End If
        Next
        ' Get Product
        Dim sSQLItemsRun As String
        Dim lblItemRun As DataTable
        sSQLItemsRun = String.Format(" SELECT * FROM [Item] WHERE [Menu Category No_] in ({0}) and IsRun = 1 and Published=1", splitDataCategory)
        lblItemRun = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQLItemsRun).Tables(0)

        Dim htmlLstItemRun As String = ""

        'For nIRun = 0 To lblItemRun.Rows.Count - 1
        Dim linkUrlMain As String = ""
        linkUrlMain = GetUrl() & "sub-category/" & objCate.Rows(0).Item("Link Display") & "/"
        htmlLstItemRun &= " <li style='width: 478px;'>" & vbCrLf
        htmlLstItemRun &= " <a href='" & linkUrlMain & "'>" & vbCrLf
        htmlLstItemRun &= " <div class='pic'><img src=" & Utils.CheckExitsImages(GetUrl() & "Images/ProductGroup/" & objCate.Rows(0).Item("Images URL")) & " alt='' style='visibility: visible;'></div>" & vbCrLf
        htmlLstItemRun &= " <div class='event-info'>" & vbCrLf
        htmlLstItemRun &= " <span class='main-title'>" & Utils.DropText(objCate.Rows(0).Item("Name"), 20) & "</span>"
        'htmlLstItemRun &= "" & vbCrLf
        htmlLstItemRun &= " </div></a> </li>" & vbCrLf

        'Next


        Dim sSQLItemsNotRun As String
        Dim lblItemNotRun As DataTable
        sSQLItemsNotRun = String.Format(" SELECT top 6 * FROM [Item] WHERE [Menu Category No_] in ({0}) and IsRun = 0 and [Hot Product] = 1 and Published=1 order by [Order Position]", splitDataCategory)
        lblItemNotRun = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQLItemsNotRun).Tables(0)
        Dim html4Item As String = ""
        Dim html2Item As String = ""
        For nIjNotRun = 0 To lblItemNotRun.Rows.Count - 1
            Dim ltrPrice As String = ""
            Dim ltrOrgPrice As String = ""
            Dim strPrices As String = ""
            Dim strTitle As String = " <span class='subject'>" & Utils.DropText(lblItemNotRun.Rows(nIjNotRun).Item("Name"), 20) & "</span>"
            ' Process price include sale off price
            Dim tblSaleoffPrice As DataTable = LoadSaleOffPrice(lblItemNotRun.Rows(nIjNotRun).Item("No_"), "008")
            If tblSaleoffPrice.Rows.Count = 0 Then
                Dim tblPriceWithHCMPlace As DataTable = GetPriceNormal(lblItemNotRun.Rows(nIjNotRun).Item("No_"), "008")
                Dim showPrice As String = ""
                If tblPriceWithHCMPlace.Rows.Count > 0 Then
                    showPrice = tblPriceWithHCMPlace.Rows(0).Item("Origin Price") & vbCrLf
                Else
                    showPrice = lblItemNotRun.Rows(nIjNotRun).Item("Sales Price")
                End If

                If showPrice <> "0" Then
                    strPrices = String.Format("<div class='price-info col-sm-9'>{0}<span class='new-price'>{1}</span></div><div class='col-sm-3 divAddCard'><span><i class='fa fa-cart-plus fa-2x'></i> </span></div>", strTitle, Convert.ToDecimal(showPrice).ToString("#,###") & " VND") & vbCrLf
                Else
                    strPrices = String.Format("<div class='price-info'>{0}<span class='new-price'>{1}</span></div>", strTitle, "Call") & vbCrLf
                End If
            Else
                Dim originPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Origin Price")
                Dim dealPrice As Decimal = tblSaleoffPrice.Rows(0).Item("Deal Price")
                'ltrPrice.Text = dealPrice.ToString("#,###") & " VND"
                'ltrOrgPrice.Text = String.Format("<div><span>Giá gốc: </span><span style='font-size:11pt;font-weight:bold; text-decoration: line-through;'> {0} VND </span></div><span>Tiết kiệm: </span> <span style='font-size:11pt;font-weight:bold'>{1}%</span>", originPrice.ToString("#,###"), Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100))
                strPrices = String.Format("<div class='price-info col-sm-9'>{3}<span class='new-price'>{1} VND</span><span class='old-price'>{0} VND</span><span class='discount-percent'>{2}%</span></div><div class='col-sm-3 divAddCard'><span><i class=' fa-2x fa fa-cart-plus'></i> </span></div>", originPrice.ToString("#,###"), dealPrice.ToString("#,###"), Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100), strTitle) & vbCrLf

                'ltrSaleOff.Text = originPrice.ToString("#,###")
                'ltrSaleOff.Text = Convert.ToInt16(((originPrice - dealPrice) / originPrice) * 100)

            End If

            If nIjNotRun = 0 Or nIjNotRun = 1 Or nIjNotRun = 2 Or nIjNotRun = 3 Or nIjNotRun = 4 Or nIjNotRun = 5 Then

                If nIjNotRun = 0 Or nIjNotRun = 1 Or nIjNotRun = 2 Then
                    html4Item &= " <li class='col-xs-6 col-lg-4 col-md-4 col-sm-6 col-xs-6 noPM'>" & vbCrLf
                Else
                    html4Item &= " <li class='col-xs-6 col-lg-4 col-md-4 col-sm-6 col-xs-6 noPM rec-bottom-line'>" & vbCrLf
                End If

                Dim linkUrl As String = ""
                linkUrl = GetUrl() & "product-detail/" & lblItemNotRun.Rows(nIjNotRun).Item("Link URL") & "/"
                'html4Item &= String.Format(" <a href='{0}'>", linkUrl)
                'html4Item &= " <span class='subject'>" & Utils.DropText(lblItemNotRun.Rows(nIjNotRun).Item("Name"), 20) & "</span>"
                'rHTML &= " <span class="sub-heading">Trendy shirts, dresses &amp; skirts</span>
                html4Item &= " <div class='pic'>" & String.Format(" <a href='{0}'>", linkUrl) & "<img src=" & Utils.CheckExitsImages(GetUrl() & "Images/Product/" & lblItemNotRun.Rows(nIjNotRun).Item("Images Thum URL")) & " alt='' style='visibility: visible;'>" & strPrices & "</div>"
                html4Item &= " </a>" & vbCrLf
                html4Item &= " </li>" & vbCrLf

            Else

                html2Item &= " <li>" & vbCrLf
                Dim linkUrl As String = ""
                linkUrl = GetUrl() & "product-detail/" & lblItemNotRun.Rows(nIjNotRun).Item("Link URL") & "/"
                'html2Item &= String.Format(" <a href='{0}'>", linkUrl)
                'html2Item &= " <a href='#'>"
                'html2Item &= " <span class='subject'>" & Utils.DropText(lblItemNotRun.Rows(nIjNotRun).Item("Name"), 20) & "</span>"
                'rHTML &= " <span class="sub-heading">Trendy shirts, dresses &amp; skirts</span>
                html2Item &= " <div class='pic'> " & String.Format(" <a href='{0}'>", linkUrl) & " <img src=" & Utils.CheckExitsImages(GetUrl() & "Images/Product/" & lblItemNotRun.Rows(nIjNotRun).Item("Images Thum URL")) & " alt='' style='visibility: visible;'>" & strPrices & "</div>"
                html2Item &= " </a>" & vbCrLf
                html2Item &= " </li>" & vbCrLf

            End If

        Next

        templateHTML = String.Format(templateHTML, htmlCategoryName, htmlLstSubCategory, htmlLstItemRun, html4Item, html2Item, objCate.Rows(0).Item("No_"))
        Return templateHTML

    End Function
    Public Shared Function LoadSaleOffPrice(ByVal itemid As String, ByVal sgroup As String) As DataTable
        Dim sToday As String = Date2Char(getToday())
        Dim sSQL As String
        sSQL = String.Format(" select * from [Sales Price] where [Item No_] like '{0}' AND [Starting Date] <= '{1}' AND ([Ending Date] = '' OR [Ending Date] > ='{2}')", itemid, sToday, sToday, sgroup)
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
End Class
