Imports System.Data

Partial Class SelectDelieveryAddress
    Inherits System.Web.UI.Page
    Dim objSH As New adv.Business.SalesHeader
    Dim objL As New adv.Business.SalesLine
    Dim objNo As New adv.Business.cNoSeri
    Dim objC As New adv.Business.Customer
    Dim objCA As New adv.Business.CustomerAddress
    Dim objPM As New adv.Business.PaymentEntry
    Dim isLoad As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim sCartNo As String = ""
            If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString

            If sCartNo.Trim = "" Then
                Response.Redirect(GetUrl())
            End If

            If ReturnIfNull(Session("CustomerNo"), "").ToString.Trim = "" Then
                Response.Redirect(GetUrl() & "thanh-vien/")
                Exit Sub
            End If

            LblPathWay.Text = ShowPathWay()
            LblCartSumary.Text = "<div id=""cartsumary"">" & ShowCartSumary(sCartNo) & "</div>"

            BuildCombo(CboCity, adv.Business.List.Province, " ", True, "Chọn tỉnh thành")
            If objSH.LoadByCartNo(sCartNo) Then
                If objSH.CustomerNo_ <> Session("CustomerNo") Then
                    DeleteOrder(objSH.No_)
                    ShowDelieveryInfo()
                Else
                    If objSH.DocumentDate.Trim <> Date2Char(getToday()) Then
                        DeleteOrder(objSH.No_)
                        ShowDelieveryInfo()
                    Else
                        ShowData()
                    End If
                End If
            Else
                If Session("CustomerNo") <> GetCustomerDefault() Then
                    ShowDelieveryInfo()
                End If
            End If
            CboDistrict.Attributes.Add("onchange", "ChangeDistrict()")

        End If

    End Sub

    Sub DeleteOrder(ByVal sOrderNo As String)
        Dim SQL As String = ""
        SQL = "DELETE FROM [Sales Header] WHERE No_ = '" & sOrderNo & "'"
    End Sub

    Sub ShowData()
        With objSH
            TxtFullName.Text = .ShiptoName
            txtOrderNo.Value = .No_
            TxtTelephone.Text = .ShiptoTelephone
            CboCity.SelectedValue = .ShiptoProvinceNo_.Trim
            CboCity_SelectedIndexChanged(CboCity, Nothing)
            CboDistrict.SelectedValue = .ShiptoDistrictNo_.Trim
            CboDistrict_SelectedIndexChanged(CboDistrict, Nothing)
            TxtHouseNo.Text = .ShipToHouseNo
            TxtBillToName.Text = .BilltoName
            TxtBillToAddress.Text = .BilltoAddress
            TxtTaxCode.Text = .TaxCode
            TxtRequirement.Text = .DeliveryComments
        End With
    End Sub


    Function ShowPathWay() As String
        Dim sHtml As String = ""
        sHtml = "<a href=""" & GetUrl() & """ class=""linkheader"">Trang chủ</a> "
        sHtml &= " / Chọn địa chỉ giao hàng"
        Return sHtml
    End Function

    Function CheckIvalid() As Boolean
        Dim script As String = ""
        If TxtFullName.Text.Trim = "" Then
            LblErr.Text = "Bạn phải nhập người nhận hàng."
            TxtFullName.Focus()
            script = "alert('Bạn phải nhập người nhận hàng.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
            Return False
        End If
        If TxtTelephone.Text.Trim = "" Then
            LblErr.Text = "Bạn phải nhập điện thoại người nhận hàng."
            TxtTelephone.Focus()
            script = "alert('Bạn phải nhập điện thoại người nhận hàng.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
            Return False
        End If
        If CboCity.SelectedValue.Trim = "" Then
            script = "alert('Bạn phải chọn thành phố.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
            Return False
        End If

        If CboDistrict.SelectedValue.Trim = "" Then
            script = "alert('Bạn phải chọn quận huyện.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
            Return False
        End If

        If TxtHouseNo.Text = "" Then
            script = "alert('Bạn phải nhập số nhà, tên đường, tên phường.')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
            Return False
        End If

        Return True
    End Function

    Function GetAllAddress() As String
        Dim sHtml As String = ""
        Dim SQL As String
        SQL = "SELECT * FROM [Customer Address] WHERE [Customer No_] = '" & Session("CustomerNo") & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<div style=""margin-top:10px;"">"
            sHtml &= "<a href=""#"" onclick=""AddressSelect('" & tbl.Rows(nIJ).Item("Line No_") & "')"" >" & tbl.Rows(nIJ).Item("Full Address") & "</a>"
            sHtml &= "</div>"
        Next

        Return sHtml
    End Function

    Function CreateSaleHeader() As String
        Dim sCartNo As String, sYM As String, Script As String = ""
        sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        If sCartNo.Trim = "" Then Return ""
        Dim sOrderNo As String = ""
        sOrderNo = txtOrderNo.Value
        If sOrderNo.Trim = "" Then
            objNo.Load("SALESNO")
            sYM = sLeft(Date2Char(getToday()), 6)
            sOrderNo = objNo.CreateNoSeri(sYM)
        End If
        Try
            objC.Load(Session("customerno"))
            With objSH
                .No_ = sOrderNo
                .DocumentDate = Date2Char(getToday)
                .CartNo_ = sCartNo
                .CustomerNo_ = Session("CustomerNo")

                .VATInvoiceIssued = CInt(ReturnIfNull(TxtVATInvoiceIssued.Value, 0))
                If Session("CustomerNo") <> GetCustomerDefault() Then
                    .CustomerName = objC.FullName

                Else
                    .CustomerName = TxtFullName.Text
                End If

                .ShiptoName = TxtFullName.Text
                .ShipToHouseNo = TxtHouseNo.Text
                .ShiptoProvinceNo_ = CboCity.SelectedValue
                .ShiptoDistrictNo_ = CboDistrict.SelectedValue
                .ShiptoWardNo_ = ""
                .ShiptoTelephone = TxtTelephone.Text
                .ShiptoCountry = "VIETNAM"
                .ShiptoAddress = objC.GetAddress(TxtHouseNo.Text, "", .ShiptoDistrictNo_, .ShiptoProvinceNo_)
                .BilltoName = TxtBillToName.Text
                .BilltoAddress = TxtBillToAddress.Text
                .TaxCode = TxtTaxCode.Text

                .PaymentStatus = 0
                .OrderStatus = 0
                .IPAddress = HttpContext.Current.Request.UserHostAddress
                .DeliveryDate = ""
                .DipositAmount = 0
                .DeliveryComments = TxtRequirement.Text
                .DeliveryFee = Val(txtDeliveryFee.Value)
                If Not .Save() Then Return ""
                Return "OK"
            End With
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function


    Sub ShowDelieveryInfo()
        Dim objC As New adv.Business.Customer
        objC.Load(Session("customerno"))
        TxtFullName.Text = objC.FullName
        TxtTelephone.Text = objC.Telephone
        TxtBillToName.Text = objC.BillToName
        TxtBillToAddress.Text = objC.BillToAddress
        TxtTaxCode.Text = objC.TaxCode

        CboCity.SelectedValue = objC.ProvinceNo_
        CboCity_SelectedIndexChanged(CboCity, Nothing)
        CboDistrict.SelectedValue = objC.DistrictNo_
        CboDistrict_SelectedIndexChanged(CboDistrict, Nothing)
        TxtHouseNo.Text = objC.Address

    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        If Not CheckIvalid() Then Exit Sub
        Dim sResult As String
        sResult = CreateSaleHeader()
        If sResult <> "OK" Then
            LblWaring.Text = sResult
            Exit Sub
        End If
        Response.Redirect(GetUrl() & "thanh-toan/")
    End Sub

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        If Session("CustomerNo") = GetCustomerDefault() Then Session("CustomerNo") = ""
        Response.Redirect(GetUrl() & "gio-hang/")
    End Sub

    Protected Sub CboCity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCity.SelectedIndexChanged
        Dim sProvinceNo As String
        sProvinceNo = ReturnIfNull(CboCity.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim
        BuildCombo(CboDistrict, adv.Business.List.District, "  [Parent No_] = '" & sProvinceNo & "'", True, "Chọn quận huyện")

    End Sub

    Protected Sub CboDistrict_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboDistrict.SelectedIndexChanged
        Dim sCartNo As String = ""
        If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        LblCartSumary.Text = "<div id=""cartsumary"">" & ShowCartSumary(sCartNo, CboCity.SelectedValue, CboDistrict.SelectedValue) & "</div>"
        txtDeliveryFee.Value = GetDeliveryFeeByAmount(CboCity.SelectedValue, CboDistrict.SelectedValue, objSH.GetAmountInCart(sCartNo))
    End Sub

    Function ShowCartSumary(ByVal sCartNo As String, Optional ByVal sProvinceNo As String = "", Optional ByVal sDistrictNo As String = "") As String
        Dim SQL As String
        Dim sHtml As String
        Dim tbl As DataTable
        Dim objItem As New adv.Business.Item
        Dim nIJ As Integer, nTotal As Double = 0
        Dim sUrl As String = GetUrl()
        Dim nDeliveryFee As Double = 0
        Dim nVolumeFee As Double = 0

        Dim sImgUrl As String = GetImgUrl()
        SQL = " SELECT L.[Line No_], L.[Item No_], I.Name + ' ' + L.Variants Name, L.Quantity, L.[Unit Price], L.[Amount Inc VAT], L.Descriptions " & _
                " FROM [Cart Line] L INNER JOIN Item I ON L.[Item No_] = I.No_ " & _
                " WHERE L.[Cart No_] = '" & sCartNo & "' "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        sHtml = ""
        sHtml &= "<table  class=""table table-bordered"">" & vbCrLf
        sHtml &= "<tr class=""active"" >" & vbCrLf

        sHtml &= "<th style=""font-weight: bold;"">" & "Tên sản phẩm" & "</th>" & vbCrLf
        sHtml &= "<th style=""font-weight: bold;"" align=""right"">" & "Đơn giá" & "</th>" & vbCrLf
        sHtml &= "<th style=""font-weight: bold;"" align=""right"">" & "Số lượng" & "</th>" & vbCrLf
        sHtml &= "<th style=""font-weight: bold;"" align=""right"">" & "Thành tiền" & "</th>" & vbCrLf

        sHtml &= "</tr>" & vbCrLf
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr>"
            objItem.Load(tbl.Rows(nIJ).Item("Item No_"))
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Name") & "</td>" & vbCrLf
            sHtml &= "<td align=""right"">" & Format(tbl.Rows(nIJ).Item("Unit Price"), "##,###").Replace(",", ".") & "</td>" & vbCrLf
            sHtml &= "<td align=""right""> " & tbl.Rows(nIJ).Item("Quantity") & "</td>" & vbCrLf
            sHtml &= "<td align=""right"">" & Format(tbl.Rows(nIJ).Item("Amount Inc VAT"), "##,###").Replace(",", ".") & "</td>" & vbCrLf
            sHtml &= "</tr>"
            nTotal += tbl.Rows(nIJ).Item("Amount Inc VAT")
        Next
        sHtml &= "<tr class=""active"">" & vbCrLf
        sHtml &= "<td colspan=""3"" align=""right""><b>Tiền hàng:</b> " & "</td>" & vbCrLf
        sHtml &= "<td align=""right""><b>" & Format(nTotal, "##,###").Replace(",", ".") & "</b></td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf

        If sDistrictNo.Trim <> "" Then
            nDeliveryFee = GetDeliveryFeeByAmount(sProvinceNo, sDistrictNo, nTotal)
            nVolumeFee = GetDelieverFeeByVolumeTemp(sCartNo, sProvinceNo, sDistrictNo)
        End If
        sHtml &= "<tr>" & vbCrLf
        sHtml &= "<td colspan=""3"" align=""right""><b>Phí giao hàng:</b> " & "</td>" & vbCrLf
        sHtml &= "<td align=""right""><b>" & Format(nDeliveryFee, "##,##0").Replace(",", ".") & "</b></td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf
        sHtml &= "<tr>" & vbCrLf
        sHtml &= "<td colspan=""3"" align=""right""><b>Phí hàng cồng kềnh:</b> " & "</td>" & vbCrLf
        sHtml &= "<td align=""right""><b>" & Format(nVolumeFee, "##,##0").Replace(",", ".") & "</b></td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf
        sHtml &= "<tr>" & vbCrLf
        sHtml &= "<td colspan=""3"" align=""right""><b>Tổng cộng:</b> " & "</td>" & vbCrLf
        sHtml &= "<td align=""right""><b>" & Format(nTotal + nDeliveryFee + nVolumeFee, "##,##0").Replace(",", ".") & "</b></td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf

        sHtml &= "</table>" & vbCrLf

        Return sHtml
    End Function

End Class
