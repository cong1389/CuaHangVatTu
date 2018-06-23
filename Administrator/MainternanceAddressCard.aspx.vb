Imports System.Data

Partial Class Administrator_ManternanceAdd
    Inherits System.Web.UI.Page

    Dim objMA As New adv.Business.MainternanceAddress
    Dim sMainternanceAddNo As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitCombo()
            sMainternanceAddNo = ReturnIfNull(Request("mainternanceaddressno"), "")
            If sMainternanceAddNo.Trim <> "" Then
                ShowData(sMainternanceAddNo)
            End If
        End If

    End Sub

    Sub InitCombo()
        Dim SQL As String
        SQL = " SELECT No_ [DivNo], No_, Name, [Menu Order] [DivOrder], 0 [CatOrder], 0 [GrOrder] FROM [Good Menu] WHERE [Menu Type] = 0 " & _
               " UNION ALL " & _
               " SELECT C.[Parent No_] [DivNo] , C.No_,'___' + C.Name, D.[Menu Order] [DivOrder], C.[Menu Order] + 1 [CatOrder], 1 [GrOrder] " & _
               " FROM [Good Menu] C LEFT JOIN [Good Menu] D ON C.[Parent No_] = D.No_ WHERE C.[Menu Type] = 1  " & _
               " ORDER BY 4 DESC,5,6"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        tbl.Rows.InsertAt(tbl.NewRow, 0)
        tbl.Rows(0).Item("No_") = ""
        tbl.Rows(0).Item("Name") = "Chọn nhóm sản phẩm"

        CboMenuGroup.DataSource = tbl
        CboMenuGroup.DataTextField = "Name"
        CboMenuGroup.DataValueField = "No_"
        CboMenuGroup.DataBind()
        CboMenuGroup.SelectedIndex = 0
    End Sub

    Sub ShowData(ByVal sNo As String)
        With objMA
            .Load(sNo)
            TxtNo.Value = sNo
            TxtDescription.Text = .Description
            'CboMenuGroup.SelectedValue = .MenuNo_
            TxtContent.Value = .Content
        End With
        LoadListMenuGroup(sNo)
    End Sub


    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sNo As String
        If TxtNo.Value.Trim = "" Then
            Dim sYM As String = ""
            sYM = sLeft(Date2Char(getToday()), 6)
            Dim objSeriNo As New adv.Business.cNoSeri
            objSeriNo.Load("PROCONTNO ")
            sNo = objSeriNo.CreateNoSeri(sYM)
        Else
            sNo = TxtNo.Value
        End If
        With objMA
            .No_ = sNo
            '.MenuNo_ = CboMenuGroup.SelectedValue
            .Description = TxtDescription.Text
            .Content = TxtContent.Value
            .Save()
            Response.Redirect("MainternanceAddressList.aspx")
        End With
    End Sub

    Protected Sub cmdSaveMainternanceAddByMenu_Click(sender As Object, e As System.EventArgs) Handles cmdSaveMainternanceAddByMenu.Click
        If ReturnIfNull(CboMenuGroup.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim = "" Then
            LblWarningAddByMenu.Text = "Bạn phải chọn nhóm sản phẩm"
            Exit Sub
        End If
        SaveAddByMenuGroup(TxtNo.Value, CboMenuGroup.SelectedValue)
        LoadListMenuGroup(TxtNo.Value)
    End Sub

    Sub SaveAddByMenuGroup(sAddNo As String, sMenuNo As String)
        Dim SQL As String
        SQL = " INSERT INTO [Mainternance Add By Menu] ([Mainternance Add No_], [Menu Group No_]) VALUES ('" & sAddNo & "','" & sMenuNo & "')"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
    End Sub

    Sub LoadListMenuGroup(sMainternanceAddNo As String)
        Dim SQL As String
        SQL = " SELECT G.No_, G.Name MenuGroup FROM [Mainternance Add By Menu] M LEFT JOIN [Good Menu] G ON M.[Menu Group No_] = G.No_ WHERE M.[Mainternance Add No_] = '" & sMainternanceAddNo & "' "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        GrList.DataSource = tbl
        GrList.DataBind()
    End Sub

    Protected Sub GrList_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrList.RowDeleting
        Dim sMenuNo As String
        sMenuNo = GrList.Rows(e.RowIndex).Cells.Item(0).Text()
        Dim SQL As String
        SQL = " DELETE FROM [Mainternance Add By Menu] WHERE [Mainternance Add No_] = '" & TxtNo.Value & "' AND [Menu Group No_] = '" & sMenuNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        LoadListMenuGroup(TxtNo.Value)
    End Sub
End Class
