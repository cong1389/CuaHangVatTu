Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Runtime.Serialization.Json


Public Module dnvModule

    Function SendDeviceToken(TokenID As String, OSName As String) As String
        Dim cSql As String
        Try
            cSql = "IF NOT EXISTS(SELECT DeviceToken FROM DeviceList WHERE  DeviceToken='" & TokenID & "')" & Chr(13) & _
                "   INSERT INTO DeviceList (DeviceToken,OSName,CustomerID,LastOnline,LastPushNotifications) VALUES ('" & TokenID & "','" & OSName & "',NULL,NULL,NULL) " & Chr(13) & _
                "ELSE " & Chr(13) & _
                "   UPDATE DeviceList SET OSName='" & OSName & "'CustomerID=NULL,LastOnline=NULL,LastPushNotifications=NULL WHERE DeviceToken='" & TokenID & "'"
            If DBHelper.ExecuteNonQuery(GetConnectString, CommandType.Text, cSql) > 0 Then
                Return "{""result"":""OK""}"
            End If
            Return "{""result"":""ERROR""}"
        Catch ex As Exception
            Return "{""result"":""ERROR""}"
        End Try
    End Function
    Function searchProductByKeyword(Optional sKeywork As String = "") As String
        Dim cCond As String = ""
        If sKeywork <> "" Then
            cCond = " WHERE I.[Name] like '%" & sKeywork & "%' OR D.[Name] like '%" & sKeywork & "%' OR C.[Name] like '%" & sKeywork & "%'"
        End If
        Dim cSql As String
        Dim tblData As DataTable
        Try
            cSql = "SELECT TOP 1000 I.[No_], I.[Name], I.[Model], I.[Brand No_], I.[Division No_], I.[Category No_], I.[Produc Group No_], I.[Menu Division No_], " & Chr(13) & _
                    "I.[Menu Category No_], I.[Menu Group No_], I.[Promotions Product], I.[Best Selling], I.[New Product], I.[Hide Feature], " & Chr(13) & _
                    "I.[Not In Stock], I.[VAT Percent], I.[Descriptions], I.[Published], I.[Sales Price], I.[Price Icl VAT], I.[Images URL], " & Chr(13) & _
                    "I.[Images Thum URL], I.[Page Title], I.[Meta Keywords], I.[Last Date Modify], I.[User ID], I.Stock, I.[Origin Country], " & Chr(13) & _
                    "I.[Order Position], I.[Hot Product], I.[VAT Group], I.[From Date], I.[Item Same Model], I.[Unit Of Measure], I.[Good Going On], I.[Num Click], I.[Link URL] " & Chr(13) & _
                    "FROM Item I INNER JOIN [Division] D ON D.[No_]=I.[Division No_] INNER JOIN [Item Category] C ON C.[No_]=I.[Category No_]" & cCond & " ORDER BY 1,2"
            tblData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, cSql).Tables(0)
            tblData.TableName = "products"
            Return toJSON(tblData)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Function SearchProductByBarcode(sBarcode As String) As String
        Dim cCond As String = "Where Barcode='" & sBarcode & "'"
        Dim cSql As String
        Dim tblData As DataTable
        Try
            cSql = "SELECT [No_], [Name], [Model], [Brand No_], [Division No_], [Category No_], [Produc Group No_], [Menu Division No_], " & Chr(13) & _
                    "[Menu Category No_], [Menu Group No_], [Promotions Product], [Best Selling], [New Product], [Hide Feature], " & Chr(13) & _
                    "[Not In Stock], [VAT Percent], [Descriptions], [Published], [Sales Price], [Price Icl VAT], [Images URL], " & Chr(13) & _
                    "[Images Thum URL], [Page Title], [Meta Keywords], [Last Date Modify], [User ID], Stock, [Origin Country], " & Chr(13) & _
                    "[Order Position], [Hot Product], [VAT Group], [From Date], [Item Same Model], [Unit Of Measure], [Good Going On], [Num Click], [Link URL] " & Chr(13) & _
                    "FROM Item " & cCond
            tblData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, cSql).Tables(0)
            tblData.TableName = "products"
            Return toJSON(tblData)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Function GetProductByDiv(Optional sDiv As String = "") As String
        Dim cCond As String = "Where [Division No_]='" & sDiv & "'"
        Dim cSql As String
        Dim tblData As DataTable
        Try
            cSql = "SELECT [No_], [Name], [Model], [Brand No_], [Division No_], [Category No_], [Produc Group No_], [Menu Division No_], " & Chr(13) & _
                    "[Menu Category No_], [Menu Group No_], [Promotions Product], [Best Selling], [New Product], [Hide Feature], " & Chr(13) & _
                    "[Not In Stock], [VAT Percent], [Descriptions], [Published], [Sales Price], [Price Icl VAT], [Images URL], " & Chr(13) & _
                    "[Images Thum URL], [Page Title], [Meta Keywords], [Last Date Modify], [User ID], Stock, [Origin Country], " & Chr(13) & _
                    "[Order Position], [Hot Product], [VAT Group], [From Date], [Item Same Model], [Unit Of Measure], [Good Going On], [Num Click], [Link URL] " & Chr(13) & _
                    "FROM Item " & cCond
            tblData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, cSql).Tables(0)
            tblData.TableName = "products"
            Return toJSON(tblData)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Function GetProductByCat(Optional sCat As String = "") As String
        Dim cCond As String = "Where  [Category No_]='" & sCat & "'"
        Dim cSql As String
        Dim tblData As DataTable
        Try
            cSql = "SELECT [No_], [Name], [Model], [Brand No_], [Division No_], [Category No_], [Produc Group No_], [Menu Division No_], " & Chr(13) & _
                    "[Menu Category No_], [Menu Group No_], [Promotions Product], [Best Selling], [New Product], [Hide Feature], " & Chr(13) & _
                    "[Not In Stock], [VAT Percent], [Descriptions], [Published], [Sales Price], [Price Icl VAT], [Images URL], " & Chr(13) & _
                    "[Images Thum URL], [Page Title], [Meta Keywords], [Last Date Modify], [User ID], Stock, [Origin Country], " & Chr(13) & _
                    "[Order Position], [Hot Product], [VAT Group], [From Date], [Item Same Model], [Unit Of Measure], [Good Going On], [Num Click], [Link URL] " & Chr(13) & _
                    "FROM Item " & cCond
            tblData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, cSql).Tables(0)
            tblData.TableName = "products"
            Return toJSON(tblData)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
   
    Function GetPriceList(Optional ItemNo As String = "") As String

        Dim cCond As String = IIf(ItemNo.Trim <> "", "Where  [Item No_]='" & ItemNo & "'", "")
        Dim cSql As String
        Dim tblData As DataTable
        Try
            cSql = "SELECT [Item No_],[Unit Price],[Price Inc VAT] FROM [iShop].[dbo].[Sales Price]" & cCond
            tblData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, cSql).Tables(0)
            Return toJSON(tblData)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
   
    Function toJSON(tData As DataTable) As String
        Dim objJSONStringBuilder = New StringBuilder()
        If ReturnIfNull(tData.TableName, "Table") <> "Table" Then
            objJSONStringBuilder.Append("{""" & tData.TableName & """:""")
            objJSONStringBuilder.Append("[")
        End If

        If Not tData Is Nothing Then

            For i As Integer = 0 To tData.Rows.Count - 1
                objJSONStringBuilder.Append("{")
                For j As Integer = 0 To tData.Columns.Count - 1
                    objJSONStringBuilder.Append("""" & tData.Columns(j).ColumnName & """")
                    objJSONStringBuilder.Append(":")
                    objJSONStringBuilder.Append("""" & tData.Rows(i)(j).ToString & """")
                    If j < tData.Columns.Count - 1 Then objJSONStringBuilder.Append(", ")
                Next
                objJSONStringBuilder.Append("},")
            Next
        End If
        objJSONStringBuilder.Remove(objJSONStringBuilder.Length - 1, 1)
        If ReturnIfNull(tData.TableName, "Table") <> "Table" Then
            objJSONStringBuilder.Append("]""")
            objJSONStringBuilder.Append("}")

        End If

        Return objJSONStringBuilder.ToString
    End Function
   
End Module

