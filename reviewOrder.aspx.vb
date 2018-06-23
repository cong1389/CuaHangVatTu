Imports System.Data

Partial Class reviewOrder
    Inherits System.Web.UI.Page

    Dim objSH As New adv.Business.SalesHeader
    Dim objC As New adv.Business.Customer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sOrderNo As String = Request("orderno")
            Dim objSH As New adv.Business.SalesHeader
            objSH.Load(sOrderNo)
            lblNuber.Text = sOrderNo
            lblAddress.Text = objSH.ShiptoAddress
            lblDate.Text = objSH.OrderTime

            If (objSH.OrderStatus = 1) Then
                lblstatus.Text = "Mới đặt hàng"
            ElseIf (objSH.OrderStatus = 2) Then
                lblstatus.Text = "Đã duyệt"
            ElseIf (objSH.OrderStatus = 3) Then
                lblstatus.Text = "Đang giao hàng"
            ElseIf (objSH.OrderStatus = 4) Then
                lblstatus.Text = "Đã hoàn thành"
            ElseIf (objSH.OrderStatus = 5) Then
                lblstatus.Text = "Hủy"

            End If
            ltrViewDetail.Text = String.Format("<a href='{0}'>Xem chi tiết</a>", GetUrl() & "hoan-thanh/" & sOrderNo)

        End If
    End Sub

    Function ShowPathWay() As String
        Dim sHtml As String = ""
        sHtml = "<a href=""" & GetUrl() & """ class=""linkheader"">Trang chủ</a> "
        sHtml &= "<b>/Hoàn tất đơn hàng</b>"
        Return sHtml
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function CheckOrder(ByVal orderno As String, ByVal phonenumber As String) As String
        Dim objSH As New adv.Business.SalesHeader
        objSH.Load(orderno)
        If (objSH.No_ <> "" And objSH.ShiptoTelephone = phonenumber) Then
            Return "1"
        Else
            Return "0"
        End If

    End Function

End Class
