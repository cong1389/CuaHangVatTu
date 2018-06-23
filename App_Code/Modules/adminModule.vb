Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text.RegularExpressions

Public Module adminModule

    Function ShowAdminOrder(ByVal sOrderNo As String, ByVal ShowAmount As Boolean) As String
        Dim objSH As New adv.Business.SalesHeader
        Dim sHtml As String = ""
        If Not objSH.Load(sOrderNo) Then Return ""

        sHtml &= "<div style=""width:897px;padding:4px;"">"
        sHtml &= "<div style=""margin-top:10px;font-size:18px;""><center><b>ĐƠN ĐẶT HÀNG</b></center></div>"

        sHtml &= "<div style=""margin-top:10px;"">"
        sHtml &= "<div style=""margin-top:5px;float:left;width:420px;height:100px;padding:10px;border:1px solid #000;"">"
        sHtml &= "<div style=""margin-top:5px"">Số đơn hàng:" & objSH.No_ & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Ngày hàng:" & Char2Date(objSH.DocumentDate) & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Mã khách hàng:" & objSH.CustomerNo_ & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Tên khách hàng:" & objSH.CustomerName & "</div>"
        sHtml &= "</div>"

        sHtml &= "<div style=""margin-top:5px;float:right;width:420px;height:100px;padding:10px;border:1px solid #000;"">"
        sHtml &= "<div style=""margin-top:5px"">Người nhận:" & objSH.ShiptoName & "  &nbsp;&nbsp;&nbsp;&nbsp;Điện thoại:" & objSH.ShiptoTelephone & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Địa chỉ giao hàng:</div>"
        sHtml &= "<div style=""margin-top:5px;margin-left:30px;"">" & objSH.ShiptoAddress & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Ngày giao:" & Char2Date(objSH.DocumentDate) & " Giời giao:" & objSH.DeliveryFrom & " - " & objSH.DeliveryTo & "</div>"
        sHtml &= "<div style=""margin-top:5px"">Lời nhắn giao hàng:" & objSH.DeliveryComments & "</div>"
        sHtml &= "</div>"
        sHtml &= "</div>"
        sHtml &= "</div>"
        sHtml &= "<div style=""clear:both;""></div>"
        sHtml &= ShowAdminOrderDetail(sOrderNo, True)
        Return sHtml
    End Function

    Function ShowAdminOrderDetail(ByVal sOrderNo As String, ByVal ShowAmount As Boolean) As String
        Dim SQL As String
        SQL = "SELECT [Item No_], [Item Name], Variants, [Unit Of Measure], Quantity, [Unit Price Inc VAT], [Amount Inc VAT] " & _
                " FROM [Sales Line] WHERE [Document No_] = '" & sOrderNo & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        Dim nTotal As Double = 0
        Dim sHtml As String = ""
        sHtml &= "<div style=""padding:5px;margin-top:10px;"">"
        sHtml &= "<table class=""order-detail"" cellpadding=""3"" rules=""cols"">"
        sHtml &= "<tr style=""color:Black;background-color:#FFE061;font-weight:bold;height:26px;"">"
        sHtml &= "<th scope=""col"">Mã hàng</th>"
        sHtml &= "<th scope=""col"">Tên hàng</th>"
        sHtml &= "<th scope=""col"">Size</th>"
        sHtml &= "<th scope=""col"">ĐVT</th>"
        sHtml &= "<th scope=""col"">Số lượng</th>"
        If ShowAmount Then
            sHtml &= "<th scope=""col"">Đơn giá</th>"
            sHtml &= "<th scope=""col"">Thành tiền</th>"
        End If
        sHtml &= "</tr>"
        For nIJ = 0 To tbl.Rows.Count - 1
            If nIJ Mod 2 = 0 Then
                sHtml &= "<tr style=""height:25px;"">"
            Else
                sHtml &= "<tr style=""background-color:#F5F5F5;height:24px;"">"
            End If
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Item No_") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Item Name") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Variants") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Unit Of Measure") & "</td>"
            sHtml &= "<td align=""right"">" & Format(Math.Round(tbl.Rows(nIJ).Item("Quantity"), 0), "##,###,###").Replace(",", ".") & "</td>"
            If ShowAmount Then
                sHtml &= "<td align=""right"">" & Format(Math.Round(tbl.Rows(nIJ).Item("Unit Price Inc VAT"), 0), "##,###,###").Replace(",", ".") & "</td>"
                sHtml &= "<td align=""right"">" & Format(Math.Round(tbl.Rows(nIJ).Item("Amount Inc VAT"), 0), "##,###,###").Replace(",", ".") & "</td>"
                nTotal += tbl.Rows(nIJ).Item("Amount Inc VAT")
            End If
            sHtml &= "</tr>"
        Next
        If ShowAmount Then
            sHtml &= "<tr style=""background-color:#FBF7BD;height:24px;"">"
            sHtml &= "<td colspan=""6""> Tổng cộng: </td>"
            sHtml &= "<td align=""right"">" & Format(Math.Round(nTotal, 0), "##,###,###").Replace(",", ".") & "</td>"
            sHtml &= "</tr>"
        End If
        sHtml &= "</table>"
        sHtml &= "</div>"
        Return sHtml
    End Function
End Module

