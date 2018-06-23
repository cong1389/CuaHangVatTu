Imports System.Data

Partial Class Administrator_Default
    Inherits System.Web.UI.Page
    Dim objU As New adv.Business.Userdefine

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If ReturnIfNull(Session("adminuser"), "").ToString.Trim = "" Then
            Response.Redirect("Login.aspx")
        End If
        If Not IsPostBack Then
            LblAdminMenu.Text = LoadAdminMenu()
           
        End If
    End Sub

    Function LoadAdminMenu() As String
        Dim sHtml As String = ""
        Dim SQL As String = ""
        Dim tbl As DataTable
        Dim nIJ As Integer
        Dim sUsername As String
        sUsername = adv.Business.MD5Encrypt(Session("adminuser"))
        objU.Load(Session("adminuser"))
        If objU.UserGroupNo_ = "01" Then
            SQL = " SELECT * FROM [Admin Menu] WHERE Show = 1 ORDER BY [Position Order] "
        Else
            SQL = " SELECT * FROM [Admin Menu] WHERE Show = 1 AND No_ IN (SELECT F6 FROM [User Right] WHERE F5 = '" & sUsername & "' AND F1 = '" & adv.Business.MD5Encrypt("1") & "' ) ORDER BY [Position Order] "
        End If
        Try

            tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
            For nIJ = 0 To tbl.Rows.Count - 1
                sHtml &= "<div style=""float:left;"">" & vbCrLf
                sHtml &= "  <div class=""icon"">" & vbCrLf
                sHtml &= "      <a href=""" & tbl.Rows(nIJ).Item("Link To Page") & """>" & vbCrLf
                sHtml &= "          <img src=""../Images/Icon/" & tbl.Rows(nIJ).Item("Link Icon") & """ alt=""" & tbl.Rows(nIJ).Item("Name") & """ align=""middle"" border=""0"">"
                sHtml &= "          <span>" & tbl.Rows(nIJ).Item("Name") & "</span>"
                sHtml &= "      </a>"
                sHtml &= "  </div>"
                sHtml &= "</div>"
            Next
        Catch ex As Exception
            sHtml = ex.Message
        End Try
        Return sHtml
    End Function

   
End Class
