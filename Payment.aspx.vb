Imports System.Data

Partial Class Payment
    Inherits System.Web.UI.Page

    Dim objSH As New adv.Business.SalesHeader
    Dim objL As New adv.Business.SalesLine
    Dim objNo As New adv.Business.cNoSeri
    Dim objC As New adv.Business.Customer
    Dim objCA As New adv.Business.CustomerAddress
    Dim objPM As New adv.Business.PaymentEntry
    Dim isLoad As Boolean = False
    Dim SECURE_SECRET, Merchant_AccessCode, Merchant_ID As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sCartNo As String = ""
            If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
            If sCartNo.Trim = "" Then Response.Redirect(GetUrl)
            If objSH.LoadByCartNo(sCartNo) Then
                TxtOrderNo.Value = objSH.No_
            Else
                Response.Redirect(GetUrl() & "dia-chi-giao-hang/")
            End If

            Dim sCart As String = ""
            sCart = ShowCartPayment(sCartNo)
            If sCart = "" Then
                Response.Redirect(GetUrl)
                Exit Sub
            End If
            LblCart.Text = sCart
            If ReturnIfNull(Session("CustomerNo"), "").ToString.Trim = "" Then
                Response.Redirect(GetUrl() & "thanh-vien/")
            End If
            LblPathWay.Text = ShowPathWay()
            LblCustomerAccount.Text = ShowCustomerPayment(sCartNo)
            If Session("CustomerNo") <> GetCustomerDefault() Then
                Dim sAmountBalance As Double = objC.GetBalanceAmount(Session("CustomerNo"))
                If sAmountBalance <> 0 Then
                    TxtBalanceAmount.Text = Format(sAmountBalance, "##,###,###")
                    cmdPayFromAccount.Visible = True
                Else
                    TxtBalanceAmount.Text = "0"
                    cmdPayFromAccount.Visible = False
                End If
            Else
                TxtBalanceAmount.Text = "0"
                cmdPayFromAccount.Visible = False
            End If
            Dim LinkLogo As String = ""
            Dim sUrlImg As String = GetImgUrl()
            'LinkLogo = "<img src=" & sUrlImg & "/Images/Bank_Card/logo_onepay.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/vcb.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/vietin.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/tech.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/bidv.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/abbank.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/donga.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/exim.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/hd.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/mari.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/mb.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/tienphong.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/vcb.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/nama.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/ocean.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/sea.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/shb.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/vib.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/vieta.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/baca.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'LinkLogo = LinkLogo & "<img src=" & sUrlImg & "/Images/Bank_Card/vpbank.jpg" & " alt=""VCB"" width=""70"" height=""50"" > "
            'lbATMLogo.Text = LinkLogo
            'lbCRCLogo.Text = "<img src=" & sUrlImg & "/Images/Bank_Card/Creditcard.jpg" & " alt=""VCB"" width=""700"" height=""50"" > "
        End If
    End Sub

    Function ShowCustomerPayment(ByVal sCartNo As String) As String
        Dim sPayment As String = GetDetailPayment(sCartNo)
        Return sPayment
    End Function


    Function GetPaymentMethodName(ByVal sPaymentMethodNo As String) As String
        Dim sTxt As String = ""
        Select Case sPaymentMethodNo
            Case 1
                sTxt = "Trực tiếp"
            Case 2
                sTxt = "Chuyển khoản"
            Case 3
                sTxt = "Chuyển qua bưu điện"
            Case 4
                sTxt = "Thanh toán online thẻ ATM"
            Case 5
                sTxt = "Thanh toán online thẻ quốc tế"
        End Select
        Return sTxt
    End Function

    Function ShowPathWay() As String
        Dim sHtml As String = ""
        sHtml = "<a href=""" & GetUrl() & """ class=""linkheader"">Trang chủ</a> "
        sHtml &= "<b>/Xác nhận đơn hàng</b>"
        Return sHtml
    End Function

    Function ShowMenuHelp() As String
        Dim sHtml As String = ""
        sHtml &= "<div class=""containerleft"">" & vbCrLf
        sHtml &= "<div class=""leftheader"">Hỗ trợ khách hàng </div>" & vbCrLf
        sHtml &= "<div style=""min-height:90px;padding:5px;"">" & vbCrLf
        sHtml &= ShowLinkMenu("10", "categorylink") & vbCrLf
        sHtml &= "</div>" & vbCrLf
        sHtml &= "</div>" & vbCrLf
        Return sHtml

    End Function


    Function ShowCartPayment(ByVal sCartNo As String) As String
        Dim SQL As String
        Dim sHtml As String = ""
        Dim tbl As DataTable
        Dim objItem As New adv.Business.Item
        Dim nIJ As Integer, nTotal As Double = 0
        Dim sUrl As String = GetUrl()
        Dim sImgUrl As String = GetImgUrl()
        SQL = " SELECT L.[Line No_], L.[Item No_], I.Name, L.Quantity, L.[Unit Price], L.[Amount Inc VAT], L.Descriptions, U.Name [Unit Of Measure] " & _
                " FROM [Cart Line] L INNER JOIN Item I ON L.[Item No_] = I.No_ " & _
                " LEFT JOIN [Unit Of Measure] U ON L.[Unit Of Measure] = U.No_ " & _
                " WHERE L.[Cart No_] = '" & sCartNo & "' "

        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tbl.Rows.Count = 0 Then Return ""
        sHtml &= "<table class='table table-bordered'>" & vbCrLf
        sHtml &= "<tr class=""active"" >" & vbCrLf
        sHtml &= "<td style=""width:100px;font-weight: bold;"">" & "Sản phẩm" & "</td>" & vbCrLf
        sHtml &= "<td style=""width:80px;font-weight: bold;"">" & "Mã sản phẩm" & "</td>" & vbCrLf
        sHtml &= "<td style=""width:240px;font-weight: bold;"">" & "Tên sản phẩm" & "</td>" & vbCrLf
        sHtml &= "<td style=""width:80px;font-weight: bold;"">" & "ĐVT" & "</td>" & vbCrLf
        sHtml &= "<td style=""width:100px;font-weight: bold;"" align=""right"">" & "Đơn giá" & "</td>" & vbCrLf
        sHtml &= "<td style=""width:80px;font-weight: bold;"" align=""right"">" & "Số lượng" & "</td>" & vbCrLf
        sHtml &= "<td style=""width:100px;font-weight: bold;"" align=""right"">" & "Thành tiền" & "</td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr>"
            objItem.Load(tbl.Rows(nIJ).Item("Item No_"))
            sHtml &= "<td>" & IIf(objItem.ImagesThumURL.Trim <> "", "<img border=""0"" src=""" & sImgUrl & "Images/Product/" & objItem.ImagesThumURL & """ width=""80"" align=""absmiddle"" alt=""" & objItem.Name & """>", "") & vbCrLf
            sHtml &= "</td>" & vbCrLf
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Item No_") & "</td>" & vbCrLf
            sHtml &= "<td><b>" & tbl.Rows(nIJ).Item("Name") & "</b></td>" & vbCrLf
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Unit Of Measure") & "</td>" & vbCrLf
            sHtml &= "<td align=""right"">" & Format(tbl.Rows(nIJ).Item("Unit Price"), "##,###").Replace(",", ".") & "</td>" & vbCrLf
            sHtml &= "<td align=""right"">" & tbl.Rows(nIJ).Item("Quantity") & " </td>" & vbCrLf
            sHtml &= "<td align=""right"">" & Format(tbl.Rows(nIJ).Item("Amount Inc VAT"), "##,###").Replace(",", ".") & "</td>" & vbCrLf
            sHtml &= "</tr>"
            nTotal += tbl.Rows(nIJ).Item("Amount Inc VAT")
        Next

        sHtml &= "<tr class=""cartheader"" style=""height:30px;"">" & vbCrLf
        sHtml &= "<td colspan=""6"" align=""right""><b>Tổng tiền hàng:</b> " & "</td>" & vbCrLf
        sHtml &= "<td align=""right""><b>" & Format(nTotal, "##,###").Replace(",", ".") & "</b></td>" & vbCrLf
        objSH.LoadByCartNo(sCartNo)
        If objSH.DeliveryFee > 0 Then
            sHtml &= "<tr class=""cartheader"" style=""height:30px;"">" & vbCrLf
            sHtml &= "<td colspan=""6"" align=""right""><b>Phí giao hàng:</b> " & "</td>" & vbCrLf
            sHtml &= "<td align=""right""><b>" & Format(objSH.DeliveryFee, "##,##0").Replace(",", ".") & "</b></td>" & vbCrLf
            sHtml &= "</tr>" & vbCrLf
        End If
        Dim nVolumeFee As Double
        nVolumeFee = GetDelieverFeeByVolume(sCartNo)
        If nVolumeFee > 0 Then
            sHtml &= "<tr class=""cartheader"" style=""height:30px;"">" & vbCrLf
            sHtml &= "<td colspan=""6"" align=""right""><b>Phí hàng cồng kềnh:</b> " & "</td>" & vbCrLf
            sHtml &= "<td align=""right""><b>" & Format(nVolumeFee, "##,##0").Replace(",", ".") & "</b></td>" & vbCrLf
        End If
        Dim nPayment As Double
        nPayment = nTotal + objSH.DeliveryFee + nVolumeFee
        sHtml &= "<tr class=""cartheader"" style=""height:30px;"">" & vbCrLf
        sHtml &= "<td colspan=""6"" align=""right""><b>Quý khách phải trả:</b> " & "</td>" & vbCrLf
        sHtml &= "<td align=""right""><b>" & Format(nPayment, "##,##0").Replace(",", ".") & "</b></td>" & vbCrLf
        sHtml &= "</table>" & vbCrLf
        Return sHtml
    End Function

    Function ShowDeliveryInfo() As String
        Dim sHtml As String = ""
        Dim objC As New adv.Business.Customer
        objC.Load(Session("customerno"))
        sHtml &= "<div>Người nhận:&nbsp;&nbsp;&nbsp;<b>" & objC.FullName & "</b>"
        sHtml &= "</div>"
        sHtml &= "<div>Điện thoại:&nbsp;&nbsp;&nbsp;" & objC.Telephone
        sHtml &= "</div>"
        sHtml &= "<div>Địa chỉ nhận hàng:&nbsp;&nbsp;&nbsp;" & objC.FullAddress
        sHtml &= "</div>"
        Return sHtml
    End Function

    Protected Sub cmdConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConfirm.Click
        Dim sOrderNo As String = ""
        Dim sCartNo As String = ""
        If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        If sCartNo.Trim = "" Then Response.Redirect(GetUrl)
        Dim sPaymentMethod As Integer
        sOrderNo = TxtOrderNo.Value
        sPaymentMethod = CInt(Request("payment_method_id"))
        objSH.UpdatePaymentMethod(sOrderNo, sPaymentMethod)
        objSH.Load(sOrderNo)
        Try
            Select Case CInt(sPaymentMethod)
                Case 1, 2, 3 ' COD, Bank transfer
                    If sOrderNo.Trim = "" Then Exit Sub
                    If Not objSH.FinishedConfirm(sOrderNo) Then Exit Sub
                    Response.Cookies("CartNo").Value = ""
                    Response.Redirect("hoan-thanh/" & sOrderNo)
                Case 4 ' Card online ATM

                    'Replace with actual Acc
                    Merchant_ID = System.Configuration.ConfigurationManager.AppSettings("lMerchant_ID").ToString
                    Merchant_AccessCode = System.Configuration.ConfigurationManager.AppSettings("lMerchant_AccessCode").ToString
                    SECURE_SECRET = System.Configuration.ConfigurationManager.AppSettings("lSECURE_SECRET").ToString

                    Dim svirtualPaymentClientURL As String = System.Configuration.ConfigurationManager.AppSettings("lsvirtualPaymentClientURL").ToString
                    Dim sAgainLink As String = System.Configuration.ConfigurationManager.AppSettings("lsAgainLink").ToString '"http://www.homeone.com.vn/xac-nhan/" & TxtPaymentMethod.Value"
                    Dim svpc_Locale As String = "en"
                    Dim svpc_Version As Integer = 2
                    Dim svpc_Command As String = "Pay" ' Chức năng thanh toán, giá trị của đối số này thường mặc định là “pay”
                    Dim svpc_Merchant As String = Merchant_ID ' Merchane ID Cặp tài khoản của mỗi đơn vị
                    Dim svpc_AccessCode As String = Merchant_AccessCode ' Acess code
                    Dim svpc_MerchTxnRef As String = System.Configuration.ConfigurationManager.AppSettings("lMerchTxnRef").ToString & " " & objSH.No_ ' Merchant Transaction Reference Mã giao dịch, biến số này yêu cầu chuyền vào duy nhất mỗi lần thanh toán
                    Dim svpc_OrderInfo As String = objSH.No_ ' Thông tin đơn hàng
                    Dim svpc_Amount As String = Math.Round(objSH.GetAmountPayment(sCartNo), 0).ToString 'Khoản tiền thanh toán, giá trị chuyền vào không có dấu. Cổng thanh toán lấy hay ký tự cuối cùng là phần thập phân
                    Dim svpc_Currency As String = "VND"
                    Dim svpc_ReturnURL As String = System.Configuration.ConfigurationManager.AppSettings("lsvpc_ReturnURL").ToString '' Địa chỉ để nhận kết quả trả về. Giá trị này đơn vị phải đăng ký với cồng thanh toán, khi chạy trên hệ môi trường thật, đơn vị cần gửi giá trị này cho OnePAY để cấu hình trên hệ thống.
                    Dim svpc_TicketNo As String = objSH.IPAddress 'objSH.IPAddress ' Địa chỉ IP khách hàng thực hiện thanh toán

                    Dim conn As VPCRequest = New VPCRequest(svirtualPaymentClientURL)
                    conn.SetSecureSecret(SECURE_SECRET)

                    Dim MyArray As String(,) = {{"Title", "ASP VPC 3-Party"}, {"AgainLink", sAgainLink},
                                                {"vpc_Locale", svpc_Locale}, {"vpc_Version", svpc_Version},
                                                {"vpc_Command", svpc_Command}, {"vpc_Merchant", svpc_Merchant},
                                                {"vpc_AccessCode", svpc_AccessCode}, {"vpc_MerchTxnRef", svpc_MerchTxnRef},
                                                {"vpc_OrderInfo", svpc_OrderInfo}, {"vpc_Amount", svpc_Amount & "00"},
                                                {"vpc_Currency", svpc_Currency}, {"vpc_ReturnURL", svpc_ReturnURL},
                                                {"vpc_SHIP_Street01", "194 Tran Quang Khai"}, {"vpc_SHIP_Provice", "Hanoi"},
                                                {"vpc_SHIP_City", "Hanoi"}, {"vpc_SHIP_Country", "Vietnam"},
                                                {"vpc_Customer_Phone", "043966668"}, {"vpc_Customer_Email", "support@onepay.vn"},
                                                {"vpc_Customer_Id", "onepay_paygate"},
                                                {"vpc_TicketNo", svpc_TicketNo}}
                    conn.SetArrayRequest(MyArray)

                    ' Chuyen huong trinh duyet sang cong thanh toan
                    Dim url As String = conn.Create3PartyQueryString()
                    Page.Response.Redirect(url)

                Case 5 ' Card online Visa, Master, AMEX...

                    objSH.Load(sOrderNo)
                    Response.Redirect(GetUrl() & "bank/checkout.aspx?OrderNo=" & sOrderNo)

            End Select
        Catch ex As Exception
            LblWaring.Text = ex.Message
        End Try
    End Sub


    Protected Sub cmdClosePayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClosePayment.Click
        ModalPopupPayment.Hide()
    End Sub

    Protected Sub cmdPayFromAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPayFromAccount.Click
        Dim sCartNo As String = ""
        If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        If sCartNo.Trim = "" Then Response.Redirect(GetUrl)
        Dim sAmountBalance As Double = objC.GetBalanceAmount(Session("CustomerNo"))
        Dim sOrderAmount As Double = objSH.GetAmountInCart(sCartNo)

        TxtBalance.Text = Format(Math.Round(sAmountBalance, 0), "##,###,###")

        If sAmountBalance > sOrderAmount Then sAmountBalance = sOrderAmount

        TxtOrderAmount.Text = Format(Math.Round(sOrderAmount, 0), "##,###,###")
        TxtPaymentAmount.Text = Format(Math.Round(sAmountBalance, 0), "##,###,###")
        TxtRemaining.Text = Format(Math.Round(sOrderAmount - sAmountBalance, 0), "##,###,###")
        ModalPopupPayment.Show()
    End Sub

    Protected Sub cmdPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPayment.Click
        If ReturnIfNull(Session("CustomerNo"), "").ToString.Trim = "" Then
            Response.Redirect(GetUrl() & "thanh-vien/")
            Exit Sub
        End If
        Dim sAmountBalance As Double = objC.GetBalanceAmount(Session("CustomerNo"))
        Dim sOrderAmount As Double = CDbl(TxtOrderAmount.Text.Replace(",", ""))

        Dim sCartNo As String = ""
        If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        If sCartNo.Trim = "" Then Response.Redirect(GetUrl)

        objSH.LoadByCartNo(sCartNo)

        If Math.Round(sAmountBalance) <> Math.Round(CDbl(TxtBalance.Text.Replace(",", ""))) Then
            LblPayWarning.Text = "Trong quá trình đợi thanh toán, số dư tài khoản của quí khách đã thay đổi."
            TxtBalance.Text = Format(Math.Round(sAmountBalance, 0), "##,###,###")
            If sAmountBalance > sOrderAmount Then sAmountBalance = sOrderAmount
            TxtPaymentAmount.Text = Format(Math.Round(sAmountBalance, 0), "##,###,###")
            TxtRemaining.Text = Format(Math.Round(sOrderAmount - sAmountBalance, 0), "##,###,###")
            TxtPaymentAmount.Text = TxtBalance.Text
            Exit Sub
        End If
        If CInt(TxtPaymentAmount.Text.Replace(",", "")) > CInt(TxtBalance.Text.Replace(",", "")) Then
            LblPayWarning.Text = "Số tiền cấn trừ không được vượt quá số dư."
            TxtPaymentAmount.Text = TxtBalance.Text
            Exit Sub
        End If
        If CInt(TxtPaymentAmount.Text.Replace(",", "")) = 0 Then
            LblPayWarning.Text = "Quí khách nhập số tiền cần cấn trừ."
            TxtPaymentAmount.Text = TxtBalance.Text
            Exit Sub
        End If
        With objPM
            .OrderNo_ = objSH.No_
            .LineNo_ = 0
            .OrderDate = Date2Char(getToday)
            .TenderType = "01"
            .Amount = CInt(TxtPaymentAmount.Text.Replace(",", ""))
            .CardNo_ = ""
            .PaymentType = 0
            .Descriptions = "Cấn trừ số dư tài khoản thưởng"
            .CartNo_ = sCartNo
            .CustomerNo_ = Session("CustomerNo")
            If Not .Save() Then Exit Sub
        End With
        sAmountBalance = objC.GetBalanceAmount(Session("CustomerNo"))
        If sAmountBalance <> 0 Then
            TxtBalanceAmount.Text = Format(sAmountBalance, "##,###,###")
            cmdPayFromAccount.Visible = True
        Else
            TxtBalanceAmount.Text = "0"
            cmdPayFromAccount.Visible = False
        End If
        LblCustomerAccount.Text = ShowCustomerPayment(sCartNo)
        ModalPopupPayment.Hide()
    End Sub

    Function GetPayed(ByVal sCartNo As String) As Double
        Dim SQL As String = " SELECT * FROM [Payment Entry] WHERE [Cart No_] = '" & sCartNo & "'"
        Return DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)

    End Function

    Protected Sub CmdDelPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdDelPayment.Click
        Dim sCartNo As String = ""
        If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        If sCartNo.Trim = "" Then Response.Redirect(GetUrl)
        objSH.LoadByCartNo(sCartNo)
        objPM.Del(objSH.No_, txtPaymentLine.Value)
        LblCustomerAccount.Text = ShowCustomerPayment(sCartNo)
        Dim sAmountBalance As Double = objC.GetBalanceAmount(Session("CustomerNo"))
        If sAmountBalance <> 0 Then
            TxtBalanceAmount.Text = Format(sAmountBalance, "##,###,###")
            cmdPayFromAccount.Visible = True
        Else
            TxtBalanceAmount.Text = "0"
            cmdPayFromAccount.Visible = False
        End If
    End Sub

    Protected Sub cmdUpdateCart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateCart.Click

        If TxtPromotionsCode.Text = "" Then
            LblDiscountWarning.Text = "Bạn phải nhập mã giảm giá."
            Exit Sub
        End If
        Dim objDataEntry As New adv.Business.CardDataEntry
        If Not objDataEntry.Load(TxtPromotionsCode.Text.Trim) Then
            LblDiscountWarning.Text = "Mã giảm giá không tồn tại."
            Exit Sub
        End If

        Dim sCartNo As String = ""
        If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        If sCartNo.Trim = "" Then Response.Redirect(GetUrl)

        If objDataEntry.MultiApplied = 0 And objDataEntry.OrderNo_.Trim <> "" Then
            LblDiscountWarning.Text = "Mã giảm giá này đã được sử dụng."
            Exit Sub
        End If

        If Not CheckPayment(sCartNo, TxtPromotionsCode.Text) Then
            LblDiscountWarning.Text = "Mã giảm giá này đã được sử dụng trên đơn hàng của bạn."
            Exit Sub
        End If

        If objDataEntry.StartingDate > Date2Char(getToday) Then
            LblDiscountWarning.Text = "Mã giảm giá này ngoài thời hạn sử dụng."
            Exit Sub
        End If

        If objDataEntry.EndingDate < Date2Char(getToday) Then
            LblDiscountWarning.Text = "Mã giảm giá này đã hết hạn sử dụng."
            Exit Sub
        End If

        Select Case objDataEntry.PaymentType
            Case 0
            Case 1
                If objDataEntry.DiscountType = 0 Then
                    DiscountCart(sCartNo, objDataEntry.Amount, 0)
                Else
                    DiscountCart(sCartNo, objDataEntry.Amount, 1)
                End If

                With objPM
                    .OrderNo_ = TxtOrderNo.Value
                    .LineNo_ = 0
                    .OrderDate = Date2Char(getToday)
                    .TenderType = ""
                    .Amount = objDataEntry.Amount
                    .PaymentType = 1
                    .Descriptions = "Chiết khấu " & objDataEntry.Descriptions
                    .CardNo_ = TxtPromotionsCode.Text
                    .CartNo_ = sCartNo
                    .CustomerNo_ = Session("CustomerNo")
                    If Not .Save() Then Exit Sub
                End With
                LblDiscountWarning.Text = "Đơn hàng của bạn đã được giảm giá."
            Case 2
                Dim nCartAmount As Double = 0, nDiscountAmount As Double = 0
                If objDataEntry.DiscountType = 0 Then
                    nCartAmount = GetAmountOfCartLine(sCartNo)
                    nDiscountAmount = (nCartAmount / 100) * objDataEntry.Amount
                    If nDiscountAmount > objDataEntry.MaxAmount Then nDiscountAmount = objDataEntry.MaxAmount
                Else
                    nDiscountAmount = objDataEntry.Amount
                End If
                With objPM
                    .OrderNo_ = TxtOrderNo.Value
                    .LineNo_ = 0
                    .OrderDate = Date2Char(getToday)
                    .TenderType = ""
                    .Amount = nDiscountAmount
                    .PaymentType = 2
                    .Descriptions = "Thưởng tiền vào tải khoản"
                    .CardNo_ = TxtPromotionsCode.Text
                    .CartNo_ = sCartNo
                    .CustomerNo_ = Session("CustomerNo")
                    If Not .Save() Then Exit Sub
                End With
                LblDiscountWarning.Text = "Bạn sẽ được tặng tiền vào tài khoản. Số tiền là " & Format(nDiscountAmount, "##,###,###") & " VNĐ"
        End Select

        LblCustomerAccount.Text = ShowCustomerPayment(sCartNo)
    End Sub

    Function CheckPayment(ByVal sCartNo As String, ByVal sCard As String) As Boolean
        Dim SQL As String
        SQL = "SELECT [Order No_] FROM [Payment Entry] WHERE [Cart No_] = '" & sCartNo & "' AND [Card No_] = '" & sCard & "'"
        Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim = ""
    End Function

    Protected Sub CmdDelDiscount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdDelDiscount.Click
        Dim sCartNo As String = ""
        If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        If sCartNo.Trim = "" Then Response.Redirect(GetUrl)
        DelDiscountCart(sCartNo)
        LblCustomerAccount.Text = ShowCustomerPayment(sCartNo)
    End Sub

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect(GetUrl() & "dia-chi-giao-hang/")
    End Sub
End Class
