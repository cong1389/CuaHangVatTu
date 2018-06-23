Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text.RegularExpressions

Public Module advModule

    
    Public Function getToday() As String
        Return sRight("00" & Today.Day, 2) & "/" & sRight("00" & Today.Month, 2) & "/" & Today.Year
    End Function

    Public Function getFirstOfMonth(ByVal iMonth As Integer, ByVal iYear As Integer) As String
        Return "01/" & sRight("00" & iMonth, 2) & "/" & iYear
    End Function

    Public Function ReturnIfNull(ByVal pValue As Object, ByVal pValueReturn As Object, Optional ByVal TypeOfValue As adv.Business.TypeOfValue = adv.Business.TypeOfValue.Nothing) As Object
        If pValue Is Nothing Then
            Return pValueReturn
        End If
        If pValue.ToString.Trim = "" Then
            Return pValueReturn
        End If
        Dim pType As String
        pType = pValue.GetType.ToString
        If TypeOfValue = adv.Business.TypeOfValue.String Then
            If pValue.GetType.ToString <> "System.String" Then Return pValueReturn
        End If
        Return pValue
    End Function

    Public Function Char2Date(ByVal str As String) As String
        If ReturnIfNull(str, "").ToString.Trim = "" Then Return ""
        Dim pTemp As String
        pTemp = sRight(str.Trim, 2) & "/" & sMid(str.Trim, 5, 2) & "/" & sLeft(str.Trim, 4)
        Return pTemp
    End Function

    Public Function Date2Char(ByVal str As String) As String
        Dim pTemp As String
        pTemp = sRight(str, 4) & sMid(str, 4, 2) & sLeft(str, 2)
        Return pTemp
    End Function

    Public Function sLeft(ByVal str As String, ByVal Lenght As Integer) As String
        Return Microsoft.VisualBasic.Left(str, Lenght)
    End Function

    Public Function sRight(ByVal str As String, ByVal Lenght As Integer) As String
        Return Microsoft.VisualBasic.Right(str, Lenght)
    End Function

    Public Function sMid(ByVal str As String, ByVal start As Integer, ByVal length As Integer) As String
        Return Microsoft.VisualBasic.Mid(str, start, length)
    End Function

    Public Function EnCode(ByVal Str As String, Optional ByVal Agree As Boolean = True, Optional ByVal Length As Integer = 100) As String
        Dim Code As String = "9516234115139741722581274187782127085621833878977232769906493916233147046623365324372128622514282972"
        Dim Oper As String = "1000100000100000000011100001000100001000000101110111110000000001000010011110010100111000000101100101"

        Str = Str.Trim

        Dim stReturn As String = ""
        Dim count As Integer = 0
        Dim k As Integer
        Dim y As Integer = 30

        For i As Integer = 0 To Length - 1
            k = 1
            If CInt("" & Oper.Chars(i)) = 1 Then k = -1
            If Agree Then k = -k

            If i <> Str.Length Then
                If Not Agree AndAlso Asc(Str.Chars(i Mod Str.Length)) + k * CInt("" & Code.Chars(i)) = y Then
                    Exit For
                Else
                    stReturn = stReturn & Chr(Asc(Str.Chars(i Mod Str.Length)) + k * CInt("" & Code.Chars(i)))
                End If
            Else
                stReturn = stReturn & Chr(y + k * CInt("" & Code.Chars(i)))
            End If
        Next
        Return stReturn
    End Function


    Public Function GetConnectString() As String
        Return System.Configuration.ConfigurationManager.AppSettings("ishopconnect").ToString()
    End Function

    Public Function GetImgUrl() As String
        Return System.Configuration.ConfigurationManager.AppSettings("imgurl").ToString()
    End Function

    Public Function GetUrl() As String
        Return System.Configuration.ConfigurationManager.AppSettings("ishopurl").ToString()
    End Function

    Public Function GetAuthenticationCode() As String
        Return System.Configuration.ConfigurationManager.AppSettings("authenticationcode").ToString()
    End Function

    Public Function GetCustomerDefault() As String
        Return System.Configuration.ConfigurationManager.AppSettings("customerdefault").ToString()
    End Function

    Public Function GetCompany() As String
        Return System.Configuration.ConfigurationManager.AppSettings("companyname").ToString()
    End Function

    Public Function GetUrlMobile() As String
        Return System.Configuration.ConfigurationManager.AppSettings("ishopurlmobile").ToString()
    End Function

    Public Function GetMailServerSmtp() As String
        Return System.Configuration.ConfigurationManager.AppSettings("emailsrvsmtp").ToString()
    End Function

    Public Function GetMailServerPort() As String
        Return System.Configuration.ConfigurationManager.AppSettings("emailsrvport").ToString()
    End Function

    Public Function GetEmailAdd() As String
        Return System.Configuration.ConfigurationManager.AppSettings("emailadd").ToString()
    End Function

    Public Function GetEmailPwd() As String
        Return System.Configuration.ConfigurationManager.AppSettings("emailpwd").ToString()
    End Function

    Public Function GetHotline() As String
        Return System.Configuration.ConfigurationManager.AppSettings("ishophotline").ToString()
    End Function

    Public Function GetEmailPacking() As String
        Return System.Configuration.ConfigurationManager.AppSettings("emailpacking").ToString()
    End Function

    Public Function GetBigItemFeeItemNo() As String
        Return System.Configuration.ConfigurationManager.AppSettings("bigitemfeeitemno").ToString()
    End Function

    Public Function GetPakingFeeItemNo() As String
        Return System.Configuration.ConfigurationManager.AppSettings("packingfeeitemno").ToString()
    End Function

    Public Function GetLogo() As String
        Return GetUrl() & "Images/Template/logo.jpg"
    End Function

    Public Function Month2Quarter(ByVal strMonth$) As String
        Dim pMonth As Integer
        Dim pYear As String = ""
        Dim pValue As String = ""
        pYear = sLeft(strMonth, 4)
        pMonth = CInt(sRight(strMonth, 2))
        If pMonth >= 1 And pMonth <= 3 Then
            pValue = pYear & "01"
        End If
        If pMonth >= 4 And pMonth <= 6 Then
            pValue = pYear & "02"
        End If
        If pMonth >= 7 And pMonth <= 9 Then
            pValue = pYear & "03"
        End If
        If pMonth >= 10 And pMonth <= 12 Then
            pValue = pYear & "04"
        End If
        Return pValue
    End Function

    Function ConvertIntoNone(ByVal sInput As String) As String
        Dim VietNamChar() As String = New String(14) {"aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", _
            "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ"}
        Dim nIJ As Integer, kIJ As Integer
        For nIJ = 1 To VietNamChar.Length - 1
            For kIJ = 0 To VietNamChar(nIJ).Length - 1
                sInput = sInput.Replace(VietNamChar(nIJ)(kIJ), VietNamChar(0)(nIJ - 1))
            Next
        Next
        sInput = sInput.ToLower
        sInput = sInput.Replace(" ", "-")
        sInput = sInput.Replace("'", "")
        Return sInput
    End Function


    Public Function IsEmail(ByVal Email As String) As Boolean
        Dim strEmail As String = "^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" & "\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" & ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"

        Dim re As New Regex(strEmail)
        If re.IsMatch(Email) Then
            Return (True)
        Else
            Return (False)
        End If
    End Function

    Function ShowRating(ByVal nRating As Integer) As String
        Dim sHtml As String = "", sUrl As String = ""
        Dim nIJ As Integer
        sUrl = GetUrl()
        For nIJ = 1 To nRating
            sHtml &= "<img border=""0"" src=""" & sUrl & "Images/Template/FilledStar.png"">"
        Next
        For nIJ = nRating + 1 To 5
            sHtml &= "<img border=""0"" src=""" & sUrl & "Images/Template/EmptyStar.png"">"
        Next
        Return sHtml
    End Function

    Function GetPageNo(ByVal sPageLink As String) As String
        Dim SQL As String
        SQL = "SELECT No_ FROM Page WHERE [Link URL] = '" & sPageLink & "'"
        Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
    End Function

  
    Public Function isMobileBrowser() As Boolean
        'GETS THE CURRENT USER CONTEXT
        Dim context As HttpContext = HttpContext.Current

        'FIRST TRY BUILT IN ASP.NT CHECK
        If context.Request.Browser.IsMobileDevice Then
            Return True
        End If

        'THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
        If context.Request.ServerVariables("HTTP_X_WAP_PROFILE") IsNot Nothing Then
            Return True
        End If

        'THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
        If context.Request.ServerVariables("HTTP_ACCEPT") IsNot Nothing AndAlso context.Request.ServerVariables("HTTP_ACCEPT").ToLower().Contains("wap") Then
            Return True
        End If

        'AND FINALLY CHECK THE HTTP_USER_AGENT HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
        If context.Request.ServerVariables("HTTP_USER_AGENT") IsNot Nothing Then
            'Create a list of all mobile types

            Dim mobiles() As String = New String() {"midp", "j2me", "avant", "docomo", "novarra", "palmos", _
                       "palmsource", "240x320", "opwv", "chtml", "pda", "windows ce", _
                       "mmp/", "blackberry", "mib/", "symbian", "wireless", "nokia", _
                       "hand", "mobi", "phone", "cdm", "up.b", "audio", _
                       "SIE-", "SEC-", "samsung", "HTC", "mot-", "mitsu", _
                       "sagem", "sony", "alcatel", "lg", "eric", "vx", _
                       "NEC", "philips", "mmm", "xx", "panasonic", "sharp", _
                       "wap", "sch", "rover", "pocket", "benq", "java", _
                       "pt", "pg", "vox", "amoi", "bird", "compal", _
                       "kg", "voda", "sany", "kdd", "dbt", "sendo", _
                       "sgh", "gradi", "jb", "dddi", "moto", "iphone"}

            'Loop through each item in the list created above 
            'and check if the header contains that text
            For Each s As String In mobiles
                If context.Request.ServerVariables("HTTP_USER_AGENT").ToLower().Contains(s.ToLower()) Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    Function GetDeliveryFeeByAmount(ByVal sProvinceNo As String, ByVal sDisctricNo As String, ByVal nAmount As Double) As Double
        Dim SQL As String
        Dim sZoneCode As String = ""
        SQL = " SELECT [Zone Code] FROM Province WHERE [Type] = 1 AND No_ ='" & sProvinceNo & "'"
        sZoneCode = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), "").ToString.Trim
        If sZoneCode = "" Then Return 0
        SQL = " SELECT ISNULL([Fee Amount],0) FeeAmount" & _
                " FROM [Delivery Fee By Amount] WHERE [Zone Code] = '" & sZoneCode & "' AND [From Order Amount] <= " & nAmount & " AND ([To Order Amount] >= " & nAmount & " OR [To Order Amount] = 0) "
        Return Val(ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0))
    End Function

    Function GetDelieverFeeByVolume(ByVal sCartNo As String) As Double
        Dim SQL As String
        Dim nFeeAmount As Double = 0
        SQL = "SELECT SUM([Delivery Fee]) FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "'"
        nFeeAmount = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
        Return nFeeAmount
    End Function

    Function GetDelieverFeeByVolumeTemp(ByVal sCartNo As String, ByVal sProvinceNo As String, ByVal sDisctricNo As String) As Double
        Dim SQL As String
        SQL = " SELECT dbo.GetDeliveryFeeByVolume('" & sCartNo & "','" & sProvinceNo & "','" & sDisctricNo & "')"
        Dim nFeeAmount As Double = 0
        nFeeAmount = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
        Return nFeeAmount
    End Function

    Function GetAmountOfCartLine(ByVal sCartNo As String) As Double
        Dim SQL As String
        SQL = " SELECT SUM([Amount Inc VAT] + ISNULL([Delievery Fee],0))  FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "'"
        Return ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
    End Function

    Function GetVolumeFee(ByVal sOrderNo As String) As Double
        Dim SQL As String
        Dim nFeeAmount As Double = 0
        SQL = "SELECT SUM([Volume Fee]) FROM [Sales Line] WHERE [Document No_] = '" & sOrderNo & "'"
        nFeeAmount = ReturnIfNull(DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL), 0)
        Return nFeeAmount
    End Function
End Module

