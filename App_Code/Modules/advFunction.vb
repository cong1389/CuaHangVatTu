Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text.RegularExpressions

Public Module advFunction
    Function GetWishList(ByVal sCustomerNo As String) As String
        Dim sHtml As String = ""
        Dim SQL As String
        SQL = "SELECT * FROM [Wishlist Header] WHERE [Customer No_] = '" & sCustomerNo & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tbl.Rows.Count = 1 Then
            sHtml = tbl.Rows(0).Item("No_")
        Else
            sHtml = ""
        End If
        Return sHtml
    End Function

    Function CreateAutoWishList(ByVal sCustomerNo As String, ByVal sHomeoneGuestNo As String, ByVal sPageNo As String) As String
        Dim objW As New adv.Business.WishListHeader
        Dim objNoSeri As New adv.Business.cNoSeri
        objNoSeri.Load("WISHLIST")
        Dim sYM As String
        sYM = sLeft(Date2Char(getToday()), 6)
        Try
            With objW
                .No_ = objNoSeri.CreateNoSeri(sYM)
                .Name = "Danh sách của tôi"
                .RolesType = 0
                .Descriptions = ""
                .CustomerNo_ = sCustomerNo
                .PageNo_ = sPageNo
                .HomeoneGuestNo_ = sHomeoneGuestNo
                .Save()
            End With
            Return objW.No_
        Catch ex As Exception
            Return ""
        End Try
    End Function

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

    Function IsElectronicOrder(ByVal sCartNo As String) As Boolean
        Dim SQL As String
        SQL = "SELECT TOP 1 C.[Item No_] FROM [Cart Line] C LEFT JOIN Item I ON C.[Item No_] = I.No_ WHERE I.[Page No_] = 'ele' AND C.[Cart No_] = '" & sCartNo & "'"
        Dim sItemNo As String
        sItemNo = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        Return sItemNo <> ""
    End Function

    Function SendEmailToCustomer(ByVal sMailContent As String, ByVal sMailSubject As String, ByVal sEmail As String, Optional ByVal EmailTemplateNo_ As String = "") As String
        Dim objWM As New adv.Business.WebEmail
        Dim objC As New adv.Business.Customer
        Dim sEmailContent As String = ""
        objWM.ToAddress = sEmail
        objWM.Subject = sMailSubject
        objWM.FromAddress = GetEmailAdd()
        objWM.IsEmailBodyHtml = True

        If EmailTemplateNo_.Trim <> "" Then
            Dim objMT As New adv.Business.MsgContent
            objMT.Load(EmailTemplateNo_)
            sEmailContent = objMT.Content
            sEmailContent = sEmailContent.Replace("[ORDER]", sMailContent)
        Else
            sEmailContent = sMailContent
        End If
        objWM.EmailBody = sEmailContent
        Dim sReturn As String = ""
        sReturn = objWM.SendMail()
        Return sReturn
    End Function

    Function GetSalesPrice(ByVal sItemNo As String, ByVal sUom As String) As Double
        Dim SQL As String
        SQL = " SELECT dbo.GetSalesPrice('" & sItemNo & "', '', '" & sUom & "') "
        Dim SqlReader As IDataReader
        Dim nSalesPrice As Double = 0
        SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, Sql)
        If SqlReader.Read() Then
            nSalesPrice = SqlReader.GetDecimal(0)
            SqlReader.Close()
        Else
            SqlReader.Close()
        End If
        Return nSalesPrice
    End Function

    Function DiscountCart(ByVal sCartNo As String, ByVal nValue As Double, ByVal nDiscountType As Integer) As Boolean

        Dim arParams() As IDataParameter = New IDataParameter(2) {}
        Try
            arParams(0) = DBHelper.createParameter("@CartNo", SqlDbType.NVarChar, ParameterDirection.Input, sCartNo)
            arParams(1) = DBHelper.createParameter("@Value", SqlDbType.Int, ParameterDirection.Input, nValue)
            arParams(2) = DBHelper.createParameter("@DiscountType", SqlDbType.NVarChar, ParameterDirection.Input, nDiscountType)
        
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "DiscountCalculate", arParams)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function DelDiscountCart(ByVal sCartNo As String) As Boolean

        Dim arParams() As IDataParameter = New IDataParameter(0) {}
        Try
            arParams(0) = DBHelper.createParameter("@CartNo", SqlDbType.NVarChar, ParameterDirection.Input, sCartNo)
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "DiscountDelete", arParams)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function CheckIsDate(ByVal strDate As String) As Boolean
        Dim pDay As Integer
        Dim pMonth As Integer
        Dim pYear As Integer
        Dim pDate As Date
        If strDate = "" Then Return False
        pDay = CInt(sLeft(strDate, 2))
        pMonth = CInt(sMid(strDate, 4, 2))
        pYear = CInt(sRight(strDate, 4))

        If pMonth > 12 Or pMonth < 1 Then Return False
        If pDay < 1 Or pDay > 31 Then Return False
        Dim pNumOfDay As Integer
        pNumOfDay = Date.DaysInMonth(pYear, pMonth)
        If pDay > pNumOfDay Then Return False
        Return True
    End Function

    Function GetDetailPayment(ByVal sCartNo As String) As String
        Dim SQL As String
        Dim sHtml As String = ""

        SQL = " SELECT SUM([Discount Amount]) FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "'"
        Dim nDiscountAmount As Double = 0
        nDiscountAmount = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)

        SQL = "SELECT P.[Line No_], P.[Tender Type], T.Name, P.Amount " & _
                " FROM [Payment Entry] P LEFT JOIN [Tender Type] T ON P.[Tender Type] = T.No_ " & _
                " WHERE P.[Payment Type] = 0 AND P.[Cart No_] = '" & sCartNo & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tbl.Rows.Count = 0 And Math.Round(nDiscountAmount) = 0 Then Return ""
        Dim nIJ As Integer
        sHtml &= "<table width=""100%"">" & vbCrLf
        If nDiscountAmount <> 0 Then
            sHtml &= "<tr>"
            sHtml &= "<td style=""width:200px;height:20px;"" align=""right"">"
            sHtml &= "Chiết khấu"
            sHtml &= "</td>"
            sHtml &= "<td style=""width:35px;"" align=""right"">"
            sHtml &= "<a href=""#"" onclick=""deldiscount(); return false;"">[ hủy ]</a>"
            sHtml &= "</td>"
            sHtml &= "<td style=""width:110px;"" align=""right"">"
            sHtml &= Format(Math.Round(nDiscountAmount, 0), "##,###,###").Replace(",", ".")
            sHtml &= "</td>"
            sHtml &= "</tr>"
        End If
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr>"
            sHtml &= "<td style=""width:200px;height:20px;"" align=""right"">"
            sHtml &= tbl.Rows(nIJ).Item("Name")
            sHtml &= "</td>"
            sHtml &= "<td style=""width:35px;"" align=""right"">"
            sHtml &= "<a href=""#"" onclick=""delpayment('" & tbl.Rows(nIJ).Item("Line No_") & "'); return false;"">[ hủy ]</a>"
            sHtml &= "</td>"

            sHtml &= "<td style=""width:110px;"" align=""right"">"
            sHtml &= Format(Math.Round(tbl.Rows(nIJ).Item("Amount"), 0), "##,###,###").Replace(",", ".")
            sHtml &= "</td>"
            sHtml &= "</tr>"
        Next

        SQL = " SELECT SUM(A.AmountRemaining) FROM " & _
            " (SELECT SUM([Amount Inc VAT]) - SUM([Discount Amount]) AmountRemaining FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "' " & _
            " UNION ALL " & _
            " SELECT - ISNULL(SUM(Amount),0) AmountRemaining FROM [Payment Entry] WHERE [Payment Type] = 0 AND [Cart No_] = '" & sCartNo & "') A "
        Dim nRemainingAmount As Double = 0
        nRemainingAmount = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
        sHtml &= "<tr>"
        sHtml &= "<td style=""width:200px;height:20px;font-size:14px;"" align=""right"" colspan=""2""><b>"
        sHtml &= "Số tiền khách còn phải trả:"
        sHtml &= "</b></td>"
        sHtml &= "<td style=""width:110px;font-size:14px;"" align=""right""><b>"
        sHtml &= Format(Math.Round(nRemainingAmount, 0), "##,###,###").Replace(",", ".")
        sHtml &= "</b></td>"
        sHtml &= "</tr>"
        sHtml &= "</table>"
        Return sHtml
    End Function

    Function ShowDetailPayment(ByVal sOrderNo As String) As String
        Dim SQL As String
        Dim sHtml As String = ""

        SQL = " SELECT SUM([Discount Amount]) FROM [Sales Line] WHERE [Document No_] = '" & sOrderNo & "'"
        Dim nDiscountAmount As Double = 0
        nDiscountAmount = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)

        SQL = "SELECT P.[Line No_], P.[Tender Type], T.Name, P.Amount " & _
                " FROM [Payment Entry] P LEFT JOIN [Tender Type] T ON P.[Tender Type] = T.No_ " & _
                " WHERE P.[Payment Type] = 0 AND P.[Order No_] = '" & sOrderNo & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tbl.Rows.Count = 0 And Math.Round(nDiscountAmount) = 0 Then Return ""
        Dim nIJ As Integer
        sHtml &= "<table width=""100%"">" & vbCrLf
        If nDiscountAmount <> 0 Then
            sHtml &= "<tr>"
            sHtml &= "<td style=""height:20px;"" align=""right"">"
            sHtml &= "Chiết khấu"
            sHtml &= "</td>"

            sHtml &= "<td style=""width:110px;padding-right:10px;"" align=""right"">"
            sHtml &= Format(Math.Round(nDiscountAmount, 0), "##,###,###").Replace(",", ".")
            sHtml &= "</td>"
            sHtml &= "</tr>"
        End If
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr>"
            sHtml &= "<td style=""height:20px;"" align=""right"">"
            sHtml &= tbl.Rows(nIJ).Item("Name")
            sHtml &= "</td>"
            sHtml &= "<td style=""width:110px;padding-right:10px;"" align=""right"">"
            sHtml &= Format(Math.Round(tbl.Rows(nIJ).Item("Amount"), 0), "##,###,###").Replace(",", ".")
            sHtml &= "</td>"
            sHtml &= "</tr>"
        Next

        SQL = " SELECT SUM(A.AmountRemaining) FROM " & _
            " (SELECT SUM([Amount Inc VAT]) - SUM([Discount Amount]) AmountRemaining FROM [Sales Line] WHERE [Document No_] = '" & sOrderNo & "' " & _
            " UNION ALL " & _
            " SELECT - ISNULL(SUM(Amount),0) AmountRemaining FROM [Payment Entry] WHERE [Payment Type] = 0 AND [Order No_] = '" & sOrderNo & "') A "
        Dim nRemainingAmount As Double = 0
        nRemainingAmount = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
        sHtml &= "<tr>"
        sHtml &= "<td style=""height:20px;font-size:14px;"" align=""right""><b>"
        sHtml &= "Số tiền khách còn phải trả:"
        sHtml &= "</b></td>"
        sHtml &= "<td style=""width:110px;font-size:14px;padding-right:10px;"" align=""right""><b>"
        sHtml &= Format(Math.Round(nRemainingAmount, 0), "##,###,###").Replace(",", ".")
        sHtml &= "</b></td>"
        sHtml &= "</tr>"
        sHtml &= "</table>"
        Return sHtml
    End Function

    Function ShowProductCompare(ByVal sGuestID As String) As String
        Dim SQL As String
        Dim sHtml As String = ""
        Dim sImgUrl As String = GetImgUrl()
        Dim nNumItem As Integer
        SQL = "SELECT I.No_, I.[Images Thum URL] FROM [Compare Product] P LEFT JOIN Item I ON P.[Item No_] = I.No_ WHERE P.GuestID = '" & sGuestID & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        nNumItem = tbl.Rows.Count
        If tbl.Rows.Count < 4 Then
            For nIJ = tbl.Rows.Count To 3
                tbl.Rows.Add(tbl.NewRow)
            Next
        End If
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<div class=""itemcompare"">"
            If ReturnIfNull(tbl.Rows(nIJ).Item("Images Thum URL"), "").ToString.Trim <> "" Then
                sHtml &= "<div style=""margin-top:3px;""><img border=""0"" src=""" & sImgUrl & "Images/Product/" & tbl.Rows(nIJ).Item("Images Thum URL") & """ width=""30"" height=""27"" align=""absmiddle""></div>"
                sHtml &= "<a href=""#"" onclick=""removecompare('" & tbl.Rows(nIJ).Item("No_") & "')""><span class=""remove""></span></a>"
            End If
            sHtml &= "</div>"
        Next
        If nNumItem < 2 Then
            sHtml = "<div style=""float:left; margin-right:10px;height:20;padding-top:8px;"">So sách 4 sản phẩm:</div>" & sHtml
        Else
            sHtml &= "<div style=""float:left;""><a class=""basketbt"" href=""" & GetUrl() & "so-sanh-san-pham/"">So sánh</a></div>"
        End If
        Return sHtml
    End Function

    Sub AddItemCompare(ByVal sGuestID As String, ByVal sItemNo As String)
        Dim objItem As New adv.Business.Item
        objItem.Load(sItemNo)
        Dim SQL As String
        SQL = "SELECT ISNULL(COUNT([Item No_]),0) FROM [Compare Product] WHERE [GuestID] = '" & sGuestID & "'"
        If DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL) >= 4 Then Exit Sub

        SQL = "INSERT INTO [Compare Product]([GuestID],[Item No_],[Category No_])VALUES('" & sGuestID & "','" & sItemNo & "','" & objItem.MenuCategoryNo_ & "')"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)

    End Sub

    Sub RemoveItemCompare(ByVal sGuestID As String, ByVal sItemNo As String)
        Dim objItem As New adv.Business.Item
        Dim SQL As String
        SQL = "DELETE FROM [Compare Product] WHERE [GuestID] = '" & sGuestID & "' AND [Item No_] = '" & sItemNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
    End Sub

    Function CreateLink(ByVal sDescriptions As String) As String
        Dim sValue As String
        sValue = ConvertIntoNone(sDescriptions)
        sValue = sValue.Replace("&", "")
        sValue = sValue.Replace("*", "")
        sValue = sValue.Replace("/", "")
        sValue = sValue.Replace("\", "")
        sValue = sValue.Replace("""", "")
        sValue = sValue.Replace("'", "")
        sValue = sValue.Replace("(", "")
        sValue = sValue.Replace(")", "")
        sValue = sValue.Replace("[", "")
        sValue = sValue.Replace("]", "")
        sValue = sValue.Replace("@", "")
        sValue = sValue.Replace("=", "")
        sValue = sValue.Replace("+", "")
        sValue = sValue.Replace("%", "")
        sValue = sValue.Replace("<", "")
        sValue = sValue.Replace(">", "")
        sValue = sValue.Replace("?", "")
        Return sValue
    End Function
End Module

