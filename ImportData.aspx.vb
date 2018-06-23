Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data

Partial Class ImportData
    Inherits System.Web.UI.Page


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        
        'Dim path As String = "E:\Project\CoVi\covisource32\productslist.xlsx"
        'Dim xlApp As Excel.Application
        'Dim xlWorkBook As Excel.Workbook
        'Dim xlWorkSheet As Excel.Worksheet
        'xlApp = New Excel.ApplicationClass
        'xlWorkBook = xlApp.Workbooks.Open(path)
        'xlWorkSheet = xlWorkBook.Worksheets("san pham update")
        ''Try

        'Dim parentCat As String = xlWorkSheet.Cells(2, 1).value
        'Dim parentCatNo As String = ""
        'Dim parentCatLink As String = ""
        'Dim childCatNo As String = ""
        'For i = 2 To 7100
        '    ' parent category
        '    'If xlWorkSheet.Cells(i, 1).value <> "" Then
        '    '    parentCat = xlWorkSheet.Cells(i, 1).value
        '    '    ' add to database
        '    '    Dim objMN As New adv.Business.MenuItem
        '    '    objMN.No_ = CreateNo(0, "")
        '    '    objMN.Name = parentCat
        '    '    objMN.MenuType = 0
        '    '    objMN.MenuOrder = 0
        '    '    objMN.LinkDisplay = VietHoaKhongDau(parentCat).Replace(" ", "-").ToLower()
        '    '    objMN.Published = 1
        '    '    objMN.MetaKeywords = parentCat
        '    '    objMN.Save()
        '    '    parentCatNo = objMN.No_
        '    '    parentCatLink = objMN.LinkDisplay
        '    'End If
        '    '' add child category
        '    'Dim parentChild As String = xlWorkSheet.Cells(i, 2).value
        '    'If parentChild <> "" Then
        '    '    ' add to database
        '    '    Dim objMNChild As New adv.Business.MenuItem
        '    '    objMNChild.No_ = CreateNo(1, parentCatNo)
        '    '    objMNChild.Name = parentChild
        '    '    objMNChild.MenuType = 1
        '    '    objMNChild.ParentNo_ = parentCatNo
        '    '    objMNChild.ParentLink = parentCatLink
        '    '    objMNChild.MenuOrder = 0
        '    '    objMNChild.LinkDisplay = VietHoaKhongDau(parentChild).Replace(" ", "-").ToLower()
        '    '    objMNChild.Published = 1
        '    '    objMNChild.MetaKeywords = parentChild
        '    '    objMNChild.Save()
        '    '    childCatNo = objMNChild.No_

        '    'End If
        '    ' add brand

        '    Dim temp As Double = 0
        '    Dim isNumber As Boolean = Double.TryParse(xlWorkSheet.Cells(i, 3).value, temp)

        '    If String.IsNullOrEmpty(xlWorkSheet.Cells(i, 3).value) = False Then

        '        'Dim arrBrand As Array = xlWorkSheet.Cells(i, 3).value.ToString().Trim().Split(" ")
        '        'Dim brand As String = ""
        '        'Dim ItemNo As String = VietHoaKhongDau(xlWorkSheet.Cells(i, 3).value.ToString.Replace("  ", " ").Replace(" ", "-"))
        '        'If arrBrand.Length > 1 Then
        '        '    brand = arrBrand(0)
        '        '    ' ItemNo = arrBrand(1)
        '        'Else
        '        '    If arrBrand.Length > 2 Then
        '        '        brand = String.Format("{0}{1}", arrBrand(0), arrBrand(1))
        '        '    End If
        '        'End If

        '        ' add Brand
        '        'If CheckExistBrand(brand) = False Then
        '        '    Dim objBrand As New adv.Business.Brand
        '        '    objBrand.No_ = CreateNoBrand()
        '        '    objBrand.Name = brand
        '        '    objBrand.LastDateModify = Date.Now
        '        '    objBrand.Save()
        '        'End If
        '        'add products
        '        ' red imaages
        '        Dim pathImages As String = String.Format("E:\Project\CoVi\public_html\public\assets\img\product\{0}\{1}", xlWorkSheet.Cells(i, 14).value, xlWorkSheet.Cells(i, 10).value).Replace("/", "\")
        '        Dim pathImagesThum As String = String.Format("E:\Project\CoVi\public_html\public\assets\img\product\{0}\covi-thumb-{1}", xlWorkSheet.Cells(i, 14).value, xlWorkSheet.Cells(i, 10).value).Replace("/", "\")
        '        Dim toPathImages As String = String.Format("E:\Project\CoVi\covisource32\covisource32\Images\Product\{0}", xlWorkSheet.Cells(i, 10).value)
        '        Dim toPathImagesThum As String = String.Format("E:\Project\CoVi\covisource32\covisource32\Images\Product\covi-thumb-{0}", xlWorkSheet.Cells(i, 10).value)

        '        'If FileIO.FileSystem.FileExists(pathImages) Then
        '        '    My.Computer.FileSystem.CopyFile(pathImages, toPathImages)
        '        'End If
        '        If FileIO.FileSystem.FileExists(pathImagesThum) Then
        '            My.Computer.FileSystem.CopyFile(pathImagesThum, toPathImagesThum)
        '        End If


        '        'Dim objItem As New adv.Business.Item
        '        'With objItem
        '        '    .No_ = ItemNo
        '        '    .Name = xlWorkSheet.Cells(i, 4).value
        '        '    .Descriptions = xlWorkSheet.Cells(i, 5).value
        '        '    .PageTitle = xlWorkSheet.Cells(i, 4).value
        '        '    .MetaKeywords = xlWorkSheet.Cells(i, 4).value
        '        '    .BrandNo_ = brand
        '        '    .MenuDivisionNo_ = parentCatNo
        '        '    .MenuCategoryNo_ = childCatNo
        '        '    .MenuGroupNo_ = childCatNo
        '        '    .ImagesURL = "covi-thumb-" & xlWorkSheet.Cells(i, 10).value
        '        '    .ImagesThumURL = xlWorkSheet.Cells(i, 10).value
        '        '    .NewProduct = 1
        '        '    .BestSelling = 1
        '        '    .Published = 1
        '        '    .HideFeature = 0
        '        '    .PromotionsProduct = 0
        '        '    .NotInStock = 0
        '        '    .Descriptions = "Giá bán tại Tp Hồ Chí Minh"
        '        '    .SalesPrice = xlWorkSheet.Cells(i, 11).value
        '        '    .LastDateModify = Date.Now
        '        '    .UserID = "admim"
        '        '    .Stock = 10
        '        '    .OrderPosition = i
        '        '    .LinkUrl = VietHoaKhongDau(.Name).Replace(" ", "-").Replace("(", "").Replace(")", "").ToLower()
        '        '    .Save()
        '        'End With
        '        'Dim objItemDes As New adv.Business.ItemDescriptions
        '        'With objItemDes
        '        '    .ItemNo_ = ItemNo
        '        '    .Content = xlWorkSheet.Cells(i, 9).value
        '        '    .UserID = "admin"
        '        '    .DateModify = Date.Now
        '        '    .Save()
        '        'End With

        '    End If


        'Next
        'xlWorkBook.Close()
        'xlApp.Quit()

        'Catch ex As Exception
        '    xlWorkBook.Close()
        '    xlApp.Quit()
        'End Try
        'UpdatePhoneNumber()
        'UpdateBrand()
        ExportProducts()

    End Sub

    Private Sub RenameImages()
        Dim SQL As String
        SQL = String.Format("SELECT * FROM [Item]")
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        'Dim pathImages As String = String.Format("E:\Project\CoVi\public_html\public\assets\img\product\{0}\{1}", xlWorkSheet.Cells(i, 14).value, xlWorkSheet.Cells(i, 10).value).Replace("/", "\")
        'Dim pathImagesThum As String = String.Format("E:\Project\CoVi\public_html\public\assets\img\product\{0}\covi-thumb-{1}", xlWorkSheet.Cells(i, 14).value, xlWorkSheet.Cells(i, 10).value).Replace("/", "\")
        For i = 0 To tbl.Rows.Count - 1
            Dim objItem As New adv.Business.Item
            objItem.Load(tbl.Rows(i).Item("No_"))
            Dim toPathImages As String = String.Format("E:\Project\CoVi\covisource34\covisource34\Images\Product\{0}", objItem.ImagesURL)
            Dim toPathImagesThum As String = String.Format("E:\Project\CoVi\covisource34\covisource34\Images\Product\{0}", objItem.ImagesThumURL)
            If objItem.ImagesURL.Split(".").Length > 1 And objItem.ImagesThumURL.Split(".").Length > 1 Then
                Dim newNameImages As String = String.Format("{0}.{1}", objItem.LinkUrl.Replace("/", "-").Replace("\", "-"), objItem.ImagesURL.Split(".")(1))
                Dim newNameImagesThum As String = String.Format("thum-{0}.{1}", objItem.LinkUrl.Replace("/", "-").Replace("\", "-"), objItem.ImagesThumURL.Split(".")(1))

                Dim RenameImages As String = String.Format("E:\Project\CoVi\covisource34\covisource34\Images\Product\{0}", newNameImages)
                Dim RenameImagesThum As String = String.Format("E:\Project\CoVi\covisource34\covisource34\Images\Product\{0}", newNameImagesThum)

                If FileIO.FileSystem.FileExists(toPathImages) Then
                    If (FileIO.FileSystem.FileExists(RenameImages) = False) Then
                        My.Computer.FileSystem.CopyFile(toPathImages, RenameImages)
                        My.Computer.FileSystem.DeleteFile(toPathImages)
                    End If


                End If
                If FileIO.FileSystem.FileExists(toPathImagesThum) Then
                    If (FileIO.FileSystem.FileExists(RenameImagesThum) = False) Then
                        My.Computer.FileSystem.CopyFile(toPathImagesThum, RenameImagesThum)
                        My.Computer.FileSystem.DeleteFile(toPathImagesThum)
                    End If


                End If
                objItem.ImagesURL = newNameImages
                objItem.ImagesThumURL = newNameImagesThum
                objItem.Save()
            End If
            
        Next
        
    End Sub
    Function CheckExistBrand(ByVal name As String) As Boolean
        Dim SQL As String
        SQL = String.Format("SELECT No_, Name FROM [Brand] where LOWER(Name) like N'%{0}%'", name.ToLower())
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        If tbl.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function VietHoaKhongDau(ByVal value As String) As String
        value = value.Replace(".", " .")
        value = value.Replace("?", " ?")
        ''---------------------------------a^
        value = value.Replace("ấ", "a")
        value = value.Replace("ầ", "a")
        value = value.Replace("ẩ", "a")
        value = value.Replace("ẫ", "a")
        value = value.Replace("ậ", "a")
        '---------------------------------A^
        value = value.Replace("Ấ", "A")
        value = value.Replace("Ầ", "A")
        value = value.Replace("Ẩ", "A")
        value = value.Replace("Ẫ", "A")
        value = value.Replace("Ậ", "A")
        ''---------------------------------a(
        value = value.Replace("ắ", "a")
        value = value.Replace("ằ", "a")
        value = value.Replace("ẳ", "a")
        value = value.Replace("ẵ", "a")
        value = value.Replace("ặ", "a")
        ''---------------------------------A(
        value = value.Replace("Ắ", "A")
        value = value.Replace("Ằ", "A")
        value = value.Replace("Ẳ", "A")
        value = value.Replace("Ẵ", "A")
        value = value.Replace("Ặ", "A")
        ''---------------------------------a
        value = value.Replace("á", "a")
        value = value.Replace("à", "a")
        value = value.Replace("ả", "a")
        value = value.Replace("ã", "a")
        value = value.Replace("ạ", "a")
        value = value.Replace("â", "a")
        value = value.Replace("ă", "a")
        ''---------------------------------A
        value = value.Replace("Á", "A")
        value = value.Replace("À", "A")
        value = value.Replace("Ả", "A")
        value = value.Replace("Ã", "A")
        value = value.Replace("Ạ", "A")
        value = value.Replace("Â", "A")
        value = value.Replace("Ă", "A")
        ''---------------------------------e^
        value = value.Replace("ế", "e")
        value = value.Replace("ề", "e")
        value = value.Replace("ể", "e")
        value = value.Replace("ễ", "e")
        value = value.Replace("ệ", "e")
        ''---------------------------------E^
        value = value.Replace("Ế", "E")
        value = value.Replace("Ề", "E")
        value = value.Replace("Ể", "E")
        value = value.Replace("Ễ", "E")
        value = value.Replace("Ệ", "E")
        ''---------------------------------e
        value = value.Replace("é", "e")
        value = value.Replace("è", "e")
        value = value.Replace("ẻ", "e")
        value = value.Replace("ẽ", "e")
        value = value.Replace("ẹ", "e")
        value = value.Replace("ê", "e")
        ''---------------------------------E
        value = value.Replace("É", "E")
        value = value.Replace("È", "E")
        value = value.Replace("Ẻ", "E")
        value = value.Replace("Ẽ", "E")
        value = value.Replace("Ẹ", "E")
        value = value.Replace("Ê", "E")
        ''---------------------------------i
        value = value.Replace("í", "i")
        value = value.Replace("ì", "i")
        value = value.Replace("ỉ", "i")
        value = value.Replace("ĩ", "i")
        value = value.Replace("ị", "i")
        ''---------------------------------I
        value = value.Replace("Í", "I")
        value = value.Replace("Ì", "I")
        value = value.Replace("Ỉ", "I")
        value = value.Replace("Ĩ", "I")
        value = value.Replace("Ị", "I")
        ''---------------------------------o^
        value = value.Replace("ố", "o")
        value = value.Replace("ồ", "o")
        value = value.Replace("ổ", "o")
        value = value.Replace("ỗ", "o")
        value = value.Replace("ộ", "o")
        ''---------------------------------O^
        value = value.Replace("Ố", "O")
        value = value.Replace("Ồ", "O")
        value = value.Replace("Ổ", "O")
        value = value.Replace("Ô", "O")
        value = value.Replace("Ộ", "O")
        ''---------------------------------o*
        value = value.Replace("ớ", "o")
        value = value.Replace("ờ", "o")
        value = value.Replace("ở", "o")
        value = value.Replace("ỡ", "o")
        value = value.Replace("ợ", "o")
        ''---------------------------------O*
        value = value.Replace("Ớ", "O")
        value = value.Replace("Ờ", "O")
        value = value.Replace("Ở", "O")
        value = value.Replace("Ỡ", "O")
        value = value.Replace("Ợ", "O")
        ''---------------------------------u*
        value = value.Replace("ứ", "u")
        value = value.Replace("ừ", "u")
        value = value.Replace("ử", "u")
        value = value.Replace("ữ", "u")
        value = value.Replace("ự", "u")
        ''---------------------------------U*
        value = value.Replace("Ứ", "U")
        value = value.Replace("Ừ", "U")
        value = value.Replace("Ử", "U")
        value = value.Replace("Ữ", "U")
        value = value.Replace("Ự", "U")
        ''---------------------------------y
        value = value.Replace("ý", "y")
        value = value.Replace("ỳ", "y")
        value = value.Replace("ỷ", "y")
        value = value.Replace("ỹ", "y")
        value = value.Replace("ỵ", "y")
        ''---------------------------------Y
        value = value.Replace("Ý", "Y")
        value = value.Replace("Ỳ", "Y")
        value = value.Replace("Ỷ", "Y")
        value = value.Replace("Ỹ", "Y")
        value = value.Replace("Ỵ", "Y")
        ''---------------------------------DD
        value = value.Replace("Đ", "D")
        value = value.Replace("Đ", "D")
        value = value.Replace("đ", "d")
        ''---------------------------------o
        value = value.Replace("ó", "o")
        value = value.Replace("ò", "o")
        value = value.Replace("ỏ", "o")
        value = value.Replace("õ", "o")
        value = value.Replace("ọ", "o")
        value = value.Replace("ô", "o")
        value = value.Replace("ơ", "o")
        ''---------------------------------O
        value = value.Replace("Ó", "O")
        value = value.Replace("Ò", "O")
        value = value.Replace("Ỏ", "O")
        value = value.Replace("Õ", "O")
        value = value.Replace("Ọ", "O")
        value = value.Replace("Ô", "O")
        value = value.Replace("Ơ", "O")
        ''---------------------------------u
        value = value.Replace("ú", "u")
        value = value.Replace("ù", "u")
        value = value.Replace("ủ", "u")
        value = value.Replace("ũ", "u")
        value = value.Replace("ụ", "u")
        value = value.Replace("ư", "u")
        ''---------------------------------U
        value = value.Replace("Ú", "U")
        value = value.Replace("Ù", "U")
        value = value.Replace("Ủ", "U")
        value = value.Replace("Ũ", "U")
        value = value.Replace("Ụ", "U")
        value = value.Replace("Ư", "U")
        ''---------------------------------
        Return value
    End Function

    Function CreateNo(ByVal nType As Integer, ByVal sParentNo As String) As String
        Dim SQL As String = ""
        Dim nMaxNo As Integer, sMaxNo As String, sNo As String = ""
        Select Case nType
            Case 0
                SQL = " SELECT ISNULL(MAX(No_),'00') FROM [Good Menu] WHERE [Menu Type] = 0 "
                sMaxNo = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
                nMaxNo = CInt(sMaxNo) + 1
                sNo = sRight("00" & nMaxNo, 2)
            Case 1
                SQL = " SELECT ISNULL(MAX(No_),'0000') FROM [Good Menu] WHERE [Menu Type] = 1 AND [Parent No_] = '" & sParentNo & "'"
                sMaxNo = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
                nMaxNo = CInt(sMaxNo) + 1
                sNo = sParentNo & sRight("00" & nMaxNo, 2)
            Case 2
                SQL = " SELECT ISNULL(MAX(No_),'000000') FROM [Good Menu] WHERE [Menu Type] = 2 AND [Parent No_] = '" & sParentNo & "'"
                sMaxNo = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
                nMaxNo = CInt(sMaxNo) + 1
                sNo = sParentNo & sRight("0000" & nMaxNo, 4)
        End Select
        Return sNo
    End Function

    Function CreateNoBrand() As String
        Dim SQL As String = ""
        Dim nMaxNo As Integer, sMaxNo As String, sNo As String = ""
        SQL = " SELECT ISNULL(MAX(No_),'00') FROM [Brand] "
        sMaxNo = DBHelper.ExecuteScalar(GetConnectString, CommandType.Text, SQL)
        nMaxNo = CInt(sMaxNo) + 1
        sNo = sRight("00" & nMaxNo, 2)
        Return sNo
    End Function

    Private Sub UpdatePhoneNumber()
        Dim SQL As String
        SQL = "SELECT * FROM [Item Descriptions]"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        For i = 0 To tbl.Rows.Count - 1
            lblCurrent.Text = i.ToString()
            Dim objItemDes As New adv.Business.ItemDescriptions
            With objItemDes
                .ItemNo_ = tbl.Rows(i).Item("Item No_")
                .DateModify = tbl.Rows(i).Item("Date Modify")
                .UserID = tbl.Rows(i).Item("User ID")
                .Content = tbl.Rows(i).Item("Content").ToString().Replace("0916.88 11 31", "0979 746 282")
                objItemDes.Save()
            End With

        Next
       
    End Sub

    Private Sub UpdateBrand()
        Dim SQL As String
        SQL = "SELECT * FROM [Item]"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        For i = 0 To tbl.Rows.Count - 1
            Dim objItem As New adv.Business.Item
            objItem.Load(tbl.Rows(i).Item("No_"))
            objItem.BrandNo_ = GetBrandNo(objItem.BrandNo_)
            objItem.Save()
        Next

    End Sub

    Private Function GetBrandNo(ByVal randName As String) As String
        Dim Sql As String = String.Format("select * from Brand where LOWER(Name) like N'%{0}%'", randName.ToLower.Trim())
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, Sql).Tables(0)
        If tbl.Rows.Count > 0 Then
            Return tbl.Rows(0).Item("No_")
        End If
        Return ""
    End Function

    Private Sub ExportProducts()
        ' Start Excel and get Application object.
        Dim excel = New Microsoft.Office.Interop.Excel.Application()
        'var dataTable = ConvertToDataTable(listItem);
        ' for making Excel visible
        excel.Visible = False
        excel.DisplayAlerts = False

        ' Creation a new Workbook
        Dim excelworkBook = excel.Workbooks.Add(Type.Missing)
        'excelworkBook.Worksheets.Add(dataTable);

        ' Workk sheet
        Dim excelSheet = DirectCast(excelworkBook.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)
        excelSheet.Name = "Test work sheet"

        ' add column name


        excelSheet.Cells(1, 1) = "ThuTu"
        excelSheet.Cells(1, 2) = "MaSp"
        excelSheet.Cells(1, 3) = "NhaSx"
        excelSheet.Cells(1, 4) = "TenSp"
        excelSheet.Cells(1, 5) = "GiaTien"
        
        Dim count = 2
        Dim SQL As String
        SQL = "select i.No_,i.Name,i.[Sales Price],b.Name as BrandName from Item i left join Brand b on i.[Brand No_] = b.No_"
        Dim tbl As DataTable
        tbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        Dim listItem = New List(Of Product)()
        For ik = 0 To tbl.Rows.Count - 1
            excelSheet.Cells(count, 1) = ik + 1
            excelSheet.Cells(count, 2) = tbl.Rows(ik).Item("No_")
            excelSheet.Cells(count, 3) = tbl.Rows(ik).Item("BrandName")
            excelSheet.Cells(count, 4) = tbl.Rows(ik).Item("Name")
            excelSheet.Cells(count, 5) = tbl.Rows(ik).Item("Sales Price")
            count = count + 1

        Next

        'var excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
        'excelCellrange.EntireColumn.AutoFit();

        'Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
        'border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        'border.Weight = 2d;
        excelworkBook.SaveAs("D:\productslist1.xlsx", AccessMode:=Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared)
        Console.Read()

        '=======================================================
        'Service provided by Telerik (www.telerik.com)
        'Conversion powered by NRefactory.
        'Twitter: @telerik
        'Facebook: facebook.com/telerik
        '=======================================================
    End Sub
End Class

Public Class Product
    Public Property ThuTu() As Integer
        Get
            Return m_ThuTu
        End Get
        Set(value As Integer)
            m_ThuTu = Value
        End Set
    End Property
    Private m_ThuTu As Integer
    Public Property MaSp() As String
        Get
            Return m_MaSp
        End Get
        Set(value As String)
            m_MaSp = Value
        End Set
    End Property
    Private m_MaSp As String
    Public Property TenSp() As String
        Get
            Return m_TenSp
        End Get
        Set(value As String)
            m_TenSp = Value
        End Set
    End Property
    Private m_TenSp As String

    Public Property GioiThieu() As String
        Get
            Return m_GioiThieu
        End Get
        Set(value As String)
            m_GioiThieu = Value
        End Set
    End Property
    Private m_GioiThieu As String

    Public Property TieuDe() As String
        Get
            Return m_TieuDe
        End Get
        Set(value As String)
            m_TieuDe = Value
        End Set
    End Property
    Private m_TieuDe As String

    Public Property MoTa() As String
        Get
            Return m_MoTa
        End Get
        Set(value As String)
            m_MoTa = Value
        End Set
    End Property
    Private m_MoTa As String

    Public Property TuKhoa() As String
        Get
            Return m_TuKhoa
        End Get
        Set(value As String)
            m_TuKhoa = Value
        End Set
    End Property
    Private m_TuKhoa As String

    Public Property NoiDung() As String
        Get
            Return m_NoiDung
        End Get
        Set(value As String)
            m_NoiDung = Value
        End Set
    End Property
    Private m_NoiDung As String

    Public Property Hinh() As String
        Get
            Return m_Hinh
        End Get
        Set(value As String)
            m_Hinh = Value
        End Set
    End Property
    Private m_Hinh As String

    Public Property GiaTien() As String
        Get
            Return m_GiaTien
        End Get
        Set(value As String)
            m_GiaTien = Value
        End Set
    End Property
    Private m_GiaTien As String

    Public Property GiaTienKhuyenMai() As String
        Get
            Return m_GiaTienKhuyenMai
        End Get
        Set(value As String)
            m_GiaTienKhuyenMai = Value
        End Set
    End Property
    Private m_GiaTienKhuyenMai As String

    Public Property GiaoTrongNoiThanh() As String
        Get
            Return m_GiaoTrongNoiThanh
        End Get
        Set(value As String)
            m_GiaoTrongNoiThanh = Value
        End Set
    End Property
    Private m_GiaoTrongNoiThanh As String

    Public Property ThuMucHinh() As String
        Get
            Return m_ThuMucHinh
        End Get
        Set(value As String)
            m_ThuMucHinh = Value
        End Set
    End Property
    Private m_ThuMucHinh As String

    Public Property LinkYoutube() As String
        Get
            Return m_LinkYoutube
        End Get
        Set(value As String)
            m_LinkYoutube = Value
        End Set
    End Property
    Private m_LinkYoutube As String

    Public Property TrangThai() As String
        Get
            Return m_TrangThai
        End Get
        Set(value As String)
            m_TrangThai = Value
        End Set
    End Property
    Private m_TrangThai As String


End Class
