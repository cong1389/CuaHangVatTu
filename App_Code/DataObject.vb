Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class DataObject
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function UpdateDiscountLine(ByVal sDiscountNo$, ByVal sDescription$, ByVal sPriceGroup$, ByVal sStoreGroup$, ByVal nLineNo As Integer, _
                                       ByVal sItemNo$, ByVal iTrigger%, ByVal sDisc_Type%, ByVal dOriginPrice As Double, ByVal dDealPriceValue As Double, _
                                       ByVal sStartingDate$, ByVal sEndingDate$, ByVal iDiscountType%, ByVal iQuantity%, ByVal sUnitOfMeasure$, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim objDL As New adv.Business.DiscountLine
            With objDL
                .DiscountNo_ = sDiscountNo
                .Description = sDescription
                .PriceGroup = sPriceGroup
                .StoreGroup = sStoreGroup
                .LineNo_ = nLineNo
                .ItemNo_ = sItemNo
                .TriggerDiscount = iTrigger
                .Disc_Type = sDisc_Type
                .OriginPrice = dOriginPrice
                .DealPriceValue = dDealPriceValue
                .StartingDate = sStartingDate
                .EndingDate = sEndingDate
                If .EndingDate = "17530101" Then .EndingDate = ""
                .DiscountType = iDiscountType
                .Quantity = iQuantity
                .UnitOfMeasure = sUnitOfMeasure
            End With
            Return objDL.Save()
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function UpdateSalesPrice(ByVal sItemNo$, ByVal sStoreGroup$, ByVal sSalePriceNo$, ByVal sUnitOfMeasure$, ByVal sStartingDate$, ByVal sEndingDate$, _
                                     ByVal dUnitPrice As Double, ByVal dDealPrice As Double, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim objSPrice As New adv.Business.SalesPrice
            With objSPrice
                .ItemNo_ = sItemNo
                .StoreGroup = sStoreGroup
                .SalesPriceNo_ = sSalePriceNo
                .UnitOfMeasureNo_ = sUnitOfMeasure
                .StartingDate = sStartingDate
                .EndingDate = sEndingDate
                .UnitPrice = dUnitPrice
                .PriceIncVAT = 1
                .OriginPrice = dUnitPrice
                .DealPrice = dDealPrice
            End With
            Return objSPrice.Save
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function AddItem(ByVal sNo_$, ByVal sName$, ByVal sModel$, ByVal sBrandNo$, ByVal sDivisionNo$, ByVal sCategoryNo$, ByVal sProductGroupNo$, _
                            ByVal iVATPercent%, ByVal sDescriptions$, ByVal dSalesPrice As Double, ByVal sVATGroup$, ByVal sUnitOfMeasure$, _
                            ByVal sManufacturerNo$, ByVal nVolume As Double, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim objItem As New adv.Business.Item
            If objItem.Load(sNo_) Then
                Throw New Exception("Item đã tồn tại")
            Else
                With objItem
                    .No_ = sNo_
                    .Name = sName
                    .Model = sModel
                    .BrandNo_ = sBrandNo
                    .DivisionNo_ = sDivisionNo
                    .CategoryNo_ = sCategoryNo
                    .ProducGroupNo_ = sProductGroupNo
                    .VATPercent = iVATPercent
                    .Descriptions = sDescriptions
                    .SalesPrice = dSalesPrice
                    .VATGroup = sVATGroup
                    .UnitOfMeasure = sUnitOfMeasure
                    Dim objProductGroup As New adv.Business.ProductGroup
                    objProductGroup.Load(sProductGroupNo)
                    .MenuGroupNo_ = objProductGroup.MenuGroupNo_
                    .MenuCategoryNo_ = objProductGroup.MenuCategoryNo_
                    .MenuDivisionNo_ = objProductGroup.MenuDivisionNo_
                    .Volume = nVolume
                    .Save()
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Function GetListItemGiftNotExists() As DataTable
        Dim SQL As String
        SQL = " SELECT [Item No_] FROM [Discount Line] WHERE [Item No_]  NOT IN (SELECT No_ FROM Item) "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    <WebMethod()> _
    Public Function UpdateInStockQty(ByVal sItemNo$, ByVal Qty%, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim SQL As String
            SQL = "UPDATE Item SET Stock = " & Qty & " WHERE No_ = '" & sItemNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function ClosedItem(ByVal sItemNo$, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim SQL As String
            SQL = "UPDATE Item SET Published = 0 WHERE No_ = '" & sItemNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function BestSelling(ByVal sItemNo$, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim SQL As String
            SQL = "UPDATE Item SET [Best Selling] = 1 WHERE No_ = '" & sItemNo & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function ResetBestSelling(ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim SQL As String
            SQL = "UPDATE Item SET [Best Selling] = 0 "
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function ItemDailySales(ByVal sItemNo$, ByVal sDate$, ByVal Qty As Integer, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim SQL As String
            SQL = " DELETE FROM [Item Daily Sales] WHERE [Item No_] = '" & sItemNo & "' AND [Order Date] = '" & sDate & "'"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            SQL = " INSERT INTO [Item Daily Sales]([Item No_],[Order Date],[Order Quantity]) " &
                    " VALUES ('" & sItemNo & "','" & sDate & "'," & Qty & ")"
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function GetSalesOrder(ByVal sConditions As String, ByVal sAuthentication As String) As DataTable
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return Nothing
        Try
            Dim SQL As String
            SQL = " SELECT No_, dbo.Char2Date([Document Date]) [Order Date], [Customer No_], [Customer Name], [Ship to Telephone], [Ship to Address], " & _
                    " dbo.Char2Date([Delivery Date]) [Delivery Date], [Delivery From], [Delivery To], [Delivery Comments], " & _
                    " [Status] = CASE [Order Status] WHEN 1 THEN N'Mới đặt hàng' WHEN 2 THEN N'Đang đóng gói tại kho' WHEN 3 THEN N'Đang đi giao' " & _
                    " WHEN 4 THEN N'Hoàn tất' WHEN 5 THEN N'Hủy' END, CONVERT(nvarchar(8),[Order Time],114) [Order Time], " & _
                    " [Bill to Name], [Bill to Address], [Tax Code] " & _
                    " FROM [Sales Header] " & _
                    " WHERE [Order Status] > 0 "
            If sConditions.Trim <> "" Then SQL &= " AND " & sConditions

            Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function GetData(ByVal sSQL As String, ByVal sAuthentication As String) As DataTable
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return Nothing
        Try
            Dim SQL As String
            SQL = sSQL
            Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ExecuteNonQuery(ByVal sSQL As String, ByVal sAuthentication As String) As String
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return "Authentication code is not correct"
        Try
            Dim SQL As String
            SQL = sSQL
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function


    <WebMethod()> _
    Public Function ApprovalSalesOrder(ByVal sOrderNo As String, ByVal sAuthentication As String) As String
        ' Cap nhat trang thai don hang
        ' Goi mail cho nhan vien chuan bi hang
        ' Tao don hang tren he thong erp
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return "Authentication code is not correct"
        Try
            Dim objSH As New adv.Business.SalesHeader
            objSH.ApprovalByHomeone(sOrderNo)

            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    <WebMethod()> _
    Public Function Delivery(ByVal sOrderNo As String, ByVal sAuthentication As String) As String
        ' Cap nhat trang thai don hang
        ' Goi mail cho nhan vien chuan bi hang
        ' Tao don hang tren he thong erp
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return "Authentication code is not correct"
        Try
            Dim objSH As New adv.Business.SalesHeader
            objSH.UpdateStatus(sOrderNo, 3)

            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    <WebMethod()> _
    Public Function FinishedOrder(ByVal sOrderNo As String, ByVal sAuthentication As String) As String
        ' Cap nhat trang thai don hang
        ' Goi mail cho nhan vien chuan bi hang
        ' Tao don hang tren he thong erp
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return "Authentication code is not correct"
        Try
            Dim objSH As New adv.Business.SalesHeader
            objSH.Finished(sOrderNo)

            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    <WebMethod()> _
    Public Function CancelOrder(ByVal sOrderNo As String, ByVal sAuthentication As String) As String
        ' Cap nhat trang thai don hang
        ' Goi mail cho nhan vien chuan bi hang
        ' Tao don hang tren he thong erp
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return "Authentication code is not correct"
        Try
            Dim objSH As New adv.Business.SalesHeader
            objSH.Cancel(sOrderNo)

            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    <WebMethod()> _
    Public Function UpdateDivision(ByVal sNo_ As String, ByVal sName As String, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim objD As New adv.Business.Division
            With objD
                .Load(sNo_)
                .No_ = sNo_
                .Name = sName
                .Save(sNo_)
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function UpdateCategory(ByVal sNo_ As String, ByVal sName As String, ByVal sDivisionNo_ As String, ByVal sMenuNo_ As String, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim objD As New adv.Business.ItemCategory
            With objD
                .Load(sNo_)
                .No_ = sNo_
                .Name = sName
                .DivisionNo_ = sDivisionNo_
                If sMenuNo_.Trim <> "" Then .MenuGroupNo_ = sMenuNo_
                .Save(sNo_)
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function UpdateProductGroup(ByVal sNo_ As String, ByVal sName As String, ByVal sDivisionNo_ As String, ByVal sCategoryNo_ As String, _
                                       ByVal sMenuNo_ As String, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim objG As New adv.Business.ProductGroup
            With objG
                .Load(sNo_)
                .No_ = sNo_
                .Name = sName
                .DivisionNo_ = sDivisionNo_
                .CategoryNo_ = sCategoryNo_
                If sMenuNo_.Trim <> "" Then
                    Dim objGmn As New adv.Business.GoodMenu
                    objGmn.Load(sMenuNo_)
                    If objGmn.MenuType = 2 Then
                        .MenuGroupNo_ = sMenuNo_
                        .MenuCategoryNo_ = objGmn.ParentNo_
                    End If
                    If objGmn.MenuType = 1 Then
                        .MenuGroupNo_ = ""
                        .MenuCategoryNo_ = sMenuNo_
                        .MenuDivisionNo_ = objGmn.ParentNo_
                    End If
                End If

                .Save(sNo_)
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function UpdateUnitOfMeasure(ByVal sNo_ As String, ByVal sName As String, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim objU As New adv.Business.UnitOfMeasure
            objU.No_ = sNo_
            objU.Name = sName
            objU.Save(sNo_)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
    Public Function UpdateBrand(ByVal sNo_ As String, ByVal sName As String, ByVal sAuthentication As String) As Boolean
        If sAuthentication.Trim <> GetAuthenticationCode() Then Return False
        Try
            Dim objB As New adv.Business.Brand
            With objB
                .No_ = sNo_
                .Name = sName
                .Save()
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class