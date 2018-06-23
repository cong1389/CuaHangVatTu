Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Net
Imports System.IO

Public Class VPCRequest
    Private _address As Uri
    Dim _requestFields As String(,)
    Private _rawResponse As [String]
    Private _responseFields As String(,)
    Private _secureSecret As [String]

    Public Sub New(ByVal URL As [String])
        _address = New Uri(URL)
    End Sub

    Public Sub SetSecureSecret(ByVal secret As [String])
        _secureSecret = secret
    End Sub
    Public Sub SetArrayRequest(ByVal data As String(,))
        _requestFields = data
    End Sub

    Public Sub SetArrayResponse(ByVal data As String(,))
        _responseFields = data
    End Sub

    Public Sub AddDigitalOrderField(ByVal key As [String], ByVal value As [String])
        If Not [String].IsNullOrEmpty(value) Then
        End If
    End Sub

    Public Function GetResultField(ByVal key As [String], ByVal defValue As [String]) As [String]
        Dim value As [String]
        value = ""
        For j As Integer = 0 To _responseFields.GetLength(0) - 1
            If _responseFields.GetValue(j, 0) = key Then
                Return _responseFields.GetValue(j, 1)
            End If
        Next
        Return defValue
    End Function

    Public Function GetResultField(ByVal key As [String]) As [String]
        Return GetResultField(key, "")
    End Function

    Private Function GetRequestRaw() As [String]
        Dim data As New StringBuilder()
        Dim j As Integer
        For j = 0 To (_requestFields.GetLength(0) - 1)
            If Not [String].IsNullOrEmpty(_requestFields.GetValue(j, 1)) Then
                data.Append(_requestFields.GetValue(j, 0) & "=" & HttpUtility.UrlEncode(_requestFields.GetValue(j, 1)) & "&")
            End If
        Next
        'remove trailing & from string
        If data.Length > 0 Then
            data.Remove(data.Length - 1, 1)
        End If
        Return data.ToString()
    End Function

    Public Function GetTxnResponseCode() As String
        Return GetResultField("vpc_TxnResponseCode")
    End Function

    '_____________________________________________________________________________________________________
    ' Three-Party order transaction processing

    Public Function Create3PartyQueryString() As [String]
        Dim url As New StringBuilder()
        'Payment Server URL
        url.Append(_address)
        url.Append("?")
        'Create URL Encoded request string from request fields 
        url.Append(GetRequestRaw())
        'Hash the request fields
        url.Append("&vpc_SecureHash=")
        url.Append(CreateSHA256Signature(True))
        Return url.ToString()
    End Function

    Public Function Process3PartyResponse(ByVal nameValueCollection As System.Collections.Specialized.NameValueCollection) As String
        Dim _buf As String(,)
        ReDim _buf(100, 2)
        _responseFields = _buf
        Dim i = 0
        For Each item As String In nameValueCollection
            If Not item.Equals("vpc_SecureHash") AndAlso Not item.Equals("vpc_SecureHashType") Then
                _responseFields(i, 0) = item
                _responseFields(i, 1) = nameValueCollection(item)
                i = i + 1
            End If
        Next

        If Not nameValueCollection("vpc_TxnResponseCode").Equals("0") AndAlso Not [String].IsNullOrEmpty(nameValueCollection("vpc_Message")) Then
            If Not [String].IsNullOrEmpty(nameValueCollection("vpc_SecureHash")) Then
                If Not CreateSHA256Signature(False).Equals(nameValueCollection("vpc_SecureHash")) Then
                    Return "INVALIDATED"
                End If
                Return "CORRECTED"
            End If
            Return "CORRECTED"
        End If

        If [String].IsNullOrEmpty(nameValueCollection("vpc_SecureHash")) Then
            'no sercurehash response
            Return "INVALIDATED"
        End If
        If Not CreateSHA256Signature(False).Equals(nameValueCollection("vpc_SecureHash")) Then
            Return "INVALIDATED"
        End If
        Return "CORRECTED"
    End Function

    '_____________________________________________________________________________________________________

    '______________________________________________________________________________
    ' SHA256 Hash Code

    Public Function CreateSHA256Signature(ByVal useRequest As Boolean) As String
        ' Hex Decode the Secure Secret for use in using the HMACSHA256 hasher
        ' hex decoding eliminates this source of error as it is independent of the character encoding
        ' hex decoding is precise in converting to a byte array and is the preferred form for representing binary values as hex strings. 
        Dim convertedHash As Byte() = New Byte(_secureSecret.Length \ 2 - 1) {}
        For i As Integer = 0 To _secureSecret.Length \ 2 - 1
            convertedHash(i) = CByte(Int32.Parse(_secureSecret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber))
        Next

        ' Build string from collection in preperation to be hashed
        Dim sb As New StringBuilder()
        Dim list As String(,) = (If(useRequest, _requestFields, _responseFields))
        ' Sort list

        SortArray(list)

        For j As Integer = 0 To list.GetLength(0) - 1
            If list.GetValue(j, 0) IsNot Nothing AndAlso (list.GetValue(j, 0).StartsWith("vpc_") OrElse list.GetValue(j, 0).StartsWith("user_")) Then
                sb.Append(list.GetValue(j, 0) + "=" + list.GetValue(j, 1) & "&")
            End If
        Next
        ' remove trailing & from string
        If sb.Length > 0 Then
            sb.Remove(sb.Length - 1, 1)
        End If

        ' Create secureHash on string
        Dim hexHash As String = ""
        Using hasher As New HMACSHA256(convertedHash)
            Dim hashValue As Byte() = hasher.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()))
            For Each b As Byte In hashValue
                hexHash += b.ToString("X2")
            Next
        End Using
        Return hexHash
    End Function

    Function SortArray(ByVal MyArray)

        Dim keepChecking
        Dim loopCounter
        Dim firstKey
        Dim secondKey
        Dim firstValue
        Dim secondValue

        keepChecking = True
        loopCounter = 0

        Do Until keepChecking = False
            keepChecking = False
            For loopCounter = 0 To (UBound(MyArray) - 1)
                If MyArray(loopCounter, 0) > MyArray((loopCounter + 1), 0) Then
                    ' transpose the key
                    firstKey = MyArray(loopCounter, 0)
                    secondKey = MyArray((loopCounter + 1), 0)
                    MyArray(loopCounter, 0) = secondKey
                    MyArray((loopCounter + 1), 0) = firstKey
                    ' transpose the key's value
                    firstValue = MyArray(loopCounter, 1)
                    secondValue = MyArray((loopCounter + 1), 1)
                    MyArray(loopCounter, 1) = secondValue
                    MyArray((loopCounter + 1), 1) = firstValue
                    keepChecking = True
                End If
            Next
        Loop
        SortArray = MyArray
    End Function
End Class

