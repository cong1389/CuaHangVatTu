Imports System.Data

Partial Class Common_AdvBanber
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetBaner()
    End Sub
    Sub GetBaner()
        Dim sSQL As String
        Dim tbl As DataTable
        sSQL = " SELECT * FROM [Banner] WHERE [Banner Group No_] = '02' And [IsRun] = 1"
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Dim rHTML As String = ""

        rHTML &= "          <li style='width: 640px;'>" & vbCrLf
        For nIJ = 0 To tbl.Rows.Count - 1
            rHTML &= "<a href='" & GetImgUrl() + "InProcess.aspx" & "'><img src=" & Utils.CheckExitsImages(GetUrl() & "Images/Banners/" & tbl.Rows(nIJ).Item("Images Src")) & " alt='' width='750' height='400'></a>" & vbCrLf

            rHTML &= "          </li>" & vbCrLf
        Next

        ltrBanerRun.Text = rHTML

        '<li><a href="#"><img src="Images/TB1.6i_IFXXXXaXXXXXXXXXXXXX.jpg" class="product-pic" alt="" width="320" height="200"></a></li>

        Dim sSQL1 As String
        Dim tbl1 As DataTable
        sSQL1 = " SELECT top 2 * FROM [Banner] WHERE [Banner Group No_] = '02' And [IsRun] = 0 order by [Position No_]"
        tbl1 = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        Dim rHTML1 As String = ""

        For nIJ = 0 To tbl.Rows.Count - 1
            rHTML1 &= "          <li>" & vbCrLf
            rHTML1 &= "<a href='" & GetImgUrl() + "InProcess.aspx" & "'><img src=" & Utils.CheckExitsImages(GetUrl() & "Images/Banners/" & tbl.Rows(nIJ).Item("Images Src")) & " alt='' class='img-responsive' >" & vbCrLf
            'rHTML1 &= "<a href='" & GetImgUrl() + "InProcess.aspx" & "'><img src=" & Utils.CheckExitsImages(GetUrl() & "Images/Banners/" & tbl.Rows(nIJ).Item("Images Src")) & " alt='' width='320' height='200'>" & vbCrLf

            rHTML1 &= "          </a></li>" & vbCrLf
        Next

        ltrBannerNotRun.Text = rHTML1
    End Sub
End Class
