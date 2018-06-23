Imports System.Data

Partial Class Administrator_Sales_Management
    Inherits System.Web.UI.Page
    Dim objSH As New adv.Business.SalesHeader
    Dim script As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TxtStartingDate.Text = getFirstOfMonth(Now.Month, Now.Year)
            TxtEndingDate.Text = getToday()
            GetListOrder()
        End If
    End Sub

    Sub GetListOrder()
        Dim SQL As String
        SQL = " SELECT H.No_, dbo.Char2Date(H.[Document Date]) [Order Date], H.[Customer Name], H.[Ship to Address], SUM(L.[Amount Inc VAT]) [Amount Inc VAT], " & _
                " [Order Status] = CASE H.[Order Status] WHEN 0 THEN N'Chưa đặt hàng hoàn tất' WHEN 1 THEN N'Mới đặt hàng' " & _
                " WHEN 2 THEN N'Đang đóng gói tại kho' WHEN 3 THEN N'Đang đi giao' WHEN 4 THEN N'Hoàn tất' WHEN 5 THEN N'Hủy' END " & _
                " FROM [Sales Header] H INNER JOIN [Sales Line] L ON H.No_ = L.[Document No_] " & _
                " WHERE H.[Document Date] BETWEEN '" & Date2Char(TxtStartingDate.Text) & "' AND '" & Date2Char(TxtEndingDate.Text) & "'" & _
                IIf(CboStatus.SelectedValue <> "0", " AND H.[Order Status] = " & CboStatus.SelectedValue, "") & _
                " GROUP BY H.No_, H.[Document Date], H.[Customer Name], H.[Ship to Address], H.[Order Status] ORDER BY H.[Document Date] DESC "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, Data.CommandType.Text, SQL).Tables(0)
        grdSaleOrderList.DataSource = tbl
        grdSaleOrderList.DataBind()
    End Sub

    Protected Sub ajaxButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ajaxButton.Click
        LblOrderDetail.Text = ShowAdminOrder(txtOrderNo.Value, True)
        ModalPopupSalesOrder.Show()
    End Sub

    Protected Sub grdSaleOrderList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSaleOrderList.PageIndexChanging
        grdSaleOrderList.PageIndex = e.NewPageIndex
        GetListOrder()
    End Sub

    Protected Sub cmdCloseModalPopupSalesOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseModalPopupSalesOrder.Click
        ModalPopupSalesOrder.Hide()
    End Sub

    Protected Sub cmdAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAccept.Click
        System.Threading.Thread.Sleep(1000)
        objSH.Load(txtOrderNo.Value)
        If objSH.OrderStatus <> 1 Then
            script = "alert('Đơn hàng đã được xử lý rồi')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallOK", script, True)
            Exit Sub
        End If
        objSH.ApprovalByHomeone(objSH.No_)
        GetListOrder()
        System.Threading.Thread.Sleep(5000)
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        System.Threading.Thread.Sleep(1000)
        objSH.Load(txtOrderNo.Value)
        If objSH.OrderStatus = 4 Then
            script = "alert('Đơn hàng đã hoàn tất không thể hủy')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
            Exit Sub
        End If
        objSH.Cancel(objSH.No_, CKEmail.Checked)
        GetListOrder()
        script = "alert('Đơn hàng đã được hủy')"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallCancel", script, True)
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        GetListOrder()
    End Sub

    Protected Sub cmdDelivery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelivery.Click
        System.Threading.Thread.Sleep(1000)
        objSH.Load(txtOrderNo.Value)
        If objSH.OrderStatus = 4 Then
            script = "alert('Đơn hàng đã hoàn tất không thể cập nhật lại tình trạng')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
            Exit Sub
        End If
        objSH.Delivery(objSH.No_, CKEmail.Checked)
        GetListOrder()
        script = "alert('Đơn hàng đã được chuyển sang tình trạng đang giao')"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallCancel", script, True)
    End Sub

    Protected Sub cmdFinished_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFinished.Click
        System.Threading.Thread.Sleep(1000)
        If objSH.OrderStatus = 4 Then
            script = "alert('Đơn hàng đã hoàn tất không thể cập nhật lại tình trạng')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCall", script, True)
            Exit Sub
        End If
        objSH.Finished(objSH.No_)
        GetListOrder()
        script = "alert('Đơn hàng đã được chuyển sang tình trạng hoàn tất')"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "jsCallCancel", script, True)
    End Sub
End Class
