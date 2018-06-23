Imports System.Data

Partial Class Cart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim sCartNo As String = ""
            If Not Request.Cookies("CartNo") Is Nothing Then sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
            If sCartNo.Trim <> "" Then
                LblCart.Text &= ShowCart(sCartNo)
            Else
                Response.Redirect(GetUrl())
            End If
            ImgPayAcept.ImageUrl = GetUrl() & "Images/Template/PayAcept.png"
            LblPathWay.Text = ShowPathWay()

        End If
    End Sub

    Function ShowCart(ByVal sCartNo As String) As String
        Dim SQL As String
        Dim sHtml As String
        Dim tbl As DataTable
        Dim objItem As New adv.Business.Item
        Dim nIJ As Integer, nTotal As Double = 0
        Dim sUrl As String = GetUrl()
        Dim sImgUrl As String = GetImgUrl()
        SQL = " SELECT L.[Line No_], L.[Item No_], I.Name + ' ' + L.Variants Name, L.Quantity, L.[Unit Price], L.[Amount Inc VAT], L.Descriptions " & _
                " FROM [Cart Line] L INNER JOIN Item I ON L.[Item No_] = I.No_ " & _
                " WHERE L.[Cart No_] = '" & sCartNo & "' "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        sHtml = ""
        sHtml &= "<table class='table table-bordered'>" & vbCrLf
        sHtml &= "<tr class='active' >" & vbCrLf
        sHtml &= "<td>" & "Sản phẩm" & "</td>" & vbCrLf
        sHtml &= "<td >" & "Mã sản phẩm" & "</td>" & vbCrLf
        sHtml &= "<td >" & "Tên sản phẩm" & "</td>" & vbCrLf
        sHtml &= "<td >" & "Đơn giá" & "</td>" & vbCrLf
        sHtml &= "<td >" & "Số lượng" & "</td>" & vbCrLf
        sHtml &= "<td>" & "Thành tiền" & "</td>" & vbCrLf
        sHtml &= "<td >" & "Thao tác" & "</td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr>"
            objItem.Load(tbl.Rows(nIJ).Item("Item No_"))
            sHtml &= "<td>" & IIf(objItem.ImagesThumURL.Trim <> "", "<img border=""0"" src=""" & sImgUrl & "Images/Product/" & objItem.ImagesThumURL & """ width=""80"" align=""absmiddle"" alt=""" & objItem.Name & """>", "") & vbCrLf
            sHtml &= "</td>" & vbCrLf
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Item No_") & "</td>" & vbCrLf
            sHtml &= "<td><b>" & tbl.Rows(nIJ).Item("Name") & "</b></td>" & vbCrLf
            sHtml &= "<td align=""right"">" & Format(tbl.Rows(nIJ).Item("Unit Price"), "##,###").Replace(",", ".") & "</td>" & vbCrLf
            sHtml &= "<td> <input name=""L" & tbl.Rows(nIJ).Item("Line No_") & """ type=""text"" class=""form-control"" style=""width:80px;"" value=""" & tbl.Rows(nIJ).Item("Quantity") & """/> </td>" & vbCrLf
            sHtml &= "<td align=""right"">" & Format(tbl.Rows(nIJ).Item("Amount Inc VAT"), "##,###").Replace(",", ".") & "</td>" & vbCrLf
            sHtml &= "<td align=""center"">" & "<a class='btn bg-orange margin' href=""#"" onclick=""del('" & tbl.Rows(nIJ).Item("Line No_") & "')""> Xoá" & "</a>" & "</td>" & vbCrLf
            sHtml &= "</tr>"
            nTotal += tbl.Rows(nIJ).Item("Amount Inc VAT")
        Next
        sHtml &= "<tr class=""cartheader"" style=""height:30px;"">" & vbCrLf
        sHtml &= "<td colspan=""5"" align=""right""><b>Tổng cộng:</b> " & "</td>" & vbCrLf
        sHtml &= "<td align=""right""><b>" & Format(nTotal, "##,###").Replace(",", ".") & "</b></td>" & vbCrLf
        sHtml &= "<td>VNĐ</td>" & vbCrLf
        sHtml &= "</tr>" & vbCrLf
        sHtml &= "</table>" & vbCrLf

        Return sHtml
    End Function

    Function ShowPathWay() As String
        Dim sHtml As String = ""
        sHtml = "<a href=""" & GetUrl() & """ class=""linkheader"">Trang chủ</a> "
        sHtml &= "<b>/Giỏ hàng</b>"
        Return sHtml
    End Function

    Function UpdateCartLine(ByVal sCartNo As String, ByVal LineNo As Integer, ByVal Qty As Integer) As Boolean
        Dim SQL As String
        Try
            SQL = "UPDATE [Cart Line] SET Quantity = " & Qty & ", [Amount Inc VAT]  = [Unit Price] * " & Qty & " WHERE [Cart No_] = '" & sCartNo & "'" & _
                    " AND [Line No_] = " & LineNo
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            Return True
        Catch ex As Exception
            Return False
        End Try
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

    Protected Sub cmdUpdateCart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateCart.Click
        Dim nIJ As Integer
        Dim sCartNo As String, SQL As String
        Dim tbl As DataTable
        Dim nQty As Integer
        sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString

        If TxtLineNo.Value = "0" Or TxtLineNo.Value = "" Then
            SQL = " SELECT [Line No_] FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "' "
            tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
            For nIJ = 0 To tbl.Rows.Count - 1
                nQty = Request("L" & tbl.Rows(nIJ).Item("Line No_"))
                UpdateCartLine(sCartNo, tbl.Rows(nIJ).Item("Line No_"), nQty)
            Next
            LblCart.Text = ShowCart(sCartNo)
        Else
            SQL = " DELETE FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "' AND [Line No_] = " & TxtLineNo.Value & " "
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
            LblCart.Text = ShowCart(sCartNo)
        End If
        TxtLineNo.Value = "0"
        ltrMessage.Text = "Cập nhật thành công!"

    End Sub

    Protected Sub cmdPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPayment.Click
        Dim sCartNo As String, SQL As String, nNum As Integer = 0
        sCartNo = ReturnIfNull(Request.Cookies("CartNo").Value, "").ToString
        SQL = "SELECT ISNULL(COUNT([Item No_]),0) FROM [Cart Line] WHERE [Cart No_] = '" & sCartNo & "'"
        nNum = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
        If nNum = 0 Then
            Response.Redirect(GetUrl)
        Else
            Response.Redirect(GetUrl() & "dia-chi-giao-hang/")
        End If
    End Sub
End Class
