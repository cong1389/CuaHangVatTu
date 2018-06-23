Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Collections.Generic
Imports System.Web
Imports System.Net

Namespace adv.Business
    Public Class WebEmail
        Private mFrom As String = ""
        Private mTo As String = ""
        Private mSubject As String = ""
        Private mCC As String = ""
        Private mBody As String = ""
        Private mAttachment As Attachment
        Private mIsBodyHtml As Boolean = False

        Property FromAddress As String
            Get
                Return mFrom
            End Get
            Set(ByVal value As String)
                mFrom = value
            End Set
        End Property

        Property ToAddress As String
            Get
                Return mTo
            End Get
            Set(ByVal value As String)
                mTo = value
            End Set
        End Property

        Property Subject As String
            Get
                Return mSubject
            End Get
            Set(ByVal value As String)
                mSubject = value
            End Set
        End Property
        Property CCAdress As String
            Get
                Return mCC
            End Get
            Set(ByVal value As String)
                mCC = value
            End Set
        End Property
        Property AttachmentFile As Attachment
            Get
                Return mAttachment
            End Get
            Set(ByVal value As Attachment)
                mAttachment = value
            End Set
        End Property
        Property IsEmailBodyHtml As Boolean
            Get
                Return mIsBodyHtml
            End Get
            Set(ByVal value As Boolean)
                mIsBodyHtml = value
            End Set
        End Property
        Property EmailBody As String
            Get
                Return mBody
            End Get
            Set(ByVal value As String)
                mBody = value
            End Set
        End Property

        Function SendMail() As String
            Dim objSmtp As New SmtpClient
            Dim objEmail As New MailMessage
            objEmail.Body = mBody
            Dim sTo As Object
            sTo = mTo.Split(";")
            Dim nIJ As Integer
            For nIJ = 0 To UBound(sTo)
                objEmail.To.Add(sTo(nIJ))
            Next
            objEmail.Subject = mSubject
            objEmail.From = New MailAddress(mFrom)
            objEmail.IsBodyHtml = IsEmailBodyHtml
            If Not mAttachment Is Nothing Then
                objEmail.Attachments.Add(mAttachment)
            End If
            objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
            objSmtp.EnableSsl = False
            objSmtp.Host = GetMailServerSmtp()
            objSmtp.Port = CInt(GetMailServerPort())
            GetUrl()
            Try
                Dim credentials As New NetworkCredential(GetEmailAdd, GetEmailPwd)
                objSmtp.UseDefaultCredentials = False
                objSmtp.Credentials = credentials
                objSmtp.Send(objEmail)
                Return "OK"
            Catch ex As Exception
                Return ex.Message
            End Try


        End Function
    End Class
End Namespace