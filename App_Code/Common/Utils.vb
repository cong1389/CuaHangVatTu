Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Net

Public Class Utils

    Public Shared Function RenderControlToHtml(ByVal controlToRender As Control) As String
        Dim sb As StringBuilder = New StringBuilder()
        Dim stWriter As HtmlTextWriter = New HtmlTextWriter(New StringWriter(sb))
        controlToRender.RenderControl(stWriter)
        Return sb.ToString()
    End Function


    Public Shared Function DropText(ByVal strChar As String, ByVal numberChar As Integer) As String
        Dim strResult As String = ""
        If Not String.IsNullOrEmpty(strChar) Then
            If numberChar = 0 Then
                Return strChar
            ElseIf numberChar > 0 And numberChar < strChar.Length Then
                ' excute drop by number 
                Dim i As Integer
                For i = numberChar To strChar.Length - 1 Step i + 1
                    If Convert.ToChar(strChar(i)) <> " "c Then
                        numberChar = numberChar + 1
                    Else
                        Exit For
                    End If
                Next
                strResult = strChar.Substring(0, numberChar)
                strResult = strResult + "..."
            Else
                Return strChar
            End If
        End If
        Return strResult
    End Function

    Public Shared Function BreakLine(ByVal strChar As String, ByVal numberChar As Integer) As String
        Dim strResult As String = ""
        If Not String.IsNullOrEmpty(strChar) Then
            If numberChar = 0 Then
                Return strChar
            Else

            End If
        End If
        Return strResult
    End Function

    Public Shared Function CheckExitsImages(ByVal strLink As String) As String
        'string jSFile = ResolveUrl("~/MyProject/JavaScripts/dir/test.js");
        If Not URLExists(strLink) Then
            Return GetUrl() & "Images/noimages.jpg"
        Else
            Return strLink
        End If


    End Function

    Private Shared Function URLExists(ByVal url As String) As Boolean
        Dim result As Boolean = True

        'Dim webRequest As Net.WebRequest = Net.WebRequest.Create(url)
        'webRequest.Timeout = 1200 ' miliseconds
        'webRequest.Method = "HEAD"

        'Try
        '    webRequest.GetResponse()
        'Catch
        '    result = False
        'End Try

        'Return (result)
        Dim request As WebRequest = WebRequest.Create(url)
        ' If required by the server, set the credentials.
        request.Credentials = CredentialCache.DefaultCredentials
        ' Get the response.
        Dim response As WebResponse
        Try
            response = request.GetResponse()
            response.Close()
        Catch
            result = False
        End Try

        Return result
    End Function

End Class
Public Class PageNumber
    Public Const NumberSize As Integer = 20
    Public Const PageSize As Integer = 1000

End Class


