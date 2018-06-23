Imports System.Data

Partial Class Administrator_CustomerList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadCustomer()
            LoadCustomerReview()
        End If
    End Sub

    Sub LoadCustomer()
        Dim SQL As String
        SQL = " SELECT  C.No_, C.[Full Name], C.[Full Address], C.Email, C.Telephone, P.Name [Province], dbo.Char2Date(C.[Register Date]) [Register Date]" & _
            " FROM Customer C LEFT JOIN Province P ON C.[Province No_] = P.No_ AND P.[Type] = 0 "
        Dim tblC As DataTable
        tblC = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrdCustomerList.DataSource = tblC
        GrdCustomerList.DataBind()
    End Sub

    Sub LoadCustomerReview()
        Dim SQL As String
        SQL = " Select V.[Line No_], V.[Item No_], I.Name [Item Name], V.[Customer Name], V.[Review Text], V.[Customer Rate], dbo.char2date(V.[Review Date])[Review Date], " & _
                " V.[Review Hour], V.[Customer IP], Convert(bit,V.Published) Published " & _
                " from [Customer Reviews] V inner join Item I ON V.[Item No_] = I.No_ " & _
                " WHERE V.Published = 0 "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrdCustomerReview.DataSource = tbl
        GrdCustomerReview.DataBind()
    End Sub

    Protected Sub GrdCustomerReview_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrdCustomerReview.RowDeleting
        Dim nLineNo As String = 0
        Dim sItemNo As String = ""
        sItemNo = GrdCustomerReview.Rows(e.RowIndex).Cells.Item(1).Text()
        nLineNo = GrdCustomerReview.Rows(e.RowIndex).Cells.Item(0).Text()
        Dim SQL As String
        SQL = " DELETE FROM [Customer Reviews] WHERE [Line No_] = " & nLineNo & " AND [Item No_] = '" & sItemNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadCustomerReview()
    End Sub

    Protected Sub GrdCustomerReview_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdCustomerReview.SelectedIndexChanged
        Dim row As GridViewRow = GrdCustomerReview.SelectedRow
        Dim nLineNo As String = 0
        Dim sItemNo As String = ""
        nLineNo = row.Cells.Item(0).Text.Trim
        sItemNo = row.Cells.Item(1).Text.Trim
        Dim SQL As String = ""
        SQL = " UPDATE [Customer Reviews] SET Published = 1 WHERE [Line No_] = " & nLineNo & " AND [Item No_] = '" & sItemNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadCustomerReview()
    End Sub
End Class
