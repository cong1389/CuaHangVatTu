Imports System.Data

Partial Class Administrator_TechnologyContentList
    Inherits System.Web.UI.Page

    Protected Sub CmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAddNew.Click
        Response.Redirect("TechnologyContentCard.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboContentType, adv.Business.List.TypeOfContent, " No_ > 0 ", True, "Chọn")
            BuildCombo(CboContentGroup, adv.Business.List.GroupContent, " [Content Type] > 0 ", True, "Chọn")
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Dim SQL As String
        Dim sWhere As String = ""
        If ReturnIfNull(CboContentType.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim <> "" Then
            sWhere &= " AND C.[Content Type] = " & CInt(CboContentType.SelectedValue)
        End If

        If ReturnIfNull(CboContentGroup.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim <> "" Then
            sWhere &= " AND C.[Group No_] = '" & CboContentGroup.SelectedValue & "'"
        End If

        SQL = " SELECT C.Title, C.No_ , G.[Group Name],[Date Create] [Created Date], dbo.Char2Date([Date Create]) [Date Create], C.[Link Menu], CONVERT(Bit, C.Published) Published" & _
                " FROM Content C INNER JOIN [Content Group] G ON C.[Group No_] = G.[Group No_] " & _
                " WHERE C.[Content Type] > 0 " & sWhere & _
                "  ORDER BY [Created Date] DESC, 1, 2 "

        Dim pData As DataTable
        pData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrContent.DataSource = pData
        GrContent.DataBind()
    End Sub

    Protected Sub GrContent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrContent.PageIndexChanging
        GrContent.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub cmdLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        LoadData()
    End Sub
End Class
