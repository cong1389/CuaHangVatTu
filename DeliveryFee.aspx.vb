Imports System.Data

Partial Class DeliveryFee
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildCombo(CboProvince, adv.Business.List.Province)
            CboProvince.SelectedValue = "008"
            CboProvince_SelectedIndexChanged(CboProvince, e)
            LblDeliveryFeeByAmount.Text = ShowDeliveryFeeByAmount()
            LblDeliveryFeeByVolume.Text = ShowDeliveryFeeByVolume()
            InitCombo()
            CboTimeDelivery.SelectedValue = "008"
            CboTimeDelivery_SelectedIndexChanged(CboTimeDelivery, e)
        End If
    End Sub

    Protected Sub CboProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboProvince.SelectedIndexChanged
        Dim SQL As String = "", sHtml As String = ""
        SQL = " exec W_GetDistric_Zone '" & CboProvince.SelectedValue & "'"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer = 0, kIJ As Integer = 0
        sHtml &= "<table class=""table table-bordered"">"
        sHtml &= "<tr class='active'>"
        sHtml &= "<th style=""width:300px;"">Quận / huyện</th>"
        For nIJ = 2 To tbl.Columns.Count - 1
            sHtml &= "<th> Phân vùng " & tbl.Columns(nIJ).ColumnName & "</th>"
        Next
        sHtml &= "</tr>"
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr>"
            For kIJ = 0 To tbl.Columns.Count - 1
                If kIJ = 0 Then sHtml &= "<td>" & tbl.Rows(nIJ).Item(kIJ) & "</td>"
                If kIJ > 1 Then sHtml &= "<td align=""center"">" & tbl.Rows(nIJ).Item(kIJ) & "</td>"
            Next

            sHtml &= "</tr>"
        Next

        sHtml &= "</table>"
        LblDictricByZone.Text = sHtml
    End Sub

    Function ShowDeliveryFeeByAmount() As String
        Dim sHtml As String = ""
        Dim SQL As String = " SELECT [Zone Code], Descriptions, [Fee Amount] FROM [Delivery Fee By Amount] "
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)

        Dim nIJ As Integer

        sHtml &= "<table class=""table table-bordered"">"
        sHtml &= "<tr class='active'>"
        sHtml &= "<th>Stt</th>"
        sHtml &= "<th style=""width:250px;"">Phân vùng</th>"
        sHtml &= "<th>Trị giá đơn hàng</th>"
        sHtml &= "<th>Phí đóng gói và giao hàng</th>"
        sHtml &= "</tr>"
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<tr>"
            sHtml &= "<td>" & (nIJ + 1) & "</td>"
            sHtml &= "<td>Phân vùng " & tbl.Rows(nIJ).Item("Zone Code") & "</td>"
            sHtml &= "<td>" & tbl.Rows(nIJ).Item("Descriptions") & "</td>"
            sHtml &= "<td align=""right"">" & Format(tbl.Rows(nIJ).Item("Fee Amount"), "##,###,##0") & " VNĐ </td>"
            sHtml &= "</tr>"
        Next
        sHtml &= "</table>"

        Return sHtml
    End Function

    Function ShowDeliveryFeeByVolume() As String
        Dim SQL As String
        SQL = " SELECT [Zone Code], Descriptions, [Fee Amount] FROM [Delivery Fee By Volume] ORDER BY [Entry No_] "
        Dim sHtml As String = "", nIJ As Integer = 0
        Dim tbl As DataTable
        sHtml &= "<table class=""table table-bordered"">"
        SQL = " SELECT * FROM Zone "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        sHtml &= "<tr class='active'>"
        sHtml &= "<th style=""width:200px;"">Kích thước hàng hóa</th>"
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<th>" & tbl.Rows(nIJ).Item("Name") & "</th>"
        Next
        sHtml &= "</tr>"



        SQL = " SELECT [Zone Code], Descriptions, [Fee Amount] FROM [Delivery Fee By Volume] "
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim sFvalue As String = ""
        For nIJ = 0 To tbl.Rows.Count - 1
            If sFvalue <> tbl.Rows(nIJ).Item("Descriptions") Then
                If nIJ <> 0 Then sHtml &= "</tr>"
                sHtml &= "<tr>"
                sHtml &= "<td>" & tbl.Rows(nIJ).Item("Descriptions") & "</td>"
            End If
            If tbl.Rows(nIJ).Item("Fee Amount") = 0 Then
                sHtml &= "<td align=""center"">Miễn phí</td>"
            Else
                sHtml &= "<td align=""right"">" & Format(tbl.Rows(nIJ).Item("Fee Amount"), "##,###,##0") & " VNĐ</td>"
            End If

            sFvalue = tbl.Rows(nIJ).Item("Descriptions")
        Next

        sHtml &= "</table>"
        Return sHtml
    End Function

    Sub InitCombo()
        Dim SQL As String
        SQL = " SELECT No_, Name + ' (' + Convert(nvarchar(2),[Num of Day From]) + ' - ' + Convert(nvarchar(2),[Num of Day To]) + N' + ngày)' Name " & _
            " FROM [Province] WHERE [Type] = 0 ORDER BY [Big City] DESC, Name "
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        CboTimeDelivery.DataSource = tbl
        CboTimeDelivery.DataTextField = "Name"
        CboTimeDelivery.DataValueField = "No_"
        CboTimeDelivery.DataBind()

    End Sub

    Protected Sub CboTimeDelivery_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboTimeDelivery.SelectedIndexChanged
        Dim SQL As String = ""
        SQL = "SELECT Name FROM Province WHERE [Type] = 1 AND [Parent No_] = '" & CboTimeDelivery.SelectedValue & "'"
        Dim tbl As DataTable = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim nIJ As Integer
        Dim sHtml As String = ""
        For nIJ = 0 To tbl.Rows.Count - 1
            sHtml &= "<div class=""documentlist"">" & tbl.Rows(nIJ).Item("Name") & "</div>"
        Next
        sHtml &= "<div style=""clear:both;""></div>"
        LblDistrictList.Text = sHtml
    End Sub
End Class
