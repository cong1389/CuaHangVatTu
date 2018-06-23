Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization

Partial Class facebookCallback
    Inherits System.Web.UI.Page
    Dim objC As New adv.Business.Customer
    Dim objNoSeri As New adv.Business.cNoSeri

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        Dim app_id As String = "628479280520933"
        Dim app_secret As String = "938472aee89c5fc8aff7bd884dd7d572"
        Dim redirectUri As String = String.Format("{0}facebookCallback.aspx", GetUrl())
        Dim code As String = Request("code")

        Dim requestUri As String = Convert.ToString((Convert.ToString((Convert.ToString((Convert.ToString("https://graph.facebook.com/oauth/access_token?client_id=") & app_id) + "&redirect_uri=") & redirectUri) + "&client_secret=") & app_secret) + "&code=") & code

        Dim str As String = getResponseFromUrl(requestUri)
        Dim index1 As Integer = str.IndexOf("&")
        str = str.Remove(index1, str.Length - index1)
        Dim accessToken As String = str.Replace("access_token=", "")
        lblAccessToken.Text = accessToken

        requestUri = Convert.ToString("https://graph.facebook.com/me?access_token=") & accessToken

        Dim data As String = getResponseFromUrl(requestUri)

        Dim json = New JavaScriptSerializer()
        Dim user = json.Deserialize(Of User)(data)
        With objC
            Dim sYM As String = ""
            objNoSeri.Load("CUSTOMERNO")
            sYM = sLeft(Date2Char(getToday()), 6)
            .No_ = objNoSeri.CreateNoSeri(sYM)
            .FullName = user.first_name
            .Sex = user.gender
            .Email = user.username
            .WardNo_ = ""
            .Birthday = ""
            .ReferenceCode = ""
            .BillToName = ""
            .BillToAddress = ""
            .TaxCode = ""
            .CustomerType = ""
            .CustomerPriceGroup = ""
            .LoyaltyCardNo_ = ""
            .RegisterDate = Date2Char(getToday)
            .LastVisited = Date2Char(getToday)

            If Not .Save() Then Exit Sub
            Session("CustomerNo") = .No_
        End With
        Response.Redirect(GetUrl() & "dia-chi-giao-hang/")

    End Sub

    Public Function getResponseFromUrl(url As String) As String
        Dim wr As WebRequest = WebRequest.Create(url)
        Dim ws As WebResponse = wr.GetResponse()
        Dim st As Stream = ws.GetResponseStream()
        Dim sr As New StreamReader(st)
        Dim str As String = sr.ReadToEnd()
        Return str
    End Function

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================
End Class

Public Class User

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(value As Integer)
            m_id = Value
        End Set
    End Property
    Private m_id As Integer
    Public Property name() As String
        Get
            Return m_name
        End Get
        Set(value As String)
            m_name = Value
        End Set
    End Property
    Private m_name As String
    Public Property first_name() As String
        Get
            Return m_first_name
        End Get
        Set(value As String)
            m_first_name = Value
        End Set
    End Property
    Private m_first_name As String
    Public Property last_name() As String
        Get
            Return m_last_name
        End Get
        Set(value As String)
            m_last_name = Value
        End Set
    End Property
    Private m_last_name As String
    Public Property username() As String
        Get
            Return m_username
        End Get
        Set(value As String)
            m_username = Value
        End Set
    End Property
    Private m_username As String
    Public Property gender() As String
        Get
            Return m_gender
        End Get
        Set(value As String)
            m_gender = Value
        End Set
    End Property
    Private m_gender As String
    Public Property locale() As String
        Get
            Return m_locale
        End Get
        Set(value As String)
            m_locale = Value
        End Set
    End Property
    Private m_locale As String
End Class