Imports System.Data

Partial Class Administrator_MainternanceAddressList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadData()
    End Sub

    Sub LoadData()
        Dim SQL As String
        SQL = "SELECT M.No_, M.Description, G.Name MenuGroup FROM [Mainternance Address] M LEFT JOIN [Good Menu] G ON M.[Menu No_] = G.No_"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrList.DataSource = tbl
        GrList.DataBind()
    End Sub


    Protected Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click
        Response.Redirect("MainternanceAddressCard.aspx")
    End Sub

    Protected Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        LoadData()
    End Sub
End Class
