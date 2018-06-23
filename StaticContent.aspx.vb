
Imports System.Data

Partial Class StaticContent
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sNo As String = ""
        If Not IsNothing(Request("sno")) Then
            sNo = Request("sno")
            Dim sSQL As String = ""
            sSQL = " SELECT * FROM [Content] WHERE [Link Menu] = '" & sNo & "'"
            Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
            If tbl.Rows().Count > 0 Then
                ltrHeader.Text = tbl.Rows(0).Item("Title")
                ltrContent.Text = tbl.Rows(0).Item("Content")
            End If
        End If
    End Sub
End Class
