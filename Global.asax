<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Dim sCustomerViewKey As String = ""
        If Not Request.Cookies("homeoneguest") Is Nothing Then sCustomerViewKey = ReturnIfNull(Request.Cookies("homeoneguest").Value, "").ToString
        Dim SQL As String = "DELETE FROM [Compare Product] WHERE GuestID = '" & sCustomerViewKey & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, Data.CommandType.Text, SQL)
        Request.Cookies.Remove("SelectedItem")
    End Sub
       
</script>