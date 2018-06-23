Imports System.Data

Partial Class NavHeader
    Inherits System.Web.UI.UserControl
    Dim objC As New adv.Business.Customer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tbl As DataTable
        tbl = GettblMenu(0)

        Dim Urlpath As Array = HttpContext.Current.Request.Url.AbsolutePath.Split("/")
        If Urlpath(Urlpath.Length - 1) <> "Default.aspx" Then
            ShowCategory()
        End If

    End Sub

    Sub ShowCategory()
        Dim sHtmlMainManu As String = ""
        Dim sHtmlFloorFixed As String = ""
        Dim strImage As String = ""
        Dim tbl As DataTable
        tbl = GettblMenu(0)
        Dim nIJ As Integer
        For nIJ = 0 To tbl.Rows.Count - 1
            strImage = " <img src=" & Utils.CheckExitsImages(GetUrl() & "Images/ProductGroup/" & tbl.Rows(nIJ).Item("Images Thum URL")) & " alt=''>"
            sHtmlMainManu &= "          <dl data-role='first-menu' class='cl-item cl-item-sports'>" & vbCrLf
            sHtmlMainManu &= " <dt class='cate-name'>  <a href='" & GetUrl() + "sub-category/" & tbl.Rows(nIJ).Item("Link Display") & "/" & "'>" & strImage & tbl.Rows(nIJ).Item("Name") & vbCrLf
            sHtmlMainManu &= "  </a> </dt>" & vbCrLf

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


End Class
