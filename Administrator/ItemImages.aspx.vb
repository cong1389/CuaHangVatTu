Imports System.Data
Imports System.Drawing
Imports System.Drawing.Image

Partial Class Administrator_ItemImages
    Inherits System.Web.UI.Page
    Dim objItem As New adv.Business.Item
    Dim objImgDetail As New adv.Business.ItemImages
    Dim sItemNo As String = ""
    Public sItemName As String = ""


    Sub LoadImgDetail(ByVal sItemNo As String)
        Dim SQL As String
        Dim sHtml As String = "", sUrl As String = GetUrl()
        Dim tDt As DataTable
        SQL = " SELECT [Image Natural] Name, 'Images/ProductImages/' + [Image Natural] ImgUrl, 'Images/ProductImages/' + [Image Thumbs] ImgUrlThumb FROM [Item Images] WHERE [Item No_] = '" & sItemNo & "'"
        tDt = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        For nIJ = 0 To tDt.Rows.Count - 1
            sHtml &= "<div class=""ImgDetail"">" & vbCrLf
            sHtml &= "<div>" & vbCrLf
            sHtml &= "<img src=""" & sUrl & tDt.Rows(nIJ).Item("ImgUrlThumb") & """ alt="""" width=""120""/>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "<div>" & vbCrLf
            sHtml &= "<input id=""ckImg"" name = """ & tDt.Rows(nIJ).Item("Name") & """ type=""checkbox"" />" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
        Next
        LblImg.Text = sHtml
    End Sub

    Protected Sub cmdDelImg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelImg.Click

    End Sub

    Protected Sub cmdUploadImg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadImg.Click

        Try

            If FileUpload1.HasFile Then
                LblWarningUploadImg.Text = "Có file"
                Dim fileExt As String
                Dim sImgFileName As String = ""
                Dim sImgThumbFileName As String
                fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)
                LblWarningUploadImg.Text &= fileExt
                If fileExt = ".gif" Or fileExt = ".jpg" Or fileExt = ".png" Then
                    objImgDetail.CreateNew(TxtNo_.Value)
                    Dim sFolderName As String = Server.MapPath("../")
                    sFolderName &= "Images\ProductImages\"
                    sImgFileName = sFolderName & objImgDetail.ImageNatural & fileExt
                    sImgThumbFileName = sFolderName & objImgDetail.ImageThumbs & fileExt
                    LblWarningUploadImg.Text &= sImgFileName
                    FileUpload1.SaveAs(sImgFileName)
                    generateThumbnail(sImgFileName, sImgThumbFileName)
                    objImgDetail.Save()
                    LoadImgDetail(TxtNo_.Value)
                End If
                LblWarningUploadImg.Text = "Cập nhật thành công!"
            Else
                LblWarningUploadImg.Text = "Không có file"
            End If
        Catch ex As Exception
            LblWarningUploadImg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ReturnIfNull(Session("adminuser"), "").ToString.Trim = "" Then
            Response.Redirect("Login.aspx")
        End If
        If Not IsPostBack Then
            Dim Lbl As Label = CType(Master.FindControl("LblMasterMenu"), Label)
            Lbl.Text = "<a href=""ItemList.aspx"">Danh sách </a>"

            sItemNo = ReturnIfNull(Request.QueryString("item"), "")
            TxtNo_.Value = sItemNo

            LoadImgDetail(sItemNo)
            objItem.Load(sItemNo)
            Dim LblW As Label = CType(Master.FindControl("LblMasterMenuDescription"), Label)
            LblW.Text = objItem.Name
        End If
    End Sub

    Function generateThumbnail(ByVal sFileName As String, ByVal sNewFileName As String) As Boolean
        'Create a new Bitmap Image loading from location of origional file
        Dim bm As Bitmap = System.Drawing.Image.FromFile(sFileName)


        'Declare Thumbnails Height and Width
        Dim newWidth As Integer = 120
        Dim newHeight As Integer = (newWidth / bm.Width) * bm.Height


        'Create the new image as a blank bitmap
        Dim resized As Bitmap = New Bitmap(newWidth, newHeight)


        'Create a new graphics object with the contents of the origional image
        Dim g As Graphics = Graphics.FromImage(resized)


        'Resize graphics object to fit onto the resized image
        g.DrawImage(bm, New Rectangle(0, 0, resized.Width, resized.Height), 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel)


        'Get rid of the evidence
        g.Dispose()
        bm.Dispose()

        'Save the new image to the same folder as the origional
        resized.Save(sNewFileName)
        Return True

    End Function
End Class
