Imports System.Data

Partial Class Administrator_ItemGift
    Inherits System.Web.UI.Page
    Dim sItemNo As String
    Dim objItemGift As New adv.Business.ItemGift
    Dim objItem As New adv.Business.Item

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            sItemNo = Request("item").Trim
            objItem.Load(sItemNo)
            TxtMainItem.Text = objItem.No_
            Dim sConditions As String = ""
            If sItemNo <> "" Then
                sConditions = " No_ IN (SELECT [Unit Of Measure] FROM Item WHERE No_ = '" & sItemNo & "')"
            End If
            BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, , True, "Chọn nhóm")
            BuildCombo(CboUnitOfMeasure, adv.Business.List.UnitOfMeasure, , False)
            LoadData(sItemNo)
        End If
    End Sub

    Sub LoadData(ByVal pItemNo As String)
        Dim SQL As String
        SQL = " select [Discount No_], [Line No_], [Gift Item No_], [Gift Item Name], [Unit of Measure No_], [Gift Quantity], dbo.Char2Date([Starting Date]) [Starting Date]," & _
                " dbo.Char2Date([Ending Date]) [Ending Date] " & _
                " from [Item Gift] where [Item No_] = '" & pItemNo & "' ORDER BY [Line No_] DESC "
        Dim ptbl As DataTable
        ptbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrdGift.DataSource = ptbl
        GrdGift.DataBind()
    End Sub

    Protected Sub FindItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FindItem.Click
        ModalPopupPanel.Show()
    End Sub

    Protected Sub cmdClosePanel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClosePanel.Click
        ModalPopupPanel.Hide()
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        Dim SQL As String, sWhere As String = ""
        Dim objGMn As New adv.Business.GoodMenu

        If CboMenuGroup.SelectedValue.Trim <> "" Then
            objGMn.Load(CboMenuGroup.SelectedValue.Trim)
            If objGMn.MenuType = 0 Then
                sWhere &= " AND I.[Menu Category No_] IN (SELECT No_ FROM [Good Menu] WHERE [Menu Type] = 1 AND [Parent No_] = '" & CboMenuGroup.SelectedValue & "')"
            End If
            If objGMn.MenuType = 1 Then
                sWhere &= " AND I.[Menu Category No_] = '" & CboMenuGroup.SelectedValue & "'"
            End If
            If objGMn.MenuType = 2 Then
                sWhere &= " AND I.[Menu Group No_] = '" & CboMenuGroup.SelectedValue & "'"
            End If

        End If

        If TxtSearch.Text.Trim <> "" Then
            sWhere &= " AND (I.No_ LIKE '%" & TxtSearch.Text.Trim & "%' OR I.Name LIKE '%" & TxtSearch.Text.Trim & "%')"
        End If

        SQL = " select I.No_, I.Name, I.Model, I.[Brand No_], I.[Unit Of Measure], I.Stock from Item I WHERE 1 = 1 " & sWhere & " order by 2 "

        Dim tblDT As New DataTable
        tblDT = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdItem.DataSource = tblDT
        grdItem.DataBind()

    End Sub

    Sub EmptyData()
        TxtOfferNo.Text = ""
        TxtItemNo.Text = ""
        TxtItemName.Text = ""
        TxtQuantity.Text = 1
        TxtLineNo.Value = 0
        TxtStartingDate.Text = getToday()
        TxtEndingDate.Text = ""
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If TxtItemNo.Text.Trim = "" Then
            LblWarning.Text = "Bạn phải chọn mã sản phẩm làm quà tặng bằng cách click link mã quà tặng."
            Exit Sub
        End If

        If Val(TxtQuantity.Text) = 0 Then
            LblWarning.Text = "Bạn phải nhập số lượng quà tặng."
            Exit Sub
        End If

        With objItemGift
            .ItemNo_ = TxtMainItem.Text
            .LineNo_ = Val(TxtLineNo.Value)
            .UnitofMeasureNo_ = CboUnitOfMeasure.SelectedValue
            .Quantity = 1
            .DiscountNo_ = TxtOfferNo.Text
            .GiftItemNo_ = TxtItemNo.Text
            .GiftItemName = TxtItemName.Text
            .GiftUnitofMeasureNo_ = ""
            .GiftQuantity = Val(TxtQuantity.Text)
            .StartingDate = Date2Char(TxtStartingDate.Text)
            .EndingDate = Date2Char(TxtEndingDate.Text)
            .AreaCode = ""
            .SalesCode = ""
            .Save()
        End With
        EmptyData()
        LoadData(TxtMainItem.Text)

    End Sub


    Sub ShowData(ByVal pItemNo As String, ByVal nLineNo As Integer)
        objItemGift.Load(pItemNo, nLineNo)
        TxtOfferNo.Text = objItemGift.DiscountNo_
        TxtItemNo.Text = objItemGift.GiftItemNo_
        TxtItemName.Text = objItemGift.GiftItemName
        TxtQuantity.Text = objItemGift.GiftQuantity
        TxtLineNo.Value = objItemGift.LineNo_
        TxtStartingDate.Text = Char2Date(objItemGift.StartingDate)
        TxtEndingDate.Text = Char2Date(objItemGift.EndingDate)
    End Sub


    Protected Sub GrdGift_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrdGift.RowDeleting
        Dim sLineNo As String, pItemNo As String
        sLineNo = GrdGift.Rows(e.RowIndex).Cells.Item(1).Text()
        pItemNo = TxtMainItem.Text
        Dim SQL As String = ""
        SQL = "DELETE FROM [Item Gift] WHERE [Item No_] = '" & pItemNo & "' AND [Line No_] = " & sLineNo
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadData(pItemNo)
    End Sub

    Protected Sub GrdGift_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdGift.SelectedIndexChanged
        Dim row As GridViewRow = GrdGift.SelectedRow
        Dim nLineNo As Integer
        nLineNo = row.Cells.Item(1).Text.Trim
        ShowData(TxtMainItem.Text, nLineNo)
    End Sub

    Protected Sub cmdItemList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdItemList.Click
        Response.Redirect("ItemList.aspx")
    End Sub
End Class
