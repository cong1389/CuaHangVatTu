Imports System.Data

Partial Class Administrator_ItemImage360
    Inherits System.Web.UI.Page
    Dim objItem As New adv.Business.Item
    Dim objItemImages360 As New adv.Business.ItemImages360
    Dim sItemNo As String

    Protected Sub OptFlash_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles OptFlash.CheckedChanged
        OptImage.Checked = Not OptFlash.Checked
    End Sub

    Protected Sub OptImage_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles OptImage.CheckedChanged
        OptFlash.Checked = Not OptImage.Checked
    End Sub

    Protected Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim sFolderName As String = Server.MapPath("../")
        Dim fileExt As String = "", sImgFileName As String = ""
        If FileUpl.HasFile Then
            fileExt = System.IO.Path.GetExtension(FileUpl.FileName)
            If fileExt = ".gif" Or fileExt = ".jpg" Or fileExt = ".png" Or fileExt = ".swf" Then
                If OptFlash.Checked Then
                    sFolderName &= "Media\"
                Else
                    sFolderName &= "Images\Img360\"
                End If
                sImgFileName = sFolderName & FileUpl.FileName
                FileUpl.SaveAs(sImgFileName)
            End If

            If OptFlash.Checked Then
                objItem.InsertFlash360Url(TxtItemNo.Value, FileUpl.FileName)
            Else
                With objItemImages360
                    .ItemNo_ = TxtItemNo.Value
                    .ImagesURL = FileUpl.FileName
                    .PostionOrder = 0
                    .Save()
                End With
            End If
        End If
        ShowData(TxtItemNo.Value)
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            sItemNo = ReturnIfNull(Request("item"), "").ToString.Trim
            If sItemNo = "" Then Response.Redirect("Default.aspx")
            TxtItemNo.Value = sItemNo
            ShowData(sItemNo)
        End If
    End Sub

    Sub ShowData(ByVal sNo As String)
        objItem.Load(sNo)
        If objItem.Flash360Url.Trim <> "" Then
            LblImg.Text = objItem.ShowFlash(objItem.Flash360Url)
            OptImage.Checked = False
            OptFlash.Checked = True
            OptImage.Enabled = False
            OptFlash.Enabled = False
        Else
            Dim s360 As String = ""
            s360 = LoadImages(sNo)
            If s360.Trim <> "" Then
                OptImage.Checked = True
                OptFlash.Checked = False
                OptImage.Enabled = False
                OptFlash.Enabled = False
            End If
            LblImg.Text = s360
        End If

    End Sub

    Function LoadImages(ByVal sItemNo As String) As String
        Dim SQL As String, sUrl As String = GetUrl()
        SQL = " SELECT [Line No_], [Images URL], [Postion Order] FROM [Item Images 360] WHERE [Item No_] = '" & sItemNo & "'" & _
                " ORDER BY [Images URL] "
        Dim tDt As DataTable
        tDt = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        Dim sHtml As String = ""
        For nIJ = 0 To tDt.Rows.Count - 1
            sHtml &= "<div class=""ImgDetail"">" & vbCrLf
            sHtml &= "<div>" & vbCrLf
            sHtml &= "<img src=""" & sUrl & "Images/Img360/" & tDt.Rows(nIJ).Item("Images URL") & """ alt="""" width=""120""/>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "<div>" & vbCrLf
            sHtml &= "<input id=""ckImg"" name=""ImgDetail"" value=""" & tDt.Rows(nIJ).Item("Line No_") & """ type=""checkbox"" />" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
        Next
        Return sHtml
    End Function
End Class
