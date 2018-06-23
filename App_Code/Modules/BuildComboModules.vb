Imports Microsoft.VisualBasic
Imports System.Data

Public Module BuildComboModules
    Public Sub BuildCombo(ByVal objCbo As Object, ByVal iList As adv.Business.List, Optional ByVal sConditions As String = "", Optional ByVal EmptyFirstRow As Boolean = False, Optional ByVal TextFirstRow As String = "")
        Dim tbl As DataTable
        Select Case iList
            Case adv.Business.List.Division
                tbl = GetDivisionList(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.ItemCategory
                tbl = GetItemCategoryList(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.ProductGroup
                tbl = GetProductGroupList(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.MenuGroup
                tbl = GetMenuGroup(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.Brand
                tbl = GetBrand(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.VATGroup
                tbl = GetVATGroup(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.UnitOfMeasure
                tbl = GetUnitOrMeasure(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.SalesPriceType
                tbl = GetSalesPriceType(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.StoreGroup
                tbl = GetArea(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.Feature
                tbl = GetFeature(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.Province
                tbl = GetProvince(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.District
                tbl = GetDistrict(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0

            Case adv.Business.List.Ward
                tbl = GetWard(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0

            Case adv.Business.List.GroupContent
                tbl = GetGroupContent(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0

            Case adv.Business.List.Modules
                tbl = GetModules(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
            Case adv.Business.List.UserGroup
                tbl = GetUserGroup(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0

            Case adv.Business.List.TypeOfContent
                tbl = GetContentType(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0

            Case adv.Business.List.Page
                tbl = GetListPage(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0

            Case adv.Business.List.Zone
                tbl = GetListZone(sConditions)
                If EmptyFirstRow Then
                    tbl.Rows.InsertAt(tbl.NewRow, 0)
                    tbl.Rows(0).Item("No_") = ""
                    tbl.Rows(0).Item("Name") = TextFirstRow
                End If
                objCbo.DataSource = tbl
                objCbo.DataTextField = "Name"
                objCbo.DataValueField = "No_"
                objCbo.DataBind()
                objCbo.SelectedIndex = 0
        End Select
    End Sub

    Function GetListZone(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_ , Name FROM Zone " & _
                " WHERE 1 = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY 2 "

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function


    Function GetListPage(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_ , Name FROM [Page] " & _
                " WHERE 1 = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY 2 "

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetContentType(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT CONVERT(nvarchar(20),No_) No_ , Name FROM [Content Type] " & _
                " WHERE 1 = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY 2 "

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function


    Function GetWard(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, [Ward Name] Name FROM Ward " & _
                " WHERE 1 = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY 2 "

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetUserGroup(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, [Group Name] Name FROM [User Group] " & _
                " WHERE 1 = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY 2 "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetModules(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM Modules " & _
                " WHERE 1 = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY 2 "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetGroupContent(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT [Group No_] No_, [Group Name] Name FROM [Content Group] " & _
                " WHERE 1 = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY 2 "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetDistrict(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM Province " & _
                " WHERE Type = 1 And Published = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY Name "

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetProvince(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM Province " & _
                " WHERE Type = 0 And Published = 1 "
        If sConditions.Trim <> "" Then
            SQL &= " AND " & sConditions
        End If
        SQL &= " ORDER BY [Big City] DESC, Name "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetFeature(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM [Feature Group] "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY Name "

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetArea(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM Area "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY [All Area] DESC, No_ "

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetSalesPriceType(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM [Sales Price Type] "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY 1 "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetUnitOrMeasure(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM [Unit Of Measure] "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY 1 "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetVATGroup(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM [VAT Group] "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY [VAT Percent] "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetBrand(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM Brand "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY 1 "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function


    Function GetDivisionList(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM Division "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY 1 "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetItemCategoryList(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM [Item Category] "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY 1 "

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetProductGroupList(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String
        SQL = " SELECT No_, Name FROM [Product Group] "
        If sConditions.Trim <> "" Then
            SQL &= " WHERE " & sConditions
        End If
        SQL &= " ORDER BY 1 "
        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function

    Function GetMenuGroup(Optional ByVal sConditions As String = "") As DataTable
        Dim SQL As String, sWHERE As String = ""
        If sConditions.Trim <> "" Then
            sWHERE = " AND " & sConditions
        End If

        SQL = " SELECT No_ [DivNo], No_, Name, [Menu Order] [DivOrder], 0 [CatOrder], 0 [GrOrder] FROM [Good Menu] WHERE [Menu Type] = 0 " & sWHERE & _
                " UNION ALL " & _
                " SELECT C.[Parent No_] [DivNo] , C.No_,'___' + C.Name, D.[Menu Order] [DivOrder], C.[Menu Order] + 1 [CatOrder], 1 [GrOrder] " & _
                " FROM [Good Menu] C LEFT JOIN [Good Menu] D ON C.[Parent No_] = D.No_ WHERE C.[Menu Type] = 1  " & sWHERE & _
                " UNION ALL " & _
                " SELECT D.No_ [DivNo], G.No_,'______' +  G.Name, D.[Menu Order] [DivOrder], C.[Menu Order] + 1 [CatOrder], G.[Menu Order] + 2  [GrOrder] " & _
                " FROM [Good Menu] G LEFT JOIN [Good Menu] C ON G.[Parent No_] = C.No_ LEFT JOIN [Good Menu] D ON C.[Parent No_] = D.No_ " & _
                " WHERE G.[Menu Type] = 2 " & sWHERE & _
                " ORDER BY 4 DESC,5,6"

        Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
    End Function
End Module
