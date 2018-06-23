Imports System.Data

Partial Class Register
    Inherits System.Web.UI.Page

    Dim objC As New adv.Business.Customer
    Dim objNoSeri As New adv.Business.cNoSeri

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LblPathWay.Text = ShowPathWay()
            BuildCombo(CboCity, adv.Business.List.Province, , True, "Chọn thành phố")
            InitBirthday()
            TxtEmail.Focus()
        End If
    End Sub

    Sub InitBirthday()
        Dim nIJ As Integer

        CboDay.Items.Clear()
        CboDay.Items.Add("Ngày")
        For nIJ = 1 To 31
            CboDay.Items.Add(nIJ)
        Next

        CboMonth.Items.Clear()
        CboMonth.Items.Add("Tháng")
        For nIJ = 1 To 12
            CboMonth.Items.Add(nIJ)
        Next

        CboYear.Items.Clear()
        CboYear.Items.Add("Năm")

        For nIJ = Now.Year - 80 To Now.Year
            CboYear.Items.Add(nIJ)
        Next
        CboYear.SelectedIndex = Now.Year - 25
    End Sub


    Function ShowPathWay() As String
        Dim sHtml As String = ""
        sHtml = "<a href=""" & GetUrl() & """ class=""linkheader"">Trang chủ</a> "
        sHtml &= " > Đăng ký tài khoản"
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

    Protected Sub CboCity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCity.SelectedIndexChanged
        Dim sProvinceNo As String
        sProvinceNo = ReturnIfNull(CboCity.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim
        BuildCombo(CboDistrict, adv.Business.List.District, " [Parent No_] = '" & sProvinceNo & "'", True, "Chọn quận huyện")
        TxtPassword.Attributes("value") = TxtPassword.Text
        TxtConfirmPwd.Attributes("value") = TxtConfirmPwd.Text
    End Sub

    Function CheckIvalid() As Boolean
        Dim bIvalid As Boolean = True
        
        If TxtEmail.Text.Trim = "" Then
            LblWarning.Text = "Bạn phải nhập email."
            TxtEmail.Focus()
            Return False
        End If
        If Not IsEmail(TxtEmail.Text.Trim) Then
            TxtEmail.Focus()
            LblWarning.Text = "Email nhập không đúng."
            Return False
        End If
        If objC.LoadByEmail(TxtEmail.Text.Trim) Then
            LblWarning.Text = "Email đã được đăng ký."
            TxtEmail.Focus()
            Return False
        End If
        If TxtPassword.Text.Trim = "" Then
            TxtPassword.Focus()
            LblWarning.Text = "Bạn phải nhập mật khẩu."
            Return False
        End If
        If TxtConfirmPwd.Text.Trim <> TxtPassword.Text.Trim Then
            LblWarning.Text = "Mật khẩu không trùng nhau."
            Return False
        End If

        If TxtFullName.Text.Trim = "" Then
            LblWarning.Text = "Bạn phải nhập tên."
            TxtFullName.Focus()
            Return False
        End If
        If TxtTelephone.Text.Trim = "" Then
            LblWarning.Text = "Bạn phải nhập điện thoại."
            TxtTelephone.Focus()
            Return False
        End If
        If CboDay.SelectedValue.ToUpper = "NGÀY" Or CboMonth.SelectedValue.ToUpper = "THÁNG" Or CboYear.SelectedValue.ToUpper = "NĂM" Then
            LblWarning.Text = "Bạn phải nhập ngày tháng năm sinh."
            Return False
        End If

        

        Return True
    End Function


    Protected Sub cmdRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        If Not CheckIvalid() Then
            TxtPassword.Attributes("value") = TxtPassword.Text
            TxtConfirmPwd.Attributes("value") = TxtConfirmPwd.Text
            Exit Sub
        End If
        With objC
            Dim sYM As String = ""
            objNoSeri.Load("CUSTOMERNO")
            sYM = sLeft(Date2Char(getToday()), 6)
            .No_ = objNoSeri.CreateNoSeri(sYM)
            .FullName = TxtFullName.Text.Trim
            .Sex = CboTitle.SelectedValue
            .Telephone = TxtTelephone.Text
            .Email = TxtEmail.Text
            .Address = TxtAddress.Text
            .ProvinceNo_ = CboCity.SelectedValue
            .DistrictNo_ = CboDistrict.SelectedValue
            .WardNo_ = ""
            .Birthday = ""
            .ReferenceCode = ""
            .FullAddress = .GetAddress(TxtAddress.Text, "", CboDistrict.SelectedValue, CboCity.SelectedValue)
            .BillToName = ""
            .BillToAddress = ""
            .TaxCode = ""
            .CustomerType = ""
            .CustomerPriceGroup = ""
            .IdentificationCode = TxtPersonalID.Text
            .LoyaltyCardNo_ = ""
            .RegisterDate = Date2Char(getToday)
            .LastVisited = Date2Char(getToday)
            .Password = adv.Business.MD5Encrypt(.Email.Trim & TxtPassword.Text.Trim)
            .ReceivingEmail = IIf(CKReceivingPromotion.Checked, 1, 0)
            If Not .Save() Then Exit Sub
            Session("CustomerNo") = .No_
        End With
        Response.Redirect(GetUrl() & "dia-chi-giao-hang/")
    End Sub

  
End Class
