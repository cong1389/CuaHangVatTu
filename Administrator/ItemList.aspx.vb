Imports System.Data

Partial Class Administrator_ItemList
    Inherits System.Web.UI.Page
    Dim objGMn As New adv.Business.MenuItem

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CboStatus.SelectedValue = "1"
            InitCombo()
            If Not Request.Cookies("brand") Is Nothing Then CboBrand.SelectedValue = ReturnIfNull(Request.Cookies("brand").Value, "").ToString
            If Not Request.Cookies("menugroup") Is Nothing Then CboMenuGroup.SelectedValue = ReturnIfNull(Request.Cookies("menugroup").Value, "").ToString
            If Not Request.Cookies("itemtype") Is Nothing Then CboType.SelectedValue = ReturnIfNull(Request.Cookies("itemtype").Value, "").ToString
            LoadData()
        End If
    End Sub

    Sub InitCombo()
        BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, , True, "Chọn nhóm")
        BuildCombo(CboBrand, adv.Business.List.Brand, , True, "Chọn brand")
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        GrProduct.PageIndex = 0
        Response.Cookies("menugroup").Value = CboMenuGroup.SelectedValue
        Response.Cookies("menugroup").Expires = Now.AddHours(8)
        Response.Cookies("brand").Value = CboBrand.SelectedValue
        Response.Cookies("brand").Expires = Now.AddHours(8)
        Response.Cookies("itemtype").Value = CboType.SelectedValue
        Response.Cookies("itemtype").Expires = Now.AddHours(8)
        LoadData()
    End Sub

    Sub LoadData()
        Try
            Dim SQL As String, WHERE As String = ""
            If CboStatus.SelectedValue = "1" Then
                WHERE &= " AND I.Published = 1 "
            End If
            If CboStatus.SelectedValue = "2" Then
                WHERE &= " AND I.Published = 0 "
            End If
            If CboMenuGroup.SelectedValue.Trim <> "" Then
                objGMn.Load(CboMenuGroup.SelectedValue.Trim)
                If objGMn.MenuType = 0 Then
                    WHERE &= " AND I.[Menu Category No_] IN (SELECT No_ FROM [Good Menu] WHERE [Menu Type] = 1 AND [Parent No_] = '" & CboMenuGroup.SelectedValue & "')"
                End If
                If objGMn.MenuType = 1 Then
                    WHERE &= " AND I.[Menu Category No_] = '" & CboMenuGroup.SelectedValue & "'"
                End If
                If objGMn.MenuType = 2 Then
                    WHERE &= " AND I.[Menu Group No_] = '" & CboMenuGroup.SelectedValue & "'"
                End If
            End If

            If CboBrand.SelectedValue.Trim <> "" Then
                WHERE &= " AND I.[Brand No_] = '" & CboBrand.SelectedValue.Trim & "'"
            End If

            If TxtSearch.Text.Trim <> "" Then
                WHERE &= " AND (I.No_ LIKE '%" & TxtSearch.Text.Trim & "%' OR I.Name LIKE '%" & TxtSearch.Text.Trim & "%')"
            End If

            SQL = " SELECT I.No_,I.[Division No_], I.[Category No_], I.Name, I.Model, I.[Brand No_], ISNULL(M.Name,'') [Group], " & _
                    " CONVERT(bit,I.[Promotions Product]) [Promotions Product], CONVERT(bit,I.[Best Selling]) [Best Selling], " & _
                    " CONVERT(bit,I.[New Product]) [New Product], CONVERT(bit,I.Published) Published, [Sales Price],[Deal Price], Stock " & _
                    " FROM Item I LEFT JOIN [Good Menu] M ON I.[Menu Category No_] = M.No_ " & _
                    " left join(select [Item No_],[Deal Price]from [Sales Price] ) s on s. [Item No_]=I.No_ " & _
                    " WHERE 1 = 1 " & WHERE & _
                    " ORDER BY 1 DESC "
            Dim tblDT As New DataTable
            tblDT = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
            GrProduct.DataSource = tblDT
            GrProduct.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GrProduct_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrProduct.PageIndexChanging
        GrProduct.PageIndex = e.NewPageIndex
        TxtPageNo.Value = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub CmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddNew.Click
        Response.Redirect("ItemCard.aspx")
    End Sub

    Protected Sub GrProduct_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrProduct.RowDeleting

        Dim sItemNo As String = ""
        sItemNo = GrProduct.Rows(e.RowIndex).Cells.Item(2).Text()
        txtItemNo.Value = sItemNo
        LblWarning.Text = "Bạn thật sự muốn xóa sản phẩm này?"
        ModalPopupWarning.Show()
    End Sub

    Protected Sub cmdCloseWarning_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseWarning.Click
        ModalPopupWarning.Hide()
    End Sub

    Protected Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim SQL As String
        SQL = "DELETE FROM Item WHERE No_ = '" & txtItemNo.Value & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadData()
        ModalPopupWarning.Hide()
    End Sub
End Class
