Imports System.Data

Partial Class Administrator_ContentConsultantList
    Inherits System.Web.UI.Page

    Protected Sub CmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddNew.Click
        Response.Redirect("ContentConsultantCard.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, " [Menu Type] < 2 ", True, "--")
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Dim SQL As String
        SQL = " SELECT C.Title, C.No_ Code, G.[Name] Category, dbo.Char2Date([Date Create])[Date Create], C.[Link Menu], CONVERT(Bit, C.Published) Published" & _
                " FROM Content C INNER JOIN [Good Menu] G ON C.[Menu Category No_] = G.No_ " & _
                " WHERE C.[Content Type] = 1 AND C.Type = 2 ORDER BY 1, 2 "
        Dim pData As DataTable
        pData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrContent.DataSource = pData
        GrContent.DataBind()
    End Sub
End Class
