Imports System.Data

Partial Class Administrator_MsgContentList
    Inherits System.Web.UI.Page

    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        Response.Redirect("MsgContentCard.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Dim SQL As String = " SELECT * FROM [Msg Content] "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrMsgContentList.DataSource = tbl
        GrMsgContentList.DataBind()
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        LoadData()
    End Sub
End Class
