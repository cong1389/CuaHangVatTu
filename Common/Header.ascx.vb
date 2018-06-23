Imports System.Data

Partial Class Common_Header
    Inherits System.Web.UI.UserControl
    Dim objC As New adv.Business.Customer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tbl As DataTable
        tbl = GettblMenu(0)
        ddlSearchCategory.DataSource = tbl
        ddlSearchCategory.DataTextField = "Name"
        ddlSearchCategory.DataValueField = "No_"

        ddlSearchCategory.DataBind()
        ddlSearchCategory.Items.Insert(0, New ListItem("Tất cả", "0"))
        ddlSearchCategory.SelectedIndex = 0
        If ReturnIfNull(Session("CustomerNo"), "").ToString.Trim <> "" Then
            objC.Load(Session("CustomerNo"))
            Dim sWishListNo As String = GetWishList(Session("CustomerNo"))
            Dim objWL As New adv.Business.WishListLine
            'ltrNumberCard.Text = objWL.GetNumberIteam(sWishListNo).ToString()
            ltrLogin.Text = "Hi, " & objC.FullName
            hpLogin.Visible = False
            lbLogout.Visible = True
        End If
        Dim sCartNo As String = ""
        If Not Context.Request.Cookies("CartNo") Is Nothing Then
            sCartNo = ReturnIfNull(Context.Request.Cookies("CartNo").Value, "").ToString.Trim
        End If
        ltrNumberCard.Text = LoadCardItem(sCartNo).Rows.Count

        Dim Urlpath As Array = HttpContext.Current.Request.Url.AbsolutePath.Split("/")
        If Urlpath(Urlpath.Length - 1) <> "Default.aspx" Then
            ShowCategory()
        End If
        hpLogin.NavigateUrl = GetUrl() & "thanh-vien/"

        GetHotLine()

    End Sub
    Public Function GettblMenu(ByVal sLevel As Integer, Optional ByVal sConditions As String = "") As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        If sConditions.Trim <> "" Then sWhere = " AND " & sConditions
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = " & sLevel & " AND Published = 1 AND [Using For] = 0 " & sWhere & " ORDER BY [Menu Order] "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Private Function LoadCardItem(ByVal cardNo As String) As DataTable
        Dim sSQL As String
        sSQL = " SELECT * FROM [Cart Line] WHERE [Cart No_] = '" & cardNo & "'"
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
    End Function

    Sub ShowCategory()
        Dim sHtmlMainManu As String = ""
        Dim sHtmlFloorFixed As String = ""
        Dim tbl As DataTable
        tbl = GettblMenu(0)
        Dim nIJ As Integer
        For nIJ = 0 To tbl.Rows.Count - 1

            sHtmlMainManu &= "          <dl data-role='first-menu' class='cl-item cl-item-sports'>" & vbCrLf
            sHtmlMainManu &= " <dt class='cate-name'><span><a href='" & GetUrl() + "sub-category/" & tbl.Rows(nIJ).Item("Link Display") & "/" & "'>" & tbl.Rows(nIJ).Item("Name") & vbCrLf
            sHtmlMainManu &= "  </a></span> </dt>" & vbCrLf

            sHtmlMainManu &= " <dd data-role='first-menu-main' class='sub-cate'>" & vbCrLf
            sHtmlMainManu &= " <div class='sub-cate-main'>" & vbCrLf

            Dim tblLev1 As DataTable
            tblLev1 = GettblMenu(1, " [Parent No_] = '" & tbl.Rows(nIJ).Item("No_") & "'")
            Dim nIJ1 As Integer
            Dim countRows As Integer
            countRows = 0
            For nIJ1 = 0 To tblLev1.Rows.Count - 1
                If nIJ1 Mod 2 = 0 Then
                    sHtmlMainManu &= " <div class='sub-cate-row'>" & vbCrLf
                End If
                countRows = countRows + 1
                sHtmlMainManu &= "<dl data-role='two-menu' class='sub-cate-items'>" & vbCrLf
                sHtmlMainManu &= "<dt><a href='" & GetImgUrl() + "sub-products-list/" & tblLev1.Rows(nIJ1).Item("Link Display") & "/" & "'>" & tblLev1.Rows(nIJ1).Item("Name") & "</a></dt>" & vbCrLf

                sHtmlMainManu &= "<dd>" & vbCrLf
                Dim tblLev2 As DataTable
                tblLev2 = GettblMenu(2, " [Parent No_] = '" & tblLev1.Rows(nIJ1).Item("No_") & "'")
                Dim nIJ2 As Integer
                For nIJ2 = 0 To tblLev2.Rows.Count - 1
                    Dim linkUrl As String = ""
                    linkUrl = GetUrl() & "products-list/" & tblLev2.Rows(nIJ2).Item("Link Display") & "/"
                    sHtmlMainManu &= "<a href='" & linkUrl & "'>" & tblLev2.Rows(nIJ2).Item("Name") & "</a>" & vbCrLf
                Next

                sHtmlMainManu &= "</dd>" & vbCrLf

                sHtmlMainManu &= "</dl>" & vbCrLf
                If countRows = 2 Then
                    sHtmlMainManu &= "</div>" & vbCrLf
                    countRows = 0
                End If
            Next

            sHtmlMainManu &= "</div></dd>" & vbCrLf
            sHtmlMainManu &= " </dl>" & vbCrLf
        Next

        ltrMainCategory.Text = sHtmlMainManu

    End Sub

    Protected Sub lbLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbLogout.Click
        Session.Abandon()
        Request.Cookies.Clear()
        Response.Redirect("Default.aspx")
    End Sub

    Private Sub GetHotLine()
        Dim sNo As String = ""

        Dim sSQL As String = ""
        sSQL = " SELECT * FROM [Content] WHERE [Link Menu] = 'hotline'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        If tbl.Rows().Count > 0 Then
            'ltrHotlineContent.Text = tbl.Rows(0).Item("Content")
            'ltrHotlineHeader.Text = tbl.Rows(0).Item("Title")
            Dim text As String = ""
            text = text & "<div class='pull-left'>"
            text = text & tbl.Rows(0).Item("Title") & ":</div> "
            text = text & "<div style='color: Red; font-weight: bold' > "
            text = text & tbl.Rows(0).Item("Content") & "</div>"


            ltrHotlineContent.Text = text
        End If

    End Sub

End Class
