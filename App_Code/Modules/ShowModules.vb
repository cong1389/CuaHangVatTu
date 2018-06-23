Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text.RegularExpressions


Public Module ShowModules

    Function ShowPrice(ByVal nPrice As Double) As String
        Dim sPrice As String, sUrl As String = GetUrl()
        sPrice = Format(nPrice / 1000, "##,###,###,###").Trim
        Dim nIJ As Integer
        sPrice = sPrice.Replace(",", ".")
        Dim sHtml As String = ""
        For nIJ = 1 To sPrice.Length
            sHtml &= "<img border=""0"" src=""" & sUrl & "/Images/Template/" & sMid(sPrice, nIJ, 1) & ".png"">"
        Next
        Return sHtml
    End Function

    Function ShowItemListSide(ByVal sItemNo As String, ByVal nWithDiv As Integer, ByVal nWithImg As Integer) As String
        Dim objItem As New adv.Business.Item
        Dim sHtml As String = ""
        Dim sUrl As String = GetUrl()
        Dim sImgUrl As String = GetImgUrl()
        Dim nOriginPrice As Double, nDealPrice As Double, sGiftDescriptions As String = "", sPromotionNo As String = ""
        Dim sItemLink As String = ""

        objItem.Load(sItemNo)
        If objItem.PageNo = "apl" Then
            sItemLink = "san-pham-tieu-dung/"
        End If
        If objItem.PageNo = "ele" Then
            sItemLink = "san-pham-dien-may/"
        End If
        sItemLink = sUrl & sItemLink
        sItemLink &= IIf(ReturnIfNull(objItem.LinkUrl, "").ToString.Trim = "", objItem.No_, objItem.LinkUrl)
        objItem.GetSalesPrice(sItemNo, nOriginPrice, nDealPrice, sGiftDescriptions, sPromotionNo)

        sHtml &= "<div style=""margin-top:10px;"">" & vbCrLf
        sHtml &= "<div style=""float:left;width:" & nWithImg & "px;"">" & vbCrLf
        sHtml &= "<a href=""" & sItemLink & """ >" & vbCrLf
        sHtml &= "<img border=""0"" src=""" & sImgUrl & "Images/Product/" & objItem.ImagesThumURL & """ width=""80"" height=""64"" align=""absmiddle"" alt=""" & objItem.Name & """>" & vbCrLf
        sHtml &= "</a>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "<div style=""margin-left:" & (nWithImg + 5) & "px;"">" & vbCrLf
        sHtml &= "<div style=""overflow: hidden;height:30px;width: 180px;"">" & vbCrLf
        sHtml &= "<a class=""linktitle"" href=""" & sItemLink & """ >" & vbCrLf
        sHtml &= objItem.Name & vbCrLf
        sHtml &= "</a>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        If nOriginPrice > nDealPrice And nDealPrice <> 0 Then
            sHtml &= "<div class=""salesprice"" style=""margin-top:4px;"">" & Format(nDealPrice, "##,###,###,###").Replace(",", ".") & " VNĐ</div>" & vbCrLf
        Else
            sHtml &= "<div class=""salesprice"" style=""margin-top:4px;"">" & Format(nOriginPrice, "##,###,###,###").Replace(",", ".") & " VNĐ</div>" & vbCrLf
        End If
        sHtml &= "</div>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "<div style = ""clear:both;""></div>" & vbCrLf
        Return sHtml

    End Function

    Function ShowItem(ByVal ItemNo_ As String, ByVal iNum As Integer, Optional ByVal nWidth As Integer = 0, Optional ByVal nMargin As Integer = 0, Optional ByVal iShowCompare As Integer = 0, Optional ByVal iChecked As Integer = 0, Optional ByVal sPrefix As String = "") As String
        Dim objItem As New adv.Business.Item
        Dim sHtml As String = ""
        Dim sUrl As String = GetUrl()
        Dim sImgUrl As String = GetImgUrl()
        Dim nOriginPrice As Double, nDealPrice As Double, sGiftDescriptions As String = "", sPromotionNo As String = ""
        Dim sItemLink As String = ""
        objItem.GetSalesPrice(ItemNo_, nOriginPrice, nDealPrice, sGiftDescriptions, sPromotionNo)
        objItem.Load(ItemNo_)

        sItemLink = "san-pham/"

        sItemLink = sUrl & sItemLink

        sItemLink &= IIf(ReturnIfNull(objItem.LinkUrl, "").ToString.Trim = "", objItem.No_, objItem.LinkUrl)

        Dim sStyle As String
        sStyle = "Style="""
        If nWidth <> 0 Then
            sStyle &= "width:" & nWidth & "px;"
        End If

        If iNum <> 0 Then
            If nMargin <> 0 Then
                sStyle &= "margin-left:" & nMargin & "px;"
            End If
        Else
            sStyle &= "margin-left:0px;"
        End If

        sStyle &= """"

        sHtml &= "<div class=""item"" " & sStyle & " id=""" & sPrefix & ItemNo_ & """ onmouseover=""ItemMouseOver(this)"" onmouseout = ""ItemMouseOut(this)"">" & vbCrLf


        If nOriginPrice > nDealPrice And nDealPrice <> 0 Then
            If (nOriginPrice - nDealPrice) * 100 / nOriginPrice >= 2 Then
                sHtml &= "<div class=""salesoff"">" & vbCrLf
                sHtml &= Math.Round((nOriginPrice - nDealPrice) * 100 / nOriginPrice, 0) & "%<br />off"
                sHtml &= "</div>" & vbCrLf
            End If
        End If
        If sGiftDescriptions.Trim <> "" Then
            sHtml &= "<div class=""giftsmall"">" & vbCrLf
            sHtml &= "</div>" & vbCrLf
        End If
        If objItem.NewProduct = 1 Then
            sHtml &= "<div class=""newproduct"">" & vbCrLf
            sHtml &= "</div>" & vbCrLf
        End If
        sHtml &= "<div class=""p-image"">" & vbCrLf
        sHtml &= "<a class=""linktitle"" href=""" & sItemLink & """ >" & vbCrLf
        sHtml &= "<img border=""0"" src=""" & sImgUrl & "Images/Product/" & objItem.ImagesThumURL & """ height=""170"" align=""absmiddle"" alt=""" & objItem.Name & ";" & objItem.MetaKeywords & """>" & vbCrLf
        sHtml &= "</a>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "<div class=""product_title"">" & vbCrLf
        sHtml &= "<a class=""linktitle"" href=""" & sItemLink & """ >" & objItem.Name & "</a>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "<div style=""margin-top:5px;""><center>"
        sHtml &= "SKU: " & objItem.No_
        'sHtml &= ShowItemRating(ItemNo_)
        sHtml &= "</center></div>"
        sHtml &= "<div class=""p-info"" style=""padding-top:5px;"">" & vbCrLf

        If nDealPrice < nOriginPrice And nDealPrice <> 0 Then
            sHtml &= "<div class=""item_price"">" & vbCrLf
            sHtml &= "<div ><center><span class=""originprice"">" & Format(nOriginPrice, "##,###,###,###").Replace(",", ".") & "</span></center></div>"
            sHtml &= "</div>" & vbCrLf
        Else
            sHtml &= "<div class=""item_desc"">" & vbCrLf
            sHtml &= "</div>" & vbCrLf
        End If
        If nOriginPrice <> 0 Then
            sHtml &= "<div id=""pn_" & sPrefix & objItem.No_ & """ class=""panelorder"" style=""display:none;"">" & vbCrLf
            sHtml &= "<center><input id=""cmd_" & objItem.No_ & """ type=""button"" class=""w8smallbutton"" value=""ĐẶT HÀNG NHANH"" onclick=""Order_Click('" & objItem.No_ & "')""/></center>"
            sHtml &= "</div>" & vbCrLf
        End If
        sHtml &= "<div>" & vbCrLf
        If nDealPrice <> 0 Then
            sHtml &= "<center><span class=""salesprice"" >" & Format(nDealPrice, "##,###,###,###").Replace(",", ".") & " VNĐ</span></center>"
        Else
            If nOriginPrice = 0 Then
                sHtml &= "<center><span class=""salesprice"" >Gọi giá</span></center>"
            Else
                sHtml &= "<center><span class=""salesprice"" >" & Format(nOriginPrice, "##,###,###,###").Replace(",", ".") & " VNĐ</span></center>"
            End If

        End If

        sHtml &= "</div>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        If iShowCompare = 1 Then
            sHtml &= "<div style=""margin-top:10px;"">" & vbCrLf
            sHtml &= "<input id=""cpck" & ItemNo_ & """ name=""compareck"" type=""checkbox"" " & IIf(iChecked = 1, " checked=""checked""", "") & " value=""" & ItemNo_ & """ onchange=""Compare_Click(this)""> So sánh"
            sHtml &= "</div>" & vbCrLf
        End If
        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowCartApl(ByVal sCartNo As String) As String
        Dim sHtml As String = ""

        sHtml &= "<div id=""contentWrapRight"" class=""basket"" >"
        sHtml &= "        <div id=""basketWrap"">"
        sHtml &= "            <div id=""basketTitleWrap"" class=""head"">"
        sHtml &= "            <a href=""" & GetUrl() & "gio-hang/"">GIỎ HÀNG</a> | <a href=""" & GetUrl() & "thanh-toan/"">TÍNH TIỀN</a><span id=""notificationsLoader""></span>"
        sHtml &= "            </div>"
        sHtml &= "            <div id=""basketItemsWrap"" >"
        sHtml &= ShowCartDetail(sCartNo)
        sHtml &= "            </div>"
        sHtml &= "        </div>"
        sHtml &= "    </div>"

        Return sHtml
    End Function

    Function ShowCartDetail(ByVal sCartNo As String) As String
        Dim SQL As String
        Dim sHtml As String = ""
        Dim tbl As DataTable
        Dim objItem As New adv.Business.Item
        Dim nIJ As Integer, nTotal As Double = 0
        SQL = " SELECT L.[Cart No_], L.[Line No_], L.[Item No_], RTRIM(I.Name) + ' ' + RTRIM(L.Variants) [Item Name], L.Quantity, L.[Unit Price], L.[Amount Inc VAT], L.Descriptions, L.[Line Type] " & _
                " FROM [Cart Line] L INNER JOIN Item I ON L.[Item No_] = I.No_ " & _
                " WHERE L.[Cart No_] = '" & sCartNo & "' ORDER BY L.[Line No_] DESC "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        sHtml &= "<div class=""bod"">"
        For nIJ = 0 To tbl.Rows.Count - 1
            If nIJ <> 0 Then
                sHtml &= "<div style=""margin-top:5px;border-top:1px solid #afafaf;padding-top:4px;"">"
            Else
                sHtml &= "<div style=""margin-top:5px;"">"
            End If

            sHtml &= "<div id='" & tbl.Rows(nIJ).Item("Line No_") & "' class=""itemname"">"
            objItem.Load(tbl.Rows(nIJ).Item("Item No_"))
            sHtml &= tbl.Rows(nIJ).Item("Item Name")
            sHtml &= "</div>"
            sHtml &= "<div class=""del"">"
            If tbl.Rows(nIJ).Item("Line Type") = 0 Then
                sHtml &= " <a href=""Handler.ashx?action=DelFromBasket&LineNo=" & tbl.Rows(nIJ).Item("Line No_") & """ onClick=""DelCartLine('" & tbl.Rows(nIJ).Item("Line No_") & "');return false;"">"
                sHtml &= "<img src=""" & GetUrl() & "Images/Template/delrow.png"" id=""" & tbl.Rows(nIJ).Item("Cart No_") & "_" & tbl.Rows(nIJ).Item("Line No_") & """>"
                sHtml &= "</a>"
            End If
            sHtml &= "</div>"
            sHtml &= "</div>"
            sHtml &= "<div>"
            sHtml &= "<div class=""columnqty"" >"
            sHtml &= tbl.Rows(nIJ).Item("Quantity")
            sHtml &= "</div>"
            sHtml &= "<div class=""columnprice"" >"
            sHtml &= Format(Math.Round(tbl.Rows(nIJ).Item("Unit Price"), 0), "##,###,###").Replace(",", ".")
            sHtml &= "</div>"
            sHtml &= "<div class=""columnamount"" >"
            sHtml &= Format(Math.Round(tbl.Rows(nIJ).Item("Amount Inc VAT"), 0), "##,###,###").Replace(",", ".")
            sHtml &= "</div>"
            sHtml &= "</div>"
            sHtml &= "<div style=""clear:both;""></div>"
            nTotal += tbl.Rows(nIJ).Item("Amount Inc VAT")
        Next
        sHtml &= "</div>"
        If nTotal = 0 Then
            sHtml &= "<div class=""foot"">"
            sHtml &= "<b>Chưa có mặt hàng nào</b>"
        Else
            sHtml &= "<div class=""foot"" style=""text-align:right;margin-top:5px;border-top:1px solid #afafaf;padding-top:4px;"">"
            sHtml &= "<b>Tổng cộng: " & Format(Math.Round(nTotal), "##,###,###").Replace(",", ".") & "</b>"
            sHtml &= "</div>"
            sHtml &= "<div style=""margin-top:5px;background:#333333;color:#fff;padding:4px;"">"
            sHtml &= "Click GIỎ HÀNG để thay đổi <br />"
            sHtml &= "Click TÍNH TIỀN để thanh toán <br />"
           
            sHtml &= "</div>"
        End If

        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowCartInfo(ByVal sCartNo As String) As String
        Dim sHtml As String = ""
        sHtml = "<div ID=""advcart"">" & GetCart(sCartNo) & "</div>"
        Return sHtml
    End Function

    Function GetCart(ByRef sCartNo As String) As String

        Dim sHtml As String = ""
        Dim SQL As String, nItem As Integer = 0, nAmount As Double = 0

        If sCartNo.Trim <> "" Then
            SQL = " SELECT ISNULL(SUM(Quantity),0) Quantity, IsNull(SUM([Amount Inc VAT]),0) Amount FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "'"
            Dim SqlReader As IDataReader
            SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, SQL)
            If SqlReader.Read() Then
                nItem = SqlReader.GetInt32(0)
                nAmount = SqlReader.GetDecimal(1)
            End If
        End If
        Dim sCart As String
        sCart = "(" & nItem & ")"
        If nAmount <> 0 Then
            sCart &= "&nbsp;&nbsp;&nbsp;&nbsp;" & Format(Math.Round(nAmount, 0), "##,###,###").Replace(",", ".") & " Đ"
        Else
            sCart &= "&nbsp;&nbsp;&nbsp;&nbsp;" & "GIỎ HÀNG"
        End If
        sHtml = "<div class=""drop""><a class=""cart"" style=""position: absolute;""><div id=""carthead"">" & sCart & "</div></a>"
        sHtml &= "<div id=""cartbody"" class=""cartpanel"">"
        sHtml &= ShowCartDetail(sCartNo, 0)
        sHtml &= "</div>"
        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowCartTotal(ByVal sCartNo As String) As String
        Dim sHtml As String = ""
        Dim SQL As String, nItem As Integer = 0, nAmount As Double = 0

        If sCartNo.Trim <> "" Then
            SQL = " SELECT ISNULL(SUM(Quantity),0) Quantity, IsNull(SUM([Amount Inc VAT]),0) Amount FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "'"
            Dim SqlReader As IDataReader
            SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, SQL)
            If SqlReader.Read() Then
                nItem = SqlReader.GetInt32(0)
                nAmount = SqlReader.GetDecimal(1)
            End If
        End If
        Dim sCart As String
        sCart = "(" & nItem & ")"
        If nAmount <> 0 Then
            sCart &= "&nbsp;&nbsp;&nbsp;&nbsp;" & Format(Math.Round(nAmount, 0), "##,###,###").Replace(",", ".") & " Đ"
        Else
            sCart &= "&nbsp;&nbsp;&nbsp;&nbsp;" & "GIỎ HÀNG"
        End If
        Return sCart
    End Function

    Function ShowCartDetail(ByVal sCartNo As String, ByRef nNumRows As Integer) As String
        Dim SQL As String
        Dim sHtml As String = ""
        Dim tbl As DataTable
        Dim objItem As New adv.Business.Item
        Dim nIJ As Integer, nTotal As Double = 0, nTotalQty As Integer = 0
        SQL = " SELECT L.[Cart No_], L.[Line No_], L.[Item No_], RTRIM(I.Name) + ' ' + RTRIM(L.Variants) [Item Name], L.Quantity, L.[Unit Price], L.[Amount Inc VAT], L.Descriptions, L.[Line Type] " & _
                " FROM [Cart Line] L INNER JOIN Item I ON L.[Item No_] = I.No_ " & _
                " WHERE L.[Cart No_] = '" & sCartNo & "' ORDER BY L.[Line No_] DESC "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        nNumRows = tbl.Rows.Count


        If tbl.Rows.Count <> 0 Then

            For nIJ = 0 To tbl.Rows.Count - 1
                If nIJ <> 0 Then
                    sHtml &= "<div style=""margin-top:2px;border-top:1px solid #afafaf;padding-top:4px;"">"
                Else
                    sHtml &= "<div style=""margin-top:2px;min-height:24px;"">"
                End If

                sHtml &= "<div id='" & tbl.Rows(nIJ).Item("Line No_") & "' class=""itemname"">"
                objItem.Load(tbl.Rows(nIJ).Item("Item No_"))
                sHtml &= tbl.Rows(nIJ).Item("Item Name")
                sHtml &= "</div>"
                sHtml &= "<div class=""del"">"
                If tbl.Rows(nIJ).Item("Line Type") = 0 Then
                    sHtml &= " <a href=""#"" onClick=""DelCartLine('" & tbl.Rows(nIJ).Item("Line No_") & "');return false;"">"
                    sHtml &= "<img src=""" & GetUrl() & "Images/Template/delrow.png"" id=""" & tbl.Rows(nIJ).Item("Cart No_") & "_" & tbl.Rows(nIJ).Item("Line No_") & """>"
                    sHtml &= "</a>"
                End If
                sHtml &= "</div>"
                sHtml &= "</div>"
                sHtml &= "<div style=""clear:both;""></div>"
                sHtml &= "<div>"
                sHtml &= "<div class=""columnqty"" >"
                sHtml &= tbl.Rows(nIJ).Item("Quantity")
                nTotalQty += tbl.Rows(nIJ).Item("Quantity")
                sHtml &= "</div>"
                sHtml &= "<div class=""columnprice"" >"
                sHtml &= Format(Math.Round(tbl.Rows(nIJ).Item("Unit Price"), 0), "##,###,###").Replace(",", ".")
                sHtml &= "</div>"
                sHtml &= "<div class=""columnamount"" >"
                sHtml &= Format(Math.Round(tbl.Rows(nIJ).Item("Amount Inc VAT"), 0), "##,###,###").Replace(",", ".")
                sHtml &= "</div>"
                sHtml &= "</div>"
                sHtml &= "<div style=""clear:both;""></div>"
                nTotal += tbl.Rows(nIJ).Item("Amount Inc VAT")
            Next
            sHtml &= "<div style=""margin-top:2px;border-top:1px solid #afafaf;"">"
            sHtml &= "</div>"
        End If
        If nTotal = 0 Then
            sHtml &= "<div class=""foot"" style=""padding:5px;"">"
            sHtml &= "<b>Chưa có mặt hàng nào</b>"
            sHtml &= "</div>"
        Else
            sHtml &= "<div class=""foot"" style=""text-align:right;padding:4px;margin-top:1px;"">"
            sHtml &= "<b>S.lượng: " & Format(Math.Round(nTotalQty), "##,###,###").Replace(",", ".") & " - Thành tiền: " & Format(Math.Round(nTotal), "##,###,###").Replace(",", ".") & "</b>"
            sHtml &= "</div>"
            sHtml &= " <div style=""padding-top:10px;height:28px;"">"
            sHtml &= "      <a class=""basketbt"" style=""float:left;margin-left:4px;width:85px;"" href=""" & GetUrl() & "gio-hang/"">XEM GIỎ HÀNG</a>"
            sHtml &= "      <a class=""basketbt"" style=""float:right;margin-right:4px;width:75"" href=""" & GetUrl() & "thanh-toan/"">TÍNH TIỀN</a><span id=""notificationsLoader""></span>"
            sHtml &= "</div>"
        End If


        Return sHtml
    End Function

    Function ShowMenuAndSub(ByVal sMenuNo As String, ByVal sImgUrl As String) As String
        Dim sHtml As String = ""
        Dim objGmn As New adv.Business.GoodMenu
        objGmn.Load(sMenuNo)
        sHtml &= "<div class=""DivisionBox"">"
        sHtml &= "<div class=""Img""><img src="""
        sHtml &= GetUrl() & "Images/ShowList/Menu/" & sImgUrl
        sHtml &= """></div>"
        sHtml &= "<div class=""Title"">"
        sHtml &= objGmn.Name.Replace("<br />", " ").Replace("<br>", " ").Replace("<br/>", " ")
        sHtml &= "</div>"
        sHtml &= "<div class=""SubMenu"">"
        sHtml &= "<div style=""height:4px;background-color:#F59C10;""></div>"
        sHtml &= "<div class=""Content"">"
        sHtml &= ShowSubOfmenu(sMenuNo, 10)
        sHtml &= "</div>"
        sHtml &= "</div>"
        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowSubOfmenu(ByVal sMenuNo As String, ByVal nNumItem As Integer) As String
        Dim SQL As String
        Dim objGmn As New adv.Business.GoodMenu
        objGmn.Load(sMenuNo)
        SQL = " SELECT TOP " & nNumItem & " * FROM [Good Menu] WHERE [Parent No_] = '" & sMenuNo & "' ORDER BY [Menu Order]"
        Dim sHtml As String = ""
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<div style=""height:22px;"">"
            sHtml &= "<a href=""" & GetUrl() & tbl.Rows(nIJ).Item("Page No_") & "/" & objGmn.LinkDisplay & "/" & tbl.Rows(nIJ).Item("Link Display") & "/"">"
            sHtml &= tbl.Rows(nIJ).Item("Name")
            sHtml &= "</a>"
            sHtml &= "</div>"
        Next


        Return sHtml
    End Function


    Function ShowItemRating(ByVal sItemNo As String) As String
        Dim objCustomerReview As New adv.Business.CustomerReview
        Dim sHtml As String, nNumOfReview As Integer
        sHtml = ShowRating(objCustomerReview.GetRating(sItemNo))
        nNumOfReview = objCustomerReview.GetNumOfRating(sItemNo)
        If nNumOfReview = 0 Then
            sHtml &= "(0)"
        Else
            sHtml &= "(" & nNumOfReview & ")"
        End If
        Return sHtml
    End Function

    Function ShowCamKet() As String
        Dim sUrl As String = GetUrl()
        Dim sHtml As String = ""
        sHtml &= "<div class=""containerleft"">" & vbCrLf
        sHtml &= "<div class=""TitleLine"">" & GetCompany() & " cam kết </div>" & vbCrLf
        sHtml &= "<div style=""min-height:90px;padding:5px;"">" & vbCrLf
        sHtml &= "<table width=""100%"">" & vbCrLf
        sHtml &= "<tr>" & vbCrLf
        sHtml &= "<td style=""width:32px; height:45px;"">" & "<img border=""0"" src=""" & sUrl & "Images/Template/origin.png"">" & "</td>" & vbCrLf
        sHtml &= "<td>Hàng chính hãng</td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf
        sHtml &= "<tr>" & vbCrLf
        sHtml &= "<td style=""width:32px;height:45px;"">" & "<img border=""0"" src=""" & sUrl & "Images/Template/delivery.png"">" & "</td>" & vbCrLf
        sHtml &= "<td><a href=""" & GetUrl() & "thong-tin/chinh-sach-ban-hang"">Giao hàng miễn phí</a></td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf
        sHtml &= "<tr>" & vbCrLf
        sHtml &= "<td style=""width:32px;height:45px;"">" & "<img border=""0"" src=""" & sUrl & "Images/Template/pay.png"">" & "</td>" & vbCrLf
        sHtml &= "<td>Thanh toán trực tiếp khi nhận hàng</td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf

        sHtml &= "</table> " & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        Return sHtml
    End Function

    Function ShowCamKetHeader() As String
        Dim sUrl As String = GetUrl()
        Dim sHtml As String = ""
        sHtml = "<img border=""0"" src=""" & sUrl & "Images/Template/Commitment.jpg"" >"
        Return sHtml
    End Function

    Function ShowMenuAccount(ByVal sCustomerNo As String) As String
        Dim sHtml As String = ""
        Dim sUrl As String = GetUrl()
        Dim SQL As String
        sHtml &= "<div id=""toplink"">"
        If ReturnIfNull(sCustomerNo, "").ToString.Trim = "" Then
            SQL = " SELECT * FROM [Link Menu] WHERE [Position No_] = '01' AND ([Login Status] = 0 OR [Login Status] = 1) ORDER BY [Order Position] "
        Else
            SQL = " SELECT * FROM [Link Menu] WHERE [Position No_] = '01' AND ([Login Status] = 0 OR [Login Status] = 2) ORDER BY [Order Position] "
        End If
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer = 0
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= IIf(nIJ > 0, "|", "") & "<span style=""margin-left:5px;margin-right:5px;""><a  href=""" & tbl.Rows(nIJ).Item("URL Link").ToString.Replace("~/", GetUrl) & """ > " & tbl.Rows(nIJ).Item("Name") & "</a></span>"
        Next
        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowVariantSelect(ByVal sItemNo As String) As String
        Dim sHtml As String = ""
        Dim SQL As String
        SQL = "SELECT * FROM [Item Variants] WHERE [Item No_] = '" & sItemNo & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tbl.Rows.Count = 0 Then Return ""
        Dim nIJ As Integer
        sHtml &= "<fieldset style=""margin:0px;padding:0 0 5px 5px;"">"
        sHtml &= "<legend >Chọn size</legend>"
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<input type=""radio"" name=""size_" & sItemNo & """ id=""size_" & tbl.Rows(nIJ).Item("Variant No_") & """ value=""" & tbl.Rows(nIJ).Item("Variant No_") & """>"
            sHtml &= tbl.Rows(nIJ).Item("Variant No_") & " &nbsp;&nbsp;&nbsp;"
        Next
        sHtml &= "</fieldset>"
        Return sHtml
    End Function

    Function ShowOrder(ByVal sOrderNo As String, ByVal ShowAmount As Boolean) As String
        Dim objSH As New adv.Business.SalesHeader
        Dim sHtml As String = ""
        If Not objSH.Load(sOrderNo) Then Return ""

        sHtml &= "<div>"
        sHtml &= "<div style=""margin-top:10px;font-size:18px;""><center><b>ĐƠN ĐẶT HÀNG</b></center></div>"

        sHtml &= "<div style=""margin-top:10px;"">"
        sHtml &= "<div class='col-lg-6 col-md-6 col-sm-12 col-xs-12' style=""margin-top:10px;padding:10px;border:1px solid #000;"">"
        sHtml &= "<div style=""margin-top:5px"">Số đơn hàng: " & objSH.No_ & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Ngày hàng: " & Char2Date(objSH.DocumentDate) & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Mã khách hàng: " & objSH.CustomerNo_ & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Tên khách hàng: " & objSH.CustomerName & "</div>"
        sHtml &= "</div>"

        sHtml &= "<div class='col-lg-6 col-md-6 col-sm-12 col-xs-12' style=""margin-top:10px;padding:10px;border:1px solid #000;"">"
        sHtml &= "<div style=""margin-top:5px"">Địa chỉ giao hàng: </div>"
        sHtml &= "<div style=""margin-top:5px;margin-left:30px;"">" & objSH.ShiptoAddress & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Ngày giao: " & Char2Date(objSH.DocumentDate) & " Thời gian giao:" & objSH.DeliveryFrom & " - " & objSH.DeliveryTo & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Lời nhắn giao hàng: " & objSH.DeliveryComments & "</div>"
        sHtml &= "</div>"
        sHtml &= "</div>"
        sHtml &= "</div>"
        sHtml &= "<div style=""clear:both;""></div>"
        sHtml &= ShowOrderDetail(sOrderNo, True)
        sHtml &= ShowDetailPayment(sOrderNo)
        Return sHtml
    End Function


    Function ShowOrderForEmail(ByVal sOrderNo As String, ByVal ShowAmount As Boolean) As String
        Dim objSH As New adv.Business.SalesHeader
        Dim sHtml As String = ""
        If Not objSH.Load(sOrderNo) Then Return ""

        sHtml &= "<div style=""width:890px;padding:4px;"">"
        sHtml &= "<div style=""margin-top:10px;font-size:18px;""><center><b>ĐƠN ĐẶT HÀNG</b></center></div>"

        sHtml &= "<div style=""margin-top:10px;padding:5px;"">"
        sHtml &= "<div style=""margin-top:10px;float:left;height:100px;padding:10px;border:1px solid #000;width:45%"">"
        sHtml &= "<div style=""margin-top:5px"">Số đơn hàng: " & objSH.No_ & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Ngày hàng: " & Char2Date(objSH.DocumentDate) & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Mã khách hàng: " & objSH.CustomerNo_ & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Tên khách hàng: " & objSH.CustomerName & "</div>"
        sHtml &= "</div>"

        sHtml &= "<div style=""margin-top:10px;float:right;width:45%;height:100px;padding:10px;border:1px solid #000;"">"
        sHtml &= "<div style=""margin-top:5px"">Địa chỉ giao hàng: </div>"
        sHtml &= "<div style=""margin-top:5px;margin-left:30px;"">" & objSH.ShiptoAddress & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Ngày giao: " & Char2Date(objSH.DocumentDate) & " Thời gian giao:" & objSH.DeliveryFrom & " - " & objSH.DeliveryTo & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Lời nhắn giao hàng: " & objSH.DeliveryComments & "</div>"
        sHtml &= "</div>"
        sHtml &= "</div>"
        sHtml &= "</div>"
        sHtml &= "<div style=""clear:both;""></div>"
        sHtml &= ShowOrderDetail(sOrderNo, True)
        Return sHtml
    End Function

    Function ShowOrderDetail(ByVal sOrderNo As String, ByVal ShowAmount As Boolean) As String
        Dim SQL As String
        SQL = "SELECT [Item No_], [Item Name], Variants, [Unit Of Measure], Quantity, [Unit Price Inc VAT], [Amount Inc VAT] " & _
                " FROM [Sales Line] WHERE [Document No_] = '" & sOrderNo & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        Dim nTotal As Double = 0
        Dim sHtml As String = ""
        sHtml &= "<div style=""padding:5px;margin-top:10px;"">"
        sHtml &= "<table class=""table table-bordered"" >"
        sHtml &= "<tr class='success'>"
        sHtml &= "<th scope=""col"">Mã hàng</th>"
        sHtml &= "<th scope=""col"">Tên hàng</th>"
        sHtml &= "<th scope=""col"">Size</th>"
        sHtml &= "<th scope=""col"">ĐVT</th>"
        sHtml &= "<th scope=""col"">Số lượng</th>"
        If ShowAmount Then
            sHtml &= "<th scope=""col"">Đơn giá</th>"
            sHtml &= "<th scope=""col"">Thành tiền</th>"
        End If
        sHtml &= "</tr>"
        For nIJ = 0 To tbl.Rows.Count - 1
            If nIJ Mod 2 = 0 Then
                sHtml &= "<tr style=""height:25px;"">"
            Else
                sHtml &= "<tr style=""background-color:#F5F5F5;height:24px;"">"
            End If
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Item No_") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Item Name") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Variants") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Unit Of Measure") & "</td>"
            sHtml &= "<td align=""right"">" & Format(Math.Round(tbl.Rows(nIJ).Item("Quantity"), 0), "##,###,###").Replace(",", ".") & "</td>"
            If ShowAmount Then
                sHtml &= "<td align=""right"">" & Format(Math.Round(tbl.Rows(nIJ).Item("Unit Price Inc VAT"), 0), "##,###,###").Replace(",", ".") & "</td>"
                sHtml &= "<td align=""right"">" & Format(Math.Round(tbl.Rows(nIJ).Item("Amount Inc VAT"), 0), "##,###,###").Replace(",", ".") & "</td>"
                nTotal += tbl.Rows(nIJ).Item("Amount Inc VAT")
            End If
            sHtml &= "</tr>"
        Next
        If ShowAmount Then
            sHtml &= "<tr style=""background-color:#FBF7BD;height:24px;"">"
            sHtml &= "<td colspan=""6""> Tổng cộng: </td>"
            sHtml &= "<td align=""right"">" & Format(Math.Round(nTotal, 0), "##,###,###").Replace(",", ".") & "</td>"
            sHtml &= "</tr>"
        End If
        sHtml &= "</table>"
        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowItemShop(ByVal ItemNo_ As String, ByVal iNum As Integer) As String
        Dim sHtml As String = ""
        Dim objItem As New adv.Business.Item
        Dim sUrl As String = GetUrl()
        Dim sImgUrl As String = GetImgUrl()
        Dim nOriginPrice As Double, nDealPrice As Double, sGiftDescriptions As String = "", sPromotionNo As String = ""
        Dim sItemLink As String = ""
        objItem.GetSalesPrice(ItemNo_, nOriginPrice, nDealPrice, sGiftDescriptions, sPromotionNo)
        objItem.Load(ItemNo_)
        'sItemLink = GetFullLinkCategory(objItem.MenuCategoryNo_)

        If objItem.PageNo = "apl" Then
            sItemLink = "san-pham-tieu-dung/"
        End If
        If objItem.PageNo = "ele" Then
            sItemLink = "san-pham-dien-may/"
        End If
        sItemLink = sUrl & sItemLink

        sItemLink &= IIf(ReturnIfNull(objItem.LinkUrl, "").ToString.Trim = "", objItem.No_, objItem.LinkUrl)
        If iNum <> 1 Then
            sHtml &= "<div class=""item"" style=""margin-left:3px;"">" & vbCrLf
        Else
            sHtml &= "<div class=""item"" style=""margin-left:0px;"">" & vbCrLf
        End If
        If nOriginPrice > nDealPrice And nDealPrice <> 0 Then
            sHtml &= "<div class=""salesoff"">" & vbCrLf
            sHtml &= Math.Round((nOriginPrice - nDealPrice) * 100 / nOriginPrice, 0) & "%<br />off"
            sHtml &= "</div>" & vbCrLf
        End If
        sHtml &= "<div class=""p-image"">" & vbCrLf
        sHtml &= "<a class=""linktitle"" href=""" & sItemLink & """ >" & vbCrLf
        sHtml &= "<img border=""0"" src=""" & sImgUrl & "Images/Product/" & objItem.ImagesThumURL & """ width=""130"" align=""absmiddle"" alt=""" & objItem.Name & ";" & objItem.MetaKeywords & """>" & vbCrLf
        sHtml &= "</a>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "<div class=""product_title"">" & vbCrLf
        sHtml &= "<a class=""linktitle"" href=""" & sItemLink & """ >" & objItem.Name & "</a>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "<div style=""margin-top:5px;""><center>"
        sHtml &= ShowItemRating(ItemNo_)
        sHtml &= "</center></div>"
        sHtml &= "<div class=""p-info"">" & vbCrLf
        If nDealPrice < nOriginPrice And nDealPrice <> 0 Then
            sHtml &= "<div class=""item_price"">" & vbCrLf
            sHtml &= "<div ><span class=""originprice"">" & Format(nOriginPrice, "##,###,###,###").Replace(",", ".") & "</span><span></span> </div>"
            sHtml &= "<div class=""salesprice"" >Tiết kiệm: " & Format(nOriginPrice - nDealPrice, "##,###,###,###").Replace(",", ".") & "</div>"
            sHtml &= "</div>" & vbCrLf
        Else
            sHtml &= "<div class=""item_desc"">" & vbCrLf
            sHtml &= objItem.GetDescription(objItem.MenuCategoryNo_, objItem.No_) & vbCrLf
            sHtml &= "</div>" & vbCrLf
        End If

        sHtml &= "<div><center>" & vbCrLf
        If nDealPrice <> 0 Then
            sHtml &= ShowPrice(nDealPrice) & vbCrLf
        Else
            sHtml &= ShowPrice(nOriginPrice) & vbCrLf
        End If

        sHtml &= "</center></div>" & vbCrLf
        sHtml &= "</div>" & vbCrLf

        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowOrderForReparing(ByVal sOrderNo As String, ByVal ShowAmount As Boolean) As String
        Dim objSH As New adv.Business.SalesHeader
        Dim sHtml As String = ""
        If Not objSH.Load(sOrderNo) Then Return ""

        sHtml &= "<div style=""width:310px;padding:4px;"">"
        sHtml &= "<div style=""margin-top:10px;font-size:18px;""><center><b>ĐƠN ĐẶT HÀNG</b></center></div>"
        sHtml &= "<div style=""margin-top:10px;padding:5px;"">"
        sHtml &= "<div style=""margin-top:5px"">Số đơn hàng: " & objSH.No_ & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Ngày hàng: " & Char2Date(objSH.DocumentDate) & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Mã khách hàng: " & objSH.CustomerNo_ & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Tên khách hàng: " & objSH.CustomerName & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Ngày giao: " & Char2Date(objSH.DeliveryDate) & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Từ: " & objSH.DeliveryFrom & "  tới: " & objSH.DeliveryTo & "</div>"
        sHtml &= "</div>"

        sHtml &= "<div style=""clear:both;""></div>"
        sHtml &= ShowOrderDetailForReparing(sOrderNo)
        Return sHtml
    End Function

    Function ShowOrderDetailForReparing(ByVal sOrderNo As String) As String
        Dim SQL As String
        SQL = "SELECT L.[Item No_], L.[Item Name], L.Variants, L.[Unit Of Measure], L.Quantity, B.Barcode " & _
                " FROM [Sales Line] L LEFT JOIN [Item Barcode] B ON L.[Item No_] = B.[Item No_] WHERE L.[Document No_] = '" & sOrderNo & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        Dim nTotal As Double = 0
        Dim sHtml As String = ""
        sHtml &= "<div style=""padding:5px;margin-top:10px;Width:310px;"">"
        sHtml &= "<table>"

        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr style=""height:25px;"">"
            sHtml &= "<td colspan=""4"">" & tbl.Rows(nIJ).Item("Item Name") & "</td>"
            sHtml &= "</tr>"
            sHtml &= "<tr style=""height:25px;"">"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Barcode") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Variants") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Unit Of Measure") & "</td>"
            sHtml &= "<td align=""right"">" & Format(Math.Round(tbl.Rows(nIJ).Item("Quantity"), 0), "##,###,###").Replace(",", ".") & "</td>"
            sHtml &= "</tr>"
        Next
        sHtml &= "</table>"
        sHtml &= "</div>"
        Return sHtml
    End Function

    Function ShowAllList() As String
        Dim SQL As String = ""
        Dim sHtml As String = ""
        Dim sDate As String = Date2Char(getToday)
        SQL = " SELECT No_ FROM [Show List Header] WHERE  Published = 1 AND [Content Type] = 0 AND [Starting Date] <= '" & sDate & "' AND ([Ending Date] >= '" & sDate & "' OR [Ending Date] = '') ORDER BY [Position Order] "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= ShowList(tbl.Rows(nIJ).Item("No_"))
            sHtml &= "<div style=""clear:both;""></div>"
        Next

        Return sHtml
    End Function

    Function ShowList(ByVal sShowListNo As String) As String
        Dim sHtml As String = ""
        Dim SQL As String = ""
        Dim objShow As New adv.Business.ShowListHeader
        Dim objGmn As New adv.Business.GoodMenu
        If Not objShow.Load(sShowListNo) Then Return ""
        Dim sDate As String = Date2Char(getToday)
        Dim sMenuNo As String = ""
        Dim sWhere As String = ""
        Dim nNumItem As Integer = 5
        Dim tbl As DataTable
        sMenuNo = objShow.MenuNo_
        If objShow.NumItemAtHome <> 0 Then nNumItem = objShow.NumItemAtHome
        If sMenuNo.Trim <> "" Then
            objGmn.Load(sMenuNo)
            If objGmn.MenuType = 0 Then
                sWhere = " AND I.[Menu Division No_] = '" & sMenuNo & "'"
            End If
            If objGmn.MenuType = 1 Then
                sWhere = " AND I.[Menu Category No_] = '" & sMenuNo & "'"
            End If
            If objGmn.MenuType = 2 Then
                sWhere = " AND I.[Menu Group No_] = '" & sMenuNo & "'"
            End If
        End If

        SQL = " SELECT TOP " & nNumItem & " [Item No_] FROM [Show List Line] WHERE [Document No_] = '" & sShowListNo & "' ORDER BY [Position Order] "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tbl.Rows.Count = 0 Then
            Select Case objShow.ListType
                Case 1 ' Sản phẩm khuyến mãi

                    SQL = " SELECT DISTINCT TOP " & nNumItem & " D.[Item No_], (100 - D.[Deal Price] * 100 / D.[Origin Price]) DiscountPercent FROM [Sales Price] D INNER JOIN Item I ON D.[Item No_] = I.No_ " & _
                        " WHERE I.Published = 1 AND D.[Starting Date] <= '" & sDate & "' AND (D.[Ending Date] >= '" & sDate & "' OR D.[Ending Date] = '') " & _
                        " AND D.[Origin Price] <> 0 AND D.[Origin Price] > D.[Deal Price] " & sWhere & " ORDER BY 2 DESC "

                Case 2 ' Sản phẩm hot
                    SQL = " SELECT TOP " & nNumItem & " I.No_ [Item No_], 1 Ord FROM Item I WHERE I.Published = 1 AND I.[Hot Product] = 1 " & sWhere & _
                        " UNION ALL " & _
                        " SELECT TOP " & nNumItem & " I.No_ [Item No_], 2 Ord FROM Item I WHERE I.Published = 1 " & sWhere & _
                        " ORDER BY Ord "

                Case 3 ' Sản phẩm mới
                    SQL = " SELECT TOP " & nNumItem & " I.No_ [Item No_] FROM Item I  WHERE I.Published = 1 AND I.[Hot Product] = 1 " & sWhere
                Case 4 ' Danh sách theo menu

                    SQL = " SELECT TOP " & nNumItem & " I.No_ [Item No_], 1 Ord FROM Item I  " & _
                        " WHERE I.Published = 1 AND I.[Hot Product] = 1 " & sWhere & _
                        " UNION ALL " & _
                        " SELECT TOP " & nNumItem & " I.No_ [Item No_], 2 Ord FROM Item I WHERE I.Published = 1 " & sWhere & _
                        " ORDER BY Ord "

                Case 5 ' Danh sách tùy chọn
                    SQL = " SELECT TOP " & nNumItem & " [Item No_] FROM [Show List Line] WHERE [Document No_] = '" & sShowListNo & "' ORDER BY [Position Order] "
            End Select
            tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        End If

        If tbl.Rows.Count = 0 Then Return ""

        Dim nIJ As Integer, kIJ As Integer = 0
        sHtml &= "<div class=""TitleLine"">" & objShow.Title
        If objShow.UrlPage.Trim <> "" Then
            sHtml &= "<span style=""font-size:12px;font-weight:normal;color:#EE4054;""><a href=""" & objShow.UrlPage & """> ( Xem thêm... ) </a></span>"
        End If
        sHtml &= "</div>"
        sHtml &= "<div class=""BoxHome"" style=""width:1000px;"">"

        If tbl.Rows.Count < nNumItem Then
            nNumItem = tbl.Rows.Count
        End If
        For nIJ = 0 To nNumItem - 1
            sHtml &= ShowItem(tbl.Rows(nIJ).Item("Item No_"), kIJ, 0, 0, 0, 0, sShowListNo)
            kIJ += 1
            If kIJ = 5 Then kIJ = 0
        Next
        sHtml &= "</div>"

        Return sHtml
    End Function

    Function ShowTemplate(sTemplateNo As String) As String
        Dim objMC As New adv.Business.MsgContent
        If Not objMC.Load(sTemplateNo) Then
            Return ""
        Else
            Return objMC.Content
        End If
    End Function
End Module

