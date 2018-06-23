Imports System.Data

Partial Class Administrator_MenuGroup
    Inherits System.Web.UI.Page
    Dim objGmn As New adv.Business.GoodMenu

    Sub FillData()
        Dim SQL As String = ""
        Dim sParentNo As String = ""
        Dim pWhereM As String = "", pWhereC As String = "", pWhereD As String = ""
        sParentNo = ReturnIfNull(CboMenuGroup.SelectedValue, "", adv.Business.TypeOfValue.String).ToString.Trim
        If sParentNo <> "" Then
            objGmn.Load(sParentNo)
            Select Case objGmn.MenuType
                Case 0
                    pWhereD &= " AND M.No_ = '" & sParentNo & "'"
                    pWhereC &= " AND M.[Parent No_] = '" & sParentNo & "'"
                    pWhereM &= " AND ( M.[Parent No_] = '" & sParentNo & "' OR M.[Parent No_] IN (SELECT No_ FROM [Good Menu] WHERE [Parent No_] = '" & sParentNo & "') )"
                Case 1
                    pWhereD &= " AND 0 = 1 "
                    pWhereC &= " AND M.No_ = '" & sParentNo & "'"
                    pWhereM &= " AND ( M.[Parent No_] = '" & sParentNo & "' OR M.[Parent No_] IN (SELECT No_ FROM [Good Menu] WHERE [Parent No_] = '" & sParentNo & "') )"
            End Select
        End If

        Select Case CboType.SelectedValue
            Case "0"
                SQL = " SELECT M.No_,'<b> ' + M.Name + ' <b />' [Menu Name], 'Division' [Type], M.[Menu Order] DivOrder, 0 CatOrder, 0 GroupOrder, '' [Parent Menu], M.[Link Display], CONVERT(Bit, M.Published) Published , M.[Menu Order] " & _
                    " FROM [Good Menu] M WHERE 1 = 1  AND [Menu Type] = 0 " & _
                    " ORDER BY 4,5,6 "

            Case "1"

                SQL = " SELECT M.No_,'<b> ' + M.Name + ' <b />' [Menu Name] , 'Division' [Type], M.[Menu Order] DivOrder, 0 CatOrder, 0 GroupOrder, '' [Parent Menu], M.[Link Display], CONVERT(Bit, M.Published) Published , M.[Menu Order] " & _
                   " FROM [Good Menu] M WHERE 1 = 1  AND [Menu Type] = 0 " & pWhereD & _
                   " UNION ALL " & _
                   " SELECT M.No_, '___' + M.Name [Menu Name], 'Division' [Type], D.[Menu Order] DivOrder, M.[Menu Order] CatOrder, 0 GroupOrder, D.Name [Parent Menu], M.[Link Display], CONVERT(Bit, M.Published) Published , M.[Menu Order]  " & _
                   " FROM [Good Menu] M LEFT JOIN [Good Menu] D ON M.[Parent No_] = D.No_ " & _
                   " WHERE 1 = 1  AND M.[Menu Type] = 1 " & pWhereC & _
                   " ORDER BY 4,5,6 "
            Case "", "2"
                SQL = " SELECT M.No_,'<b> ' + M.Name + ' <b />' [Menu Name], 'Division' [Type], M.[Menu Order] DivOrder, 0 CatOrder, 0 GroupOrder, '' [Parent Menu], M.[Link Display], CONVERT(Bit, M.Published) Published , M.[Menu Order] " & _
                   " FROM [Good Menu] M WHERE 1 = 1  AND [Menu Type] = 0 " & pWhereD & _
                   " UNION ALL " & _
                   " SELECT M.No_, '___' + M.Name [Menu Name], 'Division' [Type], D.[Menu Order] DivOrder, M.[Menu Order] CatOrder, 0 GroupOrder, D.Name [Parent Menu], M.[Link Display], CONVERT(Bit, M.Published) Published , M.[Menu Order]  " & _
                   " FROM [Good Menu] M LEFT JOIN [Good Menu] D ON M.[Parent No_] = D.No_ " & _
                   " WHERE 1 = 1  AND M.[Menu Type] = 1 " & pWhereC & _
                   " UNION ALL " & _
                   " SELECT M.No_, '______' + M.Name [Menu Name], 'Division' [Type], D.[Menu Order] DivOrder, C.[Menu Order] CatOrder, M.[Menu Order] GroupOrder, C.Name [Parent Menu], M.[Link Display], CONVERT(Bit, M.Published) Published , M.[Menu Order] " & _
                   " FROM [Good Menu] M LEFT JOIN [Good Menu] C ON  M.[Parent No_] = C.No_ LEFT JOIN [Good Menu] D ON C.[Parent No_] = D.No_ " & _
                   " WHERE 1 = 1  AND M.[Menu Type] = 2 " & pWhereM & _
                   " ORDER BY 4,5,6 "
        End Select

        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        grdMenuGroup.DataSource = tbl
        grdMenuGroup.DataBind()
    End Sub

    Sub InitComboMenu()
        Dim SQL As String, sWHERE As String = ""
        Dim tbl As DataTable

        SQL = " SELECT No_ [DivNo], No_, Name, [Page No_], [Menu Order] [DivOrder], 0 [CatOrder], 0 [GrOrder] FROM [Good Menu] WHERE [Menu Type] = 0 " & _
            " UNION ALL " & _
            " SELECT C.[Parent No_] [DivNo] , C.No_,'___' + C.Name, C.[Page No_], D.[Menu Order] [DivOrder], C.[Menu Order] [CatOrder], 0 [GrOrder] " & _
            " FROM [Good Menu] C LEFT JOIN [Good Menu] D ON C.[Parent No_] = D.No_ WHERE C.[Menu Type] = 1 " & _
            " ORDER BY 4 DESC,5,6,7"


        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        tbl.Rows.InsertAt(tbl.NewRow, 0)
        CboMenuGroup.DataSource = tbl
        CboMenuGroup.DataTextField = "Name"
        CboMenuGroup.DataValueField = "No_"
        CboMenuGroup.DataBind()

    End Sub


    Protected Sub grdMenuGroup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMenuGroup.PageIndexChanging
        grdMenuGroup.PageIndex = e.NewPageIndex
        FillData()
    End Sub

    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        Response.Redirect("MenuGroupCard.aspx")
    End Sub

    Protected Sub grdMenuGroup_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMenuGroup.RowDeleting
        Dim sNo As String
        sNo = grdMenuGroup.Rows(e.RowIndex).Cells.Item(0).Text()
        Dim SQL As String
        SQL = " DELETE FROM [Good Menu] WHERE No_ = '" & sNo & "'"
        DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, SQL)
        FillData()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitComboMenu()
            FillData()
        End If
    End Sub

    Protected Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        FillData()
    End Sub
End Class
