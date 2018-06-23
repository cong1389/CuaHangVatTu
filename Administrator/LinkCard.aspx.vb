Imports System.Data

Partial Class Administrator_LinkCard
    Inherits System.Web.UI.Page
    Dim objL As New adv.Business.LinkMenu


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sLinkNo As String = ""
            sLinkNo = ReturnIfNull(Request("linkmenuno"), "").ToString.Trim

            BuildCombo(CboMenuGroup, adv.Business.List.MenuGroup, , True, "")
            InitCombo()
            If sLinkNo.Trim <> "" Then
                LinkMenuNo.Value = sLinkNo
                showData(sLinkNo)
            End If
        End If
    End Sub

    Sub InitCombo()
        Dim SQL As String
        SQL = " SELECT No_, Name FROM Modules WHERE [Is Banner] = 0 "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        tbl.Rows.InsertAt(tbl.NewRow, 0)
        tbl.Rows(0).Item(0) = ""
        tbl.Rows(0).Item(1) = ""
        CboPosition.DataSource = tbl
        CboPosition.DataTextField = "Name"
        CboPosition.DataValueField = "No_"
        CboPosition.DataBind()

        SQL = "SELECT No_, Name FROM [Link Menu] WHERE [Is Group] = 1 ORDER BY [Order Position] "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        tbl.Rows.InsertAt(tbl.NewRow, 0)
        tbl.Rows(0).Item(0) = ""
        tbl.Rows(0).Item(1) = ""
        CboLinkGroup.DataSource = tbl
        CboLinkGroup.DataTextField = "Name"
        CboLinkGroup.DataValueField = "No_"
        CboLinkGroup.DataBind()
    End Sub

    Sub ShowData(ByVal sNo As String)
        objL.Load(sNo)
        With objL
            TxtNo_.Text = .No_
            TxtName.Text = .Name
            CboType.SelectedValue = IIf(.IsGroup = 1, 1, 0)
            CboLinkGroup.SelectedValue = .GroupNo_
            CboPosition.SelectedValue = .PositionNo_
            TxtLink.Text = .URLLink
            TxtCss.Text = .ClassCSS
            CboFloat.SelectedValue = .Float
            CKNewWindow.Checked = IIf(.LinkType = 1, True, False)
            TxtPosition.Text = .OrderPosition
            CboMenuGroup.SelectedValue = .GoodMenuNo_
            CboLinkGroup.SelectedValue = .ParentNo_
            CboLoginStatus.SelectedValue = .LoginStatus

        End With
    End Sub


    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        With objL
            .No_ = TxtNo_.Text
            .Name = TxtName.Text
            .IsGroup = CboType.SelectedValue
            .GroupNo_ = CboPosition.SelectedValue
            .PositionNo_ = CboPosition.SelectedValue
            .URLLink = TxtLink.Text
            .ClassCSS = TxtCss.Text
            .Float = CboFloat.SelectedValue
            .LinkType = IIf(CKNewWindow.Checked, 1, 0)
            .OrderPosition = TxtPosition.Text
            .GoodMenuNo_ = CboMenuGroup.SelectedValue
            .ParentNo_ = CboLinkGroup.SelectedValue
            .LoginStatus = CboLoginStatus.SelectedValue
            .URLLink = TxtLink.Text
            If Not .Save Then Exit Sub
            Response.Redirect("linklist.aspx")
        End With
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("linklist.aspx")
    End Sub
End Class
