Imports System.Data

Partial Class Complete
    Inherits System.Web.UI.Page

    Dim objSH As New adv.Business.SalesHeader
    Dim objC As New adv.Business.Customer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sOrderNo As String = Request("orderno")
            LblPathWay.Text = ShowPathWay()

            objSH.Load(sOrderNo)
            objC.Load(objSH.CustomerNo_)
            Dim objMC As New adv.Business.MsgContent
            objMC.Load("W002")
            Dim sOrder As String
            sOrder = objMC.Content
            Dim strOder As String = ShowOrder(sOrderNo, True)
            sOrder = sOrder.Replace("[ORDER]", strOder)

            LblDescription.Text = sOrder
            
        End If
    End Sub

    Function ShowPathWay() As String
        Dim sHtml As String = ""
        sHtml = "<a href=""" & GetUrl() & """ class=""linkheader"">Trang chủ</a> "
        sHtml &= "<b>/Hoàn tất đơn hàng</b>"
        Return sHtml
    End Function

End Class
